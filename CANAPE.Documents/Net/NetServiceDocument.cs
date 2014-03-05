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
using System.Collections.Specialized;
using CANAPE.Net;
using CANAPE.Net.Utils;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Security;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// Implement the basics of the TCP service form configuration
    /// </summary>
    [Serializable]
    public abstract class NetServiceDocument : BaseDocumentObject
    {
        /// <summary>
        /// The netgraph document associated with this service
        /// </summary>
        protected NetGraphDocument _netGraph;

        /// <summary>
        /// The collection of packets captured by this service
        /// </summary>
        protected LogPacketCollection _packets;

        /// <summary>
        /// History of connections
        /// </summary>
        protected ConnectionHistory _history;

        /// <summary>
        /// Credentials
        /// </summary>
        protected Dictionary<SecurityPrincipal, ICredentialObject> _credentials;

        /// <summary>
        /// The global meta for the service, not saved as it could contain anything
        /// </summary>
        [NonSerialized]
        protected MetaDictionary _globalMeta;

        /// <summary>
        /// Constructor
        /// </summary>
        public NetServiceDocument()
            : base()
        {
            _packets = new LogPacketCollection();
            _globalMeta = new MetaDictionary();
            _history = new ConnectionHistory();
            _credentials = new Dictionary<SecurityPrincipal, ICredentialObject>();
            SetupCollections();
        }

        /// <summary>
        /// Method called on deserialization
        /// </summary>
        protected override void OnDeserialization()
        {            
            _globalMeta = new MetaDictionary();
            
            SetupCollections();
        }

        private void SetupCollections()
        {
            if (_history == null)
            {
                _history = new ConnectionHistory();
            }

            _packets.CollectionChanged += new NotifyCollectionChangedEventHandler(NetServiceDocument_CollectionChanged);
            _packets.FrameModified += new EventHandler(_packets_FrameModified);
            _history.CollectionChanged += _history_CollectionChanged;
        }

        void _history_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dirty = true;
        }

        void _packets_FrameModified(object sender, EventArgs e)
        {
            Dirty = true;
        }

        void NetServiceDocument_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        /// Net graph
        /// </summary>
        public NetGraphDocument NetGraph
        {
            get { return _netGraph; }
            set
            {
                if (_netGraph != value)
                {
                    _netGraph = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get the global meta dictionary
        /// </summary>
        public MetaDictionary GlobalMeta
        {
            get { return _globalMeta; }
        }

        /// <summary>
        /// Packet data
        /// </summary>
        public LogPacketCollection Packets
        {
            get { return _packets; }
        }

        /// <summary>
        /// Connection history
        /// </summary>
        public ConnectionHistory History
        {
            get { return _history; }
        }

        /// <summary>
        /// Get dictionary of credentials
        /// </summary>
        public Dictionary<SecurityPrincipal, ICredentialObject> Credentials
        {
            get
            {
                if (_credentials == null)
                {
                    _credentials = new Dictionary<SecurityPrincipal, ICredentialObject>();
                }

                return _credentials;
            }
        }

        /// <summary>
        /// Method to create the network service based on the configuration
        /// </summary>
        /// <param name="_logger">A logger to out the log data to</param>
        /// <returns></returns>
        public abstract ProxyNetworkService Create(Logger _logger);

        /// <summary>
        /// Method to create the network service based on the configuration with default logger
        /// </summary>
        /// <returns>The network service</returns>
        public ProxyNetworkService Create()
        {
            return Create(Logger.GetSystemLogger());
        }

        /// <summary>
        /// Guild the default graph factory
        /// </summary>
        /// <returns>The graph factory</returns>
        protected static NetGraphFactory BuildDefaultProxyFactory()
        {
            NetGraphBuilder builder = new NetGraphBuilder();
            ServerEndpointFactory server = builder.AddServer("SERVER", Guid.NewGuid());
            ClientEndpointFactory client = builder.AddClient("CLIENT", Guid.NewGuid());
            LogPacketNodeFactory logOut = builder.AddLog("LOGOUT", Guid.NewGuid(), new ColorValue(0xFF, 0xC0, 0xCB, 0xFF), "Out", false);
            LogPacketNodeFactory logIn = builder.AddLog("LOGIN", Guid.NewGuid(), new ColorValue(0xB0, 0xE0, 0xE6, 0xFF), "In", false);

            builder.AddLine(server, logOut, null);
            builder.AddLine(logOut, client, null);
            builder.AddLine(client, logIn, null);
            builder.AddLine(logIn, server, null);

            return builder.Factory;
        }

        internal static T[] AddFactory<T>(T factory, T[] factories)
        {
            List<T> fs = new List<T>(factories);

            fs.Add(factory);

            return fs.ToArray();
        }

        internal static T[] InsertFactory<T>(int index, T factory, T[] factories)
        {
            List<T> fs = new List<T>(factories);
            fs.Insert(index, factory);

            return fs.ToArray();
        }

        internal static T[] RemoveFactoryAt<T>(int index, T[] factories)
        {
            List<T> fs = new List<T>(factories);

            fs.RemoveAt(index);

            return fs.ToArray();
        }

        internal static T[] RemoveFactory<T>(T factory, T[] factories)
        {
            List<T> fs = new List<T>(factories);

            fs.Remove(factory);

            return fs.ToArray();
        }
    }
}
