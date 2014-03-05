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

namespace CANAPE.Documents
{
    /// <summary>
    /// Attribute to define a document name and category
    /// </summary>
    [Serializable]
    public sealed class DocumentTypeAttribute : Attribute
    {
        string _name;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of document</param>
        public DocumentTypeAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Get or set document category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Get name of the document
        /// </summary>
        public string Name { get { return _name; } }
    }
}
