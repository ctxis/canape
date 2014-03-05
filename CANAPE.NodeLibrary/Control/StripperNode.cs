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
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Control
{
    /// <summary>
    /// Configuration for a stripper node
    /// </summary>
    [Serializable]
    public class StripperNodeConfig
    {        
        [LocalizedDescription("StripperNodeConfig_ExclusiveDescription", typeof(Properties.Resources))]
        public bool Exclusive { get; set; }        
    }

    /// <summary>
    /// Stripper node
    /// </summary>
    [NodeLibraryClass("StripperNode", typeof(Properties.Resources),
        ConfigType = typeof(StripperNodeConfig),
        Category = NodeLibraryClassCategory.Control)]
    public class StripperNode : BasePipelineNodeWithPersist<StripperNodeConfig>
    {
        protected override void OnInput(DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);

            if (Config.Exclusive)
            {
                foreach (DataNode n in nodes)
                {
                    n.RemoveNode();
                }
            }
            else if(nodes.Length > 0)
            {                
                DataNode node = nodes[0].Detach();
                DataKey key = node as DataKey;

                if (key != null)
                {
                    frame = new DataFrame(key);
                }
                else
                {
                    frame = new DataFrame();
                    frame.Root.AddSubNode(node);
                }
            }

            WriteOutput(frame);
        }
    }
}
