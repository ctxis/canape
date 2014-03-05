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
using CANAPE.NodeFactories;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Config for a delay node
    /// </summary>
    [Serializable]
    public class DelayNodeConfig : BaseNodeConfig
    {
        private int _packetDelay;

        /// <summary>
        /// Name of graph node
        /// </summary>
        public const string NodeName = "delaynode";

        /// <summary>
        /// Get graph node name
        /// </summary>
        /// <returns>Always "server"</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DelayNodeConfig()
        {
            PacketDelayMs = 100;
        }

        /// <summary>
        /// Packet delay in milli-seconds
        /// </summary>
        [LocalizedDescription("DelayNodeConfig_PacketDelayMsDescription", typeof(Properties.Resources)), Category("Behavior")]
        public int PacketDelayMs
        {
            get { return _packetDelay; }
            set
            {
                if (_packetDelay != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentException(CANAPE.Documents.Properties.Resources.DirectNode_InvalidPacketDelay);
                    }

                    _packetDelay = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Method called to create the factory
        /// </summary>
        /// <returns>The BaseNodeFactory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            if (_packetDelay > 0)
            {
                DelayNodeFactory ret = new DelayNodeFactory(_label, _id);
                ret.PacketDelayMs = _packetDelay;

                return ret;
            }
            else
            {
                // If invalid delay create a direct node
                return new DirectNodeFactory(_label, _id);
            }
        }
    }
}
