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
using System.Linq;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Net.Utils;
using CANAPE.NodeFactories;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// NetGraph document
    /// </summary>
    [Serializable, TestDocumentType(typeof(NetGraphTestDocument))]
    public class NetGraphDocument : BaseDocumentObject
    {
        /// <summary>
        /// The netgraph factory
        /// </summary>
        [NonSerialized]
        protected NetGraphFactory _factory;

        [NonSerialized]
        object _lockObject;

        BaseNodeConfig[] _nodes;
        LineConfig[] _lines;
        Dictionary<string, string> _properties;

        /// <summary>
        /// Default size for a graph document
        /// </summary>
        public const int DEFAULT_DOCUMENT_HEIGHT = 2048;

        /// <summary>
        /// Default size for a graph document
        /// </summary>
        public const int DEFAULT_DOCUMENT_WIDTH = 2048;     

        /// <summary>
        /// Default constructor
        /// </summary>
        public NetGraphDocument()
            : base()
        {
            _nodes = new BaseNodeConfig[0];
            _lines = new LineConfig[0];
            _properties = new Dictionary<string, string>();
            _factory = new NetGraphFactory();
            _lockObject = new object();
        }

        /// <summary>
        /// Method called on deserialization
        /// </summary>
        protected override void OnDeserialization()
        {
            base.OnDeserialization();

            _lockObject = new object();
        }
 
        /// <summary>
        /// Method called when document is copied
        /// </summary>
        protected override void OnCopy()
        {            
            base.OnCopy();

            lock (_lockObject)
            {                
                RebuildFactory();
            }
        }

        /// <summary>
        /// Method called when name changes
        /// </summary>
        protected override void OnNameChange()
        {
            if (_factory != null)
            {
                _factory.Name = Name;
            }
        }

        #region IDocumentObject Members

        /// <summary>
        /// Get default name
        /// </summary>
        [Browsable(false)]
        public override string DefaultName
        {
            get { return "Net Graph"; }
        }

        #endregion

        /// <summary>
        /// Get or set the graph nodes
        /// </summary>
        [Browsable(false)]
        public IEnumerable<BaseNodeConfig> Nodes
        {
            get { return (BaseNodeConfig[])_nodes.Clone(); }
        }

        /// <summary>
        /// Get or set an array of lines
        /// </summary>
        [Browsable(false)]
        public IEnumerable<LineConfig> Lines
        {
            get { return (LineConfig[])_lines.Clone(); }
        }

        /// <summary>
        /// Get or set an array of edges, synonym for lines
        /// </summary>
        [Browsable(false)]
        public IEnumerable<LineConfig> Edges
        {
            get { return Lines; }
        }

        /// <summary>
        /// Update the graph factory from list of nodes and lines
        /// </summary>
        /// <param name="nodes">The list of nodes</param>
        /// <param name="lines">The list of lines</param>
        public void UpdateGraph(IEnumerable<BaseNodeConfig> nodes, IEnumerable<LineConfig> lines)
        {
            lock (_lockObject)
            {
                _nodes = nodes.ToArray();
                _lines = lines.ToArray();

                foreach (BaseNodeConfig node in _nodes)
                {
                    node.Document = this;
                }

                foreach (LineConfig edge in _lines)
                {
                    edge.Document = this;
                }

                RebuildFactory();
            }

            Dirty = true;
        }

        /// <summary>
        /// Update the edges
        /// </summary>
        /// <param name="edges">List of edges</param>
        public void UpdateEdges(IEnumerable<LineConfig> edges)
        {
            UpdateGraph(_nodes, edges);
        }

        /// <summary>
        /// Update the nodes
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        public void UpdateNodes(IEnumerable<BaseNodeConfig> nodes)
        {
            UpdateGraph(nodes, _lines);
        }

        /// <summary>
        /// Addition properties to pre-populate the graph
        /// </summary>
        [LocalizedDescription("NetGraphDocument_PropertiesDescription", 
            typeof(Properties.Resources)), Category("Control")]
        public Dictionary<string, string> Properties
        {
            get { return _properties; }
            set
            {
                if (_properties != value)
                {
                    _properties = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get the netgraph factory
        /// </summary>
        [Browsable(false)]
        public NetGraphFactory Factory
        {
            get
            {
                lock (_lockObject)
                {
                    RebuildFactory();
                    return _factory;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void RebuildFactory()
        {
            List<NetGraphFactory.GraphNodeEntry> graphNodes = new List<NetGraphFactory.GraphNodeEntry>();
            List<NetGraphFactory.GraphLineEntry> graphLines = new List<NetGraphFactory.GraphLineEntry>();
            HashSet<Guid> createdNodes = new HashSet<Guid>();

            if (_factory == null)
            {
                _factory = new NetGraphFactory();
            }
            
            if(_nodes != null)
            {
                foreach (BaseNodeConfig node in _nodes)
                {                    
                    if (!createdNodes.Contains(node.Id))
                    {
                        BaseNodeFactory factory = node.CreateFactory();
                        ILinkedNodeConfig linkedConfig = node as ILinkedNodeConfig;

                        if ((linkedConfig != null) && (linkedConfig.LinkedNode != null))
                        {
                            BaseNodeFactory linked = linkedConfig.CreateFactory(factory);

                            createdNodes.Add(linkedConfig.LinkedNode.Id);
                            graphNodes.Add(new NetGraphFactory.GraphNodeEntry(linked));
                        }

                        graphNodes.Add(new NetGraphFactory.GraphNodeEntry(factory));

                        createdNodes.Add(node.Id);
                    }
                }
            }

            if(_lines != null)
            {
                foreach (LineConfig line in _lines)
                {
                    graphLines.Add(new NetGraphFactory.GraphLineEntry(line.SourceNode.Id, line.DestNode.Id, line.BiDirection, line.PathName, line.WeakPath));                
                }
            }

            _factory.Nodes = graphNodes.ToArray();
            _factory.Lines = graphLines.ToArray();

            foreach (KeyValuePair<string, string> pair in _properties)
            {
                _factory.Properties.Add(pair.Key, pair.Value);
            }
        }

        private T AddNode<T>(string label) where T : BaseNodeConfig, new()
        {
            T ret = new T();

            List<BaseNodeConfig> nodes = new List<BaseNodeConfig>(_nodes);

            if (label == null)
            {
                HashSet<string> names = new HashSet<string>(nodes.Select(n => n.Label));
                for (int i = 0; i < nodes.Count + 1; ++i)
                {
                    string tmplabel = String.Format("{0}{1}", ret.GetNodeName(), i);
                    if (!names.Contains(tmplabel))
                    {
                        label = tmplabel;
                        break;
                    }
                }
            }
            
            ret.Label = label;

            nodes.Add(ret);

            UpdateGraph(nodes.ToArray(), Lines);

            return ret;
        }

        /// <summary>
        /// Remove a node from the document
        /// </summary>
        /// <param name="node">The node to remove</param>
        public void RemoveNode(BaseNodeConfig node)
        {
            List<BaseNodeConfig> nodes = new List<BaseNodeConfig>(_nodes);

            foreach (LineConfig config in node.EdgesFrom)
            {
                config.RemoveEdge();
            }

            foreach (LineConfig config in node.EdgesTo)
            {
                config.RemoveEdge();
            }

            nodes.RemoveAll(n => IsEqual(n, node));            

            UpdateGraph(nodes, _lines);
        }
        
        /// <summary>
        /// Remove an edge from the document
        /// </summary>
        /// <param name="edge">The edge to remove</param>
        public void RemoveEdge(LineConfig edge)
        {
            List<LineConfig> edges = new List<LineConfig>(_lines);

            edges.RemoveAll(l => IsEqual(edge, l));

            UpdateGraph(_nodes, edges);
        }

        private static bool IsEqual(BaseNodeConfig a, BaseNodeConfig b)
        {
            return a.Id == b.Id;
        }

        private static bool IsEqual(LineConfig a, LineConfig b)
        {
            return a.BiDirection == b.BiDirection && IsEqual(a.SourceNode, b.SourceNode) &&
                IsEqual(a.DestNode, b.DestNode) && a.Label == b.Label;
        }

        /// <summary>
        /// Add an edge to the document
        /// </summary>
        /// <param name="label">The label for the edge</param>
        /// <param name="sourceNode">The source node for the edge</param>
        /// <param name="destNode">The destination node for the edge</param>
        /// <returns>The edge config, or a pre-existing one if it already exists</returns>
        public LineConfig AddEdge(string label, BaseNodeConfig sourceNode, BaseNodeConfig destNode)
        {
            foreach(LineConfig edge in sourceNode.EdgesFrom)
            {
                if (IsEqual(edge.DestNode, destNode) && IsEqual(edge.SourceNode, sourceNode))
                {
                    return edge;
                }

                if (edge.BiDirection)
                {
                    if (IsEqual(edge.DestNode, sourceNode) && IsEqual(edge.SourceNode, destNode))
                    {
                        return edge;
                    }
                }
            }

            LineConfig newedge = new LineConfig(sourceNode, destNode, false, label, false);

            List<LineConfig> edges = new List<LineConfig>(_lines);

            edges.Add(newedge);

            UpdateEdges(edges);

            return newedge;
        }

        /// <summary>
        /// Add a log node to the document
        /// </summary>
        /// <param name="label">The label for the new node</param>
        /// <returns>The created configuration object</returns>
        public LogPacketConfig AddLog(string label)
        {
            return AddNode<LogPacketConfig>(label);
        }

        /// <summary>
        /// Adds a log node to the document with an auto-generated label
        /// </summary>
        /// <returns>The created configuration object</returns>
        public LogPacketConfig AddLog()
        {
            return AddLog(null);
        }

        /// <summary>
        /// Add a client endpoint to the document
        /// </summary>
        /// <param name="label">The label for the new node</param>
        /// <returns>The created configuration object</returns>
        public ClientEndpointConfig AddClient(string label)
        {
            return AddNode<ClientEndpointConfig>(label);
        }

        /// <summary>
        /// Adds a client endpoint to the document with an auto-generated label
        /// </summary>
        /// <returns>The created configuration object</returns>
        public ClientEndpointConfig AddClient()
        {
            return AddClient(null);
        }

        /// <summary>
        /// Add a server endpoint to the document
        /// </summary>
        /// <param name="label">The label for the new node</param>
        /// <returns>The created configuration object</returns>
        public ServerEndpointConfig AddServer(string label)
        {
            return AddNode<ServerEndpointConfig>(label);
        }

         /// <summary>
        /// Adds a server endpoint to the document with an auto-generated label
        /// </summary>
        /// <returns>The created configuration object</returns>
        public ServerEndpointConfig AddServer()
        {
            return AddServer(null);
        }

        /// <summary>
        /// Add a decision to the document
        /// </summary>
        /// <param name="label">The label for the new node</param>
        /// <returns>The created configuration object</returns>
        public DecisionNodeConfig AddDecision(string label)
        {
            return AddNode<DecisionNodeConfig>(label);
        }

        /// <summary>
        /// Adds a decision node to the document with an auto-generated label
        /// </summary>
        /// <returns>The created configuration object</returns>
        public DecisionNodeConfig AddDecision()
        {
            return AddDecision(null);
        }

        /// <summary>
        /// Add an existing configuration node to the graph
        /// </summary>
        /// <param name="node">The node to add</param>
        /// <returns>The passed node</returns>
        public BaseNodeConfig AddNode(BaseNodeConfig node)
        {
            List<BaseNodeConfig> nodes = new List<BaseNodeConfig>(_nodes);            

            nodes.Add(node);

            UpdateGraph(nodes.ToArray(), Lines);

            return node;
        }

        /// <summary>
        /// Run an simple force-directed auto-layout algorithm
        /// </summary>
        public void AutoLayout()
        {
            ForceDirectedLayout layout = new ForceDirectedLayout(this);

            layout.Run();

            double maxValueX = 0.0;
            double maxValueY = 0.0;
            double centreX = 0.0;
            double centreY = 0.0;

            // Calculate the center
            foreach (PointD p in layout.NodePositions.Values)
            {                                
                centreX += p.X;
                centreY += p.Y;
            }

            centreX /= layout.NodePositions.Count;
            centreY /= layout.NodePositions.Count;

            // Normalize
            foreach (PointD p in layout.NodePositions.Values)
            {
                double x = p.X - centreX;
                double y = p.Y - centreX;

                if (Math.Abs(x) > maxValueX)
                {
                    maxValueX = Math.Abs(x);
                }

                if (Math.Abs(y) > maxValueY)
                {
                    maxValueY = Math.Abs(y);
                }
            }

            maxValueX *= 8.0;
            maxValueY *= 8.0;
            
            foreach (KeyValuePair<BaseNodeConfig, PointD> pair in layout.NodePositions)
            {
                double x = ((pair.Value.X - centreX) / maxValueX) * DEFAULT_DOCUMENT_WIDTH + DEFAULT_DOCUMENT_WIDTH / 2;
                double y = ((pair.Value.Y - centreY) / maxValueY) * DEFAULT_DOCUMENT_HEIGHT + DEFAULT_DOCUMENT_HEIGHT / 2;

                pair.Key.X = (float)x;
                pair.Key.Y = (float)y;
            }
        }
    }
}
