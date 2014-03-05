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
using CANAPE.Utils;
using System.IO;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    /// <summary>
    /// About form
    /// </summary>
    public partial class AboutForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AboutForm(bool splashScreen)
        {
            

            InitializeComponent();
            if (splashScreen)
            {                
                btnOk.Visible = false;
                FormBorderStyle = FormBorderStyle.None;
                timerClose.Enabled = true;
                ShowInTaskbar = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "CANAPE Version " + GeneralUtils.GetCanapeVersionString();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GuiUtils.OpenDocument(linkLabel.Text);
        }

    }
}
