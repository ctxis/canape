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
using CANAPE.Nodes;
using CANAPE.Scripting;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Class choice converter for only node types
    /// </summary>
    public sealed class NodeClassChoiceConverter : BaseClassChoiceConverter
    {
        /// <summary>
        /// Static method to get list of types
        /// </summary>
        /// <returns>The list of types</returns>
        public static Type[] GetTypes()
        {
            return new Type[] { 
                    typeof(BasePipelineNode),
                    typeof(IDataStreamParser),
                    typeof(IDataArrayParser),      
            };
        }

        /// <summary>
        /// Get choice types for node
        /// </summary>
        /// <returns>The choice types</returns>
        protected override Type[] GetChoiceTypes()
        {
            return GetTypes();
        }
    }
}
