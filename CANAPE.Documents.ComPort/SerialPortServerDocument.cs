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
using System.Linq;
using System.Text;
using System.Threading;
using CANAPE.DataAdapters;
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net;
using CANAPE.Net.Clients;
using CANAPE.Net.Layers;
using CANAPE.Net.Servers;
using CANAPE.Net.Tokens;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Security;
using CANAPE.Utils;

namespace CANAPE.Documents.ComPort
{
    [Serializable]
    public class SerialPortServerDocument : NetServiceDocument
    {
        private INetworkLayerFactory[] _layers;
        private DataEndpointFactory _factory;

        public SerialPortConfiguration ServerPort { get; private set; }

        public SerialPortServerDocument()
        {
            _layers = new INetworkLayerFactory[0];
            ServerPort = new SerialPortConfiguration("COM1", this);
        }

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

        public INetworkLayerFactory[] Layers
        {
            get { return _layers; }
            set
            {
                if (_layers != value)
                {
                    _layers = value ?? new INetworkLayerFactory[0];
                    Dirty = true;
                }
            }
        }

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

        public override ProxyNetworkService Create(Utils.Logger logger)
        {
            ProxyNetworkService ret = null;

            if (logger == null)
            {
                logger = Logger.GetSystemLogger();
            }

            if (_factory == null)
            {
                throw new NetServiceException("Must specify a server");
            }

            try
            {
                ProxyServer server = new PassThroughProxyServer(logger, _layers);

                ret = new ProxyNetworkService(_packets, new SerialPortProxyListener(ServerPort),
                    _netGraph == null ? BuildDefaultProxyFactory() : _netGraph.Factory, logger, _globalMeta, _history,
                    _credentials, server, new NetServerProxyClient(_factory), null, Timeout.Infinite, false);

                ret.DefaultBinding = NetworkLayerBinding.ClientAndServer;
            }
            catch (Exception ex)
            {
                if (ex is NetServiceException)
                {
                    throw;
                }

                throw new NetServiceException("Error creating service", ex);
            }

            return ret;     
        }

        public override string DefaultName
        {
            get { return "Serial Port Server"; }
        }
    }
}
