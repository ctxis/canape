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

namespace CANAPE.Extension
{
    /// <summary>
    /// Interface to specify mechanisms to convert 
    /// </summary>
    public interface IHexConverter
    {
        /// <summary>
        /// Convert a specified byte array to another
        /// </summary>
        /// <param name="startPos">The start position in the original binary data</param>
        /// <param name="ba">The byte array to convert</param>
        /// <returns>The converted byte array (could be the same as the original)</returns>
        byte[] Convert(long startPos, byte[] ba);
    }
}
