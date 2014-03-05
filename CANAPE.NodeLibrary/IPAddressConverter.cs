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
using System.Net;

namespace CANAPE.NodeLibrary
{
    public class IPAddressConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(String))
            {
                return true;
            }
            else if (typeof(IPAddress).IsAssignableFrom(sourceType))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            object ret = null;

            if (value is String)
            {
                IPAddress addr = null;

                if (IPAddress.TryParse(value.ToString(), out addr))
                {
                    ret = addr;
                }
                else
                {
                    ret = IPAddress.Any;
                }
            }
            else if (typeof(IPAddress).IsAssignableFrom(value.GetType()))
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
