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
using System.IO;
using System.Reflection;
using Microsoft.Scripting.Hosting;

namespace CANAPE.Scripting.Dynamic
{
    /// <summary>
    /// Base script engine for dynamic languages (e.g. Ruby or Python)
    /// </summary>
    public abstract class DynamicScriptEngine : IScriptEngine
    {
        /// <summary>
        /// The dynamic script engine
        /// </summary>
        protected ScriptEngine _engine;

        /// <summary>
        /// Compiled code
        /// </summary>
        protected CompiledCode _code;

        /// <summary>
        /// The source of the script
        /// </summary>
        protected ScriptSource _source;

        /// <summary>
        /// The script scope
        /// </summary>
        private ScriptScope _scope;

        /// <summary>
        /// Dictionary of global variables
        /// </summary>
        private Dictionary<string, dynamic> _globals;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="engine">The script engine to create</param>        
        protected DynamicScriptEngine(ScriptEngine engine)
        {
            _engine = engine;            
            _engine.Runtime.LoadAssembly(Assembly.GetExecutingAssembly());
            _engine.Runtime.IO.RedirectToConsole();           
        }

        #region IScriptEngine Members

        /// <summary>
        /// Method to get the globals, will execute the code if required
        /// </summary>
        /// <returns>A dictionary of globals</returns>
        protected Dictionary<string, dynamic> GetGlobals()
        {
            if (_globals == null)            
            {
                _globals = new Dictionary<string, dynamic>();

                try
                {
                    if ((_code != null) && (_scope != null))
                    {
                        _code.Execute(_scope);

                        foreach (var pair in _scope.GetItems())
                        {
                            _globals.Add(pair.Key, pair.Value);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return _globals;
        }

        /// <summary>
        /// Parse a script
        /// </summary>
        /// <param name="container">Script container</param>
        /// <returns>An array of errors, if no errors then empty</returns>
        public ScriptError[] Parse(ScriptContainer container)
        {
            ScriptErrorListener listener = new ScriptErrorListener();

            try
            {
                foreach (AssemblyName name in container.ReferencedNames)
                {
                    _engine.Runtime.LoadAssembly(Assembly.Load(name));
                }

                if (container.EnableDebug)
                {
                    string newFile = Path.GetTempFileName();

                    ScriptUtils.AddTempFile(newFile);
                    File.WriteAllText(newFile, container.Script);
                    _source = _engine.CreateScriptSourceFromFile(newFile);
                }
                else
                {
                    _source = _engine.CreateScriptSourceFromString(container.Script);
                }

                _code = _source.Compile(listener);

                if (listener.Errors.Count == 0)
                {
                    // Just create the global scope, don't execute it yet
                    _scope = _engine.CreateScope();
                    _scope.SetVariable("CANAPEScriptContainer", new ScriptContainer(container));                                                       
                }
            }
            catch (Exception ex)
            {
                listener.Errors.Add(new ScriptError(ex.Message, "FatalError", 0, 0));
            }

            return listener.Errors.ToArray();
        }

        /// <summary>
        /// Get an instance of a class from the engine
        /// </summary>
        /// <param name="classname">The class name</param>
        /// <returns>An instance of the class, null if not found</returns>
        public dynamic GetInstance(string classname)
        {
            return GetInstance(classname, null);
        }

        /// <summary>
        /// Generate code based on a code namespace (not implemented)
        /// </summary>
        /// <param name="unit">The code compile unit</param>
        /// <returns>Always String.Empty</returns>
        public virtual string GenerateCode(CodeCompileUnit unit)
        {
            return String.Empty;
        }

        /// <summary>
        /// Get list of type names that this script contained
        /// </summary>
        /// <param name="types">List of types to find, if empty will return all types</param>
        /// <returns>The array of type names</returns>
        public abstract string[] GetTypes(params Type[] types);

        /// <summary>
        /// Get an instance of a class
        /// </summary>
        /// <param name="classname">The classname</param>
        /// <param name="args">Any arguments for the constructor</param>
        /// <returns>The instance</returns>
        public abstract dynamic GetInstance(string classname, params object[] args);        

        /// <summary>
        /// Invoke a static method on a class
        /// </summary>
        /// <param name="classname">The classname</param>
        /// <param name="methodname">The name of the method</param>
        /// <param name="args">The arguments to pass to the method</param>
        /// <returns>The return value from the method</returns>
        public abstract dynamic Invoke(string classname, string methodname, params object[] args);        

        #endregion
    }
}
