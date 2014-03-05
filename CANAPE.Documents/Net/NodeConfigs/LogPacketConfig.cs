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
    /// Config for a log packet node
    /// </summary>
    [Serializable]
    public class LogPacketConfig : BaseNodeConfig
    {
        private ColorValue _color;
        private string _tag;
        private bool _convertToBytes;

        /// <summary>
        /// Get or set the colour of the log entries
        /// </summary>
        [LocalizedDescription("LogPacketConfig_ColorDescription", typeof(Properties.Resources)), Category("Behavior")]
        public ColorValue Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// The textual description assigned to the log entry
        /// </summary>
        [LocalizedDescription("LogPacketConfig_TagDescription", typeof(Properties.Resources)), Category("Behavior")]
        public string Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                if (_tag != value)
                {
                    _tag = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Get or set whether packets should be converted to bytes first
        /// </summary>
        [LocalizedDescription("LogPacketConfig_ConvertToBytesDescription", typeof(Properties.Resources)), Category("Behavior")]
        public bool ConvertToBytes
        {
            get
            {
                return _convertToBytes;
            }

            set
            {
                if (_convertToBytes != value)
                {
                    _convertToBytes = value;
                    SetDirty();
                }
            }
        }
        
        /// <summary>
        /// Get name of node
        /// </summary>
        public const string NodeName = "lognode";

        /// <summary>
        /// Get name of node
        /// </summary>
        /// <returns>The node name</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogPacketConfig()
        {
            Color = new ColorValue(255, 255, 255, 255);
        }

        /// <summary>
        /// Method to create the factory
        /// </summary>
        /// <returns>The node factory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            return new LogPacketNodeFactory(_label, _id, _color, _tag, _convertToBytes);
        }
    }
}
