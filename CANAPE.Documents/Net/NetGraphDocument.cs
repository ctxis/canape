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
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.NodeConfigs;
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
        public BaseNodeConfig[] Nodes
        {
            get { return (BaseNodeConfig[])_nodes.Clone(); }
        }

        /// <summary>
        /// Get or set an array of lines
        /// </summary>
        [Browsable(false)]
        public LineConfig[] Lines
        {
            get { return (LineConfig[])_lines.Clone(); }
        }

        /// <summary>
        /// Update the graph factory from list of nodes and lines
        /// </summary>
        /// <param name="nodes">The list of nodes</param>
        /// <param name="lines">The list of lines</param>
        public void UpdateGraph(BaseNodeConfig[] nodes, LineConfig[] lines)
        {
            lock (_lockObject)
            {
                _nodes = (BaseNodeConfig[])nodes.Clone();
                _lines = (LineConfig[])lines.Clone();

                foreach (BaseNodeConfig node in _nodes)
                {
                    node.Document = this;
                }

                RebuildFactory();
            }

            Dirty = true;
        }

        /// <summary>
        /// Addition properties to pre-populate the graph
        /// </summary>
        [LocalizedDescription("NetGraphDocument_PropertiesDescription", typeof(Properties.Resources)), Category("Control")]
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
                    if (_factory == null)
                    {
                        RebuildFactory();
                    }
                }

                return _factory;
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
    }
}
