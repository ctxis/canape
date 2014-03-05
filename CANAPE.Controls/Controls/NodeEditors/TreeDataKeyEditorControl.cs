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
using System.IO;
using System.Text;
using System.Windows.Forms;
using CANAPE.Controls.DocumentEditors;
using CANAPE.DataFrames;
using CANAPE.DataFrames.Filters;
using CANAPE.Forms;
using CANAPE.NodeFactories;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Controls.NodeEditors
{
    /// <summary>
    /// Tree DataKey editor, has no class because it only applies to DataKeys in specific circumstances
    /// </summary>    
    public partial class TreeDataKeyEditorControl : UserControl, IDataNodeEditor
    {
        DataKey _root;
        DataNode _selected;
        Color _color;
        bool _readOnly;
        Dictionary<DataFrame, DataNode> _selectedNodes;

        private class FrameReferenceEqualityComparer : IEqualityComparer<DataFrame>
        {
            public bool Equals(DataFrame x, DataFrame y)
            {
                return Object.ReferenceEquals(x, y);
            }

            public int GetHashCode(DataFrame obj)
            {
                return obj.GetHashCode();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TreeDataKeyEditorControl()
        {
            
            InitializeComponent();
            _selectedNodes = new Dictionary<DataFrame, DataNode>(new FrameReferenceEqualityComparer());
        }        

        private static string LimitName(string name)
        {
            if (name.Length > 64)
            {
                return name.Substring(0, 64) + "...";
            }
            else
            {
                return name;
            }
        }

        private TreeNode DataValueToTreeNode(DataValue value)
        {
            int image = value.Readonly ? 3 : 2;            

            TreeNode valueNode = new TreeNode(LimitName(value.Name), image, image);
            valueNode.Tag = value;

            return valueNode;
        }

        private int GetKeyImageIndex(DataKey key)
        {
            int image = key.Readonly ? 1 : 0;
            Type t = key.GetType().BaseType;

            if (key is IScriptedNode)
            {
                image += 4;
            }

            return image;
        }

        private TreeNode AddBranch(TreeNode root, DataKey node, DataNode selectedNode, ref TreeNode selectedTreeNode)
        {
            foreach (DataNode n in node.SubNodes)
            {
                if (n is DataKey)
                {
                    int image = GetKeyImageIndex((DataKey)n);
                    
                    TreeNode nextRoot = new TreeNode(LimitName(n.Name), image, image);
                    nextRoot.Tag = n;

                    AddBranch(nextRoot, (DataKey)n, selectedNode, ref selectedTreeNode);
                    root.Nodes.Add(nextRoot);

                    if (selectedNode == n)
                    {
                        selectedTreeNode = nextRoot;
                    }
                }
                else if (n is DataValue)
                {
                    int image = n.Readonly ? 3 : 2;
                    DataValue value = n as DataValue;
                    TreeNode valueNode = root.Nodes.Add(n.Name, LimitName(n.Name), image, image);
                    valueNode.Tag = n;

                    if (selectedNode == n)
                    {
                        selectedTreeNode = valueNode;
                    }
                }
                else
                {
                    throw new ArgumentException(String.Format("Invalid Node Type {0}", n.GetType()));
                }
            }

            return root;
        }

        private void UpdateTree()
        {
            TreeNode selected = null;

            treeViewNodes.Nodes.Clear();
            treeViewNodes.BeginUpdate();
            treeViewNodes.SuspendLayout();
            treeViewNodes.BackColor = _color;

            if (_root != null)
            {
                TreeNode root = new TreeNode("/", GetKeyImageIndex(_root), GetKeyImageIndex(_root));
                root.Tag = _root;

                root = AddBranch(root, _root, _selected, ref selected);

                treeViewNodes.Nodes.Add(root);                

                treeViewNodes.SelectedNode = selected != null ? selected : root;

                if (treeViewNodes.SelectedNode != null)
                {
                    treeViewNodes.SelectedNode.EnsureVisible();
                    treeViewNodes.SelectedNode.Expand();            
                }
            }
            
            if (_readOnly)
            {
                treeViewNodes.LabelEdit = false;
            }

            treeViewNodes.ResumeLayout();
            treeViewNodes.EndUpdate();                        
        }

        private void DataFrameViewerControl_Load(object sender, EventArgs e)
        {
            UpdateTree();
        }

        private void treeViewNodes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                _selected = (DataNode)e.Node.Tag;

                if ((_selected != null) && (_selected.Frame != null))
                {
                    _selectedNodes[_selected.Frame] = _selected;
                }

                dataNodeEditorControl.SetNode(_selected, null, _color, _readOnly);
            }
        }

        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewNodes.SelectedNode;

            if (selectedNode != null)
            {
                DataNode node = (DataNode)selectedNode.Tag;

                Clipboard.SetText(node.Path);
            }
        }

        private void ReplaceNodeWithValue(TreeNode selectedNode, DataValue value)
        {
            TreeNode parentNode = selectedNode.Parent;
            DataNode node = (DataNode)selectedNode.Tag;
            node.ReplaceNode(value);

            int idx = parentNode.Nodes.IndexOf(selectedNode);
            parentNode.Nodes.RemoveAt(idx);
            TreeNode newNode = DataValueToTreeNode(value);
            parentNode.Nodes.Insert(idx, newNode);
            treeViewNodes.SelectedNode = newNode;

            treeViewNodes.Invalidate();
        }

        private void convertToBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewNodes.SelectedNode;

            if (selectedNode != null)
            {                
                DataNode node = (DataNode)selectedNode.Tag;

                try
                {
                    ReplaceNodeWithValue(selectedNode, new ByteArrayDataValue(node.Name, node.ToArray()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SetNode(DataNode node, DataNode selected, Color color, bool readOnly)
        {            
            _root = node as DataKey;

            if ((selected != null) && (_selectedNodes.ContainsKey(selected.Frame)))
            {
                _selected = _selectedNodes[selected.Frame];
            }
            else if ((_selected != null) && (_root != null))
            {
                string path = _selected.Path;
                _selected = _root.SelectSingleNode(path);
            }
            else
            {
                _selected = selected;
            }
            
            _readOnly = readOnly;
            _color = color;
            UpdateTree();

            convertToToolStripMenuItem.Visible = !_readOnly;            
            importFromFileToolStripMenuItem.Visible = !_readOnly;
            parseWithToolStripMenuItem.Visible = !_readOnly;
            deleteNodeToolStripMenuItem.Visible = !_readOnly;
        }

        private void importFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeViewNodes.SelectedNode;

                if (selectedNode != null)
                {
                    DataNode node = (DataNode)selectedNode.Tag;

                    using (OpenFileDialog dlg = new OpenFileDialog())
                    {
                        dlg.Filter = "All Files (*.*)|*.*";

                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            ReplaceNodeWithValue(selectedNode, new ByteArrayDataValue(node.Name, File.ReadAllBytes(dlg.FileName)));
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, String.Format("Error loading file {0}", ex.Message), CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = treeViewNodes.SelectedNode;

                if (selectedNode != null)
                {
                    using (SaveFileDialog dlg = new SaveFileDialog())
                    {
                        dlg.Filter = "All Files (*.*)|*.*";

                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            File.WriteAllBytes(dlg.FileName, ((DataNode)selectedNode.Tag).ToArray());
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, String.Format("Error saving file {0}", ex.Message), CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyAsFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewNodes.SelectedNode;

            if (selectedNode != null)
            {
                NetGraphDocumentControl.CopyAsFilter((DataNode)selectedNode.Tag);                
            }
        }

        private void ConvertNodeToString(Encoding encoding)
        {
            TreeNode selectedNode = treeViewNodes.SelectedNode;

            if (selectedNode != null)
            {
                DataNode node = (DataNode)selectedNode.Tag;

                try
                {
                    ReplaceNodeWithValue(selectedNode, new StringDataValue(node.Name, encoding.GetString(node.ToArray()), encoding));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void convertToBinaryStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertNodeToString(new BinaryEncoding());
        }

        private void convertToUTF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertNodeToString(Encoding.UTF8);
        }

        private void convertToUTF16LEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertNodeToString(Encoding.Unicode);
        }

        private void convertToUTF16BEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertNodeToString(Encoding.BigEndianUnicode);
        }

        private void convertToUTF32LEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertNodeToString(Encoding.UTF32);
        }

        private void convertToUTF32BEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertNodeToString(new UTF32Encoding(true, false, false));
        }

        private void convertToUTF7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertNodeToString(Encoding.UTF7);
        }

        private static void FlattenTree(List<TreeNode> nodes, TreeNode root)
        {
            nodes.Add(root);
            foreach (TreeNode node in root.Nodes)
            {
                if (node.Tag is DataKey)
                {
                    FlattenTree(nodes, node);
                }
                else
                {
                    nodes.Add(node);
                }
            }
        }

        private static bool MatchDataValue(DataValue value, byte[] searchBytes, string searchString)
        {
            bool ret = false;
            if (value != null)
            {
                if (value.Value is byte[])
                {
                    byte[] data = value.Value as byte[];

                    for (int i = 0; i < (data.Length - searchBytes.Length); ++i)
                    {
                        if (GeneralUtils.MatchArray(data, i, searchBytes))
                        {
                            ret = true;
                            break;
                        }
                    }
                }
                else
                {
                    string str = value.Value.ToString();

                    if (str.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        private TreeNode DoFind(TreeNode selectedNode, InlineSearchControl.SearchEventArgs e, bool next)
        {
            byte[] searchBytes = e.Data != null ? e.Data : new BinaryEncoding().GetBytes(e.Text);
            string searchString = e.Text != null ? e.Text : new BinaryEncoding().GetString(e.Data);
            List<TreeNode> nodes = new List<TreeNode>();
            TreeNode foundNode = null;

            FlattenTree(nodes, treeViewNodes.TopNode);

            if(nodes.Count > 0)
            {
                int startIndex = nodes.IndexOf(selectedNode);

                if(startIndex < 0)
                {
                    startIndex = 0;
                }

                if(next)
                {
                    for (int i = startIndex + 1; i < nodes.Count; ++i)
                    {
                        if (MatchDataValue(nodes[i].Tag as DataValue, searchBytes, searchString))
                        {
                            foundNode = nodes[i];
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = startIndex - 1; i >= 0; --i)
                    {
                        if (MatchDataValue(nodes[i].Tag as DataValue, searchBytes, searchString))
                        {
                            foundNode = nodes[i];
                            break;
                        }
                    }
                }
            }

            return foundNode;
        }

        private void StartFind(InlineSearchControl.SearchEventArgs e, bool next)
        {
            TreeNode selectedNode = treeViewNodes.SelectedNode == null ? treeViewNodes.TopNode : treeViewNodes.SelectedNode;

            if (selectedNode != null)
            {
                TreeNode node = DoFind(selectedNode, e, next);

                if (node != null)
                {
                    treeViewNodes.SelectedNode = node;
                }
                else
                {
                    MessageBox.Show(this, CANAPE.Properties.Resources.TreeDataKeyEditorControl_NoMatch,
                        CANAPE.Properties.Resources.TreeDataKeyEditorControl_NoMatchCaption,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void inlineSearchControl_SearchNext(object sender, InlineSearchControl.SearchEventArgs e)
        {
            StartFind(e, true);
        }

        private void inlineSearchControl_SearchPrev(object sender, InlineSearchControl.SearchEventArgs e)
        {
            StartFind(e, false);
        }

        private void RunScript(TreeNode selectedNode, ScriptContainer container, string classname)
        {
            DataNode dataNode = selectedNode.Tag as DataNode;
          
            try
            {
                DataNode newNode = ParseWithUtils.ParseNode(dataNode, container, classname);

                if (newNode != null)
                {
                    dataNode.ReplaceNode(newNode);

                    if (newNode is DataKey)
                    {
                        selectedNode.Nodes.Clear();
                        selectedNode.Tag = newNode;
                        selectedNode.SelectedImageIndex = newNode.Readonly ? 1 : 0;
                        selectedNode.ImageIndex = selectedNode.SelectedImageIndex;
                        TreeNode newSelectedNode = null;
                        AddBranch(selectedNode, (DataKey)newNode, null, ref newSelectedNode);
                    }
                    else if (newNode is DataValue)
                    {
                        ReplaceNodeWithValue(selectedNode, (DataValue)newNode);
                    }

                    treeViewNodes.SelectedNode = null;
                    treeViewNodes.SelectedNode = selectedNode;
                    selectedNode.Expand();
                }
                else
                {
                    MessageBox.Show(this, CANAPE.Properties.Resources.TreeDataKeyEditorControl_NoParseResults, CANAPE.Properties.Resources.TreeDataKeyEditorControl_NoParseResultsCaption, 
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.TreeDataKeyEditorControl_ParseError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void libraryParserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeViewNodes.SelectedNode != null)
            {
                using (SelectLibraryNodeForm frm = new SelectLibraryNodeForm())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        ScriptContainer container = new ScriptContainer("assembly", Guid.NewGuid(),
                            frm.Node.Type.Assembly.FullName, false);

                        RunScript(treeViewNodes.SelectedNode, container, frm.Node.Type.FullName);
                    }
                }
            }
        }

        private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeViewNodes.SelectedNode != null)
            {
                using (SelectScriptForm frm = new SelectScriptForm())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        RunScript(treeViewNodes.SelectedNode, frm.Document.Container, frm.ClassName);
                    }
                }
            }
        }

        private void expandNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewNodes.SelectedNode != null)
            {
                treeViewNodes.SelectedNode.ExpandAll();                
            }            
        }

        private void collapseNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewNodes.SelectedNode != null)
            {
                treeViewNodes.SelectedNode.Collapse(false);
            }
        }

        private void EnableOnSelected(bool selected)
        {
            expandNodeToolStripMenuItem.Enabled = selected;
            collapseNodeToolStripMenuItem.Enabled = selected;
            copyAsFilterToolStripMenuItem.Enabled = selected;
            copyPathToolStripMenuItem.Enabled = selected;
            parseWithToolStripMenuItem.Enabled = selected;
            importFromFileToolStripMenuItem.Enabled = selected;
            exportToFileToolStripMenuItem.Enabled = selected;
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            EnableOnSelected(treeViewNodes.SelectedNode != null);                         
        }

        private void treeViewNodes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeViewNodes.GetNodeAt(e.Location);

                if (node != null)
                {                    
                    treeViewNodes.SelectedNode = node;
                }
            }
        }

        private void deleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewNodes.SelectedNode != null)
            {
                DataNode node = treeViewNodes.SelectedNode.Tag as DataNode;
                if ((node != null) && (node != _root))
                {
                    node.RemoveNode();
                    treeViewNodes.SelectedNode.Remove();
                }
            }
        }

        private void cloneNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewNodes.SelectedNode != null)
            {
                DataNode node = treeViewNodes.SelectedNode.Tag as DataNode;
                if ((node != null) && (node != _root))
                {
                    DataNode newNode = node.CloneNode();
                    node.Parent.AddSubNode(newNode);
                    UpdateTree();
                }
            }
        }

        private void renameNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewNodes.SelectedNode != null)
            {
                treeViewNodes.SelectedNode.BeginEdit();
            }
        }

        private void treeViewNodes_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node != null)
            {
                DataNode node = e.Node.Tag as DataNode;

                if (node != null)
                {
                    node.Name = e.Label;
                }
            }
        }
    }
}
