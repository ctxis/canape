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
    /// A member entry which holds a primitive type
    /// </summary>
    [Serializable]
    public abstract class PrimitiveMemberEntry : MemberEntry, IMemberReaderWriter
    {        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of entry</param>
        /// <param name="type">Primitive type of entry</param>
        protected PrimitiveMemberEntry(string name, Type type) 
            : base(name, new BuiltinParserType(type))
        {
            if (!type.IsPrimitive && !typeof(IPrimitiveValue).IsAssignableFrom(type))
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.IntegerMemberEntry_InvalidType);
            } 
        }

        /// <summary>
        /// Get type reference
        /// </summary>
        /// <returns>The type reference associated with this entry</returns>
        public override CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(GetDataType());
        }

        public abstract CodeExpression GetReaderExpression(CodeExpression reader);

        public abstract CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj);

        /// <summary>
        /// Get the underlying framework type
        /// </summary>
        /// <returns>The framework type</returns>
        public Type GetDataType()
        {
            return ((BuiltinParserType)BaseType).DataType;
        }

        public override System.CodeDom.CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            CodeExpression expression = GetWriterExpression(CodeGen.GetWriter(), CodeGen.GetObject());

            if (expression != null)
            {
                method.Statements.Add(expression);
            }

            return method;
        }

        public override CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();

            method.Statements.Add(CodeGen.GetReturn(GetReaderExpression(CodeGen.GetReader())));

            return method;
        }
    }
}
