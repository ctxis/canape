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
using CANAPE.Documents.Net.Factories;
using CANAPE.Net.Layers;
using CANAPE.Net.Utils;
using CANAPE.NodeFactories;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Config for a basic ssl layer section node
    /// </summary>
    [Serializable]
    public sealed class SslLayerSectionNodeConfig : MasterLayerNodeConfig
    {        
        private NetGraphDocument _graph;
        private SslNetworkLayerConfig _config;
        private bool _isolatedGraph;

        /// <summary>
        /// The name of the node
        /// </summary>
        public const string NodeName = "ssllayersection";

        /// <summary>
        /// Get the name of the node type
        /// </summary>
        /// <returns>Returns layersection</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SslLayerSectionNodeConfig()
        {
            _selectionPath = "";
            _config = new SslNetworkLayerConfig(false, false);
        }

        /// <summary>
        /// Create the filters
        /// </summary>
        /// <returns>An array of filter factories</returns>
        protected override LayerSectionFilterFactory[] CreateFilterFactories()
        {
            LayerSectionFilterFactory filter = new LayerSectionFilterFactory();

            filter.GraphFactory = _graph != null ? _graph.Factory : NetGraphBuilder.CreateDefaultProxyGraph(Label);
            filter.LayerFactories = new INetworkLayerFactory[1];            
            filter.LayerFactories[0] = new SslNetworkLayerFactory(_config);
            filter.SelectionPath = SelectionPath;
            filter.FilterFactory = DataFrameFilterFactory.CreateDummyFactory();
            filter.IsolatedGraph = _isolatedGraph;

            return new LayerSectionFilterFactory[1] { filter };
        }

        /// <summary>
        /// SSL configuration
        /// </summary>
        [LocalizedDescription("SslLayerSectionNodeConfig_SslConfigDescription", typeof(Properties.Resources)), Category("Behavior")]
        public SslNetworkLayerConfig SslConfig
        {
            get { return _config; }
            set
            {
                if (_config != value)
                {
                    _config = value;
                    SetDirty();
                }
            }
        }
        
        /// <summary>
        /// The graph document for the node
        /// </summary>
        [TypeConverter(typeof(DocumentChoiceConverter<NetGraphDocument>)), LocalizedDescription("SslLayerSectionNodeConfig_GraphDescription", typeof(Properties.Resources)), Category("Behavior")]
        public NetGraphDocument Graph
        {
            get
            {
                return _graph;
            }

            set
            {
                if (_graph != value)
                {
                    _graph = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Isolating the graph
        /// </summary>
        [LocalizedDescription("SslLayerSectionNodeConfig_IsolatedGraphDescription", typeof(Properties.Resources)), Category("Behavior")]
        public bool IsolatedGraph
        {
            get { return _isolatedGraph; }
            set
            {
                if (_isolatedGraph != value)
                {
                    _isolatedGraph = value;
                    SetDirty();
                }
            }
        }
    }
}
