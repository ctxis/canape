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
using System.Timers;
using CANAPE.DataAdapters;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net;
using CANAPE.Net.Listeners;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Documents.Net
{
    /// <summary>
    /// A network client which runs automatically
    /// </summary>
    [Serializable]
    public class NetAutoClientDocument : NetClientDocument
    {
        private DataEndpointFactory _factory;
        private int _concurrentConnections;
        private long _timeoutMilliSeconds;
        private bool _specifyTimeout;
        private bool _limitConnections;
        private int _totalConnections;

        class AutoNetworkListener : INetworkListener
        {
            private Timer _timer;
            private ProxyNetworkService _service;
            private NetAutoClientDocument _document;
            private Logger _logger;
            private int _connsLeft;
            private int _connCount;

            public AutoNetworkListener(NetAutoClientDocument document, Logger logger)
            {
                _document = document;

                if (_document._factory == null)
                {
                    throw new NetServiceException(CANAPE.Documents.Properties.Resources.NetAutoClientDocument_NoFactory);
                }

                _connsLeft = document._totalConnections < 0 ? 0 : document._totalConnections;
                _logger = logger;
                _timer = new Timer(10);
                _timer.AutoReset = false;
                _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            }

            public void SetService(ProxyNetworkService service)
            {
                _service = service;
                _service.CloseConnectionEvent += new EventHandler<ConnectionEventArgs>(_service_CloseConnectionEvent);
            }

            void _service_CloseConnectionEvent(object sender, ConnectionEventArgs e)
            {
                if (_document._limitConnections && (_connsLeft == 0) && (_service.Connections.Length == 0))
                {
                    _service.Stop();
                }
            }

            private void Poll()
            {
                // Not much point if no event handler
                if (ClientConnected != null)
                {
                    NetGraph[] graphs = _service.Connections;

                    int newConnections = _document._concurrentConnections - graphs.Length;

                    long currTicks = DateTime.UtcNow.Ticks;

                    if (_document._specifyTimeout && (_document._timeoutMilliSeconds >= 0))
                    {
                        TimeSpan span = new TimeSpan(currTicks);
                        foreach (NetGraph graph in graphs)
                        {
                            if (span.Subtract(graph.CreatedTicks).TotalMilliseconds >= (double)_document._timeoutMilliSeconds)
                            {
                                _service.CloseConnection(graph);

                                // Add another connection
                                newConnections++;
                            }
                        }
                    }

                    if (_document._limitConnections)
                    {
                        newConnections = Math.Min(_connsLeft, newConnections);
                    }

                    while (newConnections > 0)
                    {
                        IDataAdapter adapter = new DataEndpointAdapter(
                            _document._factory.Create(_logger, new MetaDictionary(), _document._globalMeta), _logger);

                        ClientConnectedEventArgs args = new ClientConnectedEventArgs(adapter);

                        args.Properties.AddValue("ConnectionNumber", System.Threading.Interlocked.Increment(ref _connCount));

                        ClientConnected(this, new ClientConnectedEventArgs(adapter));

                        newConnections--;
                        if (_connsLeft > 0)
                        {
                            _connsLeft--;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (_document._limitConnections && (_connsLeft == 0) && (_service.Connections.Length == 0))
                    {
                        // Don't reenable timer
                        _logger.LogInfo("Completed {0} connections", _document._totalConnections);
                    }
                    else
                    {
                        _timer.Start();
                    }
                }
            }

            void _timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                if (_service.Active)
                {
                    Poll();
                }
            }

            public void Start()
            {
                _timer.Start();
            }

            public void Stop()
            {                
                _timer.Stop();
            }

            public event EventHandler<ClientConnectedEventArgs> ClientConnected;

            public void Dispose()
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }
            }
        }

        /// <summary>
        /// Get or set the data client factory
        /// </summary>
        public DataEndpointFactory ClientFactory
        {
            get
            {
                return _factory;
            }

            set
            {
                if (_factory != value)
                {
                    _factory = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Number of connecurrent connections
        /// </summary>
        public int ConcurrentConnections
        {
            get
            {
                return _concurrentConnections;
            }

            set
            {
                if (_concurrentConnections != value)
                {
                    _concurrentConnections = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Timeout for connections in milliseconds, -1 is no timeout
        /// </summary>
        public long TimeOutMilliSeconds
        {
            get
            {
                return _timeoutMilliSeconds;
            }

            set
            {
                if (_timeoutMilliSeconds != value)
                {
                    _timeoutMilliSeconds = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Use the timeout value
        /// </summary>
        public bool SpecifyTimeout
        {
            get { return _specifyTimeout; }
            set
            {
                if (_specifyTimeout != value)
                {
                    _specifyTimeout = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Specify that the total connections should be limited
        /// </summary>
        public bool LimitConnections
        {
            get { return _limitConnections;  }
            set
            {
                if (_limitConnections != value)
                {
                    _limitConnections = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Get or set the total number of connections to make
        /// </summary>
        public int TotalConnections
        {
            get { return _totalConnections;  }
            set
            {
                if (_totalConnections != value)
                {
                    _totalConnections = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Create a network listener for the client
        /// </summary>        
        /// <param name="logger">The logger</param>
        /// <returns>The network listener</returns>
        protected override INetworkListener CreateListener(Logger logger)
        {
            AutoNetworkListener listener = new AutoNetworkListener(this, logger);

            return listener;
        }

        /// <summary>
        /// Create the network service
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <returns>The service</returns>
        public override ProxyNetworkService Create(Logger logger)
        {
            if (_concurrentConnections <= 0)
            {
                throw new NetServiceException(CANAPE.Documents.Properties.Resources.NetAutoClientDocument_InvalidConncurrentConnections);
            }

            if (_specifyTimeout && (_timeoutMilliSeconds < 0))
            {
                throw new NetServiceException(CANAPE.Documents.Properties.Resources.NetAutoClientDocument_InvalidTimeout);
            }

            if (_factory == null)
            {
                throw new NetServiceException(CANAPE.Documents.Properties.Resources.NetAutoClientDocument_NoFactory);
            }

            ProxyNetworkService service = base.Create(logger);

            ((AutoNetworkListener)service.Listener).SetService(service);

            return service;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public NetAutoClientDocument()
        {
            _specifyTimeout = true;
            _timeoutMilliSeconds = 10000;
            _concurrentConnections = 1;
            _totalConnections = 100;
        }
    }
}
