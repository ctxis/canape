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
using System.Threading;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Nodes
{    
    /// <summary>
    /// Pipeline node which handles data from a pipeline using a data converter
    /// </summary>
    public class DynamicBinaryStreamPipelineNode : BaseStreamPipelineNode
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DynamicBinaryStreamPipelineNode()
        {            
        }

        /// <summary>
        /// Script container
        /// </summary>
        public DynamicScriptContainer Container { get; set; }

        /// <summary>
        /// The state for the node
        /// </summary>
        public object State { get; set; }
        
        /// <summary>
        /// Function called to handle the reading of the stream
        /// </summary>
        /// <param name="stm">The reading stream</param>
        protected override void OnRead(PipelineStream stm)
        {
            try
            {
                while (!stm.Eof)
                {
                    DynamicStreamDataKey2 key = new DynamicStreamDataKey2("Root", Container, Graph.Logger, State);

                    DataReader reader = new DataReader(stm);

                    key.FromReader(reader);

                    // Only fill in the frame if we read something, should this exit if it continues to read nothing?
                    if (reader.ByteCount > 0)
                    {
                        WriteOutput(new DataFrame(key));
                    }
                }
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (EndOfStreamException)
            {
                // End of stream, do nothing
            }
            catch (Exception e)
            {
                LogException(e);
            }            
        }
    }
}
