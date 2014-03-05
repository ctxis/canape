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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CANAPE.DataFrames;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net;
using CANAPE.Forms;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class PacketLogDiffDocumentControl : UserControl
    {
        private const string PACKETLOG_BASE_CONFIG = "PacketLogConfig";

        PacketLogDiffDocument _document;
        
        sealed class DataNodeDiff : TreeNode
        {            
            public DataNode Left { get; private set; }
            public DataNode Right { get; private set; }
            public DiffRange.DiffType Mode { get; private set; }
            
            public DataNodeDiff(DataNode left, DataNode right) 
            {
                Left = left;
                Right = right;

                SetupNode();
            }

            public bool IsBasic
            {
                get
                {
                    Guid binaryClass = new Guid(DataNodeClasses.BINARY_NODE_CLASS);
                    if (Left != null)
                    {
                        if ((Left.Class == binaryClass) || (Left is DataValue))
                        {
                            return true;
                        }
                    }

                    if (Right != null)
                    {
                        if ((Right.Class == binaryClass) || (Right is DataValue))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            private string FormatNode(DataNode node)
            {
                string ret;

                if (IsBasic)
                {                    
                    if (node is DataValue)
                    {
                        ret = String.Format("{0}={1}", node.Name, node.ToString());
                    }
                    else
                    {
                        ret = new ByteArrayDataValue("root", node.ToArray()).ToString();
                    }
                }
                else
                {
                    string value = node.ToString();

                    if (value == node.Name)
                    {
                        ret = value;
                    }
                    else
                    {
                        ret = String.Format("{0}={1}", node.Name, value);
                    }
                }

                if (ret.Length > 32)
                {
                    return ret.Substring(0, 32);
                }
                else
                {
                    return ret;
                }
            }

            private void SetupNode()
            {             
                if (Left == null)
                {
                    Text = String.Format(Properties.Resources.DiffPacketsControl_NodeAdded, FormatNode(Right));
                    
                    Mode = DiffRange.DiffType.Added;
                }
                else if (Right == null)
                {
                    Text = String.Format(Properties.Resources.DiffPacketsControl_NodeDeleted, FormatNode(Left));
                    Mode = DiffRange.DiffType.Deleted;
                }
                else
                {
                    Text = String.Format(Properties.Resources.DiffPacketsControl_NodeModified, FormatNode(Left), FormatNode(Right));
                    Mode = DiffRange.DiffType.Modified;

                    if (!IsBasic)
                    {
                        // This is just so we can handle expanding nodes on demand
                        Nodes.Add(new TreeNode("dummy"));
                    }
                }

                BackColor = DiffRange.GetColor(Mode);                
            }
        }

        LogPacketCollection _left;
        LogPacketCollection _right;

        private PacketLogControlConfig CreateConfig(string name)
        {
            PacketLogControlConfig config = _document.GetProperty(PACKETLOG_BASE_CONFIG + name) as PacketLogControlConfig;

            if (config == null)
            {
                config = new PacketLogControlConfig();
                config.Columns.Clear();
                config.Columns.Add(new TagPacketLogColumn());
                config.Columns.Add(new DataPacketLogColumn());
            }

            return config;
        }

        public PacketLogDiffDocumentControl(PacketLogDiffDocument document)
        {
            
            InitializeComponent();

            _document = document;
            _left = document.Left;
            _right = document.Right;

            packetLogControlLeft.SetPackets(_left);
            packetLogControlRight.SetPackets(_right);
            packetLogControlLeft.Config = CreateConfig(".Left");
            packetLogControlRight.Config = CreateConfig(".Right");
        }

        public PacketLogDiffDocumentControl() 
            : this(new PacketLogDiffDocument())
        {
        }

        private DataNode[] GetPackets(IEnumerable<LogPacket> packets, bool toBytes)
        {
            lock (packets)
            {
                if (toBytes)
                {
                    return packets.Select(p => p.Frame.CloneToBasic().Root).ToArray();
                }
                else
                {
                    return packets.Select(p => p.Frame.CloneFrame().Root).ToArray();
                }
            }
        }

        private void AddNodes(TreeNodeCollection col, DataNode left, DataNode right)
        {
            col.Add(new DataNodeDiff(left, right));
        }

        private void DiffNodesAndAdd(TreeNodeCollection col, DataNode[] left, DataNode[] right)
        {
            treeViewOutput.SuspendLayout();
            col.Clear();

            if ((left.Length == 0) && (right.Length == 0))
            {
                MessageBox.Show(this, Properties.Resources.DiffPacketsControl_NoDifferencesFound, Properties.Resources.DiffPacketsControl_NoDifferencesFoundCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (left.Length == 0)
            {
                foreach (DataNode packet in right)
                {
                    AddNodes(col, null, packet);
                }
            }
            else if (right.Length == 0)
            {
                foreach (DataNode packet in left)
                {
                    AddNodes(col, packet, null);
                }
            }
            else
            {
                DiffRange[] ranges = DiffRange.BuildDifferences(left, right, new DataNodeEqualityComparer()).ToArray();

                if (ranges.Length > 0)
                {
                    foreach (DiffRange range in ranges)
                    {
                        if (range.LeftLength == range.RightLength)
                        {
                            for (int i = 0; i < range.LeftLength; ++i)
                            {
                                AddNodes(col, left[i + range.LeftStartPos], right[i + range.RightStartPos]);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < range.LeftLength; ++i)
                            {
                                AddNodes(col, left[i + range.LeftStartPos], null);
                            }

                            for (int i = 0; i < range.RightLength; ++i)
                            {
                                AddNodes(col, null, right[i + range.RightStartPos]);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, Properties.Resources.DiffPacketsControl_NoDifferencesFound, Properties.Resources.DiffPacketsControl_NoDifferencesFoundCaption,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            treeViewOutput.ResumeLayout();
        }

        private void btnDiff_Click(object sender, EventArgs e)
        {
            DataNode[] left = GetPackets(_left, checkBoxByteCompare.Checked);
            DataNode[] right = GetPackets(_right, checkBoxByteCompare.Checked);

            DiffNodesAndAdd(treeViewOutput.Nodes, left, right);
        }

        private void packetLogControlLeft_ConfigChanged(object sender, EventArgs e)
        {
            _document.SetProperty(PACKETLOG_BASE_CONFIG + ".Left", packetLogControlLeft.Config);
        }

        private void packetLogControlRight_ConfigChanged(object sender, EventArgs e)
        {
            _document.SetProperty(PACKETLOG_BASE_CONFIG + ".Right", packetLogControlRight.Config);
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewBinaryDiffToolStripMenuItem.Enabled = false;
            expandDiffToolStripMenuItem.Enabled = false;

            if (treeViewOutput.SelectedNode != null)
            {
                DataNodeDiff diff = treeViewOutput.SelectedNode as DataNodeDiff;

                if (diff != null)
                {
                    if (diff.Mode == DiffRange.DiffType.Modified)
                    {
                        viewBinaryDiffToolStripMenuItem.Enabled = true;
                    }
                }
                
                expandDiffToolStripMenuItem.Enabled = true;
            }            
        }

        private void treeViewOutput_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            DataNodeDiff node = e.Node as DataNodeDiff;

            if ((node != null) && !node.IsBasic && (node.Mode == DiffRange.DiffType.Modified))
            {                
                DataKey leftKey = (DataKey)node.Left;
                DataKey rightKey = (DataKey)node.Right;

                DiffNodesAndAdd(node.Nodes, leftKey.SubNodes.ToArray(), rightKey.SubNodes.ToArray());
            }
        }

        private void viewBinaryDiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataNodeDiff node = treeViewOutput.SelectedNode as DataNodeDiff;

            if (node != null)
            {                
                if (node.Mode == DiffRange.DiffType.Modified)
                {
                    BinaryFrameDiffForm frm = new BinaryFrameDiffForm(node.Left, node.Right);

                    frm.Show(this);
                }
            }
        }

        private void treeViewOutput_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeViewOutput.GetNodeAt(e.Location);

                if (node != null)
                {
                    treeViewOutput.SelectedNode = node;
                }
            }
        }

        private void expandDiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewOutput.SelectedNode != null)
            {
                treeViewOutput.SelectedNode.ExpandAll();
            }
        }
    }
}
