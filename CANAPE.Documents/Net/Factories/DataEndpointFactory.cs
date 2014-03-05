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
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Base class for a endpoint factory
    /// </summary>
    [Serializable]
    public abstract class DataEndpointFactory
    {
        /// <summary>
        /// The name of the endpoint
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The associated configuration
        /// </summary>
        public object Config { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the endpoint</param>
        /// <param name="config">The associated configuration</param>
        protected DataEndpointFactory(string name, object config)
        {
            Name = name;
            Config = config;
        }

        /// <summary>
        /// Method to create the data endpoint
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="globalMeta">Global meta dictionary</param>
        /// <param name="meta">Meta dictionary</param>
        /// <returns>The data endpoint</returns>
        public abstract IDataEndpoint Create(Logger logger, MetaDictionary meta, MetaDictionary globalMeta);
    }
}
