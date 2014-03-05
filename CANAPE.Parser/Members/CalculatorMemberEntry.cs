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
    public class CalculatorMemberEntry : ContainerMemberEntry, IMemberReaderWriter
    {
        IMemberReaderWriter _memberEntry;
        private string _readExpression;
        private string _writeExpression;

        [LocalizedDescription("CalculatorMemberEntry_OnlyCalculatedDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public bool OnlyCalculated { get; set; }

        [LocalizedDescription("CalculatorMemberEntry_ReadExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression ReadExpression
        {
            get
            {
                return new EvalExpression(_readExpression ?? String.Empty);
            }

            set
            {
                if (value.Expression != _readExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        NCalc.Expression.Compile(value.Expression, true);
                    }

                    _readExpression = value.Expression;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("CalculatorMemberEntry_WriteExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression WriteExpression
        {
            get
            {
                return new EvalExpression(_writeExpression ?? String.Empty);
            }

            set
            {
                if (value.Expression != _writeExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        NCalc.Expression.Compile(value.Expression, true);
                    }

                    _writeExpression = value.Expression;
                    OnDirty();
                }
            }
        }

        public CalculatorMemberEntry(IMemberReaderWriter memberEntry) 
            : base((MemberEntry)memberEntry)
        {           
            _memberEntry = memberEntry;
            _readExpression = "value";
            _writeExpression = "value";
        }

        public override System.CodeDom.CodeTypeReference GetTypeReference()
        {
            return BaseEntry.GetTypeReference();
        }

        public System.CodeDom.CodeExpression GetReaderExpression(CodeExpression reader)
        {
            CodeExpression ret = OnlyCalculated ? reader : _memberEntry.GetReaderExpression(reader);

            ret = CodeGen.GetCalc(ReadExpression, BaseEntry.GetTypeReference(), ret);

            return ret;
        }

        public System.CodeDom.CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            if (OnlyCalculated)
            {
                return CodeGen.GetResolve(WriteExpression, obj, writer);
            }
            else
            {
                obj = CodeGen.GetCalc(WriteExpression, BaseEntry.GetTypeReference(), CodeGen.GetRef(obj));

                return _memberEntry.GetWriterExpression(writer, obj);
            }
        }

        public override string TypeName
        {
            get
            {
                return String.Format("Calculated: {0}", base.TypeName);
            }
        }
    }
}
