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
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Control
{
    /// <summary>
    /// Config for injector node
    /// </summary>
    [Serializable]
    public class InjectorNodeConfig
    {
        [LocalizedDescription("InjectorNodeConfig_PacketLog", typeof(Properties.Resources)), TypeConverter(typeof(DocumentChoiceConverter<PacketLogDocument>))]
        public PacketLogDocument PacketLog { get; set; }

        [LocalizedDescription("InjectorNodeConfig_Sequential", typeof(Properties.Resources))]
        public bool Sequential { get; set; }

        [LocalizedDescription("InjectorNodeConfig_Repeat", typeof(Properties.Resources))]
        public bool Repeat { get; set; }

        [LocalizedDescription("InjectorNodeConfig_Prefix", typeof(Properties.Resources))]
        public bool Prefix { get; set; }

        [LocalizedDescription("InjectorNodeConfig_DropTrigger", typeof(Properties.Resources))]
        public bool DropTrigger { get; set; }
    }

    [NodeLibraryClass("InjectorNode_Name", "InjectorNode_Description", "InjectorNode_NodeName", typeof(Properties.Resources),        
        ConfigType=typeof(InjectorNodeConfig),        
        Category = NodeLibraryClassCategory.Control)]
    public class InjectorNode : BasePipelineNodeWithPersist<InjectorNodeConfig>
    {
        private bool _init;
        private bool _sent;
        private bool _prefix;
        private bool _drop;
        private int _currPacket;
        private LogPacket[] _packets;

        private void SendPacket(DataFrames.DataFrame frame)
        {
            if (!_drop)
            {
                WriteOutput(frame);
            }
        }

        protected override void OnInput(DataFrames.DataFrame frame)
        {
            if ((!_init) && (Config.PacketLog != null))
            {
                _packets = Config.PacketLog.GetPackets();
                _prefix = Config.Prefix;
                _drop = Config.DropTrigger;
                _currPacket = 0;
                _init = true;
            }

            if (!_prefix)
            {
                SendPacket(frame.CloneFrame());
            }

            if (_init)
            {
                if (Config.Sequential)
                {
                    if (_currPacket < _packets.Length)
                    {
                        WriteOutput(_packets[_currPacket++].Frame.CloneFrame());
                    }

                    if (_currPacket == _packets.Length)
                    {
                        if (Config.Repeat)
                        {
                            _currPacket = 0;
                        }
                    }
                }
                else if(!_sent)
                {
                    foreach (LogPacket packet in _packets)
                    {
                        WriteOutput(packet.Frame.CloneFrame());
                    }

                    if (!Config.Repeat)
                    {
                        _sent = true;
                    }
                }
            }

            if (_prefix)
            {
                SendPacket(frame.CloneFrame());
            }
        }
    }
}
