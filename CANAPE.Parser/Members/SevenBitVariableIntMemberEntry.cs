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
using System.CodeDom;
using CANAPE.DataFrames;

namespace CANAPE.Parser
{
    /// <summary>
    /// Member entry for a seven bit variable integer
    /// </summary>
    [Serializable]
    public class SevenBitVariableIntMemberEntry : IntegerPrimitiveMemberEntry
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the entry</param>
        public SevenBitVariableIntMemberEntry(string name)
            : base(name, typeof(long), Endian.LittleEndian)
        {
        }

        /// <summary>
        /// Get type reference
        /// </summary>
        /// <returns>The type reference</returns>
        public override System.CodeDom.CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(typeof(Int7V));
        }

        /// <summary>
        /// Get type name
        /// </summary>
        public override string TypeName
        {
            get
            {
                return "Int7V";
            }
        }

        /// <summary>
        /// Return size 
        /// </summary>
        /// <returns>The size of the entry</returns>
        public override int GetSize()
        {
            return -1;
        }
    }
}
