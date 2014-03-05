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
using System.Drawing;
using System.Windows.Forms;
using CANAPE.DataFrames;
using CANAPE.Forms;
using CANAPE.Utils;

namespace CANAPE.Controls.NodeEditors
{
    public partial class DataKeyEditorControl : UserControl, IDataNodeEditor
    {
        DataKey _key;
        bool _readOnly;
        Color _color;

        public DataKeyEditorControl()
        {
            
            InitializeComponent();
        }

        private void UpdateList()
        {
            if (_key != null)
            {
                listViewValues.SuspendLayout();
                listViewValues.Items.Clear();

                ListViewItem item = listViewValues.Items.Add(Properties.Resources.DataKeyEditorControl_FormatStringName);                
                item.SubItems.Add(_key.FormatString ?? String.Empty);
                item.SubItems.Add(GeneralUtils.FormatKeyString(_key.FormatString, _key));
                item.Tag = _key;

                foreach (var n in _key.SubNodes)
                {
                    DataValue value = n as DataValue;
                    if (value != null)
                    {
                        item = listViewValues.Items.Add(value.Name);
                        item.SubItems.Add(value.Value.GetType().Name);
                        item.SubItems.Add(value.ToString());
                        item.Tag = value;
                    }
                }

                listViewValues.ResumeLayout();
            }
        }

        private void listViewValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = listViewValues.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                DataKey key = item.Tag as DataKey;

                if (key != null)
                {
                    using (StringValueEditorForm frm = new StringValueEditorForm())
                    {
                        frm.Value = key.FormatString ?? String.Empty;
                        frm.ReadOnly = _readOnly || key.Readonly;

                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            key.FormatString = frm.Value;
                            item.SubItems[1].Text = key.FormatString ?? String.Empty;
                            item.SubItems[2].Text = GeneralUtils.FormatKeyString(key.FormatString, key);
                        }
                    }
                }
                else
                {
                    DataValue value = (DataValue)item.Tag;
                    if (value.Value is byte[])
                    {
                        using (ByteArrayValueEditorForm frm = new ByteArrayValueEditorForm())
                        {
                            frm.Value = (byte[])value.Value;
                            frm.ReadOnly = _readOnly || value.Readonly;

                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                value.Value = frm.Value;
                                item.SubItems[2].Text = value.ToString();
                            }
                        }
                    }
                    else if (value.Value is string)
                    {
                        using (StringValueEditorForm frm = new StringValueEditorForm())
                        {
                            frm.Value = (string)value.Value;
                            frm.ReadOnly = _readOnly || value.Readonly;

                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                value.Value = frm.Value;
                                item.SubItems[2].Text = value.ToString();
                            }
                        }
                    }
                    else
                    {
                        using (PrimitiveValueEditorForm frm = new PrimitiveValueEditorForm())
                        {
                            frm.Value = value.Value;
                            frm.ReadOnly = _readOnly || value.Readonly;

                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                value.Value = frm.Value;
                                item.SubItems[2].Text = value.ToString();
                            }
                        }
                    }
                }
            }
        }

        public void SetNode(DataNode node, DataNode selected, Color color, bool readOnly)
        {
            if (node is DataKey)
            {
                _key = node as DataKey;
                _color = color;
                _readOnly = readOnly;
                UpdateList();
            }
            else
            {
                throw new ArgumentException("Node needs to be a DataKey type");
            }
        }
    }
}
