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

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class LineConfig
    {
        /// <summary>
        /// The currently assigned netgraph, may be null
        /// </summary>
        [NonSerialized]
        private NetGraphDocument _document;

        /// <summary>
        /// Specifies the document this edge is currently assigned to
        /// </summary>        
        internal NetGraphDocument Document 
        {
            get
            {
                return _document;
            }

            set
            {
                _document = value;
            }
        }

        /// <summary>
        /// The source node configuration
        /// </summary>
        public BaseNodeConfig SourceNode { get; set; }
        /// <summary>
        /// The destination node configuration
        /// </summary>
        public BaseNodeConfig DestNode { get; set; }
        /// <summary>
        /// Indicates if the edge is bi-directional
        /// </summary>
        public bool BiDirection { get; set; }
        /// <summary>
        /// Indicates the label of the edge
        /// </summary>
        public string PathName { get; set; }
        /// <summary>
        /// Indicates a weak path 
        /// </summary>
        public bool WeakPath { get; set; }
        /// <summary>
        /// Indicates the label of the edge
        /// </summary>
        public string Label { get { return PathName; } set { PathName = value; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source">The source node configuration</param>
        /// <param name="dest">The destination node configuration</param>
        /// <param name="biDirection">Indicates if the edge is bi-directional</param>
        /// <param name="pathName">Indicates the label of the edge</param>
        /// <param name="weak">Indicates a weak path </param>
        public LineConfig(BaseNodeConfig source, BaseNodeConfig dest, 
            bool biDirection, string pathName, bool weak)
        {
            SourceNode = source;
            DestNode = dest;
            BiDirection = biDirection;
            PathName = pathName;
            WeakPath = weak;
        }

        /// <summary>
        /// Adds an edge between the destination node of this edge 
        /// and another if it doesn't exist
        /// </summary>
        /// <param name="destNode">The destination node</param>
        /// <returns>The edge configuration</returns>
        public LineConfig AddEdge(BaseNodeConfig destNode)
        {
            if (_document != null)
            {
                return _document.AddEdge(null, this.DestNode, destNode);
            }

            return null;
        }

        /// <summary>
        /// Remove the current edge
        /// </summary>
        public void RemoveEdge()
        {
            if (_document != null)
            {
                _document.RemoveEdge(this);
            }
        }
    }
}
