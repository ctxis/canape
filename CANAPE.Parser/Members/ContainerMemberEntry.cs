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
using System.ComponentModel;
using CANAPE.Utils;
using System.Runtime.Serialization;

namespace CANAPE.Parser
{
    /// <summary>
    /// A member entry which contains another (for arrays/enums etc.)
    /// </summary>
    [Serializable]
    public abstract class ContainerMemberEntry : MemberEntry, ICustomTypeDescriptor, IMemberContainer
    {
        private MemberEntry _baseEntry;

        [LocalizedDescription("ContainerMemberEntry_BaseTypeDescription", typeof(Properties.Resources)), 
            LocalizedCategory("Information")]
        public MemberEntry BaseEntry
        {
            get { return _baseEntry; }
        }

        public override Guid DisplayClass
        {
            get
            {
                return _baseEntry.DisplayClass;
            }
            set
            {
                _baseEntry.DisplayClass = value;
            }
        }

        public override EvalExpression ValidateExpression
        {
            get
            {
                return _baseEntry.ValidateExpression;
            }
            set
            {
                _baseEntry.ValidateExpression = value;
            }
        }

        public override EvalExpression OptionalExpression
        {
            get
            {
                return _baseEntry.OptionalExpression;
            }
            set
            {
                _baseEntry.OptionalExpression = value;
            }
        }

        public override bool ReadOnly
        {
            get
            {
                return _baseEntry.ReadOnly;
            }
            set
            {
                _baseEntry.ReadOnly = value;
            }
        }

        public override bool Hidden
        {
            get
            {
                return _baseEntry.Hidden;
            }
            set
            {
                _baseEntry.Hidden = value;
            }
        }

        public override string TypeName
        {
            get
            {
                return _baseEntry.TypeName;
            }
        }

        public override int GetSize()
        {
            return _baseEntry.GetSize();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseType">The base type of the member</param>
        protected ContainerMemberEntry(MemberEntry baseEntry)
            : base(baseEntry.Name, baseEntry.BaseType)
        {
            _baseEntry = baseEntry;
            _baseEntry.DirtyChanged += new EventHandler(_baseEntry_DirtyChanged);            
        }

        void _baseEntry_DirtyChanged(object sender, EventArgs e)
        {
            OnDirty();
        }

        protected override void OnNameChanged()
        {
            if (_baseEntry != null)
            {
                _baseEntry.Name = Name;
            }
        }

        [OnDeserialized]
        void OnDeserializedMethod(StreamingContext context)
        {
            _baseEntry.DirtyChanged += new EventHandler(_baseEntry_DirtyChanged);
        }

        public MemberEntry DetachBaseEntry()
        {
            _baseEntry.DirtyChanged -= _baseEntry_DirtyChanged;

            return _baseEntry;
        }

        #region ICustomTypeDescriptor Implementation

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            List<PropertyDescriptor> descs = new List<PropertyDescriptor>();
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(this, attributes, true);

            foreach (PropertyDescriptor d in coll)
            {
                descs.Add(d);
            }

            if (_baseEntry != null)
            {
                PropertyDescriptorCollection coll2 = TypeDescriptor.GetProperties(_baseEntry, attributes, true);
                foreach (PropertyDescriptor p in coll2)
                {
                    // Only expose properties specific to the entry, not base information
                    if (p.ComponentType == _baseEntry.GetType())
                    {
                        List<Attribute> attrs = new List<Attribute>();
                        foreach (Attribute a in p.Attributes)
                        {
                            attrs.Add(a);
                        }
                        attrs.Add(new CategoryAttribute("Base Type"));

                        CustomPropertyDescriptor custom = new CustomPropertyDescriptor(p, p.Name, attrs.ToArray());
                        custom.PropertyChanged += new EventHandler(custom_PropertyChanged);

                        descs.Add(custom);
                    }
                }
            }

            return new PropertyDescriptorCollection(descs.ToArray());
        }

        void custom_PropertyChanged(object sender, EventArgs e)
        {
            OnDirty();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            ICustomTypeDescriptor desc = (ICustomTypeDescriptor)this;

            return desc.GetProperties(new Attribute[0]);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            if (pd is CustomPropertyDescriptor)
            {
                return _baseEntry;
            }
            else
            {
                return this;
            }
        }

        #endregion
    }
}
