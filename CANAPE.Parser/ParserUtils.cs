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
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using CANAPE.DataFrames;
using CANAPE.Utils;
using Microsoft.CSharp;

namespace CANAPE.Parser
{
    /// <summary>
    /// Static utilities to simplify implementation of parsers
    /// </summary>
    public static class ParserUtils
    {
        /// <summary>
        /// Get a compatible type from a target assuming that is could be a container
        /// </summary>
        /// <param name="t">The type to find</param>
        /// <param name="target">The initial target</param>
        /// <returns>The compatible object, or null if not found</returns>
        public static object GetCompatibleType(Type t, object target)
        {
            if (target != null)
            {
                while (!t.IsAssignableFrom(target.GetType()))
                {
                    ContainerMemberEntry container = target as ContainerMemberEntry;
                    if (container != null)
                    {
                        target = container.BaseEntry;
                    }
                    else
                    {
                        target = null;
                        break;
                    }
                }
            }

            return target;
        }


        /// <summary>
        /// Read a string terminated with a specific character
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="terminator">The terminating character</param>        
        /// <exception cref="EndOfStreamException">Throw if hits the end of stream before termination</exception>
        /// <returns>The read string</returns>
        public static string ReadTerminatedString(DataReader reader, Encoding encoding, char terminator)
        {
            return ReadTerminatedString(reader, encoding, terminator, true);
        }

        /// <summary>
        /// Read a string terminated with a specific character
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="terminator">The terminating character</param>
        /// <param name="required">If true then the terminator must be present, if false will also terminate on EOF</param>
        /// <exception cref="EndOfStreamException">Throw if hits the end of stream before termination</exception>
        /// <returns>The read string</returns>
        public static string ReadTerminatedString(DataReader reader, Encoding encoding, char terminator, bool required)
        {
            StringBuilder builder = new StringBuilder();
            char ch = reader.ReadChar(encoding);

            try
            {                
                while (ch != terminator)
                {
                    builder.Append(ch);
                    ch = reader.ReadChar(encoding);
                }
            }
            catch (EndOfStreamException)
            {
                if (required)
                {
                    throw;
                }
            }

            return builder.ToString();
        }
        
