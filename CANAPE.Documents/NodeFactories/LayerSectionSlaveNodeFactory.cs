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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// A slave layer section node
    /// </summary>
    public class LayerSectionSlaveNodeFactory : BaseNodeFactory
    {
        /// <summary>
        /// Master node factory
        /// </summary>
        public LayerSectionMasterNodeFactory MasterFactory { get; private set; }        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">The label of the node</param>
        /// <param name="uuid">The UUID of the node</param>
        /// <param name="master">The master node factory</param>
        public LayerSectionSlaveNodeFactory(string label, Guid uuid, LayerSectionMasterNodeFactory master) 
            : base(label, uuid)
        {
            MasterFactory = master;
        }

        /// <summary>
        /// Create the node if it doesn't exist
        /// </summary>
        /// <param name="logger">The logger to use when creating</param>
        /// <param name="graph">The associated netgraph</param>
        /// <param name="stateDictionary">The state dictionary</param>
        /// <returns>The created node</returns>
        protected override BasePipelineNode OnCreate(Logger logger, NetGraph graph, Dictionary<string, object> stateDictionary)
        {
            // If this is not the linked node use the master to create it and store master
            if (!stateDictionary.ContainsKey(Id.ToString()))
            {                
                stateDictionary[MasterFactory.Id.ToString()] = MasterFactory.Create(logger, graph, stateDictionary);
            }

            return (BasePipelineNode)stateDictionary[Id.ToString()];
        }
    }
}
