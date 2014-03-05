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
using CANAPE.DataFrames;
using CANAPE.Nodes;

namespace CANAPE.Scripting
{
    /// <summary>
    /// A class to contain script code
    /// </summary>
    [Serializable]
    public class ScriptContainer
    {
        // Add runtime properties

        /// <summary>
        /// The script code itself, can be overridden
        /// </summary>
        public virtual string Script
        {
            get;
            set;
        }

        /// <summary>
        /// The engine to use
        /// </summary>
        public string Engine { get; private set; }

        /// <summary>
        /// List of assembly name references for compilation
        /// </summary>
        public IList<AssemblyName> ReferencedNames { get; private set; }

        /// <summary>
        /// The UUID of the script, used to track its source
        /// </summary>
        public Guid Uuid { get; set; }

        /// <summary>
        /// Enable debugging for this script
        /// </summary>
        public bool EnableDebug { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="engine">The script engine</param>
        /// <param name="uuid">The uuid of the script</param>
        /// <param name="script">The script text itself</param>
        /// <param name="enableDebug">Whether debug should be enabled</param>
        public ScriptContainer(string engine, Guid uuid, string script, bool enableDebug) 
            : this(engine, uuid, script, enableDebug, new AssemblyName[] { typeof(ScriptUtils).Assembly.GetName(), typeof(BasePipelineNode).Assembly.GetName() })
        {            
        }

        /// <summary>
        /// Clone constructor, copy everything but the script
        /// </summary>
        /// <param name="container">The existing container</param>
        /// <param name="newScript">New script to use</param>
        public ScriptContainer(ScriptContainer container, string newScript) 
            : this(container.Engine, container.Uuid, newScript, container.EnableDebug, container.ReferencedNames)         
        {            
            
        }

        /// <summary>
        /// Clone constructor, copy everything
        /// </summary>
        /// <param name="container">The existing container</param>
        public ScriptContainer(ScriptContainer container) 
            : this(container, container.Script)
        {            
        }

        /// <summary>
        /// Constructor
        /// </summary>        
        /// <param name="engine">The script engine</param>        
        /// <param name="enableDebug">Whether to enable dbeug</param>
        /// <param name="referencedAssemblies">Referenced assemblies</param>
        /// <param name="script">The script code</param>
        /// <param name="uuid">The uuid of the script</param>
        public ScriptContainer(string engine, Guid uuid, string script, 
            bool enableDebug, IEnumerable<AssemblyName> referencedAssemblies)
        {
            if (script == null)
            {
                Script = "";
            }
            else
            {
                Script = script;
            }

            Engine = engine;
            Uuid = uuid;
            EnableDebug = enableDebug;
            
            ReferencedNames = new AssemblyNameList(referencedAssemblies);
        }

        /// <summary>
        /// Construct an engine from an assembly
        /// </summary>
        /// <param name="asm">The assembly</param>
        public ScriptContainer(Assembly asm) : this("assembly", Guid.Empty, asm.FullName, false)
        {
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            if (ReferencedNames == null)
            {
                ReferencedNames = new AssemblyNameList();
            }
        }

        /// <summary>
        /// Get an instance of a class from the script
        /// </summary>
        /// <param name="classname">The classname to get</param>
        /// <returns>An instance of the object</returns>
        public object GetInstance(string classname)
        {
            return ScriptUtils.GetInstance(this, classname);
        }

        /// <summary>
        /// Get an instance of a class from the script
        /// </summary>
        /// <param name="classname">The classname to get</param>
        /// <param name="args">The arguments for the constructor</param>
        /// <returns>An instance of the object</returns>
        public object GetInstance(string classname, params object[] args)
        {
            return ScriptUtils.GetInstance(this, classname, args);
        }

        /// <summary>
        /// Invoke a static method on the script
        /// </summary>
        /// <param name="classname">The classname to use</param>
        /// <param name="methodname">The method name</param>
        /// <param name="args">The arguments</param>
        /// <returns>The return value of the method</returns>
        public object Invoke(string classname, string methodname, params object[] args)
        {
            return ScriptUtils.Invoke(this, classname, methodname, args);
        }

        /// <summary>
        /// Get class names from the script
        /// </summary>
        /// <param name="types">Optional list of types</param>
        /// <returns>Get list of classes for this container</returns>
        public string[] GetClassNames(params Type[] types)
        {
            return ScriptUtils.GetTypes(this, types);
        }

        /// <summary>
        /// Equals method
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>True if other object is equal</returns>
        public override bool Equals(object obj)
        {
            bool ret = ReferenceEquals(obj, this);
            ScriptContainer other = obj as ScriptContainer;

            if (ret && other != null)
            {
                return true;
            }

            if (other != null)
            {
                if ((Engine == other.Engine) && (Script == other.Script) && (Uuid == other.Uuid)
                    && (EnableDebug == other.EnableDebug))
                {
                    if ((ReferencedNames != null) && (other.ReferencedNames != null) && (ReferencedNames.Count == other.ReferencedNames.Count))
                    {
                        ret = true;

                        for (int i = 0; i < ReferencedNames.Count; ++i)
                        {
                            if (ReferencedNames[i].FullName != other.ReferencedNames[i].FullName)
                            {
                                ret = false;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }        

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>The object hash code</returns>
        public override int GetHashCode()
        {
            int ret = Script.GetHashCode() ^ Engine.GetHashCode() ^ Uuid.GetHashCode() ^ EnableDebug.GetHashCode();

            if (ReferencedNames != null)
            {
                foreach (AssemblyName asm in ReferencedNames)
                {
                    ret ^= asm.FullName.GetHashCode();
                }
            }

            return ret;
        }
    }
}
