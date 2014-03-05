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
using System.ComponentModel;
using System.Runtime.Serialization;
using CANAPE.Documents.Net.Factories;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Net.Layers;
using CANAPE.Net.Utils;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// Class which represents a state graph
    /// </summary>
    [Serializable]
    public class StateGraphDocument : NetGraphDocument
    {
        private string _metaName;
        private bool _globalState;
        private List<StateGraphEntry> _entries;
        private string _defaultState;

        private void UpdateStates()
        {
            RebuildFactory();
        }

        private static string GetDirection(bool clientToServer)
        {
            return clientToServer ? "In" : "Out";
        }

        private void AddStateFromEntry(NetGraphBuilder builder, StateGraphEntry entry, BaseNodeFactory inputNode, BaseNodeFactory outputNode, bool clientToServer)
        {
            BaseNodeFactory currentFactory;

            if (entry.Graph != null)
            {
                currentFactory = builder.AddNode(new NetGraphContainerNodeFactory(String.Format("{0} {1}", entry.StateName, 
                    GetDirection(clientToServer)), Guid.NewGuid(), entry.Graph.Factory,
                    clientToServer ? NetGraphContainerNode.GraphDirection.ClientToServer : NetGraphContainerNode.GraphDirection.ServerToClient));
                currentFactory.Hidden = true;

                builder.AddLine(inputNode, currentFactory, entry.StateName);
            }
            else
            {
                currentFactory = inputNode;
            }

            if (entry.LogPackets)
            {
                LogPacketNodeFactory log = builder.AddLog(String.Format("{0} Log {1}", entry.StateName, GetDirection(clientToServer)), 
                    Guid.NewGuid(), entry.Color, null, false);
                log.Hidden = true;

                builder.AddLine(currentFactory, log, null);

                currentFactory = log;
            }

            builder.AddLine(currentFactory, outputNode, null);
        }

        private SwitchNodeFactory CreateBaseGraph(NetGraphBuilder builder, BaseNodeFactory inputNode, BaseNodeFactory outputNode, bool clientToServer)
        {
            SwitchNodeFactory switchNode = builder.AddNode(new SwitchNodeFactory(String.Format("{0} Switch {1}",
                _metaName, GetDirection(clientToServer)), Guid.NewGuid(),
                false, CANAPE.Nodes.SwitchNodeSelectionMode.ExactMatch));
            switchNode.Hidden = true;
            switchNode.SelectionPath = "#" + (String.IsNullOrWhiteSpace(_metaName) ? "Invalid" : _metaName.Trim());

            builder.AddLine(inputNode, switchNode, null);

            LogPacketNodeFactory log = builder.AddLog(String.Format("Invalid Traffic {0}", GetDirection(clientToServer)),
                    Guid.NewGuid(), new ColorValue(255, 0, 0), null, false);

            log.Hidden = true;
            builder.AddLine(switchNode, log, null);
            builder.AddLine(log, outputNode, null);

            return switchNode;
        }

        private LogPacketNodeFactory AddLog(NetGraphBuilder builder, StateGraphEntry entry, BaseNodeFactory currentFactory, bool clientToServer)
        {
            LogPacketNodeFactory log = builder.AddLog(String.Format("{0} Log {1}", entry.StateName, 
                GetDirection(clientToServer)), Guid.NewGuid(), entry.Color, null, false);
            log.Hidden = true;

            builder.AddLine(currentFactory, log, null);

            return log;
        }

        private void AddStates(NetGraphBuilder builder, SwitchNodeFactory outputSwitch, SwitchNodeFactory inputSwitch, 
            BaseNodeFactory outputNode, BaseNodeFactory inputNode)
        {
            foreach (StateGraphEntry entry in _entries)
            {
                BaseNodeFactory currentInput = inputSwitch;
                BaseNodeFactory currentOutput = outputSwitch;
                NetGraphFactory graph = entry.Graph == null ? NetGraphBuilder.CreateDefaultProxyGraph(entry.StateName) : entry.Graph.Factory;
                
                LayerSectionMasterNodeFactory masterNode = new LayerSectionMasterNodeFactory(String.Format("{0} {1}", entry.StateName,
                    GetDirection(false)), Guid.NewGuid(), Guid.NewGuid());

                masterNode.DefaultMode = LayerSectionNodeDefaultMode.PassFrame;
                masterNode.Direction = LayerSectionGraphDirection.ServerToClient;

                builder.AddNode(masterNode);
                builder.AddNode(masterNode.SlaveFactory);

                LayerSectionFilterFactory[] filters = new LayerSectionFilterFactory[1];

                LayerSectionFilterFactory filter = new LayerSectionFilterFactory();

                filter.GraphFactory = graph;
                filter.LayerFactories = entry.GetLayerFactories();
                filter.SelectionPath = "";
                filter.FilterFactory = DataFrameFilterFactory.CreateDummyFactory();
                filter.IsolatedGraph = false;

                masterNode.LayerFactories = new LayerSectionFilterFactory[1] { filter };
                    
                masterNode.SlaveFactory.Hidden = true;

                builder.AddLine(outputSwitch, masterNode, entry.StateName);
                builder.AddLine(inputSwitch, masterNode.SlaveFactory, entry.StateName);

                if (entry.LogPackets)
                {
                    currentOutput = AddLog(builder, entry, masterNode, false);
                    currentInput = AddLog(builder, entry, masterNode.SlaveFactory, true);
                }
                else
                {
                    currentOutput = masterNode;
                    currentInput = masterNode.SlaveFactory;
                }                

                builder.AddLine(currentOutput, outputNode, null);
                builder.AddLine(currentInput, inputNode, null);
            }
        }

        /// <summary>
        /// Rebuild the factory
        /// </summary>
        protected override void RebuildFactory()
        {
            NetGraphBuilder builder = new NetGraphBuilder();
            ClientEndpointFactory client = builder.AddClient("Client", Guid.NewGuid());
            client.Hidden = true;
            ServerEndpointFactory server = builder.AddServer("Server", Guid.NewGuid());
            server.Hidden = true;

            SwitchNodeFactory outputSwitch = CreateBaseGraph(builder, server, client, false);
            SwitchNodeFactory inputSwitch = CreateBaseGraph(builder, client, server, true);

            AddStates(builder, outputSwitch, inputSwitch, client, server);
            
            NetGraphFactory factory = builder.Factory;
            factory.Properties.Add(_metaName, _defaultState);

            if (_factory == null)
            {
                _factory = factory;
            }
            else
            {
                _factory.UpdateGraph(factory);
            }

            Dirty = true;
        }

        /// <summary>
        /// A state graph entry
        /// </summary>
        [Serializable]
        public class StateGraphEntry
        {
            private StateGraphDocument _parent;
            private string _stateName;
            private bool _logPackets;
            private ColorValue _color;
            private NetGraphDocument _graph;
            private SslNetworkLayerConfig _sslConfig;
            private INetworkLayerFactory[] _layers;

            internal StateGraphEntry(StateGraphDocument parent)
            {
                _parent = parent;
                _color = new ColorValue(255, 255, 255);
                _stateName = "CHANGE ME";
                _sslConfig = null;
                _layers = new INetworkLayerFactory[0];
            }

            [OnDeserialized]
            private void OnDeserialized(StreamingContext ctx)
            {
                if ((_layers == null) && (_sslConfig != null))
                {
                    // Convert
                    _layers = new INetworkLayerFactory[1] { new SslNetworkLayerFactory(_sslConfig) };
                }
            }

            /// <summary>
            /// Parent state graph
            /// </summary>
            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public StateGraphDocument Parent
            {
                get { return _parent; }
            }

            /// <summary>
            /// State name
            /// </summary>
            [LocalizedDescription("StateGraphEntry_StateNameDescription", typeof(Properties.Resources)), Category("Behavior")]
            public string StateName
            {
                get { return _stateName; }
                set
                {
                    if (_stateName != value)
                    {
                        _stateName = value;
                        _parent.UpdateStates();
                    }
                }
            }

            /// <summary>
            /// Whether to log packets
            /// </summary>
            [LocalizedDescription("StateGraphEntry_LogPacketsDescription", typeof(Properties.Resources)), Category("Behavior")]
            public bool LogPackets
            {
                get { return _logPackets; }
                set
                {
                    if (_logPackets != value)
                    {
                        _logPackets = value;
                        _parent.UpdateStates();
                    }
                }
            }

            /// <summary>
            /// Indicates the color of the log packets
            /// </summary>
            [LocalizedDescription("StateGraphEntry_ColorDescription", typeof(Properties.Resources)), Category("Behavior")]
            public ColorValue Color
            {
                get { return _color; }
                set
                {
                    if (_color != value)
                    {
                        _color = value;
                        _parent.UpdateStates();
                    }
                }
            }

            /// <summary>
            /// The graph which implements this state
            /// </summary>
            [LocalizedDescription("StateGraphEntry_GraphDescription", typeof(Properties.Resources)), Category("Behavior"), 
                TypeConverter(typeof(DocumentChoiceConverter<NetGraphDocument>))]
            public NetGraphDocument Graph
            {
                get { return _graph; }
                set
                {
                    if (_graph != value)
                    {
                        if (Object.ReferenceEquals(_parent, value))
                        {
                            throw new ArgumentException("Cannot specify the state graph for this state");
                        }

                        _graph = value;
                        _parent.UpdateStates();
                    }
                }
            }

            /// <summary>
            /// List of layer factories
            /// </summary>
            [LocalizedDescription("StateGraphEntry_LayersDescription", typeof(Properties.Resources)), Category("Behavior")]
            public INetworkLayerFactory[] Layers
            {
                get
                {
                    return _layers;
                }

                set
                {
                    if (_layers != value)
                    {
                        _layers = value;
                        _parent.UpdateStates();
                    }
                }
            }

            internal INetworkLayerFactory[] GetLayerFactories()
            {
                List<INetworkLayerFactory> layers = new List<INetworkLayerFactory>();

                foreach (INetworkLayerFactory factory in _layers)
                {
                    layers.Add(factory.Clone());
                }

                return layers.ToArray();
            }
        }

        /// <summary>
        /// Add a state entry
        /// </summary>
        /// <returns>The new entry</returns>
        public StateGraphEntry AddEntry()
        {
            StateGraphEntry entry = new StateGraphEntry(this);
            _entries.Add(entry);

            UpdateStates();

            return entry;
        }

        /// <summary>
        /// Remove an entry
        /// </summary>
        /// <param name="entry">The entry to remove</param>
        public void RemoveEntry(StateGraphEntry entry)
        {
            _entries.Remove(entry);

            UpdateStates();
        }

        /// <summary>
        /// The meta name
        /// </summary>
        [LocalizedDescription("StateGraphDocument_MetaNameDescription", typeof(Properties.Resources)), Category("Behavior")]
        public string MetaName { 
            get { return _metaName; } 
            set
            {
                if (_metaName != value)
                {
                    _metaName = value;
                    UpdateStates();
                }
            }
        }

        /// <summary>
        /// Whether the state is in the global scope
        /// </summary>
        [LocalizedDescription("StateGraphDocument_GlobalStateDescription", typeof(Properties.Resources)), Category("Behavior")]
        public bool GlobalState
        {
            get { return _globalState; }
            set
            {
                _globalState = value;
                UpdateStates();
            }
        }

        /// <summary>
        /// Default state
        /// </summary>
        public string DefaultState
        {
            get { return _defaultState; }
            set
            {
                if (_defaultState != value)
                {
                    _defaultState = value;
                    UpdateStates();
                }
            }
        }

        /// <summary>
        /// Entries
        /// </summary>
        [Browsable(false)]
        public IEnumerable<StateGraphEntry> Entries
        {
            get { return _entries; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public StateGraphDocument()
        {
            _entries = new List<StateGraphEntry>();
            _factory = new NetGraphFactory();
            _metaName = "REPLACE ME";
        }

        /// <summary>
        /// Return default name
        /// </summary>
        public override string DefaultName
        {
            get
            {
                return "State Graph";
            }
        }
    }
}
