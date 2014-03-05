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
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Parser
{
    [NodeLibraryClass("HttpRequestStreamParser", typeof(Properties.Resources),
        Category = NodeLibraryClassCategory.Parser
    )]
    public class HttpRequestStreamParser : IDataStreamParser
    {        
        public void FromReader(DataReader reader, DataKey root, Logger logger)
        {
            HTTPDataRequest req = new HTTPDataRequest(reader, logger);

            req.ToDataKey(root);
        }

        public void ToWriter(DataWriter writer, DataKey root, Logger logger)
        {
            HTTPDataRequest resp = HTTPDataRequest.FromDataKey(root);

            resp.ToWriter(writer, logger);
        }

        public string ToDisplayString(DataKey root, Logger logger)
        {
            DataValue method = root.SelectSingleNode("Method") as DataValue;
            DataValue path = root.SelectSingleNode("Path") as DataValue;

            return String.Format("{0} {1}", method, path);
        }
    }
}
