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
using CANAPE.Documents.Net;
using CANAPE.Net.Servers;

namespace CANAPE.Forms
{
    public partial class HttpProxyOptionsForm : Form
    {
        FullHttpProxyDocument _document;
        HttpProxyServerConfig _config;

        public HttpProxyOptionsForm(FullHttpProxyDocument document)
        {
            _document = document;
            _config = _document.Config.Clone();
            
            InitializeComponent();
            sslConfigControl.Config = _config.SslConfig;
            checkBoxEnableAuth.Checked = _config.RequireAuth;
            textBoxUsername.Text = _config.ProxyUsername;
            textBoxPassword.Text = _config.ProxyPassword;
            checkBoxHttp10.Checked = _config.Version10Proxy;

            if (document is HttpReverseProxyDocument)
            {
                tabControl.TabPages.Remove(tabPageOptions);                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _config.ProxyPassword = textBoxPassword.Text;
            _config.ProxyUsername = textBoxUsername.Text;
            _config.RequireAuth = checkBoxEnableAuth.Checked;
            _config.Version10Proxy = checkBoxHttp10.Checked;
            _document.Config = _config;            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void checkBoxEnableAuth_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPassword.Enabled = checkBoxEnableAuth.Checked;
            textBoxUsername.Enabled = checkBoxEnableAuth.Checked;
        }
    }
}
