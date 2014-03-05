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
    /// Factory base class for data set matching (adds positional information)
    /// </summary>
    [Serializable]
    public abstract class SetDataFrameFilterFactory : DataFrameFilterFactory
    {
        /// <summary>
        /// Indicates how the match should be made
        /// </summary>
        [LocalizedDescription("SetDataFrameFilterFactory_SearchModeDescription", typeof(Properties.Resources))]
        public DataFrameFilterSearchMode SearchMode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected SetDataFrameFilterFactory()
        {
            SearchMode = DataFrameFilterSearchMode.Contains;
        }

        /// <summary>
        /// Overridable function to create a set filter
        /// </summary>
        /// <returns></returns>
        protected abstract SetDataFrameFilter OnCreateSetFilter();

        /// <summary>
        /// Method to create a filter
        /// </summary>
        /// <returns></returns>
        protected override BaseDataFrameFilter OnCreateFilter()
        {
            SetDataFrameFilter filter = OnCreateSetFilter();

            filter.SearchMode = SearchMode;

            return filter;
        }

        /// <summary>
        /// Method to return search mode operator
        /// </summary>
        /// <returns></returns>
        protected override string ToDisplayString()
        {
            return SearchMode.ToString();
        }
    }
}
