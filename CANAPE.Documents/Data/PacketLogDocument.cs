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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using CANAPE.DataFrames;
using CANAPE.Documents.Net;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// Document to contain a packet log
    /// </summary>
    [Serializable]
    public class PacketLogDocument : BaseDocumentObject
    {
        LogPacketCollection _packets;

        /// <summary>
        /// The collection of packets
        /// </summary>
        public LogPacketCollection Packets { get { return _packets; } }

        /// <summary>
        /// Get an array of packets by tag
        /// </summary>
        /// <param name="tag">The tag</param>
        /// <returns>The array of packets</returns>
        public LogPacket[] GetPacketsByTag(string tag)
        {
            List<LogPacket> ret = new List<LogPacket>();
            lock (_packets)
            {
                foreach (LogPacket p in _packets)
                {
                    string currTag = p.Tag ?? "";
                    if (currTag.Equals(tag, StringComparison.OrdinalIgnoreCase))
                    {
                        ret.Add(p);
                    }
                }
            }

            return ret.ToArray();
        }

        /// <summary>
        /// Get all packets from this document
        /// </summary>
        /// <returns>The array of packets</returns>
        public LogPacket[] GetPackets()
        {
            LogPacket[] ret;

            lock (_packets)
            {
                ret = _packets.ToArray();
            }

            return ret;
        }

        /// <summary>
        /// Add a packet to the log
        /// </summary>
        /// <param name="packet">The packet</param>
        public void AddPacket(LogPacket packet)
        {
            lock (_packets)
            {
                _packets.Add(packet);
            }
        }

        /// <summary>
        /// Add a range of packets
        /// </summary>
        /// <param name="packets">The packets</param>
        public void AddRangePacket(IEnumerable<LogPacket> packets)
        {
            lock (_packets)
            {
                foreach (LogPacket packet in packets)
                {
                    _packets.Add(packet);
                }
            }
        }

        /// <summary>
        /// Add a simple packet with a tag and a frame
        /// </summary>
        /// <param name="tag">The tag to add</param>
        /// <param name="frame">The frame to add, this will be cloned before putting in log</param>
        /// <returns>The logged packet</returns>
        public LogPacket AddPacket(string tag, DataFrame frame)
        {
            LogPacket ret = new LogPacket(tag, Guid.NewGuid(), "Packet Log", frame.CloneFrame(), ColorValue.White);
            AddPacket(ret);

            return ret;
        }

        /// <summary>
        /// Add a simple packet with a tag and a frame
        /// </summary>
        /// <param name="tag">The tag to add</param>
        /// <param name="frame">The frame to add</param>
        /// <returns>The logged packet</returns>
        public LogPacket AddPacket(string tag, IDictionary frame)
        {
            return AddPacket(tag, new DataFrame(frame));
        }

        /// <summary>
        /// Add a simple packet with a tag and a frame
        /// </summary>
        /// <param name="tag">The tag to add</param>
        /// <param name="frame">The frame to add</param>
        /// <returns>The logged packet</returns>
        public LogPacket AddPacket(string tag, byte[] frame)
        {
            return AddPacket(tag, new DataFrame(frame));
        }

        /// <summary>
        /// Add a simple packet with a tag and a frame
        /// </summary>
        /// <param name="tag">The tag to add</param>
        /// <param name="frame">The frame to add</param>
        /// <returns>The logged packet</returns>
        public LogPacket AddPacket(string tag, string frame)
        {
            return AddPacket(tag, new DataFrame(frame));
        }

        private void SetupCollections()
        {
            _packets.CollectionChanged += new NotifyCollectionChangedEventHandler(packets_CollectionChanged);
            _packets.FrameModified += new EventHandler(_packets_FrameModified);
        }

        void _packets_FrameModified(object sender, EventArgs e)
        {
            Dirty = true;
        }

        void packets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PacketLogDocument() 
            : this(new LogPacket[0])
        {         
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="packets"></param>
        public PacketLogDocument(LogPacket[] packets)
        {
            _packets = new LogPacketCollection(packets);
            SetupCollections();
        }

        /// <summary>
        /// Method called when deserializing
        /// </summary>        
        protected override void OnDeserialization()
        {
            SetupCollections();
        }

        /// <summary>
        /// Default name of document
        /// </summary>
        public override string DefaultName
        {
            get { return "Packet Log"; }
        }

    }
}
