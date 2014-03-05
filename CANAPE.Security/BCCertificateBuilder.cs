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
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.X509;
using SystemX509 = System.Security.Cryptography.X509Certificates;

namespace CANAPE.Security.Cryptography.X509Certificates.BouncyCastle
{
    internal class BCCertificateBuilder : ICertificateBuilder
    {

        /// <summary>
        /// Create a new managed RSA key, persisted into a provider
        /// </summary>
        /// <param name="keySize">Size of key to create</param>
        /// <param name="signature">True to create a signature key, false for exchange</param>
        /// <returns>The new key</returns>
        private static RSACryptoServiceProvider CreateRSAKey(int keySize, bool signature)
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
        /// Returns the RSA Key pair for a RSA CryptoServiceProvider
        /// Borrowed this small sample from .NET Crypto Extensions
        /// </summary>
        /// <param name="rsa">RSA CSP</param>
        /// <returns>RSA key pair</returns>
        private static AsymmetricCipherKeyPair GetRsaKeyPair(
            RSACryptoServiceProvider rsa)
        {
            RSAParameters rp = rsa.ExportParameters(true);
            BigInteger modulus = new BigInteger(1, rp.Modulus);
            BigInteger pubExp = new BigInteger(1, rp.Exponent);

            RsaKeyParameters pubKey = new RsaKeyParameters(
                false,
                modulus,
                pubExp);

            RsaPrivateCrtKeyParameters privKey = new RsaPrivateCrtKeyParameters(
                modulus,
                pubExp,
                new BigInteger(1, rp.D),
                new BigInteger(1, rp.P),
                new BigInteger(1, rp.Q),
                new BigInteger(1, rp.DP),
                new BigInteger(1, rp.DQ),
                new BigInteger(1, rp.InverseQ));

            return new AsymmetricCipherKeyPair(pubKey, privKey);
        }

        private const string szOID_AUTHORITY_KEY_IDENTIFIER2 = "2.5.29.35";


        private static string HashAlgorithmToName(CertificateHashAlgorithm hashAlgorithm)
        {
            switch (hashAlgorithm)
            {

                case CertificateHashAlgorithm.Sha1:
                    return "SHA1WITHRSA";
                case CertificateHashAlgorithm.Sha256:
                    return "SHA256WITHRSA";
                case CertificateHashAlgorithm.Sha384:
                    return "SHA384WITHRSA";
                case CertificateHashAlgorithm.Sha512:
                    return "SHA512WITHRSA";
                case CertificateHashAlgorithm.Md5:
                    return "MD5WITHRSA";
                default:
                    throw new ArgumentException(Properties.Resources.NativeCertificateBuilder_InvalidHashAlgorithm, "hashAlgorithm");
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
        /// <param name="notBefore">Start date of certificate</param>
        /// <param name="notAfter">End date of certificate</param>
        /// <param name="extensions">Array of extensions, if null then no extensions</param>
        /// <param name="hashAlgorithm">Specify the signature hash algorithm</param>
        /// <returns>The created X509 certificate</returns>
        public SystemX509.X509Certificate2 CreateCert(SystemX509.X509Certificate2 issuer, SystemX509.X500DistinguishedName subjectName,
            byte[] serialNumber, bool signature, int keySize, CertificateHashAlgorithm hashAlgorithm, DateTime notBefore,
            DateTime notAfter, SystemX509.X509ExtensionCollection extensions)
        {            
            X509V3CertificateGenerator builder = new X509V3CertificateGenerator();
            AsymmetricAlgorithm subjectKey = CreateRSAKey(keySize, signature);
            AsymmetricAlgorithm signKey = issuer == null ? subjectKey : issuer.PrivateKey;

            if (signKey == null)
            {
                throw new ArgumentException(Properties.Resources.CreateCert_NoPrivateKey);
            }

            AsymmetricCipherKeyPair bcSubjectKey = GetRsaKeyPair((RSACryptoServiceProvider)subjectKey);
            AsymmetricCipherKeyPair bcSignKey = GetRsaKeyPair((RSACryptoServiceProvider)signKey);
            
            X509Name issuerNameObj = issuer == null ? X509Name.GetInstance(Asn1Object.FromByteArray(subjectName.RawData)) 
                : X509Name.GetInstance(Asn1Object.FromByteArray(issuer.SubjectName.RawData));            
            X509Name subjectNameObj = X509Name.GetInstance(Asn1Object.FromByteArray(subjectName.RawData));

            BigInteger subjectSerial = new BigInteger(1, serialNumber != null ? serialNumber : Guid.NewGuid().ToByteArray());
            BigInteger issuerSerial = issuer == null ? subjectSerial : new BigInteger(1, issuer.GetSerialNumber());
                       
            builder.SetIssuerDN(issuerNameObj);
            builder.SetSubjectDN(subjectNameObj);
            builder.SetSerialNumber(subjectSerial);
            builder.SetSignatureAlgorithm(HashAlgorithmToName(hashAlgorithm));
            builder.SetNotBefore(notBefore.ToUniversalTime());
            builder.SetNotAfter(notAfter.ToUniversalTime());
            builder.SetPublicKey(bcSubjectKey.Public); 

            SubjectPublicKeyInfo info = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(bcSignKey.Public);
            AuthorityKeyIdentifier authKeyId = new AuthorityKeyIdentifier(info, new GeneralNames(new GeneralName(issuerNameObj)), issuerSerial);            
            SubjectKeyIdentifier subjectKeyid = new SubjectKeyIdentifier(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(bcSubjectKey.Public));

            builder.AddExtension(X509Extensions.AuthorityKeyIdentifier.Id, true, authKeyId);
            builder.AddExtension(X509Extensions.SubjectKeyIdentifier.Id, true, subjectKeyid);

            if (extensions != null)
            {
                foreach (SystemX509.X509Extension ext in extensions)
                {
                    if (!ext.Oid.Value.Equals(X509Extensions.AuthorityKeyIdentifier.Id)
                        && !ext.Oid.Value.Equals(X509Extensions.SubjectKeyIdentifier.Id)
                        && !ext.Oid.Value.Equals(szOID_AUTHORITY_KEY_IDENTIFIER2))
                    {
                        Asn1InputStream istm = new Org.BouncyCastle.Asn1.Asn1InputStream(ext.RawData);
                        Asn1Object obj = istm.ReadObject();
                        builder.AddExtension(ext.Oid.Value, ext.Critical, obj);
                    }
                }
            }

            X509Certificate cert = builder.Generate(bcSignKey.Private);

            SystemX509.X509Certificate2 ret = new SystemX509.X509Certificate2(cert.GetEncoded(), (string)null, SystemX509.X509KeyStorageFlags.Exportable);

            ret.PrivateKey = subjectKey;

            return ret;
        }
    }
}
