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
using System.Text;
using CANAPE.DataAdapters;
using CANAPE.Net.Layers;
using CANAPE.Net.Tokens;
using CANAPE.Utils;
using CANAPE.Nodes;
using System.IO;

namespace CANAPE.Net.Servers
{
    /// <summary>
    /// Legacy HTTP proxy server, probably shouldn't be used
    /// </summary>
    public class LegacyHttpProxyServer : ProxyServer
    {
        private SslNetworkLayer _ssl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ssl"></param>
        public LegacyHttpProxyServer(Logger logger, SslNetworkLayer ssl)
            : base(logger)
        {
            _ssl = ssl;
        }

        private void ReturnResponse(int no, string description, DataAdapterToStream stm)
        {
            byte[] response = Encoding.ASCII.GetBytes(String.Format("HTTP/1.0 {0} {1}\r\n\r\n", no, description));
            stm.Write(response, 0, response.Length);
        }

        private string GetProbableProtocol(int port)
        {
            switch (port)
            {
                case 443: return "https";
                case 21: return "ftp";
                default: return "http";
            }
        }

        private HttpProxyToken HandleConnect(string host, string[] headers, DataAdapterToStream stm)
        {
            string hostName = null;
            int port = 80;
            string[] connectHeader = host.Split(':');
            HttpProxyToken ret = null;            

            if (connectHeader.Length > 0)
            {
                hostName = connectHeader[0];
                if (connectHeader.Length > 1)
                {
                    if (!int.TryParse(connectHeader[1], out port))
                    {
                        _logger.LogError(CANAPE.Net.Properties.Resources.HttpProxyServer_InvalidConnect, connectHeader[1]);
                        port = 0;
                    }
                }

                if (port > 0)
                {
                    UriBuilder builder = new UriBuilder(GetProbableProtocol(port), hostName);
                    builder.Port = port;

                    ret = new HttpProxyToken(hostName, port, true, headers, builder.Uri, stm);
                }
                else
                {
                    // TODO: Put in some decent error codes
                    ReturnResponse(500, "Server Error", stm);
                }
            }

            return ret;
        }

        private HttpProxyToken HandleOtherRequest(string host, string[] headers, DataAdapterToStream stm)
        {
            Uri url;

            if(Uri.TryCreate(host, UriKind.Absolute, out url))                        
            {
                return new HttpProxyToken(url.Host, url.Port, false, headers, url, stm);
            }
            else
            {                
                _logger.LogError(CANAPE.Net.Properties.Resources.HttpProxyServer_InvalidUrl, host);

                // TODO: Put in some decent error codes
                ReturnResponse(500, "Server Error", stm);

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="globalMeta"></param>
        /// <param name="meta"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public override ProxyToken Accept(IDataAdapter adapter, MetaDictionary meta, MetaDictionary globalMeta, ProxyNetworkService service)
        {
            HttpProxyToken token = null;            

            if (_ssl != null)
            {
                IDataAdapter client = null;

                _ssl.Negotiate(ref adapter, ref client, null, _logger, null, null, 
                    new PropertyBag("Root"), NetworkLayerBinding.Server);
            }

            DataAdapterToStream stm = new DataAdapterToStream(adapter);

            List<string> headers = new List<string>();

            // Read out HTTP headers
            try
            {
                while (true)
                {
                    string nextLine = GeneralUtils.ReadLine(stm);

                    headers.Add(nextLine);

                    if (nextLine.Trim('\r', '\n').Length == 0)
                    {
                        break;
                    }
                }
            }
            catch (EndOfStreamException)
            {
                // Pass on the exception if we got killed half way through
                if (headers.Count > 0)
                {                    
                    throw;
                }                
            }

            if (headers.Count > 0)
            {
                string[] reqValues = headers[0].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Check it at least has a VERB and a PATH
                if (reqValues.Length > 1)
                {
                    if (reqValues[0].Equals("CONNECT", StringComparison.OrdinalIgnoreCase))
                    {
                        token = HandleConnect(reqValues[1], headers.ToArray(), stm);
                    }
                    else
                    {
                        token = HandleOtherRequest(reqValues[1], headers.ToArray(), stm);
                    }
                }
                else
                {
                    _logger.LogError(CANAPE.Net.Properties.Resources.HttpProxyServer_InvalidRequest, headers[0]);

                    // TODO: Put in some decent error codes
                    ReturnResponse(500, "Server Error", stm);
                }
            }

            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client"></param>
        /// <param name="globalMeta"></param>
        /// <param name="meta"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public override IDataAdapter Complete(ProxyToken token, MetaDictionary meta, MetaDictionary globalMeta, ProxyNetworkService service, IDataAdapter client)
        {
            IDataAdapter ret = null;
            HttpProxyToken httpToken = (HttpProxyToken)token;
            DataAdapterToStream stm = httpToken.Adapter;

            if (httpToken.Status == NetStatusCodes.Success)
            {
                if (httpToken.IsHTTPProxyClient)
                {
                    // We don't have to do anything as such, other than send back any smuggled data if it was a connect call
                    if (httpToken.Response != null)
                    {
                        stm.Write(httpToken.Response, 0, httpToken.Response.Length);
                    }

                    httpToken.Adapter = null;

                    if (httpToken.Connect)
                    {
                        // With CONNECT the data stream is transparent
                        ret = new StreamDataAdapter(stm);
                    }
                    else
                    {
                        // For anything else, rebuild the original headers so it can flow through the graph
                        StringBuilder builder = new StringBuilder();
                        
                        foreach (string s in httpToken.Headers)
                        {
                            builder.Append(s);
                        }

                        ret = new PrefixedDataAdapter(new StreamDataAdapter(stm), GeneralUtils.MakeByteArray(builder.ToString()));
                    }
                }
                else
                {
                    if (httpToken.Connect)
                    {
                        ReturnResponse(200, "Connection established", stm);
                        
                        httpToken.Adapter = null;
                        ret = new StreamDataAdapter(stm);
                    }
                    else
                    {                        
                        StringBuilder builder = new StringBuilder();

                        string[] reqValues = httpToken.Headers[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        // Downgrade to version 1.0
                        httpToken.Headers[0] = reqValues[0] + " " + httpToken.Url.PathAndQuery + " HTTP/1.0\r\n";
                        foreach (string s in httpToken.Headers)
                        {
                            // Remove proxy headers
                            if (!s.StartsWith("proxy", StringComparison.OrdinalIgnoreCase))
                            {
                                builder.Append(s);
                            }
                        }

                        httpToken.Adapter = null;
                        
                        ret = new PrefixedDataAdapter(new StreamDataAdapter(stm), GeneralUtils.MakeByteArray(builder.ToString()));
                    }
                }
            }
            else
            {
                ReturnResponse(404, "Not Found", stm);          
            }

            return ret;
        }
    }
}
