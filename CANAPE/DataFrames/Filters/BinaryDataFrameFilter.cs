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

using CANAPE.Utils;

namespace CANAPE.DataFrames.Filters
{
    /// <summary>
    /// Filter based on matching an array of bytes against the entire frame
    /// </summary>
    public sealed class BinaryDataFrameFilter : SetDataFrameFilter
    {
        private byte[] _match;

        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDataFrameFilter(byte[] match)
        {
            _match = match;
        }

        /// <summary>
        /// Called to make a match
        /// </summary>
        /// <param name="node">The node on which to matche this filter</param>
        /// <returns>True if the filter matched</returns>
        protected override bool OnMatch(DataNode node)
        {
            bool match = false;
            byte[] data = node.ToArray();

            if (SearchMode == DataFrameFilterSearchMode.BeginsWith)
            {
                if (_match.Length <= data.Length)
                {
                    match = GeneralUtils.MatchArray(data, 0, _match);
                }
            }
            else if (SearchMode == DataFrameFilterSearchMode.EndsWith)
            {
                if (_match.Length <= data.Length)
                {
                    match = GeneralUtils.MatchArray(data, data.Length - _match.Length, _match);
                }
            }
            else if(SearchMode == DataFrameFilterSearchMode.Contains)
            {
                for (int i = 0; i < (data.Length - _match.Length + 1) && !match; ++i)
                {
                    match = GeneralUtils.MatchArray(data, i, _match);
                }
            }
            else if(SearchMode == DataFrameFilterSearchMode.Equals)
            {
                if (data.Length == _match.Length)
                {
                    match = GeneralUtils.MatchArray(data, 0, _match);
                }
            }

            return match;
        }
    }
}
