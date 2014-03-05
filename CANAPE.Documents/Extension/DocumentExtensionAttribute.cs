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
using CANAPE.Extension;

namespace CANAPE.Documents.Extension
{
    /// <summary>
    /// Attribute to add new documents to the application
    /// </summary>
    [Obsolete]
    public sealed class DocumentExtensionAttribute : CANAPEExtensionAttribute
    {
        /// <summary>
        /// The name of the document type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the document type</param>
        public DocumentExtensionAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Register a type for this extension
        /// </summary>
        /// <param name="t"></param>
        public override void RegisterType(Type t)
        {
            DocumentExtensionManager.Instance.RegisterExtension(this, t);
        }

        /// <summary>
        /// Unregister a type for this extension
        /// </summary>
        /// <param name="t"></param>
        public override void UnregisterType(Type t)
        {
            DocumentExtensionManager.Instance.UnregisterExtension(this, t);
        }
    }
}
