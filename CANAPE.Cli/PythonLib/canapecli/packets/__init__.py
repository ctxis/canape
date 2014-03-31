#    CANAPE Network Testing Tool
#    Copyright (C) 2014 Context Information Security
#
#    This program is free software: you can redistribute it and/or modify
#    it under the terms of the GNU General Public License as published by
#    the Free Software Foundation, either version 3 of the License, or
#    (at your option) any later version.
#
#    This program is distributed in the hope that it will be useful,
#    but WITHOUT ANY WARRANTY; without even the implied warranty of
#    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#    GNU General Public License for more details.
#
#    You should have received a copy of the GNU General Public License
#    along with this program.  If not, see <http:#www.gnu.org/licenses/>.

import CANAPE.Utils.ConsoleUtils
import CANAPE.Nodes
import CANAPE.DataFrames
import CANAPE.Utils
import System

def tohexstr(packet):
    """Convert a packet to a hex string
    :param packet: The packet or frame to convert
    """    
    return CANAPE.Utils.ConsoleUtils.ConvertBinaryPacketToString(packet)

def tostr(packet):
    """Convert a packet to a text string
    :param packet: The packet or frame to convert
    """
    return CANAPE.Utils.ConsoleUtils.ConvertTextPacketToString(packet)

def totree(packet):
    """Convert a packet to a tree based text string
    :param packet: The packet or frame to convert
    """
    return CANAPE.Utils.ConsoleUtils.ConvertPacketToTreeString(packet)

def newframe(data=None):
    """Create a new data frame from existing data
    :param data: The data, can be a byte array, string, dictionary or None for an empty frame
    """
    if data is None:
        return CANAPE.DataFrames.DataFrame()
    else:
        return CANAPE.DataFrames.DataFrame(data)

def newpacket(data, tag="Unknown", network="Unknown Network", netid="{00000000-0000-0000-0000-000000000000}"):
    """Create a new log packet from existing data    
    :param data: The data, can be a byte array, string, dictionary or None for an empty frame
    :param tag: Textual tag to associate with frame
    :param network: Name of network where the log packet came from
    :param netid: GUID of originating network
    """
    return CANAPE.Nodes.LogPacket(tag, System.Guid.Parse(netid), network, newframe(data), CANAPE.Utils.ColorValue.White)

def addfilter(service, func):
    """Add a packet filter to a network service
    :param func: The filter function, takes the packet and the source graph as arguments. Can return True to filter out the packet from the normal destination
    :param service: The service to add the filter to    
    """
    def packet_filter(sender, e):
        r = func(e.Packet, e.Graph)
        if r is not None:
            e.Filter = r

    service.FilterLogPacketEvent += packet_filter