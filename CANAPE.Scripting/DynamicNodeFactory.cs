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
using System.Threading;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DynamicNodeFactory : BaseNodeFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ScriptContainer Container { get; set; }        
        /// <summary>
        /// 
        /// </summary>
        public object State { get; set; }

        /// <summary>
        /// A factory object for a dynamic scripted node
        /// </summary>
        /// <param name="label">The node label</param>
        /// <param name="guid">The node guid</param>
        /// <param name="container">A container for the script code</param>
        /// <param name="classname">The name of the class to create</param>
        /// <param name="state">Node state</param>
        public DynamicNodeFactory(string label, Guid guid, ScriptContainer container, string classname, object state) : base(label, guid)
        {
            Container = container;
            ClassName = classname;            
            State = state;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override BasePipelineNode OnCreate(Logger logger, NetGraph graph, Dictionary<string, object> stateDictionary)
        {
            BasePipelineNode node = null;

            if (Container != null)
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(ClassName))
                    {
                        throw new ArgumentException(String.Format(CANAPE.Scripting.Properties.Resources.DynamicNodeFactory_MustSpecifyClassName, Label));
                    }

                    // Determine what type of object we create, if it is an BasePipelineNode then return as is,
                    // if it is a parser then create the parsing pipeline node to contain it
                    object o = ScriptUtils.GetInstance(Container, ClassName);
                    if (o is BasePipelineNode)
                    {
                        node = o as BasePipelineNode;
                        if (State != null)
                        {
                            IPersistNode persist = node as IPersistNode;
                            if (persist != null)
                            {
                                persist.SetState(State, logger);
                            }
                        }
                    }
                    else if (o is IDataStreamParser)
                    {
                        // If we are selecting the whole packet, then we convert a binary stream
                        if (SelectionPath == "/")
                        {
                            DynamicBinaryStreamPipelineNode stmNode = new DynamicBinaryStreamPipelineNode();
                            stmNode.Container = DynamicScriptContainer.Create(Container, ClassName);
                            stmNode.State = State;

                            node = stmNode;
                        }
                        else
                        {
                            DynamicStreamPipelineNode dynNode = new DynamicStreamPipelineNode();

                            dynNode.Container = DynamicScriptContainer.Create(Container, ClassName); 
                            dynNode.State = State;

                            node = dynNode;
                        }
                    }
                    else if (o is IDataArrayParser)
                    {
                        DynamicArrayPipelineNode dynNode = new DynamicArrayPipelineNode();

                        dynNode.Container = DynamicScriptContainer.Create(Container, ClassName);
                        dynNode.State = State;

                        node = dynNode;
                    }
                    else if (o is IDataStringParser)
                    {
                        DynamicStringPipelineNode dynNode = new DynamicStringPipelineNode();

                        dynNode.Container = DynamicScriptContainer.Create(Container, ClassName);
                        dynNode.State = State;

                        node = dynNode;
                    }                    
                    else if (o == null)
                    {
                        throw new InvalidOperationException(String.Format(CANAPE.Scripting.Properties.Resources.DynamicNodeFactory_CannotCreateType, ClassName, Label));
                    }
                    else
                    {
                        throw new InvalidOperationException(String.Format(CANAPE.Scripting.Properties.Resources.DynamicNodeFactory_InvalidNodeType, o.GetType().Name, Label));
                    }

                    node.Enabled = Enabled;
                }
                catch (EndOfStreamException)
                {
                    // End of stream
                }
                catch (ThreadAbortException)
                {
                }
            }
            else
            {
                throw new ArgumentException(String.Format(CANAPE.Scripting.Properties.Resources.DynamicNodeFactory_NoScriptSpecified, Label));
            }
            
            return node;
        }
    }    

}
