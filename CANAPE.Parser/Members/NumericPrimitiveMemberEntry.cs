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
using CANAPE.Utils;

namespace CANAPE.Parser
{
    [Serializable]
    public abstract class NumericPrimitiveMemberEntry : PrimitiveMemberEntry
    {        
        Endian _endian;
        string _customFormat;

        protected NumericPrimitiveMemberEntry(string name, Type type, Endian endian)
            : base(name, type)
        {
            _endian = endian;
        }

        protected abstract string GetFormatString();

        /// <summary>
        /// Get the endian as a primitive expression (true or false)
        /// </summary>
        /// <returns>The expression for the endian</returns>
        public CodePrimitiveExpression GetEndian()
        {
            return new CodePrimitiveExpression(_endian == Parser.Endian.LittleEndian);
        }

        public override CodeExpression GetReaderExpression(CodeExpression reader)
        {
            return CodeGen.CallMethod(reader, "ReadPrimitive", GetTypeReference(), GetEndian());
        }

        public override CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            CodeExpression ret;
            Type t = GetDataType();

            if ((t == typeof(byte)) || (t == typeof(sbyte)))
            {
                ret = CodeGen.CallMethod(writer, "Write", obj);
            }
            else
            {
                ret = CodeGen.CallMethod(writer, "Write", obj, GetEndian());
            }

            return ret;
        }

        public override CodeMemberField GetMemberDeclaration()
        {
            CodeMemberField field = base.GetMemberDeclaration();
            string formatString;

            if (!String.IsNullOrWhiteSpace(_customFormat))
            {
                formatString = _customFormat;
            }
            else
            {
                formatString = GetFormatString();
            }
            
            if (formatString != null)
            {
                field.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(FormatStringAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(formatString))));
            }

            return field;
        }

        /// <summary>
        /// Get or set the endian of the type
        /// </summary>
        [LocalizedDescription("NumericPrimitiveMemberEntry_EndianDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public Endian Endian
        {
            get
            {
                return _endian;
            }

            set
            {
                if (_endian != value)
                {
                    _endian = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("NumericPrimitiveMemberEntry_CustomFormatDescription", typeof(Properties.Resources)), 
            Category("Display")]
        public string CustomFormat
        {
            get { return _customFormat;  }
            set
            {
                if (_customFormat != value)
                {
                    _customFormat = value;
                    OnDirty();
                }
            }
        }

    }
}
