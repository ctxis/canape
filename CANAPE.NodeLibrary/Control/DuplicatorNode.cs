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
    /// Configuration for duplicator node
    /// </summary>
    [Serializable]
    public class DuplicatorNodeConfig
    {
        /// <summary>
        /// The number of times you duplicate the frame
        /// </summary>
        [LocalizedDescription("DuplicatorNodeConfig_DuplicationCount", typeof(Properties.Resources))]
        public int DuplicationCount { get; set; }

        public DuplicatorNodeConfig()
        {
            DuplicationCount = 1;
        }
    }

    [NodeLibraryClass("DuplicatorNode_Name", "DuplicatorNode_Description", "DuplicatorNode_NodeName", typeof(Properties.Resources),        
        ConfigType = typeof(DuplicatorNodeConfig),
        Category = NodeLibraryClassCategory.Control)]
    public class DuplicatorNode : BasePipelineNodeWithPersist<DuplicatorNodeConfig>
    {    
        protected override void OnInput(DataFrame frame)
        {
            for (int i = 0; i < Config.DuplicationCount; ++i)
            {
                WriteOutput(frame.CloneFrame());
            }
        }
    }
}
