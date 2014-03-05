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
using System.Globalization;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Type convert to select node
    /// </summary>
    /// <typeparam name="T">The nase node type to select</typeparam>
    public class NodeChoiceConverter<T> : TypeConverter where T : BaseNodeConfig
    {
        /// <summary>
        /// Determine if standard values supported
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>Always returns true</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Get list of standard values
        /// </summary>
        /// <param name="context">The type context</param>
        /// <returns>The list of document names</returns>
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<T> nodes = new List<T>();
            BaseNodeConfig config = context.Instance as BaseNodeConfig;

            if (config != null)
            {
                foreach(BaseNodeConfig node in config.Document.Nodes)
                {
                    T n = node as T;

                    if ((n != null) && !n.Equals(config))
                    {
                        nodes.Add(n);
                    }
                }
            }

            return new TypeConverter.StandardValuesCollection(nodes.ToArray());
        }

        /// <summary>
        /// Determine if can convert from a type
        /// </summary>
        /// <param name="context">The type context</param>
        /// <param name="sourceType">The type to convert from</param>
        /// <returns>True if can convert</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if ((sourceType == typeof(string)) && context.Instance is BaseNodeConfig)
            {
                return true;
            }
            else if (typeof(T).IsAssignableFrom(sourceType))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }

        /// <summary>
        /// Convert an object to a document
        /// </summary>
        /// <param name="context">The type context</param>
        /// <param name="culture">The culture of the conversion</param>
        /// <param name="value">The value to convert</param>
        /// <returns>The converted value</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object ret = null;

            if (value.GetType() == typeof(string))
            {
                BaseNodeConfig config = context.Instance as BaseNodeConfig;

                if (config != null)
                {
                    foreach (BaseNodeConfig node in config.Document.Nodes)
                    {
                        if (node.Label == value.ToString())
                        {
                            return node;
                        }
                    }
                }
            }
            else if (typeof(T).IsAssignableFrom(value.GetType()))
            {
                ret = value;
            }
            else
            {
                ret = base.ConvertFrom(context, culture, value);
            }

            return ret;
        }
    }
}
