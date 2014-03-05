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
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using CANAPE.DataFrames;
using CANAPE.Nodes;
using System.Reflection;
using System.Threading;

namespace CANAPE.Utils
{
    /// <summary>
    /// Public static class containing some general helpful utility functions
    /// </summary>
    public static class GeneralUtils
    {
        static CultureInfo _culture = CultureInfo.CurrentUICulture;
        private static Regex _formatRegex = new Regex("\\$[a-zA-Z_/.*][a-zA-Z0-9_/.*]*");
        private static bool _onMono;

        static GeneralUtils()
        {
            _onMono = Type.GetType("Mono.Runtime") != null;
        }

        /// <summary>
        /// Aborts a live thread which isn't ourselves
        /// </summary>
        /// <param name="thread">The thread to abort</param>
        public static void AbortThread(Thread thread)
        {
            if ((thread != null) && (thread.IsAlive) && (thread != Thread.CurrentThread))
            {
                thread.Abort();
            }
        }
        
        /// <summary>
        /// Get the application's culture info
        /// </summary>
        /// <returns>The current culture info</returns>
        public static CultureInfo GetCurrentCulture()
        {
            return _culture;
        }

        /// <summary>
        /// Set the application's culture info
        /// </summary>
        /// <param name="ci">The culture info to use</param>
        public static void SetCurrentCulture(CultureInfo ci)
        {
            _culture = ci;

            // If we are running on .NET 4.5 then we can also set the default thread culture
            PropertyInfo pi = typeof(CultureInfo).GetProperty("DefaultThreadCurrentUICulture");
            if (pi != null)
            {
                pi.SetValue(null, ci, null);
            }
        }

        /// <summary>
        /// Generate a MD5 hex string from a byte array
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <returns>The MD5 hex string</returns>
        public static string GenerateMd5String(byte[] data)
        {
            byte[] hash = MD5.Create().ComputeHash(data);
            StringBuilder builder = new StringBuilder();

            foreach (byte b in hash)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X02}", b);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Print a row of hex to the string builder
        /// </summary>
        /// <param name="builder">The string builder to write to</param>
        /// <param name="rowLength">Length of the row</param>
        /// <param name="data">The data array</param>
        /// <param name="offset">Offset in the array</param>
        /// <param name="len">Length of the current row</param>
        private static void PrintRow(StringBuilder builder, int rowLength, byte[] data, int offset, int len)
        {
            builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X08}: ", offset);
            for (int i = 0; i < rowLength; i++)
            {
                if (i < len)
                {
                    builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X02} ", data[i + offset]);
                }
                else
                {
                    builder.Append("   ");
                }
            }

            builder.Append("- ");

            for (int i = 0; i < rowLength; i++)
            {
                if (i < len)
                {
                    char val = (char)data[i + offset];

                    if ((val >= 32) && (val < 127))
                    {
                        builder.AppendFormat(CultureInfo.InvariantCulture, "{0}", val);
                    }
                    else
                    {
                        builder.Append(".");
                    }
                }
                else
                {
                    builder.Append(" ");
                }
            }

