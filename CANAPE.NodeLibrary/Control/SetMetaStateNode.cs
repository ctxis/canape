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
using System.ComponentModel;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Control
{
    [Serializable]
    public class SetMetaStateNodeConfig
    {
        [LocalizedDescription("SetMetaStateNodeConfig_ResetStateOnMatch", typeof(Properties.Resources))]
        public bool ResetStateOnMatch { get; set; }

        [LocalizedDescription("SetMetaStateNodeConfig_MetaName", typeof(Properties.Resources))]
        public string MetaName { get; set; }

        [LocalizedDescription("SetMetaStateNodeConfig_Global", typeof(Properties.Resources))]
        public bool Global { get; set; }

        [LocalizedDescription("SetMetaStateNodeConfig_Private", typeof(Properties.Resources))]
        public bool Private { get; set; }

        [LocalizedDescription("SetMetaStateNodeConfig_Value", typeof(Properties.Resources))]
        public string Value { get; set; }

        [LocalizedDescription("SetMetaStateNodeConfig_UseSelection", typeof(Properties.Resources))]
        public bool UseSelection { get; set; }

        [LocalizedDescription("SetMetaStateNodeConfig_ConvertSelectionToString", typeof(Properties.Resources))]
        public bool ConvertSelectionToString { get; set; }

        public SetMetaStateNodeConfig()
        {
            MetaName = "TestMeta";
            Value = "TestValue";
            ResetStateOnMatch = true;
        }
    }

    [NodeLibraryClass("SetMetaStateNode_Name", "SetMetaStateNode_Description", "SetMetaStateNode_NodeName", typeof(Properties.Resources),
        ConfigType = typeof(SetMetaStateNodeConfig),        
        Category = NodeLibraryClassCategory.Control)]
    public class SetMetaStateNode : BasePipelineNodeWithPersist<SetMetaStateNodeConfig>
    {
        protected override void OnInput(DataFrame frame)
        {
            object value = String.Empty;

            if (Config.UseSelection)
            {
                DataNode node = SelectSingleNode(frame);                

                if (node != null)
                {
                    DataValue v = node as DataValue;

                    if (v != null)
                    {
                        value = v.Value;
                        if (Config.ConvertSelectionToString)
                        {
                            value = value.ToString();
                        }
                    }
                    else
                    {
                        value = node.ToString();
                    }                    
                }
            }
            else
            {
                value = Config.Value;
            }

            // Clear race condition here :(
            if (Config.Global)
            {
                if (Config.ResetStateOnMatch)
                {
                    Graph.SetGlobalMeta(Config.MetaName, value, Config.Private);
                }
                else
                {
                    Graph.GetGlobalMeta(Config.MetaName, value, Config.Private);
                }
            }
            else
            {
                if (Config.ResetStateOnMatch)
                {
                    Graph.SetMeta(Config.MetaName, value, Config.Private);
                }
                else
                {
                    Graph.GetMeta(Config.MetaName, value, Config.Private);
                }
            }

            WriteOutput(frame);
        }
    }
}
