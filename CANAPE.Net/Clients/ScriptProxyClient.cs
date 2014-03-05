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
using System.Net;
using CANAPE.DataAdapters;
using CANAPE.Net.Tokens;
using CANAPE.Nodes;
using CANAPE.Security;
using CANAPE.Utils;

namespace CANAPE.Net.Clients
{
    /// <summary>
    /// A proxy client which uses a proxy auto-configure script
    /// </summary>
    public class ScriptProxyClient : ProxyClient
    {
        IWebProxyScript _proxyScript;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="proxyScript">The proxy script object</param>
        public ScriptProxyClient(IWebProxyScript proxyScript)
        {
            _proxyScript = proxyScript;
        }

        private ProxyClient CreateClient(Uri url, Logger logger)
        {
            string token = _proxyScript.Run(url.AbsoluteUri, url.Host);
            List<ProxyClient> clients = new List<ProxyClient>();

            if (token != null)
            {
                string[] proxies = token.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                logger.LogVerbose(String.Format(CANAPE.Net.Properties.Resources.ScriptProxyClient_ScriptReturned, token, url.AbsoluteUri));

                foreach (string proxy in proxies)
                {
                    string[] values = proxy.Trim().Split(new char[] { ' ' });

                    if (values.Length == 2)
                    {
                        string host = null;
                        int port = 0;

                        string[] hostport = values[1].Split(':');
                        if (hostport.Length == 2)
                        {
                            host = hostport[0].Trim();
                            int.TryParse(hostport[1].Trim(), out port);
                        }

                        if (String.IsNullOrWhiteSpace(host) || (port <= 0) || (port > 65535))
                        {
                            throw new ArgumentException(String.Format(CANAPE.Net.Properties.Resources.ScriptProxyClient_InvalidServer, proxy));
                        }

                        if (values[0].Equals("PROXY", StringComparison.OrdinalIgnoreCase))
                        {
                            clients.Add(new HttpProxyClient(host, port, false));
                        }
                        else if (values[0].Equals("SOCKS", StringComparison.OrdinalIgnoreCase))
                        {
                            clients.Add(new SocksProxyClient(host, port, false, SocksProxyClient.SupportedVersion.Version4, false));
                        }
                        else
                        {
                            throw new ArgumentException(String.Format(CANAPE.Net.Properties.Resources.ScriptProxyClient_InvalidType, values[0]));
                        }
                    }
                    else
                    {
                        clients.Add(new IpProxyClient());
                    }
                }
            }

            if (clients.Count > 0)
            {
                return new ChainProxyClient(clients.ToArray());
            }
            else
            {
                return new IpProxyClient();
            }
        }

        private ProxyClient BuildProxyClient(ProxyToken token, Logger logger)
        {
            Uri url = null;

            if (token is HttpProxyToken)
            {
                url = ((HttpProxyToken)token).Url;
            }
            else if (token is IpProxyToken)
            {
                IpProxyToken iptoken = token as IpProxyToken;

                UriBuilder builder = new UriBuilder();

                switch (iptoken.Port)
                {
                    case 443:
                        builder.Scheme = "https";
                        break;
                    case 21:
                        builder.Scheme = "ftp";
                        break;
                    default:
                        builder.Scheme = "http";
                        break;
                }

                builder.Port = iptoken.Port;
                builder.Host = iptoken.Hostname ?? iptoken.Address.ToString();
                builder.Path = "/";

                url = builder.Uri;
            }
            else
            {
                throw new ArgumentException(CANAPE.Net.Properties.Resources.ScriptProxyClient_InvalidToken, "token");
            }

            return CreateClient(url, logger);
        }

        /// <summary>
        /// Connect to the required service with the token
        /// </summary> 
        /// <param name="token">The token recevied from proxy</param>
        /// <param name="globalMeta">The global meta</param>
        /// <param name="logger">The logger</param>
        /// <param name="meta">The meta</param>
        /// <param name="properties">Property bag to add any information to</param>
        /// <param name="credmgr">Credentials manager</param>
        /// <returns>The connected data adapter</returns>
        public override IDataAdapter Connect(ProxyToken token, Logger logger, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, CredentialsManagerService credmgr)
        {            
            return BuildProxyClient(token, logger).Connect(token, logger, meta, globalMeta, properties, credmgr);
        }

        /// <summary>
        /// Bind to the required service with the token
        /// </summary> 
        /// <param name="token">The token recevied from proxy</param>
        /// <param name="globalMeta">The global meta</param>
        /// <param name="logger">The logger</param>
        /// <param name="meta">The meta</param>
        /// <param name="properties">Property bag to add any information to</param>
        /// <param name="credmgr">Credentials manager</param>
        /// <returns>The connected data adapter</returns>
        public override IDataAdapter Bind(ProxyToken token, Logger logger, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, CredentialsManagerService credmgr)
        {            
            return BuildProxyClient(token, logger).Bind(token, logger, meta, globalMeta, properties, credmgr);
        }
    }
}
