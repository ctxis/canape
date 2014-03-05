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
using System.Drawing;
using System.Windows.Forms;
using CANAPE.DataFrames;

namespace CANAPE.Controls.NodeEditors
{
    [DataNodeEditor(DataNodeClasses.FLAGS_ENUM_NODE_CLASS)]
    public partial class FlagsEnumDataNodeEditorControl : UserControl, IDataNodeEditor
    {        
        EnumDataValue _node;
        bool _readOnly;        
        bool _isLoaded;
        bool _isUpdating;

        public FlagsEnumDataNodeEditorControl()
        {
            
            InitializeComponent();
        }

        private void UpdateDisplayFromValue()
        {            
            PortableEnum value = _node.Value;
            
            long realValue = _node.Value.Value;
            foreach (ListViewItem item in listViewEnums.Items)
            {
                if ((realValue & (long)item.Tag) == (long)item.Tag)
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }

            listViewEnums.Enabled = !_readOnly;
        }

        private void SetupFrame()
        {
            _isUpdating = true;

            if (_node != null)
            {          
                List<ListViewItem> items = new List<ListViewItem>();
                long usedFlags = long.MaxValue;
                int bitSize = _node.ToArray().Length * 8;
               

                foreach (PortableEnum.EnumEntry ent in _node.Value.Entries)
                {
                    if (ent.Value != 0)
                    {
                        ListViewItem item = new ListViewItem(ent.Name);
                        string v = String.Format("0x{0:X02}", ent.Value);
                        item.SubItems.Add(v);
                        item.Tag = ent.Value;
                        items.Add(item);
                        usedFlags &= ~ent.Value;
                    }
                }

                if (usedFlags != 0)
                {
                    for (int i = 0; i < bitSize; ++i)
                    {
                        long curr = 1L << i;
                        if ((usedFlags & curr) == curr)
                        {
                            ListViewItem item = new ListViewItem(String.Format("Bit{0}", i));
                            item.SubItems.Add(String.Format("0x{0:X02}", curr));
                            item.Tag = curr;
                            items.Add(item);
                        }
                    }
                }

                listViewEnums.Items.Clear();
                listViewEnums.Items.AddRange(items.ToArray());

                UpdateDisplayFromValue();
            }
            else
            {                
                listViewEnums.Items.Clear();                
            }

            _isUpdating = false;
        }

        public void SetNode(DataNode node, DataNode selected, Color color, bool readOnly)
        {
            _node = node as EnumDataValue;            
            _readOnly = readOnly;

            if (_isLoaded)
            {
                SetupFrame();
            }
        }

        private void listViewEnums_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_isLoaded && !_isUpdating)
            {
                if (_node != null)
                {
                    bool set = e.Item.Checked;

                    long newValue = _node.Value.Value;                    

                    if (set)
                    {
                        newValue = newValue | (long)e.Item.Tag;
                    }
                    else
                    {
                        newValue = newValue & ~(long)e.Item.Tag;
                    }

                    if (newValue != _node.Value.Value)
                    {
                        _node.Value = newValue;                        
                    }
                }
            }
        }

        private void EnumDataNodeEditorControl_Load(object sender, EventArgs e)
        {
            _isLoaded = true;

            if (_node != null)
            {
                SetupFrame();
            }
        }
    }
}
