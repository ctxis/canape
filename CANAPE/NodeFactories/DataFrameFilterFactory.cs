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
using CANAPE.DataFrames;
using CANAPE.DataFrames.Filters;
using System.Collections.Generic;
using CANAPE.Utils;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// Factory class to create a dataframe filter
    /// </summary>
    [Serializable]
    public abstract class DataFrameFilterFactory : IDataFrameFilterFactory
    {
        /// <summary>
        /// The dataframe path to filter on, / means the whole root
        /// </summary>
        [LocalizedDescription("DataFrameFilterFactory_PathDescription", typeof(Properties.Resources))]
        public string Path { get; set; }

        /// <summary>
        /// Indicates whether the match should be inverted
        /// </summary>
        [LocalizedDescription("DataFrameFilterFactory_InvertDescription", typeof(Properties.Resources))]
        public bool Invert { get; set; }

        /// <summary>
        /// Indicates if the filter is enabled
        /// </summary>
        [LocalizedDescription("DataFrameFilterFactory_EnabledDescription", typeof(Properties.Resources))]
        public bool Enabled { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected DataFrameFilterFactory()
        {            
            Path = "/";
            Enabled = true;
            Invert = false;
        }

        private class DummyDataFrameFilter : BaseDataFrameFilter
        {
            protected override bool OnMatch(DataNode[] nodes)
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDataFrameFilter CreateFilter()
        {
            if (Enabled)
            {
                BaseDataFrameFilter filter = OnCreateFilter();
                
                filter.Path = Path;
                filter.Invert = Invert;
             
                return filter;
            }
            else
            {
                // Return a dummy filter which always matches
                return new DummyDataFrameFilter();
            }
        }

        /// <summary>
        /// Overridable method to call when creating a filter
        /// </summary>
        /// <returns>The created filter</returns>
        protected abstract BaseDataFrameFilter OnCreateFilter();

        /// <summary>
        /// Overridable method called when showing a display string
        /// </summary>
        /// <returns></returns>
        protected abstract string ToDisplayString();

        /// <summary>
        /// Convert object to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Invert)
            {
                return String.Format("'{0}' Not {1}", Path, ToDisplayString());
            }
            else
            {
                return String.Format("'{0}' {1}", Path, ToDisplayString());
            }
        }

        /// <summary>
        /// Get a filter for a particular DataValue
        /// </summary>
        /// <param name="value">The data value to create the filter</param>
        /// <returns>The filter factory</returns>
        public static DataFrameFilterFactory CreateFilter(DataValue value)
        {
            SetDataFrameFilterFactory ret;

            if (value.Value is byte[])
            {
                ret = new BinaryDataFrameFilterFactory() { Match = value.Value as byte[] };
            }
            else
            {
                ret = new StringDataFrameFilterFactory() { Match = value.Value.ToString() };
            }

            ret.Path = value.Path;
            ret.SearchMode = DataFrameFilterSearchMode.Equals;

            return ret;
        }

        /// <summary>
        /// Create a list of filters for a key
        /// </summary>
        /// <param name="root">The root key</param>
        /// <returns>The list of filters</returns>
        public static IEnumerable<DataFrameFilterFactory> CreateFilter(DataKey root)
        {
            List<DataFrameFilterFactory> filters = new List<DataFrameFilterFactory>();

            foreach (DataNode node in root.SubNodes)
            {
                DataValue value = node as DataValue;

                if (value != null)
                {
                    filters.Add(CreateFilter(value));
                }
                else
                {
                    DataKey key = node as DataKey;
                    if (key != null)
                    {
                        filters.AddRange(CreateFilter(key));
                    }
                }
            }

            return filters;
        }

        /// <summary>
        /// Create a list of filters for a data frame
        /// </summary>
        /// <param name="frame">The data frame</param>
        /// <returns>The list of filters</returns>
        public static IEnumerable<DataFrameFilterFactory> CreateFilter(DataFrame frame)
        {
            if (frame.IsBasic)
            {
                BinaryDataFrameFilterFactory filter = new BinaryDataFrameFilterFactory();

                filter.Match = frame.ToArray();
                filter.Path = "/";
                filter.SearchMode = DataFrameFilterSearchMode.Equals;

                return new DataFrameFilterFactory[] { filter };
            }
            else
            {
                return CreateFilter(frame.Root);
            }
        }

        [Serializable]
        private class DummyDataFrameFilterFactory : DataFrameFilterFactory
        {
            protected override BaseDataFrameFilter OnCreateFilter()
            {
                return new DummyDataFrameFilter();
            }

            protected override string ToDisplayString()
            {
                return "Dummy";
            }
        }

        /// <summary>
        /// Create a dummy factory which creates filters which always match
        /// </summary>
        /// <returns>The dummy factory</returns>
        public static DataFrameFilterFactory CreateDummyFactory()
        {
            return new DummyDataFrameFilterFactory();
        }
    }
}
