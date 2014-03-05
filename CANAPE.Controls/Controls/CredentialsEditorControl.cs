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
using System.Windows.Forms;
using CANAPE.Forms;
using CANAPE.Security;

namespace CANAPE.Controls
{
    public partial class CredentialsEditorControl : UserControl
    {
        IDictionary<SecurityPrincipal, ICredentialObject> _credentials;

        public CredentialsEditorControl()
        {
            InitializeComponent();
        }

        private void AddCredential(SecurityPrincipal principal, AuthenticationCredentials creds)
        {
            ListViewItem item = new ListViewItem(String.Format("{0}@{1}", principal.Name, principal.Realm));
            item.SubItems.Add(creds.Username);
            item.SubItems.Add(creds.Domain);
        }

        public void UpdateCredentials()
        {
            listViewCredentials.SuspendLayout();

            listViewCredentials.Items.Clear();

            foreach (KeyValuePair<SecurityPrincipal, ICredentialObject> pair in _credentials)
            {
                AuthenticationCredentials creds = pair.Value as AuthenticationCredentials;

                if (creds != null)
                {
                    ListViewItem item = new ListViewItem(String.Format("{0}@{1}", pair.Key.Name, pair.Key.Realm));
                    item.SubItems.Add(creds.Username);
                    item.SubItems.Add(creds.Domain);
                    item.Tag = pair;
                    listViewCredentials.Items.Add(item);
                }
            }

            listViewCredentials.ResumeLayout();
        }

        public void SetCredentials(IDictionary<SecurityPrincipal, ICredentialObject> credentials)
        {
            _credentials = credentials;
            UpdateCredentials();
        }

        public event EventHandler CredentialsUpdated;

        private void OnCredentialsUpdated()
        {
            if (CredentialsUpdated != null)
            {
                CredentialsUpdated(this, new EventArgs());
            }
        }

        private void editCredentialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewCredentials.SelectedItems.Count > 0)
            {
                KeyValuePair<SecurityPrincipal, ICredentialObject> pair = (KeyValuePair<SecurityPrincipal, ICredentialObject>)listViewCredentials.SelectedItems[0].Tag;
                using (CredentialEditorForm frm = new CredentialEditorForm((p) => !p.Equals(pair.Key) && CheckPrincipal(p))) 
                {
                    frm.Principal = pair.Key;
                    AuthenticationCredentials c = pair.Value as AuthenticationCredentials;
                    frm.Username = c.Username;
                    frm.Password = c.Password;
                    frm.Domain = c.Domain;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        SecurityPrincipal p = frm.Principal;

                        AuthenticationCredentials creds = new AuthenticationCredentials(frm.Username, frm.Domain, frm.Password);

                        _credentials[p] = creds;
                        UpdateCredentials();
                        OnCredentialsUpdated();
                    }
                }
            }
        }

        private bool CheckPrincipal(SecurityPrincipal principal)
        {           
            return !_credentials.ContainsKey(principal);
        }

        private void addCredentialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CredentialEditorForm frm = new CredentialEditorForm(CheckPrincipal))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {                    
                    AuthenticationCredentials creds = new AuthenticationCredentials(frm.Username, frm.Domain, frm.Password);

                    _credentials[frm.Principal] = creds;
                    UpdateCredentials();
                    OnCredentialsUpdated();
                }
            }
        }

        private void removeCredentialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewCredentials.SelectedItems.Count > 0)
            {
                KeyValuePair<SecurityPrincipal, ICredentialObject> pair = (KeyValuePair<SecurityPrincipal, ICredentialObject>)listViewCredentials.SelectedItems[0].Tag;

                _credentials.Remove(pair.Key);

                listViewCredentials.SelectedItems[0].Remove();
                OnCredentialsUpdated();
            }
        }
    }
}
