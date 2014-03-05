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
using CANAPE.Controls;
using CANAPE.Net.Layers;

namespace CANAPE.Forms
{
    public partial class SslConfigForm : Form
    {
        SslNetworkLayerConfig _config;

        public SslConfigForm(SslNetworkLayerConfig config, NetworkLayerBinding binding)
        {
            
            InitializeComponent();
            _config = config.Clone();
            sslConfigControl.Config = _config;
            sslConfigControl.LayerBinding = binding != NetworkLayerBinding.Default ? binding : NetworkLayerBinding.ClientAndServer;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SslNetworkLayerConfig Config
        {
            get { return _config; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
