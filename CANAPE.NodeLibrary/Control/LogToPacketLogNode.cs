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
using CANAPE.DataFrames;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Control
{
    [Serializable]
    public class LogToPacketLogNodeConfig
    {
        /// <summary>
        /// The color of the log entry
        /// </summary>
        [LocalizedDescription("LogToPacketLogNodeConfig_ColorDescription", typeof(Properties.Resources))]
        public ColorValue Color { get; set; }
        /// <summary>
        /// A textual tag to log
        /// </summary>
        [LocalizedDescription("LogToPacketLogNodeConfig_TagDescription", typeof(Properties.Resources))]
        public string Tag { get; set; }
        /// <summary>
        /// True indicates all packets are converted to bytes before being logged
        /// </summary>
        [LocalizedDescription("LogToPacketLogNodeConfig_ConvertToBytesDescription", typeof(Properties.Resources))]
        public bool ConvertToBytes { get; set; }
        /// <summary>
        /// The packet log to log to 
        /// </summary>
        [LocalizedDescription("LogToPacketLogNodeConfig_PacketLogDescription", typeof(Properties.Resources)), TypeConverter(typeof(DocumentChoiceConverter<PacketLogDocument>))]
        public PacketLogDocument PacketLog { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogToPacketLogNodeConfig()
        {
            Color = new ColorValue(255, 255, 255, 255);
        }
    }

    [NodeLibraryClass("LogToPacketLogNode_Name", "LogToPacketLogNode_Description", "LogToPacketLogNode_NodeName", typeof(Properties.Resources),
        ConfigType = typeof(LogToPacketLogNodeConfig),
        Category = NodeLibraryClassCategory.Control)]
    public class LogToPacketLogNode : BasePipelineNodeWithPersist<LogToPacketLogNodeConfig>
    {
        protected override void OnInput(DataFrame frame)
        {
            DataFrame logFrame;

            if (Config.ConvertToBytes)
            {
                logFrame = new DataFrame(frame.ToArray());
            }
            else
            {
                logFrame = frame.CloneFrame();
            }

            LogPacket packet = new LogPacket(Config.Tag, Graph.Uuid, Guid.NewGuid(), 
                Graph.NetworkDescription, logFrame, Config.Color, DateTime.Now);

            Config.PacketLog.AddPacket(packet);

            WriteOutput(frame);  
        }

        protected override void ValidateConfig(LogToPacketLogNodeConfig config)
        {
            if (config.PacketLog == null)
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.LogToPacketLogNode_PacketLogIsNull);
            }
        }
    }
}
