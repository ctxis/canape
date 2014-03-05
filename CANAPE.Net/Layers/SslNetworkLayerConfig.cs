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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Authentication;
using CANAPE.Security.Cryptography.X509Certificates;
using CANAPE.Utils;

namespace CANAPE.Net.Layers
{
    /// <summary>
    /// The configuration of an SSL Network Layer
    /// </summary>
    [Serializable]
    public sealed class SslNetworkLayerConfig
    {
        const int DEFAULT_TIMEOUT = 4000;

        private SslProtocols _clientProtocol;
        private SslProtocols _serverProtocol;
        private bool _enabled;
        private ObservableCollection<X509CertificateContainer> _clientCertificates;
        private X509CertificateContainer _serverCertificate;
        private bool _specifyServerCertificate;
        private bool _requireClientCertificate;
        private bool _verifyServerCertificate;
        private bool _verifyClientCertificate;
        private bool _disableClient;
        private bool _disableServer;
        private int _timeout;
        
        // TODO: Possible extra options
        // Force server name for client
        // Override root CA
        // Clone chain

        //private X509CertificateContainer _rootCA;
        //private bool _cloneCompleteChain;
        //private string _forceServiceName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disableClient"></param>
        /// <param name="disableServer"></param>
        public SslNetworkLayerConfig(bool disableClient, bool disableServer)
        {
            _clientProtocol = disableClient ? SslProtocols.None : SslProtocols.Default;
            _serverProtocol = disableServer ? SslProtocols.None : SslProtocols.Default;
            _clientCertificates = new ObservableCollection<X509CertificateContainer>();
            _clientCertificates.CollectionChanged += new NotifyCollectionChangedEventHandler(_clientCertificates_CollectionChanged);
            _disableClient = disableClient;
            _disableServer = disableServer;
            _timeout = DEFAULT_TIMEOUT;
        }

        /// <summary>
        /// 
        /// </summary>
        public SslNetworkLayerConfig()
            : this(false, false)
        {
        }

        void _clientCertificates_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnConfigChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        [field: NonSerialized]
        public event EventHandler ConfigChanged;

        private void OnConfigChanged()
        {
            if (ConfigChanged != null)
            {
                ConfigChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Whether SSL is enabled at all
        /// </summary>
        public bool Enabled 
        { 
            get
            {
                return _enabled;
            }

            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnConfigChanged();
                }
            }
        }

        private void CheckClient()
        {
            if (_disableClient)
            {
                throw new NotSupportedException();
            }
        }

        private void CheckServer()
        {
            if (_disableServer)
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// List of client certificates
        /// </summary>
        public IList<X509CertificateContainer> ClientCertificates
        {
            get
            {
                // If disabled client just return a readonly empty list
                if (_disableClient)
                {
                    return new List<X509CertificateContainer>().AsReadOnly();
                }

                return _clientCertificates;
            }
        }

        /// <summary>
        /// The server certificate (if not in auto mode)
        /// </summary>
        public X509CertificateContainer ServerCertificate 
        {
            get
            {
                return _serverCertificate;
            }

            set
            {
                CheckServer();

                if (_serverCertificate != value)
                {
                    _serverCertificate = value;
                    OnConfigChanged();
                }
            }
        }

        /// <summary>
        /// Specifies the server cert (makes ServerCertificate valid)
        /// </summary>
        public bool SpecifyServerCert 
        {
            get
            {
                return _specifyServerCertificate;
            }

            set
            {
                CheckServer();

                if (_specifyServerCertificate != value)
                {
                    _specifyServerCertificate = value;
                    OnConfigChanged();
                }
            }
        }

        /// <summary>
        /// Specifed whether the remote server certificate is verified
        /// Should default to false as this is for testing only
        /// </summary>
        public bool VerifyServerCertificate
        {
            get
            {
                return _verifyServerCertificate;
            }

            set
            {
                CheckClient();

                if (_verifyServerCertificate != value)
                {
                    _verifyServerCertificate = value;
                    OnConfigChanged();
                }
            }
        }

        private SslProtocols GetProtocol(SslProtocols protocol)
        {
            if (Enum.IsDefined(typeof(SslProtocols), protocol))
            {
                return protocol;
            }
            else
            {
                return SslProtocols.Default;
            }
        }

        /// <summary>
        /// The client protocol, if SslProtocols.None then doesn't enable SSL
        /// </summary>
        public SslProtocols ClientProtocol
        {
            get
            {
                return GetProtocol(_clientProtocol);
            }
            set
            {
                CheckClient();

                if (_clientProtocol != value)
                {
                    _clientProtocol = value;
                    OnConfigChanged();
                }
            }
        }

        /// <summary>
        /// The server protocol, if SslProtocols.None then doesn't enable SSL
        /// </summary>
        public SslProtocols ServerProtocol
        {
            get
            {
                return GetProtocol(_serverProtocol);
            }
            set
            {
                CheckServer();

                if (_serverProtocol != value)
                {
                    _serverProtocol = value;
                    OnConfigChanged();
                }
            }
        }

        /// <summary>
        /// Whether a client certificate is required or not, whether it then
        /// matters what it sends depends on VerifyClientCertificate
        /// </summary>
        public bool RequireClientCertificate
        {
            get { return _requireClientCertificate; }
            set
            {
                CheckServer();

                if (_requireClientCertificate != value)
                {
                    _requireClientCertificate = value;
                    OnConfigChanged();
                }
            }
        }

        /// <summary>
        /// Specifed whether the client certificate is verified
        /// Should default to false as this is for testing only
        /// </summary>
        public bool VerifyClientCertificate
        {
            get
            {
                return _verifyClientCertificate;
            }

            set
            {
                CheckServer();

                if (_verifyClientCertificate != value)
                {
                    _verifyClientCertificate = value;
                    OnConfigChanged();
                }
            }
        }

        /// <summary>
        /// Timeout for the SSL connection
        /// </summary>
        public int Timeout
        {
            get { return _timeout == 0 ? DEFAULT_TIMEOUT : _timeout; }
            set
            {
                if (_timeout != value)
                {
                    if ((_timeout < 0) && (_timeout != System.Threading.Timeout.Infinite))
                    {
                        throw new ArgumentException();
                    }

                    _timeout = value;
                }
            }
        }

        /// <summary>
        /// Simple deep clone method
        /// </summary>
        /// <returns>The cloned configuration</returns>
        public SslNetworkLayerConfig Clone()
        {
            return (SslNetworkLayerConfig)GeneralUtils.CloneObject(this);
        }

        [OnDeserialized]
        private void OnDeserialization(StreamingContext context)
        {            
            _clientCertificates.CollectionChanged += new NotifyCollectionChangedEventHandler(_clientCertificates_CollectionChanged);
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>A textual description</returns>
        public override string ToString()
        {
            return String.Format(Properties.Resources.SslNetworkLayerConfig_ToString, _enabled);
        }
    }
}
