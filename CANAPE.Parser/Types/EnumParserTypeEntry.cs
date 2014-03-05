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

namespace CANAPE.Parser
{
    /// <summary>
    /// A single entry for an enumeration parser type
    /// </summary>
    [Serializable]
    public sealed class EnumParserTypeEntry
    {
        private string _name;
        private long _value;

        internal EnumParserType Parent { get; set; }

        /// <summary>
        /// Event for when the status changes
        /// </summary>
        [field: NonSerialized]
        public event EventHandler DirtyChanged;

        private void OnDirty()
        {
            if (DirtyChanged != null)
            {
                DirtyChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// The name of the entry
        /// </summary>
        public string Name 
        {
            get { return _name; }

            set
            {
                if (_name != value)
                {
                    if (!ParserUtils.IsValidIdentifier(value))
                    {
                        throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.MemberEntry_InvalidName, value));
                    }

                    if (Parent != null)
                    {
                        if (Parent.FindEntry(value) != null)
                        {
                            throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.MemberEntry_NameAlreadyExists, value));
                        }
                    }

                    _name = value;
                    OnDirty();
                }
            }
        }
        /// <summary>
        /// The value of the entry
        /// </summary>
        public long Value 
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of entry</param>
        /// <param name="value">Value of entry</param>
        public EnumParserTypeEntry(string name, long value)
        {
            Name = name;
            Value = value;
        }
    }
}
