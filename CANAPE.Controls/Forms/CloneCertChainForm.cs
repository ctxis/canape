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
using System.IO;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.DataAdapters;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net.Clients;
using CANAPE.Net.Tokens;
using CANAPE.Security.Cryptography.X509Certificates;
using CANAPE.Utils;

namespace CANAPE.Forms
{
    public partial class CloneCertChainForm : Form
    {
        public CloneCertChainForm()
        {
            
            InitializeComponent();            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxDest.Text = dlg.SelectedPath;
                }
            }
        }

        X509Certificate2Collection collection;

        X509Certificate2 CloneAndExportCert(X509Certificate2 rootCert, X509Certificate toClone)
        {
            X509Certificate2 cert = CertificateUtils.CloneAndSignCertificate(toClone, rootCert, false);

            collection.Add(cert);

            return cert;
        }

        bool VerifyCallback(
            Object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            X509Certificate2 rootCert = null;
            for (int i = chain.ChainElements.Count - 1; i >= 0; --i)
            {
                rootCert = CloneAndExportCert(rootCert, chain.ChainElements[i].Certificate);
            }
            return true;
        }

        private void CloneCertChain(Uri url, string destination)
        {
            IProxyClientFactory factory = proxyClientControl.Client;

            if (factory == null)
            {
                factory = new IpProxyClientFactory();
            }

            ProxyClient client = factory.Create(new Logger());
            collection = new X509Certificate2Collection();

            using (IDataAdapter adapter = client.Connect(new IpProxyToken(null, url.Host, url.Port, IpProxyToken.IpClientType.Tcp, false),
                new Logger(), new Nodes.MetaDictionary(), new Nodes.MetaDictionary(), new PropertyBag(), new Security.CredentialsManagerService()))
            {
                DataAdapterToStream stm = new DataAdapterToStream(adapter);

                using (SslStream ssl = new SslStream(stm, false, VerifyCallback))
                {
                    ssl.AuthenticateAsClient(url.Host);
                }                                
            }

            if (collection.Count > 0)
            {
                File.WriteAllBytes(Path.Combine(destination, String.Format("certchain_{0}.pfx", url.Host)), collection.Export(X509ContentType.Pfx));
                int count = 1;

                foreach (X509Certificate2 cert in collection)
                {
                    string path = Path.Combine(destination, String.Format("cert_{0}_{1}.cer", url.Host, count++));

                    File.WriteAllText(path, CertificateUtils.ExportToPEM(cert) +
                        CertificateUtils.ExportToPEM((RSA)cert.PrivateKey, null));
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Uri url = new Uri(textBoxUrl.Text);

                if ((url.Scheme != Uri.UriSchemeHttps) && (url.Scheme != Uri.UriSchemeNetTcp))
                {
                    MessageBox.Show(this, Properties.Resources.CloneCertChainForm_UrlMustBeHttpsOrTcp,
                        Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(textBoxDest.Text) || !Directory.Exists(textBoxDest.Text))
                {
                    MessageBox.Show(this, Properties.Resources.CloneCertChainForm_DestinationMustBeADirectoryAndExist,
                        Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    CloneCertChain(url, textBoxDest.Text);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowProxyOptions_Click(object sender, EventArgs e)
        {
            proxyClientControl.Visible = !proxyClientControl.Visible;
            btnShowProxyOptions.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            btnShowProxyOptions.Invalidate();
        }

    }
}
