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
using CANAPE.Net.Clients;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Base class for proxy client
    /// </summary>
    [Serializable]
    public abstract class BaseProxyClientFactory : IProxyClientFactory
    {
        /// <summary>
        /// Event for configuration changing
        /// </summary>
        [field: NonSerialized]
        public event EventHandler ConfigChanged;

        /// <summary>
        /// Method called when config changes
        /// </summary>
        protected virtual void OnConfigChanged()
        {
            if (ConfigChanged != null)
            {
                ConfigChanged.Invoke(this, new EventArgs());
            }
        }        

        /// <summary>
        /// Method to create the proxy client
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <returns>The new proxy client</returns>
        public abstract ProxyClient Create(Logger logger);

        /// <summary>
        /// IClonable method
        /// </summary>
        /// <returns>The cloned factory</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
