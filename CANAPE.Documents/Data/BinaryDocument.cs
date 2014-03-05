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
using System.IO;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// Document to contain a set of binary data
    /// </summary>
    [Serializable]
    public class BinaryDocument : BaseDocumentObject
    {
        byte[] _data;

        /// <summary>
        /// Default name
        /// </summary>
        public override string DefaultName
        {
            get { return Properties.Resources.BinaryDocument_DefaultName; }
        }

        /// <summary>
        /// The document's binary data
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDocument() : this(new byte[0])
        {
        }

        /// <summary>
        /// Constructor from a file
        /// </summary>
        /// <param name="data">The file data</param>
        public BinaryDocument(byte[] data)
        {
            _data = data;
        }
    }
}
