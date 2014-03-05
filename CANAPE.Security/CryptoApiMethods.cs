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
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace CANAPE.Security.Cryptography.X509Certificates.Win32
{
    internal static class CryptoApiMethods
    {
        [Flags]
        public enum CryptFlags : uint
        {
            CRYPT_NEWKEYSET = 0x8,
            CRYPT_DELETEKEYSET = 0x10,
            CRYPT_MACHINE_KEYSET = 0x20,
            CRYPT_SILENT = 0x40,
            CRYPT_DEFAULT_CONTAINER_OPTIONAL = 0x80,
            CRYPT_VERIFYCONTEXT = 0xF0000000,
        }

        public enum Providers
        {
            PROV_RSA_FULL = 1,
            PROV_RSA_SIG = 2,
            PROV_DSS = 3,
            PROV_FORTEZZA = 4,
            PROV_MS_EXCHANGE = 5,
            PROV_SSL = 6,
            PROV_RSA_SCHANNEL = 12,
            PROV_DSS_DH = 13,
            PROV_EC_ECDSA_SIG = 14,
            PROV_EC_ECNRA_SIG = 15,
            PROV_EC_ECDSA_FULL = 16,
            PROV_EC_ECNRA_FULL = 17,
            PROV_DH_SCHANNEL = 18,
            PROV_SPYRUS_LYNKS = 20,
            PROV_RNG = 21,
            PROV_INTEL_SEC = 22,
            PROV_REPLACE_OWF = 23,
            PROV_RSA_AES = 24,
        }

        // Leave this as private constants, I don't think they are needed outside the class
        const uint ALG_CLASS_ANY = (0);
        const uint ALG_CLASS_SIGNATURE = (1 << 13);
        const uint ALG_CLASS_MSG_ENCRYPT = (2 << 13);
        const uint ALG_CLASS_DATA_ENCRYPT = (3 << 13);
        const uint ALG_CLASS_HASH = (4 << 13);
        const uint ALG_CLASS_KEY_EXCHANGE = (5 << 13);
        const uint ALG_CLASS_ALL = (7 << 13);
        const uint ALG_TYPE_ANY = (0);
        const uint ALG_TYPE_DSS = (1 << 9);
        const uint ALG_TYPE_RSA = (2 << 9);
        const uint ALG_TYPE_BLOCK = (3 << 9);
        const uint ALG_TYPE_STREAM = (4 << 9);
        const uint ALG_TYPE_DH = (5 << 9);
        const uint ALG_TYPE_SECURECHANNEL = (6 << 9);
        const uint ALG_SID_ANY = (0);
        const uint ALG_SID_RSA_ANY = 0;
        const uint ALG_SID_RSA_PKCS = 1;
        const uint ALG_SID_RSA_MSATWORK = 2;
        const uint ALG_SID_RSA_ENTRUST = 3;
        const uint ALG_SID_RSA_PGP = 4;
        const uint ALG_SID_DSS_ANY = 0;
        const uint ALG_SID_DSS_PKCS = 1;
        const uint ALG_SID_DSS_DMS = 2;
        const uint ALG_SID_ECDSA = 3;
        const uint ALG_SID_DES = 1;
        const uint ALG_SID_3DES = 3;
        const uint ALG_SID_DESX = 4;
        const uint ALG_SID_IDEA = 5;
        const uint ALG_SID_CAST = 6;
        const uint ALG_SID_SAFERSK64 = 7;
        const uint ALG_SID_SAFERSK128 = 8;
        const uint ALG_SID_3DES_112 = 9;
        const uint ALG_SID_CYLINK_MEK = 12;
        const uint ALG_SID_RC5 = 13;
        const uint ALG_SID_AES_128 = 14;
        const uint ALG_SID_AES_192 = 15;
        const uint ALG_SID_AES_256 = 16;
        const uint ALG_SID_AES = 17;
        const uint ALG_SID_SKIPJACK = 10;
        const uint ALG_SID_TEK = 11;
        const uint CRYPT_MODE_CBCI = 6;
        const uint CRYPT_MODE_CFBP = 7;
        const uint CRYPT_MODE_OFBP = 8;
        const uint CRYPT_MODE_CBCOFM = 9;
        const uint CRYPT_MODE_CBCOFMI = 10;
        const uint ALG_SID_RC2 = 2;
        const uint ALG_SID_RC4 = 1;
        const uint ALG_SID_SEAL = 2;
        const uint ALG_SID_DH_SANDF = 1;
        const uint ALG_SID_DH_EPHEM = 2;
        const uint ALG_SID_AGREED_KEY_ANY = 3;
        const uint ALG_SID_KEA = 4;
        const uint ALG_SID_ECDH = 5;
        const uint ALG_SID_MD2 = 1;
        const uint ALG_SID_MD4 = 2;
        const uint ALG_SID_MD5 = 3;
        const uint ALG_SID_SHA = 4;
        const uint ALG_SID_SHA1 = 4;
        const uint ALG_SID_MAC = 5;
        const uint ALG_SID_RIPEMD = 6;
        const uint ALG_SID_RIPEMD160 = 7;
        const uint ALG_SID_SSL3SHAMD5 = 8;
        const uint ALG_SID_HMAC = 9;
        const uint ALG_SID_TLS1PRF = 10;
        const uint ALG_SID_HASH_REPLACE_OWF = 11;
        const uint ALG_SID_SHA_256 = 12;
        const uint ALG_SID_SHA_384 = 13;
        const uint ALG_SID_SHA_512 = 14;
        const uint ALG_SID_SSL3_MASTER = 1;
        const uint ALG_SID_SCHANNEL_MASTER_HASH = 2;
        const uint ALG_SID_SCHANNEL_MAC_KEY = 3;
        const uint ALG_SID_PCT1_MASTER = 4;
        const uint ALG_SID_SSL2_MASTER = 5;
        const uint ALG_SID_TLS1_MASTER = 6;
        const uint ALG_SID_SCHANNEL_ENC_KEY = 7;
        const uint ALG_SID_ECMQV = 1;

        public enum CALG : uint
        {
            AT_KEYEXCHANGE = 1,
            AT_SIGNATURE = 2,
            CALG_MD2 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MD2),
            CALG_MD4 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MD4),
            CALG_MD5 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MD5),
            CALG_SHA = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA),
            CALG_SHA1 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA1),
            CALG_MAC = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MAC),
            CALG_RSA_SIGN = (ALG_CLASS_SIGNATURE | ALG_TYPE_RSA | ALG_SID_RSA_ANY),
            CALG_DSS_SIGN = (ALG_CLASS_SIGNATURE | ALG_TYPE_DSS | ALG_SID_DSS_ANY),
            CALG_NO_SIGN = (ALG_CLASS_SIGNATURE | ALG_TYPE_ANY | ALG_SID_ANY),
            CALG_RSA_KEYX = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_RSA | ALG_SID_RSA_ANY),
            CALG_DES = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_DES),
            CALG_3DES_112 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_3DES_112),
            CALG_3DES = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_3DES),
            CALG_DESX = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_DESX),
            CALG_RC2 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_RC2),
            CALG_RC4 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_STREAM | ALG_SID_RC4),
            CALG_SEAL = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_STREAM | ALG_SID_SEAL),
            CALG_DH_SF = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_DH | ALG_SID_DH_SANDF),
            CALG_DH_EPHEM = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_DH | ALG_SID_DH_EPHEM),
            CALG_AGREEDKEY_ANY = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_DH | ALG_SID_AGREED_KEY_ANY),
            CALG_KEA_KEYX = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_DH | ALG_SID_KEA),
            CALG_HUGHES_MD5 = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_ANY | ALG_SID_MD5),
            CALG_SKIPJACK = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_SKIPJACK),
            CALG_TEK = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_TEK),
            CALG_CYLINK_MEK = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_CYLINK_MEK),
            CALG_SSL3_SHAMD5 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SSL3SHAMD5),
            CALG_SSL3_MASTER = (ALG_CLASS_MSG_ENCRYPT | ALG_TYPE_SECURECHANNEL | ALG_SID_SSL3_MASTER),
            CALG_SCHANNEL_MASTER_HASH = (ALG_CLASS_MSG_ENCRYPT | ALG_TYPE_SECURECHANNEL | ALG_SID_SCHANNEL_MASTER_HASH),
            CALG_SCHANNEL_MAC_KEY = (ALG_CLASS_MSG_ENCRYPT | ALG_TYPE_SECURECHANNEL | ALG_SID_SCHANNEL_MAC_KEY),
            CALG_SCHANNEL_ENC_KEY = (ALG_CLASS_MSG_ENCRYPT | ALG_TYPE_SECURECHANNEL | ALG_SID_SCHANNEL_ENC_KEY),
            CALG_PCT1_MASTER = (ALG_CLASS_MSG_ENCRYPT | ALG_TYPE_SECURECHANNEL | ALG_SID_PCT1_MASTER),
            CALG_SSL2_MASTER = (ALG_CLASS_MSG_ENCRYPT | ALG_TYPE_SECURECHANNEL | ALG_SID_SSL2_MASTER),
            CALG_TLS1_MASTER = (ALG_CLASS_MSG_ENCRYPT | ALG_TYPE_SECURECHANNEL | ALG_SID_TLS1_MASTER),
            CALG_RC5 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_RC5),
            CALG_HMAC = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_HMAC),
            CALG_TLS1PRF = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_TLS1PRF),
            CALG_HASH_REPLACE_OWF = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_HASH_REPLACE_OWF),
            CALG_AES_128 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_AES_128),
            CALG_AES_192 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_AES_192),
            CALG_AES_256 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_AES_256),
            CALG_AES = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_AES),
            CALG_SHA_256 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA_256),
            CALG_SHA_384 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA_384),
            CALG_SHA_512 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_SHA_512),
            CALG_ECDH = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_DH | ALG_SID_ECDH),
            CALG_ECMQV = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_ANY | ALG_SID_ECMQV),
            CALG_ECDSA = (ALG_CLASS_SIGNATURE | ALG_TYPE_DSS | ALG_SID_ECDSA),
        }

        [Flags]
        public enum KeyGenFlags : uint
        {
            CRYPT_EXPORTABLE = 0x00000001,
            CRYPT_USER_PROTECTED = 0x00000002,
            CRYPT_CREATE_SALT = 0x00000004,
            CRYPT_UPDATE_KEY = 0x00000008,
            CRYPT_NO_SALT = 0x00000010,
            CRYPT_PREGEN = 0x00000040,
            CRYPT_RECIPIENT = 0x00000010,
            CRYPT_INITIATOR = 0x00000040,
            CRYPT_ONLINE = 0x00000080,
            CRYPT_SF = 0x00000100,
            CRYPT_CREATE_IV = 0x00000200,
            CRYPT_KEK = 0x00000400,
            CRYPT_DATA_KEY = 0x00000800,
            CRYPT_VOLATILE = 0x00001000,
            CRYPT_SGCKEY = 0x00002000,
            CRYPT_ARCHIVABLE = 0x00004000,
            CRYPT_FORCE_KEY_PROTECTION_HIGH = 0x00008000,
            RSA1024BIT_KEY = (1024 << 16),
            RSA2048BIT_KEY = (2048 << 16),
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPT_KEY_PROV_PARAM
        {
            public uint dwParam;
            public byte[] pbData;
            public uint cbData;
            public uint dwFlags;

            public CRYPT_KEY_PROV_PARAM(uint dwParam, byte[] pbData, uint cbData, uint dwFlags)
            {
                this.dwParam = dwParam;
                this.pbData = pbData;
                this.cbData = cbData;
                this.dwFlags = dwFlags;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CRYPT_KEY_PROV_INFO
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwszContainerName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwszProvName;
            public uint dwProvType;
            public uint dwFlags;
            public uint cProvParam;
            public IntPtr rgProvParam;
            public uint dwKeySpec;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CRYPTOAPI_BLOB
        {
            public uint cbData;
            public IntPtr pbData;            

            public CRYPTOAPI_BLOB()
            {
                cbData = 0;
                pbData = IntPtr.Zero;                
            }

            public CRYPTOAPI_BLOB(byte[] bData)
            {
                this.cbData = (uint)bData.Length;
                this.pbData = Marshal.AllocHGlobal(bData.Length);
                Marshal.Copy(bData, 0, pbData, bData.Length);                
            }

            public void Release()
            {
                if (pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pbData);
                }
            }

            public static CRYPTOAPI_BLOB FromBlob(CRYPTOAPI_BLOB blob)
            {
                CRYPTOAPI_BLOB ret = new CRYPTOAPI_BLOB();
                if ((blob.cbData > 0) && (blob.pbData != IntPtr.Zero))
                {
                    ret.cbData = blob.cbData;                    
                    ret.pbData = Marshal.AllocHGlobal((int)blob.cbData);
                    byte[] buf = new byte[ret.cbData];
                    Marshal.Copy(blob.pbData, buf, 0, buf.Length);
                    Marshal.Copy(buf, 0, ret.pbData, (int)ret.cbData);
                }

                return ret;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CRYPT_BIT_BLOB
        {
            public uint cbData;
            public IntPtr pbData;
            public uint cUnusedBits;            

            public CRYPT_BIT_BLOB()
            {                
            }

            public CRYPT_BIT_BLOB(byte[] bData)
            {
                this.cbData = (uint)bData.Length;
                this.pbData = Marshal.AllocHGlobal(bData.Length);
                this.cUnusedBits = 0;
                Marshal.Copy(bData, 0, pbData, bData.Length);                
            }

            public void Release()
            {
                if (pbData != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pbData);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_BASIC_CONSTRAINTS_INFO
        {
            public CRYPT_BIT_BLOB SubjectType;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fPathLenConstraint;
            public uint dwPathLenConstraint;
            public uint cSubtreesConstraint;
            public IntPtr rgSubtreesConstraint;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_BASIC_CONSTRAINTS2_INFO
        {
            [MarshalAs(UnmanagedType.Bool)]
            public bool fCA;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fPathLenConstraint;
            public uint dwPathLenConstraint;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CRYPT_ALGORITHM_IDENTIFIER
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pszObjId;
            public CRYPTOAPI_BLOB Parameters;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_EXTENSION
        {            
            public IntPtr pszObjId;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fCritical;
            public CRYPTOAPI_BLOB Value;

            public CERT_EXTENSION()
            {
            }

            public CERT_EXTENSION(IntPtr objId, bool critical, byte[] value)
            {
                pszObjId = objId;
                fCritical = critical;
                Value = new CRYPTOAPI_BLOB(value);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_EXTENSIONS
        {
            public uint cExtension;
            public IntPtr rgExtension;            

            public CERT_EXTENSIONS()
            {
                this.cExtension = 0;
                this.rgExtension = IntPtr.Zero;                
            }

            public void Release()
            {
                if (rgExtension != IntPtr.Zero)
                {
                    IntPtr buf = rgExtension;

                    for (int i = 0; i < (int)cExtension; i++)
                    {
                        Marshal.DestroyStructure(buf, typeof(CERT_EXTENSION));
                        buf += Marshal.SizeOf(typeof(CERT_EXTENSION));
                    }

                    Marshal.FreeHGlobal(rgExtension);
                }
            }

            public CERT_EXTENSIONS(CERT_EXTENSION[] extensions)
            {
                this.cExtension = (uint)extensions.Length;

                if (extensions.Length > 0)
                {
                    IntPtr buf = Marshal.AllocHGlobal(extensions.Length * Marshal.SizeOf(typeof(CERT_EXTENSION)));
                    this.rgExtension = buf;

                    for (int i = 0; i < extensions.Length; i++)
                    {
                        Marshal.StructureToPtr(extensions[i], buf, false);
                        buf += Marshal.SizeOf(typeof(CERT_EXTENSION));
                    }                    
                }                
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_PUBLIC_KEY_INFO
        {
            CRYPT_ALGORITHM_IDENTIFIER Algorithm;
            CRYPT_BIT_BLOB PublicKey;
        }

        [Flags]
        public enum CertEncoding
        {
            CRYPT_ASN_ENCODING = 0x00000001,
            CRYPT_NDR_ENCODING = 0x00000002,
            X509_ASN_ENCODING = 0x00000001,
            X509_NDR_ENCODING = 0x00000002,
            PKCS_7_ASN_ENCODING = 0x00010000,
            PKCS_7_NDR_ENCODING = 0x00020000,

            GENERAL_ENCODING = (X509_ASN_ENCODING | PKCS_7_ASN_ENCODING)
        }

        [Flags]
        public enum CertStringType
        {
            CERT_SIMPLE_NAME_STR = 1,
            CERT_OID_NAME_STR = 2,
            CERT_X500_NAME_STR = 3,
            CERT_XML_NAME_STR = 4,
            CERT_NAME_STR_SEMICOLON_FLAG = 0x40000000,
            CERT_NAME_STR_NO_PLUS_FLAG = 0x20000000,
            CERT_NAME_STR_NO_QUOTING_FLAG = 0x10000000,
            CERT_NAME_STR_CRLF_FLAG = 0x08000000,
            CERT_NAME_STR_COMMA_FLAG = 0x04000000,
            CERT_NAME_STR_REVERSE_FLAG = 0x02000000,
            CERT_NAME_STR_FORWARD_FLAG = 0x01000000,
            CERT_NAME_STR_DISABLE_IE4_UTF8_FLAG = 0x00010000,
            CERT_NAME_STR_ENABLE_T61_UNICODE_FLAG = 0x00020000,
            CERT_NAME_STR_ENABLE_UTF8_UNICODE_FLAG = 0x00040000,
            CERT_NAME_STR_FORCE_UTF8_DIR_STR_FLAG = 0x00080000,
            CERT_NAME_STR_DISABLE_UTF8_DIR_STR_FLAG = 0x00100000,
            CERT_NAME_STR_ENABLE_PUNYCODE_FLAG = 0x00200000,
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_INFO
        {
            public uint dwVersion;
            public CRYPTOAPI_BLOB SerialNumber;
            public CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;
            public CRYPTOAPI_BLOB Issuer;
            public System.Runtime.InteropServices.ComTypes.FILETIME NotBefore;
            public System.Runtime.InteropServices.ComTypes.FILETIME NotAfter;
            public CRYPTOAPI_BLOB Subject;
            public CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;
            public CRYPT_BIT_BLOB IssuerUniqueId;
            public CRYPT_BIT_BLOB SubjectUniqueId;
            public uint cExtension;
            public IntPtr rgExtension;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_CONTEXT
        {
            public uint dwCertEncodingType;
            public IntPtr pbCertEncoded;
            public uint cbCertEncoded;
            public IntPtr pCertInfo;
            public IntPtr hCertStore;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_AUTHORITY_KEY_ID_INFO 
        {
            public CRYPTOAPI_BLOB KeyId;
            public CRYPTOAPI_BLOB CertIssuer;
            public CRYPTOAPI_BLOB CertSerialNumber;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_ALT_NAME_DIRECTORY
        {
            public uint dwAltNameChoice; // Should equal CERT_ALT_NAME_DIRECTORY_NAME
            public CRYPTOAPI_BLOB DirectoryName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_ALT_NAME_INFO {
            public uint cAltEntry;
            public IntPtr rgAltEntry;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CERT_AUTHORITY_KEY_ID2_INFO {
            public CRYPTOAPI_BLOB KeyId;
            public CERT_ALT_NAME_INFO AuthorityCertIssuer;
            public CRYPTOAPI_BLOB AuthorityCertSerialNumber;
        };

        public class SafeCryptProviderHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeCryptProviderHandle()
                : base(true)
            {
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            protected override bool ReleaseHandle()
            {
                return CryptoApiMethods.CryptReleaseContext(handle, 0);
            }
        }

        public class SafeCryptKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeCryptKeyHandle()
                : base(true)
            {
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            protected override bool ReleaseHandle()
            {
                return CryptoApiMethods.CryptDestroyKey(handle);
            }
        }


        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptAcquireContext(out SafeCryptProviderHandle hProv, string pszContainer, string pszProvider, Providers dwProvType, CryptFlags dwFlags);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptGenKey(SafeCryptProviderHandle hProv, CALG Algid, KeyGenFlags dwFlags, out SafeCryptKeyHandle phKey);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptDestroyKey(IntPtr phKey);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptReleaseContext(IntPtr hProv, uint dwFlags);

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CertStrToName(CertEncoding dwCertEncodingType, string pszX500, CertStringType dwStrType,
            IntPtr pvReserved, byte[] pbEncoded, ref uint pcbEncoded, IntPtr ppszError);

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CertFreeCertificateContext(IntPtr pCertContext);

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptExportPublicKeyInfoEx(SafeCryptProviderHandle hCryptProv, CALG dwKeySpec, CertEncoding dwCertEncodingType,
            [MarshalAs(UnmanagedType.LPStr)] string pszPublicKeyObjId, uint dwFlags, IntPtr pvAuxInfo, IntPtr pInfo, ref uint pcbInfo);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptImportKey(SafeCryptProviderHandle hProv, byte[] pbData, uint dwDataLen, 
            SafeCryptKeyHandle hPubKey, uint dwFlags, out SafeCryptKeyHandle hKey);


        [Flags]
        public enum CertCreationFlags
        {
            CERT_CREATE_SELFSIGN_NO_SIGN = 1,
            CERT_CREATE_SELFSIGN_NO_KEY_INFO = 2,
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SYSTEMTIME
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Milliseconds;
        }

        [DllImport("kernel32.dll")]
        public static extern void GetSystemTime([Out] SYSTEMTIME lpSystemTime);


        [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CertCreateSelfSignCertificate(
            SafeCryptProviderHandle hCryptProvOrNCryptKey,
            [Out] CRYPTOAPI_BLOB pSubjectIssuerBlob,
            CertCreationFlags dwFlags,
            CRYPT_KEY_PROV_INFO pKeyProvInfo,
            CRYPT_ALGORITHM_IDENTIFIER pSignatureAlgorithm,
            SYSTEMTIME pStartTime,
            SYSTEMTIME pEndTime,
            CERT_EXTENSIONS pExtension
            );

        [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptEncodeObject(
            CertEncoding dwCertEncodingType,
            [MarshalAs(UnmanagedType.LPStr)] string lpszStructType,
            IntPtr pvStructInfo,
            byte[] pbEncoded, ref uint pcbEncoded
         );

        public enum StructType
        {
            X509_CERT = 1,
            X509_CERT_TO_BE_SIGNED = 2,
            X509_CERT_CRL_TO_BE_SIGNED = 3,
            X509_CERT_REQUEST_TO_BE_SIGNED = 4,
            X509_EXTENSIONS = 5,
            X509_NAME_VALUE = 6,
            X509_NAME = 7,
            X509_PUBLIC_KEY_INFO = 8,
        }

        public enum CertVersion : uint
        {
            CERT_V1 = 0,
            CERT_V2 = 1,
            CERT_V3 = 2
        }

        [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptSignAndEncodeCertificate(
               SafeCryptProviderHandle hCryptProv,
               CALG dwKeySpec,
               CertEncoding dwCertEncodingType,
               IntPtr lpszStructType,
               IntPtr pvStructInfo,
               CRYPT_ALGORITHM_IDENTIFIER pSignatureAlgorithm,
               IntPtr pvHashAuxInfo,
               byte[] pbEncoded,
               ref uint pcbEncoded
         );

        [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CryptHashPublicKeyInfo(
            SafeCryptProviderHandle hCryptProv,
            CALG Algid,
            uint dwFlags,
            CertEncoding dwCertEncodingType,
            CERT_PUBLIC_KEY_INFO pInfo,
            byte[] pbComputedHash,
            out uint pcbComputedHash
        );

        public const string szOID_BASIC_CONSTRAINTS = "2.5.29.10";
        public const string szOID_BASIC_CONSTRAINTS2 = "2.5.29.19";
        public const string szOID_SUBJECT_KEY_IDENTIFIER = "2.5.29.14";
        public const string szOID_RSA_MD5RSA = "1.2.840.113549.1.1.4";
        public const string szOID_RSA_SHA1RSA = "1.2.840.113549.1.1.5";
        public const string szOID_RSA_SHA256RSA = "1.2.840.113549.1.1.11";
        public const string szOID_RSA_SHA384RSA = "1.2.840.113549.1.1.12";
        public const string szOID_RSA_SHA512RSA = "1.2.840.113549.1.1.13";
        public const string szOID_RSA_RSA = "1.2.840.113549.1.1.1";
        public const string szOID_AUTHORITY_KEY_IDENTIFIER = "2.5.29.1";
        public const string szOID_AUTHORITY_KEY_IDENTIFIER2 = "2.5.29.35";
        public const string szOID_OIWSEC_sha1RSASign = "1.3.14.3.2.29";
    }
}
