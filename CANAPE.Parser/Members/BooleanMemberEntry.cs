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
    [Serializable]
    public class BooleanMemberEntry : ContainerMemberEntry, IMemberReaderWriter
    {                
        private long _trueValue;
        private long _falseValue;
        private IntegerPrimitiveMemberEntry _primitiveEntry;

        public BooleanMemberEntry(IntegerPrimitiveMemberEntry entry)
             : base(entry)
        {
            _trueValue = 1;
            _falseValue = 0;
            _primitiveEntry = entry;
        }

        [LocalizedDescription("BooleanMemberEntry_TrueValueDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public long TrueValue
        {
            get { return _trueValue;  }
            set
            {
                if (_trueValue != value)
                {
                    if (!ParserUtils.CheckInRange(value, _primitiveEntry.GetDataType()))
                    {
                        throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.BooleanMemberEntry_InvalidValue,
                               value, _primitiveEntry.GetDataType().Name));
                    }

                    _trueValue = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("BooleanMemberEntry_FalseValueDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public long FalseValue
        {
            get { return _falseValue; }
            set
            {
                if (_falseValue != value)
                {
                    if (!ParserUtils.CheckInRange(value, _primitiveEntry.GetDataType()))
                    {
                        throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.BooleanMemberEntry_InvalidValue,
                               value, _primitiveEntry.GetDataType().Name));
                    }
                    _falseValue = value;
                    OnDirty();
                }
            }
        }

        public override CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(typeof(bool));
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

        public override string TypeName
        {
            get
            {
                return String.Format("System.Bool ({0})", _primitiveEntry.TypeName);
            }
        }

        public override int GetSize()
        {
            return _primitiveEntry.GetSize();
        }

        public CodeExpression GetReaderExpression(CodeExpression reader)
        {
            return CodeGen.GetOperator(CodeGen.GetPrimitive(_falseValue),
                CodeBinaryOperatorType.IdentityInequality, CodeGen.GetCast(_primitiveEntry.GetDataType(), _primitiveEntry.GetReaderExpression(reader)));
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            return _primitiveEntry.GetWriterExpression(writer, CodeGen.GetCast(_primitiveEntry.GetCastType(), CodeGen.CallMethod(typeof(ParserUtils), "GetBooleanValue",
                _primitiveEntry.GetDataType(), obj, CodeGen.GetPrimitive(_trueValue), CodeGen.GetPrimitive(_falseValue))));
        }
    }
}
