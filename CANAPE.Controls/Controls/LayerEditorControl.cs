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
using System.ComponentModel;
using System.Windows.Forms;
using CANAPE.Documents.Extension;
using CANAPE.Documents.Net.Factories;
using CANAPE.Forms;
using CANAPE.Net.Layers;

namespace CANAPE.Controls
{
    public partial class LayerEditorControl : UserControl
    {
        List<INetworkLayerFactory> _layers;

        public LayerEditorControl()
        {
            
            InitializeComponent();
            _layers = new List<INetworkLayerFactory>();

            foreach (var ext in NetworkLayerFactoryManager.Instance.GetExtensions())
            {
                ToolStripItem item = addToolStripMenuItem.DropDownItems.Add(ext.ExtensionAttribute.Name);

                item.ToolTipText = ext.Description;
                item.Tag = ext.ExtensionType;
                item.Click += item_Click;
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;

            if (item != null)
            {
                try
                {
                    Type type = (Type)item.Tag;

                    INetworkLayerFactory factory = (INetworkLayerFactory)Activator.CreateInstance(type);

                    factory.Binding = Binding;

                    _layers.Add(factory);

                    UpdateLayers();

                    listViewLayers.SelectedIndices.Add(_layers.Count - 1);

                    OnLayersUpdated();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateLayers()
        {
            listViewLayers.Items.Clear();
            int count = 0;
            
            foreach (INetworkLayerFactory layer in _layers)
            {
                ListViewItem item = listViewLayers.Items.Add(count.ToString());
                count++;

                item.SubItems.Add(layer.Description ?? layer.GetType().ToString());             

                item.Tag = layer;
            }
        }

        private void RefreshLayers()
        {
            foreach (ListViewItem item in listViewLayers.Items)
            {
                INetworkLayerFactory layer = (INetworkLayerFactory)item.Tag;

                item.SubItems[1].Text = layer.Description;
            } 
        }

        protected void OnLayersUpdated()
        {
            if (LayersUpdated != null)
            {
                LayersUpdated(this, new EventArgs());
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public INetworkLayerFactory[] Layers
        {
            get
            {
                return _layers.ToArray();
            }

            set
            {
                _layers.Clear();

                if (value != null)
                {
                    _layers.AddRange(value);
                }

                UpdateLayers();
            }
        }

        public event EventHandler LayersUpdated;

        private void buttonFilterUp_Click(object sender, EventArgs e)
        {
            if ((listViewLayers.SelectedIndices.Count > 0) && (_layers.Count > 1))
            {
                int idx = listViewLayers.SelectedIndices[0];
                if (idx > 0)
                {
                    INetworkLayerFactory factory = _layers[idx];

                    _layers[idx] = _layers[idx - 1];
                    _layers[idx - 1] = factory;

                    UpdateLayers();
                    listViewLayers.SelectedIndices.Add(idx - 1);

                    OnLayersUpdated();
                }
            }

            listViewLayers.Focus();
        }

        private void buttonFilterDown_Click(object sender, EventArgs e)
        {
            if ((listViewLayers.SelectedIndices.Count > 0) && (_layers.Count > 1))
            {
                int idx = listViewLayers.SelectedIndices[0];
                if (idx < (_layers.Count - 1))
                {
                    INetworkLayerFactory factory = _layers[idx];

                    _layers[idx] = _layers[idx + 1];
                    _layers[idx + 1] = factory;

                    UpdateLayers();
                    listViewLayers.SelectedIndices.Add(idx + 1);

                    OnLayersUpdated();
                }
            }

            listViewLayers.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SelectLayerForm frm = new SelectLayerForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    INetworkLayerFactory layer = frm.Factory;

                    layer.Binding = Binding;

                    _layers.Add(layer);

                    UpdateLayers();

                    listViewLayers.SelectedIndices.Add(_layers.Count - 1);

                    OnLayersUpdated();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewLayers.SelectedIndices.Count > 0)
            {
                int idx = listViewLayers.SelectedIndices[0];

                listViewLayers.Items.RemoveAt(idx);
                _layers.RemoveAt(idx);

                propertyGridLayer.SelectedObject = null;

                OnLayersUpdated();
            }
        }

        private void propertyGridLayer_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RefreshLayers();
            OnLayersUpdated();
        }

        private void listViewLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewLayers.SelectedItems.Count > 0)
            {
                propertyGridLayer.SelectedObject = listViewLayers.SelectedItems[0].Tag;
            }
        }

        /// <summary>
        /// Default binding mode
        /// </summary>
        public NetworkLayerBinding Binding { get; set; }
    }
}