            builder.AppendLine();
        }

        /// <summary>
        /// Write a hexdump of a byte array to a string
        /// </summary>        
        /// <param name="rowLength">Length of a row</param>
        /// <param name="data">Data length</param>
        /// <returns>The hex dump string</returns>
        public static string BuildHexDump(int rowLength, byte[] data)
        {
            StringBuilder builder = new StringBuilder();

            BuildHexDump(builder, rowLength, data);

            return builder.ToString();
        }

        /// <summary>
        /// Write a hexdump of a byte array to a string builder
        /// </summary>
        /// <param name="builder">The string builder</param>
        /// <param name="rowLength">Length of a row</param>
        /// <param name="data">Data length</param>
        public static void BuildHexDump(StringBuilder builder, int rowLength, byte[] data)
        {
            int rows = data.Length / rowLength;
            int res = data.Length % rowLength;

            builder.Append("        : ");

            for (int i = 0; i < rowLength; i++)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X02} ", i & 0xF);
            }

            builder.Append("- ");

            for (int i = 0; i < rowLength; i++)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture, "{0:X}", i & 0xF);
            }
            builder.AppendLine();
            builder.Append("--------:-");
            for (int i = 0; i < rowLength; i++)
            {
                builder.Append("---");
            }
            builder.Append("--");
            for (int i = 0; i < rowLength; i++)
            {
                builder.Append("-");
            }
            builder.AppendLine();

            for (int i = 0; i < rows; i++)
            {
                PrintRow(builder, rowLength, data, i * rowLength, rowLength);
            }

            if (res > 0)
            {
                PrintRow(builder, rowLength, data, rows * rowLength, res);
            }
        }

        /// <summary>
        /// Serialize a list of data frames to a file
        /// </summary>
        /// <param name="packets">The packets to serialize</param>
        /// <param name="stm">The stream</param>
        public static void SerializeLogPackets(LogPacket[] packets, Stream stm)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stm, packets);
        }

        /// <summary>
        /// Serialize a list of log packets to a file
        /// </summary>
        /// <param name="packets">The list of packets</param>
        /// <param name="fileName">The file name to write to</param>
        public static void SerializeLogPackets(LogPacket[] packets, string fileName)
        {
            using (Stream stm = File.Open(fileName, FileMode.Create, FileAccess.Write))
            {
                SerializeLogPackets(packets, stm);
            }
        }

        /// <summary>
        /// Deserialize a list of log packets
        /// </summary>
        /// <param name="stm"></param>
        /// <returns></returns>
        public static LogPacket[] DeserializeLogPackets(Stream stm)
        {
            BinaryFormatter fmt = new BinaryFormatter();
            List<LogPacket> packets = new List<LogPacket>();

            while (stm.Position < stm.Length)
            {
                packets.AddRange((LogPacket[])fmt.Deserialize(stm)); 
            }

            return packets.ToArray();
        }

        /// <summary>
        /// Deserialize a list of log packets from a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static LogPacket[] DeserializeLogPackets(string fileName)
        {
            using (Stream stm = File.OpenRead(fileName))
            {
                return DeserializeLogPackets(stm);
            }
        }

        /// <summary>
        /// Create a string from a byte array like python does
        /// </summary>
        /// <param name="data">The data to convert</param>
        /// <returns>The converted string, null if data was null</returns>
        public static string MakeByteString(byte[] data)
        {
            if (data != null)
            {                
                return new BinaryEncoding().GetString(data);
            }

            return null;
        }

        /// <summary>
        /// Make a byte array from a string like python
        /// </summary>
        /// <param name="s">The string to convert (note any char values > 255 will be masked)</param>
        /// <returns>The byte array, null if s was null</returns>
        public static byte[] MakeByteArray(string s)
        {            
            if (s != null)
            {
                return new BinaryEncoding().GetBytes(s);
            }

            return null;
        }

        /// <summary>
        /// Read a line of data from a stream, reads up to a NL
        /// </summary>
        /// <param name="stm">The stream to read from</param>
        /// <returns>The line</returns>
        /// <exception cref="System.IO.EndOfStreamException">Throw when no more data availeble</exception>
        public static string ReadLine(Stream stm)
        {         
            List<byte> reqBytes = new List<byte>();
            int ch = 0;

            while ((ch = stm.ReadByte()) >= 0)
            {
                reqBytes.Add((byte)ch);
                if (ch == 10)
                {                    
                    break;
                }
            }

            if (reqBytes.Count == 0)
            {
                throw new EndOfStreamException(Properties.Resources.ReadLine_CountNotReadFromStream);
            }

            return MakeByteString(reqBytes.ToArray());
        }

        /// <summary>
        /// Create a printable escaped string, converts control characters to \xXX
        /// </summary>
        /// <param name="str">The string to escape</param>
        /// <returns>The escaped string</returns>
        public static string EscapeString(string str)
        {
            StringBuilder builder = new StringBuilder();

            foreach (char c in str)
            {
                if (c < 32)
                {
                    builder.AppendFormat(@"\x{0:X02}", (int)c);
                }
                else
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Create a printable escaped string, converts control characters to \xXX from a byte array
        /// </summary>
        /// <param name="bytes">The bytes to escape</param>
        /// <returns>The escaped string</returns>
        public static string EscapeBytes(byte[] bytes)
        {
            return EscapeString(BinaryEncoding.Instance.GetString(bytes));
        }

        /// <summary>
        /// Are we running on a windows operating system?
        /// </summary>
        /// <returns></returns>
        public static bool IsRunningOnWindows()
        {
            PlatformID id = Environment.OSVersion.Platform;

            if ((id == PlatformID.Win32NT) || (id == PlatformID.Win32Windows))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Are we running on Mono
        /// </summary>
        /// <returns>True if we are running on mono</returns>
        public static bool IsRunningOnMono()
        {
            return _onMono;        
        }

        /// <summary>
        /// Swap bytes if necessary to get the correct endian
        /// </summary>
        /// <param name="arr">The bytes to swap</param>
        /// <param name="littleEndian">True for little endian, false for big endian</param>
        /// <returns></returns>
        public static byte[] SwapBytes(byte[] arr, bool littleEndian)
        {
            if ((littleEndian && !BitConverter.IsLittleEndian) || (!littleEndian && BitConverter.IsLittleEndian))
            {
                return arr.Reverse().ToArray();
            }

            return arr;
        }

        /// <summary>
        /// Convert a basic Glob to a regular expression, ignoring case
        /// </summary>
        /// <param name="glob">The glob string</param>        
        /// <returns>The regular expression</returns>
        public static Regex GlobToRegex(string glob)
        {
            return GlobToRegex(glob, true);
        }

        /// <summary>
        /// Convert a basic Glob to a regular expression
        /// </summary>
        /// <param name="glob">The glob string</param>
        /// <param name="ignoreCase">Indicates that match should ignore case</param>
        /// <returns>The regular expression</returns>
        public static Regex GlobToRegex(string glob, bool ignoreCase)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("^");

            foreach (char ch in glob)
            {
                if (ch == '*')
                {
                    builder.Append(".*");
                }
                else if (ch == '?')
                {
                    builder.Append(".");
                }
                else
                {
                    builder.Append(Regex.Escape(new String(ch, 1)));
                }
            }

            builder.Append("$");

            return new Regex(builder.ToString(), ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        /// Read out a fixed number of bytes or throw and EndOfStreamException
        /// </summary>
        /// <param name="stm">The stream</param>
        /// <param name="totalLen">The length to read</param>
        /// <exception cref="EndOfStreamException"></exception>
        /// <returns>A byte array containing the data</returns>
        public static byte[] ReadBytes(Stream stm, int totalLen)
        {
            int len = 0;
            byte[] ret = new byte[totalLen];

            while (len < totalLen)
            {
                int read = stm.Read(ret, len, totalLen - len);
                if (read == 0)
                {
                    throw new EndOfStreamException();
                }
                len += read;
            }

            return ret;
        }

        /// <summary>
        /// Convert an object to a string using serialization
        /// </summary>
        /// <param name="o">The object to convert</param>
        /// <returns>The string, null on error</returns>
        public static string ObjectToString(Object o)
        {
            if (o != null)
            {
                return Convert.ToBase64String(ObjectToBytes(o));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert a string to an object using serialization
        /// </summary>
        /// <param name="s">The base64 string for the object</param>
        /// <returns>The object, null on error</returns>
        public static Object StringToObject(string s)
        {
            if (!String.IsNullOrWhiteSpace(s))
            {
                return BytesToObject(Convert.FromBase64String(s));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert an object to a byte array using serialization
        /// </summary>
        /// <param name="o">The object to convert</param>
        /// <returns>The bytes, null on error</returns>
        public static byte[] ObjectToBytes(Object o)
        {
            byte[] ret = null;

            if (o != null)
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream stm = new MemoryStream();

                    formatter.Serialize(stm, o);

                    ret = stm.ToArray();
                }
                catch (SerializationException)
                {
                }
            }

            return ret;
        }

        /// <summary>
        /// Convert a byte array to an object using serialization
        /// </summary>
        /// <param name="bytes">The byte array</param>
        /// <returns>The object, null on error</returns>
        public static Object BytesToObject(byte[] bytes)
        {
            Object ret = null;

            if ((bytes != null) && (bytes.Length > 0))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream stm = new MemoryStream(bytes);

                    ret = formatter.Deserialize(stm);
                }
                catch (SerializationException)
                {
                }
                catch (FormatException)
                {
                }
            }

            return ret;
        }

        /// <summary>
        /// Convert hex data to binary
        /// </summary>
        /// <param name="hex">The hex string to convert</param>
        /// <returns>The byte array</returns>
        public static byte[] HexToBinary(string hex)
        {
            return HexToBinary(hex, true);
        }

        /// <summary>
        /// Convert hex data to binary
        /// </summary>
        /// <param name="hex">The hex string to convert</param>
        /// <param name="filter">Whether to filter out hyphens</param>
        /// <returns>The byte array</returns>
        public static byte[] HexToBinary(string hex, bool filter)
        {
            string values = filter ? hex.Replace(" ", "") : hex;
            List<byte> ret = new List<byte>();

            if ((values.Length % 2) != 0)
            {
                throw new ArgumentException(Properties.Resources.GeneralUtils_InvalidHexStringLength);
            }

            for (int i = 0; i < values.Length; i += 2)
            {
                byte val;

                if (!byte.TryParse(values.Substring(i, 2), NumberStyles.HexNumber, null, out val))
                {
                    throw new ArgumentException(Properties.Resources.GeneralUtils_InvalidHexString);
                }

                ret.Add(val);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Return a version string for CANAPE
        /// </summary>
        /// <returns>The version string</returns>
        public static string GetCanapeVersionString()
        {
            return String.Format("v1.4");
        }

        /// <summary>
        /// Get the version number of CANAPE
        /// </summary>
        /// <returns>The version number</returns>
        public static Version GetCanapeVersion()
        {
            return new Version(1, 4);
        }

        /// <summary>
        /// Get the local directory for configuration, creating it if necessary
        /// </summary>
        /// <returns>The configuration directory, null if couldn't create</returns>
        public static string GetConfigDirectory()
        {
            return GetConfigDirectory(true);
        }

        /// <summary>
        /// Get the local directory for configuration
        /// </summary>
        /// <param name="create">True to create the directory</param>
        /// <returns>The configuration directory, null if couldn't create</returns>
        public static string GetConfigDirectory(bool create)
        {
            string appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Context Information Security", "CANAPE");

            if (create)
            {
                if (!Directory.Exists(appData))
                {
                    try
                    {
                        Directory.CreateDirectory(appData);
                    }
                    catch (IOException)
                    {
                        Logger.GetSystemLogger().LogError(Properties.Resources.GetConfigDir_ErrorCreatingDir);
                        appData = null;
                    }
                }
            }

            return appData;
        }

        /// <summary>
        /// Make a meta name which is private to a UUID
        /// </summary>
        /// <param name="uuid">The UUID</param>
        /// <param name="name">The name</param>
        /// <returns>The private name</returns>
        public static string MakePrivateMetaName(Guid uuid, string name)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0}_{1}", uuid, name);
        }

        /// <summary>
        /// Extend selection syntax for nodes to handle global and meta parameters through
        /// # and $ prefixes
        /// </summary>
        /// <param name="path">The path to select</param>
        /// <param name="frame">The data frame to select off</param>
        /// <param name="node">The current node</param>
        /// <param name="globalMeta">Global meta</param>
        /// <param name="properties">Property bag</param>
        /// <param name="uuid">The uuid for private names</param>
        /// <param name="meta">Meta</param>
        /// <returns>An array of nodes</returns>
        public static DataNode[] SelectNodes(string path, DataFrame frame, MetaDictionary meta, MetaDictionary globalMeta, 
            PropertyBag properties, Guid uuid, BasePipelineNode node)
        {
            DataNode[] nodes = new DataNode[0];

            path = path.Trim();

            if (path.StartsWith("#"))
            {
                if (meta != null)
                {
                    string name = path.Substring(1);
                    object n = meta.GetMeta(MakePrivateMetaName(uuid, name));
                    if(n == null)
                    {
                        n = meta.GetMeta(name);
                    }

                    if (n != null)
                    {
                        nodes = new DataNode[1];
                        nodes[0] = new StringDataValue(path, n.ToString());
                    }
                }
            }
            else if (path.StartsWith("$"))
            {
                if (globalMeta != null)
                {
                    string name = path.Substring(1);
                    object n = globalMeta.GetMeta(MakePrivateMetaName(uuid, name));
                    if (n == null)
                    {
                        n = globalMeta.GetMeta(name);
                    }

                    if (n != null)
                    {
                        nodes = new DataNode[1];
                        nodes[0] = new StringDataValue(path, n.ToString());
                    }
                }
            }
            else if (path.StartsWith("~"))
            {
                if(properties != null)
                {
                    string name = path.Substring(1);
                    dynamic val = properties.GetRelativeValue(name);

                    if (val != null)
                    {
                        nodes = new DataNode[1];
                        nodes[0] = new StringDataValue(path, val.ToString());
                    }
                }
            }
            else if (path.StartsWith("&"))
            {
                DataNode n = null;

                if (path.Equals("&incount", StringComparison.OrdinalIgnoreCase))
                {
                    n = new GenericDataValue<int>(path, node != null ? node.InputPacketCount : 0);
                }
                else if (path.Equals("&outcount", StringComparison.OrdinalIgnoreCase))
                {
                    n = new GenericDataValue<int>(path, node != null ? node.OutputPacketCount : 0);
                }
                else if (path.Equals("&bytecount", StringComparison.OrdinalIgnoreCase))
                {
                    n = new GenericDataValue<long>(path, node != null ? node.ByteCount : 0);
                }
                else if (path.Equals("&length", StringComparison.OrdinalIgnoreCase))
                {
                    n = new GenericDataValue<long>(path, frame.Length);
                }
                else if (path.Equals("&md5", StringComparison.OrdinalIgnoreCase))
                {
                    n = new StringDataValue(path, frame.Hash);
                }
                else if (path.Equals("&display", StringComparison.OrdinalIgnoreCase))
                {
                    n = new StringDataValue(path, frame.ToString());
                }

                if (n != null)
                {
                    nodes = new DataNode[1];
                    nodes[0] = n;
                }
            }
            else
            {
                nodes = frame.SelectNodes(path);
            }

            return nodes;
        }

        /// <summary>
        /// Select a single node
        /// </summary>
        /// <param name="path">The path to select</param>
        /// <param name="frame">The data frame to select off</param>
        /// <param name="globalMeta">Global meta</param>
        /// <param name="meta">Meta</param>
        /// <param name="properties">PropertyBag</param>
        /// <param name="uuid">Uuid to use for private values</param>
        /// <param name="node">The current node</param>
        /// <returns>The first selected node or null if not found</returns>
        public static DataNode SelectSingleNode(string path, DataFrame frame, MetaDictionary meta, MetaDictionary globalMeta, 
            PropertyBag properties, Guid uuid, BasePipelineNode node)
        {
            DataNode[] nodes = SelectNodes(path, frame, meta, globalMeta, properties, uuid, node);

            if (nodes.Length > 0)
            {
                return nodes[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a datetime to unix time
        /// </summary>
        /// <param name="time">The datetime structure</param>
        /// <exception cref="ArgumentException">If unix time would be invalid</exception>
        /// <returns>Unix time</returns>
        public static int ToUnixTime(DateTime time)
        {
            DateTime epoc = new DateTime(1970, 1, 1);
            DateTime maxTime = epoc.AddSeconds(int.MaxValue);

            if ((epoc.CompareTo(time) < 0) || (time.CompareTo(maxTime) >= 0))
            {
                throw new ArgumentException(String.Format(Properties.Resources.ToUnixTime_CannotConvert, time));
            }

            return (int)time.Subtract(epoc).TotalSeconds;
        }

        /// <summary>
        /// Converts a unix time to a datetime
        /// </summary>
        /// <param name="time">The number of seconds since 1/1/1970</param>
        /// <returns></returns>
        public static DateTime FromUnixTime(int time)
        {
            return new DateTime(1970, 1, 1).AddSeconds(time);
        }

        /// <summary>
        /// Converts encoding to an encoding object
        /// </summary>
        /// <param name="encoding">The encoding type</param>
        /// <returns>The encoding</returns>
        public static Encoding GetEncodingFromType(BinaryStringEncoding encoding)
        {
            Encoding ret;

            switch (encoding)
            {
                case BinaryStringEncoding.ASCII:
                    ret = new BinaryEncoding();
                    break;
                case BinaryStringEncoding.UTF16_BE:
                    ret = new UnicodeEncoding(true, false);
                    break;
                case BinaryStringEncoding.UTF16_LE:
                    ret = new UnicodeEncoding(false, false);
                    break;
                case BinaryStringEncoding.UTF32_BE:
                    ret = new UTF32Encoding(true, false);
                    break;
                case BinaryStringEncoding.UTF32_LE:
                    ret = new UTF32Encoding(false, false);
                    break;
                case BinaryStringEncoding.UTF8:
                    ret = new UTF8Encoding();
                    break;
                case BinaryStringEncoding.UTF7:
                    ret = new UTF7Encoding();
                    break;
                case BinaryStringEncoding.EBCDIC_US:
                    ret = Encoding.GetEncoding(37);
                    break;
                case BinaryStringEncoding.Latin1:
                    ret = Encoding.GetEncoding(28591);
                    break;
                case BinaryStringEncoding.ShiftJIS:
                    ret = Encoding.GetEncoding(932);
                    break;
                default:                                                                    
                    ret = Encoding.GetEncoding((int)encoding);
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Match two byte arrays
        /// </summary>
        /// <param name="data">The data to match</param>
        /// <param name="pos">The position in the data array</param>
        /// <param name="match">The match array</param>
        /// <returns>True if all bytes match</returns>
        public static bool MatchArray(byte[] data, int pos, byte[] match)
        {
            bool matched = true;

            if ((data.Length - pos) >= match.Length)
            {
                for (int i = 0; i < match.Length; ++i)
                {
                    if (data[pos + i] != match[i])
                    {
                        matched = false;
                        break;
                    }
                }
            }
            else
            {
                matched = false;
            }

            return matched;
        }

        /// <summary>
        /// Determines if a character is a hex character
        /// </summary>
        /// <param name="c">The character to test</param>
        /// <returns>True if it is hex</returns>
        public static bool IsHex(char c)
        {
            char lowerCase = Char.ToLowerInvariant(c);

            return (Char.IsDigit(c) || ((lowerCase >= 'a') && (lowerCase <= 'f')));
        }

        /// <summary>
        /// Format the data key syntax
        /// </summary>
        /// <param name="format">The format string</param>
        /// <param name="root">The root key to format from</param>
        /// <returns>The formatted string</returns>
        public static string FormatKeyString(string format, DataKey root)
        {
            if (!String.IsNullOrWhiteSpace(format))
            {
                MatchCollection coll = _formatRegex.Matches(format);
                StringBuilder builder = new StringBuilder(format);

                foreach (Match match in coll)
                {
                    DataNode[] nodes = root.SelectNodes(match.Value.Substring(1));
                    string value = String.Empty;

                    if (nodes.Length == 1)
                    {
                        value = nodes[0].ToString();
                    }
                    else if (nodes.Length > 1)
                    {
                        value = String.Join(",", nodes.Select(n => String.Format("[{0} = {1}]", n.Name, n.ToString())));
                    }

                    builder.Replace(match.Value, value);
                }

                return builder.ToString();
            }
            else
            {
                return root.ToString();
            }
        }

        /// <summary>
        /// Clone an object using a binary formatter
        /// </summary>
        /// <param name="o">The object to clone</param>
        /// <returns>The cloned object</returns>
        public static T CloneObject<T>(T o)
        {
            BinaryFormatter fmt = new BinaryFormatter();
            MemoryStream stm = new MemoryStream();

            fmt.Serialize(stm, o);
            stm.Position = 0;

            return (T)fmt.Deserialize(stm);
        }

        /// <summary>
        /// Get the first attribute of a specific type
        /// </summary>
        /// <typeparam name="T">The type of attribute to get</typeparam>
        /// <param name="mi">The member to get the attribute from</param>
        /// <returns>The attribute, null if not available</returns>
        public static T GetAttribute<T>(MemberInfo mi) where T : Attribute
        {
            object[] attrs = mi.GetCustomAttributes(typeof(T), false);

            if (attrs.Length > 0)
            {
                return (T)attrs[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to sanitize a string to a valid filename
        /// </summary>
        /// <param name="name">The name to sanitize</param>
        /// <param name="replaceChar">The character to replace invalid characters with</param>
        /// <returns>The sanitized string</returns>
        public static string SanitizeFilename(string name, char replaceChar)
        {
            StringBuilder builder = new StringBuilder();
            char[] invalidPathChars = Path.GetInvalidPathChars();

            foreach (char c in name)
            {
                if ((c != ':') && !invalidPathChars.Contains(c))
                {
                    builder.Append(c);
                }
                else
                {
                    builder.Append(replaceChar);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Get the install directory for CANAPE
        /// </summary>
        /// <returns>The install directory</returns>
        public static string GetCanapeInstallDirectory()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            return Path.GetDirectoryName(asm.Location);
        }

        /// <summary>
        /// Configure the current thread with the setup culture
        /// </summary>
        public static void SetThreadCulture()
        {            
            Thread.CurrentThread.CurrentUICulture = _culture;         
        }

        /// <summary>
        /// Decode a C# style escaped string
        /// </summary>
        /// <param name="s">The string to decode</param>
        /// <returns>The decoded string</returns>
        public static string DecodeEscapedString(string s)
        {
            if ((s.Length == 0) || !s.Contains('\\'))
            {
                return s;
            }
            else
            {
                StringBuilder builder = new StringBuilder(s);
                
                int pos = 0;

                while (pos < builder.Length)
                {
                    if (builder[pos] == '\\')
                    {
                        int charsLeft = builder.Length - pos - 1;

                        if (charsLeft > 0)
                        {
                            char val = '\0';

                            builder.Remove(pos, 1);

                            switch (builder[pos])
                            {
                                case 'n':
                                    val = '\n';
                                    break;
                                case 't':
                                    val = '\t';
                                    break;
                                case 'r':
                                    val = '\r';
                                    break;
                                case '0':
                                    val = '\0';
                                    break;
                                case '\\':
                                    val = '\\';
                                    break;
                                default:
                                    throw new FormatException(String.Format(Properties.Resources.GeneralUtils_DecodeEscapedInvalidEscape, builder[pos]));
                            }

                            builder[pos] = val;                            
                        }
                        else
                        {
                            throw new FormatException(Properties.Resources.GeneralUtils_DecodeEscapedStringTrailingSlash);
                        }
                    }                    
                    
                    pos++;                    
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Compare two byte arrays
        /// </summary>
        /// <param name="x">Byte array x</param>
        /// <param name="y">Byte array y</param>
        /// <returns>True if they are equal</returns>
        public static bool CompareBytes(byte[] x, byte[] y)
        {
            if (x.Length == y.Length)
            {
                bool ret = true;

                for (int i = 0; i < x.Length; ++i)
                {
                    if (x[i] != y[i])
                    {
                        ret = false;
                        break;
                    }
                }

                return ret;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get the hash code of a byte array
        /// </summary>
        /// <param name="ba">The array of bytes</param>
        /// <returns>The hash code</returns>
        public static int GetBytesHashCode(byte[] ba)
        {
            int hash = 27;

            foreach (byte b in ba)
            {
                hash = (hash * 13) + b;
            }            

            return hash;
        }
    }
}
