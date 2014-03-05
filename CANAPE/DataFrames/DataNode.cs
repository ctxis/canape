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
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.XPath;
using CANAPE.Utils;
using System.Text.RegularExpressions;
using System.Xml;
using System.Dynamic;
using System.Collections;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// A class to contain a single data blob
    /// </summary>
    [Serializable]
    public abstract class DataNode : DynamicObject
    {        
        private bool _readOnly;
        private string _name;
        private string _description;
        private Guid _displayClass;

        /// <summary>
        /// The containing frame
        /// </summary>
        protected DataFrame _frame;

        /// <summary>
        /// Parent key
        /// </summary>
        protected DataKey _parent;

        /// <summary>
        /// Get the values of the node
        /// </summary>
        public abstract dynamic Value { get; set; }        

        /// <summary>
        /// The name of this entry
        /// </summary>
        /// <remarks>This is a display name, when used in SelectionPath 
        /// expressions you must instead use the value of PathName</remarks>
        public string Name 
        {
            get { return UnfixNodeName(_name); }
            set
            {
                string fixedName = FixNodeName(value);
                if (_name != fixedName)
                {
                    _name = fixedName;
                    OnModified();
                }             
            }
        }

        /// <summary>
        /// The path name of this entry, this could be different from the name if 
        /// it contains invalid XML characters
        /// </summary>
        public string PathName
        {
            get { return _name; }
        }

        /// <summary>
        /// Description for the node
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnModified();
                }
            }
        }

        /// <summary>
        /// Conversion implementation, only implements string and byte arrays by default
        /// </summary>
        /// <param name="binder">The binder</param>
        /// <param name="result">The output result</param>
        /// <returns>True if conversion succeeded</returns>
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof(string))
            {
                result = ToDataString();
                return true;
            }
            else if (binder.Type == typeof(byte[]))
            {
                result = ToArray();
                return true;
            }

            return base.TryConvert(binder, out result);
        }        

        /// <summary>
        /// A Guid which represents the class of the node
        /// </summary>
        public Guid Class { get; set; }
        
        /// <summary>
        /// A Guid which reflects a mechanism to choose what to display, overrides the Class property
        /// </summary>
        public Guid DisplayClass 
        {
            get { return _displayClass; }
            set
            {
                if (_displayClass != value)
                {
                    _displayClass = value;
                    OnModified();
                }
            }
        }

        /// <summary>
        /// Get the display class, chooses DisplayClass first, if zero it chooses Class
        /// </summary>
        /// <returns>The display class</returns>
        public Guid GetDisplayClass()
        {
            if (_displayClass != Guid.Empty)
            {
                return _displayClass;
            }
            else
            {
                return Class;
            }
        }

        /// <summary>
        /// The parent of this entry, null if at the root
        /// </summary>        
        public DataKey Parent 
        {
            get { return _parent; }
        }

        /// <summary>
        /// The data frame containing this node
        /// </summary>
        public DataFrame Frame
        {
            get
            {
                return _frame;
            }
        }

        /// <summary>
        /// Indicates this object is meta data and does not consitute part of the actual data
        /// </summary>        
        public bool MetaData { get; set; }

        /// <summary>
        /// Gets the rough path to this node (not exact)
        /// </summary>
        public string Path
        {
            get
            {
                DataNode p = this;

                if (p.Parent == null)
                {
                    return "/";
                }
                else
                {
                    StringBuilder builder = new StringBuilder();

                    // Parent == null is Root node which doesn't exist as a named entity
                    while (p.Parent != null)
                    {
                        builder.Insert(0, String.Format("/{0}", p._name));
                        p = p.Parent;
                    }

                    return builder.ToString();
                }
            }
        }

        /// <summary>
        /// Indicates this node is read only
        /// </summary>
        /// <remarks>Note that this is only advisory and is used to indicate objects which shouldn't be changed, however it is up to the 
        /// application itself to enforce that restriction. It is also not transitive, if a parent node is read only it doesn't automatically 
        /// mean that a child-node is also read only
        /// </remarks>
        public bool Readonly 
        {            
            get
            {
                return _readOnly;
            }

            set
            {
                _readOnly = value;
            }
        }

        /// <summary>
        /// Method called by derived objects to indicate to the frame we have been modified
        /// </summary>
        protected void OnModified()
        {
            if (_frame != null)
            {
                // Signal an event to the frame to say something has changed
                _frame.OnModified();
            }
        }

        /// <summary>
        /// Write this entry to a stream
        /// </summary>
        /// <returns>The array of data</returns>
        public abstract void ToWriter(DataWriter stm);

        /// <summary>
        /// Convert from a stream
        /// </summary>        
        public abstract void FromReader(DataReader stm);

        /// <summary>
        /// Convert to an array
        /// </summary>
        /// <returns>The node as an array</returns>
        public abstract byte[] ToArray();

        /// <summary>
        /// Convert from an array
        /// </summary>
        /// <param name="data">The data array to convert from</param>
        public abstract void FromArray(byte[] data);

        /// <summary>
        /// Convert to a byte string
        /// </summary>
        /// <returns>The byte string</returns>
        [Obsolete("Use ToDataString instead")]
        public string ToByteString()
        {            
            return ToEncodedString(BinaryEncoding.Instance);
        }

        /// <summary>
        /// Convert to a string based on a specific encoding
        /// </summary>
        /// <param name="encoding">A string encoding</param>
        /// <returns>The encoded string</returns>
        public string ToEncodedString(Encoding encoding)
        {
            return encoding.GetString(ToArray());
        }

        /// <summary>
        /// Convert from a byte string
        /// </summary>
        /// <param name="str">The string to convert</param>
        [Obsolete("Use FromDataString instead")]
        public void FromByteString(string str)
        {
            FromEncodedString(str, BinaryEncoding.Instance);
        }

        /// <summary>
        /// Convert from a specific encoded string string
        /// </summary>
        /// <param name="str">The string to convert</param>
        /// <param name="encoding">The encoding to use</param>
        public void FromEncodedString(string str, Encoding encoding)
        {
            FromArray(encoding.GetBytes(str));
        }

        /// <summary>
        /// Convert this node to string which represents the data, default is to convert using a one to one encoding
        /// </summary>
        /// <returns>The node as a string</returns>
        public virtual string ToDataString()
        {
            return ToEncodedString(BinaryEncoding.Instance);
        }

        /// <summary>
        /// Convert this node from a string which represents the data, default is to convert using a one to one encoding
        /// </summary>
        /// <param name="str">The string</param>
        public virtual void FromDataString(string str)
        {
            FromEncodedString(str, BinaryEncoding.Instance);
        }

        /// <summary>
        /// Sets the referenced data frame, internal access only
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="parent"></param>
        internal virtual void SetLinks(DataFrame frame, DataKey parent)
        {
            _frame = frame;
            _parent = parent;
        }        

        private static string FixNodeName(string name)
        {
            return name == null ? null : XmlConvert.EncodeName(name);            
        }

        private static string UnfixNodeName(string name)
        {
            return name == null ? null : XmlConvert.DecodeName(name);
        }
        
        /// <summary>
        /// Default constructor
        /// </summary>        
        /// <param name="name">The name of the data node</param>
        protected DataNode(string name)
        {
            _name = FixNodeName(name);
        }

        /// <summary>
        /// Replace node with a dictionary key
        /// </summary>
        /// <param name="dict">The dictionary key</param>
        public DataKey ReplaceNode(IDictionary dict)
        {
            DataKey ret = new DataKey(this.Name, dict);
            if (Parent != null)
            {                
                Parent.ReplaceNode(this, ret);
            }
            else
            {
                if ((_frame != null) && (_frame.Root == this))
                {
                    _frame.Root = ret;
                }
            }

            return ret;
        }

        /// <summary>
        /// Replace the current node
        /// </summary>
        /// <param name="node">The new node to replace with</param>
        public void ReplaceNode(DataNode node)
        {            
            if (Parent != null)
            {
                Parent.ReplaceNode(this, node);
            }
            else
            {
                if ((_frame != null) && (_frame.Root == this))
                {
                    DataKey key = node as DataKey;

                    if (key != null)
                    {
                        _frame.Root = key;
                    }
                    else
                    {
                        _frame.ConvertToBasic(node.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Replace the current node
        /// </summary>
        /// <param name="node">The new node as a binary string</param>
        /// <returns>The new data value</returns>
        public DataValue ReplaceNode(string node)
        {
            DataValue ret = null;            
            if (Parent != null)
            {
                ret = Parent.ReplaceNode(this, node);
            }
            else
            {
                if ((_frame != null) && (_frame.Root == this))
                {
                    ret = _frame.ConvertToBasic(node);                    
                }
            }

            return ret;
        }

        /// <summary>
        /// Replace the current node
        /// </summary>
        /// <param name="node">The new node as a string</param>
        /// <param name="encoding">The string encoding</param>
        /// <returns>The new data value</returns>
        public DataValue ReplaceNode(string node, Encoding encoding)
        {
            DataValue ret = null;
            if (Parent != null)
            {
                ret = Parent.ReplaceNode(this, node, encoding);
            }
            else
            {
                if ((_frame != null) && (_frame.Root == this))
                {
                    ret = _frame.FromDataString(node, encoding);
                }
            }

            return ret;
        }

        /// <summary>
        /// Replace the current node
        /// </summary>
        /// <param name="node">The new node as a byte array</param>
        /// <returns>The new data value</returns>
        public DataValue ReplaceNode(byte[] node)
        {
            DataValue ret = null;            
            if (Parent != null)
            {                
                ret = Parent.ReplaceNode(this, node);                
            }
            else
            {
                if ((_frame != null) && (_frame.Root == this))
                {
                    ret = _frame.ConvertToBasic(node);
                }
            }

            return ret;
        }

        /// <summary>
        /// Remove this node from the tree
        /// </summary>
        public void RemoveNode()
        {
            if (Parent != null)
            {
                Parent.RemoveNode(this);
            }
            else
            {
                // Convert to an empty frame
                if ((_frame != null) && (_frame.Root == this))
                {
                    _frame.ConvertToBasic(new byte[0]);
                    SetLinks(null, null);
                }
            }
        }

        /// <summary>
        /// Select a list of nodes based on an XPath style string
        /// </summary>
        /// <example>
        /// <code lang="cs">
        /// Select all nodes in the tree with the name value
        /// DataNode[] nodes = node.SelectNodes("//value");
        /// </code>
        /// </example>
        /// <param name="path">The XPath selector</param>
        /// <returns>A list of nodes select</returns>
        public DataNode[] SelectNodes(string path)
        {
            return SelectNodes<DataNode>(path);
        }

        /// <summary>
        /// Select a list of nodes based on an XPath style string which match a particular type
        /// </summary>
        /// <example>
        /// <code lang="cs">
        /// Select all nodes in the tree with the name value
        /// DataNode[] nodes = node.SelectNodes("//value");
        /// </code>
        /// </example>
        /// <param name="path">The XPath selector</param>
        /// <returns>A list of nodes select</returns>
        public T[] SelectNodes<T>(string path) where T : DataNode
        {
            List<T> nodes = new List<T>();

            try
            {
                XPathDataNavigator xpath = new XPathDataNavigator(this, false);
                XPathNodeIterator it = xpath.Select(path.ToLower());

                while (it.MoveNext())
                {
                    T value = it.Current.UnderlyingObject as T;

                    if (value != null)
                    {
                        nodes.Add(value);
                    }
                }
            }
            catch (XPathException ex)
            {
                Logger.GetSystemLogger().LogException(ex);
            }

            return nodes.ToArray();
        }

        /// <summary>
        /// Select the first node which matches an XPath style path
        /// </summary>
        /// <param name="path">The XPath selector</param>
        /// <returns>The first node, or null if none found</returns>
        public DataNode SelectSingleNode(string path)
        {
            return SelectSingleNode<DataNode>(path);
        }

        /// <summary>
        /// Select the first node which matches an XPath style path and is of the required type
        /// </summary>
        /// <param name="path">The XPath selector</param>
        /// <typeparam name="T">The type of node to return</typeparam>
        /// <returns>The first node, or null if none found</returns>
        public T SelectSingleNode<T>(string path) where T : DataNode
        {
            try
            {
                XPathDataNavigator xpath = new XPathDataNavigator(this, false);
                XPathNodeIterator it = xpath.Select(path.ToLower());

                if (it.MoveNext())
                {
                    T value = it.Current.UnderlyingObject as T;

                    if (value != null)
                    {
                        return value;
                    }
                }
            }
            catch (XPathException ex)
            {
                Logger.GetSystemLogger().LogException(ex);
            }

            return null;
        }

        /// <summary>
        /// Select some values with a specified actual type, will not try to auto-convert to the type
        /// </summary>
        /// <param name="path">The path to select on</param>        
        /// <returns>An array of values</returns>
        public T[] SelectValues<T>(string path)
        {
            return SelectValues<T>(path, false);
        }

        /// <summary>
        /// Select some values with a specified actual type
        /// </summary>
        /// <param name="path">The path to select on</param>
        /// <param name="tryConvert">Indicate if want to attempt to convert to type</param>
        /// <returns>An array of values</returns>
        public T[] SelectValues<T>(string path, bool tryConvert)
        {
            List<T> ret = new List<T>();
            DataValue[] values = SelectNodes<DataValue>(path);

            foreach (DataValue v in values)
            {
                if (v.Value is T)
                {
                    ret.Add((T)v.Value);
                }
                else if (tryConvert)
                {
                    try
                    {
                        ret.Add((T)Convert.ChangeType(v.Value, typeof(T)));
                    }
                    catch (InvalidCastException)
                    {
                    }
                    catch (OverflowException)
                    {
                    }
                    catch (FormatException)
                    {
                    }
                }                                
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Select a single value based on a specific type
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="path">The path</param>
        /// <param name="tryConvert">Indicate if want to attempt to convert to type</param>
        /// <param name="value">An output parameter to hold the type</param>
        /// <returns>True if found a value, otherwise false</returns>
        public bool SelectSingleValue<T>(string path, bool tryConvert, out T value)
        {
            T[] values = SelectValues<T>(path, tryConvert);
            value = default(T);

            if (values.Length > 0)
            {
                value = values[0];
                return true;
            }

            return false;
        }

        /// <summary>
        /// Select a single value based on a specific type, will not try to auto-convert
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="path">The path</param>        
        /// <param name="value">An output parameter to hold the type</param>
        /// <returns>True if found a value, otherwise false</returns>
        public bool SelectSingleValue<T>(string path, out T value)
        {
            return SelectSingleValue<T>(path, false, out value);            
        }

        /// <summary>
        /// Return either a single node or an empty value
        /// </summary>
        /// <param name="path">The XPath to select on</param>
        /// <returns>The first selected node, or an empty ByteArrayDataValue if not found</returns>
        public DataNode SelectSingleNodeOrEmpty(string path)
        {
            DataNode ret = SelectSingleNode(path);

            if (ret == null)
            {
                ret = new ByteArrayDataValue("Empty", new byte[0]);
            }

            return ret;
        }

        /// <summary>
        /// Clone the node and any contained subnodes
        /// </summary>
        /// <returns>THe cloned node detached from the frame</returns>
        public DataNode CloneNode()
        {
            BinaryFormatter fmt = new BinaryFormatter();
            MemoryStream stm = new MemoryStream();

            fmt.Serialize(stm, this);
            stm.Position = 0;

            // This will actually serialize the entire frame, no matter. 
            DataNode ret = (DataNode)fmt.Deserialize(stm);

            // Disconnect from the tree
            ret.SetLinks(null, null);

            return ret;
        }

        /// <summary>
        /// Detach this node from the tree
        /// </summary>
        public DataNode Detach()
        {
            RemoveNode();

            return this;
        }
    }
}
