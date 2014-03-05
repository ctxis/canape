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
    /// Base interface for all parsers
    /// </summary>
    public interface IDataParser
    {
        /// <summary>
        /// Convert the datakey to a text string
        /// </summary>
        /// <param name="root"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        string ToDisplayString(DataKey root, Logger logger);
    }
}
