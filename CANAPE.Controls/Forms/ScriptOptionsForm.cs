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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Controls.DocumentEditors;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.NodeConfigs;

namespace CANAPE.Forms
{
    public partial class ScriptOptionsForm : Form
    {
        ScriptDocument _document;

        public ScriptOptionsForm(ScriptDocument document)
        {            
            InitializeComponent();
            _document = document;

            checkBoxEnableDebug.Checked = _document.Container.EnableDebug;
            dynamicConfigEditorControl.Properties = _document.Configuration.ToArray();

            LoadReferenceList(_document.Container.ReferencedNames);
        }

        private void LoadReferenceList(IEnumerable<AssemblyName> names)
        {
            assemblyNameListView.Items.Clear();

            foreach (AssemblyName name in names)
            {
                assemblyNameListView.AddName(name);
            }

            assemblyNameListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            assemblyNameListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ScriptOptionsForm_Load(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _document.Container.EnableDebug = checkBoxEnableDebug.Checked;

            _document.Container.ReferencedNames.Clear();
            foreach (AssemblyName name in assemblyNameListView.GetNames(false))
            {
                _document.Container.ReferencedNames.Add(name);
            }            
            _document.ClearConfiguration();
            foreach (DynamicNodeConfigProperty p in dynamicConfigEditorControl.Properties)
            {
                _document.AddConfiguration(p);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (assemblyNameListView.SelectedItems.Count > 0)
            {
                List<ListViewItem> items = new List<ListViewItem>();

                foreach (ListViewItem i in assemblyNameListView.SelectedItems)
                {
                    items.Add(i);
                }

                foreach (ListViewItem i in items)
                {
                    assemblyNameListView.Items.Remove(i);
                }
            }
        }

        private void btnAddReference_Click(object sender, EventArgs e)
        {            
            using (AddReferenceForm frm = new AddReferenceForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {                    
                    Dictionary<string, AssemblyName> names = new Dictionary<string, AssemblyName>();

                    foreach (AssemblyName name in assemblyNameListView.GetNames(false))
                    {
                        names[name.FullName] = name;
                    }

                    foreach (AssemblyName name in frm.SelectedNames)
                    {
                        names[name.FullName] = name;
                    }

                    LoadReferenceList(names.Values);
                }
            }
        }
    }
}
