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
using CANAPE.Utils;

namespace CANAPE.Parser
{
    [Serializable]
    public class StreamParserType : ParserType
    {
        private ParserType _baseType;
        private string _formatString;

        public StreamParserType(string name, ParserType baseType)
            : base(name)
        {
            _baseType = baseType;
        }

        [LocalizedDescription("StreamParserType_BaseType", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public ParserType BaseType
        {
            get { return _baseType; }            
        }

        public override int GetSize()
        {
            return _baseType.GetSize();
        }


        [LocalizedDescription("StreamParserType_FormatStringDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public string FormatString 
        {
            get
            {
                return _formatString;
            }

            set
            {
                if (_formatString != value)
                {
                    _formatString = value;
                    OnDirty();
                }
            }
        }

        public override CodeTypeDeclaration GetCodeType()
        {
            CodeTypeDeclaration type = new CodeTypeDeclaration(Name);

            type.IsClass = true;
            type.BaseTypes.Add(new CodeTypeReference(typeof(GenericStreamParser<>).FullName, new CodeTypeReference(_baseType.Name)));
            type.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            if (!String.IsNullOrWhiteSpace(_formatString))
            {
                CodeConstructor constructor = new CodeConstructor();
                constructor.Attributes = MemberAttributes.Public;
                constructor.BaseConstructorArgs.Add(CodeGen.GetPrimitive(_formatString));

                type.Members.Add(constructor);
            }            

            return type;
        }
    }
}
