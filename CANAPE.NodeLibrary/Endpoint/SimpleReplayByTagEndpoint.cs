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
    [Serializable]
    public class SimpleReplayByTagEndpointConfig
    {
        /// <summary>
        /// Convert to basic
        /// </summary>
        [LocalizedDescription("SimpleReplayByTagEndpointConfig_ConvertToBasicDescription", typeof(Properties.Resources)), Category("Control")]
        public bool ConvertToBasic { get; set; }

        /// <summary>
        /// Packet log document
        /// </summary>
        [LocalizedDescription("SimpleReplayByTagEndpointConfig_PacketsDescription", typeof(Properties.Resources)), Category("Control"), 
            TypeConverter(typeof(DocumentChoiceConverter<PacketLogDocument>))]
        public PacketLogDocument Packets { get; set; }

        /// <summary>
        /// Close after send
        /// </summary>
        [LocalizedDescription("SimpleReplayByTagEndpointConfig_CloseAfterMatchDescription", typeof(Properties.Resources)), Category("Control")]
        public bool CloseAfterMatch { get; set; }

        [LocalizedDescription("SimpleReplayByTagEndpointConfig_CloseAfterNonMatchDescription", typeof(Properties.Resources)), Category("Control")]
        public bool CloseAfterNonMatch { get; set; }

        /// <summary>
        /// List of filters
        /// </summary>
        [LocalizedDescription("SimpleReplayByTagEndpointConfig_FiltersDescription", typeof(Properties.Resources)), Category("Control")]
        public DataFrameFilterFactory[] Filters { get; set; }

        /// <summary>
        /// Whether all the filters need to match (AND) or just one (OR)
        /// </summary>
        [LocalizedDescription("SimpleReplayByTagEndpointConfig_MatchAllFiltersDescription", typeof(Properties.Resources)), Category("Control")]
        public bool MatchAllFilters { get; set; }

        [LocalizedDescription("SimpleReplayByTagEndpointConfig_TagOnMatchDescription", typeof(Properties.Resources)), Category("Control")]
        public string TagOnMatch { get; set; }

        [LocalizedDescription("SimpleReplayByTagEndpointConfig_RepeatOnMatchDescription", typeof(Properties.Resources)), Category("Control")]
        public int RepeatOnMatch { get; set; }

        [LocalizedDescription("SimpleReplayByTagEndpointConfig_TagOnNonMatchDescription", typeof(Properties.Resources)), Category("Control")]
        public string TagOnNonMatch { get; set; }

        [LocalizedDescription("SimpleReplayByTagEndpointConfig_RepeatOnNonMatchDescription", typeof(Properties.Resources)), Category("Control")]
        public int RepeatOnNonMatch { get; set; }

        [LocalizedDescription("SimpleReplayByTagEndpointConfig_TagOnStartDescription", typeof(Properties.Resources)), Category("Control")]
        public string TagOnStart { get; set; }

        [LocalizedDescription("SimpleReplayByTagEndpointConfig_RepeatOnStartDescription", typeof(Properties.Resources)), Category("Control")]
        public int RepeatOnStart { get; set; }

        public SimpleReplayByTagEndpointConfig()
        {
            Filters = new DataFrameFilterFactory[0];
            RepeatOnMatch = 1;
            RepeatOnNonMatch = 1;
            RepeatOnStart = 1;
        }

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
    }

    [NodeLibraryClass("SimpleReplayByTagEndpoint", typeof(Properties.Resources),
            Category = NodeLibraryClassCategory.Endpoint,
            ConfigType = typeof(SimpleReplayByTagEndpointConfig))]
    public class SimpleReplayByTagEndpoint : BasePersistDataEndpoint<SimpleReplayByTagEndpointConfig>
    {
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
            DataFrameFilterExpression filters = Config.CreateFilters();

            if (!String.IsNullOrWhiteSpace(Config.TagOnStart))
            {
                LogPacket[] packets = Config.Packets.GetPacketsByTag(Config.TagOnStart);

                for(int i = 0; i < Config.RepeatOnStart; ++i)
                {
                    SendPacketLog(adapter, packets, logger);
                }
            }

            do
            {
                frame = adapter.Read();

                if (frame != null)
                {
                    if (filters.IsMatch(frame, Meta, GlobalMeta, null, Guid.Empty, null))
                    {
                        if (!String.IsNullOrWhiteSpace(Config.TagOnMatch))
                        {
                            LogPacket[] packets = Config.Packets.GetPacketsByTag(Config.TagOnMatch);

                            for (int i = 0; i < Config.RepeatOnMatch; ++i)
                            {
                                SendPacketLog(adapter, packets, logger);
                            }
                        }

                        if(Config.CloseAfterMatch)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(Config.TagOnNonMatch))
                        {
                            LogPacket[] packets = Config.Packets.GetPacketsByTag(Config.TagOnNonMatch);

                            for (int i = 0; i < Config.RepeatOnNonMatch; ++i)
                            {
                                SendPacketLog(adapter, packets, logger);
                            }
                        }

                        if(Config.CloseAfterNonMatch)
                        {
                            break;
                        }
                    }
                }
            }
            while (frame != null);
        }

        public override string Description
        {
            get { return "Simple Replay by Tag Endpoint"; }
        }

        protected override void ValidateConfig(SimpleReplayByTagEndpointConfig config)
        {
            if (config.Packets == null)
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.ReplayByTagDataServer_PacketLogIsNull);
            }
        }
    }
}
