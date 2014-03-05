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
using CANAPE.Net;
using CANAPE.Nodes;

namespace CANAPE.Controls
{
    /// <summary>
    /// Control to display a netgraph in list form
    /// </summary>
    public partial class ActiveNetgraphControl : UserControl
    {
        private ProxyNetworkService _service;
        private bool _showHidden;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ActiveNetgraphControl()
        {
            
            InitializeComponent();
        }

        private void AddGraphToList(List<ListViewItem> items, NetGraph graph)
        {
            foreach (var pair in graph.Nodes)
            {
                BasePipelineNode node = pair.Value;
                
                if (_showHidden || !node.Hidden)
                {
                    NetGraphContainerNode container = node as NetGraphContainerNode;

                    if (node is NetGraphContainerNode)
                    {
                        AddGraphToList(items, ((NetGraphContainerNode)node).ContainedGraph);
                    }
                    else if (node is LayerSectionNode)
                    {
                        LayerSectionNode layerNode = (LayerSectionNode)node;

                        if (layerNode.IsMaster)
                        {
                            foreach (NetGraph subGraph in layerNode.Graphs)
                            {
                                AddGraphToList(items, subGraph);
                            }
                        }
                    }
                    else
                    {
                        ListViewItem item = new ListViewItem(node.ToString());
                        item.SubItems.Add(node.Enabled.ToString());
                        item.SubItems.Add(node.IsShutdown.ToString());
                        item.SubItems.Add(node.InputPacketCount.ToString());
                        item.SubItems.Add(node.OutputPacketCount.ToString());
                        item.SubItems.Add(node.ByteCount.ToString());
                        item.Tag = node;
                        items.Add(item);
                    }
                    
                }
            }
        }

        private void SetupGraph(NetGraph graph)
        {
            listViewNetGraph.SuspendLayout();
            timerUpdateGraph.Enabled = false;

            listViewNetGraph.Items.Clear();

            List<ListViewItem> items = new List<ListViewItem>();

            AddGraphToList(items, graph);

            listViewNetGraph.Items.AddRange(items.ToArray());
            timerUpdateGraph.Enabled = true;
            listViewNetGraph.ResumeLayout();

            metaEditorControl.Meta = graph.Meta;
            propertyBagViewerControl.UpdateProperties(graph.ConnectionProperties);
        }

        private void SetupService(ProxyNetworkService graph)
        {
            _service = graph;

            comboBoxNetGraph.Items.Clear();
            comboBoxNetGraph.SelectedItem = null;
            metaEditorControl.Meta = null;
            propertyBagViewerControl.UpdateProperties(null);
            listViewNetGraph.Items.Clear();
        }

        private void UpdateNetGraph()
        {
            listViewNetGraph.BeginUpdate();
            foreach (ListViewItem item in listViewNetGraph.Items)
            {
                BasePipelineNode node = (BasePipelineNode)item.Tag;

                item.SubItems[0].Text = node.ToString();
                item.SubItems[1].Text = node.Enabled.ToString();
                item.SubItems[2].Text = node.IsShutdown.ToString();
                item.SubItems[3].Text = node.InputPacketCount.ToString();
                item.SubItems[4].Text = node.OutputPacketCount.ToString();
                item.SubItems[5].Text = node.ByteCount.ToString();
            }
            listViewNetGraph.EndUpdate();
        }

        private void timerUpdateGraph_Tick(object sender, EventArgs e)
        {
            UpdateNetGraph();
        }

        /// <summary>
        /// Get or set the net graph
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProxyNetworkService Service
        {
            get
            {
                return _service;
            }

            set
            {
                SetupService(value);
            }
        }

        private void comboBoxNetGraph_DropDown(object sender, EventArgs e)
        {
            if (_service != null)
            {
                comboBoxNetGraph.Items.Clear();

                foreach (NetGraph graph in _service.Connections)
                {
                    comboBoxNetGraph.Items.Add(graph);
                }
            }
        }

        private void comboBoxNetGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_service != null)
            {
                if (comboBoxNetGraph.SelectedItem != null)
                {
                    SetupGraph((NetGraph)comboBoxNetGraph.SelectedItem);
                }
                else
                {
                    listViewNetGraph.Items.Clear();
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listViewNetGraph.SelectedItems.Count > 0)
            {
                pastePacketsToolStripMenuItem.Enabled = Clipboard.ContainsData(LogPacket.LogPacketArrayFormat);
                toggleEnableToolStripMenuItem.Enabled = true;
            }
            else
            {
                pastePacketsToolStripMenuItem.Enabled = false;
                toggleEnableToolStripMenuItem.Enabled = false;
            }
        }

        private void pastePacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewNetGraph.SelectedItems.Count > 0)
            {
                BasePipelineNode node = (BasePipelineNode)listViewNetGraph.SelectedItems[0].Tag;
                if (!node.IsShutdown)
                {
                    LogPacket[] packets = (LogPacket[])Clipboard.GetData(LogPacket.LogPacketArrayFormat);

                    foreach (LogPacket packet in packets)
                    {
                        node.Input(packet.Frame.CloneFrame());
                    }                     
                }
            }
        }

        private void toggleEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewNetGraph.SelectedItems.Count > 0)
            {
                BasePipelineNode node = (BasePipelineNode)listViewNetGraph.SelectedItems[0].Tag;

                node.Enabled = !node.Enabled;
            }
        }

        private void checkBoxShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            _showHidden = checkBoxShowHidden.Checked;

            if (comboBoxNetGraph.SelectedItem != null)
            {
                SetupGraph((NetGraph)comboBoxNetGraph.SelectedItem);
            }
        }

        private void comboBoxView_SelectedIndexChanged(object sender, EventArgs e)
        {            
            switch (comboBoxView.SelectedIndex)
            {
                case 0:
                    listViewNetGraph.Visible = true;
                    metaEditorControl.Visible = false;
                    propertyBagViewerControl.Visible = false;
                    break;
                case 1:
                    listViewNetGraph.Visible = false;
                    metaEditorControl.Visible = true;
                    propertyBagViewerControl.Visible = false;
                    break;
                case 2:
                    listViewNetGraph.Visible = false;
                    metaEditorControl.Visible = false;
                    propertyBagViewerControl.Visible = true;
                    break;
            }
        }

        private void ActiveNetgraphControl_Load(object sender, EventArgs e)
        {
            comboBoxView.SelectedIndex = 0;
        }

        private void shutdownNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewNetGraph.SelectedItems.Count > 0)
            {
                BasePipelineNode node = (BasePipelineNode)listViewNetGraph.SelectedItems[0].Tag;

                try
                {
                    node.Shutdown(null);
                }
                catch
                {

                }
            }
        }
    }
}
