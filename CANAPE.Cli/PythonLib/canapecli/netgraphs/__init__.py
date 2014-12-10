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

import CANAPE.Cli.ScriptNodeConfig
import CANAPE.Cli.NetGraphUtils
import CANAPE.Cli.ScriptDataFrameFilterFactory
import inspect

def todot(netgraph):
    """Convert a netgraph document to the Graphviz dot format"""
    return CANAPE.Cli.NetGraphUtils.ToDot(netgraph)    

def defaultgraph():
    """Get a default graph with two logging nodes and two endpoints"""
    return CANAPE.Cli.NetGraphUtils.GetDefault()

def addscriptnode(graph_doc, label, func):
    """ Creates a scripted graph node from a function
    The function must take a dataframe as input and return the dataframe
    or list of frames to write out. Returning None will drop the frame entirely
    :param label: The label to apply to the node
    :param graph_doc: The graph document
    :param func: The function to add to the node
    """
    ret = CANAPE.Cli.ScriptNodeConfig(func)
    ret.Label = label
    
    return graph_doc.AddNode(ret)

def addmatcher(node, func):
    """ Adds a filter to a node configuration.
    Calls the passed function with a the received packet,
    return True to process the packet and False to drop
    :param node: The node configuration to add the filter to
    :param func: The function to call when matching, returns True to 
    make the node perform its normal operation 
    """
    arg_count = len(inspect.getargspec(func)[0])
    filter = CANAPE.Cli.ScriptDataFrameFilterFactory(func, arg_count)
    node.AddFilter(filter)

def removematcher(node, func):
    """ Removes a filter matcher
    :param node: node configuration to remove the matcher from
    :param func: The function to call when matching, 
    """
    CANAPE.Cli.ScriptDataFrameFilterFactory.RemoveFilter(func, node)