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
using System.Runtime.Serialization.Formatters.Binary;
using CANAPE.Properties;
using CANAPE.Utils;
using System.Text;
using System.Collections;
using System.Dynamic;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// A DataFrame object
    /// </summary>
    [Serializable]
    public sealed class DataFrame 
    {
        private DataKey _root;
        private DataNode _current;
        private string _hash;
        private long _length;
        private string _displayString;        

        /// <summary>
        /// An event signalled when a frame is modified
        /// </summary>
        [field: NonSerialized]
        public event EventHandler FrameModified;

        /// <summary>
        /// Internal method to call frame modified
        /// </summary>
        internal void OnModified()
        {
            if (FrameModified != null)
            {
                FrameModified.Invoke(this, new EventArgs());
            }
            _hash = null;
            _length = -1;
            _displayString = null;
            
            if ((_root != null) && (Current.Frame != this))
            {
                // Current is no longer in this frame, set it to root
                Current = Root;
            }
        }

        /// <summary>
        /// The root data key of the frame
        /// </summary>
        public DataKey Root 
        {
            get
            {
                return _root;
            }

            set
            {
                if (value == null)
                {
                    ConvertToBasic(new byte[0]);
                }
                else
                {
                    if (_root != null)
                    {
                        _root.SetLinks(null, null);
                    }

                    _root = value;
                    _root.SetLinks(this, null);
                    
                    OnModified();
                }

                _current = null;
            }
        }

        /// <summary>
        /// The root data key as a dynamic object
        /// </summary>
        public dynamic DynamicRoot
        {
            get
            {
                return _root;
            }
        }

        /// <summary>
        /// The current node pointer (might not be the root)
        /// </summary>
        public DataNode Current 
        {
            get
            {
                if (_current == null)
                {
                    _current = Root;                    
                }

                return _current;
            }

            set
            {
                if (value == null)
                {
                    _current = _root;
                }
                else
                {
                    if (value.Frame != this)
                    {
                        throw new InvalidOperationException(Resources.DataFrame_Exception1);
                    }

                    _current = value;
                }
            }
        }

        /// <summary>
        /// Clone the frame
        /// </summary>
        /// <returns>The cloned frame</returns>
        public DataFrame CloneFrame()
        {
            return (DataFrame)GeneralUtils.CloneObject(this);
        }

        /// <summary>
        /// Clone the frame to a basic frame
        /// </summary>
        /// <returns>The cloned frame</returns>
        public DataFrame CloneToBasic()
        {
            return new DataFrame(ToArray());
        }

        /// <summary>
        /// Copy one data frame into another
        /// </summary>
        /// <param name="frame">The frame to copy to</param>
        public void CopyTo(DataFrame frame)
        {
            frame.Root = (DataKey)_root.CloneNode();
        }

        /// <summary>
        /// Convert the frame to a byte array
        /// </summary>
        // <returns>The data as an array</returns>
        public byte[] ToArray()
        {
            MemoryStream stm = new MemoryStream();

            ToStream(stm);
            
            return stm.ToArray();
        }

        /// <summary>
        /// Convert the frame to a stream
        /// </summary>
        /// <param name="stm">The stream to convert to</param>
        public void ToStream(Stream stm)
        {
            if ((_root != null) && (!_root.MetaData))
            {
                Root.ToWriter(new DataWriter(stm));
            } 
        }

        /// <summary>
        /// Convert to a byte string
        /// </summary>
        /// <returns>A byte string representing the frame</returns>
        [Obsolete("Use ToDataString instead")]
        public string ToByteString()
        {
            if ((_root != null) && (!_root.MetaData))
            {
                return BinaryEncoding.Instance.GetString(_root.ToArray());
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Returns an indication if this a "basic" frame, i.e. one which only contains bytes
        /// </summary>
        public bool IsBasic
        {
            get 
            {
                if (Root != null)
                {
                    return Root.Class == new Guid(DataNodeClasses.BINARY_NODE_CLASS);
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns an indication if this is a data string frame
        /// </summary>
        public bool IsDataString
        {
            get
            {
                if (Root != null)
                {
                    return Root.Class == new Guid(DataNodeClasses.STRING_NODE_CLASS);
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Default constructor, creates a frame with a single root key
        /// </summary>
        public DataFrame() 
            : this(new DataKey("Root"))
        {            
        }

        /// <summary>
        /// Constructor, creates a basic frame with a data set
        /// </summary>
        /// <param name="data">The data set</param>
        public DataFrame(byte[] data)
        {
            ConvertToBasic(data);
        }

        /// <summary>
        /// Constructor, creates a basic frame with a binary string
        /// </summary>
        /// <param name="data">The basic data as a binary string</param>
        public DataFrame(string data)
        {
            ConvertToBasic(data);
        }

        /// <summary>
        /// Constructor, creates a basic frame with a known root
        /// </summary>
        /// <param name="root">The root key</param>
        public DataFrame(DataKey root)
        {
            Root = root;
        }

        /// <summary>
        /// Constructor, creates a basic frame from a dictionary
        /// </summary>
        /// <param name="dict">The dictionary</param>
        public DataFrame(IDictionary dict) 
            : this(new DataKey("Root",dict))
        {
        }
        
        /// <summary>
        /// Converts this frame to a basic one
        /// </summary>
        public DataValue ConvertToBasic()
        {
            return ConvertToBasic(ToArray());            
        }

        /// <summary>
        /// Convert the frame to a string value
        /// </summary>
        /// <param name="value">The string value</param>        
        /// <returns>The data value</returns>
        public DataValue ConvertToString(string value)
        {
            return FromDataString(value, BinaryEncoding.Instance);
        }

        /// <summary>
        /// Convert the frame to a data string
        /// </summary>
        /// <returns>The data string</returns>
        public string ToDataString()
        {
            if (_root != null)
            {
                return _root.ToDataString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Convert the frame to a string value
        /// </summary>
        /// <param name="value">The string value</param>
        /// <param name="encoding">The encoding for the string</param>
        /// <returns>The data value</returns>
        public DataValue FromDataString(string value, Encoding encoding)
        {
            DataValue ret = null;
            bool doConvert = true;

            // If already basic, don't convert the entire frame
            if (IsDataString)
            {
                StringDataValue dv = SelectSingleNode("/Data") as StringDataValue;

                if (dv != null)
                {
                    dv.Value = value;
                    dv.StringEncoding = encoding;
                    ret = dv;
                    doConvert = false;
                }
            }

            // Not basic or we failed
            if (doConvert)
            {
                DataKey root = new DataKey("Root");
                root.Class = new Guid(DataNodeClasses.BINARY_NODE_CLASS);
                root.FormatString = "$Data";

                root.SetLinks(this, null);
                ret = root.AddValue("Data", value, encoding);

                Root = root;
            }

            OnModified();

            return ret;
        }

        /// <summary>
        /// Converts this frame to a basic one with a data set
        /// </summary>
        /// <param name="data">The basic data to convert to</param>
        /// <returns>The data value</returns>
        public DataValue ConvertToBasic(byte[] data)
        {
            DataValue ret = null;
            bool doConvert = true;

            // If already basic, don't convert the entire frame
            if (IsBasic)
            {
                ByteArrayDataValue dv = SelectSingleNode("/Data") as ByteArrayDataValue;

                if (dv != null)
                {
                    dv.Value = data;
                    ret = dv;
                    doConvert = false;
                }
            }

            // Not basic or we failed
            if(doConvert)
            {
                DataKey root = new DataKey("Root");
                root.Class = new Guid(DataNodeClasses.BINARY_NODE_CLASS);
                root.FormatString = "$Data";

                root.SetLinks(this, null);
                ret = root.AddValue("Data", data);

                Root = root;
            }

            OnModified();

            return ret;
        }

        /// <summary>
        /// Converts this frame to a basic one with a binary string
        /// </summary>
        /// <param name="data">A binary string to use</param>
        /// <returns>The added data value object</returns>
        public DataValue ConvertToBasic(string data)
        {
            return ConvertToBasic(BinaryEncoding.Instance.GetBytes(data));
        }

        /// <summary>
        /// Select the first node which matches an XPath style path
        /// </summary>
        /// <param name="path">The XPath selector</param>
        /// <returns>The first node, or null if none found</returns>
        public DataNode SelectSingleNode(string path)
        {
            return Current.SelectSingleNode(path);
        }

        /// <summary>
        /// Select the first node which matches an XPath style path
        /// </summary>
        /// <param name="path">The XPath selector</param>
        /// <typeparam name="T">The type of node to return</typeparam>
        /// <returns>The first node, or null if none found</returns>
        public T SelectSingleNode<T>(string path) where T : DataNode
        {
            return Current.SelectSingleNode<T>(path);
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
            return Current.SelectNodes(path);
        }

        private void UpdateData()
        {
            try
            {
                byte[] data = ToArray();

                _length = data.Length;
                _hash = GeneralUtils.GenerateMd5String(data);
            }
            catch
            {
                _length = 0;
                _hash = "";   
            }
        }

        /// <summary>
        /// MD5 hash of the frame
        /// </summary>
        public string Hash
        {
            get
            {
                if (_hash == null)
                {
                    UpdateData();
                }

                return _hash;
            }
        }

        /// <summary>
        /// Cached length of the frame
        /// </summary>
        public long Length
        {
            get
            {
                if (_length < 0)
                {
                    UpdateData();
                }

                return _length;
            }
        }


        /// <summary>
        /// Converts the node to a display string of sorts
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_displayString == null)            
            {
                if (Root != null)
                {                                        
                    _displayString = Root.ToString();                    
                }
                else
                {
                    _displayString = "";
                }
            }

            return _displayString;
            
        }
    }
}
