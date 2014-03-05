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

using CANAPE.Nodes;
using System;
using CANAPE.Utils;

namespace CANAPE.DataFrames.Filters
{
    /// <summary>
    /// Base interface for a data frame filter
    /// </summary>
    public interface IDataFrameFilter
    {
        /// <summary>
        /// Called with a frame to determine if this filter matches
        /// </summary>
        /// <param name="frame">The frame to check</param>
        /// <returns>True if the filter matches</returns>
        bool IsMatch(DataFrame frame);

        /// <summary>
        /// Extended match syntax, can use details from a graph and node
        /// </summary>
        /// <param name="frame">The data frame to select off</param>
        /// <param name="globalMeta">The global meta</param>
        /// <param name="meta">The meta</param>
        /// <param name="node">The current node</param>
        /// <param name="properties">The property bag</param>
        /// <param name="uuid">The uuid for private names</param>
        /// <returns>True if the filter matches</returns>
        bool IsMatch(DataFrame frame, MetaDictionary meta, MetaDictionary globalMeta, 
            PropertyBag properties, Guid uuid, BasePipelineNode node);
      
        /// <summary>
        /// Invert the match
        /// </summary>
        bool Invert { get; set; }
    }
}
