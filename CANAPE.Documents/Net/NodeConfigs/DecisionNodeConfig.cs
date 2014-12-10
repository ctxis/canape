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
using CANAPE.NodeFactories;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Config for a decision node
    /// </summary>
    [Serializable]
    public class DecisionNodeConfig : BaseNodeConfig
    {
        private string _pathName;

        /// <summary>
        /// Get or set the path name to send matched frames down
        /// </summary>
        [LocalizedDescription("DecisionNodeConfig_PathNameDescription", typeof(Properties.Resources)), Category("Control")]
        public string PathName
        {
            get
            {
                return _pathName;
            }
            set
            {
                if (_pathName != value)
                {
                    _pathName = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Name of graph node
        /// </summary>
        public const string NodeName = "ifnode";

        /// <summary>
        /// Get graph node name
        /// </summary>
        /// <returns>Always "server"</returns>
        public override string GetNodeName()
        {
            return NodeName;
        }

        /// <summary>
        /// Method called to create the factory
        /// </summary>
        /// <returns>The BaseNodeFactory</returns>
        protected override BaseNodeFactory OnCreateFactory()
        {
            return new DecisionNodeFactory(_label, _id, _pathName);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DecisionNodeConfig()
        {
            _pathName = "True";
        }

        /// <summary>
        /// Add an edge for true, based on the current path name
        /// </summary>
        /// <param name="destNode">The destination node</param>
        /// <returns>The edge configuration</returns>
        public LineConfig AddTrueEdge(BaseNodeConfig destNode)
        {
            return AddEdge(_pathName, destNode);
        }

        /// <summary>
        /// Add an edge for false, based on the current path name
        /// </summary>
        /// <param name="destNode">The destination node</param>
        /// <returns>The edge configuration</returns>
        public LineConfig AddFalseEdge(BaseNodeConfig destNode)
        {
            return AddEdge(String.Format("!{0}", _pathName), destNode);
        }
    }
}
