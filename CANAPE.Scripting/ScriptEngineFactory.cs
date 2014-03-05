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
using System.Security;
using CANAPE.Scripting.DotNet;
using CANAPE.Scripting.Dynamic;

namespace CANAPE.Scripting
{    
    /// <summary>
    /// Factory to create a script engine
    /// </summary>
    [SecuritySafeCritical]
    public static class ScriptEngineFactory
    {
        /// <summary>
        /// 
        /// </summary>
        class ScriptEngineInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string textDescription;
            /// <summary>
            /// 
            /// </summary>
            public Type type;
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly object _lock = new object();
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, ScriptEngineInfo> _engines = new Dictionary<string,ScriptEngineInfo>();


        /// <summary>
        /// Load the current list of engines (for now just statically implemented)
        /// </summary>
        static ScriptEngineFactory()
        {
            lock (_lock)
            {
                AddScriptEngineFromType(typeof(CSharpScriptEngine));
                AddScriptEngineFromType(typeof(PythonScriptEngine));
            }
        }

        /// <summary>
        /// Add a script engine from a type, uses ScriptEngineAttribute to populate informatin
        /// </summary>
        /// <param name="engineType">The engine type</param>
        internal static void AddScriptEngineFromType(Type engineType)
        {
            if (engineType.IsClass)
            {
                object[] attrs = engineType.GetCustomAttributes(typeof(ScriptEngineAttribute), false);
                if (attrs.Length == 1)
                {
                    AddScriptEngineFromType((ScriptEngineAttribute)attrs[0], engineType);
                }
            }
        }

        /// <summary>
        /// Remove the script engine by type
        /// </summary>
        /// <param name="engineType">The engine type</param>
        /// <param name="attr">The scriptengine attribute</param>
        internal static void RemoveScriptEngineFromType(ScriptEngineAttribute attr, Type engineType)
        {
            _engines.Remove(attr.TextName.ToLowerInvariant());
        }

        /// <summary>
        /// Add a script engine from a type, uses ScriptEngineAttribute to populate informatin
        /// </summary>
        /// <param name="engineType">The engine type</param>
        /// <param name="attr">The scriptengine attribute</param>
        internal static void AddScriptEngineFromType(ScriptEngineAttribute attr, Type engineType)
        {
            ScriptEngineInfo engine = new ScriptEngineInfo();
            engine.textDescription = attr.TextName;
            engine.type = engineType;

            _engines[attr.EngineName.ToLowerInvariant()] = engine;
        }

        /// <summary>
        /// Get engine names 
        /// </summary>
        public static string[] Engines
        {
            get
            {
                string[] ret;

                lock (_lock)
                {
                    ret = _engines.Keys.ToArray<string>();
                }

                return ret;
            }
        }

        /// <summary>
        /// Get description for engine name
        /// </summary>
        /// <param name="engine">The engine name</param>
        /// <returns>The description</returns>
        public static string GetDescriptionForEngine(string engine)
        {
            string ret = "";

            lock (_lock)
            {
                if (_engines.ContainsKey(engine.ToLowerInvariant()))
                {
                    ret = _engines[engine.ToLowerInvariant()].textDescription;
                }
            }

            return ret;
        }

        /// <summary>
        /// Get the type for the implementation of the script engine script engine
        /// </summary>
        /// <param name="engine">The engine name</param>
        /// <returns>The type of script engine, null if not found</returns>
        public static Type GetTypeForScriptEngine(string engine)
        {
            string engineName = engine.ToLowerInvariant();

            if (engineName == "assembly")
            {
                // Hard code assembly script engine
                return typeof(AssemblyScriptEngine);
            }
            else
            {
                if (_engines.ContainsKey(engineName))
                {
                    return _engines[engineName].type;
                }
            }
            return null;
        }

        /// <summary>
        /// Get a script engine instance
        /// </summary>
        /// <param name="engine">The engine name</param>
        /// <param name="enableDebug">Whether to enable debug of the engine</param>
        /// <returns>The script engine</returns>
        public static IScriptEngine GetScriptEngine(string engine, bool enableDebug)
        {
            IScriptEngine se = null;
            string engineName = engine.ToLowerInvariant();

            if (engineName == "assembly")
            {
                // Hard code assembly script engine
                se = new AssemblyScriptEngine();
            }
            else
            {
                if (_engines.ContainsKey(engineName))
                {
                    se = (IScriptEngine)Activator.CreateInstance(_engines[engineName].type, new object[] { enableDebug });
                }
            }
            return se;
        }
    }

}
