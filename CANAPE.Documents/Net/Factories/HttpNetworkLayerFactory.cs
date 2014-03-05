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
using CANAPE.Documents.Extension;
using CANAPE.Net.Layers;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Network layer for a HTTP connection
    /// </summary>
    [Serializable, NetworkLayerFactory("HttpNetworkLayerFactory", typeof(Properties.Resources))]
    public class HttpNetworkLayerFactory : BaseNetworkLayerFactory, ICustomTypeDescriptor
    {
        private HttpNetworkLayerConfig _config;

        /// <summary>
        /// Constructor
        /// </summary>
        public HttpNetworkLayerFactory()
        {
            Description = Properties.Resources.HttpNetworkLayerFactory_Name;
            _config = new HttpNetworkLayerConfig();
        }

        /// <summary>
        /// Create the layer
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public override INetworkLayer CreateLayer(Utils.Logger logger)
        {
            HttpNetworkLayer layer = new HttpNetworkLayer(_config);

            layer.Binding = Binding;

            return layer;
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
                    descs.Add(custom);
                }
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
