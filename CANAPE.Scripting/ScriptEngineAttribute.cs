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

namespace CANAPE.Scripting
{
    /// <summary>
    /// Attribute for a script engine
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public sealed class ScriptEngineAttribute : CANAPEExtensionAttribute
    {
        private string _engineName;
        private string _textName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="engineName">The engine name</param>
        /// <param name="textName">The engine description</param>
        public ScriptEngineAttribute(string engineName, string textName)
        {
            _engineName = engineName;
            _textName = textName;
        }

        /// <summary>
        /// Get the name of the engine
        /// </summary>
        public string EngineName
        {
            get { return _engineName; }
        }

        /// <summary>
        /// Get the description of the engine
        /// </summary>
        public string TextName
        {
            get { return _textName; }
        }

        /// <summary>
        /// Register the type with the manager
        /// </summary>
        /// <param name="t">The type to register</param>
        public override void RegisterType(Type t)
        {
            ScriptEngineFactory.AddScriptEngineFromType(this, t);
        }

        /// <summary>
        /// Unregister the type with the manager
        /// </summary>
        /// <param name="t">The type to unregister</param>
        public override void UnregisterType(Type t)
        {
            ScriptEngineFactory.RemoveScriptEngineFromType(this, t);
        }
    }
}
