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
using System.IO;
using IronPython.Hosting;
using CANAPE.Utils;
using Microsoft.Scripting.Hosting;
using IronPython.Runtime;

namespace CANAPE.Scripting.Dynamic
{
    [ScriptEngine("python", "Python")]
    internal class PythonScriptEngine : DynamicScriptEngine
    {
        /*  PythonCompilerOptions pco = (PythonCompilerOptions) engine.GetCompilerOptions(mainScope);

   pco.ModuleName = "__main__";
   pco.Module |= ModuleOptions.Initialize;*/

        private static IDictionary<string, object> MakeSetup(bool enableDebug)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            ret.Add("Debug", enableDebug);                       

            return ret;
        }

        public PythonScriptEngine(bool enableDebug)
            : base(Python.CreateEngine(MakeSetup(enableDebug)))
        {
            ICollection<string> paths = _engine.GetSearchPaths();

            paths.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PythonLib"));

            string configDir = GeneralUtils.GetConfigDirectory();
            if (configDir != null)
            {
                paths.Add(Path.Combine(configDir, "PythonLib"));
            }

            _engine.SetSearchPaths(paths);
        }

        public override string[] GetTypes(params Type[] types)
        {
            List<string> ret = new List<string>();

            if (types.Length == 0)
            {
                types = ScriptUtils.DefaultTypes;
            }
            
            foreach (KeyValuePair<string, dynamic> pair in GetGlobals())
            {
                if (pair.Value is IronPython.Runtime.Types.PythonType)
                {
                    IronPython.Runtime.Types.PythonType t = (IronPython.Runtime.Types.PythonType)pair.Value;

                    Type clrType = t.__clrtype__();

                    string name = IronPython.Runtime.Types.PythonType.Get__name__(t);

                    // We check if the name is the same as the clr name, if so it is just a hoisted type in global namespace
                    if (name != clrType.Name)
                    {
                        foreach (Type assignType in types)
                        {
                            if (assignType.IsAssignableFrom(clrType))
                            {
                                ret.Add(pair.Key);
                                break;
                            }
                        }
                    }
                }
            }           

            return ret.ToArray();
        }

        public override dynamic GetInstance(string classname, params object[] args)
        {
            dynamic ret = null;
            Dictionary<string, dynamic> globals = GetGlobals();

            if ((classname != null) && globals.ContainsKey(classname))
            {
                if ((args == null) || (args.Length == 0))
                {
                    ret = globals[classname]();
                }
                else
                {
                    ret = _engine.Operations.CreateInstance(globals[classname], args);                    
                }
            }

            return ret;
        }

        public override dynamic Invoke(string classname, string methodname, params object[] args)
        {
            dynamic ret = null; 

            // Invoking a function, we need to go through the global code
            if (!String.IsNullOrWhiteSpace(methodname))
            {
                Dictionary<string, dynamic> globals = GetGlobals();

                if (!String.IsNullOrWhiteSpace(classname))
                {
                    object targetObject = globals[classname];
                    ret = _engine.Operations.InvokeMember(targetObject, methodname, args);
                }
                else if (globals.ContainsKey(methodname))
                {
                    ret = _engine.Operations.Invoke(globals[methodname], args);
                }
                else
                {
                    throw new MissingMethodException("<global>", methodname);
                }
            }
            else
            {
                // Create a new scope
                ScriptScope scope = _engine.CreateScope();

                scope.SetVariable("args", args);

                ret = _code.Execute(scope);                
            }

            return ret;
        }
    }
}
