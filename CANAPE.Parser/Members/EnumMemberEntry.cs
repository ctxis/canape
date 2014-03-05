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
using System.ComponentModel;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    /// <summary>
    /// Member entry to represent an enumeration
    /// </summary>
    [Serializable]
    public class EnumMemberEntry : ContainerMemberEntry, IMemberReaderWriter
    {
        private IntegerPrimitiveMemberEntry _primitiveEntry;

        public EnumMemberEntry(EnumParserType type, IntegerPrimitiveMemberEntry entry) : base(entry)
        {
            EnumType = type;
            _primitiveEntry = entry;
        }

        /// <summary>
        /// The type of the enum
        /// </summary>
        [LocalizedDescription("EnumMemberEntry_EnumTypeDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Information")]
        public EnumParserType EnumType
        {
            get;
            private set;
        }

        public override CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(EnumType.Name);   
        }

        public override CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            method.Statements.Add(GetWriterExpression(CodeGen.GetWriter(), CodeGen.GetObject()));

            return method;
        }

        public override CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();

            method.Statements.Add(
                CodeGen.GetReturn(GetReaderExpression(CodeGen.GetReader()))
            );

            return method;
        }

        public override string TypeName
        {
            get
            {
                return String.Format("{0} ({1})", EnumType.Name, _primitiveEntry.TypeName);
            }
        }

        public override int GetSize()
        {
            return _primitiveEntry.GetSize();
        }

        public CodeExpression GetReaderExpression(CodeExpression reader)
        {
            return CodeGen.GetCast(EnumType.Name, CodeGen.GetCast(typeof(long), _primitiveEntry.GetReaderExpression(reader)));
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            return _primitiveEntry.GetWriterExpression(writer, 
                CodeGen.GetCast(_primitiveEntry.GetCastType(), CodeGen.GetCast(typeof(long), obj))
                );
        }
    }
}
