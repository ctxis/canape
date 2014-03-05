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
using System.Linq;
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
    public class SequentialReplayEndpointConfig
    {
        /// <summary>
        /// Convert to basic
        /// </summary>
        [LocalizedDescription("SequentialReplayEndpointConfig_ConvertToBasicDescription", typeof(Properties.Resources)), Category("Control")]
        public bool ConvertToBasic { get; set; }

        /// <summary>
        /// Packet log document
        /// </summary>
        [LocalizedDescription("SequentialReplayEndpointConfig_PacketsDescription", typeof(Properties.Resources)), Category("Control"),
            TypeConverter(typeof(DocumentChoiceConverter<PacketLogDocument>))]
        public PacketLogDocument Packets { get; set; }

        /// <summary>
        /// Close after send
        /// </summary>
        [LocalizedDescription("SequentialReplayEndpointConfig_CloseAfterSendDescription", typeof(Properties.Resources)), Category("Control")]
        public bool CloseAfterSend { get; set; }

        /// <summary>
        /// Inidicates that the endpoint should wait for a response before closing the connection
        /// </summary>
        [LocalizedDescription("SequentialReplayEndpointConfig_WaitForResponseOnCloseDescription", typeof(Properties.Resources)), Category("Control")]
        public bool WaitForResponseOnClose { get; set; }

        [LocalizedDescription("SequentialReplayEndpointConfig_Tag", typeof(Properties.Resources)), Category("Control")]
        public string Tag { get; set; }

        /// <summary>
        /// Indicates number of times to send the packets
        /// </summary>
        [LocalizedDescription("SequentialReplayEndpointConfig_RepeatCountDescription", typeof(Properties.Resources)), Category("Control")]
        public int RepeatCount { get; set; }

        /// <summary>
        /// List of filters to indicate what packets to respond to
        /// </summary>
        [LocalizedDescription("SequentialReplayEndpointConfig_FiltersDescription", typeof(Properties.Resources)), Category("Control")]
        public DataFrameFilterFactory[] Filters { get; set; }

        /// <summary>
        /// Whether all the filters need to match (AND) or just one (OR)
        /// </summary>
        [LocalizedDescription("SequentialReplayEndpointConfig_MatchAllFiltersDescription", typeof(Properties.Resources)), Category("Control")]
        public bool MatchAllFilters { get; set; }

        /// <summary>
        /// Whether to send a packet at start
        /// </summary>
         [LocalizedDescription("SequentialReplayEndpointConfig_SendAtStartDescription", typeof(Properties.Resources)), Category("Control")]
        public bool SendAtStart { get; set; }

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

        internal LogPacket[] GetPackets()
        {
            LogPacket[] ret = null;

            if (!String.IsNullOrWhiteSpace(Tag))
            {
                ret = Packets.GetPacketsByTag(Tag);
            }
            else
            {
                ret = Packets.Packets.ToArray();
            }

            return ret;
        }

        public SequentialReplayEndpointConfig()
        {
            RepeatCount = 1;
            Filters = new DataFrameFilterFactory[0];
        }
    }

    /// <summary>
    /// An endpoint which replays a packet log sequentially
    /// </summary>
    [NodeLibraryClass("SequentialReplayEndpoint", typeof(Properties.Resources),
            Category = NodeLibraryClassCategory.Endpoint,
            ConfigType = typeof(SequentialReplayEndpointConfig))]
    public class SequentialReplayEndpoint : BasePersistDataEndpoint<SequentialReplayEndpointConfig>
    {
        public override void Run(DataAdapters.IDataAdapter adapter, Logger logger)
        {
            LogPacket[] packets = Config.GetPackets();
            DataFrameFilterExpression filters = Config.CreateFilters();
            int pos = 0;
            int repeat = 0;
            bool finished = false;
            bool sendatstart = Config.SendAtStart;

            do
            {
                bool matchedFrame = false;

                if (sendatstart)
                {
                    matchedFrame = true;
                    sendatstart = false;
                }
                else
                {
                    DataFrame frame = adapter.Read();

                    if ((frame == null) || (finished && Config.CloseAfterSend))
                    {
                        break;
                    }

                    matchedFrame = filters.IsMatch(frame);
                }

                if (matchedFrame)
                {
                    if (!finished)
                    {
                        // Send if any packets left
                        if (pos < packets.Length)
                        {
                            DataFrame current = packets[pos++].Frame.CloneFrame();
                            if (Config.ConvertToBasic)
                            {
                                current.ConvertToBasic();
                            }

                            adapter.Write(current);
                        }

                        if (pos == packets.Length)
                        {
                            repeat++;
                            pos = 0;
                        }

                        if (repeat >= Config.RepeatCount)
                        {
                            finished = true;

                            if (!Config.WaitForResponseOnClose && Config.CloseAfterSend)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            while (true);
        }

        public override string Description
        {
            get { return "Sequential Replay Endpoint"; }
        }

        protected override void ValidateConfig(SequentialReplayEndpointConfig config)
        {
            if (config.Packets == null)
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.SequentialReplayEndpoint_PacketLogIsNull);
            }
        }
    }
}
