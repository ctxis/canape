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
using CANAPE.DataAdapters;
using CANAPE.Net.Layers;
using CANAPE.Net.Tokens;
using CANAPE.Utils;
using CANAPE.Nodes;
using CANAPE.Security;

namespace CANAPE.Net.Servers
{
    /// <summary>
    /// Basic proxy client which does nothing but accept an adapter connection
    /// </summary>
    public class PassThroughProxyServer : ProxyServer
    {
        INetworkLayerFactory[] _layers;

        private class PassThroughToken : ProxyToken
        {
            public IDataAdapter Adapter { get; set; }

            public PassThroughToken(IDataAdapter adapter)
            {
                Adapter = adapter;
            }

            protected override void OnDispose(bool finalize)
            {
                base.OnDispose(finalize);

                if (Adapter != null)
                {
                    try
                    {
                        Adapter.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Logger.GetSystemLogger().LogException(ex);
                    }

                    Adapter = null;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        public PassThroughProxyServer(Logger logger)
            : this(logger, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="layers"></param>
        public PassThroughProxyServer(Logger logger, INetworkLayerFactory[] layers)
            : base(logger)
        {
            _layers = layers;
        }

        /// <summary>
        /// Accept connection (just returns a default token)
        /// </summary>
        /// <param name="adapter">The server adapter</param>
        /// <param name="globalMeta"></param>
        /// <param name="meta"></param>
        /// <param name="service"></param>        
        /// <returns>The proxy token</returns>
        public override ProxyToken Accept(IDataAdapter adapter, MetaDictionary meta, MetaDictionary globalMeta, ProxyNetworkService service)
        {
            ProxyToken token = new PassThroughToken(adapter);
            token.Layers = _layers.CreateLayers(_logger);

            return token;            
        }

        /// <summary>
        /// Complete the client connection, just returns the original adapter
        /// </summary>
        /// <param name="token">The proxy token</param>
        /// <param name="client"></param>
        /// <param name="globalMeta"></param>
        /// <param name="meta"></param>
        /// <param name="service"></param>        
        /// <returns>The data adapter</returns>
        public override IDataAdapter Complete(ProxyToken token, MetaDictionary meta, MetaDictionary globalMeta, ProxyNetworkService service, IDataAdapter client)
        {
            PassThroughToken passToken = (PassThroughToken)token;
            IDataAdapter adapter = passToken.Adapter;
            if (token.Status != NetStatusCodes.Success)
            {               
                return null;
            }
            else
            {
                // Set to null to prevent Dispose being called
                passToken.Adapter = null;
                return adapter;
            }
        }
    }
}
