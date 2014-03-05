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
using CANAPE.NodeFactories;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Config for a server endpoint
    /// </summary>
    [Serializable]
    public class ServerEndpointConfig : BaseNodeConfig
    {
        /// <summary>
        /// Name of graph node
        /// </summary>
        public const string NodeName = "server";

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
            return new ServerEndpointFactory(_label, _id);
        }
    }
}
