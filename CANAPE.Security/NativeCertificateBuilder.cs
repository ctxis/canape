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
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CANAPE.Security.Cryptography.X509Certificates.Win32
{
    /// <summary>
    /// Class containing utility functions for certificates
    /// </summary>
    internal class NativeCertificateBuilder : ICertificateBuilder
    {
        #region Private Methods

        /// <summary>
        /// Convert a datetime to a FILETIME structure
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private System.Runtime.InteropServices.ComTypes.FILETIME DateTimeToFileTime(DateTime dt)
        {
            System.Runtime.InteropServices.ComTypes.FILETIME ft = new System.Runtime.InteropServices.ComTypes.FILETIME();
            byte[] convBytes = BitConverter.GetBytes(dt.ToFileTimeUtc());

            ft.dwLowDateTime = BitConverter.ToInt32(convBytes, 0);
            ft.dwHighDateTime = BitConverter.ToInt32(convBytes, 4);

            return ft;
        }

        /// <summary>
        /// Marshal array of extensions into one big buffer
        /// </summary>
        /// <param name="extensions">Array of extensions</param>
        /// <returns>An allocated buffer (from Marshal.AllocHGlobal) containing the data</returns>
        private IntPtr MarshalExtensions(X509Extension[] extensions)
        {
            int stringSize = extensions.Sum(x => Encoding.ASCII.GetByteCount(x.Oid.Value) + 1);
            int dataSize = extensions.Sum(x => x.RawData.Length);
            int structSize = Marshal.SizeOf(typeof(CryptoApiMethods.CERT_EXTENSION));

            IntPtr ret = IntPtr.Zero;
            IntPtr currStructPtr = IntPtr.Zero;
            IntPtr currDataPtr = IntPtr.Zero;
            IntPtr currStringPtr = IntPtr.Zero;
                                  
            ret = Marshal.AllocHGlobal((extensions.Length * structSize) + dataSize + stringSize);                        

            currStructPtr = ret;
            currDataPtr = ret + (extensions.Length * structSize);
            currStringPtr = currDataPtr + dataSize;

            foreach (var x in extensions)
            {
                byte[] oidString = Encoding.ASCII.GetBytes(x.Oid.Value + "\0");
                var certExt = new CryptoApiMethods.CERT_EXTENSION();
                certExt.pszObjId = currStringPtr;
                certExt.Value = new CryptoApiMethods.CRYPTOAPI_BLOB();
                certExt.Value.pbData = currDataPtr;
                certExt.Value.cbData = (uint)x.RawData.Length;
                certExt.fCritical = x.Critical;

                Marshal.StructureToPtr(certExt, currStructPtr, false);
                Marshal.Copy(oidString, 0, currStringPtr, oidString.Length);
                Marshal.Copy(x.RawData, 0, currDataPtr, x.RawData.Length);

                currStructPtr += structSize;
                currStringPtr += oidString.Length;
                currDataPtr += x.RawData.Length;
            }
            
            return ret;
        }

        /// <summary>
        /// Encode an extension
        /// </summary>
        /// <param name="oid">The OID for the extension</param>
        /// <param name="str">The structure to encode</param>
        /// <param name="critical">Whether the extension is critical</param>
        /// <returns></returns>
        private X509Extension EncodeExtension(string oid, object str, bool critical)
        {
            return new X509Extension(oid, EncodeObject(oid, str), critical);
        }

        /// <summary>
        /// Create an authority key info X509 extension
        /// </summary>
        /// <param name="serialNumber">Serial number of the info</param>
        /// <param name="issuer">Issuer subject name</param>
        /// <param name="key">RSA key</param>
        /// <returns>The constructed X509 extension</returns>
        private X509Extension CreateAuthorityKeyInfo2(byte[] serialNumber, X500DistinguishedName issuer, RSACryptoServiceProvider key)
        {
            CryptoApiMethods.CERT_AUTHORITY_KEY_ID2_INFO keyInfo = new CryptoApiMethods.CERT_AUTHORITY_KEY_ID2_INFO();
            CryptoApiMethods.CERT_ALT_NAME_DIRECTORY directoryName = new CryptoApiMethods.CERT_ALT_NAME_DIRECTORY();
            // CERT_ALT_NAME_DIRECTORY_NAME = 5
            X509Extension ret = null;

            try
            {
                keyInfo.AuthorityCertSerialNumber = new CryptoApiMethods.CRYPTOAPI_BLOB(serialNumber);
                directoryName.dwAltNameChoice = 5;
                directoryName.DirectoryName = new CryptoApiMethods.CRYPTOAPI_BLOB(issuer.RawData);
                keyInfo.AuthorityCertIssuer = new CryptoApiMethods.CERT_ALT_NAME_INFO();
                keyInfo.AuthorityCertIssuer.cAltEntry = 1;
                keyInfo.AuthorityCertIssuer.rgAltEntry = Marshal.AllocHGlobal(Marshal.SizeOf(directoryName));
                Marshal.StructureToPtr(directoryName, keyInfo.AuthorityCertIssuer.rgAltEntry, false);
                
                keyInfo.KeyId = new CryptoApiMethods.CRYPTOAPI_BLOB(HashPublicKeyInfo(key));

                ret = EncodeExtension(CryptoApiMethods.szOID_AUTHORITY_KEY_IDENTIFIER2, keyInfo, false);
            }
            finally
            {
                if (keyInfo.AuthorityCertSerialNumber != null)
                {
                    keyInfo.AuthorityCertSerialNumber.Release();
                }

                if (keyInfo.AuthorityCertIssuer.rgAltEntry != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(keyInfo.AuthorityCertIssuer.rgAltEntry);
                }

                if (directoryName.DirectoryName != null)
                {
                    directoryName.DirectoryName.Release();
                }

                if (keyInfo.KeyId != null)
                {
                    keyInfo.KeyId.Release();
                }
            }

            return ret;
        }

        /// <summary>
        /// Create an authority key info X509 extension
        /// </summary>
        /// <param name="serialNumber">Serial number of the info</param>
        /// <param name="issuer">Issuer subject name</param>
        /// <param name="key">RSA key</param>
        /// <returns>The constructed X509 extension</returns>
        private X509Extension CreateAuthorityKeyInfo(byte[] serialNumber, X500DistinguishedName issuer, RSACryptoServiceProvider key)
        {
            CryptoApiMethods.CERT_AUTHORITY_KEY_ID_INFO keyInfo = new CryptoApiMethods.CERT_AUTHORITY_KEY_ID_INFO();
            X509Extension ret = null;

            try
            {                
                keyInfo.CertSerialNumber = new CryptoApiMethods.CRYPTOAPI_BLOB(serialNumber);
                keyInfo.CertIssuer = new CryptoApiMethods.CRYPTOAPI_BLOB(issuer.RawData);             
                keyInfo.KeyId = new CryptoApiMethods.CRYPTOAPI_BLOB(HashPublicKeyInfo(key));

                ret = EncodeExtension(CryptoApiMethods.szOID_AUTHORITY_KEY_IDENTIFIER, keyInfo, false);                
            }
            finally
            {
                if (keyInfo.CertSerialNumber != null)
                {
                    keyInfo.CertSerialNumber.Release();
                }

                if (keyInfo.CertIssuer != null)
                {
                    keyInfo.CertIssuer.Release();
                }

                if (keyInfo.KeyId != null)
                {
                    keyInfo.KeyId.Release();
                }
            }

            return ret;
        }

        
        /// <summary>
        /// Open an existing RSA CSP
        /// </summary>
        /// <param name="providerName">Name of the provider</param>
        /// <param name="containerName">Name of container</param>
        /// <exception cref="Win32Exception"/>
        /// <returns>Handle to the crypt provider</returns>
        private CryptoApiMethods.SafeCryptProviderHandle OpenRSAProvider(string providerName, string containerName)
        {
            CryptoApiMethods.SafeCryptProviderHandle hCryptProv;

            if (!CryptoApiMethods.CryptAcquireContext(out hCryptProv, containerName, providerName, CryptoApiMethods.Providers.PROV_RSA_FULL, 0))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (hCryptProv == null)
            {
                throw new CryptographicException();
            }

            return hCryptProv;
        }

        /// <summary>
        /// Hash public key info of a specified RSA key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] HashPublicKeyInfo(RSACryptoServiceProvider key)
        {
            CryptoApiMethods.SafeCryptProviderHandle hProv = new CryptoApiMethods.SafeCryptProviderHandle();
            IntPtr publicKeyInfoPtr = IntPtr.Zero;
            byte[] ret = null;
            uint len = 0;

            try
            {
                publicKeyInfoPtr = ExportPublicKeyInfo(key);
                CryptoApiMethods.CERT_PUBLIC_KEY_INFO publicKeyInfo = (CryptoApiMethods.CERT_PUBLIC_KEY_INFO)Marshal.PtrToStructure(publicKeyInfoPtr, typeof(CryptoApiMethods.CERT_PUBLIC_KEY_INFO));

                hProv = OpenRSAProvider(key.CspKeyContainerInfo.ProviderName, key.CspKeyContainerInfo.KeyContainerName);

                if (!CryptoApiMethods.CryptHashPublicKeyInfo(hProv, 0, 0, CryptoApiMethods.CertEncoding.X509_ASN_ENCODING,
                    publicKeyInfo, null, out len))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                ret = new byte[len];

                if (!CryptoApiMethods.CryptHashPublicKeyInfo(hProv, 0, 0, CryptoApiMethods.CertEncoding.X509_ASN_ENCODING,
                    publicKeyInfo, ret, out len))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                if (!hProv.IsInvalid)
                {
                    hProv.Close();
                }

                if (publicKeyInfoPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publicKeyInfoPtr);
                }
            }

            return ret;
        }

        /// <summary>
        /// Create a new managed RSA key, persisted into a provider
        /// </summary>
        /// <param name="keySize">Size of key to create</param>
        /// <param name="signature">True to create a signature key, false for exchange</param>
        /// <returns>The new key</returns>
        private RSACryptoServiceProvider CreateRSAKey(int keySize, bool signature)
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

            return new RSACryptoServiceProvider(keySize, cspParams);
        }


         
        /// <summary>
        /// Encode an arbitary crypto object
        /// </summary>
        /// <param name="objName">OID for the encoding</param>
        /// <param name="obj">The object to encode</param>
        /// <exception cref="Win32Exception"/>
        /// <returns>The encoded object</returns>
        private byte[] EncodeObject(string objName, object obj)
        {
            byte[] ret = new byte[0];
            uint cbEncoded = 0;
            IntPtr pStruct = Marshal.AllocHGlobal(Marshal.SizeOf(obj));
            bool bCreatedStruct = false;

            try
            {                
                Marshal.StructureToPtr(obj, pStruct, false);
                bCreatedStruct = true;

                if (!CryptoApiMethods.CryptEncodeObject(CryptoApiMethods.CertEncoding.X509_ASN_ENCODING,
                    objName, pStruct, null, ref cbEncoded))
                {
                    int err = Marshal.GetLastWin32Error();

                    // If not the error for needing more data
                    if (err != 234)
                    {
                        throw new Win32Exception(err);
                    }
                }

                ret = new byte[cbEncoded];

                if (!CryptoApiMethods.CryptEncodeObject(CryptoApiMethods.CertEncoding.X509_ASN_ENCODING,
                        objName, pStruct, ret, ref cbEncoded))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                if (bCreatedStruct)
                {
                    Marshal.DestroyStructure(pStruct, obj.GetType());
                }

                if (pStruct != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pStruct);
                }
            }

            return ret;
        }

        /// <summary>
        /// Export the public RSA keyinfo from the current crypto provider
        /// </summary>
        /// <param name="hProv">Handle to the crypto provider</param>
        /// <param name="signature">Set to true for AT_SIGNATURE, false for AT_KEYEXCHANGE</param>
        /// <returns>An IntPtr pointing to the public key info structure</returns>
        private IntPtr ExportPublicKeyInfo(CryptoApiMethods.SafeCryptProviderHandle hProv, bool signature)
        {
            CryptoApiMethods.CALG type = signature ? CryptoApiMethods.CALG.AT_SIGNATURE : CryptoApiMethods.CALG.AT_KEYEXCHANGE;
            IntPtr ret = IntPtr.Zero;
            uint infoLen = 0;

            if (!CryptoApiMethods.CryptExportPublicKeyInfoEx(hProv, type, CryptoApiMethods.CertEncoding.X509_ASN_ENCODING,
                CryptoApiMethods.szOID_RSA_RSA, 0, IntPtr.Zero, ret, ref infoLen))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            ret = Marshal.AllocHGlobal((int)infoLen);

            if (!CryptoApiMethods.CryptExportPublicKeyInfoEx(hProv, type, CryptoApiMethods.CertEncoding.X509_ASN_ENCODING,
                CryptoApiMethods.szOID_RSA_RSA, 0, IntPtr.Zero, ret, ref infoLen))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return ret;
        }

        /// <summary>
        /// Export the public RSA keyinfo from the current crypto provider
        /// </summary>
        /// <param name="key">RSA Key</param>        
        /// <returns>An IntPtr pointing to the public key info structure</returns>
        private IntPtr ExportPublicKeyInfo(RSACryptoServiceProvider key)
        {
            IntPtr ret = IntPtr.Zero;
            CryptoApiMethods.SafeCryptProviderHandle hProv = null;

            try
            {
                hProv = OpenRSAProvider(key.CspKeyContainerInfo.ProviderName, key.CspKeyContainerInfo.KeyContainerName);

                ret = ExportPublicKeyInfo(hProv, key.CspKeyContainerInfo.KeyNumber == KeyNumber.Signature ? true : false);
            }
            finally
            {
                if (hProv != null)
                {
                    if (!hProv.IsInvalid)
                    {
                        hProv.Close();
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Encode and sign a CERT_INFO structure
        /// </summary>
        /// <param name="key">Key to sign the certificate with</param>
        /// <param name="certInfo">The CERT_INFO structure to sign</param>
        /// <param name="hashAlgorithm"></param>
        /// <returns></returns>
        private byte[] EncodeAndSignCertInfo(RSACryptoServiceProvider key, CryptoApiMethods.CERT_INFO certInfo, CertificateHashAlgorithm hashAlgorithm)
        {
            byte[] ret = null;
            CryptoApiMethods.SafeCryptProviderHandle hProv = new CryptoApiMethods.SafeCryptProviderHandle();

            try
            {
                hProv = OpenRSAProvider(key.CspKeyContainerInfo.ProviderName, key.CspKeyContainerInfo.KeyContainerName);

                ret = EncodeAndSignCertInfo(hProv, certInfo, key.CspKeyContainerInfo.KeyNumber == KeyNumber.Signature ? true : false, hashAlgorithm);
            }
            finally
            {
                if (!hProv.IsInvalid)
                {
                    hProv.Close();
                }
            }

            return ret;
        }

        /// <summary>
        /// Encode and sign a CERT_INFO structure
        /// </summary>
        /// <param name="hProv"></param>
        /// <param name="certInfo"></param>
        /// <param name="signature">True encodes with a AT_SIGNATURE key, otherwise AT_EXCHANGE</param>
        /// <param name="hashAlgorithm"></param>
        /// <returns></returns>
        private byte[] EncodeAndSignCertInfo(CryptoApiMethods.SafeCryptProviderHandle hProv, CryptoApiMethods.CERT_INFO certInfo, bool signature, CertificateHashAlgorithm hashAlgorithm)
        {
            CryptoApiMethods.CALG keyType = signature ? CryptoApiMethods.CALG.AT_SIGNATURE : CryptoApiMethods.CALG.AT_KEYEXCHANGE;
            byte[] ret = null;
            uint certLen = 0;
            IntPtr certInfoPtr = IntPtr.Zero;
            CryptoApiMethods.CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm = new CryptoApiMethods.CRYPT_ALGORITHM_IDENTIFIER();
            //SignatureAlgorithm.pszObjId = CryptoApiMethods.szOID_OIWSEC_sha1RSASign;
            //SignatureAlgorithm.pszObjId = CryptoApiMethods.szOID_RSA_SHA1RSA;
            //SignatureAlgorithm.pszObjId = CryptoApiMethods.szOID_RSA_SHA512RSA;
            SignatureAlgorithm.pszObjId = HashAlgorithmToOID(hashAlgorithm);

            try
            {
                certInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(certInfo));
                Marshal.StructureToPtr(certInfo, certInfoPtr, false);

                if (!CryptoApiMethods.CryptSignAndEncodeCertificate(hProv, keyType, CryptoApiMethods.CertEncoding.X509_ASN_ENCODING,
                    new IntPtr((int)CryptoApiMethods.StructType.X509_CERT_TO_BE_SIGNED), certInfoPtr, SignatureAlgorithm, IntPtr.Zero, null, ref certLen))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                ret = new byte[certLen];

                if (!CryptoApiMethods.CryptSignAndEncodeCertificate(hProv, keyType, CryptoApiMethods.CertEncoding.X509_ASN_ENCODING,
                    new IntPtr((int)CryptoApiMethods.StructType.X509_CERT_TO_BE_SIGNED), certInfoPtr, SignatureAlgorithm, IntPtr.Zero, ret, ref certLen))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                Marshal.DestroyStructure(certInfoPtr, certInfo.GetType());
                Marshal.FreeHGlobal(certInfoPtr);
            }

            return ret;
        }

        private static string HashAlgorithmToOID(CertificateHashAlgorithm hashAlgorithm)
        {
            switch (hashAlgorithm)
            {
                    
                case CertificateHashAlgorithm.Sha1:
                    return CryptoApiMethods.szOID_RSA_SHA1RSA;
                case CertificateHashAlgorithm.Sha256:
                    return CryptoApiMethods.szOID_RSA_SHA256RSA;
                case CertificateHashAlgorithm.Sha384:
                    return CryptoApiMethods.szOID_RSA_SHA384RSA;
                case CertificateHashAlgorithm.Sha512:
                    return CryptoApiMethods.szOID_RSA_SHA512RSA;
                case CertificateHashAlgorithm.Md5:
                    return CryptoApiMethods.szOID_RSA_MD5RSA;
                default:
                    throw new ArgumentException(Properties.Resources.NativeCertificateBuilder_InvalidHashAlgorithm, "hashAlgorithm");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a new certificate
        /// </summary>
        /// <param name="issuer">Issuer certificate, if null then self-sign</param>
        /// <param name="subjectName">Subject name</param>
        /// <param name="serialNumber">Serial number of certificate, if null then will generate a new one</param>
        /// <param name="signature">If true create an AT_SIGNATURE key, otherwise AT_EXCHANGE</param>
        /// <param name="keySize">Size of RSA key</param>
        /// <param name="notBefore">Start date of certificate</param>
        /// <param name="notAfter">End date of certificate</param>
        /// <param name="extensions">Array of extensions, if null then no extensions</param>
        /// <param name="hashAlgorithm">Specify the signature hash algorithm</param>
        /// <returns>The created X509 certificate</returns>
        public X509Certificate2 CreateCert(X509Certificate2 issuer, X500DistinguishedName subjectName, byte[] serialNumber, bool signature, int keySize, CertificateHashAlgorithm hashAlgorithm, DateTime notBefore, DateTime notAfter, X509ExtensionCollection extensions)
        {
            CryptoApiMethods.CERT_INFO certInfo = new CryptoApiMethods.CERT_INFO();
            RSACryptoServiceProvider key = CreateRSAKey(keySize, signature);
            IntPtr publicKeyInfoPtr = IntPtr.Zero;            
            X509Certificate2 cert = null;
            List<X509Extension> newExts = null;

            if (extensions != null)
            {
                foreach (X509Extension ext in extensions)
                {
                    if (ext.RawData == null)
                    {
                        throw new ArgumentException(Properties.Resources.CreateCert_NeedEncodedData);
                    }
                }
            }

            try
            {                
                if (serialNumber == null)
                {
                    serialNumber = Guid.NewGuid().ToByteArray();                    
                }

                certInfo.dwVersion = (uint)CryptoApiMethods.CertVersion.CERT_V3;
                certInfo.SerialNumber = new CryptoApiMethods.CRYPTOAPI_BLOB(serialNumber);
                certInfo.Subject = new CryptoApiMethods.CRYPTOAPI_BLOB(subjectName.RawData);

                if (issuer == null)
                {
                    // Self-signed
                    certInfo.Issuer = new CryptoApiMethods.CRYPTOAPI_BLOB(subjectName.RawData);
                }
                else
                {
                    certInfo.Issuer = new CryptoApiMethods.CRYPTOAPI_BLOB(issuer.SubjectName.RawData);
                }

                // Never seems to need these set to anything valid?
                certInfo.SubjectUniqueId = new CryptoApiMethods.CRYPT_BIT_BLOB();
                certInfo.IssuerUniqueId = new CryptoApiMethods.CRYPT_BIT_BLOB();
                
                certInfo.NotBefore = DateTimeToFileTime(notBefore);
                certInfo.NotAfter = DateTimeToFileTime(notAfter);

                certInfo.SignatureAlgorithm = new CryptoApiMethods.CRYPT_ALGORITHM_IDENTIFIER();
                // Doesn't seem to work properly with standard szOID_RSA_SHA1RSA
                //certInfo.SignatureAlgorithm.pszObjId = CryptoApiMethods.szOID_OIWSEC_sha1RSASign;
                //certInfo.SignatureAlgorithm.pszObjId = CryptoApiMethods.szOID_RSA_SHA1RSA;
                //certInfo.SignatureAlgorithm.pszObjId = CryptoApiMethods.szOID_RSA_SHA512RSA;
                certInfo.SignatureAlgorithm.pszObjId = HashAlgorithmToOID(hashAlgorithm);

                // Add extension fields
                publicKeyInfoPtr = ExportPublicKeyInfo(key);
                certInfo.SubjectPublicKeyInfo = (CryptoApiMethods.CERT_PUBLIC_KEY_INFO)Marshal.PtrToStructure(publicKeyInfoPtr, typeof(CryptoApiMethods.CERT_PUBLIC_KEY_INFO));

                newExts = new List<X509Extension>();

                if (extensions != null)
                {
                    // Filter out some extensions we don't want
                    newExts.AddRange(                        
                        extensions.Cast<X509Extension>().Where(
                        x =>
                           !x.Oid.Value.Equals(CryptoApiMethods.szOID_AUTHORITY_KEY_IDENTIFIER)
                        && !x.Oid.Value.Equals(CryptoApiMethods.szOID_SUBJECT_KEY_IDENTIFIER)
                        && !x.Oid.Value.Equals(CryptoApiMethods.szOID_AUTHORITY_KEY_IDENTIFIER2)));
                }                

                if (issuer != null)
                {
                    newExts.Add(CreateAuthorityKeyInfo2(issuer.GetSerialNumber(), issuer.SubjectName, (RSACryptoServiceProvider)issuer.PrivateKey));
                }
                else
                {
                    newExts.Add(CreateAuthorityKeyInfo2(serialNumber, subjectName, key));
                }

                newExts.Add(new X509SubjectKeyIdentifierExtension(HashPublicKeyInfo(key), false));

                certInfo.rgExtension = MarshalExtensions(newExts.ToArray());
                certInfo.cExtension = (uint)newExts.Count;

                byte[] certData = EncodeAndSignCertInfo(issuer != null ? issuer.PrivateKey as RSACryptoServiceProvider : key, certInfo, hashAlgorithm);

                cert = new X509Certificate2(certData, (string)null, X509KeyStorageFlags.Exportable);
                cert.PrivateKey = key;
            }
            finally
            {
                if (certInfo.rgExtension != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(certInfo.rgExtension);
                }

                if (certInfo.Subject != null)
                {
                    certInfo.Subject.Release();
                }

                if (certInfo.Issuer != null)
                {
                    certInfo.Issuer.Release();
                }

                if (publicKeyInfoPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(publicKeyInfoPtr);
                }
            }

            return cert;
        }


        #endregion 
    }
}
