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

namespace CANAPE.Extension
{
    /// <summary>
    /// Indicates an extension which is shown on the main menu
    /// </summary>
    public class MainFormMenuExtensionAttribute : MenuExtensionAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of extension</param>
        public MainFormMenuExtensionAttribute(string name) : base(name)
        {
        }

        /// <summary>
        /// Constructor for localized resources
        /// </summary>
        /// <param name="localizableName">The name of the localizable resource</param>
        /// <param name="assignedType">The type of resources</param>
        public MainFormMenuExtensionAttribute(string localizableName, Type assignedType) 
            : base(localizableName, assignedType)            
        {
        }

        /// <summary>
        /// Register a type
        /// </summary>
        /// <param name="t">The type to register</param>
        public override void RegisterType(Type t)
        {
            MainFormMenuExtensionManager.Instance.RegisterExtension(this, t);
        }

        /// <summary>
        /// Unregister a type
        /// </summary>
        /// <param name="t">The type to register</param>
        public override void UnregisterType(Type t)
        {
            MainFormMenuExtensionManager.Instance.UnregisterExtension(this, t);
        }
    }
}
