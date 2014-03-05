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
using System.Linq;
using System.Text;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Net.Utils;
using System.Collections.Concurrent;

namespace CANAPE.Utils
{
    internal static class ParseWithUtils
    {
        private static NetGraph BuildGraph(ScriptContainer container, string classname, string selectionPath, out BasePipelineNode input, out ParseWithPipelineNode output)
        {
            DynamicNodeFactory factory = new DynamicNodeFactory("", Guid.NewGuid(), container, classname, null);
            ParseWithPipelineNodeFactory parseWithFactory = new ParseWithPipelineNodeFactory();

            factory.SelectionPath = selectionPath;

            NetGraphBuilder builder = new NetGraphBuilder();

            builder.AddNode(factory);
            builder.AddNode(parseWithFactory);
            builder.AddLine(factory, parseWithFactory, null);

            NetGraph graph = builder.Factory.Create(Logger.GetSystemLogger(), null, new MetaDictionary(), new MetaDictionary(), new PropertyBag("Connection"));

            input = graph.Nodes[factory.Id];
            output = (ParseWithPipelineNode)graph.Nodes[parseWithFactory.Id];

            return graph;
        }        

        public static IEnumerable<DataFrame> ParseFrames(IEnumerable<DataFrame> frames, string selectionPath, ScriptContainer container, string classname)
        {
            BasePipelineNode input;
            ParseWithPipelineNode output;
            IEnumerable<DataFrame> ret = new DataFrame[0];
            NetGraph graph = BuildGraph(container, classname, selectionPath, out input, out output);
            
            try
            {
                foreach (DataFrame frame in frames)
                {
                    input.Input(frame);
                }
                input.Shutdown(null);

                output.EventFlag.WaitOne(500);

                ret = output.Frames;
            }
            finally
            {
                ((IDisposable)graph).Dispose();
            }

            return ret;
        }

        public static DataNode ParseNode(DataNode dataNode, ScriptContainer container, string classname)
        {
            DataNode ret = null;

            if (dataNode != null)
            {
                string selectionPath = String.Format("/{0}", dataNode.PathName);
                DataFrame frame = new DataFrame(new DataKey("Root"));
                frame.Root.AddSubNode(dataNode.CloneNode());
                
                DataFrame[] frames = ParseFrames(new DataFrame[1] { frame }, selectionPath, container, classname).ToArray();
                                        
                if(frames.Length > 0)
                {
                    // We only replace the node with the first DataNode for now
                    ret = frames[0].SelectSingleNode(selectionPath);
                }
            }

            return ret;
        }
    }
}
