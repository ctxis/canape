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
using System.IO;
using System.Security.Cryptography.X509Certificates;
using CANAPE.Utils;
using CANAPE.Net;

namespace CANAPE.Security.Cryptography.X509Certificates
{
    /// <summary>
    /// X509 Certificate Manager, caches created certs
    /// </summary>
    public static class CertManager
    {
        /// <summary>
        /// The root subject name for the CANAPE CA
        /// </summary>
        public const string RootCAName = "CN=CANAPE Root CA, O=Context Information Security, OU=Certificates";

        static Dictionary<string, X509Certificate2> _certs = new Dictionary<string, X509Certificate2>();

        /// <summary>
        /// Get a certificate which matches the passed in version
        /// </summary>
        /// <param name="match">The certificate to match</param>
        /// <returns>The certificate for this match</returns>        
        public static X509Certificate2 GetCertificate(X509Certificate match)
        {
            X509Certificate2 ret = null;

            if (match == null)
            {                
                throw new ArgumentException(CANAPE.Net.Properties.Resources.CertManager_NullMatch);
            }

            lock (_certs)
            {
                if (_certs.ContainsKey(match.Subject))
                {
                    ret = _certs[match.Subject];
                }
            }

            if (ret == null)
            {
                ret = CertificateUtils.CloneAndSignCertificate(match, GetRootCert(), true);
                if (ret != null)
                {
                    lock (_certs)
                    {
                        _certs[match.Subject] = ret;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Get a certificate based on the subject name
        /// </summary>
        /// <param name="subjectName">Subject name for the certificate</param>
        /// <returns>The certificate for this match</returns>        
        public static X509Certificate2 GetCertificate(string subjectName)
        {
            X509Certificate2 ret = null;

            lock (_certs)
            {
                if (_certs.ContainsKey(subjectName))
                {
                    ret = _certs[subjectName];
                }
            }

            if (ret == null)
            {                
                DateTime notBefore = DateTime.Now;
                ret = CertificateUtils.CreateCert(GetRootCert(),
                    new X500DistinguishedName(subjectName), null, false, 1024,
                    CertificateHashAlgorithm.Sha1, notBefore, notBefore.AddYears(10), null);
                if (ret != null)
                {
                    lock (_certs)
                    {
                        _certs[subjectName] = ret;
                    }
                }                
            }

            return ret;
        }

        /// <summary>
        /// Add a certificate to a certificate store
        /// </summary>
        /// <param name="storeName">The store name</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="cert">The certificate to add</param>        
        public static void AddCertToStore(StoreName storeName, StoreLocation storeLocation, X509Certificate2 cert)
        {
            X509Store store = new X509Store(storeName, storeLocation);

            try
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(cert);
            }
            finally
            {
                store.Close();
            }
        }

        /// <summary>
        /// Get the CANAPE root certificate, well attempt to create one if it doesn't exist
        /// </summary>
        /// <returns>The root certificate</returns>
        public static X509Certificate2 GetRootCert()
        {           
            X509Certificate2 ret = null;
            
            string configDirectory = GeneralUtils.GetConfigDirectory();

            try
            {
                if (configDirectory != null)
                {
                    string certPath = Path.Combine(configDirectory, "ca.crt");
                    string keyPath = Path.Combine(configDirectory, "ca.key");

                    if (!File.Exists(certPath) || !File.Exists(keyPath))
                    {
                        CreateRootCert();
                    }

                    ret = new X509Certificate2(certPath);

                    ret.PrivateKey = CertificateUtils.CreateRSAKeyFromXML(File.ReadAllText(keyPath), false);
                }
            }
            catch
            {
                ret = CertificateUtils.GenerateCACert("CN=BrokenCA_PleaseFix");
            }

            return ret;
        }

        /// <summary>
        /// Create a new root cert
        /// </summary>
        private static void CreateRootCert()
        {
            X509Certificate2 rootCert = CertificateUtils.GenerateCACert(RootCAName);
            
            string configDirectory = GeneralUtils.GetConfigDirectory();

            File.WriteAllBytes(Path.Combine(configDirectory, "ca.crt"), rootCert.Export(X509ContentType.Cert));
            File.WriteAllText(Path.Combine(configDirectory, "ca.key"), rootCert.PrivateKey.ToXmlString(true));
        }

        /// <summary>
        /// Set the CANAPE root CA
        /// </summary>
        /// <param name="rootCert">The root cert</param>
        public static void SetRootCert(X509Certificate2 rootCert)
        {
            string configDirectory = GeneralUtils.GetConfigDirectory();

            File.WriteAllBytes(Path.Combine(configDirectory, "ca.crt"), rootCert.Export(X509ContentType.Cert));
            File.WriteAllText(Path.Combine(configDirectory, "ca.key"), rootCert.PrivateKey.ToXmlString(true));
        }
    }
}
