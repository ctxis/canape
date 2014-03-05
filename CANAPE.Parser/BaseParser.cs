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
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    /// <summary>
    /// Base class for a stream parser
    /// </summary>
    public abstract class BaseParser : IStreamTypeParser, IChildObject
    {
        ExpressionResolver _resolver;
        protected DataReader _reader;
        protected DataWriter _writer;
        protected StateDictionary _state;
        protected Logger _logger;
        protected object _parent;

        protected BaseParser()
        {
        }

        private ExpressionResolver GetResolver()
        {
            if (_resolver == null)
            {
                Interlocked.CompareExchange(ref _resolver, new ExpressionResolver(GetType(), _logger), null);                
            }

            return _resolver;
        }

        public Logger GetLogger()
        {
            return _logger;
        }

        public DataReader GetReader()
        {
            return _reader;
        }

        public DataWriter GetWriter()
        {
            return _writer;
        }

        public StateDictionary GetState()
        {
            return _state;
        }

        public BaseParser(DataReader reader, StateDictionary state, Utils.Logger logger)
        {
            FromStream(reader, state, logger);
        }

        protected dynamic Resolve(string expr)
        {
            return GetResolver().Resolve(this, expr, MakeDefaultDict());
        }

        protected dynamic Resolve(string expr, object value)
        {
            Dictionary<string, object> dict = MakeDefaultDict();
            dict["value"] = value;

            return GetResolver().Resolve(this, expr, dict);
        }

        protected dynamic Resolve(string expr, Dictionary<string, object> values)
        {
            return GetResolver().Resolve(this, expr, values);
        }

        private T CalcInternal<T>(dynamic res)
        {
            Type t = typeof(T);

            if (t.IsPrimitive || t == typeof(string))
            {
                return (T)Convert.ChangeType(res, t);
            }
            else
            {
                if (typeof(IBitValue).IsAssignableFrom(t))
                {
                    return (T)Activator.CreateInstance(t, Convert.ChangeType(res, typeof(ulong)));
                }
                else if(t == typeof(Int7V))
                {
                    return (T)Activator.CreateInstance(t, Convert.ChangeType(res, typeof(long)));
                }
                else if(t == typeof(Int24))
                {
                    return (T)Activator.CreateInstance(t, Convert.ChangeType(res, typeof(int)));
                }
                else if (t == typeof(UInt24))
                {
                    return (T)Activator.CreateInstance(t, Convert.ChangeType(res, typeof(uint)));
                }
                else
                {
                    // Just try a cast
                    return (T)res;
                }
            }
        }

        protected T Calc<T>(string expr)
        {
            return CalcInternal<T>(Resolve(expr));
        }

        protected T Calc<T>(string expr, object value)
        {
            return CalcInternal<T>(Resolve(expr, value));
        }

        protected T Calc<T>(string expr, ref T value)
        {
            value = CalcInternal<T>(Resolve(expr, value));

            return value;
        }

        protected T Calc<T>(string expr, DataReader reader)
        {
            Dictionary<string, object> dict = MakeDefaultDict();
            dict["reader"] = reader;

            return (T)Convert.ChangeType(Resolve(expr, dict), typeof(T));
        }

        private Dictionary<string, object> MakeDefaultDict()
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> pair in _state)
            {
                ret.Add(pair.Key, pair.Value);
            }            

            return ret;
        }

        protected void Resolve(string expr, DataWriter writer, object value)
        {
            Dictionary<string, object> dict = MakeDefaultDict();
            dict["value"] = value;
            dict["writer"] = writer;

            Resolve(expr, dict);
        }

        protected bool Check(string expr)
        {
            return Convert.ToBoolean(Resolve(expr));
        }

        protected bool Check(string expr, object value)
        {            
            return Convert.ToBoolean(Resolve(expr, value));
        }

        protected bool Check(string expr, object value, int i)
        {
            Dictionary<string, object> dict = MakeDefaultDict();
            dict["value"] = value;
            dict["i"] = i;

            return Convert.ToBoolean(Resolve(expr, dict));
        }

        public virtual void FromStream()
        {            
        }

        public virtual void ToStream()
        {
        }

        public void FromStream(DataReader reader, StateDictionary state, Utils.Logger logger)
        {
            _reader = reader;
            _state = state;
            _logger = logger;

            if (_state.ContainsKey("parent"))
            {
                _parent = _state["parent"];
            }

            FromStream();
        }

        public void ToStream(DataWriter writer, StateDictionary state, Utils.Logger logger)
        {
            _writer = writer;
            _state = state;
            _logger = logger;

            ToStream();
        }

        protected long BLen(IStreamTypeParser[] arr) 
        {
            DataWriter w = new DataWriter();
            for (int i = 0; (i < arr.Length); i++)
            {
                arr[i].ToStream(w, CS(), _logger);
            }
            return w.BytesWritten;
        }

        protected long BLen(IPrimitiveValue[] arr)
        {
            int size = ParserUtils.GetPrimitiveSize(arr.GetType().GetElementType());

            // No choice, write it out
            if(size < 0)
            {
                DataWriter w = new DataWriter();

                for (int i = 0; (i < arr.Length); i++)
                {
                    arr[i].ToWriter(w, false);
                }

                return w.BytesWritten;
            }
            else
            {
                return arr.Length * size;
            }
        }

        protected long BLen<T>(T[] arr) where T : struct
        {
            Type eType = arr.GetType().GetElementType();

            if ((eType == typeof(byte)) || (eType == typeof(sbyte)))
            {
                return arr.Length;
            }            
            else
            {
                return arr.Length * ParserUtils.GetPrimitiveSize(eType);
            }
        }

        protected T[] SAB<T>(DataReader reader, int length, int adjustment) where T : IStreamTypeParser, new()
        {
            List<T> ret = new List<T>();

            DataReader r = new DataReader(reader.ReadBytes(length + adjustment));

            while (r.DataLeft > 0)
            {
                T t = new T();
                t.FromStream(reader, CS(), _logger);
                ret.Add(t);
            }

            return ret.ToArray();
        }

        protected T[] SA<T>(DataReader reader, int length, int adjustment) where T : IStreamTypeParser, new()
        {
            T[] ret = new T[length+adjustment];

            for (int i = 0; i < length + adjustment; ++i)
            {
                ret[i] = new T();
                ret[i].FromStream(reader, CS(), _logger);
            }

            return ret;
        }

        protected T[] SAB<T>(DataReader reader, object length, int adjustment) where T : IStreamTypeParser, new()
        {
            return SAB<T>(reader, Convert.ToInt32(length), adjustment);
        }

        protected T[] SA<T>(DataReader reader, object length, int adjustment) where T : IStreamTypeParser, new()
        {
            return SA<T>(reader, Convert.ToInt32(length), adjustment);
        }

        protected T[] SAB<T>(DataReader reader, object length, string expr) where T : IStreamTypeParser, new()
        {
            return SAB<T>(reader, CL(length, expr), 0);
        }

        protected T[] SA<T>(DataReader reader, object length, string expr) where T : IStreamTypeParser, new()
        {
            return SA<T>(reader, CL(length, expr), 0);
        }

        protected StateDictionary CS()
        {
            StateDictionary ret = new StateDictionary();

            ret.Add("parent", this);            

            return ret;
        }

        protected void WA<T>(T[] arr, DataWriter writer) where T : IStreamTypeParser
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i].ToStream(writer, CS(), _logger);
            }
        }

        protected byte[] RB(DataReader reader, int length)
        {
            return reader.ReadBytes(length);
        }

        protected int CL(object length, string expr)
        {
            return Convert.ToInt32(Resolve(expr, length));
        }

        protected int CL(string expr)
        {
            return Convert.ToInt32(Resolve(expr));
        }

        protected int CLBits(int length, int bits)
        {
            return ((length * bits) + 7) / 8;
        }

        protected int CLBitsB(int length, int bits)
        {
            return length * 8 / bits;
        }

        protected int CL(object length)
        {
            return Convert.ToInt32(length);
        }

        /// <summary>
        /// Read a string of a fixed length
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="length">The length of the string to read</param>
        /// <returns>The string read from the stream</returns>
        protected string FixS(DataReader reader, BinaryStringEncoding encoding, object length, int adjustment)
        {
            return FixS(reader, encoding, Convert.ToInt32(length, CultureInfo.InvariantCulture), adjustment);            
        }

        /// <summary>
        /// Read a string of a fixed length
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="length">The length of the string to read</param>
        /// <returns>The string read from the stream</returns>
        protected string FixS(DataReader reader, BinaryStringEncoding encoding, object length, string expr)
        {
            return FixS(reader, encoding, CL(length, expr), 0);
        }

        /// <summary>
        /// Read a string of a fixed length
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="length">The length of the string to read</param>
        /// <returns>The string read from the stream</returns>
        protected string FixS(DataReader reader, BinaryStringEncoding encoding, int length, int adjustment)
        {
            StringBuilder builder = new StringBuilder();
            int i = 0;

            int len = length + adjustment;
            Encoding enc = GeneralUtils.GetEncodingFromType(encoding);

            while (i < len)
            {
                builder.Append(reader.ReadChar(enc));
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
        protected string FixBS(DataReader reader, BinaryStringEncoding encoding, object length, int adjustment)
        {
            return FixBS(reader, encoding, Convert.ToInt32(length, CultureInfo.InvariantCulture), adjustment);
        }

        /// <summary>
        /// Read a string of a fixed length in bytes
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="length">The length of the string to read in bytes</param>
        /// <param name="expr">An expression to calculate the length</param>
        /// <returns>The string read from the stream</returns>
        protected string FixBS(DataReader reader, BinaryStringEncoding encoding, object length, string expr)
        {
            return FixBS(reader, encoding, CL(length, expr), 0);
        }

        /// <summary>
        /// Read a string of a fixed length in bytes
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="length">The length of the string to read in bytes</param>
        /// <returns>The string read from the stream</returns>
        protected string FixBS(DataReader reader, BinaryStringEncoding encoding, int length, int adjustment)
        {
            return GeneralUtils.GetEncodingFromType(encoding).GetString(reader.ReadBytes(length + adjustment, true));
        }        

        /// <summary>
        /// Read a fixed primitive array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="length"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        protected T[] FixPA<T>(DataReader reader, int length, bool littleEndian) where T : struct
        {
            if (length == 0)
            {
                return new T[0];
            }

            if ((typeof(T) == typeof(byte[])) || (typeof(T) == typeof(sbyte[])))
            {
                return (T[])(object)reader.ReadBytes(length);
            }
            else
            {
                T[] ret = new T[length];

                for (int i = 0; i < length; ++i)
                {
                    ret[i] = reader.ReadPrimitive<T>(littleEndian);
                }

                return ret;
            }
        }

        protected void WPA<T>(T[] arr, DataWriter writer, bool littleEndian) where T : struct
        {
            if ((arr != null) && (arr.Length > 0))
            {
                if ((typeof(T) == typeof(byte)) || (typeof(T) == typeof(sbyte)))
                {
                    writer.Write((byte[])(object)arr);
                }
                else
                {
                    for (int i = 0; i < arr.Length; ++i)
                    {
                        writer.WritePrimitive(arr[i], littleEndian);
                    }
                }
            }
        }

        protected void FixWPA<T>(T[] arr, DataWriter writer, int length, bool littleEndian) where T : struct
        {
            MemoryStream stm = new MemoryStream();
            DataWriter newWriter = new DataWriter(stm);

            if ((arr != null) && (arr.Length > 0))
            {
                if ((typeof(T) == typeof(byte)) || (typeof(T) == typeof(sbyte)))
                {
                    newWriter.Write((byte[])(object)arr);
                }
                else
                {
                    for (int i = 0; i < arr.Length; ++i)
                    {
                        newWriter.WritePrimitive(arr[i], littleEndian);
                    }
                }
            }

            if (stm.Length < length)
            {
                newWriter.Write(new byte[length - stm.Length]);
            }
            else
            {
                stm.SetLength(length);                
            }

            writer.Write(stm.ToArray());
        }

        private static bool IsByte(Type t)
        {
            return (t == typeof(byte)) || (t == typeof(sbyte));
        }

        protected T[] RTEPA<T>(DataReader reader, bool littleEndian, int trailing) where T : struct
        {
            if(IsByte(typeof(T)))
            {
                if (trailing > 0)
                {
                    return (T[])(object)reader.ReadToEndTrail(trailing);
                }
                else
                {
                    return (T[])(object)reader.ReadToEnd();
                }
            }
            else
            {
                List<T> ret = new List<T>();                

                if (trailing > 0)
                {
                    // Read in new reader with trailing bytes
                    reader = new DataReader(reader.ReadToEndTrail(trailing));
                }                               

                while (reader.DataLeft > 0)
                {
                    ret.Add(reader.ReadPrimitive<T>(littleEndian));
                }

                return ret.ToArray();
            }
        }

        protected T[] FixBPA<T>(DataReader reader, int length, bool littleEndian) where T : struct
        {
            if (length == 0)
            {
                return new T[0];
            }

            if ((typeof(T) == typeof(byte[])) || (typeof(T) == typeof(sbyte[])))
            {
                return (T[])(object)reader.ReadBytes(length);
            }
            else
            {
                int primSize = ParserUtils.GetPrimitiveSize(typeof(T));

                if (primSize > 0)
                {
                    int elementCount = length / primSize;
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
        }

        /// <summary>
        /// Read a string terminated with a specific character
        /// </summary>
        /// <param name="reader">DataReader to read from</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="terminator">The terminating character</param>        
        /// <exception cref="EndOfStreamException">Throw if hits the end of stream before termination</exception>
        /// <returns>The read string</returns>
        protected string RTS(DataReader reader, BinaryStringEncoding encoding, char terminator)
        {
            return ParserUtils.ReadTerminatedString(reader, GeneralUtils.GetEncodingFromType(encoding), terminator);
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
        protected string RTS(DataReader reader, BinaryStringEncoding encoding, char terminator, bool required)
        {
            return ParserUtils.ReadTerminatedString(reader, GeneralUtils.GetEncodingFromType(encoding), terminator, required);
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
        public string RTS(DataReader reader, BinaryStringEncoding encoding, string terminator, bool required)
        {
            return ParserUtils.ReadTerminatedString(reader, GeneralUtils.GetEncodingFromType(encoding), terminator, required);
        }

        /// <summary>
        /// Write a string terminated with a specific character
        /// </summary>
        /// <param name="writer">DataWriter to write to</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="s">The string to write</param>
        /// <param name="terminator">The terminating character</param>
        protected void WTS(DataWriter writer, BinaryStringEncoding encoding, string s, char terminator)
        {
            WTS(writer, encoding, s, new string(new char[] { terminator }));
        }

        /// <summary>
        /// Write a string terminated with a specific character
        /// </summary>
        /// <param name="writer">DataWriter to write to</param>
        /// <param name="encoding">The string encoding used</param>
        /// <param name="s">The string to write</param>
        /// <param name="terminator">The terminating character</param>
        protected void WTS(DataWriter writer, BinaryStringEncoding encoding, string s, string terminator)
        {
            Encoding enc = GeneralUtils.GetEncodingFromType(encoding);
            byte[] data = enc.GetBytes(s);
            byte[] term = enc.GetBytes(terminator);

            writer.Write(data);
            writer.Write(term);
        }

        /// <summary>
        /// Read sequence choice, no fallback
        /// </summary>
        /// <param name="value">The value of the selected field</param>
        /// <param name="args">The arguments, should be laid out as 'expr', type, ... </param>
        /// <returns>The parsed sequence, null if not found a match</returns>
        protected IStreamTypeParser RSC(DataReader reader, object value, params object[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                if (value != null ? Check(args[i].ToString(), value) : Check(args[i].ToString()))
                {
                    IStreamTypeParser ret = (IStreamTypeParser)Activator.CreateInstance((Type)args[i + 1]);
                    ret.FromStream(reader, CS(), _logger);

                    return ret;
                }
            }

            throw new ArgumentException(String.Format(Properties.Resources.BaseParser_NoValidSequenceChoice, this.GetType().Name));
        }

        /// <summary>
        /// Read a sequence choice, relies on validation rules on the parsing to ensure parsing        
        /// </summary>
        /// <remarks>The reader must be seekable</remarks>
        /// <param name="reader"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        protected IStreamTypeParser RSC(DataReader reader, params Type[] types)
        {
            if (!reader.GetStream().CanSeek)
            {
                throw new ArgumentException(Properties.Resources.BaseParser_RSCStreamCannotSeek, "reader");
            }

            foreach (Type t in types)
            {
                long currPos = reader.GetStream().Position;
                long currentRead = reader.ByteCount;

                try
                {
                    IStreamTypeParser ret = (IStreamTypeParser)Activator.CreateInstance(t);
                   
                    ret.FromStream(reader, CS(), _logger);

                    return ret;
                }
                catch (Exception)
                {
                }

                reader.GetStream().Position = currPos;
                reader.ByteCount = currentRead;
            }

            throw new ArgumentException(String.Format(Properties.Resources.BaseParser_NoValidSequenceChoice, this.GetType().Name));
        }

        protected T V<T>(T value, string expr)
        {
            if (!Check(expr, value))
            {
                throw new InvalidOperationException(String.Format(Properties.Resources.BaseParser_ValidateFailed, expr));
            }

            return value;
        }

        protected int SBL(string value, BinaryStringEncoding encoding)
        {
            return ParserUtils.GetStringByteLength(value, GeneralUtils.GetEncodingFromType(encoding));
        }

        protected DataReader GBR(DataReader reader, string expr)
        {
            return new DataReader(reader.ReadBytes((int)CL(expr)));
        }

        protected DataReader GBR(DataReader reader)
        {
            return new DataReader(reader.ReadToEnd());
        }

        protected byte[] CMP(byte[] a, byte[] b)
        {
            if (!GeneralUtils.CompareBytes(a, b))
            {
                throw new ArgumentException(String.Format(Properties.Resources.BaseParser_CMPFailure, GeneralUtils.EscapeBytes(a), GeneralUtils.EscapeBytes(b)));
            }

            return a;
        }

        object IChildObject.Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
    }
}
