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
using System.IO;
using System.Text;
using CANAPE.Utils;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// Class to write output to a data stream
    /// </summary>
    public sealed class DataWriter
    {
        CountedStream _stm;
        byte _currBits;
        int _validBits;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="stm"></param>
        public DataWriter(Stream stm)
        {
            _stm = new CountedStream(stm);
        }

        /// <summary>
        /// Constructor, creates a writer with an inbuilt memorystream
        /// </summary>
        public DataWriter()
            : this(new MemoryStream())
        {
        }

        /// <summary>
        /// Write an array to stream
        /// </summary>
        /// <param name="arr">Array of bytes to write</param>
        public void Write(byte[] arr)
        {
            Write(arr, 0, arr.Length);
        }

        /// <summary>
        /// Write an array of signed bytes
        /// </summary>
        /// <param name="arr">The array to write</param>
        public void Write(sbyte[] arr)
        {
            Write((byte[])(Array)arr);
        }

        /// <summary>
        /// Write an array to stream
        /// </summary>
        /// <param name="arr">Array of bytes to write</param>
        /// <param name="pos">Position in the array to start write</param>
        /// <param name="length">Length of write</param>
        public void Write(byte[] arr, int pos, int length)
        {
            // We allow 0 byte writes to flush the stream
            Flush();

            if ((length - pos) > 0)
            {
                _stm.Write(arr, pos, length);
            }
        }

        /// <summary>
        /// Write a byte to the stream
        /// </summary>
        /// <param name="b">The byte to write</param>
        public void WriteByte(byte b)
        {
            Flush();
            _stm.WriteByte(b);
        }

        /// <summary>
        /// Write a signed byte to the stream
        /// </summary>
        /// <param name="sb">The signed byte to write</param>
        public void WriteSByte(sbyte sb)
        {            
            sbyte[] sbs = new sbyte[] { sb };

            Flush();
            _stm.WriteByte(((byte[])(Array)sbs)[0]);
        }

        /// <summary>
        /// Write a byte to the stream
        /// </summary>
        /// <param name="b">The byte to write</param>
        public void Write(byte b)
        {
            WriteByte(b);
        }

        /// <summary>
        /// Write a signed byte to the stream
        /// </summary>
        /// <param name="b">The byte to write</param>
        public void Write(sbyte b)
        {
            WriteSByte(b);
        }

        /// <summary>
        /// Write a short to the stream
        /// </summary>
        /// <param name="s">The short to write</param>
        /// <param name="littleEndian">True to write in little endian, otherwise big endian</param>
        public void Write(short s, bool littleEndian)
        {
            WriteInt16(s, littleEndian);
        }

        /// <summary>
        /// Write an unsigned  short to the stream
        /// </summary>
        /// <param name="s">The short to write</param>
        /// <param name="littleEndian">True to write in little endian, otherwise big endian</param>
        public void Write(ushort s, bool littleEndian)
        {
            WriteUInt16(s, littleEndian);
        }


        /// <summary>
        /// Write an int to the stream
        /// </summary>
        /// <param name="i">The int to write</param>
        /// <param name="littleEndian">True to write in little endian, otherwise big endian</param>
        public void Write(int i, bool littleEndian)
        {
            WriteInt32(i, littleEndian);
        }

        /// <summary>
        /// Write an unsigned int to the stream
        /// </summary>
        /// <param name="i">The int to write</param>
        /// <param name="littleEndian">True to write in little endian, otherwise big endian</param>
        public void Write(uint i, bool littleEndian)
        {
            WriteUInt32(i, littleEndian);
        }

        /// <summary>
        /// Write a long to the stream
        /// </summary>
        /// <param name="l">The long to write</param>
        /// <param name="littleEndian">True to write in little endian, otherwise big endian</param>
        public void Write(long l, bool littleEndian)
        {
            WriteInt64(l, littleEndian);
        }

        /// <summary>
        /// Write an unsigned long to the stream
        /// </summary>
        /// <param name="l">The long to write</param>
        /// <param name="littleEndian">True to write in little endian, otherwise big endian</param>
        public void Write(ulong l, bool littleEndian)
        {
            WriteUInt64(l, littleEndian);
        }

        /// <summary>
        /// Write a float with a specified endian
        /// </summary>
        /// <param name="f">The float to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void Write(float f, bool littleEndian)
        {
            WriteFloat(f, littleEndian);
        }

        /// <summary>
        /// Write a double with a specified endian
        /// </summary>
        /// <param name="d">The double to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void Write(double d, bool littleEndian)
        {
            WriteDouble(d, littleEndian);
        }

        /// <summary>
        /// Write a 24 bit integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void Write(Int24 i, bool littleEndian)
        {
            WriteInt24(i, littleEndian);
        }

        /// <summary>
        /// Write a 24bit unsigned integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void Write(UInt24 i, bool littleEndian)
        {
            WriteUInt24(i, littleEndian);
        }

        /// <summary>
        /// Write a 16bit integer to the stream
        /// </summary>
        /// <param name="s">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteInt16(short s, bool littleEndian)
        {
            Write(GeneralUtils.SwapBytes(BitConverter.GetBytes(s), littleEndian));
        }

        /// <summary>
        /// Write a little endian 16bit integer to the stream
        /// </summary>
        /// <param name="s">The integer to write</param>
        public void WriteInt16(short s)
        {
            WriteInt16(s, true);
        }
        
        /// <summary>
        /// Write a 32bit integer to the stream
        /// </summary>
        /// <param name="i">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteInt32(int i, bool littleEndian)
        {
            Write(GeneralUtils.SwapBytes(BitConverter.GetBytes(i), littleEndian));
        }

        /// <summary>
        /// Write a little endian 32bit integer to the stream
        /// </summary>
        /// <param name="i">The integer to write</param>
        public void WriteInt32(int i)
        {
            WriteInt32(i, true);
        }

        /// <summary>
        /// Write a 64bit integer to the stream
        /// </summary>
        /// <param name="l">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteInt64(long l, bool littleEndian)
        {
            Write(GeneralUtils.SwapBytes(BitConverter.GetBytes(l), littleEndian));
        }

        /// <summary>
        /// Write a little endian 64bit integer to the stream
        /// </summary>
        /// <param name="l">The integer to write</param>
        public void WriteInt64(long l)
        {
            WriteInt64(l, true);
        }

        /// <summary>
        /// Write a 16bit integer to the stream
        /// </summary>
        /// <param name="s">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteUInt16(ushort s, bool littleEndian)
        {
            Write(GeneralUtils.SwapBytes(BitConverter.GetBytes(s), littleEndian));
        }

        /// <summary>
        /// Write a little endian 16bit integer to the stream
        /// </summary>
        /// <param name="s">The integer to write</param>
        public void WriteUInt16(ushort s)
        {
            WriteUInt16(s, true);
        }

        /// <summary>
        /// Write an unsigned 32bit integer with a specified endian
        /// </summary>
        /// <param name="i">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteUInt32(uint i, bool littleEndian)
        {
            Write(GeneralUtils.SwapBytes(BitConverter.GetBytes(i), littleEndian));
        }

        /// <summary>
        /// Write a little endian unsigned 32bit integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        public void WriteUInt32(uint i)
        {
            WriteUInt32(i, true);
        }

        /// <summary>
        /// Write an unsigned 64bit integer with a specified endian
        /// </summary>
        /// <param name="l">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteUInt64(ulong l, bool littleEndian)
        {
            Write(GeneralUtils.SwapBytes(BitConverter.GetBytes(l), littleEndian));
        }

        /// <summary>
        /// Write a little endian unsigned 64bit integer
        /// </summary>
        /// <param name="l">The integer to write</param>
        public void WriteUInt64(ulong l)
        {
            WriteUInt64(l, true);
        }

        /// <summary>
        /// Write a 24 bit integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteInt24(Int24 i, bool littleEndian)
        {
            byte[] data = GeneralUtils.SwapBytes(BitConverter.GetBytes(i), littleEndian);

            if (littleEndian)
            {
                Write(data, 0, 3);
            }
            else
            {
                Write(data, 1, 3);
            }                
        }

        /// <summary>
        /// Write a 24bit unsigned integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteUInt24(UInt24 i, bool littleEndian)
        {
            byte[] data = GeneralUtils.SwapBytes(BitConverter.GetBytes(i), littleEndian);

            if (littleEndian)
            {
                Write(data, 0, 3);
            }
            else
            {
                Write(data, 1, 3);
            }  
        }

        /// <summary>
        /// Write a little endian 24bit integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        public void WriteInt24(Int24 i)
        {
            WriteInt24(i, true);
        }

        /// <summary>
        /// Write a little endian 24bit unsigned integer
        /// </summary>
        /// <param name="i">The integer to write</param>        
        public void WriteUInt24(UInt24 i)
        {
            WriteUInt24(i, true);
        }

        /// <summary>
        /// Write a 24 bit integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteInt24(int i, bool littleEndian)
        {
            WriteInt24((Int24)i, littleEndian);
        }

        /// <summary>
        /// Write a 24bit unsigned integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteUInt24(uint i, bool littleEndian)
        {
            WriteUInt24((UInt24)i, littleEndian);
        }

        /// <summary>
        /// Write a little endian 24bit integer
        /// </summary>
        /// <param name="i">The integer to write</param>
        public void WriteInt24(int i)
        {
            WriteInt24(i, true);
        }

        /// <summary>
        /// Write a little endian 24bit unsigned integer
        /// </summary>
        /// <param name="i">The integer to write</param>        
        public void WriteUInt24(uint i)
        {
            WriteUInt24(i, true);
        }

        /// <summary>
        /// Write a little endian float
        /// </summary>
        /// <param name="f">The float to write</param>    
        public void WriteFloat(float f)
        {
            WriteFloat(f, true);
        }

        /// <summary>
        /// Write a float with a specified endian
        /// </summary>
        /// <param name="f">The float to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteFloat(float f, bool littleEndian)
        {
            Write(GeneralUtils.SwapBytes(BitConverter.GetBytes(f), littleEndian));
        }

        /// <summary>
        /// Write a little endian double
        /// </summary>
        /// <param name="d">The double to write</param>    
        public void WriteDouble(double d)
        {
            WriteDouble(d, true);
        }

        /// <summary>
        /// Write a double with a specified endian
        /// </summary>
        /// <param name="d">The double to write</param>
        /// <param name="littleEndian">True for little endian, otherwise big endian</param>
        public void WriteDouble(double d, bool littleEndian)
        {
            Write(GeneralUtils.SwapBytes(BitConverter.GetBytes(d), littleEndian));
        }

        /// <summary>
        /// Write a string with a specific encoding
        /// </summary>
        /// <param name="str">The string to write</param>
        /// <param name="encoding">The encoding to use</param>
        public void Write(string str, Encoding encoding)
        {
            Write(encoding.GetBytes(str));
        }

        /// <summary>
        /// Write a string with a specific encoding
        /// </summary>
        /// <param name="str">The string to write</param>
        /// <param name="encoding">The encoding to use</param>
        public void Write(string str, BinaryStringEncoding encoding)
        {
            Write(str, GeneralUtils.GetEncodingFromType(encoding));
        }

        /// <summary>
        /// Write string to stream with a binary encoding
        /// </summary>
        /// <param name="str">The binary encoded string</param>
        public void Write(string str)
        {            
            Write(str, BinaryEncoding.Instance);
        }

        /// <summary>
        /// Write a character with binary encoding
        /// </summary>
        /// <param name="ch">The character</param>
        /// <remarks>Will remove the top 8 bits from the character</remarks>
        public void WriteChar(char ch)
        {
            Write((byte)ch);
        }

        /// <summary>
        /// Write a character with a specified encoding
        /// </summary>
        /// <param name="ch">The character</param>
        /// <param name="encoding">The encoding</param>
        public void WriteChar(char ch, Encoding encoding)
        {
            Write(encoding.GetBytes(new char[] { ch }));
        }

        /// <summary>
        /// Write a character with a specified encoding
        /// </summary>
        /// <param name="ch">The character</param>
        /// <param name="encoding">The encoding</param>
        public void WriteChar(char ch, BinaryStringEncoding encoding)
        {
            WriteChar(ch, GeneralUtils.GetEncodingFromType(encoding));
        }

        /// <summary>
        /// Write a line to the stream using a specified encoding and line ending
        /// </summary>
        /// <param name="line">The line to write</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="lineEnding">The type of line ending</param>
        public void WriteLine(string line, Encoding encoding, TextLineEnding lineEnding)
        {
            string lineEndingString = "\n";

            switch (lineEnding)
            {
                case TextLineEnding.LineFeed:
                    lineEndingString = "\n";
                    break;
                case TextLineEnding.CarriageReturn:
                    lineEndingString = "\r";
                    break;
                case TextLineEnding.CarriageReturnLineFeed:
                    lineEndingString = "\r\n";
                    break; 
            }

            Write(line, encoding);
            Write(lineEndingString, encoding);
        }

        /// <summary>
        /// Write a line to the stream using binary encoding and LF line ending
        /// </summary>
        /// <param name="line">The line to write</param>
        public void WriteLine(string line)
        {
            WriteLine(line, new BinaryEncoding(), TextLineEnding.LineFeed);
        }

        /// <summary>
        /// Write a line to the stream using a specified encoding and LF line ending
        /// </summary>
        /// <param name="line">The line to write</param>
        /// <param name="encoding">The encoding to use</param>
        public void WriteLine(string line, Encoding encoding)
        {
            WriteLine(line, encoding, TextLineEnding.LineFeed);
        }

        /// <summary>
        /// Write a line to the stream using a specified encoding and LF line ending
        /// </summary>
        /// <param name="line">The line to write</param>
        /// <param name="encoding">The encoding to use</param>
        public void WriteLine(string line, BinaryStringEncoding encoding)
        {
            WriteLine(line, GeneralUtils.GetEncodingFromType(encoding));
        }

        /// <summary>
        /// Write a line to the stream using a binary encoding and a specified line ending
        /// </summary>
        /// <param name="line">The line to write</param>
        /// <param name="lineEnding">The type of line ending</param>
        public void WriteLine(string line, TextLineEnding lineEnding)
        {
            WriteLine(line, BinaryEncoding.Instance, lineEnding);
        }

        /// <summary>
        /// Write a 7 bit integer to the stream
        /// </summary>
        /// <param name="i">The integer to write</param>
        public void Write7BitInt(ulong i)
        {
            do
            {
                byte nextLength = (byte)(i & 0x7F);
                i >>= 7;

                if (i != 0)
                {
                    nextLength |= 0x80;
                }

                Write(nextLength);
            }
            while (i != 0);
        }

        /// <summary>
        /// Write a bit field to the stream
        /// </summary>
        /// <param name="val">The value containing the bits</param>
        /// <param name="count">The number of bits to write from 0 to 64</param>
        /// <param name="littleEndian">True to write in little endian, otherwise big endian</param>
        /// <remarks>When you have finished writing bits you should either write another value, 
        /// write multiples of 8 bits or call Flush to ensure any remaining bits are written to the stream</remarks>
        public void WriteBits(ulong val, int count, bool littleEndian)
        {
            if ((count < 0) || (count > 64))
            {
                throw new ArgumentException(CANAPE.Properties.Resources.DataWriter_InvalidBitCount, "count");
            }

            if (littleEndian)
            {
                for (int i = 0; i < count; ++i)
                {
                    _currBits |= (byte)((val & 1) << _validBits);

                    val >>= 1;

                    _validBits++;

                    if (_validBits == 8)
                    {
                        _stm.WriteByte(_currBits);
                        _currBits = 0;
                        _validBits = 0;
                    }
                }
            }
            else
            {
                for (int i = count; i > 0; --i)
                {
                    if ((val & ((ulong)1 << (i - 1))) != 0)
                    {
                        _currBits |= (byte)(1 << (7 - _validBits));
                    }
                    _validBits++;

                    if (_validBits == 8)
                    {
                        _stm.WriteByte(_currBits);
                        _currBits = 0;
                        _validBits = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Write a little endian bit field to the stream
        /// </summary>
        /// <param name="val">The value containing the bits</param>
        /// <param name="count">The number of bits to write from 0 to 64</param>
        /// <remarks>When you have finished writing bits you should either write another value, 
        /// write multiples of 8 bits or call FlushBits to ensure any remaining bits are written to the stream</remarks>
        public void WriteBits(ulong val, int count)
        {
            WriteBits(val, count, true);
        }

        /// <summary>
        /// Write a terminated string to the stream
        /// </summary>
        /// <param name="str">The string to write</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="terminator">The terminator</param>
        public void WriteTerminatedString(string str, Encoding encoding, char terminator)
        {
            Write(str, encoding);
            WriteChar(terminator, encoding);
        }

        /// <summary>
        /// Write a terminated string to the stream
        /// </summary>
        /// <param name="str">The string to write</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="terminator">The terminator</param>
        public void WriteTerminatedString(string str, BinaryStringEncoding encoding, char terminator)
        {
            WriteTerminatedString(str, GeneralUtils.GetEncodingFromType(encoding), terminator);
        }

        /// <summary>
        /// Write a binary encoded terminated string to the stream
        /// </summary>
        /// <param name="str">The string to write</param>
        /// <param name="terminator">The terminator</param>
        public void WriteTerminatedString(string str, char terminator)
        {
            WriteTerminatedString(str, BinaryEncoding.Instance, terminator);
        }

        /// <summary>
        /// Write a NUL terminated string to the stream with a specified encoding
        /// </summary>
        /// <param name="str">The string to write</param>
        /// <param name="encoding">The encoding to use</param>
        public void WriteNulTerminatedString(string str, Encoding encoding)
        {
            WriteTerminatedString(str, encoding, '\0');
        }

        /// <summary>
        /// Write a NUL terminated string to the stream with a specified encoding
        /// </summary>
        /// <param name="str">The string to write</param>
        /// <param name="encoding">The encoding to use</param>
        public void WriteNulTerminatedString(string str, BinaryStringEncoding encoding)
        {
            WriteNulTerminatedString(str, GeneralUtils.GetEncodingFromType(encoding));
        }

        /// <summary>
        /// Write a NUL terminated string to the stream with a binary encoding
        /// </summary>
        /// <param name="str">The string to write</param>
        public void WriteNulTerminatedString(string str)
        {
            WriteTerminatedString(str, BinaryEncoding.Instance, '\0');
        }

        /// <summary>
        /// Write a primitive value
        /// </summary>
        /// <param name="ser">The primitive value</param>
        /// <param name="littleEndian">True for little endian</param>
        public void Write(IPrimitiveValue ser, bool littleEndian)
        {
            ser.ToWriter(this, littleEndian);
        }

        /// <summary>
        /// Write a primitive value in little endian
        /// </summary>
        /// <param name="ser">The primitive value</param>        
        public void Write(IPrimitiveValue ser)
        {
            ser.ToWriter(this, true);
        }

        /// <summary>
        /// Method to flush any pending data (only used for bit values atm)
        /// </summary>
        public void Flush()
        {
            if (_validBits > 0)
            {
                _stm.WriteByte(_currBits);
                _validBits = 0;
                _currBits = 0;
            }
        }

        /// <summary>
        /// Get the underlying stream object
        /// </summary>
        /// <returns>The stream object</returns>
        public Stream GetStream()
        {
            // Flush bits, we cannot tell for certain whether this will do harm
            Flush();
            return _stm;
        }

        /// <summary>
        /// Write a primitive type
        /// </summary>        
        /// <typeparam name="T">The primitive type to write</typeparam>   
        /// <param name="value">The value to write</param>
        /// <param name="littleEndian">Whether should write in little endian (if applicable)</param>
        /// <exception cref="ArgumentException">Throw if cannot determine type to write</exception>    
        public void WritePrimitive<T>(T value, bool littleEndian) where T : struct
        {
            WritePrimitive(value, typeof(T), littleEndian);
        }

        /// <summary>
        /// Write a primitive type
        /// </summary>        
        /// <param name="value">The value to write</param>
        /// <param name="t">The type of valie</param>
        /// <param name="littleEndian">Whether should write in little endian (if applicable)</param>
        /// <exception cref="ArgumentException">Throw if cannot determine type to write</exception>        
        public void WritePrimitive(object value, Type t, bool littleEndian)
        {
            if (t == typeof(byte))
            {
                WriteByte((byte)value);
            }
            else if (t == typeof(sbyte))
            {
                WriteSByte((sbyte)value);                
            }
            else if (t == typeof(short))
            {
                WriteInt16((short)value, littleEndian);                
            }
            else if (t == typeof(ushort))
            {
                WriteUInt16((ushort)value, littleEndian);                
            }
            else if (t == typeof(int))
            {
                WriteInt32((int)value, littleEndian);             
            }
            else if (t == typeof(uint))
            {
                WriteUInt32((uint)value, littleEndian);                
            }
            else if (t == typeof(long))
            {
                WriteInt64((long)value, littleEndian);
            }
            else if (t == typeof(ulong))
            {
                WriteUInt64((ulong)value, littleEndian);
            }
            else if (t == typeof(float))
            {
                WriteFloat((float)value, littleEndian);
            }
            else if (t == typeof(double))
            {
                WriteDouble((double)value, littleEndian);
            }
            else if (t == typeof(UInt24))
            {
                WriteUInt24((UInt24)value, littleEndian);
            }
            else if (t == typeof(Int24))
            {
                WriteInt24((Int24)value, littleEndian);
            }
            else if (typeof(IPrimitiveValue).IsAssignableFrom(t))
            {
                ((IPrimitiveValue)value).ToWriter(this, littleEndian);
            }
            else
            {
                throw new ArgumentException(String.Format(CANAPE.Properties.Resources.DataWriter_InvalidPrimitiveType, t));
            }
        }

        /// <summary>
        /// Get or set count of bytes written
        /// </summary>
        public long BytesWritten
        {
            get { return _stm.BytesWritten;  }
            set { _stm.BytesWritten = value; }
        }
    }
}
