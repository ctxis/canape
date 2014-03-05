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
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Parser
{
    /// <summary>
    /// A string member entry where the length is based on another member
    /// </summary>
    [Serializable]
    public class ReferenceLengthStringMemberEntry : StringMemberEntry, IMemberReaderWriter, IMemberReference
    {
        MemberEntryReference _reference;
        bool _isByteLength;
        int _adjustment;
        string _lengthReadExpression;
        string _lengthWriteExpression;

        public ReferenceLengthStringMemberEntry(string name)
            : base(name)
        {
            _reference = new MemberEntryReference(this, typeof(IntegerPrimitiveMemberEntry), typeof(StringMemberEntry));
        }

        /// <summary>
        /// Indicates if the length should be treated as a byte length or element
        /// </summary>
        [LocalizedDescription("ReferenceLengthStringMemberEntry_IsByteLengthDescription", typeof(Properties.Resources)), 
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

        [LocalizedDescription("ReferenceLengthStringMemberEntry_ReferenceDescription", typeof(Properties.Resources)), 
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

        [LocalizedDescription("ReferenceLengthStringMemberEntry_AdjustmentDescription", typeof(Properties.Resources)), 
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

        [LocalizedDescription("Generic_LengthReadExpressionDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public EvalExpression LengthReadExpression
        {
            get
            {
                return new EvalExpression(_lengthReadExpression ?? String.Empty);
            }

            set
            {
                if (value.Expression != _lengthReadExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _lengthReadExpression = value.Expression;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("Generic_LengthWriteExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression LengthWriteExpression
        {
            get
            {
                return new EvalExpression(_lengthWriteExpression ?? String.Empty);
            }

            set
            {
                if (value.Expression != _lengthWriteExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _lengthWriteExpression = value.Expression;
                    OnDirty();
                }
            }
        }

        public override CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            // No point doing anything if there is no length to write
            if (_reference.IsValid())
            {
                method.Statements.Add(GetWriterExpression(CodeGen.GetWriter(), CodeGen.GetObject()));
            }

            return method;
        }

        public override CodeMemberMethod GetPreSerializeMethod()
        {
            CodeMemberMethod method = base.GetPreSerializeMethod();

            if (_reference.IsValid())
            {
                CodeExpression right;

                if (_isByteLength)
                {
                    right = CodeGen.CallMethod(CodeGen.GetThis(), "SBL", CodeGen.GetThisField(Name), CodeGen.GetEnum(StringEncoding));
                }
                else
                {
                    right = CodeGen.GetProperty(CodeGen.GetThisField(Name), "Length");
                }                

                EvalExpression lengthWrite = LengthWriteExpression;

                if (lengthWrite.IsValid)
                {
                    right = lengthWrite.GetEvalExpression(right);
                }
                else if (_adjustment != 0)
                {
                    right = CodeGen.GetOperator(right, CodeBinaryOperatorType.Subtract, CodeGen.GetPrimitive(_adjustment));
                }                 

                if (_reference.GetTargetMember().BaseType is StringParserType)
                {
                    right = CodeGen.CallMethod(right, "ToString");
                }
                else
                {                   
                    right = CodeGen.GetCast(_reference.GetTargetMember().GetTypeReference(), right);
                }

                method.Statements.Add(CodeGen.GetAssign(CodeGen.GetField(CodeGen.GetThis(), _reference.GetNames()), right));
            }

            return method;
        }

        public override CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();

            method.Statements.Add(GetReaderExpression(CodeGen.GetReader()));

            return method;
        }

        public override string TypeName
        {
            get
            {
                return String.Format("{0} String (Length From '{1}')", StringEncoding.ToString(), 
                    String.IsNullOrWhiteSpace(_lengthReadExpression) ? _reference.ToString() : _lengthReadExpression);
            }
        }

        public CodeExpression GetReaderExpression(CodeExpression reader)
        {
            CodeExpression returnExpression;

            // Might as well return an empty string
            if (!_reference.IsValid())
            {
                returnExpression = CodeGen.GetPrimitive(String.Empty);
            }
            else
            {
                string name = _isByteLength ? "FixBS" : "FixS";
                List<CodeExpression> parameters = new List<CodeExpression>();

                parameters.Add(reader);
                parameters.Add(CodeGen.GetEnum(StringEncoding));
                parameters.Add(CodeGen.GetField(CodeGen.GetThis(), _reference.GetNames()));

                EvalExpression lengthRead = LengthReadExpression;

                if (lengthRead.IsValid)
                {
                    parameters.Add(CodeGen.GetPrimitive(lengthRead.Expression));
                }
                else
                {
                    parameters.Add(CodeGen.GetPrimitive(_adjustment));
                }

                returnExpression = CodeGen.CallMethod(CodeGen.GetThis(), name, parameters.ToArray());
            }

            return returnExpression;
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {            
            // No point doing anything if there is no length to write
            if (_reference.IsValid())
            {
                if (StringEncoding == BinaryStringEncoding.ASCII)
                {
                    return CodeGen.CallMethod(writer, "Write", obj);
                }
                else
                {
                    return CodeGen.CallMethod(writer, "Write", obj, CodeGen.GetEnum(StringEncoding));
                }
            }
            else
            {
                return CodeGen.CallMethod(writer, "Write", CodeGen.GetPrimitive(String.Empty));                    
            }
        }
    }
}
