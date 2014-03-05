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

using System.Collections.Generic;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    /// <summary>
    /// The interface which all array type parsers implement
    /// </summary>
    public interface IArrayTypeParser
    {
        /// <summary>
        /// Convert from an array
        /// </summary>
        /// <param name="reader">The reader which maintains current data</param>
        /// <param name="dataSet">The entire data set from which this type is parsed</param>
        /// <param name="state">A collection of state data</param>
        /// <param name="logger">Logger</param>
        void FromArray(DataReader reader, byte[] dataSet, Dictionary<string, dynamic> state, Logger logger);
        
        /// <summary>
        /// Convert to an array
        /// </summary>
        /// <param name="writer">The writer which maintains current data</param>
        /// <param name="dataSet">The current data set which this type is being created</param>
        /// <param name="state">A collection of state data</param>
        /// <param name="logger">Logger</param>
        void ToArray(DataWriter writer, byte[] dataSet, Dictionary<string, dynamic> state, Logger logger);
    }
}
