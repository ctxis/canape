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
using CANAPE.DataFrames;
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Parser
{
    [Serializable]
    public class ReadToEndGenericArrayMemberEntry : ArrayMemberEntry
    {
        IMemberReaderWriter _memberEntry;

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
        public ReadToEndGenericArrayMemberEntry(IMemberReaderWriter baseEntry) 
            : base((MemberEntry)baseEntry)
        {
            _memberEntry = baseEntry;
        }

        public override System.CodeDom.CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();
            
            CodeTypeReference listType = CodeGen.CreateGenericType(typeof(List<>), BaseEntry.GetTypeReference());
            method.Statements.Add(CodeGen.MakeVariable(listType, "ret", CodeGen.CreateObject(listType)));            

            if (TrailingLength.IsValid) 
            {
                int length;

                // If not a primitive expression or the length is not 0
                if (!TrailingLength.ParseInt(out length) || (length != 0))
                {
                    method.Statements.Add(CodeGen.GetAssign(CodeGen.GetReader(), CodeGen.CreateObject(typeof(DataReader), CodeGen.CallMethod(CodeGen.GetReader(),
                        "ReadToEndTrail", CodeGen.GetLength(TrailingLength)))));
                }
            }                

            List<CodeStatement> loopStatements = new List<CodeStatement>();

            loopStatements.Add(CodeGen.GetStatement(CodeGen.CallMethod(CodeGen.GetVariable("ret"), "Add",
                _memberEntry.GetReaderExpression(CodeGen.GetReader()))));
                
            method.Statements.Add(
                CodeGen.GetTryCatch(new CodeStatement[] { CodeGen.GetInfLoop(loopStatements.ToArray())}, typeof(EndOfStreamException)));

            method.Statements.Add(CodeGen.GetReturn(CodeGen.CallMethod(CodeGen.GetVariable("ret"), "ToArray")));
            
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
                return String.Format("{0}[*]", BaseEntry.GetTypeReference().BaseType);
            }
        }

        public override int GetSize()
        {
            return -1;
        }
    }
}
