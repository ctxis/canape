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
using CANAPE.Scripting;
using System.Reflection;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// A base factory for a library node
    /// </summary>
    [Serializable]
    public class LibraryNodeFactory : DynamicNodeFactory
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">Name of the node</param>
        /// <param name="guid">The associated guid</param>
        /// <param name="className">The name of the class to create</param>        
        /// <param name="assemblyName">The assembly name containing the library type</param>
        /// <param name="state">The object state</param>
        public LibraryNodeFactory(string label, Guid guid, string assemblyName, string className, object state) : base(label, guid, null, className, state)
        {   
            this.Container = new ScriptContainer("assembly", guid, assemblyName, false, new AssemblyName[0]);
        }        
    }
}
