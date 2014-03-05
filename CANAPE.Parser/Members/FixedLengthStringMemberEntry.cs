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
using NCalc;

namespace CANAPE.Parser
{
    [Serializable, LocalizedDescription("FixedLengthStringMemberEntry_Description", typeof(Properties.Resources))]
    public sealed class FixedLengthStringMemberEntry : StringMemberEntry, IMemberReaderWriter
    {
        private int _length;
        private char _padding;
        private bool _isByteLength;
        private string _lengthExpression;

        public FixedLengthStringMemberEntry(string name)
            : base(name)
        {
            _length = 0;
            _lengthExpression = "0";
        }

        protected override void OnDeserialized()
        {
            if (!String.IsNullOrWhiteSpace(_lengthExpression))
            {
                _lengthExpression = _length.ToString();
            }
        }

        [LocalizedDescription("Generic_LengthDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression Length
        {
            get
            {
                return new EvalExpression(_lengthExpression ?? "0");
            }

            set
            {
                if (value.Expression != _lengthExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _lengthExpression = value.Expression;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Indicates if the length should be treated as a byte length or element
        /// </summary>
        [LocalizedDescription("FixedLengthStringMemberEntry_IsByteLengthDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public bool IsByteLength
        {
            get { return _isByteLength; }
            set
            {
                if (_isByteLength != value)
                {
                    _isByteLength = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("FixedLengthStringMemberEntry_PaddingDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public ushort Padding 
        {
            get { return (ushort)_padding; }
            set
            {
                if (_padding != value)
                {
                    _padding = (char)value;
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

        public override int GetSize()
        {
            int length;

            if (Length.ParseInt(out length))
            {
                return length;
            }
            else
            {
                return -1;
            }
        }

        public override string TypeName
        {
            get
            {
                return String.Format("{0} String({1})", StringEncoding.ToString(), _lengthExpression);
            }
        }

        public CodeExpression GetReaderExpression(CodeExpression reader)
        {
            CodeExpression ret;
            
            ret = CodeGen.CallMethod(typeof(ParserUtils), _isByteLength ? "ReadFixedByteLengthString" : "ReadFixedLengthString",
                reader,
                GetEncoding(),
                CodeGen.GetLength(Length));

            return ret;
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            CodeExpression ret;

            if (_isByteLength)
            {
                ret = CodeGen.CallMethod(typeof(ParserUtils), "WriteFixedByteLengthString",
                    writer,
                    GetEncoding(), obj, CodeGen.GetLength(Length),
                    CodeGen.GetPrimitive((byte)Padding));
            }
            else
            {
                ret = CodeGen.CallMethod(typeof(ParserUtils), "WriteFixedLengthString",
                    writer,
                    GetEncoding(), obj, CodeGen.GetLength(Length),
                    CodeGen.GetPrimitive((char)Padding));                        
            }

            return ret;
        }
    }
}
