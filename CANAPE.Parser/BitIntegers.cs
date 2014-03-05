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
using CANAPE.DataFrames;

namespace CANAPE.Parser
{
    internal interface IBitValue
    {
    }

    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int1B : IConvertible, IFormattable, IComparable, IComparable<Int1B>, IEquatable<Int1B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int1B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int1B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int1B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int1B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int1B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int1B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int1B(int right)
        {
            return new Int1B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int1B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int1B(uint right)
        {
            return new Int1B(right);
        }

        /// <summary>
        /// Conversion operator to Int1B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int1B(long right)
        {
            return new Int1B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int1B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int1B(ulong right)
        {
            return new Int1B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int1B)
            {
                return Equals((Int1B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int1B left, Int1B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int1B left, Int1B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int1B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int1B)
            {
                return CompareTo((Int1B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int1B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int1B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 1, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(1, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int2B : IConvertible, IFormattable, IComparable, IComparable<Int2B>, IEquatable<Int2B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int2B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int2B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int2B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int2B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int2B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int2B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int2B(int right)
        {
            return new Int2B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int2B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int2B(uint right)
        {
            return new Int2B(right);
        }

        /// <summary>
        /// Conversion operator to Int2B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int2B(long right)
        {
            return new Int2B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int2B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int2B(ulong right)
        {
            return new Int2B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int2B)
            {
                return Equals((Int2B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int2B left, Int2B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int2B left, Int2B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int2B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int2B)
            {
                return CompareTo((Int2B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int2B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int2B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 2, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(2, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int3B : IConvertible, IFormattable, IComparable, IComparable<Int3B>, IEquatable<Int3B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int3B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int3B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int3B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int3B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int3B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int3B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int3B(int right)
        {
            return new Int3B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int3B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int3B(uint right)
        {
            return new Int3B(right);
        }

        /// <summary>
        /// Conversion operator to Int3B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int3B(long right)
        {
            return new Int3B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int3B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int3B(ulong right)
        {
            return new Int3B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int3B)
            {
                return Equals((Int3B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int3B left, Int3B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int3B left, Int3B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int3B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int3B)
            {
                return CompareTo((Int3B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int3B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int3B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 3, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(3, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int4B : IConvertible, IFormattable, IComparable, IComparable<Int4B>, IEquatable<Int4B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int4B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int4B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int4B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int4B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int4B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int4B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int4B(int right)
        {
            return new Int4B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int4B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int4B(uint right)
        {
            return new Int4B(right);
        }

        /// <summary>
        /// Conversion operator to Int4B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int4B(long right)
        {
            return new Int4B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int4B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int4B(ulong right)
        {
            return new Int4B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int4B)
            {
                return Equals((Int4B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int4B left, Int4B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int4B left, Int4B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int4B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int4B)
            {
                return CompareTo((Int4B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int4B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int4B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 4, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(4, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int5B : IConvertible, IFormattable, IComparable, IComparable<Int5B>, IEquatable<Int5B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int5B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int5B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int5B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int5B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int5B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int5B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int5B(int right)
        {
            return new Int5B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int5B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int5B(uint right)
        {
            return new Int5B(right);
        }

        /// <summary>
        /// Conversion operator to Int5B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int5B(long right)
        {
            return new Int5B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int5B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int5B(ulong right)
        {
            return new Int5B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int5B)
            {
                return Equals((Int5B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int5B left, Int5B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int5B left, Int5B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int5B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int5B)
            {
                return CompareTo((Int5B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int5B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int5B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 5, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(5, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int6B : IConvertible, IFormattable, IComparable, IComparable<Int6B>, IEquatable<Int6B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int6B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int6B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int6B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int6B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int6B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int6B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int6B(int right)
        {
            return new Int6B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int6B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int6B(uint right)
        {
            return new Int6B(right);
        }

        /// <summary>
        /// Conversion operator to Int6B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int6B(long right)
        {
            return new Int6B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int6B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int6B(ulong right)
        {
            return new Int6B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int6B)
            {
                return Equals((Int6B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int6B left, Int6B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int6B left, Int6B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int6B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int6B)
            {
                return CompareTo((Int6B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int6B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int6B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 6, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(6, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int7B : IConvertible, IFormattable, IComparable, IComparable<Int7B>, IEquatable<Int7B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int7B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int7B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int7B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int7B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int7B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int7B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int7B(int right)
        {
            return new Int7B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int7B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int7B(uint right)
        {
            return new Int7B(right);
        }

        /// <summary>
        /// Conversion operator to Int7B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int7B(long right)
        {
            return new Int7B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int7B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int7B(ulong right)
        {
            return new Int7B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int7B)
            {
                return Equals((Int7B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int7B left, Int7B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int7B left, Int7B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int7B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int7B)
            {
                return CompareTo((Int7B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int7B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int7B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 7, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(7, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int8B : IConvertible, IFormattable, IComparable, IComparable<Int8B>, IEquatable<Int8B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int8B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int8B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int8B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int8B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int8B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int8B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int8B(int right)
        {
            return new Int8B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int8B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int8B(uint right)
        {
            return new Int8B(right);
        }

        /// <summary>
        /// Conversion operator to Int8B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int8B(long right)
        {
            return new Int8B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int8B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int8B(ulong right)
        {
            return new Int8B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int8B)
            {
                return Equals((Int8B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int8B left, Int8B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int8B left, Int8B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int8B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int8B)
            {
                return CompareTo((Int8B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int8B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int8B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 8, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(8, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int9B : IConvertible, IFormattable, IComparable, IComparable<Int9B>, IEquatable<Int9B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int9B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int9B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int9B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int9B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int9B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int9B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int9B(int right)
        {
            return new Int9B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int9B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int9B(uint right)
        {
            return new Int9B(right);
        }

        /// <summary>
        /// Conversion operator to Int9B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int9B(long right)
        {
            return new Int9B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int9B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int9B(ulong right)
        {
            return new Int9B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int9B)
            {
                return Equals((Int9B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int9B left, Int9B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int9B left, Int9B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int9B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int9B)
            {
                return CompareTo((Int9B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int9B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int9B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 9, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(9, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int10B : IConvertible, IFormattable, IComparable, IComparable<Int10B>, IEquatable<Int10B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int10B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int10B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int10B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int10B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int10B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int10B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int10B(int right)
        {
            return new Int10B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int10B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int10B(uint right)
        {
            return new Int10B(right);
        }

        /// <summary>
        /// Conversion operator to Int10B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int10B(long right)
        {
            return new Int10B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int10B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int10B(ulong right)
        {
            return new Int10B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int10B)
            {
                return Equals((Int10B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int10B left, Int10B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int10B left, Int10B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int10B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int10B)
            {
                return CompareTo((Int10B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int10B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int10B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 10, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(10, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int11B : IConvertible, IFormattable, IComparable, IComparable<Int11B>, IEquatable<Int11B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int11B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int11B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int11B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int11B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int11B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int11B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int11B(int right)
        {
            return new Int11B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int11B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int11B(uint right)
        {
            return new Int11B(right);
        }

        /// <summary>
        /// Conversion operator to Int11B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int11B(long right)
        {
            return new Int11B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int11B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int11B(ulong right)
        {
            return new Int11B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int11B)
            {
                return Equals((Int11B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int11B left, Int11B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int11B left, Int11B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int11B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int11B)
            {
                return CompareTo((Int11B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int11B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int11B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 11, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(11, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int12B : IConvertible, IFormattable, IComparable, IComparable<Int12B>, IEquatable<Int12B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int12B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int12B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int12B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int12B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int12B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int12B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int12B(int right)
        {
            return new Int12B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int12B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int12B(uint right)
        {
            return new Int12B(right);
        }

        /// <summary>
        /// Conversion operator to Int12B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int12B(long right)
        {
            return new Int12B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int12B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int12B(ulong right)
        {
            return new Int12B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int12B)
            {
                return Equals((Int12B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int12B left, Int12B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int12B left, Int12B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int12B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int12B)
            {
                return CompareTo((Int12B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int12B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int12B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 12, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(12, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int13B : IConvertible, IFormattable, IComparable, IComparable<Int13B>, IEquatable<Int13B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int13B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int13B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int13B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int13B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int13B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int13B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int13B(int right)
        {
            return new Int13B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int13B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int13B(uint right)
        {
            return new Int13B(right);
        }

        /// <summary>
        /// Conversion operator to Int13B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int13B(long right)
        {
            return new Int13B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int13B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int13B(ulong right)
        {
            return new Int13B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int13B)
            {
                return Equals((Int13B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int13B left, Int13B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int13B left, Int13B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int13B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int13B)
            {
                return CompareTo((Int13B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int13B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int13B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 13, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(13, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int14B : IConvertible, IFormattable, IComparable, IComparable<Int14B>, IEquatable<Int14B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int14B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int14B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int14B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int14B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int14B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int14B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int14B(int right)
        {
            return new Int14B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int14B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int14B(uint right)
        {
            return new Int14B(right);
        }

        /// <summary>
        /// Conversion operator to Int14B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int14B(long right)
        {
            return new Int14B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int14B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int14B(ulong right)
        {
            return new Int14B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int14B)
            {
                return Equals((Int14B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int14B left, Int14B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int14B left, Int14B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int14B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int14B)
            {
                return CompareTo((Int14B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int14B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int14B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 14, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(14, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int15B : IConvertible, IFormattable, IComparable, IComparable<Int15B>, IEquatable<Int15B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int15B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int15B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int15B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int15B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int15B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int15B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int15B(int right)
        {
            return new Int15B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int15B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int15B(uint right)
        {
            return new Int15B(right);
        }

        /// <summary>
        /// Conversion operator to Int15B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int15B(long right)
        {
            return new Int15B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int15B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int15B(ulong right)
        {
            return new Int15B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int15B)
            {
                return Equals((Int15B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int15B left, Int15B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int15B left, Int15B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int15B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int15B)
            {
                return CompareTo((Int15B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int15B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int15B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 15, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(15, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int16B : IConvertible, IFormattable, IComparable, IComparable<Int16B>, IEquatable<Int16B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int16B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int16B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int16B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int16B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int16B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int16B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int16B(int right)
        {
            return new Int16B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int16B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int16B(uint right)
        {
            return new Int16B(right);
        }

        /// <summary>
        /// Conversion operator to Int16B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int16B(long right)
        {
            return new Int16B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int16B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int16B(ulong right)
        {
            return new Int16B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int16B)
            {
                return Equals((Int16B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int16B left, Int16B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int16B left, Int16B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int16B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int16B)
            {
                return CompareTo((Int16B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int16B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int16B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 16, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(16, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int17B : IConvertible, IFormattable, IComparable, IComparable<Int17B>, IEquatable<Int17B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int17B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int17B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int17B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int17B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int17B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int17B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int17B(int right)
        {
            return new Int17B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int17B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int17B(uint right)
        {
            return new Int17B(right);
        }

        /// <summary>
        /// Conversion operator to Int17B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int17B(long right)
        {
            return new Int17B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int17B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int17B(ulong right)
        {
            return new Int17B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int17B)
            {
                return Equals((Int17B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int17B left, Int17B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int17B left, Int17B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int17B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int17B)
            {
                return CompareTo((Int17B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int17B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int17B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 17, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(17, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int18B : IConvertible, IFormattable, IComparable, IComparable<Int18B>, IEquatable<Int18B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int18B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int18B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int18B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int18B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int18B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int18B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int18B(int right)
        {
            return new Int18B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int18B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int18B(uint right)
        {
            return new Int18B(right);
        }

        /// <summary>
        /// Conversion operator to Int18B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int18B(long right)
        {
            return new Int18B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int18B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int18B(ulong right)
        {
            return new Int18B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int18B)
            {
                return Equals((Int18B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int18B left, Int18B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int18B left, Int18B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int18B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int18B)
            {
                return CompareTo((Int18B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int18B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int18B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 18, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(18, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int19B : IConvertible, IFormattable, IComparable, IComparable<Int19B>, IEquatable<Int19B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int19B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int19B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int19B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int19B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int19B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int19B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int19B(int right)
        {
            return new Int19B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int19B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int19B(uint right)
        {
            return new Int19B(right);
        }

        /// <summary>
        /// Conversion operator to Int19B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int19B(long right)
        {
            return new Int19B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int19B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int19B(ulong right)
        {
            return new Int19B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int19B)
            {
                return Equals((Int19B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int19B left, Int19B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int19B left, Int19B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int19B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int19B)
            {
                return CompareTo((Int19B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int19B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int19B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 19, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(19, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int20B : IConvertible, IFormattable, IComparable, IComparable<Int20B>, IEquatable<Int20B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int20B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int20B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int20B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int20B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int20B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int20B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int20B(int right)
        {
            return new Int20B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int20B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int20B(uint right)
        {
            return new Int20B(right);
        }

        /// <summary>
        /// Conversion operator to Int20B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int20B(long right)
        {
            return new Int20B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int20B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int20B(ulong right)
        {
            return new Int20B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int20B)
            {
                return Equals((Int20B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int20B left, Int20B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int20B left, Int20B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int20B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int20B)
            {
                return CompareTo((Int20B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int20B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int20B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 20, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(20, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int21B : IConvertible, IFormattable, IComparable, IComparable<Int21B>, IEquatable<Int21B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int21B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int21B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int21B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int21B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int21B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int21B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int21B(int right)
        {
            return new Int21B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int21B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int21B(uint right)
        {
            return new Int21B(right);
        }

        /// <summary>
        /// Conversion operator to Int21B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int21B(long right)
        {
            return new Int21B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int21B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int21B(ulong right)
        {
            return new Int21B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int21B)
            {
                return Equals((Int21B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int21B left, Int21B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int21B left, Int21B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int21B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int21B)
            {
                return CompareTo((Int21B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int21B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int21B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 21, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(21, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int22B : IConvertible, IFormattable, IComparable, IComparable<Int22B>, IEquatable<Int22B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int22B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int22B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int22B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int22B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int22B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int22B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int22B(int right)
        {
            return new Int22B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int22B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int22B(uint right)
        {
            return new Int22B(right);
        }

        /// <summary>
        /// Conversion operator to Int22B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int22B(long right)
        {
            return new Int22B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int22B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int22B(ulong right)
        {
            return new Int22B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int22B)
            {
                return Equals((Int22B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int22B left, Int22B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int22B left, Int22B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int22B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int22B)
            {
                return CompareTo((Int22B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int22B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int22B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 22, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(22, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int23B : IConvertible, IFormattable, IComparable, IComparable<Int23B>, IEquatable<Int23B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int23B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int23B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int23B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int23B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int23B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int23B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int23B(int right)
        {
            return new Int23B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int23B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int23B(uint right)
        {
            return new Int23B(right);
        }

        /// <summary>
        /// Conversion operator to Int23B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int23B(long right)
        {
            return new Int23B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int23B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int23B(ulong right)
        {
            return new Int23B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int23B)
            {
                return Equals((Int23B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int23B left, Int23B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int23B left, Int23B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int23B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int23B)
            {
                return CompareTo((Int23B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int23B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int23B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 23, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(23, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int24B : IConvertible, IFormattable, IComparable, IComparable<Int24B>, IEquatable<Int24B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int24B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int24B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int24B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int24B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int24B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int24B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int24B(int right)
        {
            return new Int24B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int24B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int24B(uint right)
        {
            return new Int24B(right);
        }

        /// <summary>
        /// Conversion operator to Int24B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int24B(long right)
        {
            return new Int24B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int24B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int24B(ulong right)
        {
            return new Int24B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int24B)
            {
                return Equals((Int24B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int24B left, Int24B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int24B left, Int24B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int24B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int24B)
            {
                return CompareTo((Int24B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int24B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int24B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 24, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(24, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int25B : IConvertible, IFormattable, IComparable, IComparable<Int25B>, IEquatable<Int25B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int25B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int25B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int25B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int25B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int25B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int25B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int25B(int right)
        {
            return new Int25B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int25B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int25B(uint right)
        {
            return new Int25B(right);
        }

        /// <summary>
        /// Conversion operator to Int25B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int25B(long right)
        {
            return new Int25B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int25B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int25B(ulong right)
        {
            return new Int25B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int25B)
            {
                return Equals((Int25B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int25B left, Int25B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int25B left, Int25B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int25B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int25B)
            {
                return CompareTo((Int25B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int25B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int25B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 25, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(25, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int26B : IConvertible, IFormattable, IComparable, IComparable<Int26B>, IEquatable<Int26B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int26B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int26B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int26B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int26B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int26B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int26B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int26B(int right)
        {
            return new Int26B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int26B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int26B(uint right)
        {
            return new Int26B(right);
        }

        /// <summary>
        /// Conversion operator to Int26B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int26B(long right)
        {
            return new Int26B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int26B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int26B(ulong right)
        {
            return new Int26B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int26B)
            {
                return Equals((Int26B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int26B left, Int26B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int26B left, Int26B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int26B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int26B)
            {
                return CompareTo((Int26B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int26B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int26B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 26, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(26, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int27B : IConvertible, IFormattable, IComparable, IComparable<Int27B>, IEquatable<Int27B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int27B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int27B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int27B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int27B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int27B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int27B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int27B(int right)
        {
            return new Int27B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int27B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int27B(uint right)
        {
            return new Int27B(right);
        }

        /// <summary>
        /// Conversion operator to Int27B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int27B(long right)
        {
            return new Int27B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int27B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int27B(ulong right)
        {
            return new Int27B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int27B)
            {
                return Equals((Int27B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int27B left, Int27B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int27B left, Int27B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int27B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int27B)
            {
                return CompareTo((Int27B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int27B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int27B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 27, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(27, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int28B : IConvertible, IFormattable, IComparable, IComparable<Int28B>, IEquatable<Int28B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int28B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int28B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int28B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int28B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int28B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int28B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int28B(int right)
        {
            return new Int28B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int28B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int28B(uint right)
        {
            return new Int28B(right);
        }

        /// <summary>
        /// Conversion operator to Int28B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int28B(long right)
        {
            return new Int28B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int28B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int28B(ulong right)
        {
            return new Int28B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int28B)
            {
                return Equals((Int28B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int28B left, Int28B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int28B left, Int28B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int28B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int28B)
            {
                return CompareTo((Int28B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int28B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int28B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 28, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(28, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int29B : IConvertible, IFormattable, IComparable, IComparable<Int29B>, IEquatable<Int29B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int29B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int29B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int29B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int29B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int29B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int29B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int29B(int right)
        {
            return new Int29B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int29B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int29B(uint right)
        {
            return new Int29B(right);
        }

        /// <summary>
        /// Conversion operator to Int29B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int29B(long right)
        {
            return new Int29B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int29B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int29B(ulong right)
        {
            return new Int29B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int29B)
            {
                return Equals((Int29B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int29B left, Int29B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int29B left, Int29B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int29B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int29B)
            {
                return CompareTo((Int29B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int29B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int29B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 29, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(29, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int30B : IConvertible, IFormattable, IComparable, IComparable<Int30B>, IEquatable<Int30B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int30B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int30B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int30B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int30B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int30B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int30B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int30B(int right)
        {
            return new Int30B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int30B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int30B(uint right)
        {
            return new Int30B(right);
        }

        /// <summary>
        /// Conversion operator to Int30B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int30B(long right)
        {
            return new Int30B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int30B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int30B(ulong right)
        {
            return new Int30B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int30B)
            {
                return Equals((Int30B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int30B left, Int30B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int30B left, Int30B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int30B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int30B)
            {
                return CompareTo((Int30B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int30B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int30B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 30, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(30, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int31B : IConvertible, IFormattable, IComparable, IComparable<Int31B>, IEquatable<Int31B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int31B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int31B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int31B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int31B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int31B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int31B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int31B(int right)
        {
            return new Int31B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int31B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int31B(uint right)
        {
            return new Int31B(right);
        }

        /// <summary>
        /// Conversion operator to Int31B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int31B(long right)
        {
            return new Int31B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int31B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int31B(ulong right)
        {
            return new Int31B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int31B)
            {
                return Equals((Int31B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int31B left, Int31B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int31B left, Int31B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int31B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int31B)
            {
                return CompareTo((Int31B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int31B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int31B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 31, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(31, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int32B : IConvertible, IFormattable, IComparable, IComparable<Int32B>, IEquatable<Int32B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int32B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int32B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int32B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int32B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int32B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int32B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int32B(int right)
        {
            return new Int32B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int32B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int32B(uint right)
        {
            return new Int32B(right);
        }

        /// <summary>
        /// Conversion operator to Int32B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int32B(long right)
        {
            return new Int32B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int32B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int32B(ulong right)
        {
            return new Int32B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int32B)
            {
                return Equals((Int32B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int32B left, Int32B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int32B left, Int32B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int32B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int32B)
            {
                return CompareTo((Int32B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int32B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int32B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 32, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(32, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int33B : IConvertible, IFormattable, IComparable, IComparable<Int33B>, IEquatable<Int33B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int33B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int33B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int33B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int33B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int33B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int33B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int33B(int right)
        {
            return new Int33B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int33B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int33B(uint right)
        {
            return new Int33B(right);
        }

        /// <summary>
        /// Conversion operator to Int33B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int33B(long right)
        {
            return new Int33B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int33B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int33B(ulong right)
        {
            return new Int33B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int33B)
            {
                return Equals((Int33B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int33B left, Int33B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int33B left, Int33B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int33B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int33B)
            {
                return CompareTo((Int33B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int33B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int33B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 33, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(33, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int34B : IConvertible, IFormattable, IComparable, IComparable<Int34B>, IEquatable<Int34B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int34B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int34B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int34B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int34B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int34B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int34B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int34B(int right)
        {
            return new Int34B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int34B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int34B(uint right)
        {
            return new Int34B(right);
        }

        /// <summary>
        /// Conversion operator to Int34B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int34B(long right)
        {
            return new Int34B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int34B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int34B(ulong right)
        {
            return new Int34B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int34B)
            {
                return Equals((Int34B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int34B left, Int34B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int34B left, Int34B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int34B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int34B)
            {
                return CompareTo((Int34B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int34B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int34B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 34, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(34, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int35B : IConvertible, IFormattable, IComparable, IComparable<Int35B>, IEquatable<Int35B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int35B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int35B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int35B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int35B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int35B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int35B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int35B(int right)
        {
            return new Int35B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int35B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int35B(uint right)
        {
            return new Int35B(right);
        }

        /// <summary>
        /// Conversion operator to Int35B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int35B(long right)
        {
            return new Int35B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int35B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int35B(ulong right)
        {
            return new Int35B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int35B)
            {
                return Equals((Int35B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int35B left, Int35B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int35B left, Int35B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int35B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int35B)
            {
                return CompareTo((Int35B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int35B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int35B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 35, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(35, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int36B : IConvertible, IFormattable, IComparable, IComparable<Int36B>, IEquatable<Int36B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int36B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int36B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int36B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int36B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int36B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int36B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int36B(int right)
        {
            return new Int36B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int36B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int36B(uint right)
        {
            return new Int36B(right);
        }

        /// <summary>
        /// Conversion operator to Int36B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int36B(long right)
        {
            return new Int36B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int36B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int36B(ulong right)
        {
            return new Int36B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int36B)
            {
                return Equals((Int36B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int36B left, Int36B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int36B left, Int36B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int36B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int36B)
            {
                return CompareTo((Int36B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int36B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int36B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 36, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(36, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int37B : IConvertible, IFormattable, IComparable, IComparable<Int37B>, IEquatable<Int37B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int37B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int37B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int37B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int37B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int37B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int37B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int37B(int right)
        {
            return new Int37B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int37B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int37B(uint right)
        {
            return new Int37B(right);
        }

        /// <summary>
        /// Conversion operator to Int37B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int37B(long right)
        {
            return new Int37B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int37B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int37B(ulong right)
        {
            return new Int37B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int37B)
            {
                return Equals((Int37B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int37B left, Int37B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int37B left, Int37B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int37B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int37B)
            {
                return CompareTo((Int37B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int37B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int37B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 37, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(37, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int38B : IConvertible, IFormattable, IComparable, IComparable<Int38B>, IEquatable<Int38B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int38B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int38B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int38B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int38B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int38B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int38B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int38B(int right)
        {
            return new Int38B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int38B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int38B(uint right)
        {
            return new Int38B(right);
        }

        /// <summary>
        /// Conversion operator to Int38B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int38B(long right)
        {
            return new Int38B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int38B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int38B(ulong right)
        {
            return new Int38B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int38B)
            {
                return Equals((Int38B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int38B left, Int38B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int38B left, Int38B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int38B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int38B)
            {
                return CompareTo((Int38B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int38B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int38B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 38, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(38, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int39B : IConvertible, IFormattable, IComparable, IComparable<Int39B>, IEquatable<Int39B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int39B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int39B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int39B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int39B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int39B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int39B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int39B(int right)
        {
            return new Int39B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int39B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int39B(uint right)
        {
            return new Int39B(right);
        }

        /// <summary>
        /// Conversion operator to Int39B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int39B(long right)
        {
            return new Int39B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int39B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int39B(ulong right)
        {
            return new Int39B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int39B)
            {
                return Equals((Int39B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int39B left, Int39B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int39B left, Int39B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int39B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int39B)
            {
                return CompareTo((Int39B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int39B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int39B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 39, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(39, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int40B : IConvertible, IFormattable, IComparable, IComparable<Int40B>, IEquatable<Int40B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int40B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int40B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int40B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int40B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int40B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int40B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int40B(int right)
        {
            return new Int40B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int40B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int40B(uint right)
        {
            return new Int40B(right);
        }

        /// <summary>
        /// Conversion operator to Int40B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int40B(long right)
        {
            return new Int40B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int40B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int40B(ulong right)
        {
            return new Int40B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int40B)
            {
                return Equals((Int40B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int40B left, Int40B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int40B left, Int40B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int40B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int40B)
            {
                return CompareTo((Int40B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int40B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int40B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 40, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(40, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int41B : IConvertible, IFormattable, IComparable, IComparable<Int41B>, IEquatable<Int41B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int41B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int41B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int41B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int41B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int41B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int41B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int41B(int right)
        {
            return new Int41B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int41B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int41B(uint right)
        {
            return new Int41B(right);
        }

        /// <summary>
        /// Conversion operator to Int41B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int41B(long right)
        {
            return new Int41B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int41B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int41B(ulong right)
        {
            return new Int41B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int41B)
            {
                return Equals((Int41B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int41B left, Int41B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int41B left, Int41B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int41B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int41B)
            {
                return CompareTo((Int41B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int41B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int41B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 41, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(41, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int42B : IConvertible, IFormattable, IComparable, IComparable<Int42B>, IEquatable<Int42B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int42B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int42B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int42B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int42B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int42B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int42B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int42B(int right)
        {
            return new Int42B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int42B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int42B(uint right)
        {
            return new Int42B(right);
        }

        /// <summary>
        /// Conversion operator to Int42B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int42B(long right)
        {
            return new Int42B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int42B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int42B(ulong right)
        {
            return new Int42B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int42B)
            {
                return Equals((Int42B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int42B left, Int42B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int42B left, Int42B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int42B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int42B)
            {
                return CompareTo((Int42B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int42B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int42B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 42, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(42, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int43B : IConvertible, IFormattable, IComparable, IComparable<Int43B>, IEquatable<Int43B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int43B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int43B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int43B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int43B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int43B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int43B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int43B(int right)
        {
            return new Int43B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int43B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int43B(uint right)
        {
            return new Int43B(right);
        }

        /// <summary>
        /// Conversion operator to Int43B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int43B(long right)
        {
            return new Int43B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int43B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int43B(ulong right)
        {
            return new Int43B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int43B)
            {
                return Equals((Int43B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int43B left, Int43B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int43B left, Int43B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int43B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int43B)
            {
                return CompareTo((Int43B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int43B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int43B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 43, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(43, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int44B : IConvertible, IFormattable, IComparable, IComparable<Int44B>, IEquatable<Int44B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int44B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int44B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int44B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int44B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int44B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int44B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int44B(int right)
        {
            return new Int44B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int44B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int44B(uint right)
        {
            return new Int44B(right);
        }

        /// <summary>
        /// Conversion operator to Int44B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int44B(long right)
        {
            return new Int44B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int44B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int44B(ulong right)
        {
            return new Int44B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int44B)
            {
                return Equals((Int44B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int44B left, Int44B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int44B left, Int44B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int44B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int44B)
            {
                return CompareTo((Int44B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int44B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int44B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 44, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(44, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int45B : IConvertible, IFormattable, IComparable, IComparable<Int45B>, IEquatable<Int45B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int45B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int45B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int45B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int45B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int45B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int45B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int45B(int right)
        {
            return new Int45B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int45B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int45B(uint right)
        {
            return new Int45B(right);
        }

        /// <summary>
        /// Conversion operator to Int45B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int45B(long right)
        {
            return new Int45B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int45B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int45B(ulong right)
        {
            return new Int45B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int45B)
            {
                return Equals((Int45B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int45B left, Int45B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int45B left, Int45B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int45B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int45B)
            {
                return CompareTo((Int45B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int45B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int45B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 45, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(45, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int46B : IConvertible, IFormattable, IComparable, IComparable<Int46B>, IEquatable<Int46B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int46B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int46B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int46B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int46B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int46B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int46B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int46B(int right)
        {
            return new Int46B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int46B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int46B(uint right)
        {
            return new Int46B(right);
        }

        /// <summary>
        /// Conversion operator to Int46B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int46B(long right)
        {
            return new Int46B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int46B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int46B(ulong right)
        {
            return new Int46B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int46B)
            {
                return Equals((Int46B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int46B left, Int46B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int46B left, Int46B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int46B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int46B)
            {
                return CompareTo((Int46B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int46B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int46B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 46, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(46, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int47B : IConvertible, IFormattable, IComparable, IComparable<Int47B>, IEquatable<Int47B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int47B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int47B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int47B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int47B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int47B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int47B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int47B(int right)
        {
            return new Int47B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int47B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int47B(uint right)
        {
            return new Int47B(right);
        }

        /// <summary>
        /// Conversion operator to Int47B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int47B(long right)
        {
            return new Int47B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int47B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int47B(ulong right)
        {
            return new Int47B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int47B)
            {
                return Equals((Int47B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int47B left, Int47B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int47B left, Int47B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int47B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int47B)
            {
                return CompareTo((Int47B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int47B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int47B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 47, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(47, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int48B : IConvertible, IFormattable, IComparable, IComparable<Int48B>, IEquatable<Int48B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int48B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int48B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int48B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int48B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int48B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int48B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int48B(int right)
        {
            return new Int48B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int48B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int48B(uint right)
        {
            return new Int48B(right);
        }

        /// <summary>
        /// Conversion operator to Int48B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int48B(long right)
        {
            return new Int48B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int48B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int48B(ulong right)
        {
            return new Int48B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int48B)
            {
                return Equals((Int48B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int48B left, Int48B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int48B left, Int48B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int48B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int48B)
            {
                return CompareTo((Int48B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int48B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int48B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 48, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(48, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int49B : IConvertible, IFormattable, IComparable, IComparable<Int49B>, IEquatable<Int49B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int49B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int49B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int49B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int49B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int49B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int49B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int49B(int right)
        {
            return new Int49B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int49B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int49B(uint right)
        {
            return new Int49B(right);
        }

        /// <summary>
        /// Conversion operator to Int49B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int49B(long right)
        {
            return new Int49B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int49B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int49B(ulong right)
        {
            return new Int49B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int49B)
            {
                return Equals((Int49B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int49B left, Int49B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int49B left, Int49B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int49B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int49B)
            {
                return CompareTo((Int49B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int49B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int49B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 49, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(49, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int50B : IConvertible, IFormattable, IComparable, IComparable<Int50B>, IEquatable<Int50B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int50B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int50B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int50B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int50B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int50B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int50B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int50B(int right)
        {
            return new Int50B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int50B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int50B(uint right)
        {
            return new Int50B(right);
        }

        /// <summary>
        /// Conversion operator to Int50B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int50B(long right)
        {
            return new Int50B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int50B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int50B(ulong right)
        {
            return new Int50B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int50B)
            {
                return Equals((Int50B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int50B left, Int50B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int50B left, Int50B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int50B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int50B)
            {
                return CompareTo((Int50B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int50B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int50B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 50, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(50, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int51B : IConvertible, IFormattable, IComparable, IComparable<Int51B>, IEquatable<Int51B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int51B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int51B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int51B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int51B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int51B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int51B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int51B(int right)
        {
            return new Int51B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int51B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int51B(uint right)
        {
            return new Int51B(right);
        }

        /// <summary>
        /// Conversion operator to Int51B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int51B(long right)
        {
            return new Int51B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int51B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int51B(ulong right)
        {
            return new Int51B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int51B)
            {
                return Equals((Int51B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int51B left, Int51B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int51B left, Int51B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int51B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int51B)
            {
                return CompareTo((Int51B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int51B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int51B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 51, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(51, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int52B : IConvertible, IFormattable, IComparable, IComparable<Int52B>, IEquatable<Int52B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int52B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int52B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int52B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int52B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int52B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int52B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int52B(int right)
        {
            return new Int52B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int52B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int52B(uint right)
        {
            return new Int52B(right);
        }

        /// <summary>
        /// Conversion operator to Int52B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int52B(long right)
        {
            return new Int52B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int52B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int52B(ulong right)
        {
            return new Int52B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int52B)
            {
                return Equals((Int52B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int52B left, Int52B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int52B left, Int52B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int52B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int52B)
            {
                return CompareTo((Int52B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int52B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int52B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 52, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(52, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int53B : IConvertible, IFormattable, IComparable, IComparable<Int53B>, IEquatable<Int53B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int53B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int53B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int53B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int53B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int53B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int53B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int53B(int right)
        {
            return new Int53B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int53B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int53B(uint right)
        {
            return new Int53B(right);
        }

        /// <summary>
        /// Conversion operator to Int53B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int53B(long right)
        {
            return new Int53B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int53B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int53B(ulong right)
        {
            return new Int53B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int53B)
            {
                return Equals((Int53B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int53B left, Int53B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int53B left, Int53B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int53B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int53B)
            {
                return CompareTo((Int53B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int53B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int53B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 53, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(53, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int54B : IConvertible, IFormattable, IComparable, IComparable<Int54B>, IEquatable<Int54B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int54B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int54B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int54B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int54B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int54B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int54B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int54B(int right)
        {
            return new Int54B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int54B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int54B(uint right)
        {
            return new Int54B(right);
        }

        /// <summary>
        /// Conversion operator to Int54B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int54B(long right)
        {
            return new Int54B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int54B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int54B(ulong right)
        {
            return new Int54B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int54B)
            {
                return Equals((Int54B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int54B left, Int54B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int54B left, Int54B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int54B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int54B)
            {
                return CompareTo((Int54B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int54B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int54B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 54, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(54, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int55B : IConvertible, IFormattable, IComparable, IComparable<Int55B>, IEquatable<Int55B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int55B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int55B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int55B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int55B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int55B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int55B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int55B(int right)
        {
            return new Int55B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int55B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int55B(uint right)
        {
            return new Int55B(right);
        }

        /// <summary>
        /// Conversion operator to Int55B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int55B(long right)
        {
            return new Int55B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int55B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int55B(ulong right)
        {
            return new Int55B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int55B)
            {
                return Equals((Int55B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int55B left, Int55B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int55B left, Int55B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int55B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int55B)
            {
                return CompareTo((Int55B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int55B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int55B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 55, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(55, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int56B : IConvertible, IFormattable, IComparable, IComparable<Int56B>, IEquatable<Int56B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int56B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int56B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int56B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int56B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int56B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int56B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int56B(int right)
        {
            return new Int56B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int56B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int56B(uint right)
        {
            return new Int56B(right);
        }

        /// <summary>
        /// Conversion operator to Int56B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int56B(long right)
        {
            return new Int56B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int56B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int56B(ulong right)
        {
            return new Int56B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int56B)
            {
                return Equals((Int56B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int56B left, Int56B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int56B left, Int56B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int56B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int56B)
            {
                return CompareTo((Int56B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int56B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int56B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 56, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(56, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int57B : IConvertible, IFormattable, IComparable, IComparable<Int57B>, IEquatable<Int57B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int57B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int57B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int57B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int57B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int57B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int57B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int57B(int right)
        {
            return new Int57B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int57B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int57B(uint right)
        {
            return new Int57B(right);
        }

        /// <summary>
        /// Conversion operator to Int57B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int57B(long right)
        {
            return new Int57B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int57B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int57B(ulong right)
        {
            return new Int57B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int57B)
            {
                return Equals((Int57B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int57B left, Int57B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int57B left, Int57B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int57B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int57B)
            {
                return CompareTo((Int57B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int57B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int57B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 57, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(57, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int58B : IConvertible, IFormattable, IComparable, IComparable<Int58B>, IEquatable<Int58B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int58B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int58B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int58B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int58B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int58B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int58B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int58B(int right)
        {
            return new Int58B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int58B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int58B(uint right)
        {
            return new Int58B(right);
        }

        /// <summary>
        /// Conversion operator to Int58B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int58B(long right)
        {
            return new Int58B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int58B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int58B(ulong right)
        {
            return new Int58B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int58B)
            {
                return Equals((Int58B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int58B left, Int58B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int58B left, Int58B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int58B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int58B)
            {
                return CompareTo((Int58B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int58B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int58B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 58, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(58, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int59B : IConvertible, IFormattable, IComparable, IComparable<Int59B>, IEquatable<Int59B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int59B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int59B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int59B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int59B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int59B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int59B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int59B(int right)
        {
            return new Int59B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int59B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int59B(uint right)
        {
            return new Int59B(right);
        }

        /// <summary>
        /// Conversion operator to Int59B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int59B(long right)
        {
            return new Int59B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int59B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int59B(ulong right)
        {
            return new Int59B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int59B)
            {
                return Equals((Int59B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int59B left, Int59B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int59B left, Int59B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int59B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int59B)
            {
                return CompareTo((Int59B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int59B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int59B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 59, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(59, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int60B : IConvertible, IFormattable, IComparable, IComparable<Int60B>, IEquatable<Int60B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int60B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int60B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int60B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int60B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int60B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int60B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int60B(int right)
        {
            return new Int60B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int60B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int60B(uint right)
        {
            return new Int60B(right);
        }

        /// <summary>
        /// Conversion operator to Int60B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int60B(long right)
        {
            return new Int60B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int60B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int60B(ulong right)
        {
            return new Int60B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int60B)
            {
                return Equals((Int60B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int60B left, Int60B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int60B left, Int60B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int60B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int60B)
            {
                return CompareTo((Int60B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int60B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int60B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 60, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(60, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int61B : IConvertible, IFormattable, IComparable, IComparable<Int61B>, IEquatable<Int61B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int61B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int61B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int61B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int61B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int61B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int61B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int61B(int right)
        {
            return new Int61B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int61B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int61B(uint right)
        {
            return new Int61B(right);
        }

        /// <summary>
        /// Conversion operator to Int61B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int61B(long right)
        {
            return new Int61B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int61B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int61B(ulong right)
        {
            return new Int61B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int61B)
            {
                return Equals((Int61B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int61B left, Int61B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int61B left, Int61B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int61B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int61B)
            {
                return CompareTo((Int61B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int61B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int61B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 61, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(61, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int62B : IConvertible, IFormattable, IComparable, IComparable<Int62B>, IEquatable<Int62B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int62B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int62B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int62B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int62B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int62B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int62B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int62B(int right)
        {
            return new Int62B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int62B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int62B(uint right)
        {
            return new Int62B(right);
        }

        /// <summary>
        /// Conversion operator to Int62B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int62B(long right)
        {
            return new Int62B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int62B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int62B(ulong right)
        {
            return new Int62B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int62B)
            {
                return Equals((Int62B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int62B left, Int62B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int62B left, Int62B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int62B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int62B)
            {
                return CompareTo((Int62B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int62B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int62B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 62, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(62, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int63B : IConvertible, IFormattable, IComparable, IComparable<Int63B>, IEquatable<Int63B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int63B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int63B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int63B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int63B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int63B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int63B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int63B(int right)
        {
            return new Int63B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int63B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int63B(uint right)
        {
            return new Int63B(right);
        }

        /// <summary>
        /// Conversion operator to Int63B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int63B(long right)
        {
            return new Int63B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int63B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int63B(ulong right)
        {
            return new Int63B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int63B)
            {
                return Equals((Int63B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int63B left, Int63B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int63B left, Int63B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int63B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int63B)
            {
                return CompareTo((Int63B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int63B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int63B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 63, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(63, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
    /// <summary>
    /// Dummy structure to represent a bit field integer
    /// </summary>
    [Serializable]
    public struct Int64B : IConvertible, IFormattable, IComparable, IComparable<Int64B>, IEquatable<Int64B>, IPrimitiveValue, IBitValue
    {
        ulong value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The integer value</param>
        public Int64B(ulong value)
        {
            this.value = value;
        }

        /// <summary>
        /// Conversion operator to int
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator int(Int64B right)
        {
            return (int)right.value;
        }

        /// <summary>
        /// Conversion operator to uint
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator uint(Int64B right)
        {
            return (uint)right.value;
        }

        /// <summary>
        /// Conversion operator to long
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator long(Int64B right)
        {
            return (long)right.value;
        }

        /// <summary>
        /// Conversion operator to ulong
        /// </summary>
        /// <param name="right">The structure</param>
        /// <returns>The value</returns>
        public static explicit operator ulong(Int64B right)
        {
            return right.value;
        }

        /// <summary>
        /// Conversion operator to Int64B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int64B(int right)
        {
            return new Int64B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int64B
        /// </summary>
        /// <param name="right">The integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int64B(uint right)
        {
            return new Int64B(right);
        }

        /// <summary>
        /// Conversion operator to Int64B
        /// </summary>
        /// <param name="right">The long integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int64B(long right)
        {
            return new Int64B((ulong)right);
        }

        /// <summary>
        /// Conversion operator to Int64B
        /// </summary>
        /// <param name="right">The ulong integer</param>
        /// <returns>The structure</returns>
        public static explicit operator Int64B(ulong right)
        {
            return new Int64B(right);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible conv = (IConvertible)value;

            return conv.ToUInt64(provider);
        }

        /// <summary>
        /// Convert to a string 
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a format
        /// </summary>
        /// <param name="s">The format string</param>
        /// <returns>The string</returns>
        public string ToString(string s)
        {
            return ToString(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convert to a string using a provider
        /// </summary>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(IFormatProvider provider)
        {
            return value.ToString(provider);
        }

        /// <summary>
        /// Convert to a string using a format and provider
        /// </summary>
        /// <param name="s">The format string</param>
        /// <param name="provider">The provider</param>
        /// <returns>The string</returns>
        public string ToString(string s, IFormatProvider provider)
        {
            return value.ToString(s, provider);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <returns>True if the object is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Int64B)
            {
                return Equals((Int64B)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is equal</returns>
        public static bool operator ==(Int64B left, Int64B right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Not equal operator
        /// </summary>
        /// <param name="left">Left hand object</param>
        /// <param name="right">Right hand object</param>
        /// <returns>True if object is not equal</returns>
        public static bool operator !=(Int64B left, Int64B right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater or null</returns>
        /// <exception cref="ArgumentException">Thrown if obj is not an Int64B</exception>
        public int CompareTo(object obj)
        {
            if (obj is Int64B)
            {
                return CompareTo((Int64B)obj);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException(String.Format(Properties.Resources.IntB_InvalidCompareObject, GetType().Name), "obj");
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>less than 0 if less than, 0 if equal, greater if greater</returns>
        public int CompareTo(Int64B other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Test for equality
        /// </summary>
        /// <param name="other">The other value</param>
        /// <returns>True if equal</returns>
        public bool Equals(Int64B other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Write the object to a stream
        /// </summary>
        /// <param name="writer">The writer to write the stream to</param>
        /// <param name="littleEndian">Whether the value should be little or big endian</param>
        public void ToWriter(DataWriter writer, bool littleEndian)
        {
            writer.WriteBits(value, 64, littleEndian);
        }

        /// <summary>
        /// Read the object from a stream
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="littleEndian">Whether the value shouldbe little or big endian</param>
        public void FromReader(DataReader reader, bool littleEndian)
        {
            this.value = reader.ReadBits(64, littleEndian);
        }

        /// <summary>
        /// Get or set the value
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Convert.ToUInt64(value);
            }
        }
    }
}
