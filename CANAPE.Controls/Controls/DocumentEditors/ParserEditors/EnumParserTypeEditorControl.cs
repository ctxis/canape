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
using System.Windows.Forms;
using CANAPE.Documents.Data;
using CANAPE.Parser;
using System.Linq;
using System.ComponentModel;

namespace CANAPE.Controls.DocumentEditors.ParserEditors
{
    public partial class EnumParserTypeEditorControl : UserControl
    {
        bool _loaded;
        EnumParserType _enumType;

        public EnumParserTypeEditorControl()
        {            
            InitializeComponent();
        }

        private void AddEnumEntryToList(EnumParserTypeEntry entry, bool editName)
        {
            ListViewItem item = listViewMembers.Items.Add(entry.Name);
            if (_enumType.IsFlags)
            {
                item.SubItems.Add(String.Format("0x{0:X}", entry.Value));
            }
            else
            {
                item.SubItems.Add(entry.Value.ToString());
            }
            item.Tag = entry;

            if (editName)
            {
                item.BeginEdit();
            }
        }

        private void UpdateEnumType()
        {
            foreach (ListViewItem item in listViewMembers.Items)
            {
                EnumParserTypeEntry entry = (EnumParserTypeEntry)item.Tag;
                item.Text = entry.Name;
                if (_enumType.IsFlags)
                {
                    item.SubItems[1].Text = String.Format("0x{0:X}", entry.Value);
                }
                else
                {
                    item.SubItems[1].Text = entry.Value.ToString();
                }
            }
        }

        private void SetupEnumType()
        {
            if (_loaded)
            {
                listViewMembers.Items.Clear();
                if (_enumType != null)
                {                    
                    foreach (EnumParserTypeEntry ent in _enumType.Entries)
                    {
                        AddEnumEntryToList(ent, false);
                    }

                    propertyGrid.SelectedObject = _enumType;
                }
            }

        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EnumParserType EnumType 
        {
            get
            {
                return _enumType;
            }

            set
            {
                if(_enumType != value)
                {
                    _enumType = value;
                    SetupEnumType();
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ParserDocument Document
        {
            get;
            set;
        }

        private void EnumParserTypeEditorControl_Load(object sender, EventArgs e)
        {
            _loaded = true;

            if (EnumType != null)
            {
                SetupEnumType();
            }
        }

        private string GenerateName()
        {
            int i = 0;

            for (i = 0; i < 10000; ++i)
            {
                string name = String.Format("{0}{1}", _enumType.IsFlags ? "Flag" : "Value", i);

                if (_enumType.FindEntry(name) == null)
                {
                    return name;
                }
            }

            return null;
        }

        private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_enumType != null)
            {
                long value = 0;
                HashSet<long> values = new HashSet<long>();
                string name = GenerateName();

                if (name != null)
                {
                    foreach (EnumParserTypeEntry ent in _enumType.Entries)
                    {
                        if (!values.Contains(ent.Value))
                        {
                            values.Add(ent.Value);
                        }
                    }

                    if (!_enumType.IsFlags)
                    {
                        if (values.Count > 0)
                        {
                            value = values.Max() + 1;
                        }
                    }
                    else
                    {
                        // Find the next positive enum value for simplicity
                        for (value = 1; value < 0x4000000000000001L; value <<= 1)
                        {
                            if (!values.Contains(value))
                            {
                                break;
                            }
                        }
                    }

                    EnumParserTypeEntry entry = new EnumParserTypeEntry(name, value);
                    _enumType.AddEntry(entry);
                    AddEnumEntryToList(entry, true);
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMembers.SelectedItems.Count > 0)
            {
                _enumType.RemoveEntry((EnumParserTypeEntry)listViewMembers.SelectedItems[0].Tag);
                listViewMembers.Items.Remove(listViewMembers.SelectedItems[0]);
            }
        }

        private void listViewMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMembers.SelectedItems.Count > 0)
            {
                propertyGrid.SelectedObject = listViewMembers.SelectedItems[0].Tag;
            }
            else
            {
                propertyGrid.SelectedObject = _enumType;
            }
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (_enumType != null)
            {
                UpdateEnumType();
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMembers.SelectedItems.Count > 0)
            {
                listViewMembers.SelectedItems[0].BeginEdit();
            }
        }

        private void listViewMembers_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                try
                {
                    EnumParserTypeEntry entry = (EnumParserTypeEntry)listViewMembers.Items[e.Item].Tag;
                    entry.Name = e.Label;
                    propertyGrid.SelectedObject = entry;
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.CancelEdit = true;
                }
            }
        }
    }
}
