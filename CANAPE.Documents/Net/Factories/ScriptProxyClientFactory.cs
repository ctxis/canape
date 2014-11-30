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
using CANAPE.Net.Clients;
using System.Net;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Client factory for a PAC script client
    /// </summary>
    [Serializable]
    public sealed class ScriptProxyClientFactory : BaseProxyClientFactory
    {
        [NonSerialized]
        private IWebProxyScript _proxyScript;

        [NonSerialized]
        Type webProxyScriptType = Type.GetType("System.Net.VsaWebProxyScript, Microsoft.JScript, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);

        [NonSerialized]
        Type webProxyScriptHelperType = typeof(System.Net.WebClient).Assembly.GetType("System.Net.WebProxyScriptHelper");
        
        private string _script;
        private Uri _scriptUri;

        /// <summary>
        /// The Javascript PAC code
        /// </summary>
        public string Script
        {
            get { return _script; }
            set
            {
                if (_script != value)
                {
                    _script = value;
                    _proxyScript = null;
                    OnConfigChanged();
                }
            }
        }

        /// <summary>
        /// The script uri (if required)
        /// </summary>
        public Uri ScriptUri
        {
            get { return _scriptUri; }
            set
            {
                if (_scriptUri != value)
                {
                    _scriptUri = value;
                    _proxyScript = null;
                    OnConfigChanged();
                }
            }
        }

        /// <summary>
        /// Get the proxy script instance
        /// </summary>
        /// <returns>The proxy instance</returns>
        public IWebProxyScript GetProxyInstance()
        {
            if (_proxyScript == null)
            {                
                if ((webProxyScriptType != null) && (webProxyScriptHelperType != null))
                {
                    _proxyScript = (IWebProxyScript)Activator.CreateInstance(webProxyScriptType);

                    _proxyScript.Load(_scriptUri ?? new Uri("http://www.contextis.co.uk", UriKind.Absolute),
                        _script, webProxyScriptHelperType);
                }
            }

            return _proxyScript;
        }

        /// <summary>
        /// Create client
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <returns>The new proxy client</returns>
        public override ProxyClient Create(Utils.Logger logger)
        {            
            logger.LogError(Properties.Resources.ScriptProxyFactory_CannotCreateScript);
            return new IpProxyClient();

            // TODO: Implement a better way of using this

            // IWebProxyScript proxyScript = GetProxyInstance();
            //if (proxyScript == null)
            //{
            //    logger.LogError(Properties.Resources.ScriptProxyFactory_CannotCreateScript);
            //    return new IpProxyClient();
            //}
            //else
            //{
            //    return new ScriptProxyClient(proxyScript);
            //}
        }
    }
}
