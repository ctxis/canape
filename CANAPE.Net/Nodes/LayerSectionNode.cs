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
using System.IO;
using System.Linq;
using System.Threading;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Net.Layers;
using CANAPE.Net.Tokens;
using CANAPE.NodeFactories;
using CANAPE.Utils;
using System.Globalization;
using CANAPE.Net;

namespace CANAPE.Nodes
{
    /// <summary>
    /// Class to implement a layer section node
    /// </summary>
    public sealed class LayerSectionNode : BaseDecoupledPipelineNode
    {
        private sealed class GraphEntry : IDisposable
        {            
            LayerSectionDataAdapter _clientAdapter;
            LayerSectionDataAdapter _serverAdapter;
            Guid _clientEndpoint;
            Guid _serverEndpoint;
            NetworkLayerBinding _defaultBinding;

            public LayerSectionFilter Filter { get; set; }
            public LayerSectionDataAdapter ClientAdapter { get { return _clientAdapter; } }
            public LayerSectionDataAdapter ServerAdapter { get { return _serverAdapter; } }
            public NetGraph Graph { get; set; }
            public bool HasNegotiated { get; set; }
            public Thread NegotiationThread { get; set; }

            public LayerSectionDataAdapter GetInputAdapter(LayerSectionGraphDirection direction)
            {
                return direction == LayerSectionGraphDirection.ServerToClient ? _serverAdapter : _clientAdapter;
            }

            public LayerSectionDataAdapter GetOutputAdapter(LayerSectionGraphDirection direction)
            {
                return direction == LayerSectionGraphDirection.ClientToServer ? _serverAdapter : _clientAdapter;
            }
        
            public void  Dispose()
            {
                ClientAdapter.Dispose();
                ServerAdapter.Dispose();
                ((IDisposable)Graph).Dispose();

                GeneralUtils.AbortThread(NegotiationThread);
            }

            public GraphEntry(LayerSectionFilter filter, LayerSectionDataAdapter clientAdapter,
                LayerSectionDataAdapter serverAdapter, NetGraph graph, Guid clientEndpoint,
                Guid serverEndpoint, NetworkLayerBinding defaultBinding)
            {
                Filter = filter;
                _clientAdapter = clientAdapter;
                _serverAdapter = serverAdapter;
                Graph = graph;
                _clientEndpoint = clientEndpoint;
                _serverEndpoint = serverEndpoint;
                _defaultBinding = defaultBinding;
            }

            private void StartGraph(IDataAdapter ca, IDataAdapter sa)
            {
                Graph.BindEndpoint(_clientEndpoint, ca);
                Graph.BindEndpoint(_serverEndpoint, sa);

                Graph.Start();
            }            

            public void Start()
            {                
                IDataAdapter ca = _clientAdapter;
                IDataAdapter sa = _serverAdapter;

                foreach (INetworkLayerFactory factory in Filter.Layers)
                {
                    INetworkLayer layer = factory.CreateLayer(Graph.Logger);

                    layer.Negotiate(ref sa, ref ca, new ProxyToken(), Graph.Logger, 
                        Graph.Meta, Graph.GlobalMeta, Graph.ConnectionProperties, _defaultBinding);
                }

                StartGraph(ca, sa);

                HasNegotiated = true;
            }
        }

        private sealed class LayerSectionDataAdapter : IDataAdapter
        {
            private LockedQueue<DataFrame> _inputQueue;
            private LayerSectionNode _outputNode;
            private int _readTimeout;
            private bool _disposed;

            public LayerSectionDataAdapter(LayerSectionNode outputNode)
            {
                _inputQueue = new LockedQueue<DataFrame>();
                _readTimeout = Timeout.Infinite;
                _outputNode = outputNode;
            }

            public bool IsStopped { get { return _disposed; } }

            public DataFrame Read()
            {
                DataFrame ret = null;

                if (!_inputQueue.Dequeue(_readTimeout, out ret))
                {
                    if (!_inputQueue.IsStopped)
                    {
                        throw new IOException();
                    }
                }

                return ret;
            }

            public void Write(DataFrame data)
            {
                _outputNode.WriteOutput(data);
            }

