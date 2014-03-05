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
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Config for a netgraph container node
    /// </summary>
    [Serializable]
    public class NetGraphContainerConfig : BaseNodeConfig, ILinkedNodeConfig
    {
        NetGraphDocument _graph;
        NetGraphContainerNode.GraphDirection _direction;
        NetGraphContainerConfig _linkedNode;

        /// <summary>
        /// Constructor
        /// </summary>
        public NetGraphContainerConfig()
        {
            _direction = NetGraphContainerNode.GraphDirection.ServerToClient;
        }

        private static NetGraphContainerNode.GraphDirection InvertDirection(NetGraphContainerNode.GraphDirection direction)
        {
            return direction == NetGraphContainerNode.GraphDirection.ClientToServer ? 
                NetGraphContainerNode.GraphDirection.ServerToClient : NetGraphContainerNode.GraphDirection.ClientToServer;
        }

        /// <summary>
        /// The graph document for the node
        /// </summary>
        [TypeConverter(typeof(DocumentChoiceConverter<NetGraphDocument>)), LocalizedDescription("NetGraphContainerConfig_GraphDescription", typeof(Properties.Resources)), Category("Behavior")]
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

                    if (_linkedNode != null)
                    {
                        _linkedNode._graph = value;
                        _linkedNode.SetDirty();
                    }

                    SetDirty();
                }
            }
        }

        /// <summary>
        /// The flow direction for the graph
        /// </summary>
        [LocalizedDescription("NetGraphContainerConfig_DirectionDescription", typeof(Properties.Resources)), Category("Behavior")]
        public NetGraphContainerNode.GraphDirection Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                if (_direction != value)
                {
                    _direction = value;
                    if (_linkedNode != null)
                    {
                        _linkedNode._direction = InvertDirection(value);
                        _linkedNode.SetDirty();
                    }

                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Property for a linked node
        /// </summary>
        [TypeConverter(typeof(NodeChoiceConverter<NetGraphContainerConfig>)), LocalizedDescription("NetGraphContainerConfig_LinkedNodeDescription", typeof(Properties.Resources)), Category("Behavior")]
        public NetGraphContainerConfig LinkedNode
        {
            get
            {
                return _linkedNode;
            }

            set
            {
                if (_linkedNode != value)
                {
                    // Remove link if already linked to something
                    if (_linkedNode != null)
                    {
                        _linkedNode._linkedNode = null;
                        // Don't need to change anything else, leave graph as is
                    }

                    _linkedNode = value;

                    if (_linkedNode != null)
                    {
                        if (_linkedNode._linkedNode != null)
                        {
                            _linkedNode._linkedNode._linkedNode = null;
                        }

                        _linkedNode._linkedNode = this;
                        _linkedNode._graph = _graph;
                        _linkedNode._direction = InvertDirection(_direction);
                    }

                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Name of graph node
        /// </summary>
        public const string NodeName = "netgraph";

        /// <summary>
        /// Get graph node name
        /// </summary>
        /// <returns>Always "server"</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }

        /// <summary>
        /// Method called to create the factory, creates default, unlinked factory
        /// </summary>
        /// <returns>The BaseNodeFactory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {           
            return new NetGraphContainerNodeFactory(Label, this.Id, _graph != null ? _graph.Factory : null, _direction);
        }

        /// <summary>
        /// Delete override method
        /// </summary>
        public override void Delete()
        {
            if (_linkedNode != null)
            {
                _linkedNode._linkedNode = null;
                _linkedNode.SetDirty();
            }
        }

        /// <summary>
        /// Implementation of ILinkedeNodeConfig
        /// </summary>
        BaseNodeConfig ILinkedNodeConfig.LinkedNode
        {
            get { return _linkedNode; }
        }

        /// <summary>
        /// Implementation of ILinkedeNodeConfig
        /// </summary>
        /// <param name="linkedNode">The linked node</param>
        /// <returns>The created factory</returns>
        BaseNodeFactory ILinkedNodeConfig.CreateFactory(BaseNodeFactory linkedNode)
        {
            NetGraphContainerNodeFactory ret = (NetGraphContainerNodeFactory)LinkedNode.CreateFactory();
            NetGraphContainerNodeFactory link = (NetGraphContainerNodeFactory)linkedNode;

            ret.LinkedNode = link;
            link.LinkedNode = ret;

            return ret;
        }
    }
}
