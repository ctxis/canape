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
using System.Windows.Forms;
using CANAPE.Documents.Net.Factories;
using CANAPE.Forms;

namespace CANAPE.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ProxyClientControl : UserControl
    {
        IProxyClientFactory _client;
        bool _inupdate;
        string _currentScript;

        /// <summary>
        /// 
        /// </summary>
        public ProxyClientControl()
        {
            
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        private void UpdateClient(IProxyClientFactory client)
        {
            _inupdate = true;            

            if ((client is DefaultProxyClientFactory) && (!HideDefault))
            {
                radioDefault.Checked = true;
            }
            else if ((client is IpProxyClientFactory) || (client is DefaultProxyClientFactory))
            {
                radioDirect.Checked = true;
            }
            else if (client is TcpProxyClientFactory)
            {
                TcpProxyClientFactory c = (TcpProxyClientFactory)client;
                radioProxy.Checked = true;

                textBoxHost.Text = c.Hostname;
                numericUpDownPort.Value = c.Port;
                checkBoxIpv6.Checked = c.IPv6;

                if (client is HttpProxyClientFactory)
                {
                    radioHttp.Checked = true;
                }
                else
                {
                    SocksProxyClientFactory socks = (SocksProxyClientFactory)client;
                    if (socks.Version4)
                    {
                        radioSocksv4.Checked = true;
                    }
                    else
                    {
                        radioSocksv5.Checked = true;
                    }
                }
            }
            else if (client is SystemProxyClientFactory)
            {
                radioSystem.Checked = true;
            }
            else if (client is ScriptProxyClientFactory)
            {
                _currentScript = ((ScriptProxyClientFactory)client).Script ?? String.Empty;
                radioButtonPac.Checked = true;
            }

            _inupdate = false;
        }

        /// <summary>
        /// Event to indicate the client information has changed
        /// </summary>
        public event EventHandler ClientChanged;

        /// <summary>
        /// 
        /// </summary>
        protected void OnClientChanged()
        {
            if (!_inupdate)
            {
                RebuildClient();

                if (ClientChanged != null)
                {
                    ClientChanged.Invoke(this, new EventArgs());
                }
            }
        }

        private void RebuildClient()
        {
            if (radioDefault.Checked)
            {
                _client = new DefaultProxyClientFactory();
            }
            else if (radioDirect.Checked)
            {
                _client = new IpProxyClientFactory();
            }
            else if(radioProxy.Checked)
            {
                TcpProxyClientFactory factory;

                if (radioHttp.Checked)
                {
                    factory = new HttpProxyClientFactory();
                }
                else
                {
                    SocksProxyClientFactory f = new SocksProxyClientFactory();
                    f.Version4 = radioSocksv4.Checked;
                    f.SendHostname = checkBoxSendHostName.Checked;
                    factory = f;
                }

                factory.Hostname = textBoxHost.Text;
                factory.Port = (int)numericUpDownPort.Value;
                factory.IPv6 = checkBoxIpv6.Checked;

                _client = factory;
            }
            else if (radioSystem.Checked)
            {
                _client = new SystemProxyClientFactory();
            }
            else if (radioButtonPac.Checked)
            {
                _client = new ScriptProxyClientFactory() { Script = _currentScript };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IProxyClientFactory Client 
        {
            set
            {
                if (value == null)
                {
                    if (HideDefault)
                    {
                        _client = new IpProxyClientFactory();
                    }
                    else
                    {
                        _client = new DefaultProxyClientFactory();
                    }
                }
                else
                {
                    _client = value;
                }
            }

            get
            {
                return _client;
            }
        }

        public bool HideDefault
        {
            get;
            set;
        }

        private void ProxyClientControl_Load(object sender, EventArgs e)
        {
            if (HideDefault)
            {
                radioDefault.Visible = false;
                radioDefault.Checked = false;
                if (_client == null)
                {
                    _client = new IpProxyClientFactory();
                }
            }
            else
            {
                if (_client == null)
                {
                    _client = new DefaultProxyClientFactory();
                }
            }

            UpdateClient(_client);
        }

        private void EnableProxyConfig(bool bEnable)
        {            
            foreach (Control c in groupBoxDetails.Controls)
            {
                c.Enabled = bEnable;
            }

            groupBoxDetails.Enabled = bEnable;
        }

        private void radioDirect_CheckedChanged(object sender, EventArgs e)
        {            
            OnClientChanged();
        }

        private void textBoxHost_TextChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void numericUpDownPort_ValueChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void checkBoxIpv6_CheckedChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void radioHttp_CheckedChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void radioSocksv5_CheckedChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void radioSocksv4_CheckedChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void radioProxy_CheckedChanged(object sender, EventArgs e)
        {
            EnableProxyConfig(radioProxy.Checked);
        }

        private void radioSystem_CheckedChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void radioDefault_CheckedChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void checkBoxSendHostName_CheckedChanged(object sender, EventArgs e)
        {
            OnClientChanged();
        }

        private void UpdateScript()
        {
            using (ConfigurePacScriptForm frm = new ConfigurePacScriptForm())
            {
                frm.Script = _currentScript;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    _currentScript = frm.Script;
                }
            }
        }

        private void radioButtonPac_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPac.Checked && _currentScript == null)
            {
                UpdateScript();
            }

            btnPACScript.Enabled = radioButtonPac.Checked;

            OnClientChanged();
        }

        private void btnPACScript_Click(object sender, EventArgs e)
        {
            UpdateScript();

            OnClientChanged();
        }
    }
}
