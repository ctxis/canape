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

using System.Net;
using System.Net.Sockets;
using System.Reflection;
using CANAPE.Utils;

namespace CANAPE.Net.Utils
{
    /// <summary>
    /// Some simple utilities for network code
    /// </summary>
    public static class NetUtils
    {
        private static void AddEndpoint(string name, EndPoint ep, PropertyBag properties)
        {
            IPEndPoint ip = ep as IPEndPoint;
            properties.AddValue(name, ep);

            if(ip != null)
            {
                properties.AddValue(name + "Address", ip.Address);
                properties.AddValue(name + "Port", ip.Port);
            }
        }

        /// <summary>
        /// Populate a property bag from a socket
        /// </summary>
        /// <param name="sock">The socket</param>
        /// <param name="properties">The property bag</param>
        public static void PopulateBagFromSocket(Socket sock, PropertyBag properties)
        {
            properties.AddValue("AddressFamily", sock.AddressFamily);
            properties.AddValue("SocketType", sock.SocketType);
            properties.AddValue("ProtocolType", sock.ProtocolType);

            try
            {
                // This could throw a socket exception if not a connected socket (e.g. UDP)
                AddEndpoint("RemoteEndpoint", sock.RemoteEndPoint, properties);
            }
            catch (SocketException)
            {
            }

            AddEndpoint("LocalEndpoint", sock.LocalEndPoint, properties);
        }

        private static bool _osSupportsIPv4;
        private static bool _osSupportsIPv6;

        static NetUtils()
        {
            // Hedge our bets on it supporting both, likely
            _osSupportsIPv4 = true;
            _osSupportsIPv6 = true;

            // Initialize using reflection because Mono sucks
            try
            {
                PropertyInfo p = typeof(Socket).GetProperty("OSSupportsIPv4");
                if (p != null)
                {
                    _osSupportsIPv4 = (bool)p.GetValue(null, null);
                }
            }
            catch { }

            try
            {
                PropertyInfo p = typeof(Socket).GetProperty("OSSupportsIPv6");
                if (p != null)
                {
                    _osSupportsIPv6 = (bool)p.GetValue(null, null);
                }
            }
            catch { }
        }

        /// <summary>
        /// Method to get whether OS supports IPv4 here for compatibility with Mono
        /// </summary>
        public static bool OSSupportsIPv4
        {
            get
            {
                return _osSupportsIPv4;
            }
        }

        /// <summary>
        /// Method to get whether OS supports IPv6 here for compatibility with Mono
        /// </summary>
        public static bool OSSupportsIPv6
        {
            get
            {
                return _osSupportsIPv6;
            }
        }
    }
}
