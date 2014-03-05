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
using CANAPE.Documents.Data;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// A class to implement a custom type descriptor around a script document
    /// </summary>
    public class ScriptDynamicTypeDescriptor : ICustomTypeDescriptor
    {
        IScriptProvider _provider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="provider"></param>
        public ScriptDynamicTypeDescriptor(IScriptProvider provider)
        {
            _provider = provider;
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
                IScriptProvider config = component as IScriptProvider;

                if (config != null)
                {
                    if (config.Config.ContainsKey(_property.Name))
                    {
                        return config.Config[_property.Name];
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
                IScriptProvider config = component as IScriptProvider;

                if (config != null)
                {
                    config.Config[_property.Name] = _property.DefaultValue;
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

                IScriptProvider config = component as IScriptProvider;

                if (config != null)
                {
                    config.Config[_property.Name] = value;
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
            return TypeDescriptor.GetAttributes(_provider, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(_provider, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(_provider, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(_provider, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(_provider, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(_provider, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(_provider, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(_provider, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(_provider, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            List<PropertyDescriptor> descs = new List<PropertyDescriptor>();
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(_provider, attributes, true);

            foreach (PropertyDescriptor d in coll)
            {
                descs.Add(d);
            }

            ScriptDocument doc = _provider.Script;

            if (doc != null)
            {
                Dictionary<string, dynamic> config = new Dictionary<string, dynamic>(_provider.Config);

                _provider.Config.Clear();

                foreach (DynamicNodeConfigProperty p in doc.Configuration)
                {
                    // Copy across any valid properties
                    if (config.ContainsKey(p.Name) && p.CheckType(config[p.Name]))
                    {
                        _provider.Config[p.Name] = config[p.Name];
                    }
                    else
                    {
                        _provider.Config[p.Name] = p.DefaultValue;
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
            }

            return new PropertyDescriptorCollection(descs.ToArray());
        }

        void custom_PropertyChanged(object sender, EventArgs e)
        {
            _provider.SetDirty();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            ICustomTypeDescriptor desc = (ICustomTypeDescriptor)_provider;

            return desc.GetProperties(new Attribute[0]);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            if (pd is CustomPropertyDescriptor)
            {
                return _provider.Config;
            }
            else
            {
                return _provider;
            }
        }
        #endregion
    }
}
