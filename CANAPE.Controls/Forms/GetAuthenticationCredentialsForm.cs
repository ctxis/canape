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
using CANAPE.Security;

namespace CANAPE.Forms
{
    public partial class GetAuthenticationCredentialsForm : Form
    {
        public GetAuthenticationCredentialsForm(SecurityPrincipal principal)
        {
            InitializeComponent();
            comboBoxSaveType.SelectedIndex = 0;

            lblDescription.Text = String.Format(Properties.Resources.GetAuthenticationCredentialsForm_TitleFormat, 
                principal.Name, principal.Realm);
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public bool SaveCreds { get; set; }
        public bool SessionOnly { get; set; }                

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = !checkBoxShowPassword.Checked;
        }

        private void checkBoxSave_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSaveType.Enabled = checkBoxSave.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                MessageBox.Show(this, Properties.Resources.GetAuthenticationCredentialsForm_MustSpecifyUsername, Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Username = textBoxUsername.Text;
            Password = textBoxPassword.Text;
            Domain = textBoxDomain.Text;
            SaveCreds = checkBoxSave.Checked;
            SessionOnly = comboBoxSaveType.SelectedIndex != 0;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
