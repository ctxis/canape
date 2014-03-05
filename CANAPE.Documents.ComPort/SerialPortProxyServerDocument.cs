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
using System.Threading;
using CANAPE.Documents.Net;
using CANAPE.Net;
using CANAPE.Net.Layers;
using CANAPE.Net.Servers;
using CANAPE.Security;
using CANAPE.Utils;

namespace CANAPE.Documents.ComPort
{    
    [Serializable]
    public class SerialPortProxyServerDocument : NetServiceDocument
    {
        private INetworkLayerFactory[] _layers;

        public SerialPortConfiguration ServerPort { get; private set; }
        public SerialPortConfiguration ClientPort { get; private set; }

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

        public SerialPortProxyServerDocument()
        {
            _layers = new INetworkLayerFactory[0];
            ServerPort = new SerialPortConfiguration("COM1", this);
            ClientPort = new SerialPortConfiguration("COM2", this);
        }        

        public override ProxyNetworkService Create(Logger logger)
        {
            ProxyNetworkService ret = null;

            if (logger == null)
            {
                logger = Logger.GetSystemLogger();
            }
            
            try
            {
                ProxyServer server = new PassThroughProxyServer(logger, _layers);    

                ret = new ProxyNetworkService(_packets, new SerialPortProxyListener(ServerPort), 
                    _netGraph == null ? BuildDefaultProxyFactory() : _netGraph.Factory, logger, _globalMeta, _history,
                    _credentials, server, new SerialPortProxyClient(ClientPort), null, Timeout.Infinite, false);

                ret.DefaultBinding = NetworkLayerBinding.ClientAndServer;
            }
            catch (Exception ex)
            {
                if(ex is NetServiceException)
                {
                    throw;
                }

                throw new NetServiceException("Error creating service", ex);
            }
            
            return ret;        
        }

        public override string DefaultName
        {
            get { return "Serial Port Proxy"; }
        }
    }
}
