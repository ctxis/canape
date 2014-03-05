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
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CANAPE.Utils;

namespace CANAPE.Documents
{
    /// <summary>
    /// Base class for all document object
    /// </summary>
    [Serializable]
    public abstract class BaseDocumentObject : IDocumentObject, IDeserializationCallback, IObjectReference
    {
        #region IDocumentObject Members

        private Dictionary<string, byte[]> _properties;

        [NonSerialized]
        bool _dirty;

        /// <summary>
        /// Overridable method called when name changes
        /// </summary>
        protected virtual void OnNameChange()
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseDocumentObject()
        {
            Uuid = Guid.NewGuid();
            Dirty = true;
            _properties = new Dictionary<string, byte[]>();
        }

        /// <summary>
        /// Internal name variable
        /// </summary>
        private string _name;

        /// <summary>
        /// Internal tag variable
        /// </summary>
        private byte[] _tag;

        /// <summary>
        /// Gets or sets the name of the document
        /// </summary>
        [Browsable(false)]
        public string Name {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                Dirty = true;
                OnNameChange();
            }
        }

        /// <summary>
        /// Gets or sets the tag associated with the document
        /// </summary>
        [Browsable(false)]
        public byte[] Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                _tag = value;
                Dirty = true;
            }
        }

        /// <summary>
        /// Get a serializable property on the document
        /// </summary>
        /// <remarks>This can be used to store data important to the GUI for example, which isn't really part of the document model</remarks>
        /// <param name="name">The name of the property</param>
        /// <returns>The object, null if the property doesn't exist</returns>
        public object GetProperty(string name)
        {
            if (_properties.ContainsKey(name))
            {
                try
                {
                    return GeneralUtils.BytesToObject(_properties[name]);
                }
                catch (SerializationException)
                {
                }
            }

            return null;
        }

        /// <summary>
        /// Get a typed property
        /// </summary>
        /// <typeparam name="T">The type of object to get</typeparam>
        /// <param name="name">The name of the propert</param>
        /// <param name="create">If true create a default instance</param>
        /// <returns>The object</returns>
        public T GetProperty<T>(string name, bool create) where T : class, new()
        {
            T ret = GetProperty(name) as T;

            if ((ret == null) && create)
            {
                ret = new T();
            }

            return ret;
        }

        /// <summary>
        /// Set serializable property on the document
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="prop">The object to serialize, note this MUST be serializable</param>
        public void SetProperty(string name, object prop)
        {
            _properties[name] = GeneralUtils.ObjectToBytes(prop);
        }
        
        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>Name of document</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gets or sets the dirty flag
        /// </summary>
        [Browsable(false)]
        public bool Dirty 
        { 
            get 
            { 
                return _dirty; 
            } 
            set 
            { 
                _dirty = value; 
            } 
        }

        /// <summary>
        /// Get the document's UUID
        /// </summary>
        [Browsable(false)]
        public Guid Uuid { get; private set; } 

        /// <summary>
        /// Abstract property defining the default name
        /// </summary>
        public abstract string DefaultName { get; }

        /// <summary>
        /// Overridable method to do something special when the document is copied
        /// </summary>
        protected virtual void OnCopy()
        {
            // Do nothing
        }

        /// <summary>
        /// Copy the current document to a new object
        /// </summary>
        /// <returns>The new document object</returns>
        public IDocumentObject Copy()
        {
            // Clone using serialization, each document needs to be serializable anyway
            // to allow for trivial saving of data        
            StreamingContext context = new StreamingContext(StreamingContextStates.All, this.Uuid);    
            BinaryFormatter fmt = new BinaryFormatter(null, context);
            MemoryStream stm = new MemoryStream();

            fmt.Serialize(stm, this);
            stm.Position = 0;
            BaseDocumentObject doc = (BaseDocumentObject)fmt.Deserialize(stm);

            // Copy contains a new guid
            doc.Uuid = Guid.NewGuid();
            doc.OnCopy();

            return doc;
        }
        
        #endregion

        /// <summary>
        /// Overridable method to handle something on deserialization
        /// </summary>
        protected virtual void OnDeserialization()
        {            
            // Do nothing
        }
    
        /// <summary>
        /// Method called when deserialization complete
        /// </summary>
        /// <param name="sender"></param>
        void IDeserializationCallback.OnDeserialization(object sender)
        {
            if (_properties == null)
            {
                _properties = new Dictionary<string, byte[]>();
            }

            OnDeserialization();
        }

        /// <summary>
        /// Get the real object, if we deserialized from a file, or we have marked the context
        /// with the Guid of this object then return the current one, otherwise try and find
        /// an object out of the current project
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object GetRealObject(StreamingContext context)
        {
            object ret = this;

            if (context.State == StreamingContextStates.File)
            {
                // Do nothing in file mode                
            }
            else if ((context.Context is Guid) && (this.Uuid == (Guid)context.Context))
            {
                // Do nothing if we are at the top object                
            }
            else
            {
                IDocumentObject doc = CANAPEProject.CurrentProject.GetDocumentByUuid(this.Uuid);

                if (doc != null)
                {                    
                    ret = doc;
                }                
            }

            return ret;
        }
    }
}
