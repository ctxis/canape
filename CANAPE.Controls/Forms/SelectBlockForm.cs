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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    public partial class SelectBlockForm : Form
    {
        int _totalSize;
        
        public SelectBlockForm(int currOffset, int totalSize)
        {            
            _totalSize = totalSize;
            
            InitializeComponent();
            numericOffset.Value = currOffset;
            numericEndOffset.Value = currOffset + 1;
            numericLength.Value = 1;
        }

        public int Offset { get; set; }
        public int Length { get; set; }

        private void radioOffset_CheckedChanged(object sender, EventArgs e)
        {
            numericLength.Enabled = !radioOffset.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int currOffset = (int)numericOffset.Value;
            int currLength = 0;

            if (radioLength.Checked)
            {
                currLength = (int)numericLength.Value;
            }
            else if (radioOffset.Checked)
            {
                currLength = (int)numericEndOffset.Value - currOffset;
            }

            if (currOffset > _totalSize)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.SelectBlockForm_OffsetGreaterThanSize,
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (currLength < 0)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.SelectBlockForm_EndOffsetLessThanOffset,
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Length = currLength;
                Offset = currOffset;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
