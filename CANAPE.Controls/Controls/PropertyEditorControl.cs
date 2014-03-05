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
using System.Data;
using System.Windows.Forms;

namespace CANAPE.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PropertyEditorControl : UserControl
    {        
        DataTable _properties;
        
        public Dictionary<string, string> Properties
        {
            get
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                if (_properties != null)
                {
                    foreach (DataRow row in _properties.Rows)
                    {
                        dict.Add(row.ItemArray[0].ToString(), row.ItemArray[1].ToString());
                    }
                }

                return dict;
            }

            set
            {
                if (_properties != null)
                {
                    _properties.Rows.Clear();
                    foreach (var pair in value)
                    {
                        _properties.Rows.Add(pair.Key, pair.Value);
                    }
                }
            }
        }

        public PropertyEditorControl()
        {
            _properties = new DataTable();
            _properties.Columns.Add("Key");
            _properties.Columns.Add("Value");
            _properties.Columns[0].Unique = true;

            
            InitializeComponent();
        }

        private void PropertyEditor_Load(object sender, EventArgs e)
        {
            dataGridView.DataSource = _properties;
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dataGridView.Rows[e.RowIndex].Cells[0].ErrorText = e.Exception.Message;
            e.Cancel = true;
        }

        private void dataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.Rows[e.RowIndex].Cells[0].ErrorText = "";
        }
    }
}
