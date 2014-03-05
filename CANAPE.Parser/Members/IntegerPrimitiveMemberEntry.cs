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
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    [Serializable]
    public class IntegerPrimitiveMemberEntry : NumericPrimitiveMemberEntry
    {
        private PrimitiveMemberEntryFormat _format;

        private static Type CheckType(Type t)
        {
            // Integer primitive entires can only have types which are integers, the actual type used might 
            if ((t != typeof(byte)) && (t != typeof(sbyte)) && (t != typeof(short)) && (t != typeof(ushort)) && (t != typeof(int)) &&
                (t != typeof(uint)) && (t != typeof(long)) && (t != typeof(ulong)))
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.FloatPrimitiveMemberEntry_InvalidType);
            }

            return t;
        }        

        public IntegerPrimitiveMemberEntry(string name, Type type, Endian endian)
            : base(name, CheckType(type), endian)
        {
        }
        
        /// <summary>
        /// Get basic format
        /// </summary>
        [LocalizedDescription("IntegerPrimitiveMemberEntry_FormatDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Display")]
        public PrimitiveMemberEntryFormat Format
        {
            get
            {
                return _format;
            }

            set
            {
                if (_format != value)
                {
                    _format = value;
                    OnDirty();
                }
            }
        }

        protected override string GetFormatString()
        {
            string formatString = null;

            switch(_format)
            {
                case PrimitiveMemberEntryFormat.Decimal:
                    formatString = "{0:d}";
                    break;
                case PrimitiveMemberEntryFormat.Hexadecimal:
                    formatString = "0x{0:X}";
                    break;
            }

            return formatString;
        }

        /// <summary>
        /// Get a type to cast to
        /// </summary>
        /// <returns>The case type declaration</returns>
        public CodeTypeReference GetCastType()
        {
            return GetTypeReference();
        }
    }
}
