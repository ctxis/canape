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
    /// Interface for a document
    /// </summary>
    public interface IDocumentObject
    {
        /// <summary>
        /// Get or set document dirty flag
        /// </summary>
        bool Dirty { get; set; }

        /// <summary>
        /// Get or set the name of the document
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Get the default name of a document
        /// </summary>
        string DefaultName { get; }

        /// <summary>
        /// Get the document's UUID
        /// </summary>
        Guid Uuid { get; }

        /// <summary>
        /// Byte array for data storage outside of the normal document
        /// Perhaps useful for storing GUI specific information without needing to pollute the 
        /// objects themselves with this information
        /// </summary>
        byte[] Tag { get; set; }

        /// <summary>
        /// Copy the document object to a new one
        /// </summary>
        /// <returns>The new copy of the document object</returns>
        IDocumentObject Copy();
    }
}
