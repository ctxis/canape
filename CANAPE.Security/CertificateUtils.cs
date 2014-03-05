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
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using CANAPE.Security.Cryptography.X509Certificates.BouncyCastle;
using CANAPE.Security.Cryptography.X509Certificates.Win32;
using CANAPE.Utils;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using X509 = Org.BouncyCastle.X509;
using Org.BouncyCastle.Security.Certificates;
using System.Security;
using Org.BouncyCastle.Math;
using System.Runtime.InteropServices;

namespace CANAPE.Security.Cryptography.X509Certificates
{
    /// <summary>
    /// Static class containing utility functions for certificates, wraps the platform specific form (CryptAPI on Windows, mono specific)
    /// </summary>
    public static class CertificateUtils
    {
        private const string szOID_CRL_DISTRIBUTION = "2.5.29.31";
        private const string szOID_AUTHORITY_INFO = "1.3.6.1.5.5.7.1.1";
        private static ICertificateBuilder builder;

        static CertificateUtils()
        {
            if (!GeneralUtils.IsRunningOnMono())
            {
                // We use the native builder on windows just because
                builder = new NativeCertificateBuilder();
            }
            else
            {
                builder = new BCCertificateBuilder();
            }
        }

        /// <summary>
        /// Generate a self signed certificate including a private key
        /// </summary>
        /// <param name="subject">The X500 subject string</param>
        /// <param name="signature">True create a cert with an AT_SIGNATURE key, otherwise AT_EXCHANGE</param>
        /// <returns>An X509Certificate2 object containing the full certificate</returns>
        public static X509Certificate2 GenerateSelfSignedCert(string subject, bool signature)
        {
            return GenerateSelfSignedCert(subject, signature, 1024, CertificateHashAlgorithm.Sha1);
        }

        /// <summary>
        /// Generate a self signed certificate including a private key
        /// </summary>
        /// <param name="subject">The X500 subject string</param>
        /// <param name="signature">True create a cert with an AT_SIGNATURE key, otherwise AT_EXCHANGE</param>
        /// <param name="rsaKeySize">Specify the RSA key size in bits</param>
        /// <param name="hashAlgorithm">Specify the signature hash algorithm</param>
        /// <returns>An X509Certificate2 object containing the full certificate</returns>
        public static X509Certificate2 GenerateSelfSignedCert(string subject, bool signature, int rsaKeySize, CertificateHashAlgorithm hashAlgorithm)
        {
            DateTime dt = DateTime.Now;

            return builder.CreateCert(null, new X500DistinguishedName(subject), null, signature, rsaKeySize, hashAlgorithm, dt, dt.AddYears(10), null);
        }

        /// <summary>
        /// Export a certificate to a PEM format string
        /// </summary>
        /// <param name="cert">The certificate to export</param>
        /// <returns>A PEM encoded string</returns>
        public static string ExportToPEM(X509Certificate cert)
        {
            StringWriter swriter = new StringWriter();
            PemWriter writer = new PemWriter(swriter);

            writer.WriteObject(new X509.X509CertificateParser().ReadCertificate(cert.Export(X509ContentType.Cert)));

            return swriter.ToString();
        }

        // Not a great idea but not much choice
        private static char[] SecureStringToCharArray(SecureString str)
        {
            IntPtr bstrString = Marshal.SecureStringToBSTR(str);

            try
            {
                char[] ret = new char[str.Length];

                Marshal.Copy(bstrString, ret, 0, str.Length);

                return ret;
            }
            finally
            {
                Marshal.ZeroFreeBSTR(bstrString);
            }
        }

