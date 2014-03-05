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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using CANAPE.Documents.Net;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// Base document for tests
    /// </summary>
    [Serializable]
    public abstract class TestDocument : BaseDocumentObject
    {        
        LogPacketCollection _inputPackets;
        LogPacketCollection _outputPackets;

        /// <summary>
        /// Constructor
        /// </summary>
        public TestDocument()
        {
            _inputPackets = new LogPacketCollection();
            _outputPackets = new LogPacketCollection();
            SetupCollections();
        }

        /// <summary>
        /// Method called on deserialization
        /// </summary>
        protected override void OnDeserialization()
        {
            SetupCollections();
        }

        private void SetupCollections()
        {
            _inputPackets.CollectionChanged += new NotifyCollectionChangedEventHandler(TestDocument_CollectionChanged);
            _outputPackets.CollectionChanged += new NotifyCollectionChangedEventHandler(TestDocument_CollectionChanged);
            _inputPackets.FrameModified += new EventHandler(_packets_FrameModified);
            _outputPackets.FrameModified += new EventHandler(_packets_FrameModified);
        }

        void _packets_FrameModified(object sender, EventArgs e)
        {
            Dirty = true;
        }

        void TestDocument_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        /// Get the collection of input packets
        /// </summary>
        [Browsable(false)]
        public LogPacketCollection InputPackets
        {
            get { return _inputPackets; }
        }

        /// <summary>
        /// Get collection of output packets
        /// </summary>
        [Browsable(false)]
        public LogPacketCollection OutputPackets
        {
            get { return _outputPackets; }
        }

        /// <summary>
        /// Get all input packets
        /// </summary>
        /// <returns>Array of input packets</returns>
        public LogPacket[] GetInputPackets()
        {
            lock (_inputPackets)
            {
                return _inputPackets.ToArray();
            }
        }

        /// <summary>
        /// Add a packet to the input
        /// </summary>
        /// <param name="packet">The packet</param>
        public void AddInputPacket(LogPacket packet)
        {
            lock (_inputPackets)
            {
                _inputPackets.Add(packet);
            }
        }

        /// <summary>
        /// Add a range of packets to the input
        /// </summary>
        /// <param name="packets">The packets</param>
        public void AddRangeInputPacket(IEnumerable<LogPacket> packets)
        {
            lock (_inputPackets)
            {
                foreach (LogPacket packet in packets)
                {
                    _inputPackets.Add(packet);
                }
            }
        }

        /// <summary>
        /// Container to hold test parameters
        /// </summary>
        public class TestGraphContainer : IDisposable
        {
            /// <summary>
            /// The current netgraph
            /// </summary>
            public NetGraph Graph { get; private set; }
            /// <summary>
            /// The input pipeline node
            /// </summary>
            public BasePipelineNode Input { get; private set; }
            /// <summary>
            /// The output pipeline node
            /// </summary>
            public BasePipelineNode Output { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="graph">The current netgraph</param>
            /// <param name="input">The input pipeline node</param>
            /// <param name="output">The output pipeline node</param>
            internal TestGraphContainer(NetGraph graph, BasePipelineNode input, BasePipelineNode output)
            {
                Graph = graph;
                Input = input;
                Output = output;
            }

            #region IDisposable Members

            void IDisposable.Dispose()
            {
                if (Graph != null)
                {
                    ((IDisposable)Graph).Dispose();
                }
            }

            #endregion
        }

        /// <summary>
        /// Method to constructor the test graph
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="globals"></param>
        /// <returns></returns>
        public abstract TestGraphContainer CreateTestGraph(Logger logger, MetaDictionary globals);
    }
}
