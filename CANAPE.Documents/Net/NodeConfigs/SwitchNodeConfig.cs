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
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Node config for a switch node
    /// </summary>
    [Serializable]
    public class SwitchNodeConfig : BaseNodeConfig
    {
        private bool _dropUnknown;
        private SwitchNodeSelectionMode _mode;

        /// <summary>
        /// Get or set whether to drop unknown packets
        /// </summary>
        [LocalizedDescription("SwitchNodeConfig_DropUnknownDescription", typeof(Properties.Resources)), Category("Control")]
        public bool DropUnknown
        {
            get
            {
                return _dropUnknown;
            }

            set 
            {
                if(_dropUnknown != value)
                {
                    _dropUnknown = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// The mode of selection
        /// </summary>
        [LocalizedDescription("SwitchNodeConfig_ModeDescription", typeof(Properties.Resources)), Category("Control")]
        public SwitchNodeSelectionMode Mode
        {
            get { return _mode; }
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Node template name
        /// </summary>
        public const string NodeName = "switchnode";

        /// <summary>
        /// Get the node name
        /// </summary>
        /// <returns>Returns "switchnode"</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }

        /// <summary>
        /// Create the factory
        /// </summary>
        /// <returns>The node factory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            return new SwitchNodeFactory(_label, _id, _dropUnknown, _mode);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SwitchNodeConfig()
        {
            _selectionPath = "/replace.me/";
            _mode = SwitchNodeSelectionMode.ExactMatch;
        }
    }
}