            public void Enqueue(DataFrame frame)
            {
                _inputQueue.Enqueue(frame);
            }

            public void StopInput()
            {                
                _inputQueue.Stop();
            }


            public void Close()
            {                
                _disposed = true;
                _outputNode.CheckShutdown();
            }

            public string Description
            {
                get { return "Dummy"; }
            }

            public int ReadTimeout
            {
                get
                {
                    return _readTimeout;
                }
                set
                {
                    if ((value < 0) && (value != Timeout.Infinite))
                    {
                        throw new ArgumentException();
                    }

                    _readTimeout = value;
                }
            }

            public bool CanTimeout
            {
                get { return true; }
            }

            public void Dispose()
            {
                Close();
            }


            public void Reconnect()
            {
                throw new NotImplementedException();
            }
        }

        private LayerSectionFilter[] _filters;
        private LayerSectionNodeDefaultMode _defaultMode;
        private LayerSectionGraphDirection _direction;
        private LayerSectionNode _linkedNode;
        private Dictionary<string, GraphEntry> _graphEntries;
        private bool _shuttingDown;
        private Timer _shutdownTimer;

        private void ShutdownTimerCallback(Object o)
        {
            base.ShutdownOutputs();
        }

        /// <summary>
        /// Overridden shutdown outputs
        /// </summary>
        protected override void ShutdownOutputs()
        {            
            lock(_graphEntries)
            {
                _shuttingDown = true;

                if (_graphEntries.Count > 0)
                {
                    // Kick off closing of inputs
                    foreach (GraphEntry entry in _graphEntries.Values)
                    {
                        entry.GetInputAdapter(_direction).StopInput();
                    }

                    // Create a timer which ensure things will get shutdown at somepoint
                    _shutdownTimer = new Timer(ShutdownTimerCallback, null, 5000, Timeout.Infinite);                    
                }
                else
                {
                    base.ShutdownOutputs();
                }
            }
        }

        /// <summary>
        /// Indicates that this node is the master for this layer
        /// </summary>
        public bool IsMaster { get; private set; }

        /// <summary>
        /// Get a list of graphs which are currently open on this node
        /// </summary>
        public IEnumerable<NetGraph> Graphs
        {
            get
            {
                lock(_graphEntries)
                {
                    return _graphEntries.Values.Where(g => g.HasNegotiated).Select(g => g.Graph).ToArray();
                }
            }
        }

        /// <summary>
        /// Constructor        
        /// </summary>
        /// <param name="filters">List of filters used to determine when to create new graph</param>        
        /// <param name="defaultMode">Default mode of the layer section</param>
        /// <param name="direction">The direction of the node</param>
        public LayerSectionNode(LayerSectionFilter[] filters, LayerSectionNodeDefaultMode defaultMode, LayerSectionGraphDirection direction)
        {
            _filters = filters;
            _defaultMode = defaultMode;
            _graphEntries = new Dictionary<string, GraphEntry>();
            _direction = direction;
            IsMaster = true;
        }

        /// <summary>
        /// Linked constructor
        /// </summary>
        /// <param name="linkedNode">The linked node</param>
        public LayerSectionNode(LayerSectionNode linkedNode)
        {
            _filters = linkedNode._filters;
            _graphEntries = linkedNode._graphEntries;

            if (linkedNode._direction == LayerSectionGraphDirection.ClientToServer)
            {
                _direction = LayerSectionGraphDirection.ServerToClient;
            }
            else
            {
                _direction = LayerSectionGraphDirection.ClientToServer;
            }

            _linkedNode = linkedNode;
            _linkedNode._linkedNode = this;
            IsMaster = false;
        }

        private string GenerateName(LayerSectionFilter filter, DataFrame frame)
        {
            string ret = filter.FilterId.ToString();

            if (!String.IsNullOrWhiteSpace(filter.SelectionPath))
            {
                DataNode node = SelectSingleNode(frame);

                if (node != null)
                {
                    ret = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", ret, node);
                }
            }

            return ret;
        }

