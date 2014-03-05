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
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using CANAPE.DataFrames;
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Parser
{
    /// <summary>
    /// Parser type defining a structure/class
    /// </summary>
    [Serializable]
    public class SequenceParserType : ParserType
    {
        private List<MemberEntry> _members;
        private Endian _defaultEndian;
        private string _formatString;
        private string _preserializeExpression;
        private string _postdeserializeExpression;
        private bool _private;

        /// <summary>
        /// Members of this structure
        /// </summary>
        [Browsable(false)]
        public IEnumerable<MemberEntry> Members 
        {
            get
            {
                return _members.AsReadOnly();
            }
        }

        [LocalizedDescription("SequenceParserType_PrivateDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public bool Private
        {
            get { return _private; }
            set
            {
                if (_private != value)
                {
                    _private = value;
                    OnDirty();
                }                
            }
        }

        [LocalizedDescription("StreamParserType_FormatStringDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public string FormatString
        {
            get
            {
                return _formatString;
            }

            set
            {
                if (_formatString != value)
                {
                    _formatString = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("SequenceParserType_PreserializeExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression PreserializeExpression
        {
            get
            {
                return new EvalExpression(_preserializeExpression);
            }

            set
            {
                if (value.Expression != _preserializeExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _preserializeExpression = value.Expression;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("SequenceParserType_PostDeserializeExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public EvalExpression PostDeserializeExpression
        {
            get
            {
                return new EvalExpression(_postdeserializeExpression);
            }

            set
            {
                if (value.Expression != _postdeserializeExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _postdeserializeExpression = value.Expression;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the type</param>
        public SequenceParserType(string name) : base(name)
        {
            _members = new List<MemberEntry>();
        }
        
        /// <summary>
        /// Add a new member entry
        /// </summary>
        /// <param name="entry">The entry to add</param>
        /// <exception cref="ArgumentException">Thrown if the member name already exists</exception>
        public void AddMember(MemberEntry entry)
        {
            if (FindMember(entry.Name) != null)
            {
                throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.SequenceParserType_NameExists, entry.Name));            
            }

            if (entry.Parent != null)
            {
                throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.SequenceParserType_MemberAlreadyParented, entry.Name));
            }

            entry.Parent = this;
            entry.DirtyChanged += new EventHandler(entry_DirtyChanged);
            _members.Add(entry);            
            OnDirty();
        }

        void entry_DirtyChanged(object sender, EventArgs e)
        {
            OnDirty();
        }

        /// <summary>
        /// Remove a member entry
        /// </summary>
        /// <param name="entry">The entry to remove</param>
        public void RemoveMember(MemberEntry entry)
        {
            if (entry.Parent == this)
            {
                entry.Parent = null;
                entry.DirtyChanged -= entry_DirtyChanged;
                _members.Remove(entry);
                OnDirty();
            }
            else
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.SequenceParserType_NotOurMember);
            }
        }

        /// <summary>
        /// Replace an exist member
        /// </summary>
        /// <param name="oldEntry">The old entry to replace</param>
        /// <param name="newEntry">The new entry to replace with</param>
        public void ReplaceMember(MemberEntry oldEntry, MemberEntry newEntry)
        {
            if (oldEntry.Parent == this)
            {
                int index = _members.IndexOf(oldEntry);

                if (newEntry.Parent != null)
                {
                    throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.SequenceParserType_MemberAlreadyParented, newEntry.Name));
                }

                if (index >= 0)
                {
                    oldEntry.Parent = null;
                    oldEntry.DirtyChanged -= entry_DirtyChanged;
                    newEntry.Parent = this;
                    newEntry.DirtyChanged += entry_DirtyChanged;

                    _members[index] = newEntry;

                    OnDirty();
                }
            }
            else
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.SequenceParserType_NotOurMember);
            }
        }

        /// <summary>
        /// Moves a member in the order up a location
        /// </summary>
        /// <param name="entry">The entry to move</param>
        public void MoveMemberUp(MemberEntry entry)
        {
            if (entry.Parent == this)
            {
                int index = _members.IndexOf(entry);

                if (index > 0)
                {
                    _members.RemoveAt(index);
                    _members.Insert(index - 1, entry);
                }

                OnDirty();
            }
            else
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.SequenceParserType_NotOurMember);
            }
        }

        /// <summary>
        /// Moves a member in the order down a location
        /// </summary>
        /// <param name="entry">The entry to move</param>
        public void MoveMemberDown(MemberEntry entry)
        {
            if (entry.Parent == this)
            {
                int index = _members.IndexOf(entry);

                if ((index >= 0) && (index < (_members.Count - 1)))
                {
                    _members.RemoveAt(index);
                    _members.Insert(index + 1, entry);
                }

                OnDirty();
            }
            else
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.SequenceParserType_NotOurMember);
            }
        }

        /// <summary>
        /// Find a member by name
        /// </summary>
        /// <param name="name">The name of the member (case insensitive)</param>
        /// <returns>The member entry, null if not found</returns>
        public MemberEntry FindMember(string name)
        {
            foreach (MemberEntry ent in _members)
            {
                if (ent.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return ent;
                }
            }

            return null;
        }

        /// <summary>
        /// Find a member by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the memeber</param>
        /// <returns>The member entry, null if could not find it</returns>
        public MemberEntry FindMember(Guid uuid)
        {
            foreach(MemberEntry ent in _members)
            {
                if(ent.Uuid == uuid)
                {
                    return ent;
                }
            }
            
            return null;
        }

        /// <summary>
        /// The default endian for new members (advisory)
        /// </summary>
        [LocalizedDescription("SequenceParserType_DefaultEndian", typeof(Properties.Resources)), Category("Behavior")]
        public Endian DefaultEndian 
        {
            get
            {
                return _defaultEndian;
            }

            set
            {
                if (_defaultEndian != value)
                {
                    _defaultEndian = value;
                    OnDirty();
                }
            }
        }

        private CodeArgumentReferenceExpression GetReader()
        {
            return CodeGen.GetArgument("_reader");
        }

        private CodeArgumentReferenceExpression GetWriter()
        {
            return CodeGen.GetArgument("_writer");
        }

        /// <summary>
        /// Method to generate the code to implement the type
        /// </summary>
        /// <returns></returns>
        public override CodeTypeDeclaration GetCodeType()
        {
            CodeTypeDeclaration type = new CodeTypeDeclaration(Name);

            type.IsClass = true;          
            type.BaseTypes.Add(new CodeTypeReference(typeof(BaseParser)));
            type.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            type.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(GuidAttribute)), 
                new CodeAttributeArgument(new CodePrimitiveExpression(Uuid.ToString()))));

            if (DisplayClass != Guid.Empty)
            {
                type.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(DisplayClassAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(DisplayClass.ToString()))));
            }

            if (!String.IsNullOrWhiteSpace(_formatString))
            {
                type.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(FormatStringAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(_formatString))));
            }

            CodeConstructor defaultConstructor = new CodeConstructor();
            defaultConstructor.Attributes = MemberAttributes.Public;
            type.Members.Add(defaultConstructor);

            CodeConstructor fromStreamConstructor = new CodeConstructor();
            fromStreamConstructor.Attributes = MemberAttributes.Public;
            fromStreamConstructor.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(DataReader)), "reader"));
            fromStreamConstructor.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(StateDictionary)), "state"));
            fromStreamConstructor.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Logger)), "logger"));

            fromStreamConstructor.BaseConstructorArgs.Add(CodeGen.GetArgument("reader"));
            fromStreamConstructor.BaseConstructorArgs.Add(CodeGen.GetArgument("state"));
            fromStreamConstructor.BaseConstructorArgs.Add(CodeGen.GetArgument("logger"));
            type.Members.Add(fromStreamConstructor);

            CodeMemberMethod preSerializer = new CodeMemberMethod();
            preSerializer.Name = "PreSerializer";
            preSerializer.Attributes = MemberAttributes.Private;
 
            CodeMemberMethod postSerializer = new CodeMemberMethod();
            postSerializer.Name = "PostSerializer";
            postSerializer.Attributes = MemberAttributes.Private;
            
            CodeMemberMethod preDeserializer = new CodeMemberMethod();
            preDeserializer.Name = "PreDeserializer";
            preDeserializer.Attributes = MemberAttributes.Private;
            
            CodeMemberMethod postDeserializer = new CodeMemberMethod();
            postDeserializer.Name = "PostDeserializer";
            postDeserializer.Attributes = MemberAttributes.Private;
            
            CodeMemberMethod fromStreamMethod = new CodeMemberMethod();
            fromStreamMethod.Name = "FromStream";
            fromStreamMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;      

            CodeMemberMethod toStreamMethod = new CodeMemberMethod();
            toStreamMethod.Name = "ToStream";
            toStreamMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;

            if (PreserializeExpression.IsValid)
            {
                toStreamMethod.Statements.Add(CodeGen.CallMethod(CodeGen.GetThis(), "Resolve", CodeGen.GetPrimitive(PreserializeExpression.Expression)));
            }
            
            foreach (MemberEntry entry in Members)
            {
                IMemberReaderWriter readerWriter = entry as IMemberReaderWriter;
                CodeMemberField field = entry.GetMemberDeclaration();
                if (entry.Hidden)
                {
                    field.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(HiddenMemberAttribute)), 
                        new CodeAttributeArgument(CodeGen.GetPrimitive(true))));
                }

                if (entry.ReadOnly)
                {
                    field.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(ReadOnlyMemberAttribute)),
                        new CodeAttributeArgument(CodeGen.GetPrimitive(true))));
                }

                if (entry.DisplayClass != Guid.Empty)
                {
                    field.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(DisplayClassAttribute)),
                        new CodeAttributeArgument(CodeGen.GetPrimitive(entry.DisplayClass.ToString()))));
                }

                type.Members.Add(field);

                CodeMemberMethod serMethod = entry.GetSerializerMethod();
                CodeMemberMethod deserMethod = entry.GetDeserializerMethod();
                CodeMemberMethod preserMethod = entry.GetPreSerializeMethod();
                CodeMemberMethod postserMethod = entry.GetPostSerializeMethod();
                CodeMemberMethod predeserMethod = entry.GetPreDeserializeMethod();
                CodeMemberMethod postdeserMethod = entry.GetPostDeserializeMethod();

                if (readerWriter == null)
                {
                    type.Members.Add(serMethod);
                    type.Members.Add(deserMethod);
                }

                if (preserMethod.Statements.Count > 0)
                {
                    type.Members.Add(preserMethod);
                    preSerializer.Statements.Add(CodeGen.CallMethod(CodeGen.GetThis(), preserMethod.Name));
                }

                if (postserMethod.Statements.Count > 0)
                {
                    type.Members.Add(postserMethod);
                    postSerializer.Statements.Add(CodeGen.CallMethod(CodeGen.GetThis(), postserMethod.Name));
                }

                if (predeserMethod.Statements.Count > 0)
                {
                    type.Members.Add(predeserMethod);
                    preDeserializer.Statements.Add(CodeGen.CallMethod(CodeGen.GetThis(), 
                            predeserMethod.Name));
                }

                if (postdeserMethod.Statements.Count > 0)
                {
                    type.Members.Add(postdeserMethod);
                    postDeserializer.Statements.Add(CodeGen.CallMethod(CodeGen.GetThis(), 
                            postserMethod.Name));
                }

                CodeStatement readStatement = null;
                CodeStatement writeStatement = null;

                if (readerWriter != null)
                {
                    CodeExpression readExpression = readerWriter.GetReaderExpression(GetReader());

                    if (entry.ValidateExpression.IsValid)
                    {
                        readExpression = CodeGen.CallMethod(CodeGen.GetThis(), "V", readExpression, CodeGen.GetPrimitive(entry.ValidateExpression.Expression));
                    }                  

                    readStatement = 
                        CodeGen.GetAssign(CodeGen.GetThisField(field.Name), readExpression);

                    writeStatement =
                        new CodeExpressionStatement(readerWriter.GetWriterExpression(GetWriter(), CodeGen.GetThisField(field.Name)));
                }
                else
                {
                    CodeExpression readExpression = CodeGen.CallMethod(CodeGen.GetThis(), deserMethod.Name, GetReader());

                    if (entry.ValidateExpression.IsValid)
                    {
                        readExpression = CodeGen.CallMethod(CodeGen.GetThis(), "V", readExpression, CodeGen.GetPrimitive(entry.ValidateExpression.Expression));
                    } 

                    readStatement = 
                        CodeGen.GetAssign(CodeGen.GetThisField(field.Name), readExpression);

                    writeStatement =
                        new CodeExpressionStatement(CodeGen.CallMethod(CodeGen.GetThis(), serMethod.Name,
                        GetWriter(),                        
                        CodeGen.GetThisField(field.Name)));
                }

                if (entry.OptionalExpression.IsValid)
                {
                    readStatement = CodeGen.GetIf(entry.OptionalExpression.GetCheckExpression(), new[] { readStatement });
                    writeStatement = CodeGen.GetIf(entry.OptionalExpression.GetCheckExpression(), new[] { writeStatement });
                }
                              
                fromStreamMethod.Statements.Add(readStatement);
                toStreamMethod.Statements.Add(writeStatement);
            }

            if (preDeserializer.Statements.Count > 0)
            {
                fromStreamMethod.Statements.Insert(0, CodeGen.GetStatement(CodeGen.CallMethod(CodeGen.GetThis(), "PreDeserializer")));
                type.Members.Add(preDeserializer);
            }
           
            toStreamMethod.Statements.Add(new CodeMethodInvokeExpression(GetWriter(), "Flush"));
            if (preSerializer.Statements.Count > 0)
            {
                toStreamMethod.Statements.Insert(0, CodeGen.GetStatement(CodeGen.CallMethod(CodeGen.GetThis(), "PreSerializer")));
                type.Members.Add(preSerializer);
            }

            if (postDeserializer.Statements.Count > 0)
            {
                fromStreamMethod.Statements.Add(CodeGen.CallMethod(CodeGen.GetThis(), "PostDeserializer"));
                type.Members.Add(postDeserializer);
            }

            if (PostDeserializeExpression.IsValid)
            {
                fromStreamMethod.Statements.Add(CodeGen.CallMethod(CodeGen.GetThis(), "Resolve", CodeGen.GetPrimitive(PostDeserializeExpression.Expression)));
            }
            
            if (postSerializer.Statements.Count > 0)
            {
                toStreamMethod.Statements.Add(CodeGen.CallMethod(CodeGen.GetThis(), "PostSerializer"));
                type.Members.Add(postSerializer);
            }

            type.Members.Add(fromStreamMethod);
            type.Members.Add(toStreamMethod);
            
            return type;
        }

        /// <summary>
        /// Get the size in bytes of this sequence, if known
        /// </summary>
        /// <returns>The size in bytes</returns>
        public override int GetSize()
        {
            int ret = 0;

            try
            {
                foreach (MemberEntry ent in _members)
                {
                    int curr = ent.GetSize();
                    if (curr > 0)
                    {
                        ret += curr;
                    }
                }
            }
            catch (StackOverflowException)
            {
                // It is possible to end up in a recursive loop here, return -1
                ret = -1;
            }

            return ret;
        }

        /// <summary>
        /// Get number of members
        /// </summary>
        public int MemberCount { get { return _members.Count; } }

        [OnDeserialized]
        private void OnDeserializedMethod(StreamingContext context)
        {
            foreach (MemberEntry ent in _members)
            {
                ent.DirtyChanged += entry_DirtyChanged;
            }
        }
    }
}
