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

namespace CANAPE.Extension
{
    /// <summary>
    /// Attribute for a parser serializer
    /// </summary>
    public sealed class ParserSerializerAttribute : MenuExtensionAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name in menu</param>
        public ParserSerializerAttribute(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="localizedName">Localizable name</param>
        /// <param name="resourceType">The resource type which holds the resources</param>
        public ParserSerializerAttribute(string localizedName, Type resourceType)
            : base(localizedName, resourceType)
        {
        }

        /// <summary>
        /// Register the type
        /// </summary>
        /// <param name="t">The type to register</param>
        public override void RegisterType(Type t)
        {
            ParserSerializerExtensionManager.Instance.RegisterExtension(this, t);
        }

        /// <summary>
        /// Unregister a type
        /// </summary>
        /// <param name="t"></param>
        public override void UnregisterType(Type t)
        {
            ParserSerializerExtensionManager.Instance.UnregisterExtension(this, t);
        }
    }
}
