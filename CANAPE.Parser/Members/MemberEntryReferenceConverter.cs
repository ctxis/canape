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
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace CANAPE.Parser
{
    /// <summary>
    /// Type converter to setup a reference, real simple for now, only lets you use the current sequence
    /// </summary>
    public class MemberEntryReferenceConverter : TypeConverter
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

        private MemberEntryReference GetReference(ITypeDescriptorContext context)
        {
            object target = ParserUtils.GetCompatibleType(context.PropertyDescriptor.ComponentType, context.Instance);

            if (target != null)
            {
                return (MemberEntryReference)context.PropertyDescriptor.GetValue(target);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Get list of standard values
        /// </summary>
        /// <param name="context">Converter context</param>
        /// <returns>The list of standard values</returns>
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> names = new List<string>();  

            MemberEntry entry = context.Instance as MemberEntry;
            MemberEntryReference memberRef = GetReference(context);

            if ((entry != null) && (entry.Parent != null))
            {
                foreach (MemberEntry currEntry in entry.Parent.Members)
                {
                    // For now we only support length fields which come before the array
                    if (currEntry == entry)
                    {
                        break;
                    }

                    if (memberRef.IsValidType(currEntry))
                    {
                        names.Add(currEntry.Name);
                    }
                }
            }

            return new StandardValuesCollection(names.ToArray());
        }

        /// <summary>
        /// Whether can convert from a type (only string)
        /// </summary>
        /// <param name="context">Converter context</param>
        /// <param name="sourceType">The source type</param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(String))
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

            if (value.GetType() == typeof(String))
            {
                MemberEntryReference memberRef = GetReference(context);

                MemberEntryReference reference = new MemberEntryReference((MemberEntry)context.Instance, memberRef.ValidTypes.ToArray());

                reference.SetReferenceChain(value.ToString());

                ret = reference;
            }
            else    
            {
                ret = base.ConvertFrom(context, culture, value);
            }

            return ret;
        }
    }
}
