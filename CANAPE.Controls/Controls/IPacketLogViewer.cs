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

using CANAPE.Documents.Net;
using CANAPE.Nodes;

namespace CANAPE.Controls
{
    /// <summary>
    /// Chart interface
    /// </summary>
    public interface IPacketLogViewer
    {
        /// <summary>
        /// Method to set the packets for the chart
        /// </summary>
        /// <param name="packets">The array of packets</param>
        void SetPackets(LogPacket[] packets);

        /// <summary>
        /// Method to set the packets from a collection
        /// </summary>
        /// <param name="packets">The collection of packets</param>
        void SetPackets(LogPacketCollection packets);
    }
}
