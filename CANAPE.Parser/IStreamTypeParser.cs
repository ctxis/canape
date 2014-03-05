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

using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    /// <summary>
    /// The interface which all stream type parsers should implement
    /// </summary>
    public interface IStreamTypeParser
    {
        /// <summary>
        /// Initialize the type from a stream
        /// </summary>
        /// <param name="reader">The data reader stream to read from</param>
        /// <param name="state">A collection of state data</param>
        /// <param name="logger">The logger</param>
        void FromStream(DataReader reader, StateDictionary state, Logger logger);

        /// <summary>
        /// Convert a type to a stream
        /// </summary>
        /// <param name="writer">The data writer stream to write to</param>
        /// <param name="state">A collection of state data</param>
        /// <param name="logger">The logger</param>
        void ToStream(DataWriter writer, StateDictionary state, Logger logger);
    }
}
