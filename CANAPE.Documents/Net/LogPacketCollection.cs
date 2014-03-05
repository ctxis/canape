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
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using CANAPE.Nodes;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// Class to hold a collection of packets
    /// </summary>
    [Serializable]
    public class LogPacketCollection : ObservableCollection<LogPacket>, IDeserializationCallback
    {
        /// <summary>
        /// Event for a frame being modified
        /// </summary>
        [field: NonSerialized]
        public event EventHandler FrameModified;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogPacketCollection()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="packets">List of log packets</param>
        public LogPacketCollection(IEnumerable<LogPacket> packets)
            : base(packets)
        {
        }

        /// <summary>
        /// Insert the item at a specified index
        /// </summary>
        /// <param name="index">The index to insert into</param>
        /// <param name="item">The item to insert</param>
        protected override void InsertItem(int index, LogPacket item)
        {
            item.Frame.FrameModified += Frame_FrameModified;
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Remove an item at a specified index
        /// </summary>
        /// <param name="index">The index to remove from</param>
        protected override void RemoveItem(int index)
        {
            LogPacket packet = this[index];

            packet.Frame.FrameModified -= Frame_FrameModified;

            base.RemoveItem(index);
        }

        /// <summary>
        /// Set a packet a specified index
        /// </summary>
        /// <param name="index">The index to set</param>
        /// <param name="item">The logpacket item</param>
        protected override void SetItem(int index, LogPacket item)
        {            
            item.Frame.FrameModified += Frame_FrameModified;

            base.SetItem(index, item);
        }

        /// <summary>
        /// Clear all items
        /// </summary>
        protected override void ClearItems()
        {
            foreach (LogPacket p in this.Items)
            {
                p.Frame.FrameModified -= Frame_FrameModified;
            }
            base.ClearItems();
        }

        /// <summary>
        /// Method to call when a frame is modified in the log
        /// </summary>
        protected virtual void OnFrameModified()
        {
            if (FrameModified != null)
            {
                FrameModified.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Event handlers for modified event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Frame_FrameModified(object sender, EventArgs e)
        {
            OnFrameModified();
        }

        /// <summary>
        /// Method called on deserialization
        /// </summary>
        /// <param name="sender"></param>
        public void OnDeserialization(object sender)
        {
            foreach (LogPacket p in this.Items)
            {
                p.FrameModified += Frame_FrameModified;
            }
        }

        /// <summary>
        /// Get a list of packets for a network connection
        /// </summary>
        /// <param name="netId">The network ID</param>
        /// <returns>The list of packets</returns>
        public LogPacket[] GetPacketsForNetwork(Guid netId)
        {
            List<LogPacket> packets = new List<LogPacket>();

            lock (this)
            {
                foreach (LogPacket packet in this)
                {
                    if (packet.NetId.Equals(netId))
                    {
                        packets.Add(packet);
                    }
                }
            }

            return packets.ToArray();
        }
    }
}
