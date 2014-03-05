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
using CANAPE.Nodes;
using System.Collections.Generic;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// Config for decision node
    /// </summary>
    [Serializable]
    public class DecisionNodeFactory : BaseNodeFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public string PathName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="guid"></param>
        /// <param name="pathName"></param>
        public DecisionNodeFactory(string label, Guid guid, string pathName)
            : base(label, guid)
        {
            PathName = pathName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="graph"></param>
        /// <param name="stateDictionary"></param>
        /// <returns></returns>
        protected override Nodes.BasePipelineNode OnCreate(Utils.Logger logger, Nodes.NetGraph graph, Dictionary<string, object> stateDictionary)
        {
            return new DecisionNode(PathName);
        }
    }
}
