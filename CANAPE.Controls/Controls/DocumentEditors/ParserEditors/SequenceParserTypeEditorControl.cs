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
using System.ComponentModel;
using System.Windows.Forms;
using CANAPE.DataFrames;
using CANAPE.Documents.Data;
using CANAPE.Parser;

namespace CANAPE.Controls.DocumentEditors.ParserEditors
{
    public partial class SequenceParserTypeEditorControl : UserControl
    {        
        SequenceParserType _type;
        bool _isLoaded;        

        public SequenceParserTypeEditorControl()
        {            
            InitializeComponent();
        }

        private void SetupType()
        {
            listViewEntries.BeginUpdate();
            listViewEntries.Items.Clear();

            foreach(MemberEntry entry in _type.Members)
            {
                AddMember(entry, false);
            }
            listViewEntries.EndUpdate();

            propertyGrid.SelectedObject = _type;
        }

        private void UpdateType()
        {
            listViewEntries.BeginUpdate();
            foreach (ListViewItem item in listViewEntries.Items)
            {
                MemberEntry entry = item.Tag as MemberEntry;

                if (entry != null)
                {
                    item.Text = entry.Name;
                    item.SubItems[1].Text = entry.TypeName;
                    item.SubItems[2].Text = entry.GetSize().ToString();
                }
            }
            listViewEntries.EndUpdate();
        }

        private ListViewItem AddMember(MemberEntry entry, bool editName)
        {
            ListViewItem item = listViewEntries.Items.Add(entry.Name);
            item.SubItems.Add(entry.TypeName);
            item.SubItems.Add(entry.GetSize().ToString());
            item.Tag = entry;

            if (editName)
            {
                item.BeginEdit();
            }

            return item;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ParserDocument Document
        {
            get;
            set;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SequenceParserType CurrentType 
        {
            get
            {
                return _type;
            }

            set
            {                
                _type = value;
                if ((_type != null) && (_isLoaded))
                {
                    SetupType();
                }             
            }
        }

        public void SetRestricted()
        {
            bitVariableIntToolStripMenuItem.Visible = false;
            floatToolStripMenuItem.Visible = false;
            doubleToolStripMenuItem.Visible = false;
            convertToEnumToolStripMenuItem.Visible = false;
            lengthReferenceToolStripMenuItem.Visible = false;
            lengthReferenceToolStripMenuItem1.Visible = false;
            lengthReferenceToolStripMenuItem2.Visible = false;
            convertToBooleanToolStripMenuItem.Visible = false;            
        }

        private void SequenceEditorControl_Load(object sender, EventArgs e)
        {
            _isLoaded = true;
            if (_type != null)
            {
                SetupType();
            }
        }

        private void UpdateSelection()
        {
            if (listViewEntries.SelectedItems.Count <= 0)
            {
                propertyGrid.SelectedObject = _type;
            }
            else if (listViewEntries.SelectedItems.Count == 1)
            {
                propertyGrid.SelectedObject = listViewEntries.SelectedItems[0].Tag;
            }
            else
            {
                object[] selected = new object[listViewEntries.SelectedItems.Count];

                for (int i = 0; i < selected.Length; ++i)
                {
                    selected[i] = listViewEntries.SelectedItems[i].Tag;
                }

                propertyGrid.SelectedObjects = selected;
            }
        }

        private void listViewEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelection();
        }


        private string GetMemberName()
        {
            string ret = null;

            // Hopefully 10000 entires might be more than enough
            for (int i = _type.MemberCount; i < 10000; ++i)
            {
                string curr = String.Format("Member_{0}", i);

                if (_type.FindMember(curr) == null)
                {
                    ret = curr;
                    break;
                }
            }

            if (ret == null)
            {
                // Generate a random name, could still fail :)
                ret = "_"+Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            }

            return ret;
        }

        private void AddIntegerType(string type)
        {
            if (_type != null)
            {
                Type t = Type.GetType(type);

                if (t == null)
                {
                    t = typeof(DataReader).Assembly.GetType(type);
                }

                if (t != null)
                {
                    MemberEntry entry = new IntegerPrimitiveMemberEntry(GetMemberName(), t, _type.DefaultEndian);
                    _type.AddMember(entry);
                    AddMember(entry, true);
                }
            }
        }

        private void AddFloatType(string type)
        {
            if (_type != null)
            {
                Type t = Type.GetType(type);

                if (t != null)
                {
                    MemberEntry entry = new FloatPrimitiveMemberEntry(GetMemberName(), t, _type.DefaultEndian);
                    _type.AddMember(entry);
                    AddMember(entry, true);
                }
            }
        }

        private void addIntegerType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            AddIntegerType(item.Tag.ToString());
        }

