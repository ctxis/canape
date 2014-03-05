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

using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Base class for a pipeline node with persistance
    /// </summary>
    /// <typeparam name="T">The configuration types</typeparam>
    public abstract class BasePipelineNodeWithPersist<T> 
        : BasePipelineNode, IPersistNode where T : class, new()
    {
        /// <summary>
        /// Protected constructor
        /// </summary>
        protected BasePipelineNodeWithPersist()
        {
            _config = new T();
        }

        /// <summary>
        /// The current config
        /// </summary>
        private T _config;

        /// <summary>
        /// The current config
        /// </summary>
        public T Config
        {
            get { return _config; }
        }

        /// <summary>
        /// Overridable method to validate config, should throw an ArgumentException if invalid
        /// </summary>
        /// <param name="config">The config</param>
        protected virtual void ValidateConfig(T config)
        {
            // Do nothing
        }

        #region IPersistDynamicNode Members

        /// <summary>
        /// Get the current state
        /// </summary>
        /// <param name="logger"></param>
        /// <returns>The state object</returns>
        public object GetState(Logger logger)
        {
            return _config;
        }

        /// <summary>
        /// Set the current state
        /// </summary>
        /// <param name="state">The state object</param>
        /// <param name="logger"></param>
        public void SetState(object state, Logger logger)
        {
            T config = state as T;

            if (config == null)
            {
                logger.LogError(CANAPE.Scripting.Properties.Resources.BasePipelineNodeWithPersist_InvalidConfig, GetType().Name);
            }
            else
            {
                ValidateConfig(config);
                _config = config;
            }
        }

        #endregion
    }
}
