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
using System.CodeDom;

namespace CANAPE.Parser
{
    internal static class CodeGen
    {
        internal static CodeExpression CreateByteArray(byte[] bytes)
        {
            List<CodeExpression> inits = new List<CodeExpression>();

            foreach(byte b in bytes)
            {
                inits.Add(CodeGen.GetPrimitive(b));
            }

            return new CodeArrayCreateExpression(typeof(byte[]), inits.ToArray());
        }

        internal static CodeMethodInvokeExpression CallMethod(Type t, string name, params CodeExpression[] arguments)
        {
            return CallMethod(new CodeTypeReferenceExpression(t), name, arguments);
        }

        internal static CodeMethodInvokeExpression CallMethod(CodeExpression obj, string name, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(obj, name, arguments);
        }

        internal static CodeMethodInvokeExpression CallMethod(CodeExpression obj, string name, Type genericType, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(obj, name, new CodeTypeReference(genericType)), arguments);
        }

        internal static CodeMethodInvokeExpression CallMethod(CodeExpression obj, string name, CodeTypeReference genericType, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(obj, name, genericType), arguments);
        }

        internal static CodeMethodInvokeExpression CallMethod(Type t, string name, Type genericType, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(t), name, new CodeTypeReference(genericType)),
                arguments);
        }

        internal static CodeMethodInvokeExpression CallMethod(Type t, string name, CodeTypeReference genericType, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(t), name, genericType),
                arguments);
        }

        internal static CodeThisReferenceExpression GetThis()
        {
            return new CodeThisReferenceExpression();
        }

        internal static CodeFieldReferenceExpression GetField(CodeExpression target, string name)
        {
            return new CodeFieldReferenceExpression(target, name);
        }

        internal static CodeFieldReferenceExpression GetField(CodeExpression rootTarget, IEnumerable<string> names)
        {
            CodeExpression ret = rootTarget;

            if (names.Count() == 0)
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.MemberEntry_InvalidFieldReference);
            }

            foreach (string name in names)
            {
                ret = GetField(ret, name);
            }

            return (CodeFieldReferenceExpression)ret;
        }

        internal static CodePropertyReferenceExpression GetProperty(CodeExpression target, string name)
        {
            CodePropertyReferenceExpression ret = new CodePropertyReferenceExpression(target, name);

            return ret;
        }

        internal static CodePropertyReferenceExpression GetThisProperty(string name)
        {
            return GetProperty(GetThis(), name);
        }

        internal static CodeFieldReferenceExpression GetThisField(string name)
        {
            return GetField(GetThis(), name);
        }

        internal static CodePrimitiveExpression GetPrimitive(object o)
        {
            return new CodePrimitiveExpression(o);
        }

        internal static CodeArgumentReferenceExpression GetArgument(string name)
        {
            return new CodeArgumentReferenceExpression(name);
        }

        internal static CodeExpression GetEnum(Enum e)
        {
            return GetField(new CodeTypeReferenceExpression(e.GetType()), Enum.GetName(e.GetType(), e));
        }

        internal static CodeMethodReturnStatement GetReturn(CodeExpression e)
        {
            return new CodeMethodReturnStatement(e);
        }

        internal static CodeArgumentReferenceExpression GetReader()
        {            
            return GetArgument("reader");
        }

        internal static CodeArgumentReferenceExpression GetWriter()
        {         
            return GetArgument("writer");
        }

        internal static CodeArgumentReferenceExpression GetObject()
        {
            return GetArgument("obj");
        }

        internal static CodeCastExpression GetCast(Type t, CodeExpression e)
        {
            return new CodeCastExpression(t, e);
        }

        internal static CodeCastExpression GetCast(string targetType, CodeExpression e)
        {
            return new CodeCastExpression(targetType, e);
        }

        internal static CodeCastExpression GetCast(CodeTypeReference typeRef, CodeExpression e)
        {
            return new CodeCastExpression(typeRef, e);
        }

        internal static CodeVariableDeclarationStatement MakeVariable(Type variableType, string name, CodeExpression assignExpression)
        {
            return new CodeVariableDeclarationStatement(variableType, name, assignExpression);
        }
        
        internal static CodeVariableDeclarationStatement MakeVariable(CodeTypeReference type, string name, CodeExpression assignExpression)
        {
            return new CodeVariableDeclarationStatement(type, name, assignExpression);
        }

        internal static CodeAssignStatement GetAssign(CodeExpression left, CodeExpression right)
        {
            return new CodeAssignStatement(left, right);
        }

        internal static CodeVariableReferenceExpression GetVariable(string name)
        {
            return new CodeVariableReferenceExpression(name);
        }

        internal static CodeObjectCreateExpression CreateObject(Type type, params CodeExpression[] arguments)
        {
            return new CodeObjectCreateExpression(type, arguments);
        }
        
        internal static CodeObjectCreateExpression CreateObject(CodeTypeReference type, params CodeExpression[] arguments)
        {
            return new CodeObjectCreateExpression(type, arguments);
        }

        internal static CodeTypeReference CreateGenericType(Type genericType, params CodeTypeReference[] types)
        {
            return new CodeTypeReference(genericType.FullName, types);
        }

        internal static CodeParameterDeclarationExpression GetParameter(Type t, string name)
        {
            return new CodeParameterDeclarationExpression(t, name);
        }

        internal static CodeParameterDeclarationExpression GetParameter(CodeTypeReference typeRef, string name)
        {
            return new CodeParameterDeclarationExpression(typeRef, name);
        }

        internal static CodeArgumentReferenceExpression GetLogger()
        {            
            return GetArgument("_logger");
        }

        internal static CodeConditionStatement GetIf(CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements)
        {
            return new CodeConditionStatement(condition, trueStatements, falseStatements);
        }

        internal static CodeConditionStatement GetIf(CodeExpression condition, CodeStatement[] trueStatements)
        {
            return new CodeConditionStatement(condition, trueStatements);
        }

        internal static CodeBinaryOperatorExpression GetOperator(CodeExpression left, CodeBinaryOperatorType type, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, type, right);
        }

        internal static CodeBinaryOperatorExpression GetIsTrue(CodeExpression exp)
        {
            return new CodeBinaryOperatorExpression(exp, CodeBinaryOperatorType.IdentityEquality, GetPrimitive(true));
        }

        internal static CodeTryCatchFinallyStatement GetTryCatch(CodeStatement[] tryStatements, Type exceptionType)
        {
            return new CodeTryCatchFinallyStatement(tryStatements, new CodeCatchClause[] { new CodeCatchClause("e", new CodeTypeReference(exceptionType)) });
        }

        internal static CodeIterationStatement GetForLoop(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatement, params CodeStatement[] statements)
        {
            return new CodeIterationStatement(initStatement, testExpression, incrementStatement, statements);
        }

        internal static CodeStatement GetIncrement(CodeVariableReferenceExpression var, int value)
        {
            return new CodeAssignStatement(var, GetOperator(var, CodeBinaryOperatorType.Add, GetPrimitive(value)));
        }

        internal static CodeIterationStatement GetForLoop(string varName, int startValue, CodeExpression endValue, int increment, params CodeStatement[] statements)
        {
            return new CodeIterationStatement(MakeVariable(typeof(int), varName, GetPrimitive(startValue)),
                GetOperator(GetVariable(varName), CodeBinaryOperatorType.LessThan, endValue),
                GetIncrement(GetVariable(varName), increment), statements);
        }

        internal static CodeIterationStatement GetInfLoop(params CodeStatement[] statements)
        {
            return new CodeIterationStatement(new CodeCommentStatement(""), GetPrimitive(true), new CodeCommentStatement(""), statements);
        }

        internal static CodeExpressionStatement GetStatement(CodeExpression e)
        {
            return new CodeExpressionStatement(e);
        }

        internal static CodeArrayCreateExpression CreateArray(Type t, int length)
        {
            return new CodeArrayCreateExpression(t, GetPrimitive(length));
        }

        internal static CodeArrayCreateExpression CreateArray(Type t, CodeExpression length)
        {
            return new CodeArrayCreateExpression(t, length);
        }

        internal static CodeArrayCreateExpression CreateArray(CodeTypeReference t, CodeExpression length)
        {
            return new CodeArrayCreateExpression(t, length);
        }

        internal static CodeArrayCreateExpression CreateArray(CodeTypeReference t, int length)
        {
            return CreateArray(t, CodeGen.GetPrimitive(length));
        }

        internal static CodeIndexerExpression GetIndex(CodeExpression obj, CodeExpression index)
        {
            return new CodeIndexerExpression(obj, index);
        }

        internal static CodeExpression GetLength(int length, EvalExpression lengthExpression)
        {
            if (lengthExpression.IsValid)
            {
                return CodeGen.CallMethod(CodeGen.GetThis(), "CL", CodeGen.GetPrimitive(length), CodeGen.GetPrimitive(lengthExpression.Expression));
            }
            else
            {
                return CodeGen.GetPrimitive(length);
            }
        }

        internal static CodeExpression GetLength(EvalExpression lengthExpression)
        {
            if (lengthExpression.IsValid)
            {
                int length;

                // If we can parse then just emit the primitive
                if (lengthExpression.ParseInt(out length))
                {
                    return CodeGen.GetPrimitive(length);
                }
                else
                {
                    return CodeGen.CallMethod(CodeGen.GetThis(), "CL", CodeGen.GetPrimitive(lengthExpression.Expression));
                }
            }
            else
            {
                return CodeGen.GetPrimitive(0);
            }
        }

        internal static CodeExpression GetCalc(EvalExpression expr, CodeTypeReference fieldType, CodeExpression value)
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "Calc", fieldType, CodeGen.GetPrimitive(expr.Expression), value);
        }

        internal static CodeExpression GetRef(CodeExpression value)
        {
            if ((value is CodeFieldReferenceExpression) || (value is CodeVariableReferenceExpression) || (value is CodeArgumentReferenceExpression))
            {
                return new CodeDirectionExpression(FieldDirection.Ref, value);
            }
            else
            {
                return value;
            }
        }

        internal static CodeExpression GetResolve(EvalExpression expr, CodeExpression value, CodeExpression writer)
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "Resolve", CodeGen.GetPrimitive(expr.Expression), writer, value);
        }

        internal static CodeExpression GetCheck(EvalExpression expr, CodeExpression value, CodeExpression i)
        {
            return CodeGen.CallMethod(CodeGen.GetThis(), "Check", CodeGen.GetPrimitive(expr.Expression), value, i);
        }

        internal static CodeExpression GetTypeOf(CodeTypeReference type)
        {
            return new CodeTypeOfExpression(type);
        }
    }
}
