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
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    [Serializable]
    public class BitFieldMemberEntry : IntegerPrimitiveMemberEntry
    {
        private int _bits;        

        public BitFieldMemberEntry(string name, Endian endian)
            : base(name, typeof(ulong), endian)
        {
            _bits = 1;
        }

        [LocalizedDescription("BitFieldMemberEntry_BitsDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public int Bits
        {
            get { return _bits; }
            set
            {
                if (_bits != value)
                {
                    if ((value <= 0) || (value > 64))
                    {
                        throw new ArgumentException(CANAPE.Parser.Properties.Resources.BitField_InvalidSize);
                    }

                    _bits = value;                    

                    OnDirty();
                }
            }
        }

        private Type GetTypeForBits()
        {
            switch (_bits)
            {
                case 1: return typeof(Int1B);
                case 2: return typeof(Int2B);
                case 3: return typeof(Int3B);
                case 4: return typeof(Int4B);
                case 5: return typeof(Int5B);
                case 6: return typeof(Int6B);
                case 7: return typeof(Int7B);
                case 8: return typeof(Int8B);
                case 9: return typeof(Int9B);
                case 10: return typeof(Int10B);
                case 11: return typeof(Int11B);
                case 12: return typeof(Int12B);
                case 13: return typeof(Int13B);
                case 14: return typeof(Int14B);
                case 15: return typeof(Int15B);
                case 16: return typeof(Int16B);
                case 17: return typeof(Int17B);
                case 18: return typeof(Int18B);
                case 19: return typeof(Int19B);
                case 20: return typeof(Int20B);
                case 21: return typeof(Int21B);
                case 22: return typeof(Int22B);
                case 23: return typeof(Int23B);
                case 24: return typeof(Int24B);
                case 25: return typeof(Int25B);
                case 26: return typeof(Int26B);
                case 27: return typeof(Int27B);
                case 28: return typeof(Int28B);
                case 29: return typeof(Int29B);
                case 30: return typeof(Int30B);
                case 31: return typeof(Int31B);
                case 32: return typeof(Int32B);
                case 33: return typeof(Int33B);
                case 34: return typeof(Int34B);
                case 35: return typeof(Int35B);
                case 36: return typeof(Int36B);
                case 37: return typeof(Int37B);
                case 38: return typeof(Int38B);
                case 39: return typeof(Int39B);
                case 40: return typeof(Int40B);
                case 41: return typeof(Int41B);
                case 42: return typeof(Int42B);
                case 43: return typeof(Int43B);
                case 44: return typeof(Int44B);
                case 45: return typeof(Int45B);
                case 46: return typeof(Int46B);
                case 47: return typeof(Int47B);
                case 48: return typeof(Int48B);
                case 49: return typeof(Int49B);
                case 50: return typeof(Int50B);
                case 51: return typeof(Int51B);
                case 52: return typeof(Int52B);
                case 53: return typeof(Int53B);
                case 54: return typeof(Int54B);
                case 55: return typeof(Int55B);
                case 56: return typeof(Int56B);
                case 57: return typeof(Int57B);
                case 58: return typeof(Int58B);
                case 59: return typeof(Int59B);
                case 60: return typeof(Int60B);
                case 61: return typeof(Int61B);
                case 62: return typeof(Int62B);
                case 63: return typeof(Int63B);
                case 64: return typeof(Int64B);

                default:                    
                    throw new ArgumentException(CANAPE.Parser.Properties.Resources.BitField_InvalidSize);                    
            }
        }

        public override CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(GetTypeForBits());
        }

        public override int GetSize()
        {
            return (_bits + 7) / 8;
        }

        public override string TypeName
        {
            get
            {
                return String.Format("Int{0}B", _bits);
            }
        }
    }
}
