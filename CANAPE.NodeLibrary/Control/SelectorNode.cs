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
    /// <summary>
    /// Config for selector node
    /// </summary>
    [Serializable]
    public class SelectorNodeConfig
    {
        [LocalizedDescription("SelectorNodeConfig_PathName", typeof(Properties.Resources))]
        public string PathName { get; set; }

        public SelectorNodeConfig()
        {
            PathName = "/";
        }
    }

    /// <summary>
    /// A decision node implementation
    /// </summary>
    [NodeLibraryClass("SelectorNode_Name", "SelectorNode_Description", "SelectorNode_NodeName", typeof(Properties.Resources),        
        ConfigType = typeof(SelectorNodeConfig),
        Category = NodeLibraryClassCategory.Control
        )
    ]
    public class SelectorNode : BasePipelineNodeWithPersist<SelectorNodeConfig>
    {        
        protected override void OnInput(DataFrames.DataFrame frame)
        {            
            DataNode node = frame.SelectSingleNode(Config.PathName);

            if (node != null)
            {
                frame.Current = node;
            }            

            WriteOutput(frame);
        }
    }
}
