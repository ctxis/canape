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
    [Serializable]
    public class TerminatedGenericArrayMemberEntry : ArrayMemberEntry
    {
        private IMemberReaderWriter _memberEntry;
        private string _termCondition;
        private bool _required;

        [LocalizedDescription("TerminatedGenericArrayMemberEntry_TermExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression TermExpression
        {
            get
            {
                return new EvalExpression(_termCondition ?? String.Empty);
            }

            set
            {
                if (value.Expression != _termCondition)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _termCondition = value.Expression;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("TerminatedGenericArrayMemberEntry_RequiredDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public bool Required
        {
            get
            {
                return _required;
            }

            set
            {
                if (_required != value)
                {
                    _required = value;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of entry</param>
        /// <param name="baseType">Base type of entry</param>
        public TerminatedGenericArrayMemberEntry(IMemberReaderWriter baseEntry) 
            : base((MemberEntry)baseEntry)
        {
            _memberEntry = baseEntry;
            _required = true;
        }

        public override System.CodeDom.CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();
            
            CodeTypeReference listType = CodeGen.CreateGenericType(typeof(List<>), BaseEntry.GetTypeReference());
            method.Statements.Add(CodeGen.MakeVariable(listType, "ret", CodeGen.CreateObject(listType)));
            method.Statements.Add(CodeGen.MakeVariable(typeof(int), "i", CodeGen.GetPrimitive(0)));

            List<CodeStatement> loopStatements = new List<CodeStatement>();

            loopStatements.Add(CodeGen.MakeVariable(BaseEntry.GetTypeReference(), "o", _memberEntry.GetReaderExpression(CodeGen.GetReader())));
            loopStatements.Add(CodeGen.GetStatement(CodeGen.CallMethod(CodeGen.GetVariable("ret"), "Add", CodeGen.GetVariable("o"))));

            if(TermExpression.IsValid)
            {
                CodeStatement[] ret = new CodeStatement[] { CodeGen.GetReturn(CodeGen.CallMethod(CodeGen.GetVariable("ret"), "ToArray")) };

                loopStatements.Add(CodeGen.GetIf(CodeGen.GetCheck(TermExpression, CodeGen.GetVariable("o"), CodeGen.GetVariable("i")), ret));

                loopStatements.Add(CodeGen.GetIncrement(CodeGen.GetVariable("i"), 1));
            }

            if (!_required)
            {
                method.Statements.Add(
                        CodeGen.GetTryCatch(new CodeStatement[] { CodeGen.GetInfLoop(loopStatements.ToArray()) }, typeof(EndOfStreamException)));
                method.Statements.Add(CodeGen.GetReturn(CodeGen.CallMethod(CodeGen.GetVariable("ret"), "ToArray")));
            }
            else
            {
                method.Statements.Add(CodeGen.GetInfLoop(loopStatements.ToArray()));
            }
            
            return method;
        }
        
        public override System.CodeDom.CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            IMemberReaderWriter entry = BaseEntry as IMemberReaderWriter;

            List<CodeStatement> loopStatements = new List<CodeStatement>();

            loopStatements.Add(CodeGen.GetStatement(
                entry.GetWriterExpression(CodeGen.GetWriter(), new CodeIndexerExpression(CodeGen.GetObject(), CodeGen.GetVariable("i")))));

            method.Statements.Add(CodeGen.GetForLoop("i", 0, 
                CodeGen.GetProperty(CodeGen.GetObject(), "Length"), 1, loopStatements.ToArray())); 
            
            return method;
        }

        public override string TypeName
        {
            get
            {
                return String.Format("{0}[{1}]", BaseEntry.GetTypeReference().BaseType, _termCondition);
            }
        }

        public override int GetSize()
        {
            return -1;
        }
    }
}
