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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Documents;
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net.Layers;

namespace CANAPE.Forms
{
    internal partial class EditFilterForm : Form
    {
        public IpProxyFilterFactory Filter { get; set; }

        public EditFilterForm()
        {
            
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {            
            if(String.IsNullOrWhiteSpace(textBoxHost.Text))
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.EditFilterForm_MustProvideHost,
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Filter = new IpProxyFilterFactory();
            Filter.Address = textBoxHost.Text;
            Filter.Port = (int)numFilterPort.Value;            
            Filter.ClientType = Net.Tokens.IpProxyToken.IpClientType.Tcp;
            Filter.Block = checkBoxBlock.Checked;
            Filter.IsRegex = checkBoxRegex.Checked;
            Filter.Disabled = !checkBoxEnabled.Checked;

            if (Filter.IsRegex)
            {
                try
                {
                    new Regex(Filter.Address, RegexOptions.IgnoreCase);
                }
                catch(ArgumentException)
                {
                    MessageBox.Show(this, CANAPE.Properties.Resources.EditFilterForm_InvalidRegex, 
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Filter.Layers = layerEditorControl.Layers;

            if (comboBoxNetgraph.SelectedItem != null)
            {
                NetGraphDocument doc = comboBoxNetgraph.SelectedItem as NetGraphDocument;

                Filter.Graph = doc;
            }

            if (checkBoxRedirect.Checked)
            {
                Filter.RedirectAddress = textBoxRedirectHost.Text;
                Filter.RedirectPort = (int)numRedirectPort.Value;
            }
            else
            {
                Filter.RedirectAddress = String.Empty;
                Filter.RedirectPort = 0;
            }

            if (checkBoxClient.Checked)
            {
                Filter.Client = proxyClientControl.Client;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void UpdateNetgraphs()
        {
            var documents = CANAPEProject.CurrentProject.GetDocumentsByType(typeof(NetGraphDocument));

            comboBoxNetgraph.Items.Clear();

            foreach (var doc in documents)
            {
                comboBoxNetgraph.Items.Add(doc);
            }
        }

        private void SetupFilter()
        {
            textBoxHost.Text = Filter.Address;
            checkBoxRegex.Checked = Filter.IsRegex;
            numFilterPort.Value = (decimal)Filter.Port;

            if (Filter.Layers != null)
            {
                List<INetworkLayerFactory> layers = new List<INetworkLayerFactory>();

                foreach (INetworkLayerFactory factory in Filter.Layers)
                {
                    INetworkLayerFactory cloned = factory.Clone();

                    layers.Add(cloned);
                }

                layerEditorControl.Layers = layers.ToArray();
            }

            if (Filter.Graph != null)
            {
                foreach (NetGraphDocument doc in comboBoxNetgraph.Items)
                {
                    if (doc == Filter.Graph)
                    {
                        comboBoxNetgraph.SelectedItem = doc;
                    }                   
                }

                if (comboBoxNetgraph.SelectedItem == null)
                {
                    Filter.Graph = null;
                }
            }

            if(!String.IsNullOrWhiteSpace(Filter.RedirectAddress) || (Filter.RedirectPort > 0))
            {
                checkBoxRedirect.Checked = true;
                textBoxRedirectHost.Text = Filter.RedirectAddress;
                numRedirectPort.Value = Filter.RedirectPort;
            }

            if (Filter.Client != null)
            {
                checkBoxClient.Checked = true;
                proxyClientControl.Client = Filter.Client;
            }

            checkBoxBlock.Checked = Filter.Block;
            checkBoxEnabled.Checked = !Filter.Disabled;
        }

        private void EditFilterForm_Load(object sender, EventArgs e)
        {
            if (Filter == null)
            {
                Filter = new IpProxyFilterFactory();
                Filter.Address = "*";
                Filter.Port = 0;                
                Filter.Graph = null;
            }

            UpdateNetgraphs();
            SetupFilter();
        }

        private void comboBoxNetgraph_DropDown(object sender, EventArgs e)
        {
            UpdateNetgraphs();
        }

        private void checkBoxRedirect_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRedirectHost.Enabled = checkBoxRedirect.Checked;
            numRedirectPort.Enabled = checkBoxRedirect.Checked;
            labelRedirectHost.Enabled = checkBoxRedirect.Checked;
            labelRedirectPort.Enabled = checkBoxRedirect.Checked;
        }

        private void checkBoxClient_CheckedChanged(object sender, EventArgs e)
        {
            proxyClientControl.Enabled = checkBoxClient.Checked;
        }

        private void layerEditorControl_LayersUpdated(object sender, EventArgs e)
        {            
        }
    }
}
