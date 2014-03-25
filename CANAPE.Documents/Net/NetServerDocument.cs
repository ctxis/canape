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
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using CANAPE.DataAdapters;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net;
using CANAPE.Net.Clients;
using CANAPE.Net.Layers;
using CANAPE.Net.Listeners;
using CANAPE.Net.Servers;
using CANAPE.Net.Tokens;
using CANAPE.Net.Utils;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Security;
using CANAPE.Security.Cryptography.X509Certificates;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// A document representing a network server
    /// </summary>
    [Serializable]
    public class NetServerDocument : NetServiceDocument
    {
        private class NetServerProxyClient : ProxyClient
        {
            DataEndpointFactory _factory;

            public NetServerProxyClient(DataEndpointFactory factory) 
            {
                _factory = factory;
            }

            public override IDataAdapter Connect(ProxyToken token, Logger logger, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, CredentialsManagerService credmgr)
            {
                return new DataEndpointAdapter(_factory.Create(logger, meta, globalMeta), logger);
            }

            public override IDataAdapter Bind(ProxyToken token, Logger logger, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, CredentialsManagerService credmgr)
            {
                return null;
            }
        }
        
        private bool _ipv6Bind;
        private int _port;
        private bool _anyBind;
        private bool _udp;
        private bool _enableBroadcast;        
        private DataEndpointFactory _factory;

        // Deprecated
        private SslNetworkLayerConfig _sslConfig;

        private INetworkLayerFactory[] _layers;

        /// <summary>
        /// Constructor
        /// </summary>
        public NetServerDocument()
            : base()
        {
            _port = 12345;            
            _udp = false;
            _layers = new INetworkLayerFactory[0];
        }

        /// <summary>
        /// Get default document name
        /// </summary>
        public override string DefaultName
        {
            get { return "Net Server"; }
        }

        /// <summary>
        /// On deserialization handler
        /// </summary>
        protected override void OnDeserialization()
        {
            base.OnDeserialization();

            if (_layers == null)
            {
                _layers = new INetworkLayerFactory[0];
            }

            if (_sslConfig != null)
            {
                _layers = new INetworkLayerFactory[1] { new SslNetworkLayerFactory(_sslConfig) };
                _sslConfig = null;
            }    
        }

        /// <summary>
        /// Create the base service
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <returns>The new network service</returns>
        public override ProxyNetworkService Create(Logger logger)
        {
            ProxyNetworkService ret = null;

            if (logger == null)
            {
                logger = Logger.GetSystemLogger();
            }

            if ((_port < 0) || (_port > 65535))
            {
                throw new NetServiceException(Properties.Resources.NetServerDocument_ValidPort);
            }            
            else if (_factory == null)
            {
                throw new NetServiceException(Properties.Resources.NetServerDocument_MustSpecifyServer);
            }
            else
            {
                try
                {
                    ProxyServer server = new PassThroughProxyServer(logger, _layers);

                    ProxyClient client = new NetServerProxyClient(_factory);

                    INetworkListener listener = null;

                    if (!_udp)
                    {
                        if (NetUtils.OSSupportsIPv4)
                        {
                            listener = new TcpNetworkListener(_anyBind, false, _port, logger, false);
                        }

                        if (_ipv6Bind && NetUtils.OSSupportsIPv6)
                        {
                            INetworkListener ipv6Listener = new TcpNetworkListener(_anyBind, true, _port, logger, false);

                            if (listener != null)
                            { 
                                listener = new AggregateNetworkListener(listener, ipv6Listener);
                            }
                            else
                            {
                                listener = ipv6Listener;
                            }
                        }
                    }
                    else
                    {
                        if (NetUtils.OSSupportsIPv4)
                        {
                            listener = new UdpNetworkListener(_anyBind, false, _port, _enableBroadcast, logger);
                        }

                        if (_ipv6Bind && NetUtils.OSSupportsIPv6)
                        {
                            INetworkListener ipv6Listener = new UdpNetworkListener(_anyBind, true, _port, _enableBroadcast, logger);

                            if(listener != null)
                            {
                                listener = new AggregateNetworkListener(listener, ipv6Listener);
                            }
                            else
                            {
                                listener = ipv6Listener;
                            }
                        }
                    }

                    if (listener == null)
                    {
                        throw new NetServiceException(CANAPE.Documents.Properties.Resources.NetServiceDocument_CannotSetupListener);
                    }

                    ret = new ProxyNetworkService(
                        _packets,
                        listener,
                        _netGraph == null ? BuildDefaultProxyFactory() : _netGraph.Factory, logger, _globalMeta, _history,
                        _credentials, server, client, null, Timeout.Infinite, false);

                    ret.DefaultBinding = NetworkLayerBinding.Server;
                }
                catch (SocketException ex)
                {
                    throw new NetServiceException(Properties.Resources.NetServerDocument_ErrorCreatingServer, ex);
                }
                catch (IOException ex)
                {
                    throw new NetServiceException(Properties.Resources.NetServerDocument_ErrorCreatingServer, ex);
                }

                return ret;
            }
        }

        /// <summary>
        /// Get or set whether to bind globally
        /// </summary>
        public bool AnyBind
        {
            get { return _anyBind; }
            set
            {
                if (_anyBind != value)
                {
                    _anyBind = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set whether to bind to IPv6
        /// </summary>
        public bool Ipv6Bind
        {
            get { return _ipv6Bind; }
            set
            {
                if (_ipv6Bind != value)
                {
                    _ipv6Bind = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set service port
        /// </summary>
        public int LocalPort
        {
            get { return _port; }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set list of layers
        /// </summary>
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

                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set whether to use UDP
        /// </summary>
        public bool UdpEnable
        {
            get { return _udp; }
            set
            {
                if (_udp != value)
                {
                    _udp = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set whether to enable broadcast packets in UDP
        /// </summary>
        public bool EnableBroadcast
        {
            get { return _enableBroadcast; }
            set
            {
                if (_enableBroadcast != value)
                {
                    _enableBroadcast = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set the data server factory
        /// </summary>
        public DataEndpointFactory ServerFactory
        {
            get 
            { 
                return _factory; 
            }

            set
            {
                if (_factory != value)
                {
                    _factory = value;
                    Dirty = true;
                }
            }
        }

        private X509Certificate2 GetCertificate(X509CertificateContainer cert)
        {
            if (cert != null)
            {
                return cert.Certificate;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Add a layer to this service
        /// </summary>
        /// <param name="factory">The factory to add</param>
        public void AddLayer(INetworkLayerFactory factory)
        {
            _layers = AddFactory(factory, _layers);
        }

        /// <summary>
        /// Insert a layer into this factory
        /// </summary>
        /// <param name="factory">The factory to insert</param>
        /// <param name="index">The index of the factory</param>
        public void InsertLayer(int index, INetworkLayerFactory factory)
        {
            _layers = InsertFactory(index, factory, _layers);
        }

        /// <summary>
        /// Remove a layer by index
        /// </summary>
        /// <param name="index">The index to remove the layer at</param>
        public void RemoveLayerAt(int index)
        {
            _layers = RemoveFactoryAt(index, _layers);
        }

        /// <summary>
        /// Remove a layer
        /// </summary>
        /// <param name="factory">The layer to remove</param>
        public void RemoveLayer(INetworkLayerFactory factory)
        {
            _layers = RemoveFactory(factory, _layers);
        }

        /// <summary>
        /// Overridden method to get a description of server
        /// </summary>
        /// <returns>The description</returns>
        public override string ToString()
        {
            return String.Format("{0} - {1} server listening on port {2}", Name, _udp ? "UDP" : "TCP", _port);
        }
    }
}
