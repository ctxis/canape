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
using CANAPE.Extension;
using System.Resources;

namespace CANAPE.NodeLibrary
{
    /// <summary>
    /// Attribute to indicate a class is a library nodes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NodeLibraryClassAttribute : CANAPEExtensionAttribute
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public Type ConfigType { get; set; }
        public string NodeName { get; set; }
        public NodeLibraryClassCategory Category { get; set; }        

        /// <summary>
        /// Normal constructor
        /// </summary>
        /// <param name="name"></param>        
        public NodeLibraryClassAttribute(string name)
        {
            Name = name;
            Description = "";
        }

        /// <summary>
        /// Localised constructor
        /// </summary>
        /// <param name="className">The name of the class</param>
        /// <param name="resourceType">The resource type</param>
        public NodeLibraryClassAttribute(string className, Type resourceType) 
            : this(className + "_Name", className + "_Description", className + "_NodeName", resourceType)
        {

        }

        /// <summary>
        /// Localizable constructor
        /// </summary>
        /// <param name="name">The localized name</param>
        /// <param name="description">The localized description</param>
        /// <param name="nodename">The localized node name</param>
        /// <param name="resourceType">The resource type to load from</param>
        public NodeLibraryClassAttribute(string name, string description, string nodename, Type resourceType)
        {
            ResourceManager manager = new ResourceManager(resourceType);

            Name = manager.GetString(name);
            Description = manager.GetString(description);
            NodeName = manager.GetString(nodename);
        }

        public override void RegisterType(Type t)
        {
            NodeLibraryManager.RegisterLibraryType(this, t);
        }

        public override void UnregisterType(Type t)
        {
            NodeLibraryManager.UnregisterLibraryType(this, t);
        }
    }
}
