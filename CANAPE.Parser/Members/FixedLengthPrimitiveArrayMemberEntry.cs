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
using NCalc;

namespace CANAPE.Parser
{
    /// <summary>
    /// Member entry to represent a fixed length array of primitive values
    /// </summary>
    [Serializable]
    public sealed class FixedLengthPrimitiveArrayMemberEntry : ArrayMemberEntry, IMemberReaderWriter
    {
        private int _length;
        private bool _isByteLength;
        private IntegerPrimitiveMemberEntry _intEntry;
        private string _lengthExpression;

        protected override void OnDeserialized()
        {
            if (!String.IsNullOrWhiteSpace(_lengthExpression))
            {
                _lengthExpression = _length.ToString();
            }
        }       

        /// <summary>
        /// Indicates if the length should be treated as a byte length or element
        /// </summary>
        [LocalizedDescription("FixedLengthPrimitiveArrayMemberEntry_IsByteLengthDescription", typeof(Properties.Resources)), 
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
        /// 
        /// </summary>
        /// <param name="baseEntry"></param>
        public FixedLengthPrimitiveArrayMemberEntry(IntegerPrimitiveMemberEntry baseEntry)
            : base(baseEntry)
        {
            _intEntry = baseEntry;
            _length = 0;
        }

        public override System.CodeDom.CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            method.Statements.Add(GetWriterExpression(CodeGen.GetWriter(), CodeGen.GetObject()));

            return method;
        }

        public override System.CodeDom.CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();

            method.Statements.Add(CodeGen.GetReturn(GetReaderExpression(CodeGen.GetReader())));

            return method;
        }

        public override int GetSize()
        {
            int ret = BaseEntry.GetSize();
            int length;

            if ((ret >= 0) && (Length.ParseInt(out length)))
            {
                ret = ret * length;
            }

            return ret;
        }

        public override string TypeName
        {
            get
            {
                return String.Format("{0}[{1}]", BaseEntry.GetTypeReference().BaseType, _lengthExpression);
            }
        }

        public CodeExpression GetReaderExpression(CodeExpression reader)
        {            
            if (_isByteLength)
            {
                return CodeGen.CallMethod(CodeGen.GetThis(), "FixBPA", BaseEntry.GetTypeReference(),
                    reader, CodeGen.GetLength(Length), _intEntry.GetEndian());
            }
            else
            {
                return CodeGen.CallMethod(CodeGen.GetThis(), "FixPA", BaseEntry.GetTypeReference(),
                    reader, CodeGen.GetLength(Length), _intEntry.GetEndian());

            }
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "FixWPA", obj, writer, CodeGen.GetLength(Length), _intEntry.GetEndian());
        }
    }
}
