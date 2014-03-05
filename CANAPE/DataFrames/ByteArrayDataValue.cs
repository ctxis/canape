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
using CANAPE.Utils;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// Data value to hold a byte array
    /// </summary>
    [Serializable]
    public class ByteArrayDataValue : DataValue
    {
        private byte[] _value;

        /// <summary>
        /// Get the value object
        /// </summary>
        public override object Value
        {
            get
            {
                return _value;
            }
            set
            {
                byte[] data = value as byte[];

                if (data != null)
                {
                    _value = (byte[])data.Clone();
                }
                else
                {
                    _value = new byte[0];
                }

                OnModified();
            }
        }

        /// <summary>
        /// Convert to an array
        /// </summary>
        /// <returns></returns>
        public override byte[] ToArray()
        {
            if (_value != null)
            {
                return (byte[])_value.Clone();
            }
            else
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// Convert from an array
        /// </summary>
        /// <param name="data"></param>
        public override void FromArray(byte[] data)
        {
            _value = (byte[])data.Clone();
            OnModified();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the value node</param>
        /// <param name="data">Data</param>
        public ByteArrayDataValue(string name, byte[] data)
            : base(name)
        {
            if (data == null)
            {
                _value = new byte[0];
            }
            else
            {
                _value = data;
            }

            Class = new Guid(DataNodeClasses.BINARY_NODE_CLASS);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the value node</param>
        public ByteArrayDataValue(string name)
            : this(name, new byte[0])
        {
        }

        /// <summary>
        /// Return a display string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            int length = _value.Length;
            if(length > 64)
            {
                length = 64;
            }

            return GeneralUtils.EscapeString(BinaryEncoding.Instance.GetString(_value, 0, length));
        }
        
        /// <summary>
        /// Indicates whether this value is a fixed length, returns false
        /// </summary>
        public override bool FixedLength
        {
            get { return false; }
        }

    }
}
