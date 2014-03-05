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
using System.Reflection;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// A class which can act as a portable enumeration
    /// </summary>
    [Serializable]
    public struct PortableEnum 
    {                
        // All values must fit within a long type
        private long _value;
        private Dictionary<string, long> _entries;
        private bool _isFlags;
        private string _name;

        /// <summary>
        /// Structure to hold an enumeration entry
        /// </summary>
        public class EnumEntry
        {
            /// <summary>
            /// The name of the entry
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// The value of the entry
            /// </summary>
            public long Value { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="name"></param>
            /// <param name="value"></param>
            public EnumEntry(string name, long value)
            {
                Name = name;
                Value = value;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Name;
            }
        }

        /// <summary>
        /// The value of the enumeration
        /// </summary>
        public long Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Indicates if this enumeration is a set of flags
        /// </summary>
        public bool IsFlags
        {
            get { return _isFlags; }
        }

        /// <summary>
        /// Indicates the name of the enumeration
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Get a list of entries this enumeration supports
        /// </summary>
        public IEnumerable<EnumEntry> Entries
        {
            get
            {
                List<EnumEntry> ents = new List<EnumEntry>();
                foreach(KeyValuePair<string, long> pair in _entries)
                {
                    ents.Add(new EnumEntry(pair.Key, pair.Value));
                }

                return ents;
            }
        }

        /// <summary>
        /// Constructor from an enumerated type
        /// </summary>
        /// <param name="enumType">A type representing an existing enumeration</param>
        /// <exception cref="System.ArgumentException">Throw if the type if not an enumerated type</exception>
        public PortableEnum(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Parameter is not an enumerated type");
            }

            _entries = new Dictionary<string, long>();
            string[] names = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);

            for (int i = 0; i < names.Length; ++i)
            {
                long v = (long)Convert.ChangeType(values.GetValue(i), typeof(long));                
                
                _entries.Add(names[i], v);
            }

            if (enumType.IsDefined(typeof(FlagsAttribute), false))
            {
                _isFlags = true;
            }
            else
            {
                _isFlags = false;
            }

            _value = 0;
            _name = enumType.Name;
        }

        /// <summary>
        /// Constructor from a dictionary of entries
        /// </summary>
        /// <param name="name">The name of the enum</param>
        /// <param name="enumEntries">A dictionary mapping names to longs</param>
        /// <param name="isFlags">Indicates if this is a bitflags enumeration</param>
        public PortableEnum(string name, Dictionary<string, long> enumEntries, bool isFlags)
        {
            _entries = new Dictionary<string, long>(enumEntries);
            _isFlags = isFlags;
            _value = 0;
            _name = name;
        }

        /// <summary>
        /// Create a new enumerated value based this value
        /// </summary>
        /// <param name="value">The value to use, must be convertable to a long</param>
        public PortableEnum FromValue(object value)
        {
            PortableEnum ret = this;

            if (value is string)
            {
                ret = FromString((string)value);
            }
            else if (value is PortableEnum)
            {
                ret._value = ((PortableEnum)value)._value;
            }
            else
            {                
                ret._value = (long)Convert.ChangeType(value, typeof(long));
            }

            return ret;
        }

        private static long ParseLongString(string value)
        {
            long ret = 0;

            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                ret = long.Parse(value.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            else
            {
                ret = long.Parse(value, CultureInfo.InvariantCulture);
            }

            return ret;
        }

        /// <summary>
        /// Create a new enumeration based on a string
        /// </summary>
        /// <param name="value">The enumerate string, flags are separated with | characters</param>
        public PortableEnum FromString(string value)
        {
            PortableEnum ret = this;

            if (_isFlags)
            {                
                string[] vals = value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                long currVal = 0;

                if (vals.Length > 0)
                {
                    foreach (string s in vals)
                    {
                        currVal |= FindValueByName(s);
                    }

                    ret._value = currVal;
                }
                else
                {
                    throw new ArgumentException(CANAPE.Properties.Resources.EnumDataValue_FromString, "value");
                }
            }
            else
            {
                ret._value = FindValueByName(value);
            }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private long FindValueByName(string name)
        {
            long ret = 0;
            name = name.Trim();
            if (name.Length > 0)
            {
                if (Char.IsLetter(name[0]))
                {
                    bool found = false;

                    foreach (KeyValuePair<string, long> pair in _entries)
                    {
                        if (name.Equals(pair.Key, StringComparison.OrdinalIgnoreCase))
                        {
                            found = true;
                            ret = pair.Value;
                            break;
                        }
                    }

                    if (!found)
                    {
                        throw new ArgumentException(String.Format(CANAPE.Properties.Resources.EnumDataValue_FindValueByName, name));
                    }
                }
                else
                {
                    ret = ParseLongString(name);   
                }
            }

            return ret;
        }

        private string FindNameByValue(long value)
        {
            string ret = null;

            foreach (KeyValuePair<string, long> pair in _entries)
            {
                if (pair.Value == value)
                {
                    ret = pair.Key;
                    break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Convert to a display string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ret = FindNameByValue(_value);

            if ((_isFlags) && (ret == null))
            {                
                // No absolute match
                if (ret == null)
                {
                    List<string> buildList = new List<string>();                    
                    long currValue = _value;

                    foreach (KeyValuePair<string, long> pair in _entries)
                    {
                        if (pair.Value != 0)
                        {
                            if ((currValue & pair.Value) == pair.Value)
                            {
                                buildList.Add(pair.Key);
                                currValue &= ~pair.Value;
                                if (currValue == 0)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    if (currValue != 0)
                    {
                        buildList.Add(String.Format(CultureInfo.InvariantCulture, "0x{0:X}", currValue));
                    }

                    if(buildList.Count > 0)
                    {
                        ret = String.Join(" | ", buildList);                        
                    }
                }
            }

            if (ret == null)
            {
                ret = String.Format(CultureInfo.InvariantCulture, "{0}", _value);
            }

            return ret;
        }

        /// <summary>
        /// Equals method
        /// </summary>
        /// <param name="obj">The object to compare against</param>
        /// <returns>True if the value is equals</returns>
        public override bool Equals(object obj)
        {
            if(obj is PortableEnum)
            {
                return _value.Equals(((PortableEnum)obj)._value);
            }
            else
            {
                return _value.Equals(obj);
            }            
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    /// <summary>
    /// A data value based on a portable enumerated type
    /// </summary>
    [Serializable]
    public class EnumDataValue : PrimitiveDataValue
    {
        MethodInfo _toMethodInfo;
        MethodInfo _fromMethodInfo;
        PortableEnum _value;
        Type _underlyingType;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override byte[] OnToArray()
        {            
            object realValue = Convert.ChangeType(_value.Value, _underlyingType);

            return (byte[])_toMethodInfo.Invoke(null, new object[] { realValue });            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        protected override void OnFromArray(byte[] data)
        {
            _value.Value = (long)Convert.ChangeType(_fromMethodInfo.Invoke(null, new object[] { data, 0 }), typeof(long));
        }

        private static byte[] FromByte(byte b)
        {
            return new byte[] { b };
        }

        private static byte[] FromSByte(sbyte b)
        {
            return (byte[])(Array)new sbyte[] { b };
        }

        private static byte ToByte(byte[] b, int p)
        {
            return b[p];
        }

        private static sbyte ToSByte(byte[] b, int p)
        {
            sbyte[] sb = (sbyte[])(Array)b;

            return sb[p];
        }

        /// <summary>
        /// Get the value
        /// </summary>
        public override dynamic Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = _value.FromValue(value);

                if (_value.IsFlags)
                {
                    Class = new Guid(DataNodeClasses.FLAGS_ENUM_NODE_CLASS);
                }
                else
                {
                    Class = new Guid(DataNodeClasses.ENUM_NODE_CLASS);
                }

                OnModified();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Enumerated value</param>
        /// <param name="name">Name of the value</param>
        /// <param name="underlyingType">The underlying type which this serialized to</param>        
        /// <param name="littleEndian">The endian of the underlying type</param>
        public EnumDataValue(string name, Enum value, Type underlyingType, bool littleEndian)
            : base(name, littleEndian)
        {            
            _value = new PortableEnum(value.GetType());
            _value.Value = (long)Convert.ChangeType(value, typeof(long));

            _underlyingType = underlyingType;
            
            if (_underlyingType == typeof(byte))
            {
                _toMethodInfo = typeof(EnumDataValue).GetMethod("FromByte", BindingFlags.Static | BindingFlags.NonPublic);
                _fromMethodInfo = typeof(EnumDataValue).GetMethod("ToByte", BindingFlags.Static | BindingFlags.NonPublic);
            }
            else if (_underlyingType == typeof(sbyte))
            {
                _toMethodInfo = typeof(EnumDataValue).GetMethod("FromSByte", BindingFlags.Static | BindingFlags.NonPublic);
                _fromMethodInfo = typeof(EnumDataValue).GetMethod("ToSByte", BindingFlags.Static | BindingFlags.NonPublic);
            }
            else
            {
                _toMethodInfo = typeof(BitConverter).GetMethod("GetBytes", BindingFlags.Public | BindingFlags.Static, null, new Type[] { _underlyingType }, null);

                foreach (MethodInfo mi in typeof(BitConverter).GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    if (mi.Name.StartsWith("To") && (mi.ReturnType == _underlyingType))
                    {
                        ParameterInfo[] pis = mi.GetParameters();
                        if ((pis.Length == 2) && (pis[0].ParameterType == typeof(byte[])) && (pis[1].ParameterType == typeof(int)))
                        {
                            _fromMethodInfo = mi;
                            break;
                        }
                    }
                }
            }

            if ((_toMethodInfo == null) || (_fromMethodInfo == null))
            {
                throw new ArgumentException(CANAPE.Properties.Resources.EnumDataValue_InvalidType);
            }

            if (_value.IsFlags)
            {
                Class = new Guid(DataNodeClasses.FLAGS_ENUM_NODE_CLASS);
            }
            else
            {
                Class = new Guid(DataNodeClasses.ENUM_NODE_CLASS);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the value</param>
        /// <param name="value">Enumerated value</param>
        public EnumDataValue(string name, Enum value)
            : this(name, value, Enum.GetUnderlyingType(value.GetType()), true)
        {
        }

        /// <summary>
        /// To display string
        /// </summary>
        /// <returns>The display string</returns>
        public override string ToString()
        {
            return _value.ToString();
        }

        /// <summary>
        /// Indicates whether this value has a fixed length
        /// </summary>
        public override bool FixedLength
        {
            get { return true; }
        }
    }
}
