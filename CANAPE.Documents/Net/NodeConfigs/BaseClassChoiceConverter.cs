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

using System.ComponentModel;
using System;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Type converter to get classnames from a script document
    /// </summary>
    public abstract class BaseClassChoiceConverter : TypeConverter
    {
        /// <summary>
        /// Method to get the types which this converter handles
        /// </summary>
        /// <returns>A list of types this converter handles</returns>
        protected abstract Type[] GetChoiceTypes();

        /// <summary>
        /// Get whether standard values are supported
        /// </summary>
        /// <param name="context">The type context</param>
        /// <returns>True if supported</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            IScriptProvider config = context.Instance as IScriptProvider;

            if (config != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get standard values
        /// </summary>
        /// <param name="context">The type context</param>
        /// <returns>The list of standard values</returns>
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            IScriptProvider config = context.Instance as IScriptProvider;

            if (config != null)
            {
                if (config.Script != null)
                {
                    return new StandardValuesCollection(config.Script.Container.GetClassNames(GetChoiceTypes()));
                }
                else
                {
                    return new StandardValuesCollection(new string[0]);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
