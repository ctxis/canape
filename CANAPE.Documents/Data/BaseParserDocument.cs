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
using System.Collections.Generic;
using CANAPE.Parser;
using System.Linq;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// Base document for a parser
    /// </summary>
    [Serializable]
    public abstract class BaseParserDocument : ScriptDocument
    {        
        private List<ParserType> _types;        

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseParserDocument()
        {
            _types = new List<ParserType>();
        }

        /// <summary>
        /// Update the script container
        /// </summary>
        protected abstract void UpdateContainer();

        /// <summary>
        /// Type of types in the parser
        /// </summary>
        public IEnumerable<ParserType> Types { get { return _types.AsReadOnly(); } }

        [Serializable]
        private class SequenceTypeEnumerator : IEnumerable<SequenceParserType>
        {
            BaseParserDocument _document;

            public SequenceTypeEnumerator(BaseParserDocument document)
            {
                _document = document;
            }

            public IEnumerator<SequenceParserType> GetEnumerator()
            {
                return _document._types.OfType<SequenceParserType>().GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        /// Get the list of sequence types
        /// </summary>
        public IEnumerable<SequenceParserType> SequenceTypes
        {
            get { return new SequenceTypeEnumerator(this); }
        }

        /// <summary>
        /// Default name for the document
        /// </summary>
        public override string DefaultName
        {
            get { return "Parser"; }
        }

        /// <summary>
        /// Add a new parser type
        /// </summary>
        /// <param name="type">The type to add</param>
        public void AddParserType(ParserType type)
        {
            _types.Add(type);

            type.DirtyChanged += new EventHandler(type_DirtyChanged);

            UpdateContainer();
            Dirty = true;
        }

        /// <summary>
        /// Remove a parser type from the document
        /// </summary>
        /// <param name="type"></param>
        public void RemoveParserType(ParserType type)
        {
            _types.Remove(type);

            type.DirtyChanged -= new EventHandler(type_DirtyChanged);

            UpdateContainer();
            Dirty = true;
        }

        void type_DirtyChanged(object sender, EventArgs e)
        {
            UpdateContainer();
            Dirty = true;
        }

        /// <summary>
        /// Method called on deserialization
        /// </summary>
        protected override void OnDeserialization()
        {
            base.OnDeserialization();

            foreach (ParserType type in _types)
            {
                type.DirtyChanged += new EventHandler(type_DirtyChanged);
            }
        }

        /// <summary>
        /// Find a type by name
        /// </summary>
        /// <param name="name">The name of the type (case insensitive)</param>
        /// <returns>The type, or null if not found</returns>
        public ParserType FindType(string name)
        {
            return _types.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// The number of types defined
        /// </summary>
        public int TypeCount { get { return _types.Count; } }
    }
}
