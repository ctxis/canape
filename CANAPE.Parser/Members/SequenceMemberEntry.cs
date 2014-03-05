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

using System.CodeDom;
using System;

namespace CANAPE.Parser
{
    /// <summary>
    /// Entry for a structure which represents another
    /// </summary>
    [Serializable]
    public class SequenceMemberEntry : MemberEntry, IMemberReaderWriter
    {        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the entry</param>
        /// <param name="baseType">The base type</param>
        public SequenceMemberEntry(string name, SequenceParserType baseType)
            : base(name, baseType)
        {            
        }

        public override CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(BaseType.Name);
        }

        public virtual CodeExpression GetReaderExpression(CodeExpression reader)
        {
            return CodeGen.CreateObject(new CodeTypeReference(BaseType.Name), reader, CodeGen.CallMethod(CodeGen.GetThis(), "CS"), CodeGen.GetLogger());
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            return CodeGen.CallMethod(obj, "ToStream",
                writer, CodeGen.CallMethod(CodeGen.GetThis(), "CS"), CodeGen.GetLogger());
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

            method.Statements.Add(CodeGen.GetReturn(GetReaderExpression(CodeGen.GetReader())));
            
            return method;
        }
    }
}
