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
    /// Filter to determine if a number of nodes have been matched
    /// </summary>
    public sealed class NodeCountDataFrameFilter : BaseDataFrameFilter
    {
        int _count;
        ComparisonOperator _comparer;

        /// <summary>
        /// If we matched something then we must have succeeded
        /// </summary>
        /// <param name="nodes">An array of nodes to test</param>
        /// <returns>Always true</returns>
        protected override bool OnMatch(DataNode[] nodes)
        {
            bool ret = false;

            switch (_comparer)
            {
                case ComparisonOperator.Equal:
                    ret = nodes.Length == _count;
                    break;
                case ComparisonOperator.GreaterThan:
                    ret = nodes.Length > _count;
                    break;
                case ComparisonOperator.GreaterThanOrEqual:
                    ret = nodes.Length >= _count;
                    break;
                case ComparisonOperator.LessThan:
                    ret = nodes.Length < _count;
                    break;
                case ComparisonOperator.LessThanOrEqual:
                    ret = nodes.Length <= _count;
                    break;
                case ComparisonOperator.NotEqual:
                    ret = nodes.Length != _count;
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="count">The count to match against</param>
        /// <param name="comparer">The compare operation for the count</param>
        public NodeCountDataFrameFilter(int count, ComparisonOperator comparer)
        {
            _count = count;
            _comparer = comparer;
        }
    }
}
