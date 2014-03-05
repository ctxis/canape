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

using System.Collections.Generic;
using CANAPE.DataFrames.Filters;
using CANAPE.NodeFactories;
using CANAPE.Net.Layers;
using CANAPE.DataFrames;
using System;

namespace CANAPE.Nodes
{
    /// <summary>
    /// A filter which determines what to do when a packet arrives at a layer section    
    /// </summary>
    /// <remarks>The basics of how this should match, if IsMatch is true then we will create a graph. We can then 
    /// check the descriminator (if it is set) to see if there is a secondary selection mechanism. This is important
    /// for things like demuxing protocols as it allows you to specify a graph per-channel</remarks>
    public class LayerSectionFilter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filter">The filter which checks for a match</param>
        /// <param name="factory">The graph factory to create on match</param>
        /// <param name="layers">The binding layers for the graph</param>
        /// <param name="selectionPath">Selection path to act as a discriminator</param>
        /// <param name="isolatedGraph">Whether to isolate the graph, only sharing global meta</param>
        /// <param name="filterId">The ID of the filter</param>        
        public LayerSectionFilter(IDataFrameFilter filter, NetGraphFactory factory, INetworkLayerFactory[] layers, string selectionPath, bool isolatedGraph, Guid filterId)
        {
            Filter = filter;
            Factory = factory;
            Layers = layers;
            SelectionPath = selectionPath;
            FilterId = filterId;
            IsolatedGraph = isolatedGraph;
        }

        /// <summary>
        /// The filter which checks for a match
        /// </summary>
        public IDataFrameFilter Filter { get; private set; }

        /// <summary>
        /// The factory for the netgraph
        /// </summary>
        public NetGraphFactory Factory { get; private set; }

        /// <summary>
        /// The list of layers to apply to the graph
        /// </summary>
        public INetworkLayerFactory[] Layers { get; private set; }

        /// <summary>
        /// A selection path to use as a discriminator, for example you can select out 
        /// a channel ID which will be used to de-multiplex channels
        /// </summary>
        public string SelectionPath { get; private set; }

        /// <summary>
        /// The ID of the filter
        /// </summary>
        public Guid FilterId { get; private set; }

        /// <summary>
        /// Indicates the graph is isolated
        /// </summary>
        public bool IsolatedGraph { get; private set; }
    }
}
