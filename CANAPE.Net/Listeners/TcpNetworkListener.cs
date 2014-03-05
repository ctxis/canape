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
using CANAPE.DataAdapters;
using CANAPE.Net.Utils;
using CANAPE.Utils;

namespace CANAPE.Net.Listeners
{
    /// <summary>
    /// Network listener implement for a TCP server
    /// TODO: Add two listeners for both v4 and v6 endpoints, maybe just make that the default?
    /// </summary>
    public class TcpNetworkListener : INetworkListener
    {
        bool _isStarted;
        TcpListener _listener;
        Logger _logger;
        List<TcpClient> _pending;
        bool _autoBind;
        bool _nodelay;

        /// <summary>
        /// Listener endpoint
        /// </summary>
        public IPEndPoint EndPoint { get { return (IPEndPoint)_listener.Server.LocalEndPoint; } }

        internal static IPEndPoint BuildEndpoint(bool anyBind, bool ipv6, int port)
        {
            if (ipv6)
            {
                return new IPEndPoint(anyBind ? IPAddress.IPv6Any : IPAddress.IPv6Loopback, port);
            }
            else
            {
                return new IPEndPoint(anyBind ? IPAddress.Any : IPAddress.Loopback, port);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bindAddress">The address to bind to</param>
        /// <param name="logger">Logger to report errors to</param>
        /// <param name="nodelay">Set whether the socket will have nagle algorithm disabled</param>
        /// <param name="reuseaddr">Set whether the listener should be bound with SO_REUSEADDR flag</param>
        public TcpNetworkListener(IPEndPoint bindAddress, Logger logger, bool nodelay, bool reuseaddr)
        {
            _logger = logger;
            _listener = new TcpListener(bindAddress);
            _listener.ExclusiveAddressUse = !reuseaddr;
            _pending = new List<TcpClient>();

            if (bindAddress.Port == 0)
            {
                _autoBind = true;
            }

            _nodelay = nodelay;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="anyBind">True to bind to all addresses, otherwise just localhost</param>
        /// <param name="ipv6">Whether to use IPv6</param>
        /// <param name="port">The TCP port</param>
        /// <param name="logger">Logger to report errors to</param>
        /// <param name="nodelay">Set whether the socket will have nagle algorithm disabled</param>
        public TcpNetworkListener(bool anyBind, bool ipv6, int port, Logger logger, bool nodelay) 
            : this(BuildEndpoint(anyBind, ipv6, port), logger, nodelay, false)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="anyBind">True to bind to all addresses, otherwise just localhost</param>
        /// <param name="ipv6">Whether to use IPv6</param>        
        /// <param name="logger">Logger to report errors to</param>
        /// <param name="nodelay">Set whether the socket will have nagle algorithm disabled</param>
        public TcpNetworkListener(bool anyBind, bool ipv6, Logger logger, bool nodelay) 
            : this(anyBind, ipv6, 0, logger, nodelay)
        {            
        }

        private void AcceptCallback(IAsyncResult res)
        {
            GeneralUtils.SetThreadCulture();

            if (res.IsCompleted)
            {
                try
                {                                       
                    TcpListener listener = ((TcpListener)res.AsyncState);

                    TcpClient client = null;

                    try
                    {
                        client = listener.EndAcceptTcpClient(res);
                        client.NoDelay = _nodelay;
                    }
                    finally
                    {
                        // Restart it
                        if (_isStarted)
                        {
                            listener.BeginAcceptTcpClient(AcceptCallback, listener);
                        }
                    }

                    if (ClientConnected != null)
                    {
                        lock (_pending)
                        {
                            _pending.Add(client);
                        }

                        _logger.LogVerbose(CANAPE.Net.Properties.Resources.TcpNetworkListener_ConnectionLogString, client.Client.RemoteEndPoint);

                        TcpClientDataAdapter da = new TcpClientDataAdapter(client);
                        ClientConnectedEventArgs e = new ClientConnectedEventArgs(da);
                        NetUtils.PopulateBagFromSocket(client.Client, e.Properties);

                        ClientConnected.Invoke(this, e);

                        lock (_pending)
                        {
                            _pending.Remove(client);
                        }
                    }
                    else
                    {
                        // There was noone to accept the message, so just close
                        client.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // Don't do anything
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
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
                _listener.Start();
                if (_autoBind)
                {
                    _logger.LogInfo(Properties.Resources.TcpNetworkListener_AutoBind, _listener.Server.LocalEndPoint);
                }
                _listener.BeginAcceptTcpClient(AcceptCallback, _listener);                
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
                _isStarted = false;
                try
                {
                    _listener.Stop();
                    lock (_pending)
                    {
                        foreach (TcpClient client in _pending)
                        {
                            try
                            {
                                client.Close();
                            }
                            catch
                            {
                            }
                        }

                        _pending.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Logger.GetSystemLogger().LogException(ex);
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
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        /// Implements ToString
        /// </summary>
        /// <returns>Description of listener</returns>
        public override string ToString()
        {
            return String.Format(Properties.Resources.IpNetworkListener_ToStringFormat, "TCP", _listener.LocalEndpoint);
        }
    }
}