        /// <summary>
        /// Export an RSA key to a PEM format string
        /// </summary>
        /// <param name="rsa">The RSA key to export (must be exportable)</param>
        /// <param name="password">An optional password</param>
        /// <returns>A PEM string form of the key</returns>
        public static string ExportToPEM(RSA rsa, SecureString password)
        {
            StringWriter swriter = new StringWriter();
            PemWriter writer = new PemWriter(swriter);

            RSAParameters ps = rsa.ExportParameters(true);

            BigInteger modulus = new BigInteger(1, ps.Modulus);
            BigInteger exp = new BigInteger(1, ps.Exponent);
            BigInteger d = new BigInteger(1, ps.D);
            BigInteger p = new BigInteger(1, ps.P);
            BigInteger q = new BigInteger(1, ps.Q);
            BigInteger dp = new BigInteger(1, ps.DP);
            BigInteger dq = new BigInteger(1, ps.DQ);
            BigInteger qinv = new BigInteger(1, ps.InverseQ);

            RsaPrivateCrtKeyParameters privKey = new RsaPrivateCrtKeyParameters(modulus, exp, d, p, q, dp, dq, qinv);

            if (password != null)
            {
                writer.WriteObject(privKey, "DES-EDE3-CBC",
                    SecureStringToCharArray(password), new Org.BouncyCastle.Security.SecureRandom());
            }
            else
            {
                writer.WriteObject(privKey);
            }

            return swriter.ToString();
        }

        private static RSACryptoServiceProvider CreateRSAProvider(bool signature)
        {
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = Guid.NewGuid().ToString();
            // Must provide these details otherwise the generated RSA key will not work very well (if at all)
            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
            cspParams.ProviderType = 1;

            cspParams.Flags = CspProviderFlags.NoPrompt;
            if (signature)
            {
                cspParams.KeyNumber = (int)KeyNumber.Signature;
            }
            else
            {
                cspParams.KeyNumber = (int)KeyNumber.Exchange;
            }

            return new RSACryptoServiceProvider(cspParams);
        }

        /// <summary>
        /// Create an RSA key from XML parameters
        /// </summary>
        /// <param name="xml">The XML format</param>
        /// <param name="signature">True to create a signature key, false for exchange</param>
        /// <returns>The created provider</returns>
        public static RSACryptoServiceProvider CreateRSAKeyFromXML(string xml, bool signature)
        {            
            RSACryptoServiceProvider rsa = CreateRSAProvider(signature);

            rsa.FromXmlString(xml);

            return rsa;
        }

        /// <summary>
        /// Take an existing certificate, clone its details and resign with a new root CA
        /// </summary>
        /// <param name="toClone">The certificate to clone</param>
        /// <param name="rootCert">The root CA certificate to sign with</param>
        /// <param name="newSerial">True to generate a new serial for this certificate</param>
        /// <param name="rsaKeySize">The size of the RSA key to generate</param>
        /// <param name="hashAlgorithm">Specify the signature hash algorithm</param>
        /// <returns></returns>
        public static X509Certificate2 CloneAndSignCertificate(X509Certificate toClone, X509Certificate2 rootCert, bool newSerial, int rsaKeySize, CertificateHashAlgorithm hashAlgorithm)
        {            
            X509Certificate2 cert2 = new X509Certificate2(toClone);
            X509ExtensionCollection extensions = new X509ExtensionCollection();

            foreach (var ext in cert2.Extensions)
            {
                // Remove CRL distribution locations and authority information, they tend to break SSL negotiation
                if ((ext.Oid.Value != szOID_CRL_DISTRIBUTION) && (ext.Oid.Value != szOID_AUTHORITY_INFO))
                {
                    extensions.Add(ext);
                }
            }

            return builder.CreateCert(rootCert, cert2.SubjectName, newSerial ? null : cert2.GetSerialNumber(),              
                false, rsaKeySize, hashAlgorithm, cert2.NotBefore, cert2.NotAfter, extensions);
        }

