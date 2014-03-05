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
using System.Collections.Concurrent;
using System.Reflection;

namespace CANAPE.DataFrames
{

    /// <summary>
    /// A generic class to hold a primitive data value
    /// </summary>
    [Serializable]
    public class GenericDataValue<T> : PrimitiveDataValue where T : struct
    {
        static ConcurrentDictionary<Type, Tuple<MethodInfo, MethodInfo>> _conversion =
            new ConcurrentDictionary<Type, Tuple<MethodInfo, MethodInfo>>();

        MethodInfo _toMethodInfo;
        MethodInfo _fromMethodInfo;
        T _value;

        /// <summary>
        /// Converts the value to an array
        /// </summary>
        /// <returns></returns>
        protected override byte[] OnToArray()
        {
            object realValue = Convert.ChangeType(_value, typeof(T));

            return (byte[])_toMethodInfo.Invoke(null, new object[] { realValue });
        }

        /// <summary>
        /// Convert an array to the value
        /// </summary>
        /// <param name="data"></param>
        protected override void OnFromArray(byte[] data)
        {
            _value = (T)Convert.ChangeType(_fromMethodInfo.Invoke(null, new object[] { data, 0 }), typeof(T));
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
        /// Get the current value
        /// </summary>
        public override dynamic Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = (T)Convert.ChangeType(value, typeof(T));
                OnModified();
            }
        }

        /// <summary> 
        /// Constructor with a specific endian
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="name">The name of the value</param>        
        /// <param name="littleEndian">True for little endian, False for big endian</param>
        public GenericDataValue(string name, T value, bool littleEndian)
            : base(name, littleEndian)
        {
            _value = value;

            Tuple<MethodInfo, MethodInfo> methods;

            // Cache the reflection lookups
            if (!_conversion.TryGetValue(typeof(T), out methods))
            {
                if (typeof(T) == typeof(byte))
                {
                    _toMethodInfo = typeof(EnumDataValue).GetMethod("FromByte", BindingFlags.Static | BindingFlags.NonPublic);
                    _fromMethodInfo = typeof(EnumDataValue).GetMethod("ToByte", BindingFlags.Static | BindingFlags.NonPublic);
                }
                else if (typeof(T) == typeof(sbyte))
                {
                    _toMethodInfo = typeof(EnumDataValue).GetMethod("FromSByte", BindingFlags.Static | BindingFlags.NonPublic);
                    _fromMethodInfo = typeof(EnumDataValue).GetMethod("ToSByte", BindingFlags.Static | BindingFlags.NonPublic);
                }
                else
                {
                    _toMethodInfo = typeof(BitConverter).GetMethod("GetBytes", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(T) }, null);

                    foreach (MethodInfo mi in typeof(BitConverter).GetMethods(BindingFlags.Public | BindingFlags.Static))
                    {
                        if (mi.Name.StartsWith("To") && (mi.ReturnType == typeof(T)))
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

                _conversion[typeof(T)] = Tuple.Create<MethodInfo, MethodInfo>(_toMethodInfo, _fromMethodInfo);
            }
            else
            {
                _toMethodInfo = methods.Item1;
                _fromMethodInfo = methods.Item2;
            }

            if ((_toMethodInfo == null) || (_fromMethodInfo == null))
            {
                throw new ArgumentException(CANAPE.Properties.Resources.GenericDataValue_InvalidType);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the value</param>
        /// <param name="value">The value</param>
        public GenericDataValue(string name, T value)
            : this(name, value, true)
        {
        }

        /// <summary>
        /// To display string
        /// </summary>
        /// <returns>The display string</returns>
        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(FormatString))
            {
                return _value.ToString();
            }
            else
            {
                return String.Format(FormatString, _value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool FixedLength
        {
            get { return true; }
        }
    }

}
