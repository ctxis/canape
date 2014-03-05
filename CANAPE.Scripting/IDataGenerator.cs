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
using CANAPE.DataFrames;
using CANAPE.Nodes;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Interface for a data generator
    /// </summary>
    public interface IDataGenerator
    {
        /// <summary>
        /// Method to generate data
        /// </summary>
        /// <param name="inputFrames">Provides input data to the generator</param>
        /// <returns>Can return one of, DataFrame, string, byte array, DataKey or dictionary</returns>
        IEnumerable<object> Generate(IEnumerable<DataFrame> inputFrames);

        /// <summary>
        /// Get a descriptive name for the endpoint
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Meta
        /// </summary>
        MetaDictionary Meta { get; set; }

        /// <summary>
        /// Global meta
        /// </summary>
        MetaDictionary GlobalMeta { get; set; }
    }
}