        /// <summary>
        /// Take an existing certificate, clone its details and resign with a new root CA
        /// </summary>
        /// <param name="toClone">The certificate to clone</param>
        /// <param name="rootCert">The root CA certificate to sign with</param>
        /// <param name="newSerial">True to generate a new serial for this certificate</param>        
        /// <returns></returns>
        public static X509Certificate2 CloneAndSignCertificate(X509Certificate toClone, X509Certificate2 rootCert, bool newSerial)
        {
            return CloneAndSignCertificate(toClone, rootCert, newSerial, 1024, CertificateHashAlgorithm.Sha1);
        }

        /// <summary>
        /// Generate a self-signed CA certificate
        /// </summary>
        /// <param name="subject">The X500 subject string</param>
        /// <param name="rsaKeySize">The size of the RSA key to generate</param>
        /// <param name="hashAlgorithm">Specify the signature hash algorithm</param>
        /// <returns>An X509Certificate2 object containing the full certificate</returns>
        public static X509Certificate2 GenerateCACert(string subject, int rsaKeySize, CertificateHashAlgorithm hashAlgorithm)
        {            
            X509ExtensionCollection exts = new X509ExtensionCollection();
            DateTime dt = DateTime.Now.AddYears(-1);

            exts.Add(new X509BasicConstraintsExtension(true, false, 0, false));

            return builder.CreateCert(null, new X500DistinguishedName(subject), null, false, rsaKeySize, hashAlgorithm, dt, dt.AddYears(10), exts);
        }

        /// <summary>
        /// Generate a self-signed CA certificate
        /// </summary>
        /// <param name="subject">The X500 subject string</param>        
        /// <returns>An X509Certificate2 object containing the full certificate</returns>
        public static X509Certificate2 GenerateCACert(string subject)
        {
            return GenerateCACert(subject, 1024, CertificateHashAlgorithm.Sha1);
        }

        /// <summary>
        /// Check if a certificate key is protected
        /// </summary>
        /// <param name="cert">The certificate</param>
        /// <returns>True if protected, otherwise false</returns>
        public static bool IsProtectedPrivateKey(X509Certificate2 cert)
        {
            if (!cert.HasPrivateKey)
            {
                return false;
            }

            RSACryptoServiceProvider rsa = cert.PrivateKey as RSACryptoServiceProvider;
            if (rsa == null)
            {
                return false;
            }

            return rsa.CspKeyContainerInfo.Protected;
        }

        /// <summary>
        /// Reload certificate private key if protected with a password
        /// </summary>
        /// <param name="cert">The certificate to reload</param>
        /// <param name="password">The secure password</param>
        public static void ReloadProtectedPrivateKey(X509Certificate2 cert, SecureString password)
        {
            if(!cert.HasPrivateKey)
            {
                throw new CryptographicException(Properties.Resources.ReloadProtectedPrivateKey_NotPrivateKeyInCert);
            }

            RSACryptoServiceProvider rsa = cert.PrivateKey as RSACryptoServiceProvider;

            if (rsa == null)
            {
                throw new CryptographicException(Properties.Resources.ReloadProtectedPrivateKey_OnlySupportRSA);
            }

            if (rsa.CspKeyContainerInfo.Protected)
            {
                CspParameters csp = new CspParameters();
                csp.KeyPassword = password;
                csp.ProviderName = rsa.CspKeyContainerInfo.ProviderName;
                csp.ProviderType = rsa.CspKeyContainerInfo.ProviderType;
                csp.KeyContainerName = rsa.CspKeyContainerInfo.KeyContainerName;
                csp.KeyNumber = (int)rsa.CspKeyContainerInfo.KeyNumber;

                RSACryptoServiceProvider newKey = new RSACryptoServiceProvider(csp);

                cert.PrivateKey = newKey;

                rsa.Clear();
            }

        }

