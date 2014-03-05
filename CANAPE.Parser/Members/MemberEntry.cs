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
using System.Runtime.Serialization;
using CANAPE.DataFrames;
using CANAPE.Utils;
using NCalc;
using NCalc.Domain;

namespace CANAPE.Parser
{
    /// <summary>
    /// Base class for a member entry
    /// </summary>
    [Serializable]
    public abstract class MemberEntry
    {
        internal SequenceParserType Parent { get; set; }

        private string _name;
        private string _description;
        private bool _hidden;
        private Guid _displayClass;
        private bool _readOnly;
        private string _optional;
        private string _validate;

        /// <summary>
        /// Event for when the status changes
        /// </summary>
        [field: NonSerialized]
        public event EventHandler DirtyChanged;

        /// <summary>
        /// Overridable method for on deserialized
        /// </summary>
        protected virtual void OnDeserialized()
        {
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
        }

        /// <summary>
        /// Overridable method called when dirty
        /// </summary>
        protected virtual void OnDirty()
        {
            if (DirtyChanged != null)
            {
                DirtyChanged(this, new EventArgs());
            }            
        }

        /// <summary>
        /// Called when the name changes
        /// </summary>
        protected virtual void OnNameChanged()
        {
            // Do nothing
        }        

        /// <summary>
        /// Name of the mmeber
        /// </summary>
        [LocalizedDescription("MemberEntry_NameDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public string Name 
        {
            get
            {
                return _name;
            }

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
                        MemberEntry member = Parent.FindMember(value);

                        if ((member != null) && (member != this))
                        {
                            throw new ArgumentException(String.Format(CANAPE.Parser.Properties.Resources.MemberEntry_NameAlreadyExists, value));
                        }
                    }

                    _name = value;
                    OnDirty();
                    OnNameChanged();
                }
            }
        }

