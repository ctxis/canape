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
using System.Threading;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net;
using CANAPE.Net.Clients;
using CANAPE.Net.Filters;
using CANAPE.Net.Listeners;
using CANAPE.Net.Servers;
using CANAPE.Net.Utils;
using CANAPE.Security;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// Document to represent a generic proxy
    /// </summary>
    [Serializable]
    public abstract class GenericProxyDocument : NetServiceDocument
    {
        /// <summary>
        /// Port
        /// </summary>
        protected int _port;
        /// <summary>
        /// AnyBind
        /// </summary>
        protected bool _anyBind;
        /// <summary>
        /// Ipv6 bind
        /// </summary>
        protected bool _ipv6Bind;
        /// <summary>
        /// Filters
        /// </summary>
        protected ProxyFilterFactory[] _filters;
        /// <summary>
        /// Client factory
        /// </summary>
        protected IProxyClientFactory _clientFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        public GenericProxyDocument()
            : base()
        {            
            _port = 1080;
            _filters = new ProxyFilterFactory[0];
            _clientFactory = new DefaultProxyClientFactory();
        }

        /// <summary>
        /// Method to create a proxy server
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <returns>The proxy server</returns>
        protected abstract ProxyServer CreateServer(Logger logger);

        /// <summary>
        /// Method to create a network service
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <returns>The network service</returns>
        /// <exception cref="NetServiceException">Thrown in configuration invalid</exception>
        public override ProxyNetworkService Create(Logger logger)
        {
            ProxyNetworkService ret = null;

            if (logger == null)
            {
                logger = Logger.GetSystemLogger();
            }

            if ((_port < 0) || (_port > 65535))
            {
                throw new NetServiceException(Properties.Resources.GenericProxyDocument_MustSpecifyAValidPort);
            }
            else 
            {
                try
                {
                    List<ProxyFilter> filters = new List<ProxyFilter>();

                    foreach (ProxyFilterFactory item in _filters)
                    {
                        filters.Add(item.CreateFilter());
                    }

                    ProxyServer server = CreateServer(logger);
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

                    if (listener == null)
                    {
                        throw new NetServiceException(CANAPE.Documents.Properties.Resources.NetServiceDocument_CannotSetupListener);
                    }

                    ret = new ProxyNetworkService(
                        _packets,
                        listener,
                        _netGraph == null ? BuildDefaultProxyFactory() : _netGraph.Factory, logger, _globalMeta, _history,
                        _credentials, server, client, filters.ToArray(), Timeout.Infinite, false);

                    ret.DefaultBinding = CANAPE.Net.Layers.NetworkLayerBinding.ClientAndServer;
                }
                catch (SocketException ex)
                {
                    throw new NetServiceException(Properties.Resources.GenericProxyDocument_ErrorCreatingService, ex);
                }
                catch (IOException ex)
                {
                    throw new NetServiceException(Properties.Resources.GenericProxyDocument_ErrorCreatingService, ex);
                }

                return ret;
            }
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
        /// Get or set port
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
        /// Get or set list of filters
        /// </summary>
        public ProxyFilterFactory[] Filters
        {
            get { return _filters; }
            set
            {
                if (_filters != value)
                {
                    _filters = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Add a filter to this service
        /// </summary>
        /// <param name="factory">The factory to add</param>
        public void AddFilter(ProxyFilterFactory factory)
        {
            _filters = AddFactory(factory, _filters);
        }

        /// <summary>
        /// Insert a filter into this factory
        /// </summary>
        /// <param name="factory">The factory to insert</param>
        /// <param name="index">The index of the factory</param>
        public void InsertFilter(int index, ProxyFilterFactory factory)
        {
            _filters = InsertFactory(index, factory, _filters);
        }

        /// <summary>
        /// Remove a filter by index
        /// </summary>
        /// <param name="index">The index to remove the layer at</param>
        public void RemoveFilterAt(int index)
        {
            _filters = RemoveFactoryAt(index, _filters);
        }

        /// <summary>
        /// Remove a filter
        /// </summary>
        /// <param name="factory">The filter to remove</param>
        public void RemoveFilter(ProxyFilterFactory factory)
        {
            _filters = RemoveFactory(factory, _filters);
        }

        /// <summary>
        /// Get or set proxy client
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
    }
}