        private void addFloatType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            AddFloatType(item.Tag.ToString());
        }

        private void bitVariableIntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new SevenBitVariableIntMemberEntry(GetMemberName());
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            convertToEnumToolStripMenuItem.DropDownItems.Clear();
            addSequenceToolStripMenuItem.DropDownItems.Clear();

            if (Document != null)
            {
                foreach (ParserType type in Document.Types)
                {
                    if (type is EnumParserType)
                    {
                        ToolStripItem item = convertToEnumToolStripMenuItem.DropDownItems.Add(type.Name);
                        item.Tag = type;
                        item.Click += new EventHandler(addEnum_Click);
                    }
                    else if (type is SequenceParserType)
                    {
                        // Ensure we cannot add the current sequence to itself (not sure if this is a use case?)
                        // Technically can still cause it issues
                        if (_type != type)
                        {
                            ToolStripItem item = addSequenceToolStripMenuItem.DropDownItems.Add(type.Name);
                            item.Tag = type;
                            item.Click += new EventHandler(addSequence_Click);
                        }
                    }
                }
            }

            if (convertToEnumToolStripMenuItem.DropDownItems.Count == 0)
            {
                convertToEnumToolStripMenuItem.Enabled = false;
            }
            else
            {
                convertToEnumToolStripMenuItem.Enabled = true;
            }

            if (addSequenceToolStripMenuItem.DropDownItems.Count == 0)
            {
                addSequenceToolStripMenuItem.Enabled = false;
            }
            else
            {
                addSequenceToolStripMenuItem.Enabled = true;
            }

