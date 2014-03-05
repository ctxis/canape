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

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// A config which represents the master node of a layer
    /// </summary>
    [Serializable]
    public abstract class MasterLayerNodeConfig : BaseNodeConfig, ILinkedNodeConfig
    {
        /// <summary>
        /// The slave node config
        /// </summary>
        [Browsable(false)]
        public SlaveLayerNodeConfig Slave { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MasterLayerNodeConfig()
        {
            Slave = new SlaveLayerNodeConfig(this);
        }

        /// <summary>
        /// Get the linked node
        /// </summary>
        [Browsable(false)]
        BaseNodeConfig ILinkedNodeConfig.LinkedNode
        {
            get
            {
                return Slave;
            }
        }

        /// <summary>
        /// Create the linked node factory
        /// </summary>
        /// <param name="linkedNode"></param>
        /// <returns></returns>
        BaseNodeFactory ILinkedNodeConfig.CreateFactory(BaseNodeFactory linkedNode)
        {
            LayerSectionMasterNodeFactory factory = linkedNode as LayerSectionMasterNodeFactory;

            return factory.SlaveFactory;
        }        

        /// <summary>
        /// Method to create the filter factories
        /// </summary>
        /// <returns></returns>
        protected abstract LayerSectionFilterFactory[] CreateFilterFactories();

        /// <summary>
        /// Method to create the factory
        /// </summary>
        /// <returns>The factory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            return new LayerSectionMasterNodeFactory(_label, _id, Slave.Id)
            {
                LayerFactories = CreateFilterFactories(),
                DefaultMode = LayerSectionNodeDefaultMode.PassFrame,
                Direction = LayerSectionGraphDirection.ServerToClient,
            };
        }
    }
}
