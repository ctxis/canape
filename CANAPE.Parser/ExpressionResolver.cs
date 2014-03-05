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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using CANAPE.DataFrames;
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Parser
{
    /// <summary>
    /// A class which provides a expression resolver over an object
    /// </summary>
    public sealed class ExpressionResolver 
    {
        private Dictionary<string, Func<object, object>> _resolvers;
        private Dictionary<string, Func<object, object[], object>> _funcs;
        private Type _targetType;
        private Logger _logger;

        public ExpressionResolver(Type targetType, Logger logger) 
            : this(targetType, new Dictionary<string, Func<object, object>>(), new Dictionary<string,Func<object,object[],object>>())
        {
            _logger = logger ?? Logger.GetSystemLogger();
        }

        public ExpressionResolver(Type targetType)
            : this(targetType, null)
        {
        }

        private static StateDictionary CreateState(object target)
        {
            StateDictionary ret = new StateDictionary();

            IChildObject child = target as IChildObject;

            if (child != null)
            {
                ret.Add("parent", child.Parent);
            }

            return ret;
        }

        private byte[] ToBytes(object target, object o, bool littleEndian, Encoding encoding)
        {
            if (o is byte)
            {
                return new byte[1] { (byte)o };
            }
            else if (o is sbyte)
            {
                return (byte[])(object)(new sbyte[1] { (sbyte)o });
            }
            else if(o.GetType().IsPrimitive)
            {
                MemoryStream stm = new MemoryStream();
                DataWriter writer = new DataWriter(stm);

                writer.WritePrimitive(o, o.GetType(), littleEndian);

                return stm.ToArray();
            }
            else if (o is byte[])
            {
                return (byte[])o;
            }
            else if (o is Array)
            {                
                MemoryStream stm = new MemoryStream();
                foreach (object x in ((Array)o))
                {
                    byte[] ba = ToBytes(target, x, littleEndian, encoding);
                    stm.Write(ba, 0, ba.Length);
                }

                return stm.ToArray();
            }
            else if (o is IStreamTypeParser)
            {
                MemoryStream stm = new MemoryStream();
                DataWriter writer = new DataWriter(stm);
                IStreamTypeParser parser = (IStreamTypeParser)o;

                parser.ToStream(writer, CreateState(target), _logger);

                return stm.ToArray();
            }
            else if (o is string)
            {
                if(encoding != null)
                {
                    return encoding.GetBytes((string)o);
                }
                else
                {
                    return BinaryEncoding.Instance.GetBytes((string)o);
                }
            }
            
            return new byte[0];
        }

        private object DoSizeOf(object target, object[] args)
        {
            if (args.Length == 0)
            {
                return 0;
            }

            byte[] ba = ToBytes(target, args[0], false, null);

            return ba.Length;
        }        

        private object DoLen(object target, object[] args)
        {
            if (args.Length == 0)
            {
                return 0;
            }

            Array a = args[0] as Array;
            string s = args[0] as string;
            DataReader reader = args[0] as DataReader;
            IStreamTypeParser parser = args[0] as IStreamTypeParser;

            if (s != null)
            {
                return s.Length;
            }
            else if (a != null)
            {
                return a.Length;
            }
            else if (reader != null)
            {
                return reader.DataLeft;
            }
            else if (parser != null)
            {
                DataWriter writer = new DataWriter();

                parser.ToStream(writer, CreateState(target), _logger);

                return writer.BytesWritten;
            }

            return 0;
        }

        private object DoStr(object target, object[] args)
        {
            if (args.Length == 0)
            {
                return "";
            }

            if ((args.Length > 1) && (args[1] is IFormattable))
            {
                IFormattable fmt = args[0] as IFormattable;

                return fmt.ToString(args[1].ToString(), CultureInfo.InvariantCulture);
            }
            else
            {
                return args[0].ToString();
            }            
        }

        private object DoInt(object target, object[] args)
        {
            if (args.Length == 0)
            {
                return 0;
            }
            else
            {                
                int baseNo = 10;

                if (args.Length > 1)
                {
                    baseNo = (int)args[1];
                }

                try
                {
                    return Convert.ToInt32(args[0].ToString(), baseNo);
                }
                catch (Exception)
                {
                    return Convert.ToInt64(args[0].ToString(), baseNo);
                }
            }
        }

        private object DoUInt(object target, object[] args)
        {
            if (args.Length == 0)
            {
                return 0;
            }
            else
            {
                int baseNo = 10;

                if (args.Length > 1)
                {
                    baseNo = (int)args[1];
                }

                try
                {
                    return Convert.ToUInt32(args[0].ToString(), baseNo);
                }
                catch (Exception)
                {
                    return Convert.ToUInt64(args[0].ToString(), baseNo);
                }
            }
        }

        private object DoAt(object target, object[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException(Properties.Resources.ExpressionResolver_AtRequires2Arguments);
            }
            else
            {
                int index = Convert.ToInt32(args[1]);
                object res = null;

                if (args[0] is IList)
                {
                    res = ((IList)args[0])[index];
                    if (res is char)
                    {
                        res = new String(new char[] { (char)res });
                    }
                }
                else if (args[0] is string)
                {
                    res = ((string)args[0]).Substring(index, 1);
                }
                else
                {
                    throw new ArgumentException(Properties.Resources.ExpressionResolver_AtTargetNeedsToBeIndexable);
                }

                return res;
            }
        }

        private object DoToLower(object target, object[] args)
        {
            if (args.Length > 0)
            {
                return args[0].ToString().ToLower();
            }
            else
            {
                return String.Empty;
            }
        }

        private object DoToUpper(object target, object[] args)
        {
            if (args.Length > 0)
            {
                return args[0].ToString().ToUpper();
            }
            else
            {
                return String.Empty;
            }
        }

        private object DoPy(object target, object[] args)
        {
            if (args.Length > 0)
            {
                PySnippet snippet = new PySnippet(args[0].ToString());
                object[] newargs = new object[args.Length - 1];

                Array.Copy(args, 1, newargs, 0, args.Length - 1);

                return snippet.Invoke(target, null, newargs);
            }
            else
            {
                return null;
            }
        }

        private dynamic DoFormat(object target, object[] args)
        {
            if (args.Length > 0)
            {
                string fmt = args[0].ToString();

                object[] newargs = new object[args.Length - 1];

                Array.Copy(args, 1, newargs, 0, args.Length - 1);

                return String.Format(fmt, newargs);
            }
            else
            {
                return String.Empty;
            }
        }

        private dynamic DoHex(object target, object[] args)
        {            
            if (args.Length > 0)
            {
                if(args[0] is byte[])
                {
                    List<string> values = new List<string>();

                    foreach (byte b in ((byte[])args[0]))
                    {
                        values.Add(String.Format("{0:X02}", b));
                    }

                    string sep = String.Empty;

                    if (args.Length > 1)
                    {
                        sep = args[1].ToString();                        
                    }

                    return String.Join(sep, values);
                }
                else if (args[0].GetType().IsPrimitive)
                {
                    return String.Format("{0:X}", args[0]);
                }
                else
                {
                    return String.Empty;
                }
            }
            else
            {
                return String.Empty;
            }
        }

        private dynamic DoHash(object target, object[] args, Func<byte[], int, int, dynamic> hash)
        {
            if (args.Length > 0)
            {
                byte[] data = ToBytes(target, args[0], false, null);
                int ofs = 0;
                int len = data.Length;

                if (args.Length > 1)
                {
                    ofs = Convert.ToInt32(args[1]);
                    len = data.Length - ofs;
                }

                if (args.Length > 2)
                {
                    len = Convert.ToInt32(args[2]);
                }

                return hash(data, ofs, len);
            }
            else
            {
                return hash(new byte[0], 0, 0);
            }
        }

        private dynamic DoCrc32(object target, object[] args)
        {
            return DoHash(target, args, (ba,o,l) => Crc32.ComputeChecksum(ba, o, l));
        }

        private dynamic DoMd5(object target, object[] args)
        {            
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                return DoHash(target, args, md5.ComputeHash);
            }        
        }

        private dynamic DoSha1(object target, object[] args)
        {
            using (SHA1 sha1 = new SHA1CryptoServiceProvider())
            {
                return DoHash(target, args, sha1.ComputeHash);
            }
        }

        private static dynamic ComputeCheck(byte[] ba, int offset, int length)
        {
            uint ret = 0;

            for(int i = offset; i < length; ++i)
            {
                ret += ba[i];                
            }

            return ret;
        }

        private dynamic DoChk(object target, object[] args)
        {
            return DoHash(target, args, (ba, o, l) => ComputeCheck(ba, o, l));
        }

        private dynamic DoCmpb(object target, object[] args)
        {
            if (args.Length > 1)
            {
                byte[] a = ToBytes(target, args[0], false, null);
                byte[] b = ToBytes(target, args[1], false, null);

                return GeneralUtils.CompareBytes(a, b);
            }

            return false;
        }

        private dynamic DoBytes(object target, object[] args)
        {
            if (args.Length > 0)
            {
                bool littleEndian = false;
                Encoding encoding = null;

                if (args.Length > 1)
                {
                    if (args[1] is bool)
                    {
                        littleEndian = (bool)args[1];
                    }
                    else if (args[1] is string)
                    {
                        string enc = (string)args[1];

                        if (enc.Equals(BinaryEncoding.Instance.EncodingName, StringComparison.OrdinalIgnoreCase))
                        {
                            encoding = null;
                        }
                        else
                        {
                            encoding = Encoding.GetEncoding(enc);
                        }
                    }
                }

                return ToBytes(target, args[0], littleEndian, encoding);
            }
            else
            {
                return new byte[0];
            }
        }

        public ExpressionResolver(Type targetType, 
            Dictionary<string, Func<object, object>> resolvers, 
            Dictionary<string, Func<object, object[], object>> funcs)
        {
            _targetType = targetType;
            _resolvers = new Dictionary<string,Func<object,object>>(resolvers);
            _funcs = new Dictionary<string, Func<object, object[], object>>(funcs);

            _funcs.Add("countof", DoLen);
            _funcs.Add("len", DoLen);
            _funcs.Add("sizeof", DoSizeOf);
            _funcs.Add("str", DoStr);
            _funcs.Add("int", DoInt);
            _funcs.Add("uint", DoUInt);
            _funcs.Add("at", DoAt);
            _funcs.Add("tolower", DoToLower);
            _funcs.Add("toupper", DoToUpper);
            _funcs.Add("py", DoPy);
            _funcs.Add("format", DoFormat);
            _funcs.Add("hex", DoHex);
            _funcs.Add("crc32", DoCrc32);
            _funcs.Add("sha1", DoSha1);
            _funcs.Add("md5", DoMd5);
            _funcs.Add("chk", DoChk);
            _funcs.Add("cmpb", DoCmpb);
            _funcs.Add("bytes", DoBytes);
        }

        public dynamic Resolve(object obj, string expr, bool caseSensitive)
        {
            Expression e = new Expression(expr, caseSensitive ? EvaluateOptions.None : EvaluateOptions.IgnoreCase);

            e.EvaluateFunction += (name, args) => EvaluateFunction(obj, name, args, null, caseSensitive);
            e.EvaluateParameter += (name, args) => args.Result = EvaluateParameter(obj, name, null, caseSensitive);

            return e.Evaluate();
        }

        public dynamic Resolve(object obj, string expr)
        {
            return Resolve(obj, expr, true);
        }

        public dynamic Resolve(object obj, string expr, Dictionary<string, object> extras)
        {
            return Resolve(obj, expr, extras, true);
        }

        public dynamic Resolve(object obj, string expr, Dictionary<string, object> extras, bool caseSensitive)
        {
            Expression e = new Expression(expr, caseSensitive ? EvaluateOptions.None : EvaluateOptions.IgnoreCase);

            e.EvaluateFunction += (name, args) => EvaluateFunction(obj, name, args, extras, caseSensitive);
            e.EvaluateParameter += (name, args) => args.Result = EvaluateParameter(obj, name, extras, caseSensitive);

            return e.Evaluate();
        }

        private static BindingFlags GetFlags(bool caseSensitive)
        {
            BindingFlags ret = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

            if (!caseSensitive)
            {
                ret = ret | BindingFlags.IgnoreCase;
            }

            return ret;
        }

        void EvaluateFunction(object target, string name, FunctionArgs args, Dictionary<string, object> extras, bool caseSensitive)
        {
            if (!caseSensitive)
            {
                name = name.ToLowerInvariant();
            }

            // If a dotted name then do a slow evaluation, for functions we can only have a list of properties then a function call
            if (!_funcs.ContainsKey(name) && name.Contains("."))
            {
                string[] parts = name.Split('.');
                object ret = EvaluateParameter(target, parts[0], extras, caseSensitive);

                for (int i = 1; i < parts.Length - 1; ++i)
                {
                    ret = GetValue(ret, parts[i], caseSensitive);
                }

                object[] oa = args.EvaluateParameters();

                Type type = ret as Type;

                if ((type != null) && typeof(PySnippet).IsAssignableFrom(type))
                {
                    PySnippet snippet = (PySnippet)Activator.CreateInstance(type);

                    _funcs.Add(name, (o, a) => snippet.Invoke(o, parts[parts.Length-1], a));

                    args.Result = _funcs[name](target, oa);
                }
                else
                {
                    ret = ret.GetType();
                    Type[] ta = new Type[oa.Length];

                    for (int i = 0; i < oa.Length; ++i)
                    {
                        ta[i] = oa[i].GetType();
                    }

                    MethodInfo method = null;
                    try
                    {
                        method = type.GetMethod(parts[parts.Length - 1], GetFlags(caseSensitive), null, ta, null);
                    }
                    catch (AmbiguousMatchException)
                    {
                    }

                    if (method == null)
                    {
                        throw new ArgumentException(String.Format(Properties.Resources.ExpressionResolver_CannotFuncFunction, name));
                    }

                    args.Result = method.Invoke(ret, oa);
                }
            }
            else
            {
                if (!_funcs.ContainsKey(name))
                {
                    if (_targetType == null)
                    {
                        throw new ArgumentException(String.Format(Properties.Resources.ExpressionResolver_CannotFuncFunction, name));
                    }

                    Type baseType = _targetType;

                    MethodInfo method = baseType.GetMethod(name, GetFlags(caseSensitive));
                    if (method == null)
                    {
                        // Not a method, try a type name which we expect to be a script
                        Type type = baseType.Assembly.GetType(name);

                        if ((type == null) || !typeof(PySnippet).IsAssignableFrom(type))
                        {
                            throw new ArgumentException(String.Format(Properties.Resources.ExpressionResolver_CannotFuncFunction, name));
                        }

                        PySnippet snippet = (PySnippet)Activator.CreateInstance(type);                        

                        _funcs.Add(name, (o, a) => snippet.Invoke(o, null, a));
                    }
                    else
                    {
                        _funcs.Add(name, (o, a) => method.Invoke(o, a));
                    }
                }

                args.Result = _funcs[name](target, args.EvaluateParameters());
            }
        }

        static object GetValue(object target, string name, bool caseSensitive)
        {
            Type baseType;

            if (target is Type)
            {
                baseType = (Type)target;
            }
            else
            {
                baseType = target.GetType();
            }

            PropertyInfo prop = baseType.GetProperty(name, GetFlags(caseSensitive));

            if (prop == null)
            {
                FieldInfo field = baseType.GetField(name, GetFlags(caseSensitive));
                if (field == null)
                {
                    // If a parser type then see if we can pull the value from its state dictionary
                    BaseParser targetParser = target as BaseParser;
                    if(targetParser != null)
                    {
                        StateDictionary dict = targetParser.GetState();
                        if (dict.ContainsKey(name))
                        {
                            return dict[name];
                        }
                    }

                    throw new ArgumentException(String.Format(Properties.Resources.ExpressionResolver_CannotFindProperty, name));
                }

                return field.GetValue(target);
            }
            else
            {
                return prop.GetValue(target, null);
            }
        }
        
        object EvaluateParameter(object target, string name, Dictionary<string, object> extras, bool caseSensitive)
        {
            object ret;

            if (!caseSensitive)
            {
                name = name.ToLowerInvariant();
            }

            if (name.Equals("this") || name.Equals("self"))
            {
                return target;
            }            
            else if (name.Equals("null"))
            {
                return null;
            }
            // If a dotted name then do a slow evaluation
            else if (name.Contains("."))
            {
                string[] parts = name.Split('.');
                ret = EvaluateParameter(target, parts[0], extras, caseSensitive);

                for (int i = 1; i < parts.Length; ++i)
                {
                    ret = GetValue(ret, parts[i], caseSensitive);
                }
            }
            else
            {
                if (extras != null && extras.ContainsKey(name))
                {
                    return extras[name];
                }
                else if (!_resolvers.ContainsKey(name))
                {
                    if (_targetType == null)
                    {
                        throw new ArgumentException(String.Format(Properties.Resources.ExpressionResolver_CannotFindProperty, name));
                    }

                    Type baseType = _targetType;

                    PropertyInfo prop = baseType.GetProperty(name, GetFlags(caseSensitive));

                    if (prop == null)
                    {
                        FieldInfo field = baseType.GetField(name, GetFlags(caseSensitive));
                        if (field == null)
                        {
                            // So no field either, try for type names in the target type's assembly
                            Type t = _targetType.Assembly.GetType(name, false, caseSensitive);

                            if (t == null)
                            {
                                throw new ArgumentException(String.Format(Properties.Resources.ExpressionResolver_CannotFindProperty, name));
                            }
                            else
                            {
                                _resolvers.Add(name, o => t);
                            }
                        }
                        else
                        {
                            _resolvers.Add(name, o => field.GetValue(o));
                        }
                    }
                    else
                    {
                        _resolvers.Add(name, o => prop.GetValue(o, null));
                    }
                }

                ret = _resolvers[name](target);
            }

            // If a primitive value or enum then unwrap for purposes of processing            
            if (ret is IPrimitiveValue)
            {
                return ((IPrimitiveValue)ret).Value;
            }
            else if (ret is Enum)
            {                
                return Convert.ToInt64(ret);
            }
            else
            {
                return ret;
            }
        }
    }
}
