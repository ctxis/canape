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
using NCalc;

namespace CANAPE.Parser
{
    [Serializable]
    public class SizedMemberEntry : ContainerMemberEntry, IMemberReaderWriter
    {
        private IMemberReaderWriter _baseWriter;
        private bool _readToEnd;

        private string _sizeExpression;        

        [LocalizedDescription("SizedMemberEntry_ReadExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression SizeExpression
        {
            get
            {
                return new EvalExpression(_sizeExpression ?? String.Empty);
            }

            set
            {
                if (value.Expression != _sizeExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        NCalc.Expression.Compile(value.Expression, true);
                    }

                    _sizeExpression = value.Expression;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("SizedMemberEntry_ReadToEndDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public bool ReadToEnd
        {
            get
            {
                return _readToEnd;
            }

            set
            {
                if (_readToEnd != value)
                {
                    _readToEnd = true;
                    _readToEnd = value;
                    OnDirty();
                }
            }
        }

        public SizedMemberEntry(IMemberReaderWriter baseEntry)
            : base((MemberEntry)baseEntry)
        {
            _baseWriter = baseEntry;
        }

        public System.CodeDom.CodeExpression GetReaderExpression(CodeExpression reader)
        {
            if (ReadToEnd)
            {
                return _baseWriter.GetReaderExpression(CodeGen.CallMethod(CodeGen.GetThis(), "GBR", reader));
            }
            else
            {
                return _baseWriter.GetReaderExpression(CodeGen.CallMethod(CodeGen.GetThis(), "GBR", reader, CodeGen.GetPrimitive(_sizeExpression)));
            }
        }

        public System.CodeDom.CodeExpression GetWriterExpression(CodeExpression writer, System.CodeDom.CodeExpression obj)
        {
            return _baseWriter.GetWriterExpression(writer, obj);
        }

        public override System.CodeDom.CodeTypeReference GetTypeReference()
        {
            return BaseEntry.GetTypeReference();
        }

        public override string TypeName
        {
            get
            {
                return String.Format("{0} sized '{1}'", BaseEntry.TypeName, _sizeExpression ?? String.Empty);        
            }
        }
    }
}
