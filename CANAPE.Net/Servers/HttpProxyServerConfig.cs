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
using CANAPE.Net.Layers;
using CANAPE.Utils;

namespace CANAPE.Net.Servers
{
    /// <summary>
    /// Configuration for HTTP proxy
    /// </summary>
    [Serializable]
    public class HttpProxyServerConfig
    {       
        /// <summary>
        /// 
        /// </summary>
        public bool Version10Proxy
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public SslNetworkLayerConfig SslConfig
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RequireAuth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProxyUsername { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProxyPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthRealm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DebugLog { get; set; }


        /// <summary>
        /// Specify the number of times we will retry to connect a connection
        /// </summary>
        public int ConnectionRetries { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpProxyServerConfig Clone()
        {
            return (HttpProxyServerConfig)GeneralUtils.CloneObject(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpProxyServerConfig()
        {
            SslConfig = new SslNetworkLayerConfig(true, false);
            ProxyUsername = "canape";
            ProxyPassword = "canape";
            AuthRealm = "canape.local";
            ConnectionRetries = 2;
        }
    }
}
