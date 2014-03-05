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
using System.Linq;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Scripting
{
    /// <summary>
    /// A datakey which is implemented by a dynamic stream object
    /// </summary>
    [Serializable, Obsolete("Use DynamicStreamDataKey2 instead")]
    public class DynamicStreamDataKey : DataKey
    {
        [NonSerialized]
        Logger _logger;

        [NonSerialized]
        IDataStreamParser _convert;

        string _script;
        string _engine;
        string _classname;
        bool _enableDebug;
        IEnumerable<Assembly> _referencedAssemblies;
        object _state;

        private Logger GetLogger()
        {
            if (_logger == null)
            {
                _logger = Logger.GetSystemLogger();
            }

            return _logger;
        }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            IPersistDynamicNode persist = _convert as IPersistDynamicNode;

            if (persist != null)
            {
                _state = persist.GetState(GetLogger());
            }

            // Intern strings so they all end up with the same reference this massively improves data usage
            // really should be doing this at a container level, but oh well.
            _script = String.Intern(_script);
            _engine = String.Intern(_engine);
            _classname = String.Intern(_classname);
        }

        /// <summary>
        /// Create a key from a DataReader
        /// </summary>
        /// <param name="stm">The data reader</param>
        /// <remarks>This method could throw almost any exception</remarks>
        /// <exception cref="System.IO.EndOfStreamException">Throw when end of stream reached</exception>
        public override void FromReader(DataReader stm)
        {
            IDataStreamParser convert = CreateConverter();

            convert.FromReader(stm, this, GetLogger());
        }

        /// <summary>
        /// Write the key to a DataWriter
        /// </summary>
        /// <remarks>This method could throw almost any exception</remarks>
        /// <param name="stm">The DataWriter object</param>
        public override void ToWriter(DataWriter stm)
        {
            IDataStreamParser convert = CreateConverter();

            convert.ToWriter(stm, this, GetLogger());
        } 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the node</param>
        /// <param name="engine">The script engine to use</param>
        /// <param name="script">The script text</param>
        /// <param name="classname">The classname to instantiate</param>
        /// <param name="enableDebug">True to enable debug mode</param>
        /// <param name="referencedAssemblies">List of referenced assemblies</param>
        /// <param name="logger">Logger object</param>
        /// <param name="state">Object state</param>
        public DynamicStreamDataKey(string name, string engine, string script, bool enableDebug,
            string classname, IEnumerable<Assembly> referencedAssemblies, 
            Logger logger, object state)
            : base(name)
        {
            _engine = engine;
            _script = script;
            _classname = classname;
            _enableDebug = enableDebug;
            _logger = logger;            
            _referencedAssemblies = referencedAssemblies;
            _state = state;
        }
        
        private IDataStreamParser CreateConverter()
        {
            if (_convert == null)
            {
                ScriptContainer container = new ScriptContainer(_engine, Guid.Empty, _script, _enableDebug, _referencedAssemblies.Select(a => a.GetName()));
                _convert = (IDataStreamParser)ScriptUtils.GetInstance(container, _classname);
                if (_state != null)
                {
                    IPersistDynamicNode persist = _convert as IPersistDynamicNode;
                    if(persist != null)
                    {
                        persist.SetState(_state, GetLogger());
                    }
                }
            }

            return _convert;
        }

        /// <summary>
        /// To string override
        /// </summary>
        /// <returns>The value of the converter's ToDisplayString method</returns>
        public override string ToString()
        {
            try
            {
                IDataParser convert = CreateConverter();

                return convert.ToDisplayString(this, GetLogger());
            }
            catch (Exception ex)
            {
                GetLogger().LogException(ex);
                return "Error getting display string";
            }
        }
    }
}
