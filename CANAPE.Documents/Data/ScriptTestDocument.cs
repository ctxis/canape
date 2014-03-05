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
using System.Collections.Concurrent;
using System.Collections.Generic;
using CANAPE.Net.Utils;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;
using CANAPE.Documents.Net.NodeConfigs;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// Document representing a script test
    /// </summary>
    [Serializable]
    public class ScriptTestDocument : TestDocument
    {
        // Convert to using a config
        [Obsolete]
        private ScriptDocument _document;
        [Obsolete]
        private string _classname;
        [Obsolete]
        private string _path;
        [Obsolete]
        private Dictionary<string, string> _props;


        private DynamicNodeConfig _config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document">The script document to test</param>
        public ScriptTestDocument(ScriptDocument document)
        {
#pragma warning disable 612
            _document = null;
            _path = null;
#pragma warning restore 612
            _config = new DynamicNodeConfig();
            _config.Label = "node";
            _config.Script = document;
            _config.DirtyChanged += new EventHandler(_config_DirtyChanged);
        }

        /// <summary>
        /// On deserialization
        /// </summary>
        protected override void OnDeserialization()
        {
            base.OnDeserialization();

            if (_config == null)
            {
                _config = new DynamicNodeConfig();
                _config.Label = "node";

#pragma warning disable 612
                _config.Properties = _props;
                _config.SelectionPath = _path;
                _config.ClassName = _classname;
                _config.Script = _document;
                _props = null;
                _classname = null;
#pragma warning restore 612

                _config.DirtyChanged += new EventHandler(_config_DirtyChanged);

            }
        }

        void _config_DirtyChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        /// Create the test graph
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="globals">The global meta</param>
        /// <returns>The new test graph container</returns>
        /// <exception cref="ArgumentException">Throw if script invalid</exception>
        public override TestGraphContainer CreateTestGraph(Logger logger, MetaDictionary globals)
        {
            if (String.IsNullOrWhiteSpace(_config.ClassName))
            {
                throw new ArgumentException(CANAPE.Documents.Properties.Resources.ScriptTestDocument_MustProvideClassname);
            }

            NetGraphBuilder builder = new NetGraphBuilder();
            ServerEndpointFactory server = builder.AddServer("server", Guid.NewGuid());
            DynamicNodeFactory node = builder.AddNode((DynamicNodeFactory)_config.CreateFactory());            
            LogPacketNodeFactory log = builder.AddLog("Test Network", Guid.NewGuid(), new ColorValue(0xFF, 0xFF, 0xFF, 0xFF), "Entry", false);
            ClientEndpointFactory client = builder.AddClient("client", Guid.NewGuid());
            
            builder.AddLine(server, node, null);
            builder.AddLine(node, log, null);
            builder.AddLine(log, client, null);

            NetGraph graph = builder.Factory.Create(logger, null, globals, new MetaDictionary(), new PropertyBag("Connection"));

            return new TestGraphContainer(graph, graph.Nodes[server.Id], graph.Nodes[client.Id]);
        }

        /// <summary>
        /// Get default document name
        /// </summary>
        public override string DefaultName
        {
            get { return "Script Test"; }
        }

        /// <summary>
        /// Get or set class name to use for test
        /// </summary>
        public string ClassName
        {
            get { return _config.ClassName; }
            set
            {
                if (_config.ClassName != value)
                {
                    _config.ClassName = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set selection path for test
        /// </summary>
        public string Path
        {
            get { return _config.SelectionPath; }
            set
            {
                if (_config.SelectionPath != value)
                {
                    _config.SelectionPath = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set properties
        /// </summary>
        public Dictionary<string, string> Properties
        {
            get { return _config.Properties; }
            set
            {
                if (_config.Properties != value)
                {
                    _config.Properties = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// The script document
        /// </summary>
        public ScriptDocument Document
        {
            get { return _config.Script; }
        }

        /// <summary>
        /// The dynamic config
        /// </summary>
        public DynamicNodeConfig Config { get { return _config; } }

        /// <summary>
        /// Get list of classes
        /// </summary>
        /// <returns></returns>
        public string[] GetClassNames()
        {
            return ScriptUtils.GetTypes(_config.Script.Container);
        }
    }
}
