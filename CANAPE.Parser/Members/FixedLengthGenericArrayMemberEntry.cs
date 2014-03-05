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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.CodeDom;
using CANAPE.Utils;
using NCalc;
using System.IO;

namespace CANAPE.Parser
{
    [Serializable]
    public sealed class FixedLengthGenericArrayMemberEntry : ArrayMemberEntry
    {
        private int _length;
        private string _lengthExpression;
        private bool _isByteLength;

        protected override void OnDeserialized()
        {
            if (String.IsNullOrWhiteSpace(_lengthExpression))
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseEntry">The base entry</param>
        public FixedLengthGenericArrayMemberEntry(IMemberReaderWriter baseEntry)
            : base((MemberEntry)baseEntry)
        {
            _length = 0;
            _lengthExpression = "0";
        }

        public override System.CodeDom.CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            IMemberReaderWriter entry = BaseEntry as IMemberReaderWriter;
                
            List<CodeStatement> loopStatements = new List<CodeStatement>();            

            loopStatements.Add(CodeGen.GetStatement(
                entry.GetWriterExpression(CodeGen.GetWriter(),
                    CodeGen.GetIndex(CodeGen.GetObject(), CodeGen.GetVariable("i"))
            )));

            method.Statements.Add(
                    CodeGen.GetForLoop("i", 0, 
                        CodeGen.GetProperty(CodeGen.GetObject(), "Length"), 
                    1, loopStatements.ToArray()));

            return method;
        }

        public override System.CodeDom.CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();
            IMemberReaderWriter entry = BaseEntry as IMemberReaderWriter;

            if (_isByteLength)
            {
                CodeTypeReference listType = CodeGen.CreateGenericType(typeof(List<>), BaseEntry.GetTypeReference());
                method.Statements.Add(CodeGen.MakeVariable(listType, "ret",
                    CodeGen.CreateObject(listType)));

                method.Statements.Add(CodeGen.GetAssign(CodeGen.GetReader(), 
                    CodeGen.CallMethod(CodeGen.GetThis(), "GBR", CodeGen.GetReader(), CodeGen.GetPrimitive(_lengthExpression))));

                List<CodeStatement> loopStatements = new List<CodeStatement>();

                CodeExpression expr = CodeGen.CallMethod(CodeGen.GetVariable("ret"), "Add", entry.GetReaderExpression(CodeGen.GetReader()));

                method.Statements.Add(CodeGen.GetTryCatch(new CodeStatement[] { CodeGen.GetInfLoop(new CodeExpressionStatement(expr)) }, typeof(EndOfStreamException)));

                method.Statements.Add(CodeGen.GetReturn(CodeGen.CallMethod(CodeGen.GetVariable("ret"), "ToArray")));
            }
            else
            {

                method.Statements.Add(CodeGen.MakeVariable(GetTypeReference(), "ret",
                    CodeGen.CreateArray(GetTypeReference(), CodeGen.GetLength(Length))));

                List<CodeStatement> loopStatements = new List<CodeStatement>();

                loopStatements.Add(CodeGen.GetAssign(
                        CodeGen.GetIndex(
                            CodeGen.GetVariable("ret"), CodeGen.GetVariable("i")
                        ),
                        entry.GetReaderExpression(CodeGen.GetReader()))
                );

                method.Statements.Add(CodeGen.GetForLoop("i", 0, CodeGen.GetProperty(CodeGen.GetVariable("ret"), "Length"), 1, loopStatements.ToArray()));

                method.Statements.Add(CodeGen.GetReturn(CodeGen.GetVariable("ret")));
            }

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

    }
}
