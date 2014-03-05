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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Extension;
using CANAPE.Parser;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Controls.DocumentEditors
{
    /// <summary>
    /// TODO: Need to remove nodes which relate to the types, e.g. removing member entries from sequences which match enums or 
    /// other sequences, or removing parsers for sequences.
    /// </summary>
    public partial class ParserDocumentControl : UserControl
    {       
        ParserDocument _document;
        TreeNode _enumNode;
        TreeNode _sequenceNode;
        TreeNode _parserNode;
        TreeNode _scriptNode;
        Dictionary<Guid, TreeNode> _nodeMap;        

        const int FOLDER_OPEN_IMAGE = 4;

        public ParserDocumentControl(IDocumentObject document)
            : this(document, false)
        {
        }

        public ParserDocumentControl(IDocumentObject document, bool restricted)
        {
            _document = (ParserDocument)document;
            
            InitializeComponent();
            TreeNode root = new TreeNode(Properties.Resources.ParserDocumentControl_RootNodeName, 6, 6);

            if (!restricted)
            {
                _enumNode = root.Nodes.Add("Enums", Properties.Resources.ParserDocumentControl_EnumsNodeName, FOLDER_OPEN_IMAGE, FOLDER_OPEN_IMAGE);
            }

            _sequenceNode = root.Nodes.Add("Sequences", Properties.Resources.ParserDocumentControl_SequencesNodeName, FOLDER_OPEN_IMAGE, FOLDER_OPEN_IMAGE);
            _parserNode = root.Nodes.Add("Parsers", Properties.Resources.ParserDocumentControl_ParsersNodeName, FOLDER_OPEN_IMAGE, FOLDER_OPEN_IMAGE);
            _scriptNode = root.Nodes.Add("Scripts", Properties.Resources.ParserDocumentControl_ScriptsNodeName, FOLDER_OPEN_IMAGE, FOLDER_OPEN_IMAGE);

            treeViewTypes.Nodes.Add(root);
            sequenceEditorControl.Document = _document;
            if (restricted)
            {
                sequenceEditorControl.SetRestricted();
            }
            enumParserTypeEditorControl.Document = _document;
            _nodeMap = new Dictionary<Guid, TreeNode>();

            foreach (ParserType t in _document.Types)
            {
                AddTreeNode(t, false);
            }

            treeViewTypes.ExpandAll();

            if (restricted)
            {
                addEnumToolStripMenuItem.Visible = false;

            }

            ParserSerializerExtensionManager.Instance.AddToMenu(serializersToolStripMenuItem, ExtensionMenuClicked);
            if (!serializersToolStripMenuItem.HasDropDownItems)
            {
                serializersToolStripMenuItem.Visible = false;
            }
        }
     
        private void ExtensionMenuClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if ((item != null) && (item.Tag is Type))
            {
                Type t = (Type)item.Tag;

                IParserSerializer ext = (IParserSerializer)Activator.CreateInstance(t);

                ext.Serialize(_document.Types, this);
            }
        }

        private TreeNode AddTreeNode(ParserType type, bool editName)
        {
            if ((type is EnumParserType) && (_enumNode != null))
            {
                return AddTreeNode(_enumNode, type, editName);
            }
            else if (type is SequenceParserType)
            {
                return AddTreeNode(_sequenceNode, type, editName);
            }
            else if (type is StreamParserType)
            {
                return AddTreeNode(_parserNode, type, editName);
            }
            else if (type is ScriptParserType)
            {
                return AddTreeNode(_scriptNode, type, editName);
            }
            else
            {
                throw new ArgumentException(Properties.Resources.ParserDocumentControl_InvalidParserType, "type");
            }
        }

        private TreeNode AddTreeNode(TreeNode rootNode, ParserType type, bool editName)
        {
            TreeNode node = new TreeNode(type.Name, 2, 2);
            node.Tag = type;
            rootNode.Nodes.Add(node);

            if (editName)
            {
                node.EnsureVisible();
                node.BeginEdit();
            }

            _nodeMap.Add(type.Uuid, node);

            return node;
        }

        private void RemoveTreeNode(ParserType type)
        {
            if(_nodeMap.ContainsKey(type.Uuid))
            {
                TreeNode parentNode = _nodeMap[type.Uuid].Parent;
                _nodeMap[type.Uuid].Remove();
                _nodeMap.Remove(type.Uuid);
                _document.RemoveParserType(type);

                if (parentNode.Nodes.Count == 0)
                {
                    parentNode.ImageIndex = FOLDER_OPEN_IMAGE;
                    parentNode.SelectedImageIndex = FOLDER_OPEN_IMAGE;
                }
            }
        }

        private string GenerateName(string typeName)
        {
            string ret = null;            

            // Hopefully 10000 entires might be more than enough
            for (int i = _document.TypeCount; i < 10000; ++i)
            {
                string curr = String.Format("{0}_{1}", typeName, i);

                if (_document.FindType(curr) == null)
                {
                    ret = curr;
                    break;
                }
            }            

            return ret;
        }

        private void addSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SequenceParserType type = new SequenceParserType(GenerateName("Sequence"));
            type.DefaultEndian = Endian.BigEndian;

            _document.AddParserType(type);

            treeViewTypes.SelectedNode = AddTreeNode(_sequenceNode, type, true);            
        }

        private void treeViewTypes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewTypes.SelectedNode != null)
            {
                ParserType type = (ParserType)treeViewTypes.SelectedNode.Tag;

                if (type is SequenceParserType)
                {
                    sequenceEditorControl.Visible = true;
                    enumParserTypeEditorControl.Visible = false;
                    propertyGrid.Visible = false;
                    scriptParserTypeEditorControl.Visible = false;
                    sequenceEditorControl.CurrentType = (SequenceParserType)type;
                }
                else if (type is EnumParserType)
                {
                    enumParserTypeEditorControl.Visible = true;
                    sequenceEditorControl.Visible = false;
                    propertyGrid.Visible = false;
                    scriptParserTypeEditorControl.Visible = false;
                    enumParserTypeEditorControl.EnumType = (EnumParserType)type;
                }
                else if (type is StreamParserType)
                {
                    enumParserTypeEditorControl.Visible = false;
                    sequenceEditorControl.Visible = false;
                    propertyGrid.Visible = true;
                    scriptParserTypeEditorControl.Visible = false;
                    propertyGrid.SelectedObject = type;
                }
                else if (type is ScriptParserType)
                {
                    enumParserTypeEditorControl.Visible = false;
                    sequenceEditorControl.Visible = false;
                    propertyGrid.Visible = false;
                    scriptParserTypeEditorControl.Visible = true;
                    scriptParserTypeEditorControl.CurrentType = (ScriptParserType)type;
                }
                else
                {
                    sequenceEditorControl.Visible = false;
                    enumParserTypeEditorControl.Visible = false;
                    scriptParserTypeEditorControl.Visible = false;
                }
            }            
        }

        private void EditNodeLabel(TreeNode node)
        {
            removeToolStripMenuItem.ShortcutKeys = Keys.None;
            node.BeginEdit();
        }

        private void treeViewTypes_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            removeToolStripMenuItem.ShortcutKeys = Keys.Delete;
            if (e.Label != null)
            {
                if(e.Node.Tag is ParserType)
                {
                    if(!ParserUtils.IsValidIdentifier(e.Label))
                    {
                        MessageBox.Show(this, String.Format(CANAPE.Properties.Resources.ParserDocumentControl_InvalidIdentifier, e.Label),
                            CANAPE.Properties.Resources.ParserDocumentControl_InvalidIdentifierCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.CancelEdit = true;
                        EditNodeLabel(e.Node);
                    }                        
                    else if (_document.FindType(e.Label) != null)
                    {
                        MessageBox.Show(this, String.Format(CANAPE.Properties.Resources.ParserDocumentControl_TypeNameExists, e.Label),
                            CANAPE.Properties.Resources.ParserDocumentControl_TypeNameExistsCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.CancelEdit = true;
                        EditNodeLabel(e.Node);
                    }
                    else
                    {
                        ParserType type = (ParserType)e.Node.Tag;
                        type.Name = e.Label;
                    }
                }
            }
        }

        private void treeViewTypes_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                removeToolStripMenuItem.ShortcutKeys = Keys.Delete;
                e.CancelEdit = true;
            }
            else
            {
                removeToolStripMenuItem.ShortcutKeys = Keys.None;
            }
        }        

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewTypes.SelectedNode;

            if (selectedNode != null)
            {
                ParserType type = selectedNode.Tag as ParserType;

                if (type != null)
                {
                    if (MessageBox.Show(this, String.Format(CANAPE.Properties.Resources.ParserDocumentControl_RemoveType, type.Name),
                        String.Format(CANAPE.Properties.Resources.ParserDocumentControl_RemoveTypeCaption, type.Name),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {                       
                        RemoveTreeNode(type);
                        ParserType[] types = _document.Types.ToArray();

                        foreach (ParserType t in types)
                        {
                            if (t is SequenceParserType)
                            {
                                SequenceParserType seq = t as SequenceParserType;
                                List<MemberEntry> removeList = new List<MemberEntry>();

                                foreach (MemberEntry entry in seq.Members)
                                {
                                    if (entry.BaseType.Uuid == type.Uuid)
                                    {
                                        removeList.Add(entry);
                                    }
                                }

                                foreach (MemberEntry ent in removeList)
                                {
                                    seq.RemoveMember(ent);
                                }
                            }
                            else if (t is StreamParserType)
                            {
                                StreamParserType parser = t as StreamParserType;

                                if (parser.BaseType.Uuid == type.Uuid)
                                {
                                    RemoveTreeNode(parser);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((treeViewTypes.SelectedNode != null) && (treeViewTypes.SelectedNode.Tag is ParserType))
            {
                removeToolStripMenuItem.Enabled = true;

            }
            else
            {
                removeToolStripMenuItem.Enabled = false;
            }

            addParserFromSequenceToolStripMenuItem.DropDownItems.Clear();

            foreach (ParserType type in _document.Types)
            {
                SequenceParserType parser = type as SequenceParserType;
                if (parser != null && !parser.Private)
                {
                    ToolStripItem item = addParserFromSequenceToolStripMenuItem.DropDownItems.Add(type.Name);
                    item.Tag = type;
                    item.Click += new EventHandler(addParserFromSequenceToolStripMenuItem_Click);
                }
            }

            if(addParserFromSequenceToolStripMenuItem.DropDownItems.Count > 0)
            {
                addParserFromSequenceToolStripMenuItem.Enabled = true;
            }
            else
            {
                addParserFromSequenceToolStripMenuItem.Enabled = false;
            }
        }

        private void exportScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string code = _document.Script;

            if (code != null)
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = Properties.Resources.ParserDocumentControl_CSSourceFilter;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            File.WriteAllText(dlg.FileName, code);                            
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void addParserFromSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;

            if((item != null) && (item.Tag is SequenceParserType))
            {
                ParserType type = item.Tag as ParserType;
                type = new StreamParserType(GenerateName(String.Format("{0}_Parser", type.Name)), type);
                _document.AddParserType(type);

                treeViewTypes.SelectedNode = AddTreeNode(_parserNode, type, true); 
            }
        }

        private void treeViewTypes_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex == FOLDER_OPEN_IMAGE + 1)
            {
                e.Node.ImageIndex = FOLDER_OPEN_IMAGE;
                e.Node.SelectedImageIndex = FOLDER_OPEN_IMAGE;
            }
        }

        private void treeViewTypes_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex == FOLDER_OPEN_IMAGE)
            {
                e.Node.ImageIndex = FOLDER_OPEN_IMAGE + 1;
                e.Node.SelectedImageIndex = FOLDER_OPEN_IMAGE + 1;
            }
        }

        private void AddEnum(bool flags)
        {
            ParserType type = new EnumParserType(GenerateName(flags ? "Flags" : "Enum"), flags);
            _document.AddParserType(type);

            treeViewTypes.SelectedNode = AddTreeNode(_enumNode, type, true);
        }

        private void valueEnumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEnum(false);
        }

        private void flagsEnumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEnum(true);
        }

        private void validateParserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptError[] errors = ScriptUtils.Parse(_document.Container);

            if (errors.Length == 0)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.ParserDocumentControl_Validation,
                    CANAPE.Properties.Resources.ParserDocumentControl_ValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                StringBuilder builder = new StringBuilder();

                foreach (ScriptError error in errors)
                {
                    builder.AppendLine(error.Description);
                }

                MessageBox.Show(this, builder.ToString(), CANAPE.Properties.Resources.ParserDocumentControl_ValidationError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewTypes.SelectedNode;

            if ((node != null) && (node.Tag != null))
            {
                EditNodeLabel(node);
            }
        }

        private void treeViewTypes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeViewTypes.GetNodeAt(e.Location);

                if (node != null)
                {
                    treeViewTypes.SelectedNode = node;
                }
            }
        }

        private void createScriptDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string script = _document.Script;

            if (script != null)
            {
                ScriptDocument doc = CANAPEProject.CurrentProject.CreateDocument<ScriptDocument>("csharp");

                doc.Script = script;

                DocumentControl.Show(doc);
            }
        }
        
        private void copyScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string script = _document.Script;

            if (!String.IsNullOrWhiteSpace(script))
            {
                Clipboard.SetText(script);
            }
        }

        private void toolStripButtonOpenTest_Click(object sender, EventArgs e)
        {
            ScriptTestDocument testDocument = null;

            foreach (ScriptTestDocument doc in CANAPEProject.CurrentProject.GetDocumentsByType(typeof(ScriptTestDocument)))
            {
                if (doc.Document == _document)
                {
                    testDocument = doc;
                    break;
                }
            }

            if (testDocument == null)
            {
                testDocument = new ScriptTestDocument(_document);
                testDocument.Name = String.Format("Test {0}", _document.Name);

                CANAPEProject.CurrentProject.AddDocument(testDocument, false);
            }

            DocumentControl.Show(testDocument);
        }

        private void addScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            ScriptParserType type = new ScriptParserType(GenerateName("Script"), "python", 
                Properties.Resources.DefaultPythonParserScript);

            _document.AddParserType(type);

            treeViewTypes.SelectedNode = AddTreeNode(_scriptNode, type, true);
        }

        private void duplicateTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeViewTypes.SelectedNode != null)
                {
                    ParserType type = treeViewTypes.SelectedNode.Tag as ParserType;

                    if (type != null)
                    {
                        ParserType newType = type.Copy();

                        newType.Name = "Copy_" + newType.Name;

                        _document.AddParserType(newType);

                        treeViewTypes.SelectedNode = AddTreeNode(newType, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
