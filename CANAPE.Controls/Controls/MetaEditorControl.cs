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
using System.Windows.Forms;
using CANAPE.Nodes;

namespace CANAPE.Controls
{
    /// <summary>
    /// A control to view and edit meta data
    /// </summary>
    public partial class MetaEditorControl : UserControl
    {
        MetaDictionary _meta;

        private void UpdateMeta()
        {
            if (_meta != null)
            {
                listViewMeta.BeginUpdate(); 
                
                // First capture the current meta data in a static dictionary         
                Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                List<ListViewItem> deleteList = new List<ListViewItem>();

                foreach (KeyValuePair<string, dynamic> pair in _meta)
                {
                    dict.Add(pair.Key, pair.Value);
                }

                foreach (ListViewItem item in listViewMeta.Items)
                {       
                    if (dict.ContainsKey(item.Text))
                    {
                        object value = dict[item.Text];

                        item.SubItems[1].Text = value.GetType().Name;
                        item.SubItems[2].Text = value.ToString();

                        // Remove from dictionary of values
                        dict.Remove(item.Text);
                    }
                    else
                    {
                        deleteList.Add(item);
                    }
                }

                foreach (ListViewItem item in deleteList)
                {
                    listViewMeta.Items.Remove(item);
                }

                List<ListViewItem> items = new List<ListViewItem>();
                foreach (KeyValuePair<string, dynamic> pair in dict)
                {
                    ListViewItem item = new ListViewItem(pair.Key);
                    item.SubItems.Add(pair.Value.GetType().Name);
                    item.SubItems.Add(pair.Value.ToString());
                    items.Add(item);
                } 
                
                listViewMeta.Items.AddRange(items.ToArray());

                listViewMeta.EndUpdate();
            }
            else
            {
                listViewMeta.Items.Clear();
            }
        }

        /// <summary>
        /// Set the current dictionary
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public MetaDictionary Meta 
        {
            get
            {
                return _meta;
            }

            set
            {
                _meta = value;
            }
        }

        public MetaEditorControl()
        {
            
            InitializeComponent();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            UpdateMeta();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_meta != null)
            {
                _meta.Clear();
                UpdateMeta();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((_meta != null) && (listViewMeta.SelectedItems.Count > 0))
            {                
                dynamic val;

                _meta.TryRemove(listViewMeta.SelectedItems[0].Text, out val);

                listViewMeta.Items.Remove(listViewMeta.SelectedItems[0]);
            }
        }

        private void MetaEditorControl_Load(object sender, EventArgs e)
        {
            UpdateMeta();
        }
    }
}
