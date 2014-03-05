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
using System.IO;
using System.Reflection;
using CANAPE.DataFrames;
using CANAPE.Nodes;
using System.Threading;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Class to implement a stream node which only selects a specific part of the frame
    /// </summary>
    public class DynamicStreamPipelineNode  : BasePipelineNode
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DynamicStreamPipelineNode()
            : base()
        {
        }

        /// <summary>
        /// Method called when a new frame arraives
        /// </summary>
        /// <param name="frame">The frame</param>
        protected override void OnInput(DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);

            foreach (DataNode node in nodes)
            {
                try
                {
                    MemoryStream stm = new MemoryStream(node.ToArray());
                    DataReader reader = new DataReader(stm);
                    string name = node.Name;
                    DataKey parentKey = node.Parent;
                    node.RemoveNode();

                    while (stm.Position < stm.Length)
                    {
                        DynamicStreamDataKey2 key = new DynamicStreamDataKey2(name, Container, Graph.Logger, State);

                        reader.ByteCount = 0;
                        key.FromReader(reader);

                        // The reader clearly didn't care
                        if (reader.ByteCount == 0)
                        {
                            break;
                        }

                        parentKey.AddSubNode(key);
                        frame.Current = key;
                    }
                }
                catch (EndOfStreamException)
                {
                }
                catch (ThreadAbortException)
                {
                    throw;
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
