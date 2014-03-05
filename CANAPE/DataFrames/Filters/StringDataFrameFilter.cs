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

namespace CANAPE.DataFrames.Filters
{
    /// <summary>
    /// A filter to match on a string value
    /// </summary>
    public class StringDataFrameFilter : SetDataFrameFilter
    {
        private string _match;        
        private bool _caseSensitive;
        private Encoding _encoding;

        /// <summary>
        /// Called to make a match
        /// </summary>
        /// <param name="node">The node on which to match this filter</param>
        /// <returns>True if the filter matched</returns>
        protected override bool OnMatch(DataNode node)
        {
            bool ret = false;
            string dataSet = null;

            try
            {
                // If this is a value object but not a byte array we can try and match without conversion
                if (node is DataValue)
                {
                    DataValue value = (DataValue)node;
                    if (value.Value is byte[])
                    {
                        dataSet = _encoding.GetString(value.Value);
                    }
                    else
                    {
                        dataSet = ((DataValue)node).Value.ToString();
                    }
                }
                else
                {
                    dataSet = node.ToEncodedString(_encoding);
                }

                if (!_caseSensitive)
                {
                    dataSet = dataSet.ToLower();
                }

                switch (SearchMode)
                {
                    case DataFrameFilterSearchMode.Contains:
                        ret = dataSet.Contains(_match);
                        break;
                    case DataFrameFilterSearchMode.BeginsWith:
                        ret = dataSet.StartsWith(_match);
                        break;
                    case DataFrameFilterSearchMode.EndsWith:
                        ret = dataSet.EndsWith(_match);
                        break;
                    case DataFrameFilterSearchMode.Equals:
                        ret = dataSet.Equals(_match);
                        break;
                }
            }
            catch (ArgumentException)
            {
                // Should we be logging this?
            }

            return ret;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="match">The string to match against</param>
        /// <param name="caseSensitive">Whether the match should be case sensitive</param>
        /// <param name="encoding">String encoding to use if the value is not a string</param>
        public StringDataFrameFilter(string match, bool caseSensitive, Encoding encoding)
        {
            if (caseSensitive)
            {
                _match = match;
            }
            else
            {
                _match = match.ToLower();
            }

            _caseSensitive = caseSensitive;
            _encoding = encoding;
        }
    }
}
