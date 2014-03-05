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
using CANAPE.DataFrames;
using CANAPE.Parser;

namespace CANAPE.Net.Protocols.Parser
{
    [Serializable]
    internal class HttpDataKey<T> : DataKey where T : HttpDataChunk
    {
        public HttpDataKey(string name)
            : base(name)
        {
        }

        public override void ToWriter(DataWriter stm)
        {
            T chunk = ObjectConverter.FromNode<T>(this);

            chunk.WriteChunk(stm);
        }

        public override string ToString()
        {
            DataValue chunkNumber = GetValue("ChunkNumber");
            DataValue finalChunk = GetValue("FinalChunk");
            string chunkFormat = "";

            if ((chunkNumber != null) && (finalChunk != null))
            {
                if ((chunkNumber.Value > 0) || !finalChunk.Value)
                {
                    chunkFormat = String.Format(" Chunk: {0} Final: {1}", chunkNumber.Value, finalChunk.Value);
                }
            }

            if (typeof(T) == typeof(HttpRequestDataChunk))
            {
                DataValue method = GetValue("Method") as DataValue;
                DataValue path = GetValue("Path") as DataValue;
                               
                return String.Format("HTTP Request: {0} {1}{2}", method, path, chunkFormat);
            }
            else
            {
                DataValue respcode = SelectSingleNode("ResponseCode") as DataValue;
                DataValue message = SelectSingleNode("Message") as DataValue;

                return String.Format("HTTP Response: {0} {1}{2}", respcode, message, chunkFormat);
            }
        }
    }

}
