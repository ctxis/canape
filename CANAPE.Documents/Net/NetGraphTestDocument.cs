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
using CANAPE.Documents.Data;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// Document representing a netgraph test
    /// </summary>
    [Serializable]
    public class NetGraphTestDocument : TestDocument
    {
        private bool _clientToServer;
        private NetGraphDocument _document;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document">The netgraph document to test</param>
        public NetGraphTestDocument(NetGraphDocument document)
        {
            _document = document;
        }

        /// <summary>
        /// Get or set the direction of the test
        /// </summary>
        public bool ClientToServer
        {
            get { return _clientToServer; }
            set
            {
                if (_clientToServer != value)
                {
                    _clientToServer = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Create the test graph container
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="globals">Global meta</param>
        /// <returns>The new test graph container</returns>
        public override TestDocument.TestGraphContainer CreateTestGraph(Utils.Logger logger, MetaDictionary globals)
        {
            NetGraphFactory factory = _document.Factory;
            ClientEndpointFactory[] clients = factory.GetNodes<ClientEndpointFactory>();
            ServerEndpointFactory[] servers = factory.GetNodes<ServerEndpointFactory>();

            if ((clients.Length == 0) || (servers.Length == 0))
            {
                throw new ArgumentException("Graph must have a one client and one server endpoint to perform a test");
            }

            Guid inputNode = _clientToServer ? clients[0].Id : servers[0].Id;
            Guid outputNode = _clientToServer ? servers[0].Id : clients[0].Id;

            NetGraph graph = factory.Create(logger, null, globals, new MetaDictionary(), new PropertyBag("Connection"));

            return new TestDocument.TestGraphContainer(graph, graph.Nodes[inputNode], graph.Nodes[outputNode]);
        }

        /// <summary>
        /// Get default document name
        /// </summary>
        public override string DefaultName
        {
            get { return "NetGraph Test"; }
        }
    }
}
