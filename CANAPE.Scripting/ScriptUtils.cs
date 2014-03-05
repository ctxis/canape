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
using System.Xml;
using CANAPE.Utils;
using CANAPE.Extension;
using CANAPE.Nodes;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Static utility class to handle scripting
    /// </summary>
    public static class ScriptUtils
    {
        private static object _lockObject = new object();
        private static Dictionary<string, IScriptEngine> _scriptObjects = new Dictionary<string, IScriptEngine>();
        private static List<string> _tempFiles = new List<string>();

        /// <summary>
        /// Reinitialize the script engine
        /// </summary>
        public static void ReInitialize()
        {
            lock(_lockObject)
            {
                _scriptObjects.Clear();
                DeleteTempFiles();
            }
        }

        /// <summary>
        /// Delete any temporary files
        /// </summary>
        public static void DeleteTempFiles()
        {
            foreach (string file in _tempFiles)
            {
                if (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                    }
                }
            }
            _tempFiles.Clear();
        }

        /// <summary>
        /// Add a temp file for later deletion
        /// </summary>
        /// <param name="tempFile">The temporary file</param>
        public static void AddTempFile(string tempFile)
        {
            _tempFiles.Add(tempFile);
        }

        /// <summary>
        /// Parse the supplied code and return errors. The results are not cached
        /// </summary>
        /// <param name="container">The script container</param>
        /// <returns>An array of script errors, if length of 0 means no errors</returns>
        public static ScriptError[] Parse(ScriptContainer container)
        {
            IScriptEngine se = GetScriptEngine(container.Engine, container.EnableDebug);

            if(se != null)
            {
                return se.Parse(container);
            }
            else
            {
                return new ScriptError[] { new ScriptError(String.Format("Unknown script engine {0}", container.Engine), "FatalError", 0, 0) };
            }
        }

        /// <summary>
        /// Get a script engine
        /// </summary>
        /// <param name="engine">The engine name</param>
        /// <param name="enableDebug">Whether to enable debug</param>
        /// <returns>The script engine instance</returns>
        private static IScriptEngine GetScriptEngine(string engine, bool enableDebug)
        {            
            return ScriptEngineFactory.GetScriptEngine(engine, enableDebug);
        }

        /// <summary>
        /// Generate code from a compile unit and an engine
        /// </summary>
        /// <param name="engine">The script engine to use</param>
        /// <param name="unit">The compile unit</param>
        /// <returns>The code or an empty string</returns>
        public static string GenerateCode(string engine, CodeCompileUnit unit)
        {
            IScriptEngine script = GetScriptEngine(engine, false);

            if (script != null)
            {
                return script.GenerateCode(unit);
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Get an instance of a specific type
        /// </summary>
        /// <typeparam name="T">The type the object derives from to instantiate</typeparam>
        /// <returns>The instance of T, null if it doesn't exist</returns>
        public static T GetInstance<T>(ScriptContainer container) where T : class
        {
            string[] types = GetTypes(container, typeof(T));

            if (types.Length > 0)
            {
                return (T)GetInstance(container, types[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get type names in script
        /// </summary>
        /// <param name="container">The script container</param>
        /// <param name="types">Optional list of types to filter on</param>
        /// <returns>The list of type names</returns>
        public static string[] GetTypes(ScriptContainer container, params Type[] types)
        {
            IScriptEngine scriptEngine = null;

            lock (_lockObject)
            {
                try
                {
                    if (!_scriptObjects.ContainsKey(container.Script))
                    {
                        scriptEngine = GetScriptEngine(container.Engine, container.EnableDebug);
                        if (scriptEngine != null)
                        {
                            if (scriptEngine.Parse(container).Length == 0)
                            {
                                _scriptObjects.Add(container.Script, scriptEngine);
                            }
                        }
                    }
                    else
                    {
                        scriptEngine = _scriptObjects[container.Script];
                    }
                }
                catch (Exception ex)
                {
                    Logger.GetSystemLogger().LogException(ex);
                }
            }

            if (scriptEngine != null)
            {
                return scriptEngine.GetTypes(types);
            }
            else
            {
                return new string[0];
            }
        }

        private static IScriptEngine GetEngineForScript(ScriptContainer container)
        {
            IScriptEngine scriptEngine = null;

            lock (_lockObject)
            {
                try
                {
                    if (!_scriptObjects.ContainsKey(container.Script))
                    {
                        scriptEngine = GetScriptEngine(container.Engine, container.EnableDebug);
                        if (scriptEngine != null)
                        {
                            if (scriptEngine.Parse(container).Length == 0)
                            {
                                _scriptObjects.Add(container.Script, scriptEngine);
                            }
                        }
                    }
                    else
                    {
                        scriptEngine = _scriptObjects[container.Script];
                    }
                }
                catch (Exception ex)
                {
                    Logger.GetSystemLogger().LogException(ex);
                }
            }

            return scriptEngine;
        }

        /// <summary>
        /// Invoke a static method on the script
        /// </summary>
        /// <param name="container">The script container</param>
        /// <param name="classname">The name of the class to invoke on</param>
        /// <param name="methodname">The method name</param>
        /// <param name="args">Any optional arguments</param>
        /// <returns>The return value if it is supported</returns>
        public static object Invoke(ScriptContainer container, string classname, string methodname, params object[] args)
        {
            IScriptEngine scriptEngine = GetEngineForScript(container);

            if (scriptEngine != null)
            {
                return scriptEngine.Invoke(classname, methodname, args);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get an instance of a script generated object, this caches class objects for performance
        /// </summary>
        /// <param name="container">The script container</param>
        /// <param name="classname">The name of the class to get an instance of</param>
        /// <returns>The object instance</returns>
        public static object GetInstance(ScriptContainer container, string classname)
        {
            return GetInstance(container, classname, null);
        }

        /// <summary>
        /// Get an instance of a script generated object, this caches class objects for performance
        /// </summary>
        /// <param name="container">The script container</param>
        /// <param name="classname">The name of the class to get an instance of</param>
        /// <param name="args">Any parmeters to the constructor</param>
        /// <returns>The object instance</returns>
        public static object GetInstance(ScriptContainer container, string classname, params object[] args)
        {
            IScriptEngine scriptEngine = GetEngineForScript(container);

            if (scriptEngine != null)
            {
                return scriptEngine.GetInstance(classname, args);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get an array of script templates for a specified engine
        /// </summary>
        /// <param name="engine">The engine</param>
        /// <returns>An array of available templates</returns>
        public static IEnumerable<CANAPETemplate> GetTemplates(string engine)
        {            
            Type engineType = ScriptEngineFactory.GetTypeForScriptEngine(engine);

            if (engineType != null)
            {
                return CANAPEExtensionManager.GetTemplates(engineType, engine);
            }
            else
            {
                return new CANAPETemplate[0];
            }
        }        

        /// <summary>
        /// Default scriptable types
        /// </summary>
        public static Type[] DefaultTypes
        {
            get
            {
                return new Type[] { 
                    typeof(BasePipelineNode),
                    typeof(IDataStreamParser),
                    typeof(IDataArrayParser),
                    typeof(IDataEndpoint),
                    typeof(IDataGenerator),
                };
            }
        }
    }
}
