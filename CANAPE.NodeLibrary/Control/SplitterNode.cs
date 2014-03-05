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

namespace CANAPE.NodeLibrary.Control
{
    /// <summary>
    /// Configuration for a splitter node
    /// </summary>
    [Serializable]
    public class SplitterNodeConfig
    {

    }

    /// <summary>
    /// Splitter node
    /// </summary>
    [NodeLibraryClass("SplitterNode_Name", "SplitterNode_Description", "SplitterNode_NodeName", typeof(Properties.Resources),        
        ConfigType = typeof(SplitterNodeConfig),        
        Category = NodeLibraryClassCategory.Control)]
    public class SplitterNode : BasePipelineNodeWithPersist<SplitterNodeConfig>
    {
        protected override void OnInput(DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);

            if (nodes.Length > 1)
            {
                for (int i = 0; i < nodes.Length; ++i)
                {
                    DataFrame newFrame = frame.CloneFrame();

                    DataNode[] newNodes = newFrame.SelectNodes(SelectionPath);

                    for (int j = 0; j < newNodes.Length; ++j)
                    {
                        if (j != i)
                        {
                            newNodes[j].RemoveNode();
                        }
                    }

                    newFrame.Current = newNodes[i];
                    WriteOutput(newFrame);
                }
            }
            else
            {
                WriteOutput(frame);
            }
        }
    }
}
