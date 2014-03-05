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
using System.Text.RegularExpressions;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// A type converter to transform between strings and a regex
    /// </summary>
    public class RegexConverter : TypeConverter
    {
        /// <summary>
        /// Determine if the type can be converted
        /// </summary>
        /// <param name="context">The type context</param>
        /// <param name="destinationType">The destination type</param>
        /// <returns>True if string or regex</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            else if (destinationType == typeof(Regex))
            {
                return true;
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
        }

        /// <summary>
        /// Convert a value to a specified type
        /// </summary>
        /// <param name="context">The type context</param>
        /// <param name="culture">The culture</param>
        /// <param name="value">The object value</param>
        /// <param name="destinationType">The destination type</param>
        /// <returns>The converted object</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((Regex)value).ToString();
            }
            else if (destinationType == typeof(Regex))
            {
                return value;
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        /// <summary>
        /// Determine if can convert from a type
        /// </summary>
        /// <param name="context">The type context</param>
        /// <param name="sourceType">The type to convert from</param>
        /// <returns>True if can convert</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(String))
            {
                return true;
            }
            else if(sourceType == typeof(Regex))
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
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            object ret = null;

            if (value is string)
            {
                ret = new Regex(value.ToString());
            }
            else if (value is Regex)
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
