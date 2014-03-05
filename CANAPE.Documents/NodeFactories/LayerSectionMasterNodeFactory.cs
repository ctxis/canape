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
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// Factory for layer section node
    /// </summary>    
    public class LayerSectionMasterNodeFactory : BaseNodeFactory
    {        
        /// <summary>
        /// Layer factories
        /// </summary>
        public LayerSectionFilterFactory[] LayerFactories { get; set; }

        /// <summary>
        /// Default mode with node match
        /// </summary>
        public LayerSectionNodeDefaultMode DefaultMode { get; set; }

        /// <summary>
        /// Direction of layer
        /// </summary>
        public LayerSectionGraphDirection Direction { get; set; }

        /// <summary>
        /// Slave Node
        /// </summary>
        public LayerSectionSlaveNodeFactory SlaveFactory { get; private set; }

        /// <summary>
        /// OnCreate method
        /// </summary>
        /// <param name="logger">The logger for this graph</param>
        /// <param name="graph">The associated netgraph</param>
        /// <param name="stateDictionary">State dictionary</param>
        /// <returns>The new pipeline node</returns>
        protected override BasePipelineNode OnCreate(Logger logger, NetGraph graph, Dictionary<string, object> stateDictionary)
        {
            // If this node has been created by a slave return as appropriate
            if (stateDictionary.ContainsKey(Id.ToString()))
            {
                return (BasePipelineNode)stateDictionary[Id.ToString()];
            }
            
            List<LayerSectionFilter> filters = new List<LayerSectionFilter>();

            foreach (LayerSectionFilterFactory factory in LayerFactories)
            {
                filters.Add(factory.Create());
            }

            LayerSectionNode ret = new LayerSectionNode(filters.ToArray(), DefaultMode, LayerSectionGraphDirection.ServerToClient);
            
            // Create paired slave node
            stateDictionary[SlaveFactory.Id.ToString()] = new LayerSectionNode(ret);

            return ret;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">The node label</param>
        /// <param name="uuid">The guid of the node</param>
        /// <param name="slaveuuid">Slave guid</param>
        public LayerSectionMasterNodeFactory(string label, Guid uuid, Guid slaveuuid) 
            : base(label, uuid)
        {
            SlaveFactory = new LayerSectionSlaveNodeFactory(label + "-Slave", slaveuuid, this);
        }
    }
}
