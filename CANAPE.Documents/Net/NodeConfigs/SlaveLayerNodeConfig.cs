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
using CANAPE.NodeFactories;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// A config which represents the slave node of a layer, this is generic for all slaves
    /// </summary>
    [Serializable]
    public sealed class SlaveLayerNodeConfig : BaseNodeConfig, ILinkedNodeConfig
    {        
        /// <summary>
        /// The master node
        /// </summary>
        public MasterLayerNodeConfig Master { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="master">The associated master node config</param>
        public SlaveLayerNodeConfig(MasterLayerNodeConfig master)
        {
            Master = master;            
        }

        /// <summary>
        /// The node name from the master
        /// </summary>
        /// <returns>The node name from the master config</returns>
        public override string GetNodeName()
        {
            return Master.GetNodeName();
        }

        /// <summary>
        /// Creates a factory
        /// </summary>
        /// <returns></returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            LayerSectionMasterNodeFactory masterFactory = Master.CreateFactory() as LayerSectionMasterNodeFactory;

            return masterFactory.SlaveFactory;
        }

        /// <summary>
        /// Get linked node
        /// </summary>
        public BaseNodeConfig LinkedNode
        {
            get { return Master; }
        }

        /// <summary>
        /// Get master factory
        /// </summary>
        /// <param name="linkedNode">The linked node factory</param>
        /// <returns>The factory</returns>
        public BaseNodeFactory CreateFactory(BaseNodeFactory linkedNode)
        {
            LayerSectionSlaveNodeFactory factory = linkedNode as LayerSectionSlaveNodeFactory;

            return factory.MasterFactory;
        }
    }
}
