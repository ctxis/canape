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


namespace CANAPE.DataFrames.Filters
{
    /// <summary>
    /// Class to base filters which work on sets of data with searching
    /// </summary>
    public abstract class SetDataFrameFilter : BaseDataFrameFilter
    {
        /// <summary>
        /// Indicates how this filter should match
        /// </summary>
        public DataFrameFilterSearchMode SearchMode { get; set; }

        /// <summary>
        /// Indicates that the filter should match on all nodes
        /// </summary>
        public bool MatchAllNodes { get; set; }

        /// <summary>
        /// Called for each data node which matches the path
        /// </summary>
        /// <param name="node">The node</param>
        /// <returns>True if a match is made, otherwise false</returns>
        protected abstract bool OnMatch(DataNode node);

        /// <summary>
        /// Overridden method to match on a set of data nodes
        /// </summary>
        /// <param name="nodes">The nodes to match against</param>
        /// <returns>True if the nodes match</returns>
        protected override bool OnMatch(DataNode[] nodes)
        {
            bool ret = false;

            foreach (DataNode node in nodes)
            {
                ret = OnMatch(node);

                if (MatchAllNodes)
                {
                    // Exit on first mismatch
                    if (!ret)
                    {
                        break;
                    }
                }
                else
                {
                    // Exit on first match
                    if (ret)
                    {
                        break;
                    }
                }
            }
        
            return ret;
        }
    }
}
