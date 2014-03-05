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
using CANAPE.Controls;
using CANAPE.Extension;
using CANAPE.Utils;

namespace CANAPE.Forms
{
    public partial class SelectTemplateForm : Form
    {
        Dictionary<string, ListViewExtension> _tabs;
        Dictionary<string, List<CANAPETemplate>> _templates;

        public SelectTemplateForm(Dictionary<string, List<CANAPETemplate>> templates)
        {
            
            InitializeComponent();
            _tabs = new Dictionary<string, ListViewExtension>();
            _templates = templates;
        }

        public SelectTemplateForm() : this(new Dictionary<string, List<CANAPETemplate>>())
        {
        }

        public CANAPETemplate Template
        {
            get;
            private set;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ListView listView = _tabs[tabControl.SelectedTab.Text];

            if (listView.SelectedItems.Count > 0)
            {
                Template = (CANAPETemplate)listView.SelectedItems[0].Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.SelectTemplateForm_SpecifyTemplate, 
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ListViewExtension CreateTabPage(string name)
        {
            TabPage page = new TabPage(name);
            ListViewExtension ret = new ListViewExtension();

            ret.View = View.Details;
            ret.Dock = DockStyle.Fill;
            ret.FullRowSelect = true;
            ret.MultiSelect = false;
            ret.GridLines = true;
            ret.Columns.Add(CANAPE.Properties.Resources.SelectTemplateForm_NameColumn, 146);
            ret.Columns.Add(CANAPE.Properties.Resources.SelectTemplateForm_DescriptionColumn, 369);

            ret.ColumnClick += listViewNodes_ColumnClick;
            ret.MouseDoubleClick += listViewNodes_MouseDoubleClick;

            page.Controls.Add(ret);
            _tabs[name] = ret;
            tabControl.TabPages.Add(page);

            return ret;
        }

        private static ListViewItem CreateItem(CANAPETemplate template)
        {
            ListViewItem item = new ListViewItem(template.Name);
            item.SubItems.Add(template.Description);
            item.Tag = template;

            return item;
        }

        private void SelectTemplateForm_Load(object sender, EventArgs e)
        {            
            if (_templates != null)
            {
                foreach (KeyValuePair<string, List<CANAPETemplate>> pair in _templates)
                {
                    ListView listView = CreateTabPage(pair.Key);

                    foreach (CANAPETemplate template in pair.Value)
                    {
                        listView.Items.Add(CreateItem(template));
                    }
                }
            }

            foreach (KeyValuePair<string, ListViewExtension> pair in _tabs)
            {
                pair.Value.ListViewItemSorter = new ListViewItemSorter(0, false);
                pair.Value.Sort();
                pair.Value.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                pair.Value.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void listViewNodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewExtension listView = (ListViewExtension)sender;
            if (listView.SelectedItems.Count > 0)
            {
                Template = (CANAPETemplate)listView.SelectedItems[0].Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void listViewNodes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewExtension listView = (ListViewExtension)sender;
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
