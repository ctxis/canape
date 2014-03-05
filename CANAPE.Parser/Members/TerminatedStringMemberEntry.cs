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
    public class TerminatedStringMemberEntry : StringMemberEntry, IMemberReaderWriter
    {
        private char _termChar;
        private string _termString;
        private bool _notrequired;

        public TerminatedStringMemberEntry(string name)
            : base(name)
        {            
        }

        [LocalizedDescription("TerminatedStringMemberEntry_TermCharDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]

        public ushort TermChar
        {
            get
            {
                return (ushort)_termChar;
            }

            set
            {
                if (_termChar != value)
                {
                    _termChar = (char)value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("TerminatedStringMemberEntry_TermStringDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public string TermString
        {
            get
            {               
                return _termString;
            }

            set
            {
                if(_termString != value)
                {                   
                    // Check the string can decode
                    GeneralUtils.DecodeEscapedString(value);
                    _termString = value;
                 
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("TerminatedStringMemberEntry_RequiredDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public bool Required
        {
            get
            {
                return !_notrequired;
            }

            set
            {
                if (_notrequired == value)
                {
                    _notrequired = !value;
                    OnDirty();
                }
            }
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
            CodeExpression returnExpression = GetReaderExpression(CodeGen.GetReader());

            method.Statements.Add(new CodeMethodReturnStatement(returnExpression));

            return method;
        }

        public override string TypeName
        {
            get
            {
                return String.Format("{0} String ({1} terminated)", StringEncoding.ToString(), String.IsNullOrWhiteSpace(_termString) ? ((ushort)_termChar).ToString() : _termString);
            }
        }

        public CodeExpression GetReaderExpression(CodeExpression reader)
        {
            CodeExpression ret;

            if (!String.IsNullOrEmpty(_termString))
            {
                ret = CodeGen.CallMethod(CodeGen.GetThis(), "RTS",
                    reader, CodeGen.GetEnum(StringEncoding), CodeGen.GetPrimitive(GeneralUtils.DecodeEscapedString(_termString)), CodeGen.GetPrimitive(!_notrequired));
            }
            else
            {
                ret = CodeGen.CallMethod(CodeGen.GetThis(), "RTS",
                    reader, CodeGen.GetEnum(StringEncoding), CodeGen.GetPrimitive(_termChar), CodeGen.GetPrimitive(!_notrequired));
            }

            return ret;
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            CodeExpression ret;

            if (!String.IsNullOrEmpty(_termString))
            {
                ret = CodeGen.CallMethod(CodeGen.GetThis(), "WTS",
                   writer, CodeGen.GetEnum(StringEncoding), obj, CodeGen.GetPrimitive(GeneralUtils.DecodeEscapedString(_termString)));
            }
            else
            {
                ret = CodeGen.CallMethod(CodeGen.GetThis(), "WTS",
                    writer, CodeGen.GetEnum(StringEncoding), obj, CodeGen.GetPrimitive(_termChar)
                 );
            }

            return ret;
        }
    }
}
