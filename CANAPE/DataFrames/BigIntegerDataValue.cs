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
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// Data value which wraps a BigInteger structure
    /// </summary>
    [Serializable]
    public class BigIntegerDataValue : PrimitiveDataValue
    {
        private BigInteger _int;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of value</param>
        public BigIntegerDataValue(string name) 
            : this(name, BigInteger.Zero, true)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of value</param>
        /// <param name="i">The initial integer</param>
        /// <param name="littleEndian">True for little endian</param>
        public BigIntegerDataValue(string name, BigInteger i, bool littleEndian) 
            : base(name, littleEndian)
        {
            _int = i;            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of value</param>
        /// <param name="i">The initial integer</param>
        public BigIntegerDataValue(string name, BigInteger i) 
            : this(name, i, true)            
        {            
        }

        /// <summary>
        /// Convert to an array
        /// </summary>
        /// <returns>The result</returns>
        protected override byte[] OnToArray()
        {
            if (!LittleEndian)
            {
                return _int.ToByteArray().Reverse().ToArray();
            }
            else
            {
                return _int.ToByteArray();
            }
        }

        /// <summary>
        /// Convert from an array
        /// </summary>
        /// <param name="data">The data</param>
        protected override void OnFromArray(byte[] data)
        {
            if (!LittleEndian)
            {
                _int = new BigInteger(data.Reverse().ToArray());
            }
            else
            {
                _int = new BigInteger(data);
            }
        }

        /// <summary>
        /// Get or set the BigInteger value
        /// </summary>
        public override dynamic Value
        {
            get
            {
                return _int;
            }

            set
            {
                if (value is BigInteger)
                {
                    _int = (BigInteger)value;
                }
                else if (value is string)
                {
                    string s = (string)value;

                    if (s.StartsWith("0x"))
                    {
                        _int = BigInteger.Parse(s.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        _int = BigInteger.Parse(s, CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    _int = Activator.CreateInstance(typeof(BigInteger), value);
                }
                OnModified();
            }
        }

        /// <summary>
        /// Fixed length, always false
        /// </summary>
        public override bool FixedLength
        {
            get { return false; }
        }

        /// <summary>
        /// Convert integer to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(FormatString))
            {
                return _int.ToString();
            }
            else
            {
                return _int.ToString(FormatString);                
            }            
        }
    }
}
