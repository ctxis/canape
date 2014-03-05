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

namespace CANAPE.Controls.NodeEditors
{
    [DataNodeEditor(DataNodeClasses.ENUM_NODE_CLASS)]
    public partial class EnumDataNodeEditorControl : UserControl, IDataNodeEditor
    {
        EnumDataValue _node;
        bool _readOnly;
        bool _isUpdating;        

        public EnumDataNodeEditorControl()
        {            
            InitializeComponent();
        }

        private void SetupFrame()
        {
            _isUpdating = true;

            comboBoxValues.Items.Clear();
            if (_node != null)
            {
                PortableEnum.EnumEntry selected = null;

                foreach (PortableEnum.EnumEntry ent in _node.Value.Entries)
                {
                    if (ent.Value == _node.Value.Value)
                    {
                        selected = ent;
                    }

                    comboBoxValues.Items.Add(ent);
                }

                if (selected != null)
                {
                    comboBoxValues.SelectedItem = selected;
                }
                else
                {
                    comboBoxValues.Text = _node.Value.ToString();
                }
            }
            else
            {                
                comboBoxValues.Text = String.Empty;
            }

            comboBoxValues.Enabled = !_readOnly;

            _isUpdating = false;
        }

        public void SetNode(DataNode node, DataNode selected, Color color, bool readOnly)
        {
            _node = node as EnumDataValue;
            _readOnly = readOnly;
            SetupFrame();                    
        }

        private void comboBoxValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isUpdating)
            {
                PortableEnum.EnumEntry ent = comboBoxValues.SelectedItem as PortableEnum.EnumEntry;

                if (ent != null)
                {
                    if (_node.Value != ent.Value)
                    {
                        _node.Value = ent.Value;
                    }
                }
            }
        }

        private void comboBoxValues_TextChanged(object sender, EventArgs e)
        {
            if (!_isUpdating)
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(comboBoxValues.Text))
                    {
                        _node.Value = comboBoxValues.Text;
                    }
                    else
                    {
                        _node.Value = 0;
                    }
                }
                catch (ArgumentException)
                {
                }
            }
        }
    }
}
