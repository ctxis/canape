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

namespace CANAPE.Utils
{
    /// <summary>
    /// A custom property descriptor, used to implement custom merging of types
    /// </summary>
    public sealed class CustomPropertyDescriptor : PropertyDescriptor
    {
        PropertyDescriptor _desc;

        /// <summary>
        /// Event to signal a property has been changed
        /// </summary>
        public event EventHandler PropertyChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="desc">The base property descriptor</param>
        /// <param name="name">The name of the property</param>
        /// <param name="attributes">Attributes of the property</param>
        public CustomPropertyDescriptor(PropertyDescriptor desc, string name, Attribute[] attributes)
            : base(name, attributes)
        {
            _desc = desc;
        }

        /// <summary>
        /// Can the value be reset
        /// </summary>
        /// <param name="component">The component to use</param>
        /// <returns>True if it can reset</returns>
        public override bool CanResetValue(object component)
        {
            return _desc.CanResetValue(component);
        }

        /// <summary>
        /// Get component type
        /// </summary>
        public override Type ComponentType
        {
            get { return _desc.ComponentType; }
        }

        /// <summary>
        /// Get the property value
        /// </summary>
        /// <param name="component">The component to use</param>
        /// <returns>The property value</returns>
        public override object GetValue(object component)
        {
            if (_desc.ComponentType.IsAssignableFrom(component.GetType()))
            {
                return _desc.GetValue(component);
            }            

            return null;
        }

        /// <summary>
        /// Is the property read only
        /// </summary>
        public override bool IsReadOnly
        {
            get { return _desc.IsReadOnly; }
        }

        /// <summary>
        /// Get the type of property
        /// </summary>
        public override Type PropertyType
        {
            get { return _desc.PropertyType; }
        }

        /// <summary>
        /// Reset the value
        /// </summary>
        /// <param name="component">The component to use</param>
        public override void ResetValue(object component)
        {
            _desc.ResetValue(component);
        }

        /// <summary>
        /// Set the property value
        /// </summary>
        /// <param name="component">The component to use</param>
        /// <param name="value">The value to set</param>
        public override void SetValue(object component, object value)
        {
            if (_desc.ComponentType.IsAssignableFrom(component.GetType()))
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

                _desc.SetValue(component, value);
            }
        }

        /// <summary>
        /// Should something serialize this value
        /// </summary>
        /// <param name="component">The component to use</param>
        /// <returns>True if it should serialize</returns>
        public override bool ShouldSerializeValue(object component)
        {
            return _desc.ShouldSerializeValue(component);
        }
    }
}
