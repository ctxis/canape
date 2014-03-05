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
using CANAPE.Forms;
using CANAPE.Net.Layers;
using CANAPE.Security.Cryptography.X509Certificates;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    public partial class SslConfigControl : UserControl
    {
        SslNetworkLayerConfig _config;

        public SslConfigControl()
        {
            _config = new SslNetworkLayerConfig(false, false);
            _config.Enabled = true;
            LayerBinding = NetworkLayerBinding.ClientAndServer;
            
            InitializeComponent();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SslNetworkLayerConfig Config 
        { 
            get
            {
                return _config;
            }

            set
            {
                UpdateConfig(value);
            }
        }

        public NetworkLayerBinding LayerBinding { get; set; }

        private bool DisableClientCert
        {
            get { return (LayerBinding & NetworkLayerBinding.Client) != NetworkLayerBinding.Client; }
        }
        
        private bool DisableServerCert
        {
            get { return (LayerBinding & NetworkLayerBinding.Server) != NetworkLayerBinding.Server; }
        }            

        private void SetSslEnabled(bool value)
        {
            if (value != _config.Enabled)
            {
                _config.Enabled = value;
                OnConfigChanged();
            }
        }

        private void SetSpecifyCert(bool value)
        {
            if (_config.SpecifyServerCert != value)
            {
                _config.SpecifyServerCert = value;

                OnConfigChanged();
            }
        }

        public event EventHandler ConfigChanged;

        protected virtual void OnConfigChanged()
        {
            if (ConfigChanged != null)
            {
                ConfigChanged.Invoke(this, new EventArgs());
            }
        }

        private void SetCertificate(X509Certificate2 cert, bool fromStore)
        {
            if (cert.HasPrivateKey)
            {
                textBoxSubject.Text = cert.SubjectName.Format(false);
                if (fromStore)
                {
                    _config.ServerCertificate = new X509CertificateContainer(StoreName.My, StoreLocation.CurrentUser, cert);
                }
                else
                {
                    _config.ServerCertificate = new X509CertificateContainer(cert);
                }

                OnConfigChanged();
            }
            else
            {
                MessageBox.Show(CANAPE.Properties.Resources.SslConfigControl_SpecifyCertPrivateKey, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetClientCertificate(X509Certificate2 cert, bool fromStore)
        {
            if (cert.HasPrivateKey)
            {
                textBoxClientCert.Text = cert.SubjectName.Format(false);
                _config.ClientCertificates.Clear();

                if (fromStore)
                {
                    _config.ClientCertificates.Add(new X509CertificateContainer(StoreName.My, StoreLocation.CurrentUser, cert));
                }
                else
                {
                    _config.ClientCertificates.Add(new X509CertificateContainer(cert));
                }

                OnConfigChanged();
            }
            else
            {
                MessageBox.Show(CANAPE.Properties.Resources.SslConfigControl_SpecifyCertPrivateKey, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private X509Certificate2 LoadCertFromPfxFile(string pfxFile)
        {
            X509Certificate2 ret = null;

            try
            {
                ret = new X509Certificate2(File.ReadAllBytes(pfxFile));
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show(ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }

        private X509Certificate2 LoadCertFromFile()
        {
            using (LoadCertFromFileForm frm = new LoadCertFromFileForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    return frm.Certificate;
                }
            }

            return null;
        }

        private void buttonLoadFromFile_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = LoadCertFromFile();

            if (cert != null)
            {
                SetCertificate(cert, false);
            }            
        }

        private X509Certificate2 LoadCertFromStore()
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
                    if (cert.HasPrivateKey)
                    {
                        pcollection.Add(cert);
                    }
                }

                if (pcollection.Count > 0)
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(pcollection, "Certificate Select", "Select a certificate from the following list", X509SelectionFlag.SingleSelection);

                    if (scollection.Count > 0)
                    {
                        ret = scollection[0];
                    }
                }
                else
                {
                    MessageBox.Show(Properties.Resources.SslConfigControl_NoPrivateKeyInStore, 
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                store.Close();
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show(String.Format(Properties.Resources.SslConfigControl_ErrorLoadingStoreCert, ex.Message), 
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }

        private void buttonLoadStore_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = LoadCertFromStore();

            if (cert != null)
            {
                SetCertificate(cert, true);
            }
        }

        private void ViewCertificate(X509CertificateContainer cert)
        {            
            if ((cert != null) && (cert.PublicCertificate != null))
            {
                X509Certificate2UI.DisplayCertificate(cert.PublicCertificate, Handle);
            }
        }

        private void buttonView_Click(object sender, EventArgs e)
        {            
            ViewCertificate(_config.ServerCertificate);
        }

        private void EnableSpecifyControls(bool bEnable)
        {
            textBoxSubject.Enabled = bEnable;
            buttonLoadFromFile.Enabled = bEnable;
            buttonLoadStore.Enabled = bEnable;
            buttonView.Enabled = bEnable;
            buttonCreate.Enabled = bEnable;
            labelSubjectName.Enabled = bEnable;
        }

        private void EnableClientControls(bool bEnable)
        {
            textBoxClientCert.Enabled = bEnable;            
            btnLoadClient.Enabled = bEnable;
            btnLoadStoreClient.Enabled = bEnable;
            btnViewClient.Enabled = bEnable;
            lblClientCert.Enabled = bEnable;
            buttonClear.Enabled = bEnable;
        }

        private void radioSpecify_CheckedChanged(object sender, EventArgs e)
        {
            SetSpecifyCert(radioSpecify.Checked);
            EnableSpecifyControls(radioSpecify.Checked);
        }

        private X509Certificate2 CreateCert()
        {
            X509Certificate2 ret = null;
            using (CreateCertForm frm = new CreateCertForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ret = frm.Certificate;
                }
            }

            return ret;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = CreateCert();

            if (cert != null)
            {
                SetCertificate(cert, false);
            }
        }

        private void EnableControls(bool bEnable)
        {
            groupBox.Enabled = bEnable;
            radioAuto.Enabled = bEnable && !DisableServerCert;
            radioSpecify.Enabled = bEnable && !DisableServerCert;
            btnAdvancedOptions.Enabled = bEnable;
            
            if (bEnable && radioSpecify.Checked && !DisableServerCert)
            {
                EnableSpecifyControls(true);
            }
            else
            {
                EnableSpecifyControls(false);
            }

            if (!DisableClientCert)
            {
                EnableClientControls(bEnable);
            }
        }

        private void checkBoxSsl_CheckedChanged(object sender, EventArgs e)
        {
            SetSslEnabled(checkBoxSsl.Checked);
            EnableControls(checkBoxSsl.Checked);
        }

        private void UpdateConfig(SslNetworkLayerConfig config)
        {
            _config = config;

            if(_config.SpecifyServerCert)
            {
                radioSpecify.Checked = true;
            }

            if (_config.ServerCertificate != null)
            {
                if ((_config.ServerCertificate.Certificate != null) && (_config.ServerCertificate.Certificate.HasPrivateKey))
                {
                    textBoxSubject.Text = _config.ServerCertificate.Certificate.SubjectName.Format(false);
                }
                else
                {
                    _config.ServerCertificate = null;
                }
            }

            if (_config.ClientCertificates.Count > 0)
            {
                X509CertificateContainer clientCertificate = _config.ClientCertificates[0];

                if ((clientCertificate.Certificate != null) && (clientCertificate.Certificate.HasPrivateKey))
                {
                    textBoxClientCert.Text = clientCertificate.Certificate.SubjectName.Format(false);
                }
                else
                {
                    _config.ClientCertificates.Clear();
                }
            }

            if (_config.Enabled)
            {
                checkBoxSsl.Checked = true;
                EnableControls(true);
            }
            else
            {
                checkBoxSsl.Checked = false;
                EnableControls(false);
            }
        }

        private void SslConfigControl_Load(object sender, EventArgs e)
        {
            if (DisableClientCert)
            {
                textBoxClientCert.Enabled = false;
                lblClientCert.Enabled = false;
                btnViewClient.Enabled = false;
                btnLoadStoreClient.Enabled = false;
                btnLoadClient.Enabled = false;
                buttonClear.Enabled = false;
                splitContainer.Panel2Collapsed = true;
            }

            if (DisableServerCert)
            {
                splitContainer.Panel1Collapsed = true;
            }

            if (GeneralUtils.IsRunningOnMono())
            {
                buttonView.Visible = false;
                btnViewClient.Visible = false;
            }
        }

        private void btnLoadClient_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = LoadCertFromFile();

            if (cert != null)
            {
                SetClientCertificate(cert, false);
            }
        }

        private void btnLoadStoreClient_Click(object sender, EventArgs e)
        {
            X509Certificate2 cert = LoadCertFromStore();

            if (cert != null)
            {
                SetClientCertificate(cert, true);
            }
        }

        private void btnViewClient_Click(object sender, EventArgs e)
        {            
            if (_config.ClientCertificates.Count > 0)
            {
                ViewCertificate(_config.ClientCertificates[0]);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (_config.ClientCertificates.Count > 0)
            {
                _config.ClientCertificates.Clear();
                textBoxClientCert.Text = null;
                OnConfigChanged();
            }
        }

        private void btnAdvancedOptions_Click(object sender, EventArgs e)
        {
            using (SslConfigAdvancedOptionsForm frm = new SslConfigAdvancedOptionsForm(_config, DisableClientCert, DisableServerCert))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    // Do not need to do anything
                }
            }
        }
    }
}
