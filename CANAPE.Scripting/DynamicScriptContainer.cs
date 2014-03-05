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
using System.Reflection;
using System.Runtime.Serialization;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Script container which also includes a default class name
    /// </summary>
    [Serializable]
    public sealed class DynamicScriptContainer
    {
        private static Dictionary<DynamicScriptContainer, DynamicScriptContainer> _cache = new Dictionary<DynamicScriptContainer, DynamicScriptContainer>();

        private ScriptContainer _container;
        private string _classname;

        /// <summary>
        /// Constructor, internal use only
        /// </summary>
        /// <param name="container">The script container</param>
        /// <param name="classname">The classname</param>
        private DynamicScriptContainer(ScriptContainer container, string classname)             
        {
            // Clone the container
            _container = new ScriptContainer(container);
            _classname = classname;
        }

        /// <summary>
        /// Get a cached version of the container
        /// </summary>
        /// <param name="key">The container</param>
        /// <returns>The identified script container</returns>
        public static DynamicScriptContainer GetCachedContainer(DynamicScriptContainer key)
        {
            lock (_cache)
            {
                if (!_cache.ContainsKey(key))
                {
                    _cache[key] = key;
                }

                return _cache[key];
            }
        }
                
        /// <summary>
        /// Create a container
        /// </summary>
        /// <param name="container">The script container</param>
        /// <param name="classname">The classname</param>
        /// <returns>The new container, will reuse an existing one if appropriate</returns>
        public static DynamicScriptContainer Create(ScriptContainer container, string classname)
        {
            return GetCachedContainer(new DynamicScriptContainer(container, classname));
        }

        /// <summary>
        /// Create a container, used for types which are implemented by non-scripted assemblies
        /// </summary>
        /// <param name="type">The type, must be in a referencable assembly, not a scripted type</param>
        /// <returns>The new container, will reuse an existing one if appropriate</returns>
        public static DynamicScriptContainer Create(Type type)
        {
            return Create(new ScriptContainer(type.Assembly), type.FullName);
        }

        /// <summary>
        /// Get an instance of a class from the script using internal classname
        /// </summary>        
        /// <returns>An instance of the object</returns>
        public object GetInstance()
        {
            return ScriptUtils.GetInstance(_container, _classname);
        }

        /// <summary>
        /// Get an instance of a class from the script using internal classname
        /// </summary>        
        /// <param name="args">The arguments for the constructor</param>
        /// <returns>An instance of the object</returns>
        public object GetInstance(params object[] args)
        {
            return ScriptUtils.GetInstance(_container, _classname, args);
        }

        /// <summary>
        /// Invoke a static method on the script using internal classname
        /// </summary>        
        /// <param name="methodname">The method name</param>
        /// <param name="args">The arguments</param>
        /// <returns>The return value of the method</returns>
        public object Invoke(string methodname, params object[] args)
        {
            return ScriptUtils.Invoke(_container, _classname, methodname, args);
        }

        /// <summary>
        /// Equals method
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>True if other object is equal</returns>
        public override bool Equals(object obj)
        {
            bool ret = ReferenceEquals(this, obj);
            
            // If reference equals then it is automatically the same
            if (!ret)
            {
                DynamicScriptContainer other = obj as DynamicScriptContainer;

                if (other != null)
                {
                    ret = _container.Equals(other._container) && _classname == other._classname;
                }
                else
                {
                    ret = false;
                }
            }

            return ret;
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The object hash code</returns>
        public override int GetHashCode()
        {
            return _container.GetHashCode() ^ _classname.GetHashCode();
        }
    }
}
