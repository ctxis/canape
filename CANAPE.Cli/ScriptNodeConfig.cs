using System;
using System.Collections;
using System.Collections.Generic;
using CANAPE.DataFrames;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Cli
{
    public class ScriptNodeConfig : BaseNodeConfig
    {
        class ScriptNode : BasePipelineNode
        {
            dynamic _scriptMethod;

            internal ScriptNode(dynamic scriptMethod)
            {
                _scriptMethod = scriptMethod;
            }

            protected override void OnInput(DataFrame frame)
            {
                dynamic f = _scriptMethod(frame);

                if (f != null)
                {
                    IEnumerable e = f as IEnumerable;

                    if (e != null)
                    {
                        foreach (dynamic o in e)
                        {
                            if ((o is IEnumerable) && !(o is byte[]))
                            {
                                WriteOutput(ConsoleUtils.ConvertListToByteArray(o));
                            }
                            else
                            {
                                WriteOutput(o);
                            }
                        }
                    }
                    else
                    {
                        WriteOutput(f);
                    }
                }
            }
        }
    
        class ScriptNodeFactory : BaseNodeFactory
        {
            dynamic _scriptMethod;

            internal ScriptNodeFactory(string label, Guid id, dynamic scriptMethod) 
                : base(label, id)
            {
                _scriptMethod = scriptMethod;
            }
    
            protected override Nodes.BasePipelineNode OnCreate(Utils.Logger logger, Nodes.NetGraph graph, Dictionary<string, object> stateDictionary)
            {
                return new ScriptNode(_scriptMethod);
            }
        }

        object _scriptMethod;

        public ScriptNodeConfig(object scriptMethod)
        {
            _scriptMethod = scriptMethod;
        }

        public const string NodeName = "script";

        public override string GetNodeName()
        {
            return NodeName;
        }

        protected override NodeFactories.BaseNodeFactory OnCreateFactory()
        {
            return new ScriptNodeFactory(_label, _id, _scriptMethod);
        }
    }
}
