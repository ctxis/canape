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
using System.Linq;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Parser
{
    [NodeLibraryClass("HTTPCookieParser", typeof(Properties.Resources),
        Category = NodeLibraryClassCategory.Parser)
    ]
    public class HTTPCookieParser : IDataStringParser
    {
        private void AddCookie(string cookie, DataKey root, Logger logger)
        {
            string[] kv = cookie.Split(new [] {'='}, 2, StringSplitOptions.RemoveEmptyEntries);

            string k = kv[0].Trim();
            string v = "";

            if(kv.Length > 1)
            {
                v = kv[1].Trim();
            }
                
            if(!String.IsNullOrEmpty(k))
            {
                root.AddValue(k, v);
            }
        }

        #region IDataStringParser Members

        public void FromString(string data, DataFrames.DataKey root, Utils.Logger logger)
        {
            string[] cookies = data.Split(';');

            foreach (string cookie in cookies)
            {
                AddCookie(cookie, root, logger);
            }
        }

        public string ToString(DataFrames.DataKey root, Utils.Logger logger)
        {
            return String.Join("; ", root.SubNodes.Select(n => String.Format("{0}={1}", n.Name, n.ToDataString())));
        }

        #endregion

        #region IDataParser Members

        public string ToDisplayString(DataFrames.DataKey root, Utils.Logger logger)
        {
            return String.Format("Cookies: {0}", String.Join(", ", root.SubNodes.Select(n => n.Name)));
        }

        #endregion
    }
}
