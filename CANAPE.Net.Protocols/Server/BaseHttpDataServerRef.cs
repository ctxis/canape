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
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Net.Protocols.Parser;
using CANAPE.Parser;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Net.Protocols.Server
{
    /// <summary>
    /// Base HTTP Data Server class with persistence
    /// </summary>
    /// <typeparam name="T">Type of configuration class to persist</typeparam>
    /// <typeparam name="R">Reference type to access the configuration</typeparam>
    public abstract class BaseHttpDataServerRef<T,R> : BasePersistDataEndpointRef<T,R> where R : class where T : class, R, new()
    {       
        /// <summary>
        /// Handle a HTTP request
        /// </summary>
        /// <param name="method">The HTTP method</param>
        /// <param name="path">The HTTP path</param>
        /// <param name="body">The body of the data</param>
        /// <param name="headers">A dictionary of headers</param>
        /// <param name="version">HTTP version</param>
        /// <param name="logger">A logger to log data to</param>
        /// <returns>A HTTP response data object, or null if no response</returns>
        protected abstract HttpServerResponseData HandleRequest(string method, string path, byte[] body, 
            Dictionary<string, string> headers, HttpVersion version, Logger logger);

        /// <summary>
        /// Run the end point
        /// </summary>
        /// <param name="adapter">The data adapter</param>
        /// <param name="logger">The logger to log data to</param>
        public sealed override void Run(IDataAdapter adapter, Logger logger)
        {            
            DataReader reader = new DataReader(new DataAdapterToStream(adapter));

            while (true)
            {
                HttpRequestHeader request = HttpParser.ReadRequestHeader(reader, false, logger);
                HttpRequestDataChunk req = request.ReadRequest();                

                Dictionary<string, string> headers = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
                foreach(KeyDataPair<string> pair in req.Headers)
                {
                    headers[pair.Name] = pair.Value;
                }

                HttpServerResponseData data = HandleRequest(req.Method, req.Path, req.Body, headers, req.Version, logger);

                HttpResponseDataChunk response = new HttpResponseDataChunk();
                
                response.Version = request.Version;
                response.ResponseCode = data.ResponseCode;
                response.Message = data.Message;
                
                List<KeyDataPair<string>> newHeaders = new List<KeyDataPair<string>>();

                foreach (KeyValuePair<string, string> pair in data.Headers)
                {
                    newHeaders.Add(new KeyDataPair<string>(pair.Key, pair.Value));                    
                }

                if (!data.Headers.ContainsKey("content-length"))
                {
                    newHeaders.Add(new KeyDataPair<string>("Content-Length", data.Body.Length.ToString()));
                }

                response.Headers = newHeaders.ToArray();                
                response.FinalChunk = true;

                if (request.Method.Equals("HEAD", StringComparison.OrdinalIgnoreCase))
                {
                    response.Body = new byte[0];
                    response.HeadResponse = true;
                }
                else
                {
                    response.Body = data.Body;
                }

                adapter.Write(new DataFrame((DataKey)response.ToNode("Root")));

                if (data.CloseAfterSending || request.Version.IsVersionUnknown || request.Version.IsVersion10)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Description of the server
        /// </summary>
        public override string Description
        {
            get { return "HTTP Server"; }
        }
    }
}
