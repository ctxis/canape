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
using NCalc;
using NCalc.Domain;

namespace CANAPE.Parser
{
    /// <summary>
    /// An object which represents an evaluation expression
    /// </summary>
    [TypeConverter(typeof(EvalExpressionTypeConverter))]
    public class EvalExpression
    {
        public EvalExpression()
        {
            Expression = "";
        }

        public EvalExpression(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; set; }

        public override string ToString()
        {
            return Expression;
        }

        class ExprVisitor : NCalc.Domain.LogicalExpressionVisitor
        {
            public bool ComplexExpression { get; set; }

            public override void Visit(NCalc.Domain.Function function)
            {
                ComplexExpression = true;
            }

            public override void Visit(NCalc.Domain.Identifier function)
            {
                ComplexExpression = true;
            }

            public override void Visit(NCalc.Domain.TernaryExpression expression)
            {
                ComplexExpression = true;
            }

            public override void Visit(NCalc.Domain.ValueExpression expression)
            {                
            }

            public override void Visit(NCalc.Domain.UnaryExpression expression)
            {                
            }

            public override void Visit(NCalc.Domain.BinaryExpression expression)
            {
            }

            public override void Visit(NCalc.Domain.LogicalExpression expression)
            {
                throw new NotImplementedException();
            }
        }

        public bool ParseInt(out int value)
        {
            bool parsed = false;

            value = 0;
            if (IsValid)
            {               
                try
                {
                    NCalc.Expression expr = new NCalc.Expression(Expression);
                    LogicalExpression pexpr = NCalc.Expression.Compile(Expression, false);
                    ExprVisitor v = new ExprVisitor();

                    pexpr.Accept(v);

                    if (!v.ComplexExpression)
                    {
                        value = Convert.ToInt32(expr.Evaluate());
                        parsed = true;
                    }
                }
                catch (Exception)
                {
                }
            }

            return parsed;
        }

        public CodeExpression GetCheckExpression()
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "Check", CodeGen.GetPrimitive(Expression));
        }

        public CodeExpression GetCheckExpression(CodeExpression valueExpression)
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "Check", CodeGen.GetPrimitive(Expression), valueExpression);
        }

        public CodeExpression GetEvalExpression()
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "Resolve", CodeGen.GetPrimitive(Expression));
        }

        public CodeExpression GetEvalExpression(CodeExpression valueExpression)
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "Resolve", CodeGen.GetPrimitive(Expression), valueExpression);
        }

        public Expression GetRawExpression()
        {
            return new Expression(Expression);
        }

        public bool IsValid
        {
            get 
            {
                if (!string.IsNullOrWhiteSpace(Expression))
                {
                    try
                    {
                        NCalc.Expression.Compile(Expression, true);

                        // This is as valid as we can easily tell
                        return true;
                    }
                    catch (Exception)
                    {
                    }
                }

                return false;
            }
        }
    }
}
