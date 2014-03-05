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
    /// A document repsenting some text
    /// </summary>
    [Serializable]
    public class TextDocument : BaseDocumentObject
    {
        private string _text;

        [NonSerialized]
        private string[] _lines;

        /// <summary>
        /// Default document name
        /// </summary>
        public override string DefaultName
        {
            get { return Properties.Resources.TextDocument_DefaultName; }
        }

        /// <summary>
        /// Get the lines and cache them for later use
        /// </summary>
        /// <returns>The lines of the document</returns>
        public string[] GetLines()
        {
            string[] lines = _lines;

            lock (_text)
            {
                if (lines == null)
                {
                    lines = _text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    _lines = lines;
                }
            }

            return lines;
        }

        /// <summary>
        /// Get or set the text of the document
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text != value)
                {                    
                    _text = value;
                    _lines = null;
                    Dirty = true;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TextDocument() : this(String.Empty)
        {
        }

        /// <summary>
        /// Constructor from file
        /// </summary>
        /// <param name="text">The text</param>
        public TextDocument(string text)
        {
            _text = text;
        }
    }
}
