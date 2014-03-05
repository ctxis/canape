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

using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net.Layers;

namespace CANAPE.Forms
{
    public partial class LayerEditorForm : Form
    {
        public INetworkLayerFactory[] Layers
        {
            get
            {
                return layerEditorControl.Layers;
            }

            set
            {
                INetworkLayerFactory[] layers;

                if (value == null)
                {
                    layers = new INetworkLayerFactory[0];
                }
                else
                {
                    layers = new INetworkLayerFactory[value.Length];

                    for (int i = 0; i < layers.Length; ++i)
                    {
                        layers[i] = value[i].Clone();
                    }
                }

                layerEditorControl.Layers = layers;
            }
        }

        public LayerEditorForm()
        {
            
            InitializeComponent();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
