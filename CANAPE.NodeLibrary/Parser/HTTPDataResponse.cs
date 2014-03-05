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
using System.IO;
using System.Runtime.InteropServices;
using CANAPE.DataFrames;
using CANAPE.Parser;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Parser
{
    [Guid("40CF854F-AB68-47D7-92F8-000000000000")]
    public class HTTPDataResponse : HTTPData
    {
        public string Version { get; set; }
        public int Respcode { get; set; }
        public string Message { get; set; }

        protected override HTTPData.HttpVersion OnReadHeader(string header, Utils.Logger logger)
        {
            HttpVersion ver = HttpVersion.Unknown;
            string[] values = header.Split(new char[] { ' ' }, 3);
            if (values.Length < 2)
            {
                throw new InvalidDataException("Response header is invalid");
            }

            Version = values[0];
            ver = StringToVersion(values[0]);

            Respcode = int.Parse(values[1]);
            if (values.Length > 2)
            {
                Message = values[2];
            }

            return ver;
        }

        protected override string OnWriteHeader(Utils.Logger logger)
        {
            string ret = null;

            if (Version != null)
            {
                ret = String.Format("{0} {1}", Version, Respcode);
                if (Message != null)
                {
                    ret = String.Format("{0} {1}\r\n", ret, Message);
                }
                else
                {
                    ret = String.Format("{0}\r\n", ret);
                }
            }
            else
            {
                throw new InvalidDataException("Missing version and response code in frame");
            }

            return ret;
        }

        protected override bool IsRequest
        {
            get { return false; }
        }

        public static HTTPDataResponse FromDataKey(DataKey key)
        {
            return ObjectConverter.FromNode<HTTPDataResponse>(key);
        }

        public void ToDataKey(DataKey key)
        {
            ObjectConverter.ToNode(key, this);
        }

        public HTTPDataResponse(DataReader stm, Logger logger)
            : base(stm, logger)
        {
        }

        public HTTPDataResponse()
            : base()
        {
            Version = String.Empty;
        }

        protected override bool CanHaveBody()
        {
            switch(Respcode)
            {
                case 304:
                    return false;
                default: 
                    return true;
            }
        }
    }
}
