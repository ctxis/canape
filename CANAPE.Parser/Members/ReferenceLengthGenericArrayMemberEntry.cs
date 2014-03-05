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
using System.ComponentModel;
using System.IO;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    [Serializable]
    public sealed class ReferenceLengthGenericArrayMemberEntry : ArrayMemberEntry, IMemberReference
    {
        MemberEntryReference _reference;
        bool _isByteLength;
        int _adjustment;

        public ReferenceLengthGenericArrayMemberEntry(IMemberReaderWriter entry)
            : base((MemberEntry)entry)
        {
            _reference = new MemberEntryReference(this);
        }

        [LocalizedDescription("ReferenceLengthGenericArrayMemberEntry_ReferenceDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior"), TypeConverter(typeof(MemberEntryReferenceConverter))]
        public MemberEntryReference Reference
        {
            get { return _reference; }

            set
            {
                if (!_reference.Equals(value))
                {
                    _reference = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("ReferenceLengthGenericArrayMemberEntry_AdjustmentDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public int Adjustment
        {
            get { return _adjustment; }
            set
            {
                if (_adjustment != value)
                {
                    _adjustment = value;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Indicates if the length should be treated as a byte length or element
        /// </summary>
        [LocalizedDescription("ReferenceLengthGenericArrayMemberEntry_IsByteLengthDescription", typeof(Properties.Resources)), 
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

        [Browsable(false)]
        public override string TypeName
        {
            get
            {
                return String.Format("{0}[{1}]", BaseEntry.GetTypeReference().BaseType, _reference.ToString());
            }
        }

        private CodeExpression GetLength()
        {
            Type refType = ((BuiltinParserType)_reference.GetTargetMember().BaseType).DataType;

            CodeExpression ret = CodeGen.GetField(CodeGen.GetThis(), _reference.GetNames());

            if (refType != typeof(int))
            {
                ret = CodeGen.CallMethod(CodeGen.GetThis(), "CL", ret);
            }

            if (_adjustment != 0)
            {
                ret = CodeGen.GetOperator(ret, CodeBinaryOperatorType.Add, CodeGen.GetPrimitive(_adjustment));
            }

            return ret;
        }

        public override System.CodeDom.CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();
            IMemberReaderWriter entry = BaseEntry as IMemberReaderWriter;

            if (_reference.IsValid())
            {
                CodeTypeReference listType = CodeGen.CreateGenericType(typeof(List<>), BaseEntry.GetTypeReference());
                List<CodeStatement> loopStatements = new List<CodeStatement>();

                method.Statements.Add(CodeGen.MakeVariable(listType, "ret", CodeGen.CreateObject(listType)));

                method.Statements.Add(CodeGen.MakeVariable(typeof(int), "length", GetLength()));

                loopStatements.Add(CodeGen.GetStatement(CodeGen.CallMethod(CodeGen.GetVariable("ret"), "Add", entry.GetReaderExpression(CodeGen.GetReader()))));

                if (_isByteLength)
                {
                    // Reassign reader to just the sub data
                    method.Statements.Add(CodeGen.MakeVariable(typeof(DataReader), "reader", CodeGen.CreateObject(typeof(DataReader), 
                        CodeGen.CallMethod(CodeGen.GetArgument("reader"), "ReadBytes", GetLength()))));
                    
                    method.Statements.Add(CodeGen.GetTryCatch(new CodeStatement[] { CodeGen.GetInfLoop(loopStatements.ToArray()) }, typeof(EndOfStreamException)));

                    method.Statements.Add(CodeGen.CallMethod(CodeGen.GetArgument("reader"), "Flush"));
                }
                else
                {
                    method.Statements.Add(CodeGen.GetForLoop("i", 0, CodeGen.GetVariable("length"), 1, loopStatements.ToArray()));
                }
                
                method.Statements.Add(CodeGen.GetReturn(CodeGen.CallMethod(CodeGen.GetVariable("ret"), "ToArray")));
            }
            else
            {
                method.Statements.Add(CodeGen.GetReturn(CodeGen.CreateArray(GetTypeReference(), 0)));
            }
            
            return method;
        }

        public override System.CodeDom.CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            // No point doing anything if there is no length to write
            if (_reference.IsValid())
            {
                IMemberReaderWriter entry = BaseEntry as IMemberReaderWriter;
                    
                List<CodeStatement> loopStatements = new List<CodeStatement>();

                loopStatements.Add(CodeGen.GetStatement(entry.GetWriterExpression(CodeGen.GetWriter(), CodeGen.GetIndex(CodeGen.GetObject(), 
                    CodeGen.GetVariable("i")))));

                method.Statements.Add(CodeGen.GetForLoop("i", 0, CodeGen.GetProperty(CodeGen.GetObject(), "Length"), 
                    1, loopStatements.ToArray()));
            }

            return method;
        }

        public override System.CodeDom.CodeMemberMethod GetPreSerializeMethod()
        {
            CodeMemberMethod method = base.GetPreSerializeMethod();

            if (_reference.IsValid())
            {
                CodeExpression right;

                if (_isByteLength)
                {
                    // Should really save this out and reuse when it comes to actually writing the data, seems a waste
                    method.Statements.Add(CodeGen.MakeVariable(typeof(DataWriter), "writer", CodeGen.CreateObject(typeof(DataWriter))));                    

                    IMemberReaderWriter entry = BaseEntry as IMemberReaderWriter;

                    List<CodeStatement> loopStatements = new List<CodeStatement>();

                    loopStatements.Add(CodeGen.GetStatement(entry.GetWriterExpression(CodeGen.GetWriter(), CodeGen.GetIndex(CodeGen.GetThisField(Name),
                        CodeGen.GetVariable("i")))));

                    method.Statements.Add(CodeGen.GetForLoop("i", 0, CodeGen.GetProperty(CodeGen.GetThisField(Name), "Length"),
                        1, loopStatements.ToArray()));

                    right = CodeGen.GetProperty(CodeGen.GetVariable("writer"), "BytesWritten");
                }
                else
                {
                    right = CodeGen.GetProperty(CodeGen.GetThisField(Name), "Length");
                }

                if (_adjustment != 0)
                {
                    right = CodeGen.GetOperator(right, CodeBinaryOperatorType.Subtract, CodeGen.GetPrimitive(_adjustment));
                }
                               
                method.Statements.Add(CodeGen.GetAssign(CodeGen.GetField(CodeGen.GetThis(), 
                    _reference.GetNames()), CodeGen.GetCast(_reference.GetTargetMember().GetTypeReference(), right)));
            }

            return method;
        }
    }
}
