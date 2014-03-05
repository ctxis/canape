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
using System.Security.Cryptography.X509Certificates;
using CANAPE.Security;

namespace CANAPE.Security.Cryptography.X509Certificates
{
    /// <summary>
    /// A class to hold a reference to an X509 certificate and where it came from
    /// </summary>
    [Serializable]
    public sealed class X509CertificateContainer : ICredentialObject
    {
        [NonSerialized]
        private X509Certificate2 _certificate;

        private byte[] _certificateData;
        private string _privateKey;
        private bool _fromStore;
        private StoreName _storeName;
        private StoreLocation _storeLocation;

        /// <summary>
        /// Clear all certificate information
        /// </summary>
        private void ClearCertificateInfo()
        {
            _certificateData = null;
            _certificate = null;
            _privateKey = null;
            _fromStore = false;
            _storeName = StoreName.My;
            _storeLocation = StoreLocation.CurrentUser;
        }

        /// <summary>
        /// Get the public certificate information, will not resolve the private key
        /// </summary>
        public X509Certificate2 PublicCertificate
        {
            get
            {
                if (_certificateData != null)
                {
                    return new X509Certificate2(_certificateData);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The actual certificate with private key, if key is protected will attempt to resolve the password
        /// </summary>
        public X509Certificate2 Certificate
        {
            get
            {
                if (_certificate == null)
                {
                    X509Certificate2 cert = new X509Certificate2(_certificateData);

                    if (_fromStore)
                    {
                        _certificate = CertificateUtils.FindCertByThumbprint(_storeName, _storeLocation, cert.Thumbprint);
                        // If no certificate + private key then request load through credentials manager (so for example insert smartcard)
                        // principal == X509CertificateContainer,THUMBPRINT@(_storeName/_storeLocation)
                        // If private key but protected then request password through credentials manager
                        // principal == SecurePasword,THUMBPRINT@(_storeName/_storeLocation)
                        // If couldn't get key or couldn't unprotect then throw an exception
                    }
                    else
                    {
                        _certificate = cert;
                        _certificate.PrivateKey = CertificateUtils.CreateRSAKeyFromXML(_privateKey, false);
                    }
                }

                return _certificate;
            }
        }        

        /// <summary>
        /// Load a certificate from a store
        /// </summary>
        /// <param name="storeName">The store name</param>
        /// <param name="storeLocation">The store location</param>        
        /// <param name="cert">The certificate to load</param>
        private void LoadCertificate(StoreName storeName, StoreLocation storeLocation, X509Certificate2 cert)
        {
            if (!cert.HasPrivateKey)
            {
                throw new ArgumentException(Properties.Resources.LoadCert_NeedPrivateKey);
            }

            ClearCertificateInfo();

            _certificate = cert;
            _certificateData = cert.Export(X509ContentType.Cert);
            _fromStore = true;
            _storeLocation = storeLocation;
            _storeName = storeName;
        }

        /// <summary>
        /// Load a certificate which can be serialized
        /// </summary>
        /// <param name="cert">The certificate to load</param>
        private void LoadCertificate(X509Certificate2 cert)
        {
            if (!cert.HasPrivateKey)
            {
                throw new ArgumentException(Properties.Resources.LoadCert_NeedPrivateKey);
            }

            ClearCertificateInfo();

            _certificate = cert;
            _certificateData = cert.Export(X509ContentType.Cert);
            _fromStore = false;
            
            _privateKey = _certificate.PrivateKey.ToXmlString(true);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cert">The certificate to load</param>
        public X509CertificateContainer(X509Certificate2 cert)
        {
            LoadCertificate(cert);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="storeName">The store name</param>
        /// <param name="storeLocation">The store location</param>        
        /// <param name="cert">The certificate to load</param>
        public X509CertificateContainer(StoreName storeName, StoreLocation storeLocation, X509Certificate2 cert)
        {
            LoadCertificate(storeName, storeLocation, cert);
        }

        /// <summary>
        /// Get the principal name, which is the thumbprint for a certificate
        /// </summary>
        /// <returns>The principal name</returns>
        public string GetPrincipalName()
        {
            return PublicCertificate.Thumbprint;
        }
    }
}
