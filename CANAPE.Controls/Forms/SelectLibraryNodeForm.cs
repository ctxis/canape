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
using CANAPE.Utils;
using CANAPE.NodeLibrary;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    public partial class SelectLibraryNodeForm : Form
    {
        Dictionary<string, ListView> _tabs;

        public SelectLibraryNodeForm()
        {
            
            InitializeComponent();
            _tabs = new Dictionary<string, ListView>();
        }

        public NodeLibraryManager.NodeLibraryType Node
        {
            get;
            private set;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ListView listView = _tabs[tabControl.SelectedTab.Text];

            if (listView.SelectedItems.Count > 0)
            {
                Node = (NodeLibraryManager.NodeLibraryType)listView.SelectedItems[0].Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.SelectLibraryNodeForm_SelectNode, 
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ListView CreateTabPage(string name)
        {
            TabPage page = new TabPage(name);
            ListView ret = new ListView();

            ret.View = View.Details;
            ret.Dock = DockStyle.Fill;
            ret.FullRowSelect = true;
            ret.Columns.Add(CANAPE.Properties.Resources.SelectLibraryNodeForm_NameColumn, 146);
            ret.Columns.Add(CANAPE.Properties.Resources.SelectLibraryNodeForm_DescriptionColumn, 369);

            ret.ColumnClick += listViewNodes_ColumnClick;
            ret.MouseDoubleClick += listViewNodes_MouseDoubleClick;

            page.Controls.Add(ret);
            _tabs[name] = ret;
            tabControl.TabPages.Add(page);

            return ret;
        }

        private void SelectLibraryNodeForm_Load(object sender, EventArgs e)
        {
            foreach (NodeLibraryManager.NodeLibraryType t in NodeLibraryManager.NodeTypes)
            {
                ListView listView;
                string name = t.Category.ToString();

                if (!_tabs.ContainsKey(name))
                {
                    listView = CreateTabPage(name);
                }
                else
                {
                    listView = _tabs[name];
                }

                ListViewItem item = listView.Items.Add(t.Name);                
                item.SubItems.Add(t.Description);
                item.Tag = t;
            }

            foreach (KeyValuePair<string, ListView> pair in _tabs)
            {
                pair.Value.ListViewItemSorter = new ListViewItemSorter(0, false);
                pair.Value.Sort();
                pair.Value.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                pair.Value.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void listViewNodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.SelectedItems.Count > 0)
            {
                Node = (NodeLibraryManager.NodeLibraryType)listView.SelectedItems[0].Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void listViewNodes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView listView = (ListView)sender;
            ListViewItemSorter sorter = (ListViewItemSorter)listView.ListViewItemSorter;

            if (sorter.Column == e.Column)
            {
                sorter.Reverse = !sorter.Reverse;
            }
            else
            {
                sorter.Reverse = false;
                sorter.Column = e.Column;
            }

            listView.Sort();
        }
    }
}
