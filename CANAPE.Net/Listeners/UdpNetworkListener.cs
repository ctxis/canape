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
using System.Net;
using System.Net.Sockets;
using CANAPE.Net.DataAdapters;
using CANAPE.Utils;
using CANAPE.Net.Utils;

namespace CANAPE.Net.Listeners
{
    /// <summary>
    /// This class implements a mechanism to multiplex connections to UDP sockets (which are inherently stateless)
    /// </summary>
    public class UdpNetworkListener : INetworkListener
    {
        UdpClient _clientSocket;
        Dictionary<IPEndPoint, LockedQueue<byte[]>> _conns;
        IPEndPoint _localEndpoint;
        Logger _logger;
        bool _broadcast;
        bool _isStarted;
        bool _reuseaddr;
        IPAddress[] _multicastGroups;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bindAddress">The address to bind to</param>
        /// <param name="broadcast">Set whether the socket is broadcast enabled</param>
        /// <param name="multicastGroups">A list of multicast groups to join</param>
        /// <param name="reuseaddr">Set whether the listener should be bound with SO_REUSEADDR flag</param>
        /// <param name="logger">Logger to report errors to</param>
        public UdpNetworkListener(IPEndPoint bindAddress, IPAddress[] multicastGroups , bool broadcast, bool reuseaddr, Logger logger)
        {
            _conns = new Dictionary<IPEndPoint, LockedQueue<byte[]>>();
            _logger = logger;
            _localEndpoint = bindAddress;
            _isStarted = false;
            _broadcast = broadcast;
            _reuseaddr = reuseaddr;
            _multicastGroups = multicastGroups ?? new IPAddress[0];

            ReopenConnection();

            if (_localEndpoint.Port == 0)
            {
                _logger.LogInfo(Properties.Resources.UdpNetworkListener_AutoBind, _clientSocket.Client.LocalEndPoint);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="anyBind"></param>
        /// <param name="ipv6"></param>
        /// <param name="port"></param>
        /// <param name="broadcast"></param>
        /// <param name="logger"></param>
        public UdpNetworkListener(bool anyBind, bool ipv6, int port, bool broadcast, Logger logger) 
            : this(TcpNetworkListener.BuildEndpoint(anyBind, ipv6, port), null, broadcast, false, logger)
        {
        }

        /// <summary>
        /// Constructor, bind to a random port
        /// </summary>
        /// <param name="anyBind"></param>
        /// <param name="ipv6"></param>
        /// <param name="broadcast"></param>
        /// <param name="logger"></param>
        public UdpNetworkListener(bool anyBind, bool ipv6, bool broadcast, Logger logger) 
            : this(anyBind, ipv6, 0, broadcast, logger)
        {            
        }

        private void ReopenConnection()
        {
            if (_clientSocket != null)
            {
                try
                {
                    _clientSocket.Close();
                }
                catch (SocketException)
                {
                }
            }
            _clientSocket = new UdpClient(_localEndpoint);
            _clientSocket.EnableBroadcast = _broadcast;
            _clientSocket.ExclusiveAddressUse = !_reuseaddr;
            foreach (IPAddress addr in _multicastGroups)
            {
                _clientSocket.JoinMulticastGroup(addr);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ep"></param>
        /// <returns></returns>
        public byte[] Read(IPEndPoint ep)
        {
            lock (_conns)
            {
                if (!_conns.ContainsKey(ep))
                {
                    throw new ArgumentException(CANAPE.Net.Properties.Resources.UdpNetworkListener_NoUdpConnection);
                }
            }

            return _conns[ep].Dequeue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ep"></param>
        public void CloseConnection(IPEndPoint ep)
        {
            lock (_conns)
            {
                if (_conns.ContainsKey(ep))
                {
                    _conns.Remove(ep);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ep"></param>
        public void Write(byte[] data, IPEndPoint ep)
        {
            _clientSocket.Send(data, data.Length, ep);
        }

        private void ReceiveCallback(IAsyncResult res)
        {
            GeneralUtils.SetThreadCulture();

            if (res.IsCompleted)
            {
                try
                {                    
                    IPEndPoint ep = null;
                    bool bNewConnection = false;
                    UdpClient client = (UdpClient)res.AsyncState;

                    byte[] data = null;

                    try
                    {
                        data = client.EndReceive(res, ref ep);
                    }
                    catch (SocketException ex)
                    {
                        // For a server this just means the thing we last sent to ignored us
                        // Should possibly reopen the connection?
                        if ((SocketError)ex.ErrorCode == SocketError.ConnectionReset)
                        {
                            ReopenConnection();
                            client = _clientSocket;
                        }
                        else
                        {
                            // Rethrow
                            throw;
                        }
                    }

                    try
                    {
                        client.BeginReceive(ReceiveCallback, client);
                    }                    
                    catch (SocketException ex)
                    {
                        // For a server this just means the thing we last sent to ignored us
                        if ((SocketError)ex.ErrorCode == SocketError.ConnectionReset)
                        {
                            ReopenConnection();
                            client = _clientSocket;
                            client.BeginReceive(ReceiveCallback, client);
                        }
                        else
                        {
                            // Rethrow
                            throw;
                        }
                    }

                    if (data != null)
                    {
                        lock (_conns)
                        {
                            if (!_conns.ContainsKey(ep))
                            {
                                _logger.LogVerbose(CANAPE.Net.Properties.Resources.UdpNetworkListener_ConnectionLogString, ep);

                                _conns.Add(ep, new LockedQueue<byte[]>());
                                bNewConnection = true;
                            }
                        }

                        _conns[ep].Enqueue(data);

                        if (bNewConnection && (ClientConnected != null))
                        {
                            ClientConnectedEventArgs args = new ClientConnectedEventArgs(new UdpServerDataAdapter(this, ep));

                            NetUtils.PopulateBagFromSocket(_clientSocket.Client, args.Properties);
                            
                            ClientConnected.Invoke(this, args);
                        }
                    }
                }
                catch (SocketException)
                {
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        /// <summary>
        /// Start the listener
        /// </summary>
        public void Start()
        {
            if (!_isStarted)
            {                
                _clientSocket.BeginReceive(ReceiveCallback, _clientSocket);
                _isStarted = true;
            }
        }

        /// <summary>
        /// Stop the listener
        /// </summary>
        public void Stop()
        {
            if (_isStarted)
            {
                foreach (KeyValuePair<IPEndPoint, LockedQueue<byte[]>> pair in _conns)
                {
                    pair.Value.Stop();
                }

                _isStarted = false;
                try
                {
                    _clientSocket.Close();
                }
                catch (SocketException)
                {
                }
                finally
                {
                    _clientSocket = null;
                }
            }
        }

        /// <summary>
        /// Event called when a client connects
        /// </summary>
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            try
            {
                Stop();
            }
            catch (SocketException)
            {
            }
        }

        /// <summary>
        /// Implements ToString
        /// </summary>
        /// <returns>Description of listener</returns>
        public override string ToString()
        {
            return String.Format(Properties.Resources.IpNetworkListener_ToStringFormat, "UDP", _localEndpoint);
        }
    }
}
