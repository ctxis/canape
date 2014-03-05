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
using System.Resources;
using System.Text;
using CANAPE.Extension;

namespace CANAPE.Documents.Extension
{
    /// <summary>
    /// Network layer factory attribute
    /// </summary>
    public sealed class NetworkLayerFactoryAttribute : CANAPEExtensionAttribute
    {
        /// <summary>
        /// Name of the layer
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Description of the layer
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Normal constructor
        /// </summary>
        /// <param name="name"></param>        
        public NetworkLayerFactoryAttribute(string name)
        {
            Name = name;
            Description = "";
        }

        /// <summary>
        /// Localised constructor
        /// </summary>
        /// <param name="className">The name of the class</param>
        /// <param name="resourceType">The resource type</param>
        public NetworkLayerFactoryAttribute(string className, Type resourceType) 
            : this(className + "_Name", className + "_Description", resourceType)
        {

        }

        /// <summary>
        /// Localizable constructor
        /// </summary>
        /// <param name="name">The localized name</param>
        /// <param name="description">The localized description</param>        
        /// <param name="resourceType">The resource type to load from</param>
        public NetworkLayerFactoryAttribute(string name, string description, Type resourceType)
        {
            ResourceManager manager = new ResourceManager(resourceType);

            Name = manager.GetString(name);
            Description = manager.GetString(description);            
        }

        /// <summary>
        /// Register the type
        /// </summary>
        /// <param name="t">The type to register</param>
        public override void RegisterType(Type t)
        {
            NetworkLayerFactoryManager.Instance.RegisterExtension(this, t);
        }

        /// <summary>
        /// Unregister the type
        /// </summary>
        /// <param name="t">The type to unregister</param>
        public override void UnregisterType(Type t)
        {
            NetworkLayerFactoryManager.Instance.UnregisterExtension(this, t);
        }
    }
}