        /// <summary>
        /// Create a new certificate
        /// </summary>
        /// <param name="issuer">Issuer certificate, if null then self-sign</param>
        /// <param name="subjectName">Subject name</param>
        /// <param name="serialNumber">Serial number of certificate, if null then will generate a new one</param>
        /// <param name="signature">If true create an AT_SIGNATURE key, otherwise AT_EXCHANGE</param>
        /// <param name="keySize">Size of RSA key</param>
        /// <param name="hashAlgorithm">The hash algorithm for the certificate</param>
        /// <param name="notBefore">Start date of certificate</param>
        /// <param name="notAfter">End date of certificate</param>
        /// <param name="extensions">Array of extensions, if null then no extensions</param>
        /// <returns>The created X509 certificate</returns>
        public static X509Certificate2 CreateCert(X509Certificate2 issuer, X500DistinguishedName subjectName, 
            byte[] serialNumber, bool signature, int keySize, CertificateHashAlgorithm hashAlgorithm, DateTime notBefore, DateTime notAfter, X509ExtensionCollection extensions)
        {           
            return builder.CreateCert(issuer, subjectName, serialNumber, signature, keySize, hashAlgorithm, notBefore, notAfter, extensions);
        }

        private static X509Certificate2 FindCert(StoreName storeName, StoreLocation storeLocation,X509FindType findType, object findObject)
        {
            X509Certificate2 ret = null;

            try
            {
                X509Store store = new X509Store(storeName, storeLocation);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(findType, findObject, false);

                if (fcollection.Count > 0)
                {
                    ret = fcollection[0];
                }

                store.Close();
            }
            catch (CryptographicException)
            {
            }

            return ret;
        }

        /// <summary>
        /// Find a certificate from the certificate store matching the basic one passed
        /// </summary>
        /// <param name="storeName">The store to search</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="thumbprint">The thumbprint of the certificate in string format</param>
        /// <returns>The found certificate, or null if not found</returns>
        public static X509Certificate2 FindCertByThumbprint(StoreName storeName, StoreLocation storeLocation, string thumbprint)
        {
            return FindCert(storeName, storeLocation, X509FindType.FindByThumbprint, thumbprint);
        }

        /// <summary>
        /// Find a certificate from the certificate store with a matching subject name
        /// </summary>
        /// <param name="storeName">The store to search</param>
        /// <param name="storeLocation">The store location</param>
        /// <param name="subject">The subject name to find</param>
        /// <returns>The found certificate, or null if not found</returns>        
        public static X509Certificate2 FindCertBySubject(StoreName storeName, StoreLocation storeLocation, string subject)
        {
            return FindCert(storeName, storeLocation, X509FindType.FindBySubjectDistinguishedName, subject);
        }

        class PasswordFinderClass : IPasswordFinder
        {
            char[] _password;

            public PasswordFinderClass(string password)
            {
                _password = password.ToCharArray();
            }

            public char[] GetPassword()
            {
                return _password;
            }
        }

        /// <summary>
        /// Load a certificate from an OpenSSL style file (PEM or DER)
        /// </summary>
        /// <param name="file">The file to load</param>
        /// <returns>The certificate, null if the file wasn't a certificate</returns>
        public static X509Certificate2 LoadCertFromOpenSslFile(string file)
        {
            X509Certificate2 ret = null;

            try
            {                
                using (Stream stm = File.OpenRead(file))
                {
                    X509.X509CertificateParser parser = new X509.X509CertificateParser();
                    X509.X509Certificate cert = parser.ReadCertificate(stm);

                    if (cert != null)
                    {
                        ret = new X509Certificate2(cert.GetEncoded(), (string)null, X509KeyStorageFlags.Exportable);
                    }
                    else
                    {
                        throw new CryptographicException(Properties.Resources.LoadCertFromOpenSslFile_InvalidOpenSslCert);
                    }
                }
            }
            catch (CertificateException ex)
            {
                throw new CryptographicException(ex.Message, ex);
            }

            return ret;
        }

