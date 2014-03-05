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
using System.Reflection;
using CANAPE.Net;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Data server factory based on a library implementation
    /// </summary>
    [Serializable]
    public class LibraryDataEndpointFactory : DataEndpointFactory
    {
        private Type _type;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The type of the endpoint</param>
        /// <param name="configType">The type of the config</param>
        /// <param name="name">The name of the endpoint</param>
        public LibraryDataEndpointFactory(Type type, Type configType, string name) 
            : base(name, Activator.CreateInstance(configType))
        {
            _type = type;            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">The type of the endpoint</param>
        /// <param name="name">The name of the endpoint</param>
        public LibraryDataEndpointFactory(Type type, string name)
            : base(name, null)
        {
            _type = type;
        }

        /// <summary>
        /// Method to create the data endpoint
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="globalMeta">Global meta dictionary</param>
        /// <param name="meta">Meta dictionary</param>
        /// <returns>The data endpoint</returns>
        public override IDataEndpoint Create(Logger logger, MetaDictionary meta, MetaDictionary globalMeta)
        {
            IDataEndpoint server = null;

            ConstructorInfo ci = _type.GetConstructor(new [] { typeof(Logger) });
            if (ci != null)
            {
                server = (IDataEndpoint)ci.Invoke(new [] { logger });
            }
            else
            {
                ci = _type.GetConstructor(new Type[0]);
                if (ci == null)
                {
                    throw new NetServiceException("Can not find an appropriate constructor for endpoint");
                }

                server = (IDataEndpoint)ci.Invoke(new object[0]);
            }

            server.Meta = meta;
            server.GlobalMeta = globalMeta;

            if (Config != null)
            {
                IPersistNode persist = server as IPersistNode;

                if (persist != null)
                {
                    persist.SetState(Config, logger);
                }
            }

            return server;
        }
    }
}
