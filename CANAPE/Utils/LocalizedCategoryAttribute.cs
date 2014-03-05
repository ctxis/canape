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
using System.Resources;

namespace CANAPE.Utils
{
    /// <summary>
    /// A category attribute which can be localized
    /// </summary>
    public sealed class LocalizedCategoryAttribute : CategoryAttribute
    {
        /// <summary>
        /// Default, non-localized constructor
        /// </summary>
        /// <param name="category">The category name</param>
        public LocalizedCategoryAttribute(string category) : base(category)
        {
        }

        /// <summary>
        /// Localized constructor, passes a localizable string and the type this is applied to
        /// </summary>
        /// <param name="localizableCategory">The localizable category name</param>
        /// <param name="assignedType">The type of resources (normally Properties.Resources)</param>
        public LocalizedCategoryAttribute(string localizableCategory, Type assignedType) 
            : base(new ResourceManager(assignedType).GetString(localizableCategory, GeneralUtils.GetCurrentCulture()))
        {
        }
    }
}
