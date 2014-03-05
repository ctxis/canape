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
using System.IO.Ports;
using System.Windows.Forms;

namespace CANAPE.Documents.ComPort
{
    public partial class SerialPortConfigurationControl : UserControl
    {
        SerialPortConfiguration _config;

        public SerialPortConfigurationControl()
        {
            InitializeComponent();
            foreach (string name in SerialPort.GetPortNames())
            {
                comboBoxPort.Items.Add(name);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SerialPortConfiguration Config 
        {
            get { return _config; }
            set
            {
                _config = value;
                comboBoxPort.Text = _config.Port;
                comboBoxFlowControl.FromEnum(_config.FlowControl);
                comboBoxParity.FromEnum(_config.Parity);
                comboBoxStopBits.FromEnum(_config.StopBits);
                numericUpDownBaudRate.Value = _config.BaudRate;
                numericUpDownDataBits.Value = _config.DataBits;
            }
        }

        private void comboBoxPort_TextUpdate(object sender, EventArgs e)
        {
            _config.Port = comboBoxPort.Text;
        }

        private void numericUpDownBaudRate_ValueChanged(object sender, EventArgs e)
        {
            _config.BaudRate = (int)numericUpDownBaudRate.Value;
        }

        private void comboBoxFlowControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.FlowControl = (Handshake)comboBoxFlowControl.SelectedItem;
        }

        private void comboBoxParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.Parity = (Parity)comboBoxParity.SelectedItem;
        }

        private void comboBoxStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.StopBits = (StopBits)comboBoxStopBits.SelectedItem;
        }

        private void numericUpDownDataBits_ValueChanged(object sender, EventArgs e)
        {
            _config.DataBits = (int)numericUpDownDataBits.Value;
        }
    }
}
