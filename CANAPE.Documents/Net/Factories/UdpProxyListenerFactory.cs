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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CANAPE.Net.Listeners;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Factory for a UDP proxy listener
    /// </summary>
    [Serializable]
    public sealed class UdpProxyListenerFactory : IpProxyListenerFactory
    {
        private bool _broadcast;

        /// <summary>
        /// Get or set broadcast mode
        /// </summary>
        public bool Broadcast
        {
            get { return _broadcast; }
            set { _broadcast = value; }
        }

        /// <summary>
        /// Create the network listener
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <returns>The new network listener</returns>
        public override INetworkListener Create(Utils.Logger logger)
        {
            return new UdpNetworkListener(AnyBind, IPv6, _broadcast, logger);
        }
    }
}
