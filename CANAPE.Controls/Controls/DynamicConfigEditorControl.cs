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
using System.Linq;
using System.Windows.Forms;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Forms;

namespace CANAPE.Controls
{
    public partial class DynamicConfigEditorControl : UserControl
    {
        Dictionary<string, DynamicNodeConfigProperty> _properties;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DynamicNodeConfigProperty[] Properties
        {
            get
            {
                return _properties.Values.ToArray();
            }

            set
            {
                _properties = new Dictionary<string, DynamicNodeConfigProperty>();

                if (value != null)
                {
                    foreach (DynamicNodeConfigProperty p in value)
                    {
                        _properties[p.Name] = p;
                    }
                }

                RefreshList();
            }
        }

        public DynamicConfigEditorControl()
        {
            
            InitializeComponent();
        }

        private void RefreshList()
        {
            listViewConfig.Items.Clear();
            
            foreach (DynamicNodeConfigProperty p in _properties.Values)
            {
                ListViewItem item = listViewConfig.Items.Add(p.Name);

                item.SubItems.Add(p.PropertyType.Name);                
                item.SubItems.Add(p.DefaultValue != null ? p.DefaultValue.ToString() : String.Empty);
                item.SubItems.Add(p.Description ?? String.Empty);
                item.Tag = p;
            }
        }

        private void addPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (EditConfigPropertyForm frm = new EditConfigPropertyForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    DynamicNodeConfigProperty p = frm.Property;
                    _properties[p.Name] = p;
                    RefreshList();
                }
            }            
        }

        private void editPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewConfig.SelectedItems.Count > 0)
            {
                using (EditConfigPropertyForm frm = new EditConfigPropertyForm())
                {
                    frm.Property = (DynamicNodeConfigProperty)listViewConfig.SelectedItems[0].Tag;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        DynamicNodeConfigProperty p = frm.Property;
                        _properties[p.Name] = p;
                        RefreshList();
                    }
                }
            }
        }

        private void removePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewConfig.SelectedItems.Count > 0)
            {
                _properties.Remove(((DynamicNodeConfigProperty)listViewConfig.SelectedItems[0].Tag).Name);
                RefreshList();
            }
        }
    }
}
