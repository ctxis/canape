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

namespace CANAPE.Security
{
    /// <summary>
    /// A class which holds an authentication username, password and domain
    /// </summary>
    [Serializable]
    public class AuthenticationCredentials : ICredentialObject
    {
        /// <summary>
        /// The username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="domain">The domani</param>
        /// <param name="password">The password</param>
        public AuthenticationCredentials(string username, string domain, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (domain == null)
            {
                throw new ArgumentNullException("domain");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            Username = username;
            Domain = domain;
            Password = password;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthenticationCredentials() : this(String.Empty, String.Empty, String.Empty)
        {            
        }

        /// <summary>
        /// Get the principal name, which is the name and/or the domain\username
        /// </summary>
        /// <returns></returns>
        public string GetPrincipalName()
        {
            if (String.IsNullOrWhiteSpace(Domain))
            {
                return Username;
            }
            else
            {
                return String.Format("{0}\\{1}", Domain, Username);
            }
        }
    }
}
