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
using System.Runtime.InteropServices;
using CANAPE.DataFrames;

namespace CANAPE.Parser
{
    /// <summary>
    /// A parser type which just wraps an existing type
    /// </summary>
    [Serializable]
    public class BuiltinParserType : ParserType
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The built in type</param>
        public BuiltinParserType(Type type)
            : base(type.Name, type.GUID)
        {
            DataType = type;
        }

        /// <summary>
        /// The built-in type
        /// </summary>
        public Type DataType { get; private set; }

        /// <summary>
        /// Get the implementation of the type
        /// </summary>
        /// <returns>Always returns null</returns>
        public override System.CodeDom.CodeTypeDeclaration GetCodeType()
        {
            return null;
        }

        /// <summary>
        /// Get size of type
        /// </summary>
        /// <returns>The size of the type</returns>
        public override int GetSize()
        {
            return ParserUtils.GetPrimitiveSize(DataType);
        }
    }
}