            // Limit to only primitive types atm
            if (listViewEntries.SelectedItems.Count > 0)
            {
                MemberEntry entry = listViewEntries.SelectedItems[0].Tag as MemberEntry;
                bool isArray = (entry is PrimitiveMemberEntry) || (entry is IMemberReaderWriter);
                convertToArrayToolStripMenuItem.Enabled = isArray;
                convertToBooleanToolStripMenuItem.Enabled = entry is IntegerPrimitiveMemberEntry;
                convertToEnumToolStripMenuItem.Enabled = entry is IntegerPrimitiveMemberEntry;
                convertToBaseToolStripMenuItem.Enabled = entry is ContainerMemberEntry;
                convertToCalculatedToolStripMenuItem.Enabled = (entry is IMemberReaderWriter) && !(entry is CalculatorMemberEntry);
                convertToSizedToolStripMenuItem.Enabled = entry is IMemberReaderWriter;
                terminatedToolStripMenuItem.Enabled = entry is IMemberReaderWriter;
            }
            else
            {
                convertToArrayToolStripMenuItem.Enabled = false;
                convertToBooleanToolStripMenuItem.Enabled = false;
                convertToEnumToolStripMenuItem.Enabled = false;
                convertToBaseToolStripMenuItem.Enabled = false;
                convertToCalculatedToolStripMenuItem.Enabled = false;
                convertToSizedToolStripMenuItem.Enabled = false;
                terminatedToolStripMenuItem.Enabled = false;
            }
        }

        void addEnum_Click(object sender, EventArgs e)
        {
            if ((_type != null) && (((ToolStripItem)sender).Tag is EnumParserType) && (listViewEntries.SelectedItems.Count > 0))
            {
                EnumParserType type = (EnumParserType)((ToolStripItem)sender).Tag;
                IntegerPrimitiveMemberEntry entry = listViewEntries.SelectedItems[0].Tag as IntegerPrimitiveMemberEntry;

                if (entry != null)
                {
                    EnumMemberEntry enumEntry = new EnumMemberEntry(type, entry);

                    _type.ReplaceMember(entry, enumEntry);
                    listViewEntries.SelectedItems[0].Tag = enumEntry;

                    UpdateType();
                    UpdateSelection();
                }             
            }
        }

        void addSequence_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                SequenceParserType type = ((ToolStripItem)sender).Tag as SequenceParserType;

                if (type != null)
                {
                    MemberEntry entry = new SequenceMemberEntry(GetMemberName(), type);
                    _type.AddMember(entry);
                    AddMember(entry, true);
                }
            }
        }

        private void bitFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new BitFieldMemberEntry(GetMemberName(), _type.DefaultEndian);
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (_type != null)
            {
                UpdateType();
            }
        }

        private void fixedCharLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new FixedLengthStringMemberEntry(GetMemberName());

                _type.AddMember(entry);

                AddMember(entry, true);
            }
        }

        private void readToEndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new ReadToEndStringMemberEntry(GetMemberName());

                _type.AddMember(entry);

                AddMember(entry, true);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((_type != null) && (listViewEntries.SelectedItems.Count > 0))
            {
                listViewEntries.BeginUpdate();
                int index = listViewEntries.SelectedIndices[0];
                listViewEntries.SelectedIndices.Clear();

                ListViewItem item = listViewEntries.Items[index];
                _type.RemoveMember((MemberEntry)item.Tag);
                listViewEntries.Items.Remove(item);

                if (index < listViewEntries.Items.Count)
                {
                    listViewEntries.SelectedIndices.Add(index);
                }
                else if (listViewEntries.Items.Count > 0)
                {
                    listViewEntries.SelectedIndices.Add(listViewEntries.Items.Count - 1);
                }                
                
                listViewEntries.EndUpdate();
            }
        }

        private void buttonEntryUp_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    ListViewItem item = listViewEntries.SelectedItems[0];
                    int index = item.Index;

                    if (index > 0)
                    {
                        listViewEntries.Items.RemoveAt(index);
                        listViewEntries.Items.Insert(index - 1, item);
                        listViewEntries.SelectedItems.Clear();
                        item.Selected = true;
                        _type.MoveMemberUp((MemberEntry)item.Tag);
                    }
                }
            }
        }

        private void buttonEntryDown_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    ListViewItem item = listViewEntries.SelectedItems[0];
                    int index = item.Index;

                    if (index < (listViewEntries.Items.Count - 1))
                    {
                        listViewEntries.Items.RemoveAt(index);
                        listViewEntries.Items.Insert(index + 1, item);
                        listViewEntries.SelectedItems.Clear();
                        item.Selected = true;
                        _type.MoveMemberDown((MemberEntry)item.Tag);
                    }
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    listViewEntries.SelectedItems[0].BeginEdit();
                }
            }
        }        

        private void fixedLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    MemberEntry entry = listViewEntries.SelectedItems[0].Tag as MemberEntry;
                    ArrayMemberEntry arrayEntry = null;

                    if (entry is IntegerPrimitiveMemberEntry)
                    {
                        arrayEntry = new FixedLengthPrimitiveArrayMemberEntry((IntegerPrimitiveMemberEntry)entry);

                    }
                    else if(entry is IMemberReaderWriter)
                    {
                        arrayEntry = new FixedLengthGenericArrayMemberEntry((IMemberReaderWriter)entry);
                    }

                    if (arrayEntry != null)
                    {
                        _type.ReplaceMember(entry, arrayEntry);
                        listViewEntries.SelectedItems[0].Tag = arrayEntry;

                        UpdateType();
                        UpdateSelection();
                    }
                }
            }
        }

        private void readToEndArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    MemberEntry entry = listViewEntries.SelectedItems[0].Tag as MemberEntry;
                    ArrayMemberEntry arrayEntry = null;

                    if (entry is IntegerPrimitiveMemberEntry)
                    {
                        arrayEntry = new ReadToEndPrimitiveArrayMemberEntry((IntegerPrimitiveMemberEntry)entry);
                    }
                    else if (entry is IMemberReaderWriter)
                    {
                        arrayEntry = new ReadToEndGenericArrayMemberEntry((IMemberReaderWriter)entry);
                    }

                    if (arrayEntry != null)
                    {
                        _type.ReplaceMember(entry, arrayEntry);
                        listViewEntries.SelectedItems[0].Tag = arrayEntry;

                        UpdateType();
                        UpdateSelection();
                    }
                }
            }
        }

        private void lengthReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    MemberEntry entry = listViewEntries.SelectedItems[0].Tag as MemberEntry;
                    ArrayMemberEntry arrayEntry = null;

                    if (entry is IntegerPrimitiveMemberEntry)
                    {
                        arrayEntry = new ReferenceLengthPrimitiveArrayMemberEntry((IntegerPrimitiveMemberEntry)entry);
                    }
                    else if (entry is SequenceMemberEntry)
                    {
                        arrayEntry = new ReferenceLengthSequenceArrayMemberEntry((SequenceMemberEntry)entry);
                    }
                    else if (entry is IMemberReaderWriter)
                    {
                        arrayEntry = new ReferenceLengthGenericArrayMemberEntry((IMemberReaderWriter)entry);
                    }

                    if (arrayEntry != null)
                    {
                        _type.ReplaceMember(entry, arrayEntry);
                        listViewEntries.SelectedItems[0].Tag = arrayEntry;

                        UpdateType();
                        UpdateSelection();
                    }
                }
            }
        }

        private void listViewEntries_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item = listViewEntries.Items[e.Item];

            if (!String.IsNullOrEmpty(e.Label))
            {
                try
                {
                    MemberEntry entry = (MemberEntry)item.Tag;
                    entry.Name = e.Label;
                    propertyGrid.SelectedObject = entry;
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.CancelEdit = true;
                }
            }

            deleteToolStripMenuItem.ShortcutKeys = Keys.Delete;
        }

        private void lengthReferenceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new ReferenceLengthStringMemberEntry(GetMemberName());

                _type.AddMember(entry);

                AddMember(entry, true);
            }        
        }

        private void terminatedStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new TerminatedStringMemberEntry(GetMemberName());

                _type.AddMember(entry);

                AddMember(entry, true);
            }
        }

        private void convertToBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    ContainerMemberEntry entry = listViewEntries.SelectedItems[0].Tag as ContainerMemberEntry;

                    if (entry != null)
                    {
                        MemberEntry baseEntry = entry.DetachBaseEntry();
                        baseEntry.Name = entry.Name;
                        _type.ReplaceMember(entry, baseEntry);
                        listViewEntries.SelectedItems[0].Tag = baseEntry;

                        UpdateType();
                        UpdateSelection();
                    }
                }
            }
        }

        private void convertToBooleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    IntegerPrimitiveMemberEntry entry = listViewEntries.SelectedItems[0].Tag as IntegerPrimitiveMemberEntry;

                    if (entry != null)
                    {
                        BooleanMemberEntry boolEntry = new BooleanMemberEntry(entry);

                        _type.ReplaceMember(entry, boolEntry);
                        listViewEntries.SelectedItems[0].Tag = boolEntry;

                        UpdateType();
                        UpdateSelection();
                    }
                }
            }
        }

        private void unsigned24BitIntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new UInt24MemberEntry(GetMemberName(), _type.DefaultEndian);
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void signed24BitIntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new Int24MemberEntry(GetMemberName(), _type.DefaultEndian);
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void convertToCalculatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    IMemberReaderWriter entry = listViewEntries.SelectedItems[0].Tag as IMemberReaderWriter;

                    if (entry != null)
                    {
                        CalculatorMemberEntry calcEntry = new CalculatorMemberEntry(entry);

                        _type.ReplaceMember((MemberEntry)entry, calcEntry);
                        listViewEntries.SelectedItems[0].Tag = calcEntry;

                        UpdateType();
                        UpdateSelection();
                    }
                }
            }
        }

        private void terminatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    IMemberReaderWriter entry = listViewEntries.SelectedItems[0].Tag as IMemberReaderWriter;

                    if (entry != null)
                    {
                        TerminatedGenericArrayMemberEntry calcEntry = new TerminatedGenericArrayMemberEntry(entry);

                        _type.ReplaceMember((MemberEntry)entry, calcEntry);
                        listViewEntries.SelectedItems[0].Tag = calcEntry;

                        UpdateType();
                        UpdateSelection();
                    }
                }
            }
        }

        private void addSequenceChoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new SequenceChoiceMemberEntry(GetMemberName(), Document.SequenceTypes);
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void convertToSizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                if (listViewEntries.SelectedItems.Count > 0)
                {
                    IMemberReaderWriter entry = listViewEntries.SelectedItems[0].Tag as IMemberReaderWriter;

                    if (entry != null)
                    {
                        SizedMemberEntry sizedEntry = new SizedMemberEntry(entry);

                        _type.ReplaceMember((MemberEntry)entry, sizedEntry);
                        listViewEntries.SelectedItems[0].Tag = sizedEntry;

                        UpdateType();
                        UpdateSelection();
                    }
                }
            }
        }

        private void listViewEntries_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            deleteToolStripMenuItem.ShortcutKeys = Keys.None;
        }

        private void addFixedBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new FixedBytesMemberEntry(GetMemberName());
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void fixedLengthToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {                
                MemberEntry entry = new FixedLengthPrimitiveArrayMemberEntry(
                    new IntegerPrimitiveMemberEntry(GetMemberName(), typeof(byte), _type.DefaultEndian));
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void readToEndToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new ReadToEndPrimitiveArrayMemberEntry(
                    new IntegerPrimitiveMemberEntry(GetMemberName(), typeof(byte), _type.DefaultEndian));
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void lengthReferenceToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new ReferenceLengthPrimitiveArrayMemberEntry(
                    new IntegerPrimitiveMemberEntry(GetMemberName(), typeof(byte), _type.DefaultEndian));
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }

        private void terminatedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_type != null)
            {
                MemberEntry entry = new TerminatedGenericArrayMemberEntry(
                    new IntegerPrimitiveMemberEntry(GetMemberName(), typeof(byte), _type.DefaultEndian));
                _type.AddMember(entry);
                AddMember(entry, true);
            }
        }
    }
}
