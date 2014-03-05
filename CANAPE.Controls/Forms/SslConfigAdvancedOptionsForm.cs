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
using System.Security.Authentication;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Net.Layers;

namespace CANAPE.Forms
{
    public partial class SslConfigAdvancedOptionsForm : Form
    {
        SslNetworkLayerConfig _config;
        bool _hideClient;
        bool _hideServer;

        private void SetupComboBox(ComboBox comboBox, SslProtocols currProtocol)
        {
            foreach (SslProtocols sslValue in Enum.GetValues(typeof(SslProtocols)))
            {
                comboBox.Items.Add(sslValue);                
            }

            if (!Enum.IsDefined(typeof(SslProtocols), currProtocol))
            {
                currProtocol = SslProtocols.Default;
            }

            comboBox.SelectedItem = currProtocol;
        }

        public SslConfigAdvancedOptionsForm(SslNetworkLayerConfig config, bool hideClient, bool hideServer)
        {
            _config = config;
            _hideClient = hideClient;
            _hideServer = hideServer;

            
            InitializeComponent();
            if (hideClient)
            {                
                tabControl.TabPages.Remove(tabPageClient);
            }
            else
            {
                SetupComboBox(comboBoxClientProtocol, config.ClientProtocol);
                checkBoxVerifyServerCert.Checked = config.VerifyServerCertificate;
            }

            if (hideServer)
            {
                tabControl.TabPages.Remove(tabPageServer);
            }
            else
            {
                SetupComboBox(comboBoxServerProtocol, config.ServerProtocol);
                checkBoxRequireClientCert.Checked = config.RequireClientCertificate;
                checkBoxVerifyClientCert.Checked = config.VerifyClientCertificate;
                checkBoxVerifyClientCert.Enabled = config.RequireClientCertificate;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!_hideClient)
            {                
                _config.ClientProtocol = (SslProtocols)comboBoxClientProtocol.SelectedItem;
                _config.VerifyServerCertificate = checkBoxVerifyServerCert.Checked;
            }

            if (!_hideServer)
            {
                _config.ServerProtocol = (SslProtocols)comboBoxServerProtocol.SelectedItem;
                _config.RequireClientCertificate = checkBoxRequireClientCert.Checked;
                _config.VerifyClientCertificate = checkBoxVerifyClientCert.Checked;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void checkBoxRequireClientCert_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxVerifyClientCert.Enabled = checkBoxRequireClientCert.Checked;
        }
    }
}
