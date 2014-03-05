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
    /// Config for an edit node
    /// </summary>
    [Serializable]
    public class EditPacketNodeConfig : BaseNodeConfig
    {        
        string _tag;
        ColorValue _color;

        /// <summary>
        /// The name of the node
        /// </summary>
        public const string NodeName = "editnode";

        /// <summary>
        /// Get or set the colour to show in the edit window
        /// </summary>
        [LocalizedDescription("EditPacketNodeConfig_ColorDescription", typeof(Properties.Resources)), Category("Behavior")]
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
        /// Get or set the textual tag to show in edit window
        /// </summary>
        [LocalizedDescription("EditPacketNodeConfig_TagDescription", typeof(Properties.Resources)), Category("Behavior")]
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
        /// Get node name
        /// </summary>
        /// <returns>The node name</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }

        /// <summary>
        /// Method called to create the factory
        /// </summary>
        /// <returns>The BaseNodeFactory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            return new EditPacketNodeFactory(_label, _id, _color, _tag);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public EditPacketNodeConfig()
        {
            Color = new ColorValue(255, 255, 255, 255);
        }
    }
}
