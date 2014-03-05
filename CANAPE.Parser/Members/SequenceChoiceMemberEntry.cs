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
using System.ComponentModel;
using System.Linq;
using System.Text;
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Parser
{
    /// <summary>
    /// Represents a member entry which defines choices of sequences
    /// </summary>
    [Serializable]
    public class SequenceChoiceMemberEntry : SequenceMemberEntry, IMemberReference
    {        
        private MemberEntryReference _reference;
        private SequenceChoice[] _choices;
        private SequenceParserType _defaultType;
        private IEnumerable<SequenceParserType> _types;
        private bool _magicParser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the entry</param>
        /// <param name="types">The list of types this member can choose from</param>
        public SequenceChoiceMemberEntry(string name, IEnumerable<SequenceParserType> types)
            : base(name, new SequenceParserType("DUMMY"))
        {
            _choices = new SequenceChoice[0];
            _reference = new MemberEntryReference(this, typeof(MemberEntry));
            _types = types;
        }

        [LocalizedDescription("SequenceChoiceMemberEntry_ReferenceDescription", typeof(Properties.Resources)),
                LocalizedCategory("Behavior"), TypeConverter(typeof(MemberEntryReferenceConverter))]
        public MemberEntryReference Reference
        {
            get { return _reference; }

            set
            {
                if (!_reference.Equals(value))
                {
                    _reference = value;
                    OnDirty();
                }
            }
        }

        [Browsable(false)]
        public override string TypeName
        {
            get
            {
                return "SequenceChoice";
            }
        }

        [Browsable(false)]
        internal IEnumerable<SequenceParserType> ChoiceTypes { get { return _types; } }

        [LocalizedDescription("SequenceChoiceMemberEntry_ChoicesDescription", typeof(Properties.Resources)),
                LocalizedCategory("Behavior")]
        public SequenceChoice[] Choices
        {
            get { return _choices; }
            set
            {
                if (_choices != value)
                {
                    _choices = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("SequenceChoiceMemberEntry_DefaultType", typeof(Properties.Resources)),
                LocalizedCategory("Behavior"), TypeConverter(typeof(SequenceTypeConverter))]
        public SequenceParserType DefaultType
        {
            get { return _defaultType; }
            set
            {
                if (_defaultType != value)
                {
                    _defaultType = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("SequenceChoiceMemberEntry_MagicParserDescription", typeof(Properties.Resources)),
                LocalizedCategory("Behavior")]
        public bool MagicParser
        {
            get { return _magicParser; }
            set
            {
                if (_magicParser != value)
                {
                    _magicParser = value;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Get type reference
        /// </summary>
        /// <returns>Returns type reference</returns>
        public override CodeTypeReference GetTypeReference()
        {
            return new CodeTypeReference(typeof(IStreamTypeParser));
        }

        public override CodeExpression GetReaderExpression(CodeExpression reader)
        {
            CodeExpression refExpr;

            if (_magicParser)
            {
                List<CodeExpression> args = new List<CodeExpression>();

                args.Add(reader);

                foreach (SequenceChoice choice in _choices)
                {
                    if (choice.SequenceType != null)
                    {                        
                        args.Add(CodeGen.GetTypeOf(new CodeTypeReference(choice.SequenceType.Name)));
                    }
                }

                if (DefaultType != null)
                {                    
                    args.Add(CodeGen.GetTypeOf(new CodeTypeReference(DefaultType.Name)));
                }

                return CodeGen.CallMethod(CodeGen.GetThis(), "RSC", args.ToArray());
            }
            else
            {
                if (_reference.IsValid())
                {
                    refExpr = CodeGen.GetField(CodeGen.GetThis(), _reference.GetNames());
                }
                else
                {
                    refExpr = CodeGen.GetPrimitive(null);
                }

                List<CodeExpression> args = new List<CodeExpression>();

                args.Add(reader);
                args.Add(refExpr);

                foreach (SequenceChoice choice in _choices)
                {
                    if ((choice.BooleanExpression.IsValid) && (choice.SequenceType != null))
                    {
                        args.Add(CodeGen.GetPrimitive(choice.BooleanExpression.Expression));
                        args.Add(CodeGen.GetTypeOf(new CodeTypeReference(choice.SequenceType.Name)));
                    }
                }

                if (DefaultType != null)
                {
                    args.Add(CodeGen.GetPrimitive("true"));
                    args.Add(CodeGen.GetTypeOf(new CodeTypeReference(DefaultType.Name)));
                }

                return CodeGen.CallMethod(CodeGen.GetThis(), "RSC", args.ToArray());
            }
        }
    }
}
