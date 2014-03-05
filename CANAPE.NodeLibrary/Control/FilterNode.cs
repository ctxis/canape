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
using CANAPE.Scripting;

namespace CANAPE.NodeLibrary.Control
{
    [Serializable]
    public class FilterNodeConfig
    {
    }

    [NodeLibraryClass("FilterNode_Name", "FilterNode_Description", "FilterNode_NodeName", typeof(Properties.Resources),
        ConfigType = typeof(FilterNodeConfig),        
        Category = NodeLibraryClassCategory.Control)]
    public class FilterNode : BasePipelineNodeWithPersist<FilterNodeConfig>
    {
        protected override void OnInput(DataFrames.DataFrame frame)
        {
            // Do nothing, anything matching the filters should be dropped       
        }
    }
}
