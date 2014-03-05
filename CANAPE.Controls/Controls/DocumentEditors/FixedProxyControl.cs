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
using CANAPE.Documents.Net;

namespace CANAPE.Controls
{
    public partial class FixedProxyControl : UserControl, IReadOnlyControl
    {        
        FixedProxyDocument _document;
        bool _readOnly;

        public FixedProxyControl(FixedProxyDocument document)
        {
            _document = document;
            
            InitializeComponent();
            layerEditorControl.Layers = _document.Layers;
            proxyClientControl.Client = _document.Client;

            numLocalTcpPort.Value = (decimal)_document.LocalPort;
            numTcpPort.Value = (decimal)_document.Port;
            if (_document.Host != null)
            {
                textBoxHost.Text = _document.Host;
            }
            checkBoxGlobal.Checked = _document.AnyBind;
            checkBoxIpv6.Checked = _document.Ipv6Bind;

            if (_document.UdpEnable)
            {
                radioButtonUdp.Checked = true;
            }
            else
            {
                radioButtonTcp.Checked = true;
            }
        }

        private void FixedProxyControl_Load(object sender, EventArgs e)
        {           
            
        }

        private void numLocalTcpPort_ValueChanged(object sender, EventArgs e)
        {
            _document.LocalPort = (int)numLocalTcpPort.Value;
        }

        private void checkBoxGlobal_CheckedChanged(object sender, EventArgs e)
        {
            _document.AnyBind = checkBoxGlobal.Checked;
        }

        private void textBoxHost_TextChanged(object sender, EventArgs e)
        {
            _document.Host = textBoxHost.Text;
        }

        private void numTcpPort_ValueChanged(object sender, EventArgs e)
        {
            _document.Port = (int)numTcpPort.Value;
        }

        private void radioButtonUdp_CheckedChanged(object sender, EventArgs e)
        {
            _document.UdpEnable = radioButtonUdp.Checked;
            checkBoxBroadcast.Enabled = _document.UdpEnable;
        }

        private void proxyClientControl_ClientChanged(object sender, EventArgs e)
        {
            _document.Client = proxyClientControl.Client;
        }

        private void checkBoxIpv6_CheckedChanged(object sender, EventArgs e)
        {
            _document.Ipv6Bind = checkBoxIpv6.Checked;
        }

        private void checkBoxBroadcast_CheckedChanged(object sender, EventArgs e)
        {
            _document.EnableBroadcast = checkBoxBroadcast.Checked;
        }

        private void UpdateReadOnly()
        {
            bool bEnabled = !_readOnly;

            numLocalTcpPort.Enabled = bEnabled;
            numTcpPort.Enabled = bEnabled;
            layerEditorControl.Enabled = bEnabled;
            proxyClientControl.Enabled = bEnabled;
            lblRemoteHost.Enabled = bEnabled;
            lblLocalPort.Enabled = bEnabled;
            lblRemotePort.Enabled = bEnabled;
            checkBoxBroadcast.Enabled = radioButtonUdp.Checked && bEnabled;
            checkBoxGlobal.Enabled = bEnabled;
            checkBoxIpv6.Enabled = bEnabled;
            radioButtonTcp.Enabled = bEnabled;
            radioButtonUdp.Enabled = bEnabled;
            textBoxHost.Enabled = bEnabled;
        }

        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    UpdateReadOnly();
                }
            }
        }

        private void layerEditorControl_LayersUpdated(object sender, EventArgs e)
        {
            _document.Layers = layerEditorControl.Layers;
        }
    }
}
