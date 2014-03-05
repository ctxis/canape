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
using System.Collections.Generic;
using System.IO;
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Parser
{
    /// <summary>
    /// Member entry which reads an array until the end of the stream
    /// </summary>
    [Serializable]
    public class ReadToEndPrimitiveArrayMemberEntry : ArrayMemberEntry, IMemberReaderWriter
    {
        private IntegerPrimitiveMemberEntry _intEntry;
        private string _trailingLength;


        [LocalizedDescription("Generic_TrailingLengthDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression TrailingLength
        {
            get
            {
                return new EvalExpression(_trailingLength ?? "0");
            }

            set
            {
                if (value.Expression != _trailingLength)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _trailingLength = value.Expression;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of entry</param>
        /// <param name="baseType">Base type of entry</param>
        public ReadToEndPrimitiveArrayMemberEntry(IntegerPrimitiveMemberEntry baseEntry) 
            : base(baseEntry)
        {
            _intEntry = baseEntry;
            _trailingLength = "0";
        }

        public override System.CodeDom.CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();

            method.Statements.Add(CodeGen.GetReturn(GetReaderExpression(CodeGen.GetReader())));

            return method;
        }

        public override System.CodeDom.CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            method.Statements.Add(GetWriterExpression(CodeGen.GetWriter(), CodeGen.GetObject()));

            return method;
        }

        public override string TypeName
        {
            get
            {
                return String.Format("{0}[*]", BaseEntry.GetTypeReference().BaseType);
            }
        }

        public CodeExpression GetReaderExpression(CodeExpression reader)
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "RTEPA", _intEntry.GetTypeReference(), reader, _intEntry.GetEndian(), CodeGen.GetLength(TrailingLength));
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "WPA", obj, writer, _intEntry.GetEndian());
        }

        public override int GetSize()
        {
            return -1;
        }
    }
}
