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
    public partial class CredentialEditorForm : Form
    {
        Func<SecurityPrincipal, bool> _checkPrincipal;

        public CredentialEditorForm(Func<SecurityPrincipal, bool> checkPrincipal)
        {
            _checkPrincipal = checkPrincipal;
            InitializeComponent();
        }

        public SecurityPrincipal Principal
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;

        }

        public string Password
        {
            get;
            set;
        }

        public string Domain
        {
            get;
            set;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxPrincipalRealm.Text))
            {
                MessageBox.Show(this,
                    Properties.Resources.CredentialsEditorForm_MustProvideARealm, Properties.Resources.MessageBox_ErrorString, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                MessageBox.Show(this,
                    Properties.Resources.CredentialsEditorForm_MustProvideAUsername, Properties.Resources.MessageBox_ErrorString, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
            else 
            {
                SecurityPrincipal p = new SecurityPrincipal(typeof(AuthenticationCredentials), textBoxPrincipalName.Text, textBoxPrincipalRealm.Text);
                if(_checkPrincipal(p) || (MessageBox.Show(this, 
                    String.Format(Properties.Resources.CredentialsEditorForm_PrincipalAlreadyExists, textBoxPrincipalName.Text, textBoxPrincipalRealm.Text),
                    Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))      
                {
                    Principal = p;                    
                    Username = textBoxUsername.Text;
                    Password = textBoxPassword.Text;
                    Domain = textBoxDomain.Text;

                    DialogResult = DialogResult.OK;
                    Close();             
                }
            }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {            
            textBoxPassword.UseSystemPasswordChar = !checkBoxShowPassword.Checked;         
        }

        private void CredentialEditorForm_Load(object sender, EventArgs e)
        {
            textBoxDomain.Text = Domain ?? "";
            textBoxPassword.Text = Password ?? "";
            if (Principal != null)
            {
                textBoxPrincipalName.Text = Principal.Name;
                textBoxPrincipalRealm.Text = Principal.Realm;
            }
            textBoxUsername.Text = Username ?? "";
        }
    }
}