        /// <summary>
        /// Read a string terminated with a specific character
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="terminator">The terminating string</param>
        /// <param name="required">If true then the terminator must be present, if false will also terminate on EOF</param>
        /// <exception cref="EndOfStreamException">Throw if hits the end of stream before termination</exception>        
        /// <returns>The read string</returns>
        public static string ReadTerminatedString(DataReader reader, Encoding encoding, string terminator, bool required)
        {
            if (terminator.Length == 0)
            {
                throw new ArgumentException(terminator);
            }

            if (terminator.Length == 1)
            {
                // Slightly quicker probably
                return ReadTerminatedString(reader, encoding, terminator[0], required);
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                StringBuilder buf = new StringBuilder(reader.Read(terminator.Length, true, encoding));

                try
                {
                    while (buf.ToString() != terminator)
                    {
                        builder.Append(buf[0]);

                        buf.Remove(0, 1);
                        buf.Append(reader.ReadChar(encoding));
                    }
                }
                catch (EndOfStreamException)
                {
                    if (required)
                    {
                        throw;
                    }

                    builder.Append(buf);
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Write a string terminated with a specific character
        /// </summary>
        /// <param name="writer">DataWriter to write to</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="s">The string to write</param>
        /// <param name="terminator">The terminating character</param>
        public static void WriteTerminatedString(DataWriter writer, Encoding encoding, string s, char terminator)
        {
            WriteTerminatedString(writer, encoding, s, new string(new char[] { terminator }));
        }

        /// <summary>
        /// Write a string terminated with a specific character
        /// </summary>
        /// <param name="writer">DataWriter to write to</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="s">The string to write</param>
        /// <param name="terminator">The terminating character</param>
        public static void WriteTerminatedString(DataWriter writer, Encoding encoding, string s, string terminator)
        {
            byte[] data = encoding.GetBytes(s);
            byte[] term = encoding.GetBytes(terminator);

            writer.Write(data);
            writer.Write(term);
        }


        /// <summary>
        /// Read a string of a fixed length
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="length">The length of the string to read</param>
        /// <returns>The string read from the stream</returns>
        public static string ReadFixedLengthString(DataReader reader, Encoding encoding, int length)
        {
            StringBuilder builder = new StringBuilder();
            int i = 0;

            while (i < length)
            {
                builder.Append(reader.ReadChar(encoding));
                ++i;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Read a string of a fixed length in bytes
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="length">The length of the string to read in bytes</param>
        /// <returns>The string read from the stream</returns>
        public static string ReadFixedByteLengthString(DataReader reader, Encoding encoding, int length)
        {
            return encoding.GetString(reader.ReadBytes(length, true));
        }

        /// <summary>
        /// Write a string of fixed length
        /// </summary>
        /// <param name="writer">DataWriter to write to</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="s">The string to write</param>
        /// <param name="length">The length of the string</param>
        /// <param name="padding">Padding character if the string is shorter than length</param>
        public static void WriteFixedLengthString(DataWriter writer, Encoding encoding, string s, int length, char padding)
        {
            if (s.Length > length)
            {
                s = s.Substring(0, length);
            }
            else if (s.Length < length)
            {
                StringBuilder builder = new StringBuilder(s);
                builder.Append(padding, length - s.Length);
            }

            writer.Write(s, encoding);
        }

        /// <summary>
        /// Get the length of a string in bytes
        /// </summary>
        /// <param name="s">The string to get the length of</param>
        /// <param name="encoding">The encoding to use</param>
        /// <returns>The length in bytes</returns>
        public static int GetStringByteLength(string s, Encoding encoding)
        {
            return encoding.GetByteCount(s);
        }

        /// <summary>
        /// Get the length of an array in bytes
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static long GetArrayByteLength(Array arr)
        {
            Type elementType = arr.GetType().GetElementType();
            
            if (elementType.IsPrimitive)
            {
                // If a primitive type we can just do a straight calculation
                return GetPrimitiveSize(elementType)*arr.LongLength;
            }
            else
            {
                // We need to serialize out the data, let the caller worry about it
                return -1;
            }            
        }

        /// <summary>
        /// Get the length of an array in bytes
        /// </summary>
        /// <param name="arr">The array of strings</param>
        /// <param name="encoding">The string encoding</param>
        /// <returns></returns>
        public static long GetStringArrayByteLength(string[] arr, Encoding encoding)
        {
            return -1;
        }

        /// <summary>
        /// Write a string with a fixed byte (not character) length
        /// </summary>
        /// <param name="writer">The DataWriter to write to</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="s">The string to write</param>
        /// <param name="length">The length of the string in bytes</param>
        /// <param name="padding">Padding byte to use if string less than length</param>
        public static void WriteFixedByteLengthString(DataWriter writer, Encoding encoding, string s, int length, byte padding)
        {
            byte[] data = encoding.GetBytes(s);

            WriteFixedByteLengthArray(writer, data, length, padding);
        }

        /// <summary>
        /// Write a byte array with a fixed byte length
        /// </summary>
        /// <param name="writer">DataWriter to write to</param>
        /// <param name="data">The data to write</param>
        /// <param name="length">The length of the data to write</param>
        /// <param name="padding">Padding byte to use if string less than length</param>
        public static void WriteFixedByteLengthArray(DataWriter writer, byte[] data, int length, byte padding)
        {
            int currLength = data.Length;

            if (currLength < length)
            {
                Array.Resize(ref data, length);
                for (int i = currLength; i < length; ++i)
                {
                    data[i] = padding;
                }
            }
            else if (currLength > length)
            {
                Array.Resize(ref data, length);
            }

            writer.Write(data);
        }

        public static int GetPrimitiveSize(Type t)
        {
            if (t == typeof(Int24) || t == typeof(UInt24))
            {
                return 3;
            }
            else if (t.IsPrimitive)
            {
                return Marshal.SizeOf(t);
            }
            else
            {
                return -1;
            }
        }

        public static T[] ReadFixedLengthPrimitiveArray<T>(DataReader reader, int length, bool littleEndian) where T : struct
        {
            T[] ret = new T[length];

            for (int i = 0; i < length; ++i)
            {
                ret[i] = reader.ReadPrimitive<T>(littleEndian);
            }

            return ret;
        }        

        public static T[] ReadFixedByteLengthPrimitiveArray<T>(DataReader reader, int length, bool littleEndian) where T : struct
        {
            int primSize = GetPrimitiveSize(typeof(T));

            if (primSize > 0)
            {
                int elementCount = length / GetPrimitiveSize(typeof(T));
                T[] ret = new T[elementCount];

                for (int i = 0; i < elementCount; ++i)
                {
                    ret[i] = reader.ReadPrimitive<T>(littleEndian);
                }

                return ret;
            }
            else
            {
                List<T> ret = new List<T>();
                reader = new DataReader(reader.ReadBytes(length));

                while (reader.DataLeft > 0)
                {
                    ret.Add(reader.ReadPrimitive<T>(littleEndian));
                }

                return ret.ToArray();
            }
            
        }

        public static T[] ReadToEndPrimitiveArray<T>(DataReader reader, bool littleEndian) where T : struct
        {           
            List<T> ret = new List<T>();
            // Could improve performance by using unsafe copying
            try
            {
                if ((typeof(T) == typeof(byte)) || (typeof(T) == typeof(sbyte)))
                {
                    Array arr = reader.ReadToEnd();

                    ret.AddRange((T[])arr);
                }
                else
                {
                    while (true)
                    {
                        ret.Add(reader.ReadPrimitive<T>(littleEndian));
                    }
                }
            }
            catch (EndOfStreamException)
            {
            }

            return ret.ToArray();
        }

        public static void WritePrimitiveArray<T>(DataWriter writer, T[] data, bool littleEndian) where T : struct
        {
            if ((typeof(T) == typeof(byte)) || (typeof(T) == typeof(sbyte)))
            {
                byte[] bytes = (byte[])(Array)data;

                writer.Write(bytes);
            }            
            else
            {
                for (int i = 0; i < data.Length; ++i)
                {
                    writer.WritePrimitive(data[i], littleEndian);
                }
            }
        }        

        public static void WriteFixedLengthPrimitiveArray<T>(DataWriter writer, T[] data, int length, bool littleEndian) where T : struct
        {
            if (data.Length != length)
            {
                Array.Resize(ref data, length);
            }

            WritePrimitiveArray(writer, data, littleEndian);
        }

        public static void WriteFixedByteLengthPrimitiveArray<T>(DataWriter writer, T[] data, int length, bool littleEndian) where T : struct
        {
            int elementCount = length / Marshal.SizeOf(typeof(T));

            if (data.Length != elementCount)
            {
                Array.Resize(ref data, length);
            }

            WritePrimitiveArray(writer, data, littleEndian);
        }

        public static T[] ReadFixedLengthSequenceArray<T>(DataReader reader, StateDictionary state, int length, Logger logger) where T : IStreamTypeParser, new()
        {
            T[] ret = new T[length];

            for (int i = 0; i < length; ++i)
            {
                ret[i] = new T();
                ret[i].FromStream(reader, state, logger);
            }

            return ret;
        }

        public static void WriteFixedLengthSequenceArray<T>(DataWriter writer, T[] data, StateDictionary state, int length, Logger logger) where T : IStreamTypeParser, new()
        {
            if (length != data.Length)
            {
                Array.Resize(ref data, length);
            }

            foreach (T value in data)
            {
                if (value != null)
                {
                    value.ToStream(writer, state, logger);
                }
                else
                {
                    new T().ToStream(writer, state, logger);
                }
            }
        }

        public static string GetReaderMethodForType(Type t)
        {
            string ret = null;

            if (t == typeof(byte))
            {
                ret = "ReadByte";
            }
            else if (t == typeof(sbyte))
            {
                ret = "ReadSByte";
            }
            else if (t == typeof(short))
            {
                ret = "ReadInt16";
            }
            else if (t == typeof(ushort))
            {
                ret = "ReadUInt16";
            }
            else if (t == typeof(int))
            {
                ret = "ReadInt32";
            }
            else if (t == typeof(uint))
            {
                ret = "ReadUInt32";
            }
            else if (t == typeof(long))
            {
                ret = "ReadInt64";
            }
            else if (t == typeof(ulong))
            {
                ret = "ReadUInt64";
            }
            else if (t == typeof(float))
            {
                ret = "ReadFloat";
            }
            else if (t == typeof(double))
            {
                ret = "ReadDouble";
            }
            else
            {
                throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.ParserUtils_InvalidPrimitiveType));
            }

            return ret;
        }

        /// <summary>
        /// Determine if a string is a valid identifier
        /// </summary>
        /// <param name="identifier">The identifer to check</param>
        /// <returns>True if valid</returns>
        public static bool IsValidIdentifier(string identifier)
        {
            return new CSharpCodeProvider().IsValidIdentifier(identifier);
        }

        /// <summary>
        /// Get the representation of the boolean value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        /// <returns></returns>
        public static T GetBooleanValue<T>(bool value, T trueValue, T falseValue)
        {
            return value ? trueValue : falseValue;
        }        

        /// <summary>
        /// Determine if a type can hold a specific value
        /// </summary>
        /// <returns>True if it can hold the value</returns>
        public static bool CheckInRange(long value, Type t)
        {
            bool ret = false;

            if(t == typeof(long))
            {
                ret = true;
            }
            else if (t == typeof(ulong))
            {
                if (value >= 0)
                {
                    ret = true;
                }
            }
            else
            {
                long minValue = 0;
                long maxValue = -1;

                if (t == typeof(byte))
                {
                    minValue = byte.MinValue;
                    maxValue = byte.MaxValue;
                }
                else if (t == typeof(sbyte))
                {
                    minValue = sbyte.MinValue;
                    maxValue = sbyte.MaxValue;
                }
                else if (t == typeof(short))
                {
                    minValue = short.MinValue;
                    maxValue = short.MaxValue;
                }
                else if (t == typeof(ushort))
                {
                    minValue = ushort.MinValue;
                    maxValue = ushort.MaxValue;
                }
                else if (t == typeof(int))
                {
                    minValue = int.MinValue;
                    maxValue = int.MaxValue;
                }
                else if (t == typeof(uint))
                {
                    minValue = uint.MinValue;
                    maxValue = uint.MaxValue;
                }

                if ((value >= minValue) && (value <= maxValue))
                {
                    ret = true;
                }
            }

            return ret;
        }


    }
}
