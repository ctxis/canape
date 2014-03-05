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
using System.Net;
using System.Numerics;
using System.Reflection;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    /// <summary>
    /// A class to convert an object via reflection 
    /// to a DataFrame tree and back again
    /// </summary>
    public static class ObjectConverter
    {
        private static bool IsHidden(MemberInfo member)
        {
            object[] attrs = member.GetCustomAttributes(typeof(HiddenMemberAttribute), true);

            if (attrs.Length > 0)
            {
                HiddenMemberAttribute attr = (HiddenMemberAttribute)attrs[0];

                return attr.Hidden;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get whether a member is read only
        /// </summary>
        /// <param name="member">The member to check</param>
        /// <returns>True if read only</returns>
        public static bool IsReadOnly(MemberInfo member)
        {
            object[] attrs = member.GetCustomAttributes(typeof(ReadOnlyMemberAttribute), true);

            if (attrs.Length > 0)
            {
                return ((ReadOnlyMemberAttribute)attrs[0]).ReadOnly;
            }

            return false;
        }

        /// <summary>
        /// Get format string for member
        /// </summary>
        /// <param name="member">The member</param>
        /// <returns>The format string, null if not available</returns>
        public static string GetFormatString(MemberInfo member)
        {
            object[] attrs = member.GetCustomAttributes(typeof(FormatStringAttribute), true);

            if (attrs.Length > 0)
            {
                FormatStringAttribute attr = (FormatStringAttribute)attrs[0];

                return attr.FormatString;
            }
            else
            {
                return null;
            }
        }

        private static Guid GetMemberDisplayClass(MemberInfo member)
        {
            Guid ret = Guid.Empty;
            object[] attrs = member.GetCustomAttributes(typeof(DisplayClassAttribute), true);

            if (attrs.Length > 0)
            {
                DisplayClassAttribute attr = (DisplayClassAttribute)attrs[0];

                ret = attr.DisplayClass;
            }
            else
            {
                if (member is FieldInfo)
                {
                    ret = GetMemberDisplayClass(((FieldInfo)member).FieldType);
                }
                else if (member is PropertyInfo)
                {
                    ret = GetMemberDisplayClass(((PropertyInfo)member).PropertyType);
                }
            }

            return ret;
        }

        private static bool IsKeyDataPair(Type valueType)
        {
            return valueType.IsGenericType && (valueType.GetGenericTypeDefinition() == typeof(KeyDataPair<>));
        }

        private static DataValue GetPrimitiveType(string name, Type t, object o)
        {
            Type genType = typeof(GenericDataValue<>).MakeGenericType(t);

            DataValue ret = (DataValue)Activator.CreateInstance(genType, name, o);

            return ret;
        }

        private static void GetComplexType(DataKey key, Type t, object o, Dictionary<Type, Guid> derivedTypes)
        {
            PropertyInfo[] props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                if (!IsHidden(prop))
                {
                    DataNode node = GetNode(prop.Name, prop.GetValue(o, null), derivedTypes, IsReadOnly(prop));
                    if (node == null)
                    {
                        continue;
                    }

                    DataValue v = node as DataValue;
                    if (v != null)
                    {
                        v.FormatString = GetFormatString(prop);
                    }

                    node.DisplayClass = GetMemberDisplayClass(prop);                    

                    key.AddSubNode(node);
                }
            }

            FieldInfo[] fields = t.GetFields();

            foreach (FieldInfo field in fields)
            {
                if (!IsHidden(field))
                {
                    DataNode node = GetNode(field.Name, field.GetValue(o), derivedTypes, IsReadOnly(field));
                    if (node == null)
                    {
                        continue;
                    }

                    DataValue v = node as DataValue;
                    if (v != null)
                    {
                        v.FormatString = GetFormatString(field);
                    }

                    node.DisplayClass = GetMemberDisplayClass(field);

                    key.AddSubNode(node);
                }
            }

            // Annotate with type GUID (if such a thing exists)
            if (derivedTypes.ContainsKey(t))
            {
                key.Class = derivedTypes[t];
            }

            key.FormatString = GetFormatString(t);
        }

        private static void GetArrayType(DataKey key, Type t, object o, Dictionary<Type, Guid> derivedTypes, bool readOnly)
        {
            Array arr = (Array)o;

            for (int i = 0; i < arr.Length; ++i)
            {
                string name;
                object value = arr.GetValue(i);
                Type valuetype = value.GetType();

                if (IsKeyDataPair(valuetype))
                {
                    Type g = value.GetType();
                    name = (string)g.GetProperty("Name").GetValue(value, null);
                    value = g.GetProperty("Value").GetValue(value, null);
                }
                else
                {
                    name = String.Format("Item{0}", i);
                }
                
                key.AddSubNode(GetNode(name, value, derivedTypes, readOnly));
            }
        }

        private static DataNode GetNode(string name, object o, Dictionary<Type, Guid> derivedTypes, bool readOnly)
        {
            DataNode ret = null;

            if (o != null)
            {
                Type t = o.GetType();

                if (o is DataNode)
                {
                    ret = ((DataNode)o).CloneNode();
                }
                else if (t.IsEnum)
                {
                    ret = new EnumDataValue(name, (Enum)o);
                }
                else if (t.IsPrimitive)
                {
                    ret = GetPrimitiveType(name, t, o);
                }
                else if (o is IPrimitiveValue)
                {
                    object v = ((IPrimitiveValue)o).Value;

                    ret = GetPrimitiveType(name, v.GetType(), v);
                }
                else if (t == typeof(DateTime))
                {
                    // Use a unix datetime, doesn't _really_ matter
                    ret = new UnixDateTimeDataValue(name, false, (DateTime)o);
                }
                else if (t == typeof(string))
                {
                    ret = new StringDataValue(name, (string)o);
                }
                else if (t == typeof(BigInteger))
                {
                    ret = new BigIntegerDataValue(name, (BigInteger)o);
                }
                else if (t.IsArray)
                {
                    if (t.GetElementType() == typeof(byte))
                    {
                        byte[] newArr = (byte[])((byte[])o).Clone();
                        ret = new ByteArrayDataValue(name, (byte[])o);
                    }
                    else if (t.GetElementType() == typeof(char))
                    {
                        ret = new StringDataValue(name, new string((char[])o));
                    }
                    else
                    {
                        DataKey key = new DataKey(name);

                        GetArrayType(key, t, o, derivedTypes, readOnly);

                        ret = key;
                    }
                }
                else if (t == typeof(IPAddress))
                {
                    ret = new IPAddressDataValue(name, (IPAddress)o);
                }
                else
                {
                    INodeConverter converter = o as INodeConverter;

                    if (converter != null)
                    {
                        ret = converter.ToNode(name);
                    }
                    else
                    {
                        DataKey key = new DataKey(name);

                        GetComplexType(key, t, o, derivedTypes);

                        ret = key;
                    }
                }

                if (ret != null)
                {
                    ret.Readonly = readOnly;
                }
            }            

            return ret;
        }

        /// <summary>
        /// Convert an object to a tree
        /// </summary>
        /// <remarks>Converts all public properties and fields on the object a data frame tree</remarks>
        /// <param name="name">Root name of tree</param>
        /// <param name="o">The object to convert</param>
        /// <returns>The DataNode which represents the object</returns>
        public static DataNode ToNode(string name, object o)
        {
            Dictionary<Type, Guid> derivedTypes = new Dictionary<Type, Guid>();
            Type objType = o.GetType();

            derivedTypes[objType] = objType.GUID;

            return GetNode(name, o, derivedTypes, o != null ? IsReadOnly(objType) : false);
        }

        /// <summary>
        /// Convert an object to a tree, putting it into an existing DataKey
        /// </summary>
        /// <remarks>Converts all public properties and fields on the object a data frame tree</remarks>
        /// <param name="key">Root key</param>
        /// <param name="o">The object to convert</param> 
        /// <param name="derivedTypes">Dictionary of derived types</param>
        public static void ToNode(DataKey key, object o, Dictionary<Type, Guid> derivedTypes)
        {
            if (o != null)
            {
                Dictionary<Type, Guid> dict = new Dictionary<Type, Guid>(derivedTypes);

                // Ensure top object is in the list
                dict[o.GetType()] = o.GetType().GUID;

                GetComplexType(key, o.GetType(), o, dict);
            }
        }

        /// <summary>
        /// Convert an object to a tree, putting it into an existing DataKey
        /// </summary>
        /// <remarks>Converts all public properties and fields on the object a data frame tree</remarks>
        /// <param name="key">Root key</param>
        /// <param name="o">The object to convert</param> 
        /// <param name="derivedTypes">Dictionary of derived types</param>
        public static void ToNode(DataKey key, object o, params Type[] derivedTypes)
        {
            if (o != null)
            {
                Dictionary<Type, Guid> dict = new Dictionary<Type, Guid>();

                foreach (Type t in derivedTypes)
                {
                    dict[t] = t.GUID;
                }

                // Annotate for this type as well
                dict[o.GetType()] = o.GetType().GUID;

                GetComplexType(key, o.GetType(), o, dict);
            }
        }

        /// <summary>
        /// Convert an object to a tree, putting it into an existing DataKey
        /// </summary>
        /// <remarks>Converts all public properties and fields on the object a data frame tree</remarks>
        /// <param name="key">Root key</param>
        /// <param name="o">The object to convert</param>        
        public static void ToNode(DataKey key, object o)
        {
            ToNode(key, o, new Dictionary<Type, Guid>());
        }

        /// <summary>
        /// Convert an object to a tree
        /// </summary>
        /// <remarks>Converts all public properties and fields on the object a data frame tree</remarks>
        /// <param name="name">Root name of tree</param>
        /// <param name="o">The object to convert</param>
        /// <param name="derivedTypes">Type to guid mappings for handling derived types</param>
        /// <returns>The DataNode which represents the object</returns>
        public static DataNode ToNode(string name, object o, Dictionary<Type, Guid> derivedTypes)
        {
            if (o != null)
            {
                Dictionary<Type, Guid> dict = new Dictionary<Type, Guid>(derivedTypes);

                // Ensure top object is in the list
                dict[o.GetType()] = o.GetType().GUID;

                return GetNode(name, o, dict, IsReadOnly(o.GetType()));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert an object to a tree
        /// </summary>
        /// <remarks>Converts all public properties and fields on the object a data frame tree</remarks>
        /// <param name="name">Root name of tree</param>
        /// <param name="o">The object to convert</param>
        /// <param name="derivedTypes">Type to guid mappings for handling derived types</param>
        /// <returns>The DataNode which represents the object</returns>
        public static DataNode ToNode(string name, object o, params Type[] derivedTypes)
        {
            if (o != null)
            {
                Dictionary<Type, Guid> dict = new Dictionary<Type, Guid>();

                foreach (Type t in derivedTypes)
                {
                    dict[t] = t.GUID;
                }

                dict[o.GetType()] = o.GetType().GUID;

                return GetNode(name, o, dict, IsReadOnly(o.GetType()));
            }
            else
            {
                return null;
            }
        }

        private static Type GetTypeFromNode(Type baseType, DataNode node, Dictionary<Guid, Type> derivedTypes)
        {
            Type ret = baseType;

            if (derivedTypes.ContainsKey(node.Class))
            {
                ret = derivedTypes[node.Class];
    
                if(!baseType.IsAssignableFrom(ret))
                {
                    throw new ArgumentException(String.Format(Properties.Resources.ObjectConverter_GetTypeFromNode, ret, baseType));
                }
            }

            return ret;
        }

        private static void PopulateObject(DataKey key, object o, Dictionary<Guid, Type> derivedTypes)
        {            
            Type baseType = o.GetType();

            PropertyInfo[] props = baseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                if (!IsHidden(prop))
                {
                    DataNode node = key.SelectSingleNode(prop.Name);

                    if (node != null)
                    {
                        prop.SetValue(o, GetObject(node, prop.PropertyType, derivedTypes, o), null);
                    }
                }
            }

            FieldInfo[] fields = baseType.GetFields();
            foreach (FieldInfo field in fields)
            {
                if (!IsHidden(field))
                {
                    DataNode node = key.SelectSingleNode(field.Name);

                    if (node != null)
                    {
                        field.SetValue(o, GetObject(node, field.FieldType, derivedTypes, o));
                    }
                }
            }
        }

        private static void SetParent(object target, object parent)
        {
            IChildObject child = target as IChildObject;

            if (child != null)
            {
                child.Parent = parent;
            }
        }

        private static Array GetArray(DataKey key, Type baseType, Dictionary<Guid, Type> derivedTypes, object parent)
        {
            Array ret = Array.CreateInstance(baseType, key.SubNodes.Count);          

            if(IsKeyDataPair(baseType))
            {
                for(int i = 0; i < ret.Length; ++i)
                {
                    DataNode node = key.SubNodes[i];
                    object o = Activator.CreateInstance(baseType, 
                        node.Name, GetObject(node, baseType.GetGenericArguments()[0], derivedTypes, parent));

                    ret.SetValue(o, i); 
                }
            }
            else
            {
                for (int i = 0; i < ret.Length; ++i)
                {
                    DataNode node = key.SubNodes[i];
                    object o = GetObject(node, baseType, derivedTypes, parent);

                    ret.SetValue(o, i);
                }
            }

            return ret;
        }

        private static object GetObject(DataNode root, Type baseType, Dictionary<Guid, Type> derivedTypes, object parent)
        {
            object ret = null;

            if (typeof(DataNode).IsAssignableFrom(baseType))
            {
                ret = root.CloneNode();
            }
            else if (baseType == typeof(byte[]))
            {
                ret = root.ToArray();
            }
            else if (baseType == typeof(char[]))
            {
                ret = root.ToDataString().ToCharArray();
            }
            else if (baseType == typeof(string))
            {    
                ret = root.ToDataString();
            }           
            else if (typeof(IPrimitiveValue).IsAssignableFrom(baseType))
            {
                IPrimitiveValue prim = (IPrimitiveValue)Activator.CreateInstance(baseType);
                DataValue value = root as DataValue;
                if (value == null)
                {
                    throw new ArgumentException(Properties.Resources.ObjectConverter_GetObjectPrimitive);
                }

                prim.Value = value.Value;

                ret = prim;
            }
            else if (baseType == typeof(DateTime))
            {
                DateTimeDataValue value = root as DateTimeDataValue;
                if (value == null)
                {
                    throw new ArgumentException(Properties.Resources.ObjectConverter_GetObjectDateTime);
                }

                ret = value.Value;
            }
            else if (baseType == typeof(BigInteger))
            {
                BigIntegerDataValue value = root as BigIntegerDataValue;
                if (value != null)
                {
                    ret = value.Value;
                }
                else
                {                    
                    ret = new BigInteger(root.ToArray());                    
                }
            }
            else if (baseType.IsPrimitive)
            {
                DataValue value = root as DataValue;
                if (value == null)
                {
                    throw new ArgumentException(Properties.Resources.ObjectConverter_GetObjectPrimitive);
                }

                ret = Convert.ChangeType(value.Value, baseType);
            }
            else if (baseType.IsArray)
            {
                DataKey key = root as DataKey;
                if (key == null)
                {
                    throw new ArgumentException(Properties.Resources.ObjectConverter_GetObjectArray);
                }

                ret = GetArray(key, baseType.GetElementType(), derivedTypes, parent);
            }
            else if (baseType.IsEnum)
            {
                DataValue value = root as DataValue;

                if (value != null)
                {
                    Type valueType = value.Value.GetType();

                    if (valueType == typeof(string))
                    {
                        ret = Enum.Parse(baseType, value.Value.ToString());
                    }
                    else if (valueType == typeof(PortableEnum))
                    {
                        ret = Enum.ToObject(baseType, Convert.ChangeType(value.Value.Value, baseType.GetEnumUnderlyingType()));
                    }
                    else if (valueType.IsPrimitive)
                    {
                        ret = Enum.ToObject(baseType, Convert.ChangeType(value.Value, baseType.GetEnumUnderlyingType()));
                    }
                    else
                    {
                        throw new ArgumentException(Properties.Resources.ObjectConverter_GetObjectEnum);
                    }
                }
            }
            else if (baseType == typeof(IPAddress))
            {
                ret = new IPAddress(root.ToArray());
            }
            else
            {
                try
                {
                    Type createType = GetTypeFromNode(baseType, root, derivedTypes);

                    if (createType.IsAbstract)
                    {
                        throw new ArgumentException(String.Format(Properties.Resources.ObjectConverter_GetObjectAbstract, createType));
                    }

                    ret = Activator.CreateInstance(createType);
                }
                catch (MissingMethodException ex)
                {
                    throw new ArgumentException(String.Format(Properties.Resources.ObjectConverter_GetObjectCouldntCreate, baseType), ex);
                }

                INodeInitializer converter = ret as INodeInitializer;

                if (converter != null)
                {
                    converter.FromNode(root);
                }
                else
                {
                    DataKey key = root as DataKey;

                    if (key == null)
                    {
                        throw new ArgumentException(Properties.Resources.ObjectConverter_GetObjectComplex);
                    }

                    PopulateObject(key, ret, derivedTypes);
                }
            }

            SetParent(ret, parent);

            return ret;
        }

        /// <summary>
        /// Convert a tree to an object with a specified type
        /// </summary>
        /// <remarks>
        /// This will not handle many cases, such as use of derived types. Each type referenced must have
        /// a constructor which takes no arguments. Possibly in the future will add annotation mechanisms
        /// to allow you to specify special cases
        /// </remarks>
        /// <param name="root">The data node root</param>        
        /// <exception cref="ArgumentException">Thrown if the data node cannot be converted</exception>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <returns>The object if possible, otherwise null</returns>
        public static T FromNode<T>(DataNode root)
        {
            return (T)GetObject(root, typeof(T), new Dictionary<Guid,Type>(), null);
        }

        /// <summary>
        /// Convert a tree to an object with a specified type
        /// </summary>
        /// <remarks>
        /// This will not handle many cases, such as use of derived types. Each type referenced must have
        /// a constructor which takes no arguments. Possibly in the future will add annotation mechanisms
        /// to allow you to specify special cases
        /// </remarks>
        /// <param name="root">The data node root</param>  
        /// <param name="derivedTypes">Dictionary containing derived type mapping</param>
        /// <exception cref="ArgumentException">Thrown if the data node cannot be converted</exception>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <returns>The object if possible, otherwise null</returns>
        public static T FromNode<T>(DataNode root, Dictionary<Guid, Type> derivedTypes)
        {
            return (T)GetObject(root, typeof(T), derivedTypes, null);
        }

        /// <summary>
        /// Convert a tree to an object with a specified type
        /// </summary>
        /// <remarks>
        /// This will not handle many cases, such as use of derived types. Each type referenced must have
        /// a constructor which takes no arguments. Possibly in the future will add annotation mechanisms
        /// to allow you to specify special cases
        /// </remarks>
        /// <param name="root">The data node root</param>  
        /// <param name="derivedTypes">List of types containing derived type mapping</param>
        /// <exception cref="ArgumentException">Thrown if the data node cannot be converted</exception>
        /// <typeparam name="T">The type of object to return</typeparam>
        /// <returns>The object if possible, otherwise null</returns>
        public static T FromNode<T>(DataNode root, params Type[] derivedTypes)
        {
            Dictionary<Guid, Type> dict = new Dictionary<Guid, Type>();

            foreach (Type t in derivedTypes)
            {
                dict[t.GUID] = t;
            }

            return FromNode<T>(root, dict);
        }
    }
}
