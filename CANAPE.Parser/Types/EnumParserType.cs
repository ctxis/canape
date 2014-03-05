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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    /// <summary>
    /// Parser type
    /// </summary>
    [Serializable]
    public class EnumParserType : ParserType
    {
        private List<EnumParserTypeEntry> _entries;
        private bool _isFlags;

        /// <summary>
        /// Indicates whether the enumeration is flags
        /// </summary>
        [LocalizedDescription("EnumParserType_IsFlagsDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public bool IsFlags 
        {
            get
            {
                return _isFlags;
            }
        }

        /// <summary>
        /// Find the entry
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EnumParserTypeEntry FindEntry(string name)
        {
            return _entries.Find(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// The entries of the enumeration
        /// </summary>
        [Browsable(false)]
        public IEnumerable<EnumParserTypeEntry> Entries 
        {
            get { return _entries; }
        }

        public void AddEntry(EnumParserTypeEntry entry)
        {
            if (FindEntry(entry.Name) != null)
            {
                throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.SequenceParserType_NameExists, entry.Name));
            }

            if (entry.Parent != null)
            {
                throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.SequenceParserType_MemberAlreadyParented, entry.Name));
            }

            entry.Parent = this;
            entry.DirtyChanged += new EventHandler(entry_DirtyChanged);

            _entries.Add(entry);
            OnDirty();
        }

        void entry_DirtyChanged(object sender, EventArgs e)
        {
            OnDirty();
        }

        public void RemoveEntry(EnumParserTypeEntry entry)
        {
            if (entry.Parent == this)
            {
                entry.Parent = null;
                entry.DirtyChanged -= entry_DirtyChanged;
                _entries.Remove(entry);
                OnDirty();
            }
            else
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.SequenceParserType_NotOurMember);
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public EnumParserType(string name, bool isFlags) : base(name)
        {
            _entries = new List<EnumParserTypeEntry>();
            _isFlags = isFlags;
        }

        /// <summary>
        /// Generate the code for the type
        /// </summary>
        /// <returns></returns>
        public override System.CodeDom.CodeTypeDeclaration GetCodeType()
        {
            CodeTypeDeclaration ret = new CodeTypeDeclaration(Name);

            ret.BaseTypes.Add(typeof(long));
            ret.IsEnum = true;
            if(IsFlags)
            {
                ret.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(FlagsAttribute))));
            }
            ret.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(GuidAttribute)), 
                new CodeAttributeArgument(new CodePrimitiveExpression(Uuid.ToString()))));
            if (DisplayClass != Guid.Empty)
            {
                ret.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(DisplayClassAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(DisplayClass.ToString()))));
            }
            ret.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SerializableAttribute))));
           
            foreach(EnumParserTypeEntry entry in Entries)
            {
                CodeMemberField field = new CodeMemberField(Name, entry.Name);
                field.InitExpression = new CodePrimitiveExpression(entry.Value);
                ret.Members.Add(field);
            }

            return ret;
        }

        public override int GetSize()
        {
            return -1;
        }
    }
}
