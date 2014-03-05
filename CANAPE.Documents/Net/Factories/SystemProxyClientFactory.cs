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
using System.Linq;
using System.Net;
using System.Security;
using CANAPE.Net.Clients;
using Microsoft.Win32;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// A factory to use the current system settings
    /// </summary>
    [Serializable]
    public class SystemProxyClientFactory : BaseProxyClientFactory
    {
        [NonSerialized]
        private ScriptProxyClientFactory _scriptFactory;

        /// <summary>
        /// Method to create a proxy client
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <returns>The new proxy client</returns>
        public override ProxyClient Create(Utils.Logger logger)
        {
            ProxyClient ret = new IpProxyClient();

            try
            {
                RegistryKey settings = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");

                int enabled = (int)settings.GetValue("ProxyEnable", 0);

                if (enabled != 0)
                {
                    string autoConfigUrl = settings.GetValue("AutoConfigURL") as string;

                    if (autoConfigUrl != null)
                    {
                        Uri autoConfigUri = new Uri(autoConfigUrl, UriKind.Absolute);

                        if ((_scriptFactory == null) || (!_scriptFactory.ScriptUri.Equals(autoConfigUri)))
                        {
                            using (WebClient client = new WebClient())
                            {
                                client.Proxy = null;
                                string scriptData = client.DownloadString(autoConfigUrl);

                                logger.LogVerbose("Received auto config script from {0}", autoConfigUrl);
                                logger.LogVerbose(scriptData);

                                _scriptFactory = new ScriptProxyClientFactory();
                                _scriptFactory.ScriptUri = autoConfigUri;
                                _scriptFactory.Script = scriptData;
                            }
                        }

                        ret = _scriptFactory.Create(logger);
                    }
                    else
                    {
                        string proxyServer = settings.GetValue("ProxyServer") as string;

                        if (proxyServer != null)
                        {
                            string[] servers = proxyServer.ToLower().Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                            string currServer = null;
                            bool socks = false;

                            // Take socks in preference, otherwise accept HTTP or default                      
                            foreach (string server in servers)
                            {
                                if (server.Contains('='))
                                {
                                    if (server.StartsWith("socks="))
                                    {
                                        currServer = server.Substring(6).Trim();
                                        logger.LogVerbose("Found system SOCKS server {0}", currServer);
                                        socks = true;
                                        break;
                                    }
                                    else if (server.StartsWith("http="))
                                    {
                                        currServer = server.Substring(5).Trim();
                                        logger.LogVerbose("Found system HTTP proxy {0}", currServer);
                                    }
                                }
                                else
                                {
                                    currServer = server.Trim();
                                    logger.LogVerbose("Found default HTTP proxy {0}", currServer);
                                }
                            }

                            if (currServer != null)
                            {
                                string host = null;
                                int port = 0;

                                if (currServer.Contains("/"))
                                {
                                    if (Uri.IsWellFormedUriString(currServer, UriKind.Absolute))
                                    {
                                        Uri uri = new Uri(currServer);

                                        host = uri.Host;
                                        port = uri.Port;
                                    }
                                }
                                else
                                {
                                    string[] values = currServer.Split(':');
                                    if (values.Length == 2)
                                    {
                                        host = values[0].Trim();
                                        int.TryParse(values[1].Trim(), out port);
                                    }
                                }

                                if (String.IsNullOrWhiteSpace(host) || (port <= 0) || (port > 65535))
                                {
                                    logger.LogError("Invalid system proxy string {0}", currServer);
                                }
                                else
                                {
                                    if (socks)
                                    {
                                        ret = new SocksProxyClient(host, port, false, SocksProxyClient.SupportedVersion.Version4, false);
                                    }
                                    else
                                    {
                                        ret = new HttpProxyClient(host, port, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SecurityException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (WebException ex)
            {
                logger.LogException(ex);
            }

            return ret;
        }
    }
}
