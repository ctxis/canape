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
using CANAPE.Utils;
using System.Linq;

namespace CANAPE.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, Obsolete("Use DynamicArrayDataKey2 instead")]
    public class DynamicArrayDataKey : DataKey
    {
        [NonSerialized]
        Logger _logger;

        [NonSerialized]
        IDataParser _convert;

        string _script;
        string _engine;
        string _classname;
        bool _enableDebug;
        object _state;
        IEnumerable<Assembly> _referencedAssemblies;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected Logger GetLogger()
        {
            if (_logger == null)
            {
                _logger = Logger.GetSystemLogger();
            }

            return _logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Indicates if the end of stream has been reached</returns>
        public override void FromArray(byte[] data)
        {
            IDataArrayParser convert = (IDataArrayParser)CreateConverter();

            try
            {
                convert.FromArray(data, this, GetLogger());
            }
            catch (Exception ex)
            {
                GetLogger().LogException(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override byte[] ToArray()
        {
            IDataArrayParser convert = (IDataArrayParser)CreateConverter();

            byte[] ret = new byte[0];

            ret = convert.ToArray(this, GetLogger());

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stm"></param>
        public override void ToWriter(DataWriter stm)
        {
            stm.Write(ToArray());   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="engine"></param>
        /// <param name="script"></param>
        /// <param name="enableDebug"></param>
        /// <param name="classname"></param>
        /// <param name="referencedAssemblies"></param>
        /// <param name="logger"></param>
        /// <param name="state"></param>
        public DynamicArrayDataKey(string name, string engine, string script, bool enableDebug, 
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

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            if (_convert != null)
            {
                IPersistDynamicNode persist = _convert as IPersistDynamicNode;
                if (persist != null)
                {
                    _state = persist.GetState(GetLogger());
                }
            }

            _script = String.Intern(_script);
            _engine = String.Intern(_engine);
            _classname = String.Intern(_classname);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IDataParser CreateConverter()
        {
            if (_convert == null)
            {
                ScriptContainer container = new ScriptContainer(_engine, Guid.Empty, _script, _enableDebug, _referencedAssemblies.Select(a => a.GetName()));
                _convert = (IDataParser)ScriptUtils.GetInstance(container, _classname);

                if (_state != null)
                {
                    IPersistDynamicNode persist = _convert as IPersistDynamicNode;
                    if (persist != null)
                    {
                        persist.SetState(_state, GetLogger());
                    }
                }
            }

            return _convert;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                IDataParser convert = CreateConverter();

                return convert.ToDisplayString(this, GetLogger());
            }
            catch (Exception)
            {
                return "Error getting display string";
            }
        }
    }
}
