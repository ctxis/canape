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
using System.Globalization;
using System.IO;
using CANAPE.DataFrames;
using CANAPE.Parser;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Parser
{    
    public abstract class HTTPData 
    {
        public KeyDataPair<string>[] Headers { get; set; }
        public byte[] Body { get; set; }

        protected abstract HttpVersion OnReadHeader(string header, Logger logger);
        protected abstract string OnWriteHeader(Logger logger);

        protected enum HttpVersion
        {
            Unknown,
            Http1_0,
            Http1_1
        }

        private bool IsGetRequest()
        {
            HTTPDataRequest req = this as HTTPDataRequest;

            if (req != null)
            {
                if (req.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        protected HttpVersion StringToVersion(string verString)
        {
            if (verString.Equals("http/1.0", StringComparison.OrdinalIgnoreCase))
            {
                return HttpVersion.Http1_0;
            }
            else if (verString.Equals("http/1.1", StringComparison.OrdinalIgnoreCase))
            {
                return HttpVersion.Http1_1;
            }
            else
            {
                return HttpVersion.Unknown;
            }
        }
        
        private byte[] ReadChunk(DataReader stm, Logger logger)
        {
            string s = stm.ReadLine();
            if (s.Length == 0)
            {
                return new byte[0];
            }

            string lenstr = s.Trim();
            if (lenstr.Length == 0)
            {
                return new byte[0];
            }

            lenstr = lenstr.Split(new char[] { ';' }, 1)[0];
            int lenval = int.Parse(lenstr, NumberStyles.HexNumber);

            if (lenval > 0)
            {
                byte[] data = stm.ReadBytes(lenval);
                stm.ReadLine();

                return data;
            }
            else
            {
                // Read out final trailing data
                string line = stm.ReadLine();
                while ((line.Length > 0) && (line.Trim().Length > 0))
                {
                    line = stm.ReadLine();
                }
            }

            return new byte[0];
        }

        public void ReadHeaders(DataReader stm, Logger logger)
        {
            List<KeyDataPair<string>> headers = new List<KeyDataPair<string>>();

            while (true)
            {
                string line = stm.ReadLine();

                if (line.Length == 0)
                {
                    throw new EndOfStreamException();
                }

                line = line.Trim();

                if (line.Length == 0)
                {
                    break;
                }

                string[] values = line.Split(new char[] { ':' }, 2);

                if (values.Length != 2)
                {
                    throw new InvalidDataException(String.Format("Header '{0}' is invalid", line));
                }

                headers.Add(new KeyDataPair<string>(values[0].Trim(), values[1].Trim()));
            }

            Headers = headers.ToArray();
        }

        public HTTPData()
        {
            Headers = new KeyDataPair<string>[0];
            Body = new byte[0];
        }

        protected abstract bool CanHaveBody();

        public HTTPData(DataReader stm, Logger logger)
        {
            string header = stm.ReadLine();            
            int contentLength = -1;
            bool chunked = false;
            byte[] body = null;
            bool connClose = false;

            if (header.Length == 0)
            {
                throw new EndOfStreamException();
            }

            HttpVersion ver = OnReadHeader(header.Trim(), logger);

            ReadHeaders(stm, logger);

            foreach (KeyDataPair<string> value in Headers)
            {
                if (value.Name.Equals("content-length", StringComparison.OrdinalIgnoreCase))
                {
                    contentLength = int.Parse(value.Value.ToString());
                    break;
                }
                else if (value.Name.Equals("transfer-encoding", StringComparison.OrdinalIgnoreCase))
                {
                    if (value.Value.ToString().Equals("chunked", StringComparison.OrdinalIgnoreCase))
                    {
                        chunked = true;
                        break;
                    }
                }
                else if (value.Name.Equals("connection", StringComparison.OrdinalIgnoreCase))
                {
                    // If the server indicates it will close the connection afterwards
                    if (value.Value.ToString().Equals("close", StringComparison.OrdinalIgnoreCase))
                    {
                        connClose = true;
                    }
                }
            }

            body = new byte[0];

            if (CanHaveBody())
            {
                if (chunked)
                {
                    List<byte> bodyData = new List<byte>();
                    byte[] data = ReadChunk(stm, logger);

                    while (data.Length > 0)
                    {
                        bodyData.AddRange(data);

                        data = ReadChunk(stm, logger);
                    }

                    body = bodyData.ToArray();
                }
                else if (contentLength > 0)
                {
                    body = stm.ReadBytes(contentLength);
                }
                else if ((contentLength < 0) && ((ver == HttpVersion.Http1_0) || connClose) && !IsRequest)
                {
                    // HTTP1.0 waits till end of stream (need to only do this on response though)
                    // Also sometimes happen on v1.1 if the client indicates that it is closing the connection
                    body = stm.ReadToEnd();
                }
            }
            
            Body = body;
        }


        public void ToWriter(DataWriter stm, Logger logger)
        {
            if ((Headers != null) && (Body != null))
            {               
                stm.Write(OnWriteHeader(logger));

                foreach (KeyDataPair<string> header in Headers)
                {
                    if (!header.Name.Equals("content-length", StringComparison.OrdinalIgnoreCase)
                        && !header.Name.Equals("transfer-encoding", StringComparison.OrdinalIgnoreCase))
                    {                        
                        stm.Write(String.Format("{0}: {1}\r\n", header.Name, header.Value));
                    }
                }

                if (!IsRequest || (Body.Length > 0) || !IsGetRequest())
                {
                    stm.Write(String.Format("Content-Length: {0}\r\n", Body.Length));
                }

                stm.Write("\r\n");
                
                if (Body.Length > 0)
                {
                    stm.Write(Body);
                }
            }
        }

        protected abstract bool IsRequest { get; }
    }
}
