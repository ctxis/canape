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
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Nodes
{
    /// <summary>
    /// A class to hold a single log entry
    /// </summary>
    [Serializable]
    public class LogPacket : ICloneable
    {        
        /// <summary>
        /// Array format string for copy and paste
        /// </summary>
        public const string LogPacketArrayFormat = "Packet Data Set";

        /// <summary>
        /// Capture timestamp
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Data tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Uuid of the packet
        /// </summary>
        public Guid Uuid { get; private set; }

        /// <summary>
        /// Uuid of the original connection where this came from
        /// </summary>
        public Guid NetId { get; private set; }

        /// <summary>
        /// Network description
        /// </summary>
        public string Network { get; set; }

        /// <summary>
        /// The captured frame
        /// </summary>
        public DataFrame Frame { get; private set; }

        /// <summary>
        /// The color to draw
        /// </summary>
        public ColorValue Color { get; set; }

        /// <summary>
        /// Get the hash of the logged frame
        /// </summary>
        public string Hash
        {
            get
            {
                
                return Frame.Hash;
            }
        }

        /// <summary>
        /// Get the length of the logged frame
        /// </summary>
        public long Length
        {
            get
            {
                return Frame.Length;
            }
        }

        /// <summary>
        /// Get a string version of the packet
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Frame.ToString();
        }

        /// <summary>
        /// An event when the logged frame is modified
        /// </summary>        
        public event EventHandler FrameModified
        {
            add { Frame.FrameModified += value;  }
            remove { Frame.FrameModified += value;  }
        }

        /// <summary>
        /// Private Constructor
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="netid"></param>
        /// <param name="uuid"></param>
        /// <param name="network"></param>
        /// <param name="frame"></param>
        /// <param name="color"></param>
        /// <param name="timestamp"></param>
        public LogPacket(string tag, Guid netid, Guid uuid, string network, DataFrame frame, ColorValue color, DateTime timestamp)
        {
            Tag = tag;
            NetId = netid;
            Uuid = uuid;
            Network = network;
            Frame = frame;
            Color = color;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="netid"></param>
        /// <param name="network"></param>
        /// <param name="frame"></param>
        /// <param name="color"></param>
        public LogPacket(string tag, Guid netid, string network, DataFrame frame, ColorValue color) 
            : this(tag, netid, Guid.NewGuid(), network, frame, color, DateTime.Now)        
        {
        }

        /// <summary>
        /// Constructor from an event object
        /// </summary>
        /// <param name="args">The event args</param>
        public LogPacket(LogPacketEventArgs args) 
            : this(args.Tag, args.NetId, Guid.NewGuid(), args.NetworkDescription, args.Frame, args.Color, args.Timestamp) 
        {
        }

        /// <summary>
        /// Clone the entire log packet
        /// </summary>
        /// <returns>The cloned packet</returns>
        public LogPacket ClonePacket()
        {
            return new LogPacket(Tag, NetId, Uuid, Network, Frame.CloneFrame(), Color, Timestamp);
        }

        /// <summary>
        /// Clone the entire log packet
        /// </summary>
        /// <returns>The cloned packet</returns>
        public object Clone()
        {
            return ClonePacket();
        }
    }
}
