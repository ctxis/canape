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
using System.Text.RegularExpressions;
using CANAPE.Utils;

namespace CANAPE.Security
{
    /// <summary>
    /// A class which represents a security principal
    /// </summary>
    /// <remarks>Principals are case insensitive</remarks>
    [Serializable]
    public class SecurityPrincipal
    {
        /// <summary>
        /// The type of principal, used to distinguish between types
        /// </summary>
        public Type PrincipalType { get; private set; }

        /// <summary>
        /// Principal name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Principal realm, for example the domain on which the authentication is used
        /// </summary>
        public string Realm { get; private set; }

        /// <summary>
        /// Equals operator
        /// </summary>
        /// <param name="left">The left object</param>
        /// <param name="right">The right object</param>
        /// <returns>True if they are equal</returns>
        public static bool operator==(SecurityPrincipal left, SecurityPrincipal right)
        {
            bool ret = Object.ReferenceEquals(left, right);

            if (!ret && !Object.ReferenceEquals(left, null))
            {                
                ret = left.Equals(right);                
            }

            return ret;
        }

        /// <summary>
        /// Not Equals operator
        /// </summary>
        /// <param name="left">The left object</param>
        /// <param name="right">The right object</param>
        /// <returns>True if they are equal</returns>
        public static bool operator !=(SecurityPrincipal left, SecurityPrincipal right)
        {
            bool ret = Object.ReferenceEquals(left, right);

            if (!ret && !Object.ReferenceEquals(left, null))
            {
                ret = left.Equals(right);
            }

            return !ret;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="principalType">Principal type</param>
        /// <param name="name">Principal name, can contain wildcards</param>
        /// <param name="realm">Principal realm, can contain wildcards</param>
        /// <exception cref="ArgumentException">Thrown if principalType or realm are empty</exception>
        public SecurityPrincipal(Type principalType, string name, string realm)
        {
            if (principalType == null)
            {
                throw new ArgumentException(Properties.Resources.SecurityPrincipal_MustProvideType, "principalType");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            PrincipalType = principalType;
            Name = name == null ? String.Empty : name.ToLowerInvariant();

            if (String.IsNullOrWhiteSpace(realm))
            {
                throw new ArgumentException(Properties.Resources.SecurityPrincipal_MustProvideRealm, "realm");
            }

            Realm = realm.ToLowerInvariant();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="principalType">Principal type</param>        
        /// <param name="realm">Principal realm</param>
        /// <exception cref="ArgumentException">Thrown if principalType or realm are empty</exception>
        public SecurityPrincipal(Type principalType, string realm) : this(principalType, String.Empty, realm)
        {
        }

        /// <summary>
        /// GetHashCode override
        /// </summary>
        /// <returns>The hash code of the object</returns>
        public override int GetHashCode()
        {
            int hash = 27;

            hash = (hash * 13) + PrincipalType.GetHashCode();
            hash = (hash * 13) + Name.GetHashCode();
            hash = (hash * 13) + Realm.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Equals override
        /// </summary>
        /// <param name="obj">The object to check against</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {           
            if (obj is SecurityPrincipal)
            {
                SecurityPrincipal other = obj as SecurityPrincipal;
                return other.Name.Equals(Name) && other.PrincipalType.Equals(PrincipalType) && other.Realm.Equals(Realm);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>The principal as a display string</returns>
        public override string ToString()
        {
            return String.Format("{0},{1}@{2}", PrincipalType.Name, Name, Realm);                        
        }
    }
}
