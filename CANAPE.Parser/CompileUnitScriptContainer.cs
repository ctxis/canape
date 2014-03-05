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
using System.Runtime.Serialization;
using CANAPE.Scripting;

namespace CANAPE.Parser
{
    /// <summary>
    /// A script container which generates the script from a code compile unit
    /// </summary>
    [Serializable]
    public class CompileUnitScriptContainer : ScriptContainer
    {
        [NonSerialized]
        private object _lockObject = new object();

        [NonSerialized]
        private string _script;

        private CodeCompileUnit _unit;

        public CompileUnitScriptContainer(string engine, Guid uuid, CodeCompileUnit unit, bool enableDebug)
            : base(engine, uuid, null, enableDebug)
        {
            _unit = unit;
            ReferencedNames.Add(typeof(CompileUnitScriptContainer).Assembly.GetName());
        }

        [OnDeserialized]
        private void OnDeserializedMethod(StreamingContext ctx)
        {
            _lockObject = new object();
        }

        /// <summary>
        /// The compile unit
        /// </summary>
        public CodeCompileUnit CompileUnit 
        {
            get { return _unit; }
            set
            {
                lock (_lockObject)
                {
                    _unit = value;
                    _script = null;
                }
            }
        }

        /// <summary>
        /// The script, generates it from the compile unit
        /// </summary>
        public override string Script
        {
            get
            {
                string ret = String.Empty;

                lock (_lockObject)
                {
                    if (_unit != null)
                    {
                        if (_script == null)
                        {
                            _script = ScriptUtils.GenerateCode(Engine, CompileUnit);
                        }

                        ret = _script ?? String.Empty;
                    }
                }

                return ret;
            }
            set
            {
                // If we set with a string then convert to a code snippet
                _script = null;
                CompileUnit = new CodeSnippetCompileUnit(value);
            }
        }
    }
}
