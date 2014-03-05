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
using System.Runtime.Serialization;
using CANAPE.DataFrames;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Dynamic data key base class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class DynamicDataKey<T> : DataKey, IScriptedNode where T : IDataParser
    {
        [NonSerialized]
        Logger _logger;

        [NonSerialized]
        T _convert;

        [NonSerialized]
        bool _errorInConvert;

        DynamicScriptContainer _container;
        object _state;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the key</param>        
        /// <param name="container">The script container</param>
        /// <param name="logger">The logger</param>
        /// <param name="state">The object state</param>
        protected DynamicDataKey(string name, DynamicScriptContainer container, Logger logger, object state) : base(name)
        {
            _container = container;
            _logger = logger;

            if (state != null)
            {
                if (!state.GetType().IsSerializable)
                {
                    throw new ArgumentException(CANAPE.Scripting.Properties.Resources.DynamicDataKey_StateNotSerializable, "state");
                }
                _state = state;
            }
        }

        /// <summary>
        /// Get the current logger
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

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            IPersistNode persist = _convert as IPersistNode;

            if (persist != null)
            {
                _state = persist.GetState(GetLogger());
            }

            _container = DynamicScriptContainer.GetCachedContainer(_container);
        }

        /// <summary>
        /// Create the converter
        /// </summary>
        /// <returns></returns>
        protected T CreateConverter()
        {
            if ((_convert == null) && !_errorInConvert)
            {
                _convert = (T)_container.GetInstance();
                if (_convert == null)
                {
                    _errorInConvert = true;
                }
                else
                {
                    if (_state != null)
                    {
                        IPersistNode persist = _convert as IPersistNode;
                        if (persist != null)
                        {
                            persist.SetState(_state, GetLogger());
                        }
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

                if (convert == null)
                {
                    return String.Format(Properties.Resources.DynamicDataKey_ErrorGettingDisplayString, Properties.Resources.Scripting_FailedToCreateInstance);
                }

                return convert.ToDisplayString(this, GetLogger());
            }
            catch (Exception ex)
            {
                GetLogger().LogException(ex);
                return String.Format(CANAPE.Scripting.Properties.Resources.DynamicDataKey_ErrorGettingDisplayString, ex.Message);
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _container = DynamicScriptContainer.GetCachedContainer(_container);
        }

        /// <summary>
        /// Return the node's script container
        /// </summary>
        public DynamicScriptContainer Script
        {
            get { return _container; }
        }
    }
}
