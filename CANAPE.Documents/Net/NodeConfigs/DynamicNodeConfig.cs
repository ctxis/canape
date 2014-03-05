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
using System.ComponentModel;
using CANAPE.NodeFactories;
using CANAPE.Utils;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CANAPE.Scripting;
using CANAPE.Documents.Data;
using CANAPE.Nodes;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Config for a dynamic node
    /// </summary>
    [Serializable]
    public sealed class DynamicNodeConfig : BaseNodeConfig, IScriptProvider, ICustomTypeDescriptor
    {        
        [NonSerialized]
        private ScriptDocument _script;
        private Guid _uuid;
        private string _className;
        private Dictionary<string, dynamic> _config;

        Dictionary<string, object> IScriptProvider.Config { get { return _config; } }

        void IScriptProvider.SetDirty()
        {
            base.SetDirty();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DynamicNodeConfig()
        {            
            _className = "";
            _config = new Dictionary<string, dynamic>();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_config == null)
            {
                _config = new Dictionary<string, dynamic>();
            }
        }
        
        /// <summary>
        /// Get or set the script document which implements this node
        /// </summary>
        [TypeConverter(typeof(DocumentChoiceConverter<ScriptDocument>)), LocalizedDescription("DynamicNodeConfig_ScriptDescription", typeof(Properties.Resources)), Category("Scripting")]
        public ScriptDocument Script
        {
            get
            {
                ReloadScript();

                return _script;
            }

            set
            {
                if (value == null)
                {
                    if (_script != null)
                    {
                        _script = null;
                        _uuid = Guid.Empty;
                        SetDirty();
                    }
                }
                else
                {
                    if (value.Uuid != _uuid)
                    {
                        _script = value;
                        if (_script != null)
                        {
                            _uuid = _script.Uuid;
                        }
                        else
                        {
                            _uuid = Guid.Empty;
                        }

                        string[] classNames = _script.Container.GetClassNames(new Type[] { 
                                typeof(BasePipelineNode),
                                typeof(IDataStreamParser),
                                typeof(IDataArrayParser) });

                        if (classNames.Length > 0)
                        {
                            _className = classNames[0];
                        }
                        else
                        {
                            _className = String.Empty;
                        }

                        SetDirty();
                    }
                }
            }
        }

        private void ReloadScript()
        {
            if ((_script == null) && (_uuid != Guid.Empty))
            {
                _script = (ScriptDocument)CANAPEProject.CurrentProject.GetDocumentByUuid(_uuid);
                if (_script == null)
                {
                    _uuid = Guid.Empty;
                    _className = String.Empty;
                }
            }
        }

        /// <summary>
        /// Get or set the class name to use
        /// </summary>
        [TypeConverter(typeof(NodeClassChoiceConverter)), LocalizedDescription("DynamicNodeConfig_ClassNameDescription", typeof(Properties.Resources)), Category("Scripting")]
        public string ClassName
        {
            get
            {
                return _className;
            }
            set
            {
                if (_className != value)
                {
                    _className = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Name of graph node
        /// </summary>
        public const string NodeName = "dynnode";

        /// <summary>
        /// Get graph node name
        /// </summary>
        /// <returns>Always "server"</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }        

        /// <summary>
        /// Method called to create the factory
        /// </summary>
        /// <returns>The BaseNodeFactory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            ReloadScript();

            Dictionary<string, dynamic> config = new Dictionary<string, dynamic>();

            if (_script != null)
            {
                foreach (DynamicNodeConfigProperty p in _script.Configuration)
                {                    
                    if (_config.ContainsKey(p.Name) && p.CheckType(_config[p.Name]))
                    {
                        config[p.Name] = _config[p.Name];
                    }
                    else
                    {
                        config[p.Name] = p.DefaultValue;
                    }
                }
            }

            DynamicNodeFactory factory = new DynamicNodeFactory(_label, _id, _script != null ? _script.Container : null, 
                _className, new DynamicConfigObject(config));

            return factory;
        }

        #region  ICustomTypeDescriptor Members

        /// <summary>
        /// A custom property descriptor, used to implement custom merging of types
        /// </summary>
        private sealed class DynamicNodePropertyDescriptor : PropertyDescriptor
        {            
            private DynamicNodeConfigProperty _property;

            /// <summary>
            /// Event to signal a property has been changed
            /// </summary>
            public event EventHandler PropertyChanged;

            /// <summary>
            /// Constructor
            /// </summary>        
            /// <param name="property">The property description</param>
            /// <param name="attributes">Attributes of the property</param>            
            public DynamicNodePropertyDescriptor(DynamicNodeConfigProperty property, Attribute[] attributes)
                : base(property.Name, attributes)
            {                
                _property = property;
            }

            /// <summary>
            /// Can the value be reset
            /// </summary>
            /// <param name="component">The component to use</param>
            /// <returns>True if it can reset</returns>
            public override bool CanResetValue(object component)
            {
                return true;
            }

            /// <summary>
            /// Get component type
            /// </summary>
            public override Type ComponentType
            {
                get { return typeof(DynamicNodeConfig); }
            }

            /// <summary>
            /// Get the property value
            /// </summary>
            /// <param name="component">The component to use</param>
            /// <returns>The property value</returns>
            public override object GetValue(object component)
            {
                DynamicNodeConfig config = component as DynamicNodeConfig;

                if(config != null)
                {
                    if (config._config.ContainsKey(_property.Name))
                    {
                        return config._config[_property.Name];
                    }
                    else
                    {
                        return _property.DefaultValue;
                    }
                }
                else
                {
                    return null;
                }
            }

            /// <summary>
            /// Is the property read only
            /// </summary>
            public override bool IsReadOnly
            {
                get { return false; }
            }

            /// <summary>
            /// Get the type of property
            /// </summary>
            public override Type PropertyType
            {
                get { return _property.PropertyType; }
            }

            /// <summary>
            /// Reset the value
            /// </summary>
            /// <param name="component">The component to use</param>
            public override void ResetValue(object component)
            {
                DynamicNodeConfig config = component as DynamicNodeConfig;

                if (config != null)
                {
                    config._config[_property.Name] = _property.DefaultValue;
                }
            }

            /// <summary>
            /// Set the property value
            /// </summary>
            /// <param name="component">The component to use</param>
            /// <param name="value">The value to set</param>
            public override void SetValue(object component, object value)
            {
                if (PropertyChanged != null)
                {
                    bool changed = false;
                    object old = GetValue(component);

                    if (old != null)
                    {
                        if (!old.Equals(value))
                        {
                            changed = true;
                        }
                    }
                    else if (value != null)
                    {
                        changed = true;
                    }

                    if (changed)
                    {
                        PropertyChanged.Invoke(this, new EventArgs());
                    }
                }

                DynamicNodeConfig config = component as DynamicNodeConfig;

                if (config != null)
                {
                    config._config[_property.Name] = value;
                }
            }

            /// <summary>
            /// Should something serialize this value
            /// </summary>
            /// <param name="component">The component to use</param>
            /// <returns>True if it should serialize</returns>
            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }
        }

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

            ScriptDocument doc = Script;
            
            if (doc != null)
            {
                Dictionary<string, dynamic> config = new Dictionary<string, dynamic>();

                foreach (DynamicNodeConfigProperty p in doc.Configuration)
                {
                    // Copy across any valid properties
                    if (_config.ContainsKey(p.Name) && p.CheckType(_config[p.Name]))
                    {
                        config[p.Name] = _config[p.Name];
                    }
                    else
                    {
                        config[p.Name] = p.DefaultValue;
                    }

                    List<Attribute> attrs = new List<Attribute>();
                    attrs.Add(new CategoryAttribute("Node Config"));

                    if (!String.IsNullOrWhiteSpace(p.Description))
                    {
                        attrs.Add(new DescriptionAttribute(p.Description));
                    }

                    if (typeof(IDocumentObject).IsAssignableFrom(p.PropertyType))
                    {
                        attrs.Add(new TypeConverterAttribute(typeof(DocumentChoiceConverter<>).MakeGenericType(p.PropertyType)));
                    }

                    DynamicNodePropertyDescriptor custom = new DynamicNodePropertyDescriptor(p, attrs.ToArray());
                    custom.PropertyChanged += new EventHandler(custom_PropertyChanged);
                    descs.Add(custom);
                }

                _config = config;
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
