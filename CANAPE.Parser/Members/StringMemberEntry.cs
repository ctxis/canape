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
using System.ComponentModel;
using CANAPE.Utils;
using System;

namespace CANAPE.Parser
{
    /// <summary>
    /// Member entry defining a string
    /// </summary>
    [Serializable]
    public abstract class StringMemberEntry : MemberEntry
    {
        private BinaryStringEncoding _stringEncoding;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the member</param>        
        protected StringMemberEntry(string name)
            : base(name, new StringParserType())
        {
        }
        
        /// <summary>
        /// Create expression to get the encoding of the string
        /// </summary>
        /// <returns>The expression to get the encoding</returns>
        protected CodeExpression GetEncoding()
        {
            return CodeGen.CallMethod(typeof(GeneralUtils), "GetEncodingFromType", CodeGen.GetEnum(StringEncoding));
        }

        /// <summary>
        /// Get approximate size of the encoding
        /// </summary>
        /// <returns>The approximate size</returns>
        protected int GetEncodingSize()
        {
            switch (_stringEncoding)
            {
                case BinaryStringEncoding.ASCII:
                case BinaryStringEncoding.EBCDIC_US:
                case BinaryStringEncoding.Latin1:
                case BinaryStringEncoding.UTF7:
                case BinaryStringEncoding.UTF8:
                    return 1;
                case BinaryStringEncoding.ShiftJIS:
                case BinaryStringEncoding.UTF16_BE:
                case BinaryStringEncoding.UTF16_LE:
                    return 2;
                case BinaryStringEncoding.UTF32_BE:
                case BinaryStringEncoding.UTF32_LE:
                    return 4;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Get the code type reference
        /// </summary>
        /// <returns></returns>
        public override CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(typeof(string));
        }

        /// <summary>
        /// Get or set the string encoding for the entry
        /// </summary>
        [LocalizedDescription("StringMemberEntry_StringEncodingDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public BinaryStringEncoding StringEncoding 
        {
            get
            {
                return _stringEncoding;
            }

            set
            {
                if (_stringEncoding != value)
                {
                    _stringEncoding = value;
                    OnDirty();
                }
            }
        }
    }
}
