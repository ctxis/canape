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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using CANAPE.Controls.Forms;
using CANAPE.Security;
using CANAPE.Security.Cryptography.X509Certificates;

namespace CANAPE.Forms
{
    public partial class CreateCertForm : Form
    {
        X509Certificate2 _specifyCert;
        X509Certificate2 _templateCert;

        public CreateCertForm() 
            : this(false)
        {                        
        }

        public CreateCertForm(bool makeCA)
        {
            InitializeComponent();
            int keySize = 1024;

            while (keySize <= 16 * 1024)
            {
                comboBoxRsaKeySize.Items.Add(keySize);
                keySize *= 2;
            }

            foreach (object value in Enum.GetValues(typeof(CertificateHashAlgorithm)))
            {
                comboBoxHash.Items.Add(value);
            }
            comboBoxHash.SelectedIndex = 0;

            comboBoxRsaKeySize.SelectedIndex = 0;

            if (makeCA)
            {
                checkBoxCA.Checked = true;
                checkBoxCA.Enabled = false;
                radioButtonSelfSign.Checked = true;
                radioButtonDefaultCA.Enabled = false;
            }
        }

        public X509Certificate2 Certificate { get; private set; }        

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            int rsaKeySize = 0;

            if (radioButtonSimpleCN.Checked && String.IsNullOrWhiteSpace(textBoxCN.Text))
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.CreateCertForm_MustSpecifyCN, 
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButtonTemplate.Checked && _templateCert == null)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.CreateCertForm_MustSpecifyTemplate,
                   CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButtonSpecifyCA.Checked && _specifyCert == null)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.CreateCertForm_MustSpecifyCA,
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(comboBoxRsaKeySize.Text, out rsaKeySize))
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.CreateCertForm_MustSpecifyAValidRSAKeySize,
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    X509Certificate2 rootCert = null;

                    if (radioButtonSpecifyCA.Checked)
                    {
                        rootCert = _specifyCert;
                    }
                    else if (radioButtonDefaultCA.Checked)
                    {
                        rootCert = CertManager.GetRootCert();
                    }
                    else
                    {
                        // Self signed
                    }

                    if (radioButtonTemplate.Checked)
                    {
                        Certificate = CertificateUtils.CloneAndSignCertificate(_templateCert, rootCert, false, rsaKeySize, (CertificateHashAlgorithm)comboBoxHash.SelectedItem);
                    }
                    else
                    {
                        X509ExtensionCollection exts = new X509ExtensionCollection();
                        if (checkBoxCA.Checked)
                        {
                            exts.Add(new X509BasicConstraintsExtension(true, false, 0, true));
                        }

                        DateTime notBefore = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                        Certificate = CertificateUtils.CreateCert(rootCert,
                            new X500DistinguishedName(radioButtonSubject.Checked ? textBoxCN.Text : String.Format("CN={0}", textBoxCN.Text)), null, false, rsaKeySize,
                            (CertificateHashAlgorithm)comboBoxHash.SelectedItem, notBefore, notBefore.AddYears(10), exts);
                    }
                }
                catch (Win32Exception ex)
                {
                    MessageBox.Show(ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (CryptographicException ex)
                {
                    MessageBox.Show(ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        private void CreateCertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((Certificate == null) && (DialogResult != System.Windows.Forms.DialogResult.Cancel))
            {
                e.Cancel = true;
            }
        }

        private X509Certificate2 LoadFromStore(bool needPrivateKey)
        {
            X509Certificate2 ret = null;
            try
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection pcollection = new X509Certificate2Collection();

                foreach (var cert in fcollection)
                {
                    if (!needPrivateKey || cert.HasPrivateKey)
                    {
                        pcollection.Add(cert);
                    }
                }

                if (pcollection.Count > 0)
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(pcollection,
                        CANAPE.Properties.Resources.CreateCertForm_CertSelect,
                        CANAPE.Properties.Resources.CreateCertForm_CertSelectTitle,
                        X509SelectionFlag.SingleSelection);

                    if (scollection.Count > 0)
                    {
                        ret = scollection[0];

                        if (CertificateUtils.IsProtectedPrivateKey(ret))
                        {
                            if (MessageBox.Show(this, Properties.Resources.CreateCertForm_SpecifyPassword, 
                                Properties.Resources.CreateCertForm_SpecifyPasswordCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                using (GetPasswordForm frm = new GetPasswordForm())
                                {
                                    if (frm.ShowDialog(this) == DialogResult.OK)
                                    {
                                        CertificateUtils.ReloadProtectedPrivateKey(ret, frm.Password);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, CANAPE.Properties.Resources.CreateCertForm_NoStoreCerts,
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                store.Close();
            }
            catch (CryptographicException)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.CreateCertForm_StoreError,
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }

        private void btnLoadStore_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = LoadFromStore(true);

            if (cert != null)
            {
                textBoxSubject.Text = cert.Subject;
                _specifyCert = cert;
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            using (LoadCertFromFileForm frm = new LoadCertFromFileForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    _specifyCert = frm.Certificate;
                    textBoxSubject.Text = _specifyCert.Subject;
                }
            }
        }

        private void radioButtonSpecifyCA_CheckedChanged(object sender, EventArgs e)
        {
            btnLoadFile.Enabled = radioButtonSpecifyCA.Checked;
            btnLoadStore.Enabled = radioButtonSpecifyCA.Checked;
            textBoxSubject.Enabled = radioButtonSpecifyCA.Checked;
            lblSubject.Enabled = radioButtonSpecifyCA.Checked;
        }

        private void radioButtonTemplate_CheckedChanged(object sender, EventArgs e)
        {
            btnLoadFileCert.Enabled = radioButtonTemplate.Checked;
            btnLoadStoreCert.Enabled = radioButtonTemplate.Checked;
            textBoxCN.ReadOnly = radioButtonTemplate.Checked;
            checkBoxCA.Enabled = radioButtonTemplate.Checked;

            textBoxCN.Text = String.Empty;
            _templateCert = null;
        }

        private void btnLoadStoreCert_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = LoadFromStore(false);

            if (cert != null)
            {
                textBoxCN.Text = cert.Subject;
                _templateCert = cert;
            }
        }

        private void btnLoadFileCert_Click(object sender, EventArgs e)
        {
            using (LoadCertFromFileForm frm = new LoadCertFromFileForm())
            {
                frm.NoPrivateKey = true;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    _templateCert = frm.Certificate;
                    textBoxCN.Text = _templateCert.Subject;
                }
            }
        }
    }
}
