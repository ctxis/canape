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
using System.Globalization;
using System.Runtime.InteropServices;
using CANAPE.Parser;

namespace CANAPE.Net.Protocols.Parser
{
    /// <summary>
    /// 
    /// </summary>
    [Guid("40CF854F-AB68-47D7-92F8-000000000002")]
    public sealed class HttpResponseDataChunk : HttpDataChunk
    {
        /// <summary>
        /// 
        /// </summary>
        public int ResponseCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ReadOnlyMember(true)]
        public bool HeadResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ReadOnlyMember(true)]
        public bool ConnectResponse { get; set; }        

        /// <summary>
        /// 
        /// </summary>
        public HttpResponseDataChunk()
        {
        }

        internal HttpResponseDataChunk(HttpResponseHeader header) : base(header.Headers, header.Version)
        {
            ResponseCode = header.ResponseCode;
            Message = header.Message;
            HeadResponse = header.HeadRequest;
            ConnectResponse = header.ConnectRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string OnWriteHeader()
        {
            string ret = null;

            if (!Version.IsVersionUnknown)
            {                
                if (!String.IsNullOrWhiteSpace(Message))
                {
                    ret = String.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", Version, ResponseCode, Message);
                }
                else
                {
                    ret = String.Format(CultureInfo.InvariantCulture, "{0} {1}", Version, ResponseCode);
                }
            }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool CanSendBody()
        {
            if (HeadResponse || ConnectResponse || ResponseCode == 304 || (ResponseCode >= 100 && ResponseCode < 200))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool FilterContentLength()
        {
            return !HeadResponse && !ConnectResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override DataFrames.DataNode ToNode(string name)
        {
            HttpDataKey<HttpResponseDataChunk> root = new HttpDataKey<HttpResponseDataChunk>(name);

            ObjectConverter.ToNode(root, this);

            return root;
        }
    }
}
