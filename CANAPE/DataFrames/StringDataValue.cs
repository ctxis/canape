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
using System.Text;
using CANAPE.Utils;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// A data value which represents a string
    /// </summary>
    [Serializable]
    public class StringDataValue : DataValue
    {
        /// <summary>
        /// Internal value
        /// </summary>
        private string _value;

        /// <summary>
        /// Internal encoding
        /// </summary>
        private Encoding _encoding;

        /// <summary>
        /// Don't store binary encoding in the object, seems kind of pointless
        /// </summary>
        /// <returns></returns>
        private Encoding GetEncoding()
        {
            return _encoding ?? BinaryEncoding.Instance;
        }        

        /// <summary>
        /// Get or set the string encoding
        /// </summary>
        public Encoding Encoding
        {
            get { return GetEncoding(); }

            set
            {
                if (value is BinaryEncoding)
                {
                    _encoding = null;
                }
                else
                {
                    _encoding = value;
                }
            }
        }

        /// <summary>
        /// Get or set encoding as a string
        /// </summary>
        public string EncodingString
        {
            get { return GetEncoding().EncodingName; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }

                if (value.Equals(BinaryEncoding.Instance.EncodingName, StringComparison.OrdinalIgnoreCase))
                {
                    _encoding = null;
                }
                else
                {
                    _encoding = Encoding.GetEncoding(value);
                }
            }
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public override dynamic Value
        {
            get
            {
                return _value;
            }

            set
            {
                SetValue(value.ToString());
            }
        }

        /// <summary>
        /// Set the value
        /// </summary>
        /// <param name="value"></param>
        private void SetValue(string value)
        {
            if (value != _value)
            {
                _value = value;
                OnModified();
            }
        }

        /// <summary>
        /// Get or set the string encoding
        /// </summary>
        public Encoding StringEncoding
        {
            get
            {
                return GetEncoding();
            }

            set
            {
                if (_encoding != value)
                {
                    if (value is BinaryEncoding)
                    {
                        _encoding = null;
                    }
                    else
                    {
                        _encoding = value;
                    }

                    OnModified();
                }
            }
        }

        /// <summary>
        /// Convert string to an array
        /// </summary>
        /// <returns></returns>
        public override byte[] ToArray()
        {
            return GetEncoding().GetBytes(_value);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public override void FromArray(byte[] data)
        {
            SetValue(GetEncoding().GetString(data));            
        }

        /// <summary>
        /// Convert to a data string
        /// </summary>
        /// <returns>The internal string</returns>
        public override string ToDataString()
        {
            return _value;
        }

        /// <summary>
        /// Convert from a data string
        /// </summary>
        /// <param name="str">The string</param>
        public override void FromDataString(string str)
        {
            SetValue(str);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="encoding">The string encoding to use</param>
        public StringDataValue(string name, string value, Encoding encoding) 
            : base(name)
        {
            _value = value;

            if (!(encoding is BinaryEncoding))
            {
                _encoding = encoding;
            }

            Class = new Guid(DataNodeClasses.STRING_NODE_CLASS);
        }

        /// <summary>
        /// Default constructor, uses BinaryEncoding
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public StringDataValue(string name, string value) 
            : this(name, value, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public StringDataValue(string name) 
            : this(name, String.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encoding"></param>
        public StringDataValue(string name, Encoding encoding)
            : this(name, String.Empty, encoding)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _value;
        }

        /// <summary>
        /// Indicates whether this value has a fixed length
        /// </summary>
        public override bool FixedLength
        {
            get { return false; }
        }
    }
}
