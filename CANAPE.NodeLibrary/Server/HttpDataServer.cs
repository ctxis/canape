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
using System.ComponentModel;
using System.Text.RegularExpressions;
using CANAPE.Net.Protocols.Parser;
using CANAPE.Net.Protocols.Server;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Server
{
    /// <summary>
    /// Config for simple HTTP server
    /// </summary>
    [Serializable]
    public class HttpDataServerConfig
    {
        /// <summary>
        /// A HTTP path to match against
        /// </summary>
        [LocalizedDescription("HttpDataServerConfig_HttpPathDescription", typeof(Properties.Resources)), Category("Control")]
        public string HttpPath { get; set; }

        [LocalizedDescription("HttpDataServerConfig_ValidResponseDataDescription", typeof(Properties.Resources)), Category("Control")]
        public byte[] ValidResponseData { get; set; }

        [LocalizedDescription("HttpDataServerConfig_NotFoundResponseDataDescription", typeof(Properties.Resources)), Category("Control")]
        public byte[] NotFoundResponseData { get; set; }

        [LocalizedDescription("HttpDataServerConfig_CloseAfterSendingDescription", typeof(Properties.Resources)), Category("Control")]
        public bool CloseAfterSending { get; set; }

        [LocalizedDescription("HttpDataServerConfig_ContentTypeDescription", typeof(Properties.Resources)), Category("Control")]
        public string ContentType { get; set; }

        public HttpDataServerConfig()
        {
            HttpPath = "/*";
            ContentType = "text/html";
            ValidResponseData = new byte[0];
            NotFoundResponseData = new byte[0];
            CloseAfterSending = true;
        }
    }

    [NodeLibraryClass("HttpDataServer", typeof(Properties.Resources),
        Category = NodeLibraryClassCategory.Server,
        ConfigType = typeof(HttpDataServerConfig))]
    public class HttpDataServer : BaseHttpDataServer<HttpDataServerConfig>
    {        
        protected override HttpServerResponseData HandleRequest(string method, string path, byte[] body, 
            Dictionary<string, string> headers, HttpVersion version, Logger logger)
        {
            Regex pathRegex = GeneralUtils.GlobToRegex(Config.HttpPath);
            HttpServerResponseData data = new HttpServerResponseData();

            data.CloseAfterSending = Config.CloseAfterSending;

            if (pathRegex.IsMatch(path))
            {
                data.ResponseCode = 200;
                data.Message = "OK";
                if (Config.ValidResponseData != null)
                {
                    data.Body = Config.ValidResponseData;
                }                
            }
            else
            {
                data.ResponseCode = 404;
                data.Message = "Not Found";
                if (Config.NotFoundResponseData != null)
                {
                    data.Body = Config.NotFoundResponseData;
                }                
            }

            data.Headers["Content-Type"] = Config.ContentType ?? "text/html";

            return data;
        }

        public override string Description
        {
            get { return "Simple HTTP Server"; }
        }
    }
}
