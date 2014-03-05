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

namespace CANAPE.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ByteArrayValueEditorForm : Form
    {        
        /// <summary>
        /// 
        /// </summary>
        public ByteArrayValueEditorForm()
        {
            
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ReadOnly { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ReadOnly)
            {
                Value = binaryDataEditorControl.Data;
            }

            DialogResult = DialogResult.OK;

            Close();
        }

        private void ByteArrayValueEditorForm_Load(object sender, EventArgs e)
        {
            binaryDataEditorControl.Data = Value;
            binaryDataEditorControl.ReadOnly = ReadOnly;
        }
    }
}
