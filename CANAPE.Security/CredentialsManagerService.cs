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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using CANAPE.Security.Cryptography.X509Certificates;

namespace CANAPE.Security
{
    /// <summary>
    /// A service to provide authentication details
    /// </summary>
    public sealed class CredentialsManagerService
    {        
        private ConcurrentDictionary<SecurityPrincipal, object> _resolveLock 
            = new ConcurrentDictionary<SecurityPrincipal, object>();
        
        private Dictionary<SecurityPrincipal, ICredentialObject> _cachedCredentials
            = new Dictionary<SecurityPrincipal, ICredentialObject>();
        
        private ICredentialObject GetCachedCredentials(SecurityPrincipal principal)
        {
            lock (_cachedCredentials)
            {
                if (_cachedCredentials.ContainsKey(principal))
                {
                    return _cachedCredentials[principal];
                }
                else
                {
                    return null;
                }
            }
        }

        private static Type MapStringToType(string type)
        {
            if (type.Equals("user", StringComparison.OrdinalIgnoreCase) || type.Equals("username", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(AuthenticationCredentials);
            }
            else if (type.Equals("certificate", StringComparison.OrdinalIgnoreCase) 
                || type.Equals("cert", StringComparison.OrdinalIgnoreCase) 
                || type.Equals("x509", StringComparison.OrdinalIgnoreCase))
            {
                return typeof(X509CertificateContainer);
            }

            Type ret = Assembly.GetExecutingAssembly().GetType(type);
            if (ret == null)
            {
                ret = Type.GetType(type);
            }

            return ret;
        }

        /// <summary>
        /// Event signaled to resolve credentials
        /// </summary>
        public event EventHandler<ResolveCredentialsEventArgs> ResolveCredentials;

        /// <summary>
        /// Request credentials for a particular purpose
        /// </summary>
        /// <param name="type">The type of credentials, for example certificate</param>
        /// <param name="name">Principal name, e.g. a username, can be empty</param>
        /// <param name="realm">The principal realm, used to determine the target of the credentials, e.g. a domain</param>
        /// <param name="details">A textual description in case the user has to pick</param>
        /// <remarks>
        /// The type string can be one of:
        /// user or username - Returns AuthenticationCredentials
        /// cert or certificate or x509 - Returns an X509CertificateContainer
        /// A full type name for CANAPE.Security types
        /// A fully qualified assembly type name for any type including custom ones
        /// </remarks>
        /// <returns>The credentials object (depends on type)</returns>
        public ICredentialObject RequestCredentials(string type, string name, string realm, string details)
        {
            return RequestCredentials(MapStringToType(type), name, realm, details, null, null);
        }

        /// <summary>
        /// Request credentials for a particular purpose
        /// </summary>
        /// <typeparam name="T">The type of credentials, for example X509Certificate</typeparam>
        /// <param name="name">Principal name, e.g. a username, can be empty</param>
        /// <param name="realm">The principal realm, used to determine the target of the credentials, e.g. a domain</param>
        /// <param name="details">A textual description in case the user has to pick</param>
        /// <returns>The credentials object (depends on type)</returns>
        public T RequestCredentials<T>(string name, string realm, string details) where T : ICredentialObject
        {
            return (T) RequestCredentials(typeof(T), name, realm, details, null, null);
        }

        /// <summary>
        /// Request credentials for a particular purpose
        /// </summary>
        /// <param name="type">The type of credentials, for example X509CertificateContainer</param>
        /// <param name="name">Principal name, e.g. a username, can be empty</param>
        /// <param name="realm">The principal realm, used to determine the target of the credentials, e.g. a domain</param>
        /// <param name="details">A textual description in case the user has to pick</param>
        /// <param name="validateCredentials">A callback method to validate the credentials before placing them in the store, only called 
        /// when new credentials are requested, not cached (as they are assumed to be valid). Can be null</param>
        /// <param name="context">An arbitrary context, passed in to the function for use by the authenticator and the gathering of credentials</param>
        /// <returns>The credentials object (depends on type)</returns>
        /// <exception cref="ArgumentException">Thrown if type not derived from ICredentialObject</exception>
        public ICredentialObject RequestCredentials(Type type, string name, string realm, string details, Func<ICredentialObject, object, bool> validateCredentials, object context)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!typeof(ICredentialObject).IsAssignableFrom(type))
            {
                throw new ArgumentException(Properties.Resources.CredentialsManagerService_TypeMustDeriveFromICredentialObject);
            }

            SecurityPrincipal principal = new SecurityPrincipal(type, name, realm);

            ICredentialObject ret = GetCachedCredentials(principal);

            if(ret == null)
            {
                ret = GetCredentials(principal, details, validateCredentials, context);
            }

            return ret;
        }

        private ICredentialObject GetCredentials(SecurityPrincipal principal, string details, Func<ICredentialObject, object, bool> validateCredentials, object context)
        {
            ICredentialObject ret = null;            

            if (ResolveCredentials != null)
            {
                bool resolved = false;

                // Exit if we tried to resolve or ret is not null
                while (!resolved)
                {
                    // Resolve a single principal only one at a time
                    object lockObject = new object();
                    object currObject = _resolveLock.GetOrAdd(principal, lockObject);

                    lock (currObject)
                    {
                        // This is our object, and we know it is already locked
                        if (currObject == lockObject)
                        {
                            try
                            {
                                ResolveCredentialsEventArgs args = new ResolveCredentialsEventArgs(principal, details, context);

                                ResolveCredentials(this, args);

                                ResolveCredentialsResult credentials = args.Result;

                                if ((credentials != null) && (credentials.Credentials != null))
                                {
                                    if (validateCredentials == null || 
                                        validateCredentials(credentials.Credentials, context))
                                    {
                                        // Perhaps should log this as an error?
                                        if (principal.PrincipalType.IsAssignableFrom(credentials.Credentials.GetType()))
                                        {
                                            if (credentials.Cache)
                                            {
                                                AddCredentials(principal, credentials.Credentials);
                                            }

                                            ret = credentials.Credentials;
                                            break;
                                        }
                                    }
                                }

                                resolved = true;
                            }
                            finally
                            {
                                _resolveLock.TryRemove(principal, out lockObject);
                            }
                        }
                        else // Not our object, but doesn't matter we won't get here until the originator unlocks
                        {
                            ret = GetCachedCredentials(principal);
                            if (ret != null)
                            {
                                resolved = true;
                            }
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Add a credentials object
        /// </summary>
        /// <param name="principal">The security principal</param>        
        /// <param name="credentials">The credentials object (depends on type)</param>
        public void AddCredentials(SecurityPrincipal principal, ICredentialObject credentials)
        {
            lock (_cachedCredentials)
            {
                _cachedCredentials[principal] = credentials;
            }
        }

         /// <summary>
        /// Remove a credentials object
        /// </summary>
        /// <param name="principal">The security principal</param>     
        public void RemoveCredentials(SecurityPrincipal principal)
        {
            lock (_cachedCredentials)
            {
                _cachedCredentials.Remove(principal);
            }
        }

        /// <summary>
        /// Clear the cached credentials data
        /// </summary>
        public void Clear()
        {
            lock (_cachedCredentials)
            {
                _cachedCredentials.Clear();
            }
        }
    }
}