        /// <summary>
        /// Load a certificate from a PFX file with an optional password
        /// </summary>
        /// <param name="file">The file to load</param>
        /// <param name="password">Password for private key, null for no password</param>
        /// <returns>The certificate</returns>
        public static X509Certificate2 LoadCertFromPfxFile(string file, string password)
        {
            X509Certificate2 ret = null;

            if (password == null)
            {
                ret = new X509Certificate2(File.ReadAllBytes(file), (string)null, X509KeyStorageFlags.Exportable);
            }
            else
            {
                ret = new X509Certificate2(File.ReadAllBytes(file), password, X509KeyStorageFlags.Exportable);
            }

            return ret;
        }

        private static RSA LoadKey(RsaPrivateCrtKeyParameters keyParams, bool signature)
        {
            RSAParameters rp = new RSAParameters();
            rp.Modulus = keyParams.Modulus.ToByteArrayUnsigned();
            rp.Exponent = keyParams.PublicExponent.ToByteArrayUnsigned();
            rp.D = keyParams.Exponent.ToByteArrayUnsigned();
            rp.P = keyParams.P.ToByteArrayUnsigned();
            rp.Q = keyParams.Q.ToByteArrayUnsigned();
            rp.DP = keyParams.DP.ToByteArrayUnsigned();
            rp.DQ = keyParams.DQ.ToByteArrayUnsigned();
            rp.InverseQ = keyParams.QInv.ToByteArrayUnsigned();
            
            RSACryptoServiceProvider rsa = CreateRSAProvider(signature);

            rsa.ImportParameters(rp);

            return rsa;
        }

        /// <summary>
        /// Load an RSA private key from a OpenSSL style file (DER or PEM)
        /// </summary>
        /// <param name="keyFile">The file to load</param>
        /// <param name="password">The optional password</param>
        /// <param name="signature">Whether to load  the key as a signature or key exchange</param>
        /// <returns>The RSA algorithm object</returns>
        public static RSA LoadRsaKeyFromFile(string keyFile, string password, bool signature)
        {
            RSA ret = null;

            byte[] data = File.ReadAllBytes(keyFile);
            RsaPrivateCrtKeyParameters keyParams = null;

            if (data.Length > 0)
            {
                if (data[0] != 0x30)
                {
                    try
                    {
                        // Probably a PEM file
                        using (StreamReader reader = new StreamReader(new MemoryStream(data)))
                        {
                            PemReader pem;

                            if (password != null)
                            {
                                pem = new PemReader(reader, new PasswordFinderClass(password));
                            }
                            else
                            {
                                pem = new PemReader(reader);
                            }

                            object o = pem.ReadObject();

                            while (o != null)
                            {
                                if (o is RsaPrivateCrtKeyParameters)
                                {
                                    keyParams = o as RsaPrivateCrtKeyParameters;
                                    break;
                                }
                                else if (o is AsymmetricCipherKeyPair)
                                {
                                    AsymmetricCipherKeyPair pair = o as AsymmetricCipherKeyPair;

                                    keyParams = pair.Private as RsaPrivateCrtKeyParameters;
                                    break;
                                }

                                o = pem.ReadObject();
                            }
                        }
                    }
                    catch (InvalidCipherTextException)
                    {
                        // Catch this exception and convert to standard CryptographicException
                        throw new CryptographicException(Properties.Resources.LoadRsaKeyFromFile_CannotDecryptKey);
                    }
                }
                else
                {
                    Asn1Sequence seq = (Asn1Sequence)Asn1Object.FromByteArray(data);

                    if (seq.Count != 9)
                    {
                        throw new CryptographicException(Properties.Resources.LoadRsaKeyFromFile_MalformedSequence);
                    }

                    RsaPrivateKeyStructure rsa = new RsaPrivateKeyStructure(seq);

                    keyParams = new RsaPrivateCrtKeyParameters(
                        rsa.Modulus, rsa.PublicExponent, rsa.PrivateExponent,
                        rsa.Prime1, rsa.Prime2, rsa.Exponent1, rsa.Exponent2,
                        rsa.Coefficient);
                }

                if (keyParams != null)
                {
                    ret = LoadKey(keyParams, signature);
                }
            }

            return ret;
        }
    }
}
