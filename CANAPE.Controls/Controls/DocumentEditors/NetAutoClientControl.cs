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
using CANAPE.Documents;
using CANAPE.Documents.Net;
using CANAPE.Net;
using CANAPE.Forms;
using CANAPE.Documents.Net.Factories;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class NetAutoClientControl : UserControl, IReadOnlyControl
    {
        NetAutoClientDocument _document;
        private bool _readOnly;

        public NetAutoClientControl(NetAutoClientDocument document)
        {
            _document = document;
            
            InitializeComponent();
            proxyClientControl.Client = _document.Client;
            layerEditorControl.Layers = _document.Layers;         
            textBoxHost.Text = _document.Destination;
            numTcpPort.Value = _document.Port;
            checkBoxIpv6.Checked = _document.IPv6;
            checkBoxSpecifyTimeout.Checked = _document.SpecifyTimeout;
            numericTimeout.Enabled = _document.SpecifyTimeout;
            if (_document.Udp)
            {
                radioButtonUdp.Checked = true;
            }
            else
            {
                radioButtonTcp.Checked = true;
            }
            numericTimeout.Value = _document.TimeOutMilliSeconds;
            numericMaxConns.Value = _document.ConcurrentConnections;
            if (document.ClientFactory != null)
            {
                dataEndpointSelectionControl.Factory = _document.ClientFactory;
            }
            checkBoxLimitConns.Checked = _document.LimitConnections;
            numericTotalConns.Enabled = _document.LimitConnections;
            numericTotalConns.Value = _document.TotalConnections;
        }

        private void textBoxHost_TextChanged(object sender, EventArgs e)
        {
            _document.Destination = textBoxHost.Text;
        }

        private void numTcpPort_ValueChanged(object sender, EventArgs e)
        {
            _document.Port = (int)numTcpPort.Value;
        }

        private void radioButtonTcp_CheckedChanged(object sender, EventArgs e)
        {
            _document.Udp = !radioButtonTcp.Checked;
        }

        private void checkBoxIpv6_CheckedChanged(object sender, EventArgs e)
        {
            _document.IPv6 = checkBoxIpv6.Checked;
        }

        private void proxyClientControl_ClientChanged(object sender, EventArgs e)
        {
            _document.Client = proxyClientControl.Client;
        }

        private void numericMaxConns_ValueChanged(object sender, EventArgs e)
        {
            _document.ConcurrentConnections = (int)numericMaxConns.Value;
        }

        private void numericTimeout_ValueChanged(object sender, EventArgs e)
        {
            _document.TimeOutMilliSeconds = (long)numericTimeout.Value;
        }

        private void checkBoxSpecifyTimeout_CheckedChanged(object sender, EventArgs e)
        {
            numericTimeout.Enabled = checkBoxSpecifyTimeout.Checked;
            _document.SpecifyTimeout = checkBoxSpecifyTimeout.Checked;
        }

        private void numericUpDownTotalConns_ValueChanged(object sender, EventArgs e)
        {
            _document.TotalConnections = (int)numericTotalConns.Value;
        }

        private void checkBoxLimitConns_CheckedChanged(object sender, EventArgs e)
        {
            numericTotalConns.Enabled = checkBoxLimitConns.Checked;
            _document.LimitConnections = checkBoxLimitConns.Checked;
        }

        private void UpdateReadOnly()
        {
            bool bEnabled = !_readOnly;

            lblConn.Enabled = bEnabled;
            lblHost.Enabled = bEnabled;
            lblPort.Enabled = bEnabled;

            layerEditorControl.Enabled = bEnabled;
            proxyClientControl.Enabled = bEnabled;
            groupBoxClient.Enabled = bEnabled;
            textBoxHost.Enabled = bEnabled;
            dataEndpointSelectionControl.Enabled = bEnabled;
            checkBoxSpecifyTimeout.Enabled = bEnabled;
            numericMaxConns.Enabled = bEnabled;
            numericTimeout.Enabled = bEnabled;
            numericTotalConns.Enabled = bEnabled;
            numTcpPort.Enabled = bEnabled;
            checkBoxLimitConns.Enabled = bEnabled;
            checkBoxIpv6.Enabled = bEnabled;
            radioButtonTcp.Enabled = bEnabled;
            radioButtonUdp.Enabled = bEnabled;            
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

        private void dataEndpointSelectionControl_FactoryChanged(object sender, EventArgs e)
        {
            _document.ClientFactory = dataEndpointSelectionControl.Factory;
            _document.Dirty = true;
        }
    }
}
