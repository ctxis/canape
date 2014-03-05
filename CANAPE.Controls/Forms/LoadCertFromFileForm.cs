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
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Security.Cryptography.X509Certificates;

namespace CANAPE.Forms
{
    public partial class LoadCertFromFileForm : Form
    {
        public LoadCertFromFileForm()
        {
            
            InitializeComponent();
        }

        private void checkBoxPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPassword.Enabled = checkBoxPassword.Checked;
        }

        private void checkBoxKey_CheckedChanged(object sender, EventArgs e)
        {
            textBoxKey.Enabled = checkBoxKey.Checked;
            btnBrowseKey.Enabled = checkBoxKey.Checked;
        }

        private void btnBrowseCert_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = CANAPE.Properties.Resources.LoadCertFromFileForm_CertFilter;
                dlg.ShowReadOnly = false;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxCertificate.Text = dlg.FileName;
                }
            }
        }

        private void btnBrowseKey_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = CANAPE.Properties.Resources.LoadCertFromFileForm_KeyFilter;
                dlg.ShowReadOnly = false;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxKey.Text = dlg.FileName;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public X509Certificate2 Certificate { get; private set; }

        public bool NoPrivateKey { get; set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string certFile = textBoxCertificate.Text;
                string keyFile = textBoxKey.Text;

                if (String.IsNullOrWhiteSpace(certFile))
                {
                    MessageBox.Show(this, CANAPE.Properties.Resources.LoadCertFromFileForm_SpecifyCert,
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (checkBoxKey.Checked && String.IsNullOrWhiteSpace(keyFile))
                {
                    MessageBox.Show(this, CANAPE.Properties.Resources.LoadCertFromFileForm_SpecifyKey,
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    X509Certificate2 cert = null;
                    string password = checkBoxPassword.Checked ? textBoxPassword.Text : null;
                    string ext = Path.GetExtension(certFile).ToLowerInvariant();

                    if (ext.Equals(".pfx") || ext.Equals(".p12"))
                    {
                        cert = CertificateUtils.LoadCertFromPfxFile(certFile, password);
                    }
                    else
                    {
                        cert = CertificateUtils.LoadCertFromOpenSslFile(certFile);
                        if (!NoPrivateKey)
                        {
                            if (!checkBoxKey.Checked)
                            {
                                RSA key = CertificateUtils.LoadRsaKeyFromFile(certFile, password, false);
                                if (key != null)
                                {
                                    cert.PrivateKey = key;
                                }
                            }
                        }
                    }

                    if (checkBoxKey.Checked)
                    {
                        cert.PrivateKey = CertificateUtils.LoadRsaKeyFromFile(keyFile, password, false);
                        if (cert.PrivateKey == null)
                        {
                            throw new CryptographicException(Properties.Resources.LoadCertFromFileForm_InvalidPrivateKeyFile);
                        }
                    }

                    if (cert.HasPrivateKey || NoPrivateKey)
                    {
                        Certificate = cert;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(this, CANAPE.Properties.Resources.LoadCertFromFileForm_NoPrivateKey,
                            CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
