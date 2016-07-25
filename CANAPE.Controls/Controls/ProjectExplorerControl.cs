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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Extension;
using CANAPE.Forms;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    /// <summary>
    /// Control to manage a project
    /// </summary>
    public partial class ProjectExplorerControl : UserControl
    {
        const string DocumentDataType = "IDocumentObject";     
                
        private Dictionary<Guid, TreeNode> _documentToNode;
        private Dictionary<Type, IDocumentFactory> _documentFactories;

        const int CLOSED_FOLDER_IMAGE_INDEX = 2;
        const int OPEN_FOLDER_IMAGE_INDEX = 3;

        TreeNode _rootNode;
        
        public class ProjectEventArgs : EventArgs
        {
            public IDocumentObject Document
            {
                get;
                private set;
            }

            public ProjectEventArgs(IDocumentObject document)
            {
                Document = document;
            }
        }

        private IDocumentFactory GetFactory(Type factoryType)
        {
            if (!_documentFactories.ContainsKey(factoryType))
            {
                _documentFactories[factoryType] = (IDocumentFactory)Activator.CreateInstance(factoryType);
            }

            return _documentFactories[factoryType];
        }

        public ProjectExplorerControl()
        {            
            InitializeComponent();

            InitializeTree();

            _documentFactories = new Dictionary<Type, IDocumentFactory>();

            CANAPEProject.DocumentAdded += new EventHandler<DocumentEventArgs>(CANAPEProject_DocumentAdded);
            CANAPEProject.DocumentDeleted += new EventHandler<DocumentEventArgs>(CANAPEProject_DocumentDeleted);
            CANAPEProject.DocumentRenamed += new EventHandler<DocumentEventArgs>(CANAPEProject_DocumentRenamed);
            CANAPEProject.ProjectLoaded += new EventHandler(CANAPEProject_ProjectLoaded);

            foreach (IDocumentObject doc in CANAPEProject.CurrentProject.Documents)
            {
                AddEntryToTree(doc);
            }

            if (DocumentFactoryManager.Instance.Count > 0)
            {
                DocumentFactoryManager.Instance.AddToMenu(addToolStripMenuItem, extensionItem_Click);
            }
        }

        private static Type GetDocumentTestType(IDocumentObject document)
        {
            object[] attrs = document.GetType().GetCustomAttributes(typeof(TestDocumentTypeAttribute), true);

            if (attrs.Length > 0)
            {
                return ((TestDocumentTypeAttribute)attrs[0]).DocumentType;
            }
            else
            {
                return null;
            }
        }

        private static bool HasDocumentTestType(IDocumentObject document)
        {
            if (document != null)
            {
                return GetDocumentTestType(document) != null;
            }
            else
            {
                return false;
            }
        }

        private Icon GetIconForDocument(IDocumentObject document)
        {
            Type docType = document.GetType();

            // Query factories for an Icon first
            foreach (var ext in DocumentFactoryManager.Instance.GetExtensions())
            {                
                IDocumentFactory factory = GetFactory(ext.ExtensionType);
                Icon ret = factory.GetIconForDocument(document);
                if (ret != null)
                {
                    return ret;
                }
            }

            // No extension icon, go for defaults
            if (document is NetGraphDocument)
            {                
                return Properties.Resources.NetGraphIcon;
            }
            else if (document is ParserDocument)
            {
                return Properties.Resources.ParserComponent;                    
            }
            else if (document is AssemblyDocument)
            {
                return Properties.Resources.Assembly_ProjectEntry;
            }
            else if (document is ScriptDocument)
            {
                ScriptDocument doc = (ScriptDocument)document;

                switch (doc.Container.Engine)
                {
                    case "csharp": return Properties.Resources.CSScriptIcon;
                    case "python": return Properties.Resources.PythonScriptIcon;
                    case "visualbasic": return Properties.Resources.VBScriptIcon;
                    case "fsharp": return Properties.Resources.CSScriptIcon;
                }

                return Properties.Resources.ScriptIcon;
            }
            else if (document is NetServiceDocument)
            {
                return Properties.Resources.Network_Map;
            }
            else if (document is TextDocument)
            {
                return Properties.Resources.textdoc;
            }
            else if (document is TestDocument)
            {
                return Properties.Resources.otheroptions;
            }

            return Properties.Resources.UtilityText;
        }

        private void BuildDefaultProxyNetgraph(NetGraphDocument document)
        {
            List<BaseNodeConfig> nodes = new List<BaseNodeConfig>();            

            ServerEndpointConfig server = new ServerEndpointConfig();
            server.Label = "SERVER";
            server.X = 80;
            server.Y = 150;
            nodes.Add(server);

            ClientEndpointConfig client = new ClientEndpointConfig();
            client.Label = "CLIENT";
            client.X = 280;
            client.Y = 150;
            nodes.Add(client);

            LogPacketConfig logout = new LogPacketConfig();
            logout.Label = "LOGOUT";
            logout.X = 180;
            logout.Y = 60;
            logout.Color = ColorValue.Pink;
            logout.Tag = "Out";
            nodes.Add(logout);

            LogPacketConfig login = new LogPacketConfig();
            login.Label = "LOGIN";
            login.X = 180;
            login.Y = 240;
            login.Color = ColorValue.Powderblue;
            login.Tag = "In";
            nodes.Add(login);

            List<LineConfig> lines = new List<LineConfig>();
            lines.Add(new LineConfig(server, logout, false, null, false));
            lines.Add(new LineConfig(logout, client, false, null, false));
            lines.Add(new LineConfig(client, login, false, null, false));
            lines.Add(new LineConfig(login, server, false, null, false));


            document.UpdateGraph(nodes.ToArray(), lines.ToArray());
        }

        private void InitializeTree()
        {
            treeViewProject.Nodes.Clear();
            _rootNode = treeViewProject.Nodes.Add("project", CANAPE.Properties.Resources.ProjectExplorer_RootNodeName, 0, 0);
            _rootNode.ContextMenuStrip = contextMenuStrip;

            _rootNode.Nodes.Add("scripts", CANAPE.Properties.Resources.ProjectExplorer_ScriptsNodeName, 2, 2);
            _rootNode.Nodes.Add("services", CANAPE.Properties.Resources.ProjectExplorer_ServicesNodeName, 2, 2);
            _rootNode.Nodes.Add("graphs", CANAPE.Properties.Resources.ProjectExplorer_GraphsNodeName, 2, 2);
            _rootNode.Nodes.Add("data", CANAPE.Properties.Resources.ProjectExplorer_DataNodeName, 2, 2);
            _rootNode.Nodes.Add("tests", CANAPE.Properties.Resources.ProjectExplorer_TestsNodeName, 2, 2);

            _documentToNode = new Dictionary<Guid, TreeNode>();
        }

        private static string GetDocumentIconName(IDocumentObject document)
        {
            string iconName = document.DefaultName;

            if ((document is ScriptDocument) && !(document is ParserDocument))
            {
                ScriptDocument doc = (ScriptDocument)document;
                iconName = doc.Container.Engine;
            }

            return iconName;
        }

        public void SetIconForDocument(IDocumentObject document, string iconName, Icon icon)
        {
            if (_documentToNode.ContainsKey(document.Uuid))
            {
                TreeNode node = _documentToNode[document.Uuid];

                if (iconName == null)
                {
                    iconName = GetDocumentIconName(document);
                }

                if (!imageListIcons.Images.ContainsKey(iconName))
                {
                    imageListIcons.Images.Add(iconName, icon);
                }

                node.ImageKey = iconName;
                node.SelectedImageKey = iconName;
            }
        }

        public void Clear()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(Clear));
            }
            else
            {
                InitializeTree();
            }
        }

        protected void OnDisplayDocument(IDocumentObject document)
        {
            DocumentControl.Show(document);
        }

        protected void OnDocumentDeleted(IDocumentObject document)
        {
            DocumentControl.Close(document);
        }

        protected void OnDocumentRenamed(IDocumentObject document)
        {
            DocumentControl.Rename(document);   
        }

        private TreeNode GetFolderNodeForDocument(IDocumentObject document)
        {
            if ((document is ScriptDocument) || (document is AssemblyDocument))
            {
                return _rootNode.Nodes["scripts"];
            }
            else if (document is NetServiceDocument)
            {
                return _rootNode.Nodes["services"];
            }
            else if (document is NetGraphDocument)
            {
                return _rootNode.Nodes["graphs"];
            }
            else if (document is TestDocument)
            {
                return _rootNode.Nodes["tests"];
            }
            else
            {
                return _rootNode.Nodes["data"];
            }
        }        

        private void AddEntryToTree(IDocumentObject document)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<IDocumentObject>(AddEntryToTree), document);
            }
            else
            {
                TreeNode node = GetFolderNodeForDocument(document).Nodes.Add(document.Name);
                Icon icon = GetIconForDocument(document);
                if (icon != null)
                {
                    string iconName = GetDocumentIconName(document);

                    if (!imageListIcons.Images.ContainsKey(iconName))
                    {
                        imageListIcons.Images.Add(iconName, icon);
                    }
                    node.ImageKey = iconName;
                    node.SelectedImageKey = iconName;
                }
                else
                {
                    node.SelectedImageIndex = 1;
                    node.ImageIndex = 1;
                }

                node.Tag = document;
                _documentToNode[document.Uuid] = node;
                node.EnsureVisible();
            }
        }

        private void treeViewProject_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != treeViewProject.TopNode)
            {
                IDocumentObject ent = e.Node.Tag as IDocumentObject;

                if (ent != null)
                {
                    OnDisplayDocument(ent);
                }
            }
        }

        private void treeViewProject_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            IDocumentObject entry = e.Node.Tag as IDocumentObject;

            deleteToolStripMenuItem.ShortcutKeys = Keys.Delete;

            if (entry != null)
            {
                bool ret = CANAPEProject.CurrentProject.RenameDocument(entry, e.Label);
                e.CancelEdit = !ret;

                OnDocumentRenamed(entry);   
            }
        }

        void CANAPEProject_ProjectLoaded(object sender, EventArgs e)
        {
            Clear();
            foreach (IDocumentObject doc in CANAPEProject.CurrentProject.Documents)
            {
                AddEntryToTree(doc);
            }
        }

        void CANAPEProject_DocumentRenamed(object sender, DocumentEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DocumentEventArgs>(CANAPEProject_DocumentRenamed), sender, e);
            }
            else
            {
                if(_documentToNode.ContainsKey(e.Document.Uuid))
                {
                    _documentToNode[e.Document.Uuid].Text = e.Document.Name;
                    OnDocumentRenamed(e.Document);                    
                }
            }
        }

        void CANAPEProject_DocumentDeleted(object sender, DocumentEventArgs e)
        {
            //if (InvokeRequired)
            //{
            //    Invoke(new EventHandler<DocumentEventArgs>(CANAPEProject_DocumentDeleted), sender, e);
            //}
            //else
            //{
            //    TreeNode folder = GetFolderNodeForDocument(e.Document);
            //    if (folder != null)
            //    {
            //        foreach (TreeNode node in folder.Nodes)
            //        {
            //            if (node.Tag == e.Document)
            //            {
            //                node.Remove();
            //                break;
            //            }
            //        }
            //    }
            //}
        }

        void CANAPEProject_DocumentAdded(object sender, DocumentEventArgs e)
        {
            AddEntryToTree(e.Document);            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewProject.SelectedNode != null)
            {
                IDocumentObject ent = treeViewProject.SelectedNode.Tag as IDocumentObject;

                if(ent != null)
                {
                    Clipboard.SetData(DocumentDataType, ent.Copy());
                }
            }
        }

        private void contextMenuStripProject_Opening(object sender, CancelEventArgs e)
        {
            if (treeViewProject.SelectedNode != null)
            {
                object selectedTag = treeViewProject.SelectedNode.Tag;
                if (selectedTag is IDocumentObject)
                {
                    duplicateDocumentToolStripMenuItem.Enabled = true;
                    propertiesToolStripMenuItem.Enabled = false;
                }
                else
                {
                    duplicateDocumentToolStripMenuItem.Enabled = false;
                    propertiesToolStripMenuItem.Enabled = true;
                }

                testDocumentToolStripMenuItem.Enabled = HasDocumentTestType(selectedTag as IDocumentObject);
                
                renameToolStripMenuItem.Enabled = true;
            }
            else
            {
                renameToolStripMenuItem.Enabled = false;
                duplicateDocumentToolStripMenuItem.Enabled = false;
                testDocumentToolStripMenuItem.Visible = false;
            }

            testDocumentToolStripMenuItem.DropDownItems.Clear();
            foreach (IDocumentObject doc in CANAPEProject.CurrentProject.GetDocumentsByType(typeof(ScriptDocument)))
            {
                ToolStripItem item = testDocumentToolStripMenuItem.DropDownItems.Add(doc.Name);
                item.Click += new EventHandler(this.testDocumentToolStripMenuItem_Click);
                item.Tag = doc;
            }

            testGraphDocumentToolStripMenuItem.DropDownItems.Clear();
            foreach (IDocumentObject doc in CANAPEProject.CurrentProject.GetDocumentsByType(typeof(NetGraphDocument)))
            {
                ToolStripItem item = testGraphDocumentToolStripMenuItem.DropDownItems.Add(doc.Name);
                item.Click += new EventHandler(this.testDocumentToolStripMenuItem_Click);
                item.Tag = doc;
            }

            testDocumentToolStripMenuItem.Enabled = testDocumentToolStripMenuItem.DropDownItems.Count > 0;
            testGraphDocumentToolStripMenuItem.Enabled = testGraphDocumentToolStripMenuItem.DropDownItems.Count > 0;            
        }

        void extensionItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item != null)
            {
                IDocumentFactory factory = GetFactory((Type)item.Tag);

                OnDisplayDocument(CANAPEProject.CurrentProject.AddNewDocument(factory.CreateDocument(this)));
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (Clipboard.ContainsData(DocumentDataType))
            {
                IDocumentObject document = (IDocumentObject)Clipboard.GetData(DocumentDataType);
                
                CANAPEProject.CurrentProject.AddDocument(document, true);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewProject.SelectedNode != null)
            {
                IDocumentObject ent = treeViewProject.SelectedNode.Tag as IDocumentObject;

                if (ent != null)
                {
                    if (MessageBox.Show(this, String.Format(CANAPE.Properties.Resources.ProjectExplorer_DeleteDocument, ent.Name),
                        CANAPE.Properties.Resources.ProjectExplorer_DeleteDocumentCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Call out to close window
                        TreeNode parentNode = treeViewProject.SelectedNode.Parent;

                        CANAPEProject.CurrentProject.DeleteDocument(ent);
                        treeViewProject.SelectedNode.Remove();
                        _documentToNode.Remove(ent.Uuid);
                        OnDocumentDeleted(ent);

                        if (parentNode.Nodes.Count == 0)
                        {
                            parentNode.SelectedImageIndex = CLOSED_FOLDER_IMAGE_INDEX;
                            parentNode.ImageIndex = CLOSED_FOLDER_IMAGE_INDEX;
                        }
                    }
                }
            }
        }

        private void packetLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument<PacketLogDocument>());
        }

        private void tCPServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument<NetServerDocument>());
        }

        private void fixedTCPProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument<FixedProxyDocument>());
        }

        private void socksProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument<SocksProxyDocument>());
        }

        private void hTTPProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument<FullHttpProxyDocument>());
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewProject.SelectedNode != null)
            {
                deleteToolStripMenuItem.ShortcutKeys = Keys.None;
                treeViewProject.SelectedNode.BeginEdit();
            }
        }

        private void textDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument<TextDocument>());
        }

        private void emptyGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetGraphDocument document = CANAPEProject.CurrentProject.CreateDocument<NetGraphDocument>();
            OnDisplayDocument(document);
        }

        private void basicProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetGraphDocument document = CANAPEProject.CurrentProject.CreateDocument<NetGraphDocument>();
            BuildDefaultProxyNetgraph(document);
            OnDisplayDocument(document);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (EditProjectPropertiesForm frm = new EditProjectPropertiesForm())
            {
                frm.ShowDialog(this);
            }
        }

        private void testDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDocumentObject doc = ((ToolStripItem)sender).Tag as IDocumentObject;

            if (doc != null)
            {
                Type testType = GetDocumentTestType(doc);

                if (testType != null)
                {
                    try
                    {
                        TestDocument document = (TestDocument)Activator.CreateInstance(testType, doc);
                        document.Name = String.Format(CANAPE.Properties.Resources.ProjectExplorer_TestName, doc.Name);
                        CANAPEProject.CurrentProject.AddDocument(document, false);

                        OnDisplayDocument(document);
                    }
                    catch (MissingMethodException)
                    {
                        MessageBox.Show(this, CANAPE.Properties.Resources.ProjectExplorer_CouldntCreateTest, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private void assemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = CANAPE.Properties.Resources.ProjectExplorer_AssembliesFileFilter;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        AssemblyDocument document = new AssemblyDocument(dlg.FileName);
                        document.Name = document.GetName().Name;

                        CANAPEProject.CurrentProject.AddDocument(document, false);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, ex.Message,
                            CANAPE.Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (BadImageFormatException ex)
                    {
                        MessageBox.Show(this, ex.Message,
                            CANAPE.Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void binaryDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument(typeof(BinaryDocument)));
        }

        private void parserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument(typeof(ParserDocument)));
        }

        private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SelectTemplateForm frm = new SelectScriptTemplateForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    CANAPETemplate template = frm.Template;

                    ScriptDocument document = CANAPEProject.CurrentProject.CreateDocument<ScriptDocument>(template.TypeName);

                    document.Script = template.GetText();

                    OnDisplayDocument(document);
                }
            }
        }

        private void treeViewProject_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != treeViewProject.TopNode)
            {
                e.Node.ImageIndex = OPEN_FOLDER_IMAGE_INDEX;
                e.Node.SelectedImageIndex = OPEN_FOLDER_IMAGE_INDEX;
            }
        }

        private void treeViewProject_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != treeViewProject.TopNode)
            {
                e.Node.ImageIndex = CLOSED_FOLDER_IMAGE_INDEX;
                e.Node.SelectedImageIndex = CLOSED_FOLDER_IMAGE_INDEX;
            }
        }

        private void networkClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument(typeof(NetAutoClientDocument)));
        }

        private void stateGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument(typeof(StateGraphDocument)));
        }

        private void editProjectMetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(MetaEditorForm frm = new MetaEditorForm())
            {
                frm.ShowDialog(this);
            }
        }

        private string OpenFile(string filter)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = filter;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    return dlg.FileName;
                }
            }

            return null;
        }

        private void CreateFromFile(string filter, Func<string, IDocumentObject> docFromFile)
        {
            string fileName = OpenFile(filter);

            if (fileName != null)
            {
                try
                {
                    OnDisplayDocument(CANAPEProject.CurrentProject.AddNewDocument(docFromFile(fileName)));
                }
                catch (IOException ex)
                {
                    MessageBox.Show(this, ex.Message,
                            CANAPE.Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(this, ex.Message,
                         CANAPE.Properties.Resources.MessageBox_ErrorString,
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFromFile(Properties.Resources.TextFileFilter_String, fileName => new TextDocument(File.ReadAllText(fileName)));
        }

        private void fromFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateFromFile(Properties.Resources.AllFilesFilter_String, fileName => new BinaryDocument(File.ReadAllBytes(fileName)));
        }        

        private void treeViewProject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeViewProject.GetNodeAt(e.Location);

                if (node != null)
                {
                    treeViewProject.SelectedNode = node;
                }
            }
        }

        private void fromSerializedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFromFile(Properties.Resources.AllFilesFilter_String, fileName => new PacketLogDocument(GeneralUtils.DeserializeLogPackets(fileName)));
        }

        private void fromPCAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFromFile(Properties.Resources.PCAPFileFilter_String, fileName => new PacketLogDocument(PcapReader.Load(fileName, false)));            
        }

        private void hTTPReverseProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument<HttpReverseProxyDocument>());
        }

        private void packetLogDiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisplayDocument(CANAPEProject.CurrentProject.CreateDocument<PacketLogDiffDocument>());
        }

        private void duplicateDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDocumentObject ent = treeViewProject.SelectedNode.Tag as IDocumentObject;

            if (ent != null)
            {
                CANAPEProject.CurrentProject.AddDocument(ent.Copy(), true);
            }
        }

        private void treeViewProject_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            deleteToolStripMenuItem.ShortcutKeys = Keys.None;
        }  
    }
}
