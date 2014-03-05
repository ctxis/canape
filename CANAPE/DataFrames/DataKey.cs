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
using System.Text;
using CANAPE.Utils;
using System.Globalization;
using System.Collections;
using System.Linq;
using System.Dynamic;
using System.Numerics;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// A data node which is a container of other nodes
    /// </summary>
    [Serializable]
    public class DataKey : DataNode
    {
        private DataNode _internalNode;

        /// <summary>
        /// List of sub nodes
        /// </summary>
        private List<DataNode> _subNodes;        

        /// <summary>
        /// The list of contained sub nodes
        /// </summary>
        public IList<DataNode> SubNodes
        {
            get
            {
                return _subNodes;
            }
        }

        /// <summary>
        /// Get a list of the keys values
        /// </summary>
        public IEnumerable<DataValue> Values
        {
            get
            {
                return _subNodes.OfType<DataValue>().ToArray();
            }
        }

        /// <summary>
        /// A custom format string for display, might not always be honoured        
        /// </summary>
        /// <remarks>Uses a special syntax to select the nodes to display. Node names are prefixed by $ symbols.</remarks>
        /// <example>
        /// <code lang="cs">
        /// This string will print the subvalues A and B with some extra text
        /// "A'value value is $A, B'value value is $B"
        /// </code>
        /// </example>
        public string FormatString { get; set; }

        /// <summary>
        /// Returns name of the key
        /// </summary>
        /// <returns>The key name</returns>
        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(FormatString))
            {
                return Name;
            }
            else
            {
                // Do format
                return GeneralUtils.FormatKeyString(FormatString, this);
            }
        }

        /// <summary>
        /// Overridden form, sets all subnodes to the current frame as well
        /// </summary>
        /// <param name="frame">The root data frame</param>
        /// <param name="parent">The parent datakey</param>
        internal override void SetLinks(DataFrame frame, DataKey parent)
        {
            base.SetLinks(frame, parent);

            foreach(DataNode node in _subNodes)
            {
                node.SetLinks(frame, this);
            }
        }

        /// <summary>
        /// This method can be overriden by a deriving class to customise how data is stored
        /// in the key
        /// </summary>
        /// <param name="node"></param>
        public virtual void AddSubNode(DataNode node)
        {
            if (node != null)
            {
                if (node.Frame != null)
                {
                    throw new InvalidOperationException(CANAPE.Properties.Resources.DataKey_AddSubNodeException);
                }

                node.SetLinks(_frame, this);
                _subNodes.Add(node);
                OnModified();
            }
        }

        private static IDictionary ListToDictionary(string prefix, IList list)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            for (int i = 0; i < list.Count; ++i)
            {
                dict[prefix + i.ToString()] = list[i];
            }

            return dict;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">Name of key</param>
        public DataKey(string name)
            : base(name)            
        {
            _subNodes = new List<DataNode>();
        }

        /// <summary>
        /// Constructor from a dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dict"></param>
        public DataKey(string name, IDictionary dict) : this(name)
        {            
            if(dict == null)
            {
                throw new ArgumentNullException("dict");
            }
            
            PopulateFromDictionary(dict);            
        }

        /// <summary>
        /// Constructor from a list
        /// </summary>
        /// <param name="name">The name of the key</param>
        /// <param name="prefix">The prefix name for each item</param>
        /// <param name="list">The associated list</param>
        public DataKey(string name, string prefix, IList list)
            : this(name, ListToDictionary(prefix, list))
        {
        }

        /// <summary>
        /// Constructor from a list with default Item prefix
        /// </summary>
        /// <param name="name">The name of the key</param>        
        /// <param name="list">The associated list</param>
        public DataKey(string name, IList list) 
            : this(name, "Item", list)
        {
        }

        /// <summary>
        /// Write this entry to a stream
        /// </summary>        
        public override void ToWriter(DataWriter stm)
 	    {
            // We ignore internal value if we have subnodes
            if ((_internalNode != null) && (_subNodes.Count == 0))
            {
                _internalNode.ToWriter(stm);
            }
            else
            {
                foreach (DataNode entry in SubNodes)
                {
                    if (!entry.MetaData)
                    {
                        entry.ToWriter(stm);
                    }
                }
            }
        }

        /// <summary>
        /// Not currently implemented
        /// </summary>
        public override void FromReader(DataReader stm)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clear all sub nodes
        /// </summary>
        public void Clear()
        {
            // Break links with these nodes
            foreach (DataNode node in _subNodes)
            {
                node.SetLinks(null, null);
            }

            _subNodes.Clear();
            OnModified();
        }

        private DataKey AddKey(DataKey key)
        {
            AddSubNode(key);

            return key;
        }

        /// <summary>
        /// Add a new key with a specific name to the subkeys
        /// </summary>
        /// <param name="name">The key name</param>
        /// <returns>The created subkey</returns>
        public DataKey AddKey(string name)
        {
            return AddKey(new DataKey(name));
        }

        /// <summary>
        /// Add a new key with a specific name to the subkeys, initialized with a dictionary
        /// </summary>
        /// <param name="name">The key name</param>
        /// <param name="dict"></param>
        /// <returns>The created subkey</returns>
        public DataKey AddKey(string name, IDictionary dict)
        {
            return AddKey(new DataKey(name, dict));
        }

        /// <summary>
        /// Add a new key with a list of values
        /// </summary>
        /// <param name="name">The name of the key</param>
        /// <param name="list">The list of values</param>
        /// <returns>The created subkey</returns>
        public DataKey AddKey(string name, IList list)
        {
            return AddKey(new DataKey(name, list));
        }

        /// <summary>
        /// Add a new key with a list of values
        /// </summary>
        /// <param name="name">The name of the key</param>
        /// <param name="prefix">The item prefix for list items</param>
        /// <param name="list">The list of values</param>
        /// <returns>The created subkey</returns>
        public DataKey AddKey(string name, string prefix, IList list)
        {
            return AddKey(new DataKey(name, prefix, list));
        }

        /// <summary>
        /// Add a boolean value
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, bool value)
        {
            DataValue dataValue = new GenericDataValue<bool>(name, value);

            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a new integer value with endian
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="littleEndian">True if the integer is little-endian, otherwise big-endian</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, int value, bool littleEndian)
        {
            DataValue dataValue = new GenericDataValue<int>(name, value, littleEndian);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a new integer value
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, int value)
        {
            return AddValue(name, value, true);
        }

        /// <summary>
        /// Add a new byte value
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, byte value)
        {
            DataValue dataValue = new GenericDataValue<byte>(name, value);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a new signed byte value
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, sbyte value)
        {
            DataValue dataValue = new GenericDataValue<sbyte>(name, value);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a new unsigned short value with endian
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="littleEndian">True if the integer is little-endian, otherwise big-endian</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, ushort value, bool littleEndian)
        {
            DataValue dataValue = new GenericDataValue<ushort>(name, value, littleEndian);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a new short value with endian
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="littleEndian">True if the integer is little-endian, otherwise big-endian</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, short value, bool littleEndian)
        {
            DataValue dataValue = new GenericDataValue<short>(name, value, littleEndian);

            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a new unsigned integer value with endian
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="littleEndian">True if the integer is little-endian, otherwise big-endian</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, uint value, bool littleEndian)
        {
            DataValue dataValue = new GenericDataValue<uint>(name, value, littleEndian);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a new unsigned short
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, ushort value)
        {
            return AddValue(name, value, true);
        }

        /// <summary>
        /// Add a new short
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, short value)
        {
            return AddValue(name, value, true);
        }

        /// <summary>
        /// Add a new unsigned integer value
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, uint value)
        {
            return AddValue(name, value, true);
        }

        /// <summary>
        /// Add a string with a specific encoding
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="encoding">String encoding type to use</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, string value, Encoding encoding)
        {
            DataValue dataValue = new StringDataValue(name, value, encoding);

            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a string with a specific named encoding 
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="encoding">The encoding name (e.g. UTF-8)</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, string value, string encoding)
        {
            Encoding enc = Encoding.GetEncoding(encoding);
            if (enc == null)
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, CANAPE.Properties.Resources.DataKey_AddValueInvalidEncoding, encoding));
            }

            DataValue dataValue = new StringDataValue(name, value, enc);

            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a string with binary encoding
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, string value)
        {
            return AddValue(name, value, new BinaryEncoding());
        }

        /// <summary>
        /// Add an array of bytes
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="data">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, byte[] data)
        {
            DataValue dataValue = new ByteArrayDataValue(name, data);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add an existing DataValue object
        /// </summary>
        /// <param name="dataValue">The datavalue object to add</param>
        public void AddValue(DataValue dataValue)
        {            
            AddSubNode(dataValue);
        }

        /// <summary>
        /// Add an enumeration value to the tree
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added value object</returns>
        public DataValue AddValue(string name, Enum value)
        {
            DataValue dataValue = new EnumDataValue(name, value);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a long value to the key
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="littleEndian">True to add as little endian</param>
        /// <returns>The added data value</returns>
        public DataValue AddValue(string name, long value, bool littleEndian)
        {
            DataValue dataValue = new GenericDataValue<long>(name, value, littleEndian);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a little endian long value to the key
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added data value</returns>
        public DataValue AddValue(string name, long value)
        {
            return AddValue(name, value, true);
        }

        /// <summary>
        /// Add a ulong value to the key
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="littleEndian">True to add as little endian</param>
        /// <returns>The added data value</returns>
        public DataValue AddValue(string name, ulong value, bool littleEndian)
        {
            DataValue dataValue = new GenericDataValue<ulong>(name, value, littleEndian);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a little endian ulong value to the key
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added data value</returns>
        public DataValue AddValue(string name, ulong value)
        {
            return AddValue(name, value, true);               
        }

        /// <summary>
        /// Add a big integer value to the key
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <param name="littleEndian">True to add as little endian</param>
        /// <returns>The added data value</returns>
        public DataValue AddValue(string name, BigInteger value, bool littleEndian)
        {
            DataValue dataValue = new BigIntegerDataValue(name, value, littleEndian);
            AddSubNode(dataValue);

            return dataValue;
        }

        /// <summary>
        /// Add a big integer value to the key, default little endian
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value itself</param>
        /// <returns>The added data value</returns>
        public DataValue AddValue(string name, BigInteger value)
        {
            return AddValue(name, value, true);
        }

        /// <summary>
        /// Get a data value by name
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <returns>The data value</returns>
        public DataValue GetValue(string name)
        {
            DataValue ret = this[name] as DataValue;

            if (ret == null)
            {
                throw new MissingDataNodeException(name);
            }

            return ret;
        }

        /// <summary>
        /// Convert this node and all subnodes to a byte array
        /// </summary>
        /// <returns>The array of bytes</returns>
        public override byte[] ToArray()
        {
            MemoryStream stm = new MemoryStream();
            DataWriter writer = new DataWriter(stm);

            ToWriter(writer);

            return stm.ToArray();
        }

        /// <summary>
        /// Rebuild this node and all subnodes from a byte array
        /// </summary>
        /// <param name="data">The byte array to convert from</param>
        public override void FromArray(byte[] data)
        {
            MemoryStream stm = new MemoryStream(data);
            DataReader reader = new DataReader(stm);

            FromReader(reader);
        }

        private int IndexOfSubNode(DataNode node)
        {
            for (int i = 0; i < _subNodes.Count; ++i)
            {
                if (Object.ReferenceEquals(node, _subNodes[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Replace the a node in the subnodes with a new one
        /// </summary>
        /// <param name="oldNode">The old node to replace</param>
        /// <param name="newNode">The new node</param>
        internal void ReplaceNode(DataNode oldNode, DataNode newNode)
        {
            // Don't replace the node if it happens to be exactly the same
            if (!Object.ReferenceEquals(oldNode, newNode))
            {
                int idx = IndexOfSubNode(oldNode);

                if (idx >= 0)
                {
                    newNode.SetLinks(_frame, this);
                    oldNode.SetLinks(null, null);
                    _subNodes[idx] = newNode;
                    OnModified();
                }
                else
                {
                    throw new ArgumentException(CANAPE.Properties.Resources.DataKey_InvalidChildNode);
                }
            }
        }

        /// <summary>
        /// Replace the a node in the subnodes with a new binary version
        /// </summary>
        /// <param name="oldNode">The old node to replace</param>
        /// <param name="newNode">The new node as a binary string</param>
        /// <returns>The new data value</returns>
        internal DataValue ReplaceNode(DataNode oldNode, string newNode)
        {
            return ReplaceNode(oldNode, GeneralUtils.MakeByteArray(newNode));            
        }

        /// <summary>
        /// Replace the a node in the subnodes with a new string version
        /// </summary>
        /// <param name="oldNode">The old node to replace</param>
        /// <param name="newNode">The new node as a string</param>
        /// <param name="encoding">The string encoding</param>        
        /// <returns>The new data value</returns>
        internal DataValue ReplaceNode(DataNode oldNode, string newNode, Encoding encoding)
        {
            StringDataValue ret = oldNode as StringDataValue;

            if (ret != null)
            {                
                // We can replace the contents without having to do anything too clever               
                ret.Value = newNode;
                ret.StringEncoding = encoding;

                OnModified();
            }
            else
            {
                int idx = IndexOfSubNode(oldNode);

                if (idx >= 0)
                {
                    ret = new StringDataValue(oldNode.Name, newNode, encoding);
                    ret.SetLinks(_frame, this);
                    oldNode.SetLinks(null, null);
                    _subNodes[idx] = ret;
                    OnModified();
                }
                else
                {
                    throw new ArgumentException(CANAPE.Properties.Resources.DataKey_InvalidChildNode);
                }
            }

            return ret;
        }

        /// <summary>
        /// Replace a node in the subnodes with a new binary version
        /// </summary>
        /// <param name="oldNode">The old node to replace</param>
        /// <param name="newNode">The new node as a binary string</param>
        /// <returns>The new data value</returns>
        internal DataValue ReplaceNode(DataNode oldNode, byte[] newNode)
        {
            DataValue ret = null;

            if (oldNode is ByteArrayDataValue)
            {
                // We can replace the contents without having to do anything too clever 
                ret = oldNode as DataValue;
                ret.Value = newNode;

                OnModified();
            }
            else
            {
                int idx = IndexOfSubNode(oldNode);

                if (idx >= 0)
                {
                    ret = new ByteArrayDataValue(oldNode.Name, newNode);
                    ret.SetLinks(_frame, this);
                    oldNode.SetLinks(null, null);
                    _subNodes[idx] = ret;
                    OnModified();
                }
            }

            return ret;
        }

        /// <summary>
        /// Determine if this key contains a specific node
        /// </summary>
        /// <param name="name">The name to find</param>
        /// <returns>True if the named node exists</returns>
        public bool ContainsNode(string name)
        {
            return GetNodeNoThrow(name) != null;
        }

        private DataNode GetNodeNoThrow(string name)
        {
            return _subNodes.Find(n => n.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Select a DataNode by name
        /// </summary>        
        /// <param name="name">The name of the node to select</param>
        /// <exception cref="MissingDataNodeException">Thrown if the path is not found</exception>
        /// <returns>The selected node</returns>
        public DataNode this[string name]
        {
            get
            {
                DataNode ret = GetNodeNoThrow(name);
                if (ret == null)
                {
                    throw new MissingDataNodeException(name);
                }

                return ret;
            }
        }

        /// <summary>
        /// Remove the specified node
        /// </summary>
        /// <param name="node">The node to remove</param>
        internal void RemoveNode(DataNode node)
        {
            node.SetLinks(null, null);
            int index = IndexOfSubNode(node);

            if (index >= 0)
            {
                _subNodes.RemoveAt(index);
                OnModified();
            }
        }

        /// <summary>
        /// Dynamic get member
        /// </summary>
        /// <param name="binder">The binder</param>
        /// <param name="result">The output result</param>
        /// <returns>True if member was found</returns>
        public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
        {
            DataNode n = GetNodeNoThrow(binder.Name);

            if (n != null)
            {
                result = n;
                return true;
            }
            else
            {
                return base.TryGetMember(binder, out result);
            }
        }

        private void PopulateFromDictionary(IDictionary dict)
        {
            foreach (DictionaryEntry ent in dict)
            {
                SetMember(ent.Key.ToString(), ent.Value);
            }
        }

        private bool SetMember(string name, dynamic value)
        {
            DataValue v = GetNodeNoThrow(name) as DataValue;
            DataNode n = value as DataNode;
            IDictionary dict = value as IDictionary;
            IList list = value as IList;

            if ((dict == null) && (list != null) && !(value is byte[]))
            {
                dict = ListToDictionary("Item", list);
            }

            if (dict != null)
            {
                DataKey key = new DataKey(name);

                key.PopulateFromDictionary(dict);

                n = key;
            }            

            if (v != null)
            {
                if (n != null)
                {
                    v.ReplaceNode(n);
                    n.Name = name;
                }
                else
                {
                    v.Value = value;
                }
            }
            else
            {
                if (n != null)
                {
                    n.Name = name;
                    AddSubNode(n);
                }
                else
                {
                    dynamic key = this;                    

                    key.AddValue(name, value);
                }
            }

            return true;
        }

        /// <summary>
        /// Remove a node by name
        /// </summary>
        /// <param name="node">The name of the node to remove</param>
        public void RemoveNode(string node)
        {
            DataNode ret = GetNodeNoThrow(node);
            if (ret != null)
            {
                ret.RemoveNode();
            }
        }

        /// <summary>
        /// Dynamic set member
        /// </summary>
        /// <param name="binder">The binder</param>
        /// <param name="value">The value</param>
        /// <returns>True if set member</returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return SetMember(binder.Name, value);
        }

        /// <summary>
        /// Implementation of delete member
        /// </summary>
        /// <param name="binder">The binder</param>
        /// <returns>True if member could be deleted</returns>
        public override bool TryDeleteMember(System.Dynamic.DeleteMemberBinder binder)
        {
            DataNode n = GetNodeNoThrow(binder.Name);

            if (n != null)
            {
                n.RemoveNode();
                return true;
            }
            else
            {
                return base.TryDeleteMember(binder);
            }
        }        

        /// <summary>
        /// Convert to a dictionary, this can be a lossy process if multiple nodes with the same value exist
        /// </summary>        
        /// <returns>The key as a dictionary</returns>
        public IDictionary ToDictionary()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (DataNode node in _subNodes)
            {
                DataKey key = node as DataKey;

                if (key != null)
                {
                    dict[node.Name] = key.ToDictionary();
                }
                else
                {
                    dict[node.Name] = ((DataValue)node).Value;
                }
            }

            return dict;
        }

        /// <summary>
        /// Conversion override
        /// </summary>
        /// <param name="binder">The binder</param>
        /// <param name="result">The result</param>
        /// <returns>True if able to convert</returns>
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof(IDictionary))
            {
                result = ToDictionary();
                return true;
            }

            return base.TryConvert(binder, out result);
        }

        /// <summary>
        /// Get or set the value of the node
        /// </summary>
        public override dynamic Value
        {
            get
            {
                if (_internalNode != null)
                {
                    return _internalNode.Value;
                }
                else
                {
                    return ToArray();
                }
            }
            set
            {
                if (value is DataNode)
                {
                    DataNode v = value as DataNode;

                    v.Detach();

                    _internalNode = v;
                    _internalNode.SetLinks(_frame, this);
                }
                else if ((_internalNode != null) && (_internalNode.Value.GetType() == value.GetType()))
                {
                    _internalNode.Value = value;
                }
                else if (value is byte[])
                {
                    ReplaceNode((byte[])value);
                }
                else if (value is string)
                {
                    ReplaceNode((string)value);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}
