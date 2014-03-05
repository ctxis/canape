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
using System.Text;
using System.Text.RegularExpressions;

namespace CANAPE.DataFrames.Filters
{
    /// <summary>
    /// A filter to match on a regular expression
    /// </summary>
    public class RegexDataFrameFilter : SetDataFrameFilter
    {
        // Note: The Search parameter doesn't make a difference from
        // the actual match point of view on an RE filter as that is part
        // of the filter string itself

        private Encoding _encoding;
        private Regex _re;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="re">The regular expression to match</param>
        /// <param name="encoding">The text encoding to use</param>
        public RegexDataFrameFilter(Regex re, Encoding encoding)
        {
            _re = re;
            _encoding = encoding;
        }

        /// <summary>
        /// Called to make a match
        /// </summary>
        /// <param name="node">The node on which to match this filter</param>
        /// <returns>True if the filter matched</returns>
        protected override bool OnMatch(DataNode node)
        {
            bool ret = false;

            // If this is a value object but not a byte array we can try and match without conversion
            if ((node is DataValue) && !(node is ByteArrayDataValue))
            {
                string value = ((DataValue)node).Value.ToString();

                ret = _re.IsMatch(value);
            }
            else
            {
                byte[] data = node.ToArray();

                try
                {
                    string str = _encoding.GetString(data);

                    ret = _re.IsMatch(str);
                }
                catch (ArgumentException)
                {
                }
            }

            return ret;

        }
    }
}
