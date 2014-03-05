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
using CANAPE.Documents.Net.Factories;
using CANAPE.Net;
using CANAPE.Net.Clients;
using CANAPE.Net.Layers;
using CANAPE.Net.Listeners;
using CANAPE.Net.Servers;
using CANAPE.Net.Tokens;
using CANAPE.Net.Utils;
using CANAPE.Security;
using CANAPE.Security.Cryptography.X509Certificates;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// Document to represent a fixed proxy
    /// </summary>
    [Serializable]
    public class FixedProxyDocument : NetServiceDocument
    {
        private bool _ipv6Bind;
        private bool _ipv6;        
        private int _localPort;
        private int _port;
        private string _host;
        private bool _anyBind;
        private bool _udp;
        private bool _enableBroadcast;
        private IProxyClientFactory _clientFactory;
        private SslNetworkLayerConfig _sslConfig;
        private INetworkLayerFactory[] _layers;

        /// <summary>
        /// Constructor
        /// </summary>
        public FixedProxyDocument()
            : base()
        {
            _localPort = 10000;
            _port = 12345;
            _host = "127.0.0.1";
            _udp = false;
            _clientFactory = new DefaultProxyClientFactory();
            _layers = new INetworkLayerFactory[0];
            _sslConfig = null;
        }

        /// <summary>
        /// On deserialization callback
        /// </summary>
        protected override void OnDeserialization()
        {
            base.OnDeserialization();

            if (_layers == null)
            {
                if (_sslConfig != null)
                {
                    _layers = new INetworkLayerFactory[1] { new SslNetworkLayerFactory(_sslConfig) };
                    _sslConfig = null;
                }
                else
                {
                    _layers = new INetworkLayerFactory[0];
                }
            }                        
        }

        /// <summary>
        /// Default name of the document
        /// </summary>
        public override string DefaultName
        {
            get { return "Fixed Proxy"; }
        }

        /// <summary>
        /// Get or set whether to bind to global address
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
        /// Get or set whether to connect to an IPv6 host
        /// </summary>
        public bool Ipv6
        {
            get { return _ipv6;  }
            set
            {
                if (_ipv6 != value)
                {
                    _ipv6 = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set local port
        /// </summary>
        public int LocalPort
        {
            get { return _localPort; }
            set
            {
                if (_localPort != value)
                {
                    _localPort = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set remote port
        /// </summary>
        public int Port
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
        /// Get or set remote host
        /// </summary>
        public string Host
        {
            get { return _host; }
            set
            {
                if (_host != value)
                {
                    _host = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get layers
        /// </summary>
        public INetworkLayerFactory[] Layers
        {
            get { return _layers; }
            set
            {
                if (_layers != value)
                {
                    _layers = value;
                    Dirty = true;
                }
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
        /// Get or set UDP
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
        /// Get or set broadcast use for UDP
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
        /// Get or set the client factory
        /// </summary>
        public IProxyClientFactory Client
        {
            get { return _clientFactory; }
            set
            {
                if (_clientFactory != value)
                {
                    _clientFactory = value;
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
        /// Method to create the network service
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <returns>The network service</returns>
        /// <exception cref="NetServiceException">Thrown if invalid configuration</exception>
        public override ProxyNetworkService Create(Logger logger)
        {
            ProxyNetworkService ret = null;

            if (logger == null)
            {
                logger = Logger.GetSystemLogger();
            }

            if ((_port <= 0) || (_port > 65535))
            {
                throw new NetServiceException(Properties.Resources.FixedProxyDocument_MustProvideValidPort);
            }
            else if ((_localPort < 0) || (_localPort > 65535))
            {
                throw new NetServiceException(Properties.Resources.FixedProxyDocument_MustProvideValidLocalPort);
            }            
            else
            {
                try
                {                    
                    ProxyServer server = new FixedProxyServer(logger, _host, _port, 
                        _udp ? IpProxyToken.IpClientType.Udp : IpProxyToken.IpClientType.Tcp,
                        _ipv6, _layers);

                    ProxyClient client = null;

                    if (_clientFactory != null)
                    {
                        client = _clientFactory.Create(logger);
                    }
                    else
                    {
                        client = new IpProxyClient();
                    }

                    INetworkListener listener = null;

                    if (!_udp)
                    {
                        if (NetUtils.OSSupportsIPv4)
                        {
                            listener = new TcpNetworkListener(_anyBind, false, _localPort, logger, false);
                        }

                        if (_ipv6Bind && NetUtils.OSSupportsIPv6)
                        {
                            INetworkListener ipv6Listener = new TcpNetworkListener(_anyBind, true, _localPort, logger, false);

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
                            listener = new UdpNetworkListener(_anyBind, false, _localPort, _enableBroadcast, logger);
                        }

                        if (_ipv6Bind && NetUtils.OSSupportsIPv6)
                        {
                            INetworkListener ipv6Listener = new UdpNetworkListener(_anyBind, true, _localPort, _enableBroadcast, logger);

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

                    ret = new ProxyNetworkService(_packets,
                        listener,
                        _netGraph == null ? BuildDefaultProxyFactory() : _netGraph.Factory, logger, _globalMeta, _history,
                        _credentials, server, client, null, Timeout.Infinite, false);

                    ret.DefaultBinding = NetworkLayerBinding.ClientAndServer;
                }
                catch (SocketException ex)
                {
                    throw new NetServiceException(Properties.Resources.FixedProxyDocument_ErrorCreatingService, ex);
                }
                catch (IOException ex)
                {
                    throw new NetServiceException(Properties.Resources.FixedProxyDocument_ErrorCreatingService, ex);
                }

                return ret;
            }
        }

        /// <summary>
        /// Overridden method to get description of service
        /// </summary>
        /// <returns>The description</returns>
        public override string ToString()
        {
            return String.Format(Properties.Resources.FixedProxyDocument_ToString, 
                Name, _udp ? "UDP" : "TCP", _localPort, _host, _port);
        }
    }
}
