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
using CANAPE.Controls;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.Factories;
using CANAPE.NodeLibrary;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Forms
{
    public abstract partial class SelectLibraryOrScriptForm : Form
    {
        protected SelectLibraryOrScriptForm(Type[] scriptTypes)
        {            
            InitializeComponent();
            scriptSelectionControl.ScriptTypes = scriptTypes;
        }

        protected virtual bool SelectScript(ScriptDocument document, string className)
        {
            // Do nothing
            return true;
        }

        protected virtual bool SelectTemplate(object template)
        {
            // Do nothing
            return true;
        }

        protected virtual ListViewItem[] PopulateTemplates()
        {
            return new ListViewItem[0];
        }

        private void SelectServerForm_Load(object sender, EventArgs e)
        {
            listViewNodes.Items.AddRange(PopulateTemplates());

            
            listViewNodes.ListViewItemSorter = new ListViewItemSorter(0, false);
            listViewNodes.Sort();
        }

        private void HandleLibraryNodeSelection()
        {
            if (listViewNodes.SelectedItems.Count > 0)
            {
                if (SelectTemplate(listViewNodes.SelectedItems[0].Tag))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.SelectEndointForm_SelectServer, 
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool FilterServers { get; set; }

        private void listViewNodes_DoubleClick(object sender, EventArgs e)
        {
            HandleLibraryNodeSelection();
        }

        private void listViewNodes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewItemSorter sorter = (ListViewItemSorter)listViewNodes.ListViewItemSorter;

            if (sorter.Column == e.Column)
            {
                sorter.Reverse = !sorter.Reverse;
            }
            else
            {
                sorter.Reverse = false;
                sorter.Column = e.Column;
            }

            listViewNodes.Sort(); 
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageScript)
            {
                HandleScriptSelected();
            }
            else
            {
                HandleLibraryNodeSelection();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void HandleScriptSelected()
        {
            if (scriptSelectionControl.Document == null)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.SelectEndpointForm_SelectScript,
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrWhiteSpace(scriptSelectionControl.ClassName))
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.SelectEndpointForm_SelectClass,
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (SelectScript(scriptSelectionControl.Document, scriptSelectionControl.ClassName))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

    }
}
