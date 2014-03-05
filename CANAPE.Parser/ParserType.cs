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
using System.CodeDom;
using System.ComponentModel;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    /// <summary>
    /// Class to implement a parser type
    /// </summary>
    [Serializable]
    public abstract class ParserType
    {
        /// <summary>
        /// Event sent when type is dirty
        /// </summary>
        [field:NonSerialized]
        public event EventHandler DirtyChanged;

        private string _name;
        private string _description;
        private Guid _displayClass;

        /// <summary>
        /// Method to change the dirty flag
        /// </summary>
        protected virtual void OnDirty()
        {
            if (DirtyChanged != null)
            {
                DirtyChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="name">Name of the type</param>
        /// <param name="uuid">The uuid of the type</param>
        protected ParserType(string name, Guid uuid)
        {
            _name = name;
            Uuid = uuid;
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="name">Name of the type</param>
        protected ParserType(string name) : this(name, Guid.NewGuid())
        {
        }

        /// <summary>
        /// Type uuid
        /// </summary>
        [LocalizedDescription("ParserType_UuidDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Information")]
        public Guid Uuid { get; private set; }

        /// <summary>
        /// The name of the parser type
        /// </summary>
        [Browsable(false)]
        public string Name 
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("ParserType_DescriptionDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Information")]
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("ParserType_DisplayClassDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Display")]
        public Guid DisplayClass
        {
            get { return _displayClass;  }
            set
            {
                if (_displayClass != value)
                {
                    _displayClass = value;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Size of type, if possible to determine
        /// </summary>
        /// <returns>The size of the type, -1 if cannot be determine (e.g. variable length)</returns>
        public abstract int GetSize();

        /// <summary>
        /// Method to get the type defintion
        /// </summary>
        /// <returns></returns>
        public abstract CodeTypeDeclaration GetCodeType();

        /// <summary>
        /// Override of ToString
        /// </summary>
        /// <returns>The name of the type</returns>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Copy the type
        /// </summary>
        /// <returns>The new copied type, has a new UUID</returns>
        public ParserType Copy()
        {
            ParserType ret = GeneralUtils.CloneObject(this);

            ret.Uuid = Guid.NewGuid();

            return ret;
        }
    }
}
