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
using CANAPE.Documents.Extension;
using CANAPE.Net.Layers;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Network layer factory for an SSL connection
    /// </summary>
    [Serializable, NetworkLayerFactory("SslNetworkLayerFactory", typeof(Properties.Resources))]
    public class SslNetworkLayerFactory : BaseNetworkLayerFactory
    {
        /// <summary>
        /// The SSL configuration
        /// </summary>
        public SslNetworkLayerConfig Config { get; set; }

        /// <summary>
        /// Create the network layer
        /// </summary>
        /// <param name="logger">The logger to use when creating</param>
        /// <returns>The created layer</returns>
        public override INetworkLayer CreateLayer(Logger logger)
        {
            SslNetworkLayer layer = new SslNetworkLayer(Config);
            layer.Binding = Binding;

            return layer;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config">The SSL Configuration</param>
        public SslNetworkLayerFactory(SslNetworkLayerConfig config)
        {
            Config = config;
            Description = Properties.Resources.SslNetworkLayerFactory_Description;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SslNetworkLayerFactory()
            : this(new SslNetworkLayerConfig(false, false))
        {
            Config.Enabled = true;
        }

        [OnDeserialized]
        private void OnDeserializing(StreamingContext context)
        {
            if (Config == null)
            {
                Config = new SslNetworkLayerConfig(false, false);
            }
        }
    }
}
