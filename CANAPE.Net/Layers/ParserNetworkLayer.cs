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
using System.IO;
using System.Threading;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Net.Layers
{
    /// <summary>
    /// A network layer which uses an IDataStreamParser to create packets
    /// </summary>
    public sealed class ParserNetworkLayer : DynamicNetworkLayer
    {
        private DynamicScriptContainer _clientContainer;
        private DynamicScriptContainer _serverContainer;
        private Logger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientContainer">The script container for the client sourced packets</param>
        /// <param name="serverContainer">The script container for the server sourced packets</param>
        /// <param name="logger">Logger to log output</param>
        public ParserNetworkLayer(DynamicScriptContainer clientContainer, DynamicScriptContainer serverContainer, Logger logger)
        {
            _clientContainer = clientContainer;
            _serverContainer = serverContainer;
            _logger = logger;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">The script container for both directions</param>
        /// <param name="logger">Logger to log output</param>
        public ParserNetworkLayer(DynamicScriptContainer container, Logger logger)
            : this(container, container, logger)
        {
        }

        private static IEnumerable<DataFrame> ReadFrames(IDataAdapter client, DynamicScriptContainer container, Logger logger, object config)
        {
            DataReader reader = new DataReader(new DataAdapterToStream(client));

            while (!reader.Eof)
            {
                DynamicStreamDataKey2 key = new DynamicStreamDataKey2("Root", container, logger, config);

                try
                {
                    key.FromReader(reader);
                }
                catch (ThreadAbortException)
                {
                    throw;
                }
                catch (EndOfStreamException)
                {
                    // End of stream, do nothing
                }
                catch (Exception e)
                {
                    logger.LogException(e);
                    yield break;
                }

                // Only fill in the frame if we read something, should this exit if it continues to read nothing?
                if (reader.ByteCount > 0)
                {
                    yield return new DataFrame(key);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        protected override IEnumerable<DataFrame> ReadClientFrames(IDataAdapter client)
        {
            return ReadFrames(client, _clientContainer, _logger, Config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        protected override IEnumerable<DataFrame> ReadServerFrames(IDataAdapter server)
        {
            return ReadFrames(server, _serverContainer, _logger, Config);
        }
    }
}
