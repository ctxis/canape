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

using CANAPE.DataFrames;
using System.Collections.Generic;

namespace CANAPE.Nodes
{
    /// <summary>
    /// 
    /// </summary>
    public class DecisionNode : BasePipelineNode
    {
        string _pathName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathName"></param>
        public DecisionNode(string pathName)
        {
            _pathName = pathName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        protected override void OnInput(DataFrame frame)
        {
            bool match = false;

            if (Filters != null)
            {
                match = Filters.IsMatch(frame, Graph.Meta, Graph.GlobalMeta, Graph.ConnectionProperties, Graph.Uuid, this);
            }

            if (match)
            {
                WriteOutput(frame, _pathName);
            }
            else
            {
                WriteOutputExclude(frame, _pathName);
            }
        }

        /// <summary>
        /// Always return true, we will use the filters in OnInput
        /// </summary>
        /// <param name="frame">The frame to check</param>
        /// <returns>Always true</returns>
        protected override bool CanHandleFrame(DataFrame frame)
        {
            return true;
        }
    }
}
