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
using CANAPE.Nodes;
using System.Collections.Generic;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// Config for switch node
    /// </summary>
    [Serializable]
    public class SwitchNodeFactory : BaseNodeFactory
    {        
        /// <summary>
        /// Whether to drop unknown packets
        /// </summary>
        public bool DropUnknown { get; set; }
        /// <summary>
        /// Switch node selection mode
        /// </summary>
        public SwitchNodeSelectionMode Mode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">Label of the node</param>
        /// <param name="guid">Guid of the node</param>
        /// <param name="dropUnknown">Whether to drop unknown packets</param>
        /// <param name="mode">The mode to determine the path to take</param>
        public SwitchNodeFactory(string label, Guid guid, bool dropUnknown, SwitchNodeSelectionMode mode)
            : base(label, guid)
        {            
            DropUnknown = dropUnknown;
            Mode = mode;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="graph">The hosting graph</param>
        /// <param name="stateDictionary"></param>
        /// <returns>The created pipeline node</returns>
        protected override Nodes.BasePipelineNode OnCreate(Utils.Logger logger, Nodes.NetGraph graph, Dictionary<string, object> stateDictionary)
        {
            return new SwitchNode(DropUnknown, Mode);
        }
    }
}