        private void CheckShutdown()
        {
            lock (_graphEntries)
            {
                bool allStopped = true;
                
                lock (_graphEntries)
                {
                    foreach (GraphEntry entry in _graphEntries.Values)
                    {
                        if (!entry.GetOutputAdapter(_direction).IsStopped)
                        {
                            allStopped = false;
                            break;
                        }
                    }
                }

                if (allStopped)
                {
                    if (_shutdownTimer != null)
                    {
                        _shutdownTimer.Dispose();
                        _shutdownTimer = null;
                    }
                    
                    base.ShutdownOutputs();
                }
            }
        }

        private void HandleFrame(LayerSectionFilter filter, DataFrame frame)
        {            
            string name = GenerateName(filter, frame);
            GraphEntry startEntry = null;

            lock (_graphEntries)
            {
                if (!_shuttingDown)
                {
                    if (_graphEntries.ContainsKey(name))
                    {
                        _graphEntries[name].GetInputAdapter(_direction).Enqueue(frame);
                    }
                    else
                    {
                        LayerSectionDataAdapter clientAdapter = new LayerSectionDataAdapter(this);
                        LayerSectionDataAdapter serverAdapter = new LayerSectionDataAdapter(_linkedNode);

                        if (_direction == LayerSectionGraphDirection.ClientToServer)
                        {
                            LayerSectionDataAdapter temp = clientAdapter;
                            clientAdapter = serverAdapter;
                            serverAdapter = temp;
                        }
                        
                        var clients = filter.Factory.GetNodes<ClientEndpointFactory>();
                        var servers = filter.Factory.GetNodes<ServerEndpointFactory>();

                        if ((clients.Length > 0) && (servers.Length > 0))
                        {
                            MetaDictionary meta = filter.IsolatedGraph ? new MetaDictionary() : Graph.Meta;

                            NetGraph graph = filter.Factory.Create(Graph.Logger, Graph, Graph.GlobalMeta, meta, Graph.ConnectionProperties.AddBag(name));
                            graph.Name = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", Name, name);
                            startEntry = new GraphEntry(filter, clientAdapter, serverAdapter, 
                                graph, clients[0].Id, servers[0].Id, ProxyNetworkService.GetLayerBinding(Graph));
                            startEntry.GetInputAdapter(_direction).Enqueue(frame);

                            _graphEntries[name] = startEntry;
                        }
                        else
                        {
                            throw new ArgumentException(CANAPE.Net.Properties.Resources.LayerSectionNode_InvalidGraph);
                        }
                    }
                }
            }

            // Ensure this is done outside the lock
            if (startEntry != null)
            {
                startEntry.NegotiationThread = new Thread(() =>
                {
                    try
                    {                        
                        startEntry.Start();
                    }
                    catch (Exception ex)
                    {
                        Graph.Logger.LogException(ex);
                        lock (_graphEntries)
                        {
                            _graphEntries.Remove(name);
                            startEntry.Dispose();
                        }
                    }
                }
                );
                startEntry.NegotiationThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                startEntry.NegotiationThread.IsBackground = true;
                startEntry.NegotiationThread.Start();
            }
        }

        /// <summary>
        /// Overridden OnInput method
        /// </summary>
        /// <param name="frame">The input frame</param>
        protected override void OnInput(DataFrame frame)
        {
            bool filtersMatched = false;

            foreach (LayerSectionFilter filter in _filters)
            {                
                if (filter.Filter.IsMatch(frame, Graph.Meta, Graph.GlobalMeta, 
                    Graph.ConnectionProperties, Graph.Uuid, this))
                {
                    filtersMatched = true;
                    HandleFrame(filter, frame);
                    break;
                }
            }

            if ((!filtersMatched) && (_defaultMode == LayerSectionNodeDefaultMode.PassFrame))
            {
                WriteOutput(frame);
            }
        }

        /// <summary>
        /// Dispose implementation
        /// </summary>
        /// <param name="disposing">True if disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_graphEntries)
                {                    
                    foreach (GraphEntry ent in _graphEntries.Values)
                    {
                        ent.Dispose();
                    }
                }

                if (_shutdownTimer != null)
                {
                    _shutdownTimer.Dispose();
                }
            }
        }
    }
}
