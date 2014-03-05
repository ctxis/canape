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

using System.CodeDom;
using System;
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Parser
{
    /// <summary>
    /// A string member entry where data should read to the end
    /// </summary>
    [Serializable]
    public class ReadToEndStringMemberEntry : StringMemberEntry
    {
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

        public ReadToEndStringMemberEntry(string name)
            : base(name)
        {
            _trailingLength = "0";
        }

        public override System.CodeDom.CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = base.GetSerializerMethod();

            method.Statements.Add(CodeGen.CallMethod(CodeGen.GetWriter(), "Write", 
                CodeGen.GetObject(), GetEncoding()));

            return method;
        }

        public override System.CodeDom.CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = base.GetDeserializerMethod();

            CodeExpression returnExpression;

            returnExpression = CodeGen.CallMethod(CodeGen.GetReader(), "ReadToEndTrail", GetEncoding(), CodeGen.GetLength(TrailingLength));

            method.Statements.Add(CodeGen.GetReturn(returnExpression));

            return method;
        }

        public override string TypeName
        {
            get
            {
                return String.Format(Properties.Resources.ReadToEndStringMemberEntry_ToStringFormat, StringEncoding.ToString());
            }
        }
    }
}
