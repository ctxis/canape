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
using System.IO;
using CANAPE.DataFrames;
using CANAPE.Nodes;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Pipeline node which parses an array
    /// </summary>
    public class DynamicArrayPipelineNode : BasePipelineNode
    {        
        /// <summary>
        /// OnInput Method
        /// </summary>
        /// <param name="frame">The data frame</param>
        protected override void OnInput(DataFrame frame)
        {             
            DataNode[] nodes = frame.SelectNodes(SelectionPath);

            foreach (DataNode node in nodes)
            {
                try
                {                    
                    DynamicArrayDataKey2 key = new DynamicArrayDataKey2(node.Name, Container, Graph.Logger, State);                                       

                    key.FromArray(node.ToArray());
                    node.ReplaceNode(key);
                    frame.Current = key;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                }
            }

            WriteOutput(frame);
        }


        /// <summary>
        /// Script container
        /// </summary>
        public DynamicScriptContainer Container { get; set; }
      
        /// <summary>
        /// Gets or sets the state for the dynamic object
        /// </summary>
        public object State { get; set; }
    }
}
