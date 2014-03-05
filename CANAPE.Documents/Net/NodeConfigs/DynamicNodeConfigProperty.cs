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

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// A dynamic node config property
    /// </summary>
    [Serializable]
    public sealed class DynamicNodeConfigProperty
    {
        /// <summary>
        /// The name of the property
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The property type
        /// </summary>
        public Type PropertyType { get; private set; }

        /// <summary>
        /// The default value of the property
        /// </summary>
        public object DefaultValue { get; private set; }

        /// <summary>
        /// Description of the config property
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Check that the value can be assigned to this type
        /// </summary>
        /// <param name="o">The value to check</param>
        /// <returns>True if this type can be applied</returns>
        public bool CheckType(object o)
        {
            if (o == null)
            {
                return !PropertyType.IsValueType;
            }
            else
            {
                return PropertyType.IsAssignableFrom(o.GetType());
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="propertyType">The type of the property</param>
        /// <param name="defaultValue">The default value of the property</param>
        /// <param name="description">Description of the property</param>
        public DynamicNodeConfigProperty(string name, Type propertyType, object defaultValue, string description)
        {
            Name = name;
            PropertyType = propertyType;

            if ((defaultValue == null) && (propertyType.IsValueType))
            {
                defaultValue = Activator.CreateInstance(propertyType);
            }

            DefaultValue = defaultValue;

            Description = description;
        }
    }
}
