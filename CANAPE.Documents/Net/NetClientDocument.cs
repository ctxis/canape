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
using CANAPE.Security;
using CANAPE.Security.Cryptography.X509Certificates;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// Document to represent a network client
    /// </summary>
    [Serializable]
    public abstract class NetClientDocument : NetServiceDocument
    {
        private IProxyClientFactory _clientFactory;
        private int _port;
        private string _destination;
        private bool _ipv6;
        private bool _udp;

        // Deprecated
        private SslNetworkLayerConfig _sslConfig;

        private INetworkLayerFactory[] _layers;

        /// <summary>
        /// Constructor
        /// </summary>
        public NetClientDocument()
        {
            _clientFactory = new DefaultProxyClientFactory();
            _layers = new INetworkLayerFactory[0];

            _port = 12345;
            _destination = "127.0.0.1";
        }

        /// <summary>
        /// On deserialization callback
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

        void packets_CollectionChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        /// Get or set the proxy client
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

        /// <summary>
        /// Get or set the port used for the connection
        /// </summary>
        public int Port
        {
            get
            {
                return _port;
            }

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
        /// Get or set the destination for the connection
        /// </summary>
        public string Destination
        {
            get
            {
                return _destination;
            }

            set
            {
                if (_destination != value)
                {
                    _destination = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set whether to use IPv6 if available
        /// </summary>
        public bool IPv6
        {
            get
            {
                return _ipv6;
            }

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
        /// Get or set whether the client uses UDP
        /// </summary>
        public bool Udp
        {
            get
            {
                return _udp;
            }

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
        /// Network layers to apply to client
        /// </summary>
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
        /// Get document default name
        /// </summary>
        public override string DefaultName
        {
            get { return "Network Client"; }
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
        /// Method to create the listener
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <returns>The network listener</returns>
        protected abstract INetworkListener CreateListener(Logger logger);

        /// <summary>
        /// Create the network service from this document
        /// </summary>
        /// <param name="logger">The logger for the service</param>
        /// <returns>The new network service</returns>
        public override ProxyNetworkService Create(Logger logger)
        {
            ProxyNetworkService ret = null;

            if (logger == null)
            {
                logger = Logger.GetSystemLogger();
            }

            if ((_port <= 0) || (_port > 65535))
            {
                throw new NetServiceException("Must provide a valid port");
            }
            else
            {
                try
                {                    
                    ProxyServer server = new FixedProxyServer(logger, _destination, _port, _udp ? IpProxyToken.IpClientType.Udp : IpProxyToken.IpClientType.Tcp,
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
                    
                    ret = new ProxyNetworkService(
                        _packets,
                        CreateListener(logger), _netGraph == null ? BuildDefaultProxyFactory() : _netGraph.Factory, logger, _globalMeta, _history,
                                _credentials, server, client, null, Timeout.Infinite, false);

                    ret.DefaultBinding = NetworkLayerBinding.Client;
                }
                catch (SocketException ex)
                {
                    throw new NetServiceException("Error creating network service", ex);
                }
                catch (IOException ex)
                {
                    throw new NetServiceException("Error creating network service", ex);
                }

                return ret;
            }
        }
    }
}
