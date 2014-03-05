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
using System.Collections.Generic;
using CANAPE.Nodes;
using CANAPE.Properties;
using CANAPE.Utils;

namespace CANAPE.DataFrames.Filters
{
    /// <summary>
    /// A list of data frame filters, currently a simple list, but could be more expressive
    /// </summary>    
    public class DataFrameFilterExpression : IDataFrameFilter
    {
        private bool _matchAll;
        private List<IDataFrameFilter> _filters;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="matchAll"></param>
        public DataFrameFilterExpression(IEnumerable<IDataFrameFilter> filters, bool matchAll)             
        {
            if (filters != null)
            {
                _filters = new List<IDataFrameFilter>(filters);
            }
            else
            {
                _filters = new List<IDataFrameFilter>();
            }

            _matchAll = matchAll;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filters"></param>
        public DataFrameFilterExpression(IEnumerable<IDataFrameFilter> filters) 
            : this(filters, false)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="matchAll">Whether all the filters need to match</param>
        public DataFrameFilterExpression(bool matchAll) : this(null, matchAll)
        {                    
        }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public DataFrameFilterExpression() : this(false)
        {
        }

        /// <summary>
        /// Add a filter
        /// </summary>
        /// <param name="filter"></param>
        public void Add(IDataFrameFilter filter)
        {
            _filters.Add(filter);
        }

        /// <summary>
        /// Remove a filter
        /// </summary>
        /// <param name="filter"></param>
        public void Remove(IDataFrameFilter filter)
        {
            _filters.Remove(filter);
        }

         /// <summary>
        /// Extended match syntax, can use details from a graph and node
        /// </summary>
        /// <param name="frame">The data frame to select off</param>
        /// <param name="globalMeta">The global meta</param>
        /// <param name="meta">The meta</param>
        /// <param name="node">The current node</param>
        /// <param name="properties">The property bag</param>
        /// <param name="uuid">The uuid for private meta names</param>
        /// <returns>True if the filter matches</returns>
        public bool IsMatch(DataFrame frame, MetaDictionary meta, MetaDictionary globalMeta, 
            PropertyBag properties, Guid uuid, BasePipelineNode node)
        {
            bool ret = true;

            foreach (IDataFrameFilter filter in _filters)
            {                
                ret = filter.IsMatch(frame, meta, globalMeta, properties, uuid, node);
                
                if (_matchAll)
                {
                    if (!ret)
                    {
                        break;
                    }
                }
                else
                {
                    if (ret)
                    {
                        break;
                    }
                }
            }

            if (Invert)
            {
                return !ret;
            }
            else
            {
                return ret;
            }
        }

        /// <summary>
        /// Method to match the filter expression
        /// </summary>
        /// <param name="frame">The frame to match on</param>
        /// <returns>True if the filters match</returns>
        public bool IsMatch(DataFrame frame)
        {
            return IsMatch(frame, null, null, null, Guid.Empty, null);
        }

        /// <summary>
        /// Whether this is a exclude filter
        /// </summary>
        public bool Exclude { get; set; }

        /// <summary>
        /// Invert the match
        /// </summary>
        public bool Invert { get; set; }

        /// <summary>
        /// Overriden to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(Resources.DataFrameFilterExpression_ToString, _filters.Count);
        }
    }
}
