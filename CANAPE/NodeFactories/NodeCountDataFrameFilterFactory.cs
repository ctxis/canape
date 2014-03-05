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
using System.ComponentModel;
using CANAPE.DataFrames.Filters;
using CANAPE.Utils;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class NodeCountDataFrameFilterFactory : DataFrameFilterFactory
    {
        /// <summary>
        /// The number of nodes to match against
        /// </summary>
        [LocalizedDescription("NodeCountDataFrameFilterFactory_CountDescription", typeof(Properties.Resources))]
        public int Count { get; set; }

        /// <summary>
        /// The operation to compare the count against
        /// </summary>
        [LocalizedDescription("NodeCountDataFrameFilterFactory_OperationDescription", typeof(Properties.Resources))]
        public ComparisonOperator Operation { get; set; }

        /// <summary>
        /// Method to create the filter
        /// </summary>
        /// <returns></returns>
        protected override BaseDataFrameFilter OnCreateFilter()
        {
            return new NodeCountDataFrameFilter(Count, Operation);
        }

        /// <summary>
        /// Return a display string for the filter
        /// </summary>
        /// <returns></returns>
        protected override string ToDisplayString()
        {
            return String.Format(CANAPE.Properties.Resources.NodeCountDataFrameFilterFactory_DisplayString, Operation, Count);
        }
    }
}
