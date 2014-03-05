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
using System.Linq;
using System.Text;
using CANAPE.NodeFactories;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// An interface which indicates that this node has a link to another
    /// </summary>
    public interface ILinkedNodeConfig
    {
        /// <summary>
        /// Get the linked node
        /// </summary>
        BaseNodeConfig LinkedNode { get; }

        /// <summary>
        /// Create factory with a linked node
        /// </summary>
        /// <param name="linkedNode">The linked node</param>
        /// <returns>The created factory</returns>
        BaseNodeFactory CreateFactory(BaseNodeFactory linkedNode);
    }
}
