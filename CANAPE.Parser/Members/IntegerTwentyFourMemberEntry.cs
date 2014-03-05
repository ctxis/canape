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

namespace CANAPE.Parser
{
    /// <summary>
    /// Member entry for a signed 24 bit integer 
    /// </summary>
    [Serializable]
    public class Int24MemberEntry : IntegerPrimitiveMemberEntry
    {               
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the entry</param>
        /// <param name="endian">The default endian</param>
        public Int24MemberEntry(string name, Endian endian)
            : base(name, typeof(int), endian)
        {
        }

        /// <summary>
        /// Get type reference
        /// </summary>
        /// <returns>The type reference</returns>
        public override System.CodeDom.CodeTypeReference GetTypeReference()
        {
            return new System.CodeDom.CodeTypeReference(typeof(Int24));
        }

        /// <summary>
        /// Get type name
        /// </summary>
        public override string TypeName
        {
            get
            {
                return "Int24";
            }
        }

        /// <summary>
        /// Return size 
        /// </summary>
        /// <returns>The size of the entry</returns>
        public override int GetSize()
        {
            return 3;
        }
    }

    /// <summary>
    /// Member entry for a unsigned 24 bit integer 
    /// </summary>
    [Serializable]
    public class UInt24MemberEntry : IntegerPrimitiveMemberEntry
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the entry</param>
        /// <param name="endian">The default endian</param>
        public UInt24MemberEntry(string name, Endian endian)
            : base(name, typeof(uint), endian)
        {
        }

        /// <summary>
        /// Get type reference
        /// </summary>
        /// <returns>The type reference</returns>
        public override System.CodeDom.CodeTypeReference GetTypeReference()
        {
            return new System.CodeDom.CodeTypeReference(typeof(UInt24));
        }

        /// <summary>
        /// Get type name
        /// </summary>
        public override string TypeName
        {
            get
            {
                return "UInt24";
            }
        }

        /// <summary>
        /// Return size 
        /// </summary>
        /// <returns>The size of the entry</returns>
        public override int GetSize()
        {
            return 3;
        }
    }
}
