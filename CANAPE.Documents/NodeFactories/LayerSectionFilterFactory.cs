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

using System.Collections.Generic;
using CANAPE.Documents;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net.Layers;
using CANAPE.Nodes;
using System;
using CANAPE.Utils;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// Factory class for a layer section filter
    /// </summary>    
    public sealed class LayerSectionFilterFactory
    {
        private Guid _filterId;

        /// <summary>
        /// Constructor
        /// </summary>
        public LayerSectionFilterFactory()
        {
            _filterId = Guid.NewGuid();
        }

        /// <summary>
        /// Frame filter factory
        /// </summary>
        public DataFrameFilterFactory FilterFactory { get; set; }

        /// <summary>
        /// Selection path for discriminator
        /// </summary>
        public string SelectionPath { get; set; }

        /// <summary>
        /// Layer factories
        /// </summary>
        public INetworkLayerFactory[] LayerFactories { get; set; }

        /// <summary>
        /// The netgraph factory
        /// </summary>
        public NetGraphFactory GraphFactory { get; set; }

        /// <summary>
        /// Indicates the graph is isolated from the parent (from the point of view of meta data)
        /// </summary>
        public bool IsolatedGraph { get; set; }

        /// <summary>
        /// Create a layer filter
        /// </summary>
        /// <returns>The layer filter</returns>
        public LayerSectionFilter Create()
        {
            if (GraphFactory == null)
            {
                throw new NodeFactoryException(CANAPE.Documents.Properties.Resources.LayerSectionFilterFactory_MustSpecifyGraph);
            }

            return new LayerSectionFilter(FilterFactory.CreateFilter(), GraphFactory, LayerFactories, SelectionPath, IsolatedGraph, _filterId);
        }
    }
}
