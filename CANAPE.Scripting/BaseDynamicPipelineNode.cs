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

using System.Collections.Generic;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Nodes
{
    /// <summary>
    /// A base class for dynamic node scripts which incorporates configuration settings
    /// </summary>
    public abstract class BaseDynamicPipelineNode : BasePipelineNode, IPersistNode
    {
        private DynamicConfigObject _config;

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseDynamicPipelineNode()
        {
            _config = new DynamicConfigObject();
        }
        
        object IPersistNode.GetState(Utils.Logger logger)
        {
            return _config;
        }

        void IPersistNode.SetState(object state, Utils.Logger logger)
        {
            if (state is DynamicConfigObject)
            {
                _config = (DynamicConfigObject)state;
            }
        }

        /// <summary>
        /// Gets the config object
        /// </summary>
        public dynamic Config { get { return _config; } }
    }
}
