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
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using CANAPE.Nodes;

namespace CANAPE.Scripting
{
    /// <summary>
    /// A script engine to contain an assembly reference
    /// </summary>    
    public class AssemblyScriptEngine : IScriptEngine
    {
        /// <summary>
        /// The associated assembly for the script engine
        /// </summary>
        protected Assembly _assembly;

        /// <summary>
        /// Load an assembly
        /// </summary>
        /// <param name="container">The script container, the script value indicates the assembly name</param>
        /// <returns></returns>
        public virtual ScriptError[] Parse(ScriptContainer container)
        {
            List<ScriptError> errors = new List<ScriptError>();

            try
            {
                _assembly = Assembly.Load(container.Script);
            }
            catch (Exception ex)
            {
                errors.Add(new ScriptError(ex.Message, "Error", 0, 0));
            }

            return errors.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        public dynamic GetInstance(string classname)
        {
            return GetInstance(classname, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public virtual string GenerateCode(CodeCompileUnit unit)
        {
            return String.Empty;
        }

        /// <summary>
        /// Get the types in this script engine
        /// </summary>
        /// <param name="types">List of types to find, if empty will return default types</param>
        /// <remarks>Note that the list of types could be ignored by the script engine</remarks>
        /// <returns></returns>
        public string[] GetTypes(params Type[] types)
        {
            List<string> ret = new List<string>();

            if (types.Length == 0)
            {
                types = ScriptUtils.DefaultTypes;
            }

            if (_assembly != null)
            {
                Type[] asmTypes = _assembly.GetTypes();

                foreach (Type t in asmTypes)
                {
                    if (t.IsClass && !t.IsAbstract)
                    {
                        foreach(Type assignType in types)
                        {
                            if (assignType.IsAssignableFrom(t))
                            {
                                ret.Add(t.FullName);
                                break;
                            }
                        }
                    }                    
                }
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Get an instance from this script engine with arguments
        /// </summary>
        /// <param name="classname">The class name to create</param>
        /// <param name="args">The constructor arguments</param>
        /// <returns>An instance of the object, null if failed</returns>
        public dynamic GetInstance(string classname, params object[] args)
        {
            dynamic ret = null;

            if ((classname != null) && (_assembly != null))
            {
                Type t = _assembly.GetType(classname);

                if (t == null)
                {
                    // Lets try not fully qualified names
                    foreach (Type type in _assembly.GetTypes())
                    {
                        if (type.Name == classname)
                        {
                            t = type;
                            break;
                        }
                    }
                }

                if (t != null)
                {
                    ret = Activator.CreateInstance(t, args);
                }
            }

            return ret;
        }

        private MethodInfo FindMethod(Type type, string name, ref object[] args)
        {
            MethodBase[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(
                    m => m.Name.Equals(name)).ToArray();
            
            if(methods.Length > 0)
            {
                object state = null;
                return (MethodInfo)Type.DefaultBinder.BindToMethod(BindingFlags.Static | BindingFlags.Public, methods, ref args, null, null, null, out state);
            }

            return null;
        }

        /// <summary>
        /// Invoke a static method on a class in this engine
        /// </summary>
        /// <param name="classname">The classname</param>
        /// <param name="methodname">The method name to invoke</param>
        /// <param name="args">The arguments to pass to the method</param>
        /// <returns>The return value from the invoke, or null if no return</returns>
        public dynamic Invoke(string classname, string methodname, params object[] args)
        {
            dynamic ret = null;

            if (_assembly != null)
            {
                if (String.IsNullOrWhiteSpace(methodname))
                {
                    Type runType = null;                    

                    if (String.IsNullOrWhiteSpace(classname))
                    {
                        foreach (Type t in _assembly.GetTypes())
                        {
                            if (t.IsClass && !t.IsAbstract && typeof(IRunScript).IsAssignableFrom(t))
                            {
                                runType = t;
                                break;
                            }
                        }
                    }
                    else
                    {
                        runType = _assembly.GetType(classname);
                    }

                    if (runType != null)
                    {
                        IRunScript run = (IRunScript)Activator.CreateInstance(runType);

                        ret = run.Run(args);
                    }
                    else
                    {
                        throw new ArgumentException(Properties.Resources.AssemblyScriptEngine_MissingRunType);
                    }
                }
                else 
                {
                    MethodInfo method = null;

                    if (!String.IsNullOrWhiteSpace(classname))
                    {
                        Type t = _assembly.GetType(classname);

                        if (t == null)
                        {
                            // Lets try not fully qualified names
                            foreach (Type type in _assembly.GetTypes())
                            {
                                if (type.Name == classname)
                                {
                                    t = type;
                                    break;
                                }
                            }
                        }

                        if(t != null)
                        {
                            method = FindMethod(t, methodname, ref args);
                        }
                    }
                    else
                    {
                        foreach(Type t in _assembly.GetTypes())
                        {
                            method = FindMethod(t, methodname, ref args);
                            if(method != null)
                            {
                                break;
                            }
                        }
                    }

                    if (method != null)
                    {
                        ret = method.Invoke(null, args);
                    }
                    else
                    {
                        throw new MissingMethodException(classname ?? "<global>", methodname);
                    }
                }                
            }

            return ret;
        }
    }
}
