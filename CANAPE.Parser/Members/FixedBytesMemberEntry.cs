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
    /// <summary>
    /// An entry which just contains a fixed set of bytes
    /// </summary>
    [Serializable]
    public class FixedBytesMemberEntry : MemberEntry, IMemberReaderWriter
    {
        private bool _validate;
        private byte[] _bytes;

        public FixedBytesMemberEntry(string name)
            : base(name, new BuiltinParserType(typeof(byte[])))
        {            
            _bytes = new byte[0];
        }

        [LocalizedDescription("FixedBytesMemberEntry_ValidateDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public bool Validate
        {
            get { return _validate; }
            set
            {
                if (_validate != value)
                {
                    _validate = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("FixedBytesMemberEntry_BytesDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public byte[] Bytes
        {
            get
            {
                return _bytes;
            }

            set
            {
                if (_bytes != value)
                {
                    _bytes = value;
                    OnDirty();
                }
            }
        }

        public CodeExpression GetReaderExpression(CodeExpression reader)
        {
            CodeExpression ret = CodeGen.CallMethod(reader, "ReadBytes", CodeGen.GetPrimitive(_bytes.Length));
            if (_validate)
            {
                ret = CodeGen.CallMethod(CodeGen.GetThis(), "CMP", ret, CodeGen.CreateByteArray(_bytes));
            }
            
            return ret;
        }

        public CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj)
        {
            return CodeGen.CallMethod(writer, "Write", CodeGen.CreateByteArray(_bytes));
        }

        public override CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(typeof(byte[]));
        }

        public override int GetSize()
        {
            return _bytes != null ? _bytes.Length : 0;
        }

        public override string TypeName
        {
            get
            {
                return String.Format("Fixed Bytes: '{0}'", GeneralUtils.EscapeBytes(_bytes ?? new byte[0]));
            }
        }
    }
}
