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


namespace CANAPE.Controls.GraphEditor
{    
    /// <summary>
    /// Class to store the state of a directed graph
    /// </summary>
    public class GraphData
    {
        GraphNode[] _nodes;
        public GraphNode[] Nodes
        {
            get
            {
                return (GraphNode[])_nodes.Clone();
            }
        }

        GraphLine[] _lines;
        public GraphLine[] Lines
        {
            get
            {
                return (GraphLine[])_lines.Clone();
            }
        }

        public GraphData(GraphNode[] nodes, GraphLine[] lines)
        {
            _nodes = nodes;
            _lines = lines;
        }

        public GraphData()
        {
            _nodes = new GraphNode[0];
            _lines = new GraphLine[0];
        }
    }
}
