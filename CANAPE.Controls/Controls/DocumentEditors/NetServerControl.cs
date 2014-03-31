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
using CANAPE.Documents.Net.Factories;
using CANAPE.Forms;

namespace CANAPE.Controls
{
    public partial class NetServerControl : UserControl, IReadOnlyControl
    {
        NetServerDocument _document;
        bool _readOnly;

        public NetServerControl(NetServerDocument document)
        {
            _document = document;
            
            InitializeComponent();

            layerEditorControl.Layers = _document.Layers;
            if (document.ServerFactory != null)
            {
                dataEndpointSelectionControl.Factory = document.ServerFactory;                
            }

            numLocalTcpPort.Value = (decimal)_document.LocalPort;

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

        private void numLocalTcpPort_ValueChanged(object sender, EventArgs e)
        {
            _document.LocalPort = (int)numLocalTcpPort.Value;
        }

        private void checkBoxGlobal_CheckedChanged(object sender, EventArgs e)
        {
            _document.AnyBind = checkBoxGlobal.Checked;
        }

        private void radioButtonUdp_CheckedChanged(object sender, EventArgs e)
        {
            _document.UdpEnable = radioButtonUdp.Checked;
            checkBoxBroadcast.Enabled = _document.UdpEnable;
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
            bool bEnable = !_readOnly;

            lblLocalPort.Enabled = bEnable;
            numLocalTcpPort.Enabled = bEnable;
            checkBoxBroadcast.Enabled = radioButtonUdp.Checked && bEnable;
            checkBoxGlobal.Enabled = bEnable;
            checkBoxIpv6.Enabled = bEnable;
            dataEndpointSelectionControl.Enabled = bEnable;            
            groupBoxServer.Enabled = bEnable;
            layerEditorControl.Enabled = bEnable;
            radioButtonUdp.Enabled = bEnable;
            radioButtonTcp.Enabled = bEnable;
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
            _document.ServerFactory = dataEndpointSelectionControl.Factory;
            _document.Dirty = true;
        }
    }
}
