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
using CANAPE.NodeFactories;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Config for a library node
    /// </summary>
    [Serializable]
    public class LibraryNodeConfig : BaseNodeConfig, ICustomTypeDescriptor
    {
        private Type _type;        
        private object _config;

        /// <summary>
        /// The class name of the node
        /// </summary>
        [LocalizedDescription("LibraryNodeConfig_ClassNameDescription", typeof(Properties.Resources)), Category("Scripting")]
        public string ClassName
        {
            get;
            private set; 
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The type of the implementation</param>
        /// <param name="name">The name of the node</param>
        /// <param name="config">Optional configuration</param>
        public LibraryNodeConfig(Type type, string name, object config)
        {            
            _type = type;
            _config = config;
            ClassName = name;
        }

        /// <summary>
        /// Name of the node
        /// </summary>
        public const string NodeName = "libnode";

        /// <summary>
        /// Get the node name
        /// </summary>
        /// <returns>The node name</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }

        /// <summary>
        /// Method to create the factory
        /// </summary>
        /// <returns>The new factory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            return new LibraryNodeFactory(_label, _id, _type.Assembly.FullName, _type.FullName, _config);
        }

        #region  ICustomTypeDescriptor Members
        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            List<PropertyDescriptor> descs = new List<PropertyDescriptor>();
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(this, attributes, true);

            foreach (PropertyDescriptor d in coll)
            {
                descs.Add(d);
            }

            if (_config != null)
            {
                PropertyDescriptorCollection coll2 = TypeDescriptor.GetProperties(_config, attributes, true);
                foreach (PropertyDescriptor p in coll2)
                {
                    List<Attribute> attrs = new List<Attribute>();
                    bool hasCategory = false;
                    foreach (Attribute a in p.Attributes)
                    {
                        if (a is CategoryAttribute)
                        {
                            hasCategory = true;
                        }

                        attrs.Add(a);
                    }

                    if (!hasCategory)
                    {
                        attrs.Add(new CategoryAttribute("Node Config"));
                    }

                    CustomPropertyDescriptor custom = new CustomPropertyDescriptor(p, p.Name, attrs.ToArray());
                    custom.PropertyChanged += new EventHandler(custom_PropertyChanged);
                    descs.Add(custom);
                }
            }

            return new PropertyDescriptorCollection(descs.ToArray());
        }

        void custom_PropertyChanged(object sender, EventArgs e)
        {
            SetDirty();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            ICustomTypeDescriptor desc = (ICustomTypeDescriptor)this;

            return desc.GetProperties(new Attribute[0]);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            if (pd is CustomPropertyDescriptor)
            {
                return _config;
            }
            else
            {
                return this;
            }
        }
#endregion
    }
}
