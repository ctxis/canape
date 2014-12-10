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
import inspect
import types

def tohex(packet):
    """Convert a packet to a hex string
    :param packet: The packet or frame to convert
    """    
    return CANAPE.Utils.ConsoleUtils.ConvertBinaryPacketToString(packet)

def totext(packet):
    """Convert a packet to a text string
    :param packet: The packet or frame to convert
    """
    return CANAPE.Utils.ConsoleUtils.ConvertTextPacketToString(packet)

def totree(packet):
    """Convert a packet to a tree based text string
    :param packet: The packet or frame to convert
    """
    return CANAPE.Utils.ConsoleUtils.ConvertPacketToTreeString(packet)

def tostr(packet):
    """Convert a packet to string depending on the type of packet it is,
    for example basic frames will be in hex, basic text as a text string
    and more complex packets as a tree
    :param packet: The packet or frame to convert
    """
    return CANAPE.Utils.ConsoleUtils.ConvertPacketToString(packet)

    """Convert a list of packets to HTML
    :param ps: The list of packet or frames
    """
def tohtml(ps):
    return CANAPE.Utils.ConsoleUtils.ConvertPacketsToHtml(ps)

    """ Replace the contents of a frame with a list or generator
    :param frame: The frame to replace
    :param list: The list or generator to replace with
    """
def fromlist(frame, list):
    return frame.FromArray(CANAPE.Utils.ConsoleUtils.ConvertListToByteArray(list))    

    """ Convert a frame to a list object
    :param frame: The frame to convert
    """
def tolist(frame):
    ret = []
    for b in frame.ToArray():
        ret.append(int(b))

    return ret

def newframe(data=None, encoding="binary"):
    """Create a new data frame from existing data
    :param data: The data, can be a byte array, string, dictionary or None for an empty frame
    """
    if data is None:
        return CANAPE.DataFrames.DataFrame()
    elif (type(data) is list) or (type(data) is types.GeneratorType) or (type(data) is types.XRangeType):
        return CANAPE.DataFrames.DataFrame(CANAPE.Utils.ConsoleUtils.ConvertListToByteArray(data)) 
    elif type(data) is str:
        return CANAPE.DataFrames.DataFrame(data, encoding)
    else:
        return CANAPE.DataFrames.DataFrame(data)

def newframe_bytes(data):
    """Create a new data frame from existing data
    :param data: The data can be a byte array or a string, creates a bytes frame
    """
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
    arg_count = len(inspect.getargspec(func)[0])

    def packet_filter_1(sender, e):    
        r = func(e.Packet)
        if r is not None:
            e.Filter = r

    def packet_filter_2(sender, e):        
        r = func(e.Packet, e.Graph)
        if r is not None:
            e.Filter = r

    if arg_count == 1:
        service.FilterLogPacketEvent += packet_filter_1
    elif arg_count == 2:
        service.FilterLogPacketEvent += packet_filter_2

def inject(graph, nodename, data):
    """Inject a packet into the graph a specific node
    :param graph: The graph to inject into
    "param nodename: The name of the node, can a UUID or a label name
    :param data: The data to inject, can be any form supported by newframe, or data frame
    """
    frame = None
    if type(data) is not CANAPE.DataFrames.DataFrame:
        frame = newframe(data)

    node = graph.GetNodeByName(nodename)

    if node is not None:
        node.Input(frame)
    else:
        print "Couldn't find node " + nodename

def save(filename, packets):
    """ Save a list of packets to a file for later use
    :param filename: The output file name to write to
    :param packets: A packet or list of packets to write
    """
    CANAPE.Utils.ConsoleUtils.SavePackets(filename, packets)

def load(filename):
    """ Load a list of packets from a file
    :param filename: The input file, previous saved with the save method
    """
    return list(CANAPE.Utils.ConsoleUtils.LoadPackets(filename))

def loadpcap(filename):
    """ Basic load packets from a PCAP
    Must be an Ethernet link type, also only loads TCPv4
    :param filename: The input PCAP file
    """
    return list(CANAPE.Utils.PcapReader.Load(filename, False))