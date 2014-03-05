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
using System.ComponentModel;
using System.IO;
using System.Xml;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Parser
{
    /// <summary>
    /// Config for WCF binary parser
    /// </summary>
    [Serializable]
    public class WcfXmlMessageConfig
    {
        [LocalizedDescription("WcfXmlMessageConfig_NoEncodeDescription", typeof(Properties.Resources))]
        public bool NoEncode { get; set; }

        [LocalizedDescription("WcfXmlMessageConfig_NoDecodeDescription", typeof(Properties.Resources))]
        public bool NoDecode { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [NodeLibraryClass("WcfXmlMessage", typeof(Properties.Resources),        
        ConfigType=typeof(WcfXmlMessageConfig), 
        Category =NodeLibraryClassCategory.Parser)]
    public class WcfXmlMessage : BasePersistDynamicNode<WcfXmlMessageConfig>, IDataArrayParser
    {
        static XmlDictionary wcfDictionary;

        static WcfXmlMessage()
        {
            wcfDictionary = new XmlDictionary();

            wcfDictionary.Add("mustUnderstand");
            wcfDictionary.Add("Envelope");
            wcfDictionary.Add("http://www.w3.org/2003/05/soap-envelope");
            wcfDictionary.Add("http://www.w3.org/2005/08/addressing");
            wcfDictionary.Add("Header");
            wcfDictionary.Add("Action");
            wcfDictionary.Add("To");
            wcfDictionary.Add("Body");
            wcfDictionary.Add("Algorithm");
            wcfDictionary.Add("RelatesTo");
            wcfDictionary.Add("http://www.w3.org/2005/08/addressing/anonymous");
            wcfDictionary.Add("URI");
            wcfDictionary.Add("Reference");
            wcfDictionary.Add("MessageID");
            wcfDictionary.Add("Id");
            wcfDictionary.Add("Identifier");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/rm");
            wcfDictionary.Add("Transforms");
            wcfDictionary.Add("Transform");
            wcfDictionary.Add("DigestMethod");
            wcfDictionary.Add("DigestValue");
            wcfDictionary.Add("Address");
            wcfDictionary.Add("ReplyTo");
            wcfDictionary.Add("SequenceAcknowledgement");
            wcfDictionary.Add("AcknowledgementRange");
            wcfDictionary.Add("Upper");
            wcfDictionary.Add("Lower");
            wcfDictionary.Add("BufferRemaining");
            wcfDictionary.Add("http://schemas.microsoft.com/ws/2006/05/rm");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/rm/SequenceAcknowledgement");
            wcfDictionary.Add("SecurityTokenReference");
            wcfDictionary.Add("Sequence");
            wcfDictionary.Add("MessageNumber");
            wcfDictionary.Add("http://www.w3.org/2000/09/xmldsig#");
            wcfDictionary.Add("http://www.w3.org/2000/09/xmldsig#enveloped-signature");
            wcfDictionary.Add("KeyInfo");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/sc");
            wcfDictionary.Add("DerivedKeyToken");
            wcfDictionary.Add("Nonce");
            wcfDictionary.Add("Signature");
            wcfDictionary.Add("SignedInfo");
            wcfDictionary.Add("CanonicalizationMethod");
            wcfDictionary.Add("SignatureMethod");
            wcfDictionary.Add("SignatureValue");
            wcfDictionary.Add("DataReference");
            wcfDictionary.Add("EncryptedData");
            wcfDictionary.Add("EncryptionMethod");
            wcfDictionary.Add("CipherData");
            wcfDictionary.Add("CipherValue");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
            wcfDictionary.Add("Security");
            wcfDictionary.Add("Timestamp");
            wcfDictionary.Add("Created");
            wcfDictionary.Add("Expires");
            wcfDictionary.Add("Length");
            wcfDictionary.Add("ReferenceList");
            wcfDictionary.Add("ValueType");
            wcfDictionary.Add("Type");
            wcfDictionary.Add("EncryptedHeader");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd");
            wcfDictionary.Add("RequestSecurityTokenResponseCollection");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust#BinarySecret");
            wcfDictionary.Add("http://schemas.microsoft.com/ws/2006/02/transactions");
            wcfDictionary.Add("s");
            wcfDictionary.Add("Fault");
            wcfDictionary.Add("MustUnderstand");
            wcfDictionary.Add("role");
            wcfDictionary.Add("relay");
            wcfDictionary.Add("Code");
            wcfDictionary.Add("Reason");
            wcfDictionary.Add("Text");
            wcfDictionary.Add("Node");
            wcfDictionary.Add("Role");
            wcfDictionary.Add("Detail");
            wcfDictionary.Add("Value");
            wcfDictionary.Add("Subcode");
            wcfDictionary.Add("NotUnderstood");
            wcfDictionary.Add("qname");
            wcfDictionary.Add("");
            wcfDictionary.Add("From");
            wcfDictionary.Add("FaultTo");
            wcfDictionary.Add("EndpointReference");
            wcfDictionary.Add("PortType");
            wcfDictionary.Add("ServiceName");
            wcfDictionary.Add("PortName");
            wcfDictionary.Add("ReferenceProperties");
            wcfDictionary.Add("RelationshipType");
            wcfDictionary.Add("Reply");
            wcfDictionary.Add("a");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2006/02/addressingidentity");
            wcfDictionary.Add("Identity");
            wcfDictionary.Add("Spn");
            wcfDictionary.Add("Upn");
            wcfDictionary.Add("Rsa");
            wcfDictionary.Add("Dns");
            wcfDictionary.Add("X509v3Certificate");
            wcfDictionary.Add("http://www.w3.org/2005/08/addressing/fault");
            wcfDictionary.Add("ReferenceParameters");
            wcfDictionary.Add("IsReferenceParameter");
            wcfDictionary.Add("http://www.w3.org/2005/08/addressing/reply");
            wcfDictionary.Add("http://www.w3.org/2005/08/addressing/none");
            wcfDictionary.Add("Metadata");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/08/addressing");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/08/addressing/fault");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/06/addressingex");
            wcfDictionary.Add("RedirectTo");
            wcfDictionary.Add("Via");
            wcfDictionary.Add("http://www.w3.org/2001/10/xml-exc-c14n#");
            wcfDictionary.Add("PrefixList");
            wcfDictionary.Add("InclusiveNamespaces");
            wcfDictionary.Add("ec");
            wcfDictionary.Add("SecurityContextToken");
            wcfDictionary.Add("Generation");
            wcfDictionary.Add("Label");
            wcfDictionary.Add("Offset");
            wcfDictionary.Add("Properties");
            wcfDictionary.Add("Cookie");
            wcfDictionary.Add("wsc");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/sc");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/sc/dk");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/sc/sct");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/SCT");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/SCT");
            wcfDictionary.Add("RenewNeeded");
            wcfDictionary.Add("BadContextToken");
            wcfDictionary.Add("c");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/sc/dk");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/sc/sct");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#aes128-cbc");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes128");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#aes192-cbc");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes192");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes256");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#des-cbc");
            wcfDictionary.Add("http://www.w3.org/2000/09/xmldsig#dsa-sha1");
            wcfDictionary.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments");
            wcfDictionary.Add("http://www.w3.org/2000/09/xmldsig#hmac-sha1");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#ripemd160");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p");
            wcfDictionary.Add("http://www.w3.org/2000/09/xmldsig#rsa-sha1");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#rsa-1_5");
            wcfDictionary.Add("http://www.w3.org/2000/09/xmldsig#sha1");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#sha256");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#sha512");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#tripledes-cbc");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-tripledes");
            wcfDictionary.Add("http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap");
            wcfDictionary.Add("http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap");
            wcfDictionary.Add("http://schemas.microsoft.com/ws/2006/05/security");
            wcfDictionary.Add("dnse");
            wcfDictionary.Add("o");
            wcfDictionary.Add("Password");
            wcfDictionary.Add("PasswordText");
            wcfDictionary.Add("Username");
            wcfDictionary.Add("UsernameToken");
            wcfDictionary.Add("BinarySecurityToken");
            wcfDictionary.Add("EncodingType");
            wcfDictionary.Add("KeyIdentifier");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID");
            wcfDictionary.Add("Assertion");
            wcfDictionary.Add("urn:oasis:names:tc:SAML:1.0:assertion");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license");
            wcfDictionary.Add("FailedAuthentication");
            wcfDictionary.Add("InvalidSecurityToken");
            wcfDictionary.Add("InvalidSecurity");
            wcfDictionary.Add("k");
            wcfDictionary.Add("SignatureConfirmation");
            wcfDictionary.Add("TokenType");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID");
            wcfDictionary.Add("AUTH-HASH");
            wcfDictionary.Add("RequestSecurityTokenResponse");
            wcfDictionary.Add("KeySize");
            wcfDictionary.Add("RequestedTokenReference");
            wcfDictionary.Add("AppliesTo");
            wcfDictionary.Add("Authenticator");
            wcfDictionary.Add("CombinedHash");
            wcfDictionary.Add("BinaryExchange");
            wcfDictionary.Add("Lifetime");
            wcfDictionary.Add("RequestedSecurityToken");
            wcfDictionary.Add("Entropy");
            wcfDictionary.Add("RequestedProofToken");
            wcfDictionary.Add("ComputedKey");
            wcfDictionary.Add("RequestSecurityToken");
            wcfDictionary.Add("RequestType");
            wcfDictionary.Add("Context");
            wcfDictionary.Add("BinarySecret");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/spnego");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego");
            wcfDictionary.Add("wst");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/trust");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/Issue");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/Issue");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/trust/Issue");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/trust/CK/PSHA1");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/trust/SymmetricKey");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/security/trust/Nonce");
            wcfDictionary.Add("KeyType");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/trust/SymmetricKey");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/04/trust/PublicKey");
            wcfDictionary.Add("Claims");
            wcfDictionary.Add("InvalidRequest");
            wcfDictionary.Add("RequestFailed");
            wcfDictionary.Add("SignWith");
            wcfDictionary.Add("EncryptWith");
            wcfDictionary.Add("EncryptionAlgorithm");
            wcfDictionary.Add("CanonicalizationAlgorithm");
            wcfDictionary.Add("ComputedKeyAlgorithm");
            wcfDictionary.Add("UseKey");
            wcfDictionary.Add("http://schemas.microsoft.com/net/2004/07/secext/WS-SPNego");
            wcfDictionary.Add("http://schemas.microsoft.com/net/2004/07/secext/TLSNego");
            wcfDictionary.Add("t");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/Issue");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/Nonce");
            wcfDictionary.Add("RenewTarget");
            wcfDictionary.Add("CancelTarget");
            wcfDictionary.Add("RequestedTokenCancelled");
            wcfDictionary.Add("RequestedAttachedReference");
            wcfDictionary.Add("RequestedUnattachedReference");
            wcfDictionary.Add("IssuedTokens");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/Renew");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/Cancel");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey");
            wcfDictionary.Add("Access");
            wcfDictionary.Add("AccessDecision");
            wcfDictionary.Add("Advice");
            wcfDictionary.Add("AssertionID");
            wcfDictionary.Add("AssertionIDReference");
            wcfDictionary.Add("Attribute");
            wcfDictionary.Add("AttributeName");
            wcfDictionary.Add("AttributeNamespace");
            wcfDictionary.Add("AttributeStatement");
            wcfDictionary.Add("AttributeValue");
            wcfDictionary.Add("Audience");
            wcfDictionary.Add("AudienceRestrictionCondition");
            wcfDictionary.Add("AuthenticationInstant");
            wcfDictionary.Add("AuthenticationMethod");
            wcfDictionary.Add("AuthenticationStatement");
            wcfDictionary.Add("AuthorityBinding");
            wcfDictionary.Add("AuthorityKind");
            wcfDictionary.Add("AuthorizationDecisionStatement");
            wcfDictionary.Add("Binding");
            wcfDictionary.Add("Condition");
            wcfDictionary.Add("Conditions");
            wcfDictionary.Add("Decision");
            wcfDictionary.Add("DoNotCacheCondition");
            wcfDictionary.Add("Evidence");
            wcfDictionary.Add("IssueInstant");
            wcfDictionary.Add("Issuer");
            wcfDictionary.Add("Location");
            wcfDictionary.Add("MajorVersion");
            wcfDictionary.Add("MinorVersion");
            wcfDictionary.Add("NameIdentifier");
            wcfDictionary.Add("Format");
            wcfDictionary.Add("NameQualifier");
            wcfDictionary.Add("Namespace");
            wcfDictionary.Add("NotBefore");
            wcfDictionary.Add("NotOnOrAfter");
            wcfDictionary.Add("saml");
            wcfDictionary.Add("Statement");
            wcfDictionary.Add("Subject");
            wcfDictionary.Add("SubjectConfirmation");
            wcfDictionary.Add("SubjectConfirmationData");
            wcfDictionary.Add("ConfirmationMethod");
            wcfDictionary.Add("urn:oasis:names:tc:SAML:1.0:cm:holder-of-key");
            wcfDictionary.Add("urn:oasis:names:tc:SAML:1.0:cm:sender-vouches");
            wcfDictionary.Add("SubjectLocality");
            wcfDictionary.Add("DNSAddress");
            wcfDictionary.Add("IPAddress");
            wcfDictionary.Add("SubjectStatement");
            wcfDictionary.Add("urn:oasis:names:tc:SAML:1.0:am:unspecified");
            wcfDictionary.Add("xmlns");
            wcfDictionary.Add("Resource");
            wcfDictionary.Add("UserName");
            wcfDictionary.Add("urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName");
            wcfDictionary.Add("EmailName");
            wcfDictionary.Add("urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress");
            wcfDictionary.Add("u");
            wcfDictionary.Add("ChannelInstance");
            wcfDictionary.Add("http://schemas.microsoft.com/ws/2005/02/duplex");
            wcfDictionary.Add("Encoding");
            wcfDictionary.Add("MimeType");
            wcfDictionary.Add("CarriedKeyName");
            wcfDictionary.Add("Recipient");
            wcfDictionary.Add("EncryptedKey");
            wcfDictionary.Add("KeyReference");
            wcfDictionary.Add("e");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#Element");
            wcfDictionary.Add("http://www.w3.org/2001/04/xmlenc#Content");
            wcfDictionary.Add("KeyName");
            wcfDictionary.Add("MgmtData");
            wcfDictionary.Add("KeyValue");
            wcfDictionary.Add("RSAKeyValue");
            wcfDictionary.Add("Modulus");
            wcfDictionary.Add("Exponent");
            wcfDictionary.Add("X509Data");
            wcfDictionary.Add("X509IssuerSerial");
            wcfDictionary.Add("X509IssuerName");
            wcfDictionary.Add("X509SerialNumber");
            wcfDictionary.Add("X509Certificate");
            wcfDictionary.Add("AckRequested");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/rm/AckRequested");
            wcfDictionary.Add("AcksTo");
            wcfDictionary.Add("Accept");
            wcfDictionary.Add("CreateSequence");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequence");
            wcfDictionary.Add("CreateSequenceRefused");
            wcfDictionary.Add("CreateSequenceResponse");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/rm/CreateSequenceResponse");
            wcfDictionary.Add("FaultCode");
            wcfDictionary.Add("InvalidAcknowledgement");
            wcfDictionary.Add("LastMessage");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage");
            wcfDictionary.Add("LastMessageNumberExceeded");
            wcfDictionary.Add("MessageNumberRollover");
            wcfDictionary.Add("Nack");
            wcfDictionary.Add("netrm");
            wcfDictionary.Add("Offer");
            wcfDictionary.Add("r");
            wcfDictionary.Add("SequenceFault");
            wcfDictionary.Add("SequenceTerminated");
            wcfDictionary.Add("TerminateSequence");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/rm/TerminateSequence");
            wcfDictionary.Add("UnknownSequence");
            wcfDictionary.Add("http://schemas.microsoft.com/ws/2006/02/tx/oletx");
            wcfDictionary.Add("oletx");
            wcfDictionary.Add("OleTxTransaction");
            wcfDictionary.Add("PropagationToken");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wscoor");
            wcfDictionary.Add("wscoor");
            wcfDictionary.Add("CreateCoordinationContext");
            wcfDictionary.Add("CreateCoordinationContextResponse");
            wcfDictionary.Add("CoordinationContext");
            wcfDictionary.Add("CurrentContext");
            wcfDictionary.Add("CoordinationType");
            wcfDictionary.Add("RegistrationService");
            wcfDictionary.Add("Register");
            wcfDictionary.Add("RegisterResponse");
            wcfDictionary.Add("ProtocolIdentifier");
            wcfDictionary.Add("CoordinatorProtocolService");
            wcfDictionary.Add("ParticipantProtocolService");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wscoor/CreateCoordinationContext");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wscoor/CreateCoordinationContextResponse");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wscoor/Register");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wscoor/RegisterResponse");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wscoor/fault");
            wcfDictionary.Add("ActivationCoordinatorPortType");
            wcfDictionary.Add("RegistrationCoordinatorPortType");
            wcfDictionary.Add("InvalidState");
            wcfDictionary.Add("InvalidProtocol");
            wcfDictionary.Add("InvalidParameters");
            wcfDictionary.Add("NoActivity");
            wcfDictionary.Add("ContextRefused");
            wcfDictionary.Add("AlreadyRegistered");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat");
            wcfDictionary.Add("wsat");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Completion");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Durable2PC");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Volatile2PC");
            wcfDictionary.Add("Prepare");
            wcfDictionary.Add("Prepared");
            wcfDictionary.Add("ReadOnly");
            wcfDictionary.Add("Commit");
            wcfDictionary.Add("Rollback");
            wcfDictionary.Add("Committed");
            wcfDictionary.Add("Aborted");
            wcfDictionary.Add("Replay");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Commit");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Rollback");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Committed");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Aborted");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Prepare");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Prepared");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/ReadOnly");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/Replay");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2004/10/wsat/fault");
            wcfDictionary.Add("CompletionCoordinatorPortType");
            wcfDictionary.Add("CompletionParticipantPortType");
            wcfDictionary.Add("CoordinatorPortType");
            wcfDictionary.Add("ParticipantPortType");
            wcfDictionary.Add("InconsistentInternalState");
            wcfDictionary.Add("mstx");
            wcfDictionary.Add("Enlistment");
            wcfDictionary.Add("protocol");
            wcfDictionary.Add("LocalTransactionId");
            wcfDictionary.Add("IsolationLevel");
            wcfDictionary.Add("IsolationFlags");
            wcfDictionary.Add("Description");
            wcfDictionary.Add("Loopback");
            wcfDictionary.Add("RegisterInfo");
            wcfDictionary.Add("ContextId");
            wcfDictionary.Add("TokenId");
            wcfDictionary.Add("AccessDenied");
            wcfDictionary.Add("InvalidPolicy");
            wcfDictionary.Add("CoordinatorRegistrationFailed");
            wcfDictionary.Add("TooManyEnlistments");
            wcfDictionary.Add("Disabled");
            wcfDictionary.Add("ActivityId");
            wcfDictionary.Add("http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics");
            wcfDictionary.Add("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1");
            wcfDictionary.Add("http://schemas.xmlsoap.org/ws/2002/12/policy");
            wcfDictionary.Add("FloodMessage");
            wcfDictionary.Add("LinkUtility");
            wcfDictionary.Add("Hops");
            wcfDictionary.Add("http://schemas.microsoft.com/net/2006/05/peer/HopCount");
            wcfDictionary.Add("PeerVia");
            wcfDictionary.Add("http://schemas.microsoft.com/net/2006/05/peer");
            wcfDictionary.Add("PeerFlooder");
            wcfDictionary.Add("PeerTo");
            wcfDictionary.Add("http://schemas.microsoft.com/ws/2005/05/routing");
            wcfDictionary.Add("PacketRoutable");
            wcfDictionary.Add("http://schemas.microsoft.com/ws/2005/05/addressing/none");
            wcfDictionary.Add("http://schemas.microsoft.com/ws/2005/05/envelope/none");
            wcfDictionary.Add("http://www.w3.org/2001/XMLSchema-instance");
            wcfDictionary.Add("http://www.w3.org/2001/XMLSchema");
            wcfDictionary.Add("nil");
            wcfDictionary.Add("type");
            wcfDictionary.Add("char");
            wcfDictionary.Add("boolean");
            wcfDictionary.Add("byte");
            wcfDictionary.Add("unsignedByte");
            wcfDictionary.Add("short");
            wcfDictionary.Add("unsignedShort");
            wcfDictionary.Add("int");
            wcfDictionary.Add("unsignedInt");
            wcfDictionary.Add("long");
            wcfDictionary.Add("unsignedLong");
            wcfDictionary.Add("float");
            wcfDictionary.Add("double");
            wcfDictionary.Add("decimal");
            wcfDictionary.Add("dateTime");
            wcfDictionary.Add("string");
            wcfDictionary.Add("base64Binary");
            wcfDictionary.Add("anyType");
            wcfDictionary.Add("duration");
            wcfDictionary.Add("guid");
            wcfDictionary.Add("anyURI");
            wcfDictionary.Add("QName");
            wcfDictionary.Add("time");
            wcfDictionary.Add("date");
            wcfDictionary.Add("hexBinary");
            wcfDictionary.Add("gYearMonth");
            wcfDictionary.Add("gYear");
            wcfDictionary.Add("gMonthDay");
            wcfDictionary.Add("gDay");
            wcfDictionary.Add("gMonth");
            wcfDictionary.Add("integer");
            wcfDictionary.Add("positiveInteger");
            wcfDictionary.Add("negativeInteger");
            wcfDictionary.Add("nonPositiveInteger");
            wcfDictionary.Add("nonNegativeInteger");
            wcfDictionary.Add("normalizedString");
            wcfDictionary.Add("ConnectionLimitReached");
            wcfDictionary.Add("http://schemas.xmlsoap.org/soap/envelope/");
            wcfDictionary.Add("actor");
            wcfDictionary.Add("faultcode");
            wcfDictionary.Add("faultstring");
            wcfDictionary.Add("faultactor");
            wcfDictionary.Add("detail");
        }

        #region IDataArrayParser Members

        public void FromArray(byte[] data, DataFrames.DataKey root, Utils.Logger logger)
        {
            if(!Config.NoDecode)
            {                              
                XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(data, 0, data.Length, wcfDictionary, XmlDictionaryReaderQuotas.Max);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                StringWriter sWriter = new StringWriter();
                XmlTextWriter writer = new XmlTextWriter(sWriter);
                writer.Formatting = Formatting.Indented;

                doc.WriteTo(writer);

                root.AddValue("xml", sWriter.ToString());
            }
            else
            {
                root.AddValue("xml", new BinaryEncoding().GetString(data));
            }
        }

        public byte[] ToArray(DataFrames.DataKey root, Utils.Logger logger)
        {            
            DataValue value = root.SelectSingleNode("xml") as DataValue;
            byte[] ret = new byte[0];

            if (value != null)
            {
                if (!Config.NoEncode)
                {
                    XmlDocument doc = new XmlDocument();

                    doc.LoadXml(value.Value.ToString());

                    MemoryStream stm = new MemoryStream();                    
                    XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(stm, wcfDictionary);

                    doc.Save(writer);
                    writer.Flush();

                    ret = stm.ToArray();
                }
                else
                {
                    ret = new BinaryEncoding().GetBytes(value.Value.ToString());
                }
            }

            return ret;
        }

        #endregion

        #region IDataParser Members

        public string ToDisplayString(DataFrames.DataKey root, Utils.Logger logger)
        {
            DataValue value = root.SelectSingleNode("xml") as DataValue;

            if (value != null)
            {
                return value.ToString();
            }

            return String.Empty;
        }

        #endregion
    }
}
