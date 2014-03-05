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
using System.Runtime.InteropServices;
using System;

namespace CANAPE.Parser
{
    [Serializable]
    public class DataStreamParserType : ParserType
    {
        public SequenceParserType BaseType { get; private set; }

        public DataStreamParserType(string name, SequenceParserType baseType) 
            : base(name)
        {
            BaseType = baseType;
        }

        public override System.CodeDom.CodeTypeDeclaration GetCodeType()
        {
            CodeTypeDeclaration type = new CodeTypeDeclaration(Name);
            
            CodeTypeReference baseParser = new CodeTypeReference("CANAPE.Parser.GenericStreamParser", 
                new CodeTypeReference[] { new CodeTypeReference(BaseType.Name) });
            
            type.IsClass = true;
            type.BaseTypes.Add(baseParser);
            type.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            type.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(GuidAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression(Uuid.ToString()))));

            return type;
        }

        public override int GetSize()
        {
            return BaseType.GetSize();
        }
    }
}
