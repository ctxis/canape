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


namespace CANAPE.Security
{
    /// <summary>
    /// Class to hold the result of a credentials resolve
    /// </summary>
    public sealed class ResolveCredentialsResult
    {
        /// <summary>
        /// The credentials object
        /// </summary>
        public ICredentialObject Credentials { get; private set; }

        /// <summary>
        /// Set to ensure the credentials will be cached
        /// </summary>
        public bool Cache { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">The credentials</param>
        public ResolveCredentialsResult(ICredentialObject credentials)
            : this(credentials, true)
        {
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentials">The credentials</param>
        /// <param name="cache">Whether to cache the credentials</param>
        public ResolveCredentialsResult(ICredentialObject credentials, bool cache)
        {
            Credentials = credentials;
            Cache = cache;
        }
    }

}
