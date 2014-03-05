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
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.NodeLibrary.Control;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class StateGraphDocumentControl : UserControl
    {
        StateGraphDocument _document;

        private ListViewItem AddEntry(StateGraphDocument.StateGraphEntry entry)
        {
            ListViewItem item = new ListViewItem(entry.StateName);

            item.SubItems.Add(entry.Graph != null ? entry.Graph.Name : "None");
            item.Tag = entry;
            listViewStateEntries.Items.Add(item);

            return item;
        }

        private void SetupEntries()
        {
            foreach (StateGraphDocument.StateGraphEntry entry in _document.Entries)
            {
                AddEntry(entry);
            }
        }

        private void UpdateEntries()
        {
            foreach (ListViewItem item in listViewStateEntries.Items)
            {
                StateGraphDocument.StateGraphEntry entry = (StateGraphDocument.StateGraphEntry)item.Tag;

                item.SubItems[0].Text = entry.StateName;
                item.SubItems[1].Text = entry.Graph != null ? entry.Graph.Name : "None";
            }
        }

        public StateGraphDocumentControl(IDocumentObject document)
        {
            _document = (StateGraphDocument)document;
            
            InitializeComponent();

            checkGlobalMeta.Checked = _document.GlobalState;
            textBoxMetaName.Text = _document.MetaName;
            BuildComboBox();
            comboDefaultState.Text = _document.DefaultState;

            SetupEntries();
        }

        private void listViewStateEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStateEntries.SelectedItems.Count > 0)
            {
                propertyGrid.SelectedObject = listViewStateEntries.SelectedItems[0].Tag;
            }
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateEntries();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEntry(_document.AddEntry()).Selected = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listViewStateEntries.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewStateEntries.SelectedItems[0];

                _document.RemoveEntry((StateGraphDocument.StateGraphEntry)item.Tag);
                listViewStateEntries.Items.Remove(item);
            }
        }

        private void BuildComboBox()
        {
            comboDefaultState.Items.Clear();

            string selectedItem = comboDefaultState.SelectedItem as string;
            bool foundItem = false;

            foreach (StateGraphDocument.StateGraphEntry entry in _document.Entries)
            {
                if (!String.IsNullOrWhiteSpace(entry.StateName))
                {
                    comboDefaultState.Items.Add(entry.StateName);
                    if (entry.StateName == selectedItem)
                    {
                        foundItem = true;
                    }
                }
            }

            if (foundItem)
            {
                comboDefaultState.SelectedItem = selectedItem;
            }
        }

        private void comboDefaultState_DropDown(object sender, EventArgs e)
        {
            BuildComboBox();
        }

        private void textBoxMetaName_TextChanged(object sender, EventArgs e)
        {
            _document.MetaName = textBoxMetaName.Text;
        }

        private void comboDefaultState_SelectedIndexChanged(object sender, EventArgs e)
        {
            _document.DefaultState = comboDefaultState.Text;
        }

        private void copySetMetaNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewStateEntries.SelectedItems.Count > 0)
            {
                StateGraphDocument.StateGraphEntry entry = (StateGraphDocument.StateGraphEntry)listViewStateEntries.SelectedItems[0].Tag;

                SetMetaStateNodeConfig config = new SetMetaStateNodeConfig();
                config.MetaName = textBoxMetaName.Text;
                config.Value = entry.StateName;
                config.ResetStateOnMatch = true;

                LibraryNodeConfig libNode = new LibraryNodeConfig(typeof(SetMetaStateNode), 
                    String.Format(Properties.Resources.StateGraphDocumentControl_SetStateName, entry.StateName), 
                    config);

                libNode.Label = String.Format(Properties.Resources.StateGraphDocumentControl_SetStateName, entry.StateName);

                NetGraphDocumentControl.CopyNode(libNode);
            }
        }
    }
}
