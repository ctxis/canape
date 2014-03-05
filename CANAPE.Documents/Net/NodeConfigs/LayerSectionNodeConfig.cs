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
using CANAPE.Net.Layers;
using CANAPE.Net.Utils;
using CANAPE.NodeFactories;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Config node for a layer section
    /// </summary>
    [Serializable]
    public class LayerSectionNodeConfig : MasterLayerNodeConfig
    {       
        private NetGraphDocument _graph;
        private INetworkLayerFactory[] _layers;
        private bool _isolatedGraph;

        /// <summary>
        /// The name of the node
        /// </summary>
        public const string NodeName = "layersection";

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
        public LayerSectionNodeConfig()
        {
            _selectionPath = "";
            _layers = new INetworkLayerFactory[0];
        }

        /// <summary>
        /// Create the filters
        /// </summary>
        /// <returns>An array of filter factories</returns>
        protected override LayerSectionFilterFactory[] CreateFilterFactories()
        {
            LayerSectionFilterFactory filter = new LayerSectionFilterFactory();

            filter.GraphFactory = _graph != null ? _graph.Factory : NetGraphBuilder.CreateDefaultProxyGraph(Label);
            filter.LayerFactories = new INetworkLayerFactory[_layers.Length];
            for (int i = 0; i < _layers.Length; ++i)
            {
                filter.LayerFactories[i] = _layers[i].Clone();
            }

            filter.SelectionPath = SelectionPath;
            filter.FilterFactory = DataFrameFilterFactory.CreateDummyFactory();
            filter.IsolatedGraph = _isolatedGraph;

            return new LayerSectionFilterFactory[1] { filter };
        }

        /// <summary>
        /// Layer list
        /// </summary>
        [LocalizedDescription("LayerSectionNodeConfig_LayersDescription", typeof(Properties.Resources)), Category("Behavior")]
        public INetworkLayerFactory[] Layers
        {
            get { return _layers; }
            set
            {
                if (_layers != value)
                {
                    if (value == null)
                    {
                        _layers = new INetworkLayerFactory[0];
                    }
                    else
                    {
                        _layers = value;
                    }

                    SetDirty();
                }
            }
        }
        
        /// <summary>
        /// The graph document for the node
        /// </summary>
        [TypeConverter(typeof(DocumentChoiceConverter<NetGraphDocument>)), LocalizedDescription("LayerSectionNodeConfig_GraphDescription", typeof(Properties.Resources)), Category("Behavior")]
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
        [LocalizedDescription("LayerSectionNodeConfig_IsolatedGraphDescription", typeof(Properties.Resources)), Category("Behavior")]
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
