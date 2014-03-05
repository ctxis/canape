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

namespace CANAPE.Scripting
{
    /// <summary>
    /// Interface to parse data as a string
    /// </summary>
    public interface IDataStringParser : IDataParser
    {
        /// <summary>
        /// Convert from a string
        /// </summary>
        /// <param name="data">The data string</param>
        /// <param name="root">The root key to populate</param>
        /// <param name="logger">The logger</param>
        void FromString(string data, DataKey root, Logger logger);
        
        /// <summary>
        /// Get the datakey as a string
        /// </summary>
        /// <param name="root">The root key to use</param>
        /// <param name="logger">The logger</param>
        /// <returns>The string form of the key</returns>
        string ToString(DataKey root, Logger logger);
    }
}