        /// <summary>
        /// Whether the member is hidden
        /// </summary>
        [LocalizedDescription("MemberEntry_HiddenDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Behavior")]
        public virtual bool Hidden
        {
            get { return _hidden; }
            set
            {
                if (_hidden != value)
                {
                    _hidden = value;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Whether the member is marked as read only
        /// </summary>
        [LocalizedDescription("MemberEntry_ReadOnlyDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public virtual bool ReadOnly
        {
            get { return _readOnly; }

            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    OnDirty();
                }
            }
        }

        [LocalizedDescription("MemberEntry_DisplayClassDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Display")]
        public virtual Guid DisplayClass
        {
            get { return _displayClass; }
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
        /// Unique id for this entry
        /// </summary>
        [LocalizedDescription("MemberEntry_UuidDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Information")]
        public Guid Uuid { get; private set; }

        /// <summary>
        /// The underlying parser type
        /// </summary>
        [Browsable(false)]
        public ParserType BaseType { get; protected set; }

        [LocalizedDescription("MemberEntry_DescriptionDescription", typeof(Properties.Resources)), 
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

        /// <summary>
        /// Get the name of the type this entry contains
        /// </summary>
        [LocalizedDescription("MemberEntry_TypeNameDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Information")]
        public virtual string TypeName { get { return BaseType.Name; } }


        /// <summary>
        /// Get an expression which is used to specify optional behaviour
        /// </summary>
        [LocalizedDescription("MemberEntry_OptionalExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public virtual EvalExpression OptionalExpression
        {
            get
            {
                return new EvalExpression(_optional ?? String.Empty);
            }

            set
            {
                if (value.Expression != _optional)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _optional = value.Expression;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Get an expression which is used to specify optional behaviour
        /// </summary>
        [LocalizedDescription("MemberEntry_ValidateExpressionDescription", typeof(Properties.Resources)),
            LocalizedCategory("Behavior")]
        public virtual EvalExpression ValidateExpression
        {
            get
            {
                return new EvalExpression(_validate ?? String.Empty);
            }

            set
            {
                if (value.Expression != _validate)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _validate = value.Expression;
                    OnDirty();
                }
            }
        }

        /// <summary>
        /// Get the size of the entry in bytes
        /// </summary>
        /// <returns>The size of the entry in bytes</returns>
        public virtual int GetSize()
        {
            return BaseType.GetSize();
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the member</param>
        /// <param name="baseType">The base type for this member entry</param>
        protected MemberEntry(string name, ParserType baseType)
        {
            // TODO: Validate the name to make sure it is a valid identifier
            Name = name;
            Uuid = Guid.NewGuid();
            BaseType = baseType;
        }

        /// <summary>
        /// Method to get the type reference for this member, needs to be implemented
        /// </summary>
        /// <returns>The type reference</returns>
        public abstract CodeTypeReference GetTypeReference();

        /// <summary>
        /// Get the member declaration for the sequence
        /// </summary>
        /// <returns></returns>
        public virtual CodeMemberField GetMemberDeclaration()
        {
            CodeMemberField field = new CodeMemberField(GetTypeReference(), Name);
            field.Attributes = MemberAttributes.Public;
            
            return field;
        }

        /// <summary>
        /// Get the serializer method for this type
        /// </summary>
        /// <returns></returns>
        public virtual CodeMemberMethod GetSerializerMethod()
        {
            CodeMemberMethod method = new CodeMemberMethod();

            method.Attributes = MemberAttributes.Private;
            method.Name = String.Format("{0}_Serialize", Name);
            method.ReturnType = null;
            method.Parameters.Add(CodeGen.GetParameter(typeof(DataWriter), "writer"));
            method.Parameters.Add(CodeGen.GetParameter(GetTypeReference(), "obj"));            

            return method;
        }

        /// <summary>
        /// Get the 
        /// </summary>
        /// <returns></returns>
        public virtual CodeMemberMethod GetDeserializerMethod()
        {
            CodeMemberMethod method = new CodeMemberMethod();

            method.Attributes = MemberAttributes.Private;
            method.Name = String.Format("{0}_Deserialize", Name);
            method.ReturnType = GetTypeReference();
            method.Parameters.Add(CodeGen.GetParameter(typeof(DataReader), "reader"));
            
            return method;
        }

        /// <summary>
        /// Get a method which is called pre serialization
        /// </summary>
        /// <returns>The method member</returns>
        public virtual CodeMemberMethod GetPreSerializeMethod()
        {
            CodeMemberMethod method = new CodeMemberMethod();

            method.Attributes = MemberAttributes.Private;
            method.Name = String.Format("{0}_PreSerialize", Name);

            return method;
        }

        /// <summary>
        /// Get a method which is called post serialization
        /// </summary>
        /// <returns>The method member</returns>
        public virtual CodeMemberMethod GetPostSerializeMethod()
        {
            CodeMemberMethod method = new CodeMemberMethod();

            method.Attributes = MemberAttributes.Private;
            method.Name = String.Format("{0}_PostSerialize", Name);
            
            return method;
        }

        /// <summary>
        /// Get a method which is called pre deserialization
        /// </summary>
        /// <returns>The method member</returns>
        public virtual CodeMemberMethod GetPreDeserializeMethod()
        {
            CodeMemberMethod method = new CodeMemberMethod();

            method.Attributes = MemberAttributes.Private;
            method.Name = String.Format("{0}_PreDeserialize", Name);                      

            return method;
        }

        /// <summary>
        /// Get a method which is called post deserialization
        /// </summary>
        /// <returns>The method member</returns>
        public virtual CodeMemberMethod GetPostDeserializeMethod()
        {
            CodeMemberMethod method = new CodeMemberMethod();

            method.Attributes = MemberAttributes.Private;
            method.Name = String.Format("{0}_PostDeserialize", Name);                      

            return method;
        }        

        internal ParserType GetReference(string reference)
        {
            ParserType ret = null;
            string[] vals = reference.Split('/');
            if (Parent != null)
            {
                SequenceParserType currParent = Parent;                

                for (int i = 0; i < vals.Length - 1; ++i )
                {
                    MemberEntry ent = currParent.FindMember(vals[i]);
                                        
                    SequenceParserType s = ent.BaseType as SequenceParserType;
                    if (s == null)
                    {
                        currParent = null;
                        break;
                    }
                    else
                    {
                        currParent = s;
                    }
                }

                if (currParent != null)
                {
                    MemberEntry ent = currParent.FindMember(vals[vals.Length - 1]);
                }
            }            

            return ret;
        }
    }
}
