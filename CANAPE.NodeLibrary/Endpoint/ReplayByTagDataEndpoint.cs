//    CANAPE Network Testing Tool
//    Copyright (C) 2014 Context Information Security
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.DataFrames.Filters;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Endpoint
{
    /// <summary>
    /// A server which replays packets when it matches a filter and sends based on a tag
    /// </summary>
    [Serializable]
    public class ReplayByTagDataEndpointConfig
    {
        [Serializable]
        public class ReplayByTagEntryFactory
        {
            /// <summary>
            /// The tag in the packet log to determine what packets to send
            /// </summary>
            [LocalizedDescription("ReplayByTagEntryFactory_TagDescription", typeof(Properties.Resources)), Category("Control")]
            public string Tag { get; set; }

            /// <summary>
            /// Close after send
            /// </summary>
            [LocalizedDescription("ReplayByTagEntryFactory_CloseAfterSendDescription", typeof(Properties.Resources)), Category("Control")]
            public bool CloseAfterSend { get; set; }

            /// <summary>
            /// List of filters
            /// </summary>
            [LocalizedDescription("ReplayByTagEntryFactory_FiltersDescription", typeof(Properties.Resources)), Category("Control")]
            public DataFrameFilterFactory[] Filters { get; set; }

            /// <summary>
            /// Whether all the filters need to match (AND) or just one (OR)
            /// </summary>
            [LocalizedDescription("ReplayByTagEntryFactory_MatchAllFiltersDescription", typeof(Properties.Resources)), Category("Control")]
            public bool MatchAllFilters { get; set; }

            /// <summary>
            /// Indicates number of times to send the packet
            /// </summary>
            [LocalizedDescription("ReplayByTagEntryFactory_RepeatCountDescription", typeof(Properties.Resources)), Category("Control")]
            public int RepeatCount { get; set; }

            /// <summary>
            /// Create the filter list
            /// </summary>
            /// <returns></returns>
            internal DataFrameFilterExpression CreateFilters()
            {
                DataFrameFilterExpression filters = new DataFrameFilterExpression(MatchAllFilters);

                if (Filters != null)
                {
                    foreach (DataFrameFilterFactory factory in Filters)
                    {
                        filters.Add(factory.CreateFilter());
                    }
                }

                return filters;
            }

            public ReplayByTagEntryFactory()
            {
                Filters = new DataFrameFilterFactory[0];
                RepeatCount = 1;
            }
        }

        /// <summary>
        /// Convert to basic
        /// </summary>
        [LocalizedDescription("ReplayByTagDataEndpointConfig_ConvertToBasicDescription", typeof(Properties.Resources)), Category("Control")]
        public bool ConvertToBasic { get; set; }

        /// <summary>
        /// Packet log document
        /// </summary>
        [LocalizedDescription("ReplayByTagDataEndpointConfig_PacketsDescription", typeof(Properties.Resources)), Category("Control"), 
            TypeConverter(typeof(DocumentChoiceConverter<PacketLogDocument>))]
        public PacketLogDocument Packets { get; set; }

        [LocalizedDescription("ReplayByTagDataEndpointConfig_ReplayEntriesDescription", typeof(Properties.Resources)), Category("Control")]
        public ReplayByTagEntryFactory[] ReplayEntries { get; set; }

        [LocalizedDescription("ReplayByTagDataEndpointConfig_TagOnStartDescription", typeof(Properties.Resources)), Category("Control")]
        public string TagOnStart { get; set; }

        public ReplayByTagDataEndpointConfig()
        {
            ReplayEntries = new ReplayByTagEntryFactory[0];
        }
    }

    [NodeLibraryClass("ReplayByTagDataEndpoint", typeof(Properties.Resources),            
            Category = NodeLibraryClassCategory.Endpoint,
            ConfigType = typeof(ReplayByTagDataEndpointConfig))]
    public class ReplayByTagDataEndpoint : BasePersistDataEndpoint<ReplayByTagDataEndpointConfig>
    {
        private class ReplayByTagEntry
        {
            public DataFrameFilterExpression Filters;
            public string Tag;
            public bool CloseAfterSend;
            public int RepeatCount;
        }

        private void SendPacketLog(IDataAdapter adapter, LogPacket[] packets, Logger logger)
        {
            foreach (LogPacket p in packets)
            {
                DataFrame newFrame;

                if (Config.ConvertToBasic)
                {
                    newFrame = new DataFrame(p.Frame.ToArray());
                }
                else
                {
                    newFrame = p.Frame.CloneFrame();
                }

                adapter.Write(newFrame);         
            }
        }

        public override void Run(IDataAdapter adapter, Logger logger)
        {
            DataFrame frame = null;
            ReplayByTagEntry[] entries = new ReplayByTagEntry[Config.ReplayEntries.Length];            

            for (int i = 0; i < Config.ReplayEntries.Length; ++i)
            {
                entries[i] = new ReplayByTagEntry();
                entries[i].Filters = Config.ReplayEntries[i].CreateFilters();
                entries[i].Tag = Config.ReplayEntries[i].Tag;
                entries[i].CloseAfterSend = Config.ReplayEntries[i].CloseAfterSend;
                entries[i].RepeatCount = Config.ReplayEntries[i].RepeatCount;
            }

            if (!String.IsNullOrWhiteSpace(Config.TagOnStart))
            {
                SendPacketLog(adapter, Config.Packets.GetPacketsByTag(Config.TagOnStart), logger);
            }

            do
            {
                frame = adapter.Read();

                if (frame != null)
                {
                    foreach (ReplayByTagEntry entry in entries)
                    {
                        if (!String.IsNullOrWhiteSpace(entry.Tag) && entry.Filters.IsMatch(frame, Meta, GlobalMeta, null, Guid.Empty, null))
                        {
                            logger.Log(Logger.LogEntryType.Verbose, "Replay Server", Guid.Empty, "Server: Matched on {0}", frame);
                            LogPacket[] packets = Config.Packets.GetPacketsByTag(entry.Tag);

                            logger.Log(Logger.LogEntryType.Verbose, "Replay Server", Guid.Empty, "Server: Got {0} packets for tag {1}", packets.Length, entry.Tag);
                            for (int i = 0; i < entry.RepeatCount; ++i)
                            {
                                SendPacketLog(adapter, packets, logger);
                            }

                            if (entry.CloseAfterSend)
                            {
                                frame = null;
                                break;
                            }
                        }                        
                    }
                }
            }
            while (frame != null);
        }

        public override string Description
        {
            get { return "Replay by Tag Endpoint"; }
        }

        protected override void ValidateConfig(ReplayByTagDataEndpointConfig config)
        {
            if (config.Packets == null)
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.ReplayByTagDataServer_PacketLogIsNull);
            }
        }
    }
}
