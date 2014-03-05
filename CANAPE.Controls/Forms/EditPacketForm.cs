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
using System.Threading;
using CANAPE.DataFrames;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    public partial class EditPacketForm : Form
    {
        public DataFrame Frame { get; set; }
        public string Selector { get; set; }
        public string DisplayTag { get; set; }
        public Color DisplayColor { get; set; }
        public ManualResetEvent WaitEvent { get; private set; }

        public bool ReadOnly
        {
            get
            {
                return frameEditorControl.ReadOnly;
            }

            set
            {
                frameEditorControl.ReadOnly = value;
            }
        }

        public bool DisableEditor
        {
            get;
            private set;
        }

        public bool ShowReadOnly
        {
            get { return checkBoxReadOnly.Visible; }
            set { checkBoxReadOnly.Visible = value; }
        }

        public bool ShowDisableEditor
        {
            get { return checkBoxDisableEdit.Visible; }
            set { checkBoxDisableEdit.Visible = value; }
        }

        public EditPacketForm()
        {
            DisplayColor = Color.White;
            WaitEvent = new ManualResetEvent(false);

            InitializeComponent();
        }

        private void EditPacketForm_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(DisplayTag))
            {
                Text = String.Format(CANAPE.Properties.Resources.EditPacketForm_Title, DisplayTag);
            }

            frameEditorControl.SetFrame(Frame, Selector, DisplayColor);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Frame = frameEditorControl.GetFrame();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void checkBoxReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            frameEditorControl.ReadOnly = checkBoxReadOnly.Checked;
        }

        private void checkBoxDisableEdit_CheckedChanged(object sender, EventArgs e)
        {
            DisableEditor = checkBoxDisableEdit.Checked;
        }

        private void EditPacketForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            WaitEvent.Set();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
