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
	public class SimpleReplayEndpointConfig
	{
        /// <summary>
        /// Convert to basic
        /// </summary>
        [LocalizedDescription("SimpleReplayEndpointConfig_ConvertToBasicDescription", typeof(Properties.Resources)), Category("Control")]
	    public bool ConvertToBasic { get; set; }

        /// <summary>
        /// Packet log document
        /// </summary>
        [LocalizedDescription("SimpleReplayEndpointConfig_PacketsDescription", typeof(Properties.Resources)), 
            Category("Control"), TypeConverter(typeof(DocumentChoiceConverter<PacketLogDocument>))]
	    public PacketLogDocument Packets { get; set; }

        /// <summary>
        /// Close after send
        /// </summary>
        [LocalizedDescription("SimpleReplayEndpointConfig_CloseAfterSendDescription", typeof(Properties.Resources)), Category("Control")]
        public bool CloseAfterSend { get; set; }

        [LocalizedDescription("SimpleReplayEndpointConfig_CloseAfterRecvDescription", typeof(Properties.Resources)), Category("Control")]
        public bool CloseAfterRecv { get; set; }

        [LocalizedDescription("SimpleReplayEndpointConfig_SendAtStartDescription", typeof(Properties.Resources)), Category("Control")]
        public bool SendAtStart { get; set; }
    }

    [NodeLibraryClass("SimpleReplayDataEndpoint", typeof(Properties.Resources),
            Category = NodeLibraryClassCategory.Endpoint,
            ConfigType = typeof(SimpleReplayEndpointConfig))]
    public class SimpleReplayDataEndpoint : BasePersistDataEndpoint<SimpleReplayEndpointConfig>
    {
        private void SendPacketLog(IDataAdapter adapter, PacketLogDocument doc, Logger logger)
        {
            LogPacket[] packets = doc.GetPackets();

            foreach (LogPacket p in packets)
            {
                DataFrame newFrame = p.Frame.CloneFrame();

                if (Config.ConvertToBasic)
                {
                    newFrame.ConvertToBasic();
                }

                adapter.Write(newFrame);
            }
        }

        public override void Run(IDataAdapter adapter, Logger logger)
        {
            DataFrame frame = null;

            if (Config.SendAtStart)
            {
                SendPacketLog(adapter, Config.Packets, logger);
            }
            
            do
            {
                frame = adapter.Read();
                
                if(frame != null)
                {
                    if (Config.CloseAfterRecv)
                    {
                        break;
                    }

                    SendPacketLog(adapter, Config.Packets, logger);
                    if (Config.CloseAfterSend)
                    {
                        break;
                    }
                }
            }
            while (frame != null);
        }

        public override string Description { get { return "Simple Replay Server"; } }

        protected override void ValidateConfig(SimpleReplayEndpointConfig config)
        {
            if (config.Packets == null)
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.SimpleReplayDataEndpoint_PacketLogIsNull);
            }
        }
    }
}
