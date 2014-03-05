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
using System.Runtime.Serialization;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Net;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Server factory based on a endpoint
    /// </summary>
    [Serializable]
    public class ScriptDataEndpointFactory : DataEndpointFactory
    {
        /// <summary>
        /// The script document to use
        /// </summary>
        [TypeConverter(typeof(DocumentChoiceConverter<ScriptDocument>)),
            LocalizedDescription("ScriptDataEndpointFactory_ScriptDescription", typeof(Properties.Resources)), Category("Scripting")]
        public ScriptDocument Script { get; set; }

        /// <summary>
        /// The name of the class
        /// </summary>
        public string ClassName { get; set; }
        
        [Serializable]
        class ServerConfig : ICustomTypeDescriptor
        {
            public Dictionary<string, string> Properties { get; set; }

            public ServerConfig()
            {
                Properties = new Dictionary<string,string>();
                _config = new Dictionary<string, object>(); 
            }
        
            Dictionary<string, object> _config;
            ScriptDataEndpointFactory _factory;

            private Dictionary<string, object> CurrConfig()
            {
                if (_config == null)
                {
                    _config = new Dictionary<string, object>();
                }
                return _config;
            }
            
            public void SetFactory(ScriptDataEndpointFactory factory)
            {
                _factory = factory;
            }

            public DynamicConfigObject GetConfig()
            {
                Dictionary<string, object> config = new Dictionary<string,object>();

                if (_factory.Script != null)
                {
                    foreach (DynamicNodeConfigProperty p in _factory.Script.Configuration)
                    {
                        if (CurrConfig().ContainsKey(p.Name) && p.CheckType(CurrConfig()[p.Name]))
                        {
                            config[p.Name] = CurrConfig()[p.Name];
                        }
                        else
                        {
                            config[p.Name] = p.DefaultValue;
                        }
                    }
                }

                return new DynamicConfigObject(config);
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
                    ServerConfig config = component as ServerConfig;

                    if (config != null)
                    {
                        if (config.CurrConfig().ContainsKey(_property.Name))
                        {
                            return config.CurrConfig()[_property.Name];
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
                    ServerConfig config = component as ServerConfig;

                    if (config != null)
                    {
                        config.CurrConfig()[_property.Name] = _property.DefaultValue;
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

                    ServerConfig config = component as ServerConfig;

                    if (config != null)
                    {
                        config.CurrConfig()[_property.Name] = value;
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

                ScriptDocument doc = _factory.Script;

                if (doc != null)
                {
                    Dictionary<string, dynamic> config = new Dictionary<string, dynamic>();

                    foreach (DynamicNodeConfigProperty p in doc.Configuration)
                    {
                        // Copy across any valid properties
                        if (CurrConfig().ContainsKey(p.Name) && p.CheckType(CurrConfig()[p.Name]))
                        {
                            config[p.Name] = CurrConfig()[p.Name];
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
                        //custom.PropertyChanged += new EventHandler(custom_PropertyChanged);
                        descs.Add(custom);
                    }

                    _config = config;
                }

                return new PropertyDescriptorCollection(descs.ToArray());
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
                    return CurrConfig();
                }
                else
                {
                    return this;
                }
            }
            #endregion
        }

        /// <summary>
        /// Create the data endpoint
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="globalMeta">Global meta dictionary</param>
        /// <param name="meta">Meta dictionary</param>
        /// <returns></returns>
        public override IDataEndpoint Create(Logger logger, MetaDictionary meta, MetaDictionary globalMeta)
        {
            IDataEndpoint ret = Script.Container.GetInstance(ClassName) as IDataEndpoint;

            if (ret == null)
            {
                throw new NetServiceException(CANAPE.Documents.Properties.Resources.ScriptDataEndpointFactory_InvalidType);
            }

            ServerConfig config = (ServerConfig)Config;

            foreach (KeyValuePair<string, string> pair in config.Properties)
            {
                meta[pair.Key] = pair.Value;
            }

            ret.Meta = meta;

            ret.GlobalMeta = globalMeta;

            if (ret is IPersistNode)
            {
                IPersistNode persist = ret as IPersistNode;

                persist.SetState(config.GetConfig(), logger);
            }

            return ret;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext ctx)
        {
            ServerConfig config = (ServerConfig)Config;

            if (config == null)
            {
                config = new ServerConfig();                
            }

            config.SetFactory(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document">The script document</param>
        /// <param name="className">The class name</param>
        public ScriptDataEndpointFactory(ScriptDocument document, string className)
            : base(className, new ServerConfig())
        {
            ServerConfig config = Config as ServerConfig;
            config.SetFactory(this);
            Script = document;
            ClassName = className;
        }
    }
}
