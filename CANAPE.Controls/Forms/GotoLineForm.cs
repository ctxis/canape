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
    public partial class GotoLineForm : Form
    {
        public GotoLineForm()
        {
            
            InitializeComponent();            
        }

        public int CurrentLine { get; set; }
        public int MaximumLine { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CurrentLine = (int)numericUpDownLine.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void GotoLineForm_Load(object sender, EventArgs e)
        {
            numericUpDownLine.Maximum = MaximumLine;
            numericUpDownLine.Value = CurrentLine;
            lblLineNumber.Text = String.Format(CANAPE.Properties.Resources.GotoLineForm_Label, MaximumLine);
        }
    }
}
