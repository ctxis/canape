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

namespace CANAPE.Controls
{
    partial class ProjectExplorerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectExplorerControl));
            this.treeViewProject = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicProxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stateGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testGraphDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tCPServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixedTCPProxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.socksProxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTTPProxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTTPReverseProxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.fromSerializedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromPCAPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.packetLogDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editProjectMetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewProject
            // 
            this.treeViewProject.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.treeViewProject, "treeViewProject");
            this.treeViewProject.ImageList = this.imageListIcons;
            this.treeViewProject.LabelEdit = true;
            this.treeViewProject.Name = "treeViewProject";
            this.treeViewProject.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeViewProject.Nodes")))});
            this.treeViewProject.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewProject_BeforeLabelEdit);
            this.treeViewProject.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewProject_AfterLabelEdit);
            this.treeViewProject.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewProject_BeforeCollapse);
            this.treeViewProject.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewProject_BeforeExpand);
            this.treeViewProject.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewProject_NodeMouseDoubleClick);
            this.treeViewProject.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewProject_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.duplicateDocumentToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.editProjectMetaToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripProject_Opening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netGraphToolStripMenuItem,
            this.networkServiceToolStripMenuItem,
            this.networkClientToolStripMenuItem,
            this.dynamicToolStripMenuItem,
            this.dataToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            // 
            // netGraphToolStripMenuItem
            // 
            this.netGraphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyGraphToolStripMenuItem,
            this.basicProxyToolStripMenuItem,
            this.stateGraphToolStripMenuItem,
            this.testGraphDocumentToolStripMenuItem});
            this.netGraphToolStripMenuItem.Name = "netGraphToolStripMenuItem";
            resources.ApplyResources(this.netGraphToolStripMenuItem, "netGraphToolStripMenuItem");
            // 
            // emptyGraphToolStripMenuItem
            // 
            this.emptyGraphToolStripMenuItem.Name = "emptyGraphToolStripMenuItem";
            resources.ApplyResources(this.emptyGraphToolStripMenuItem, "emptyGraphToolStripMenuItem");
            this.emptyGraphToolStripMenuItem.Click += new System.EventHandler(this.emptyGraphToolStripMenuItem_Click);
            // 
            // basicProxyToolStripMenuItem
            // 
            this.basicProxyToolStripMenuItem.Name = "basicProxyToolStripMenuItem";
            resources.ApplyResources(this.basicProxyToolStripMenuItem, "basicProxyToolStripMenuItem");
            this.basicProxyToolStripMenuItem.Click += new System.EventHandler(this.basicProxyToolStripMenuItem_Click);
            // 
            // stateGraphToolStripMenuItem
            // 
            this.stateGraphToolStripMenuItem.Name = "stateGraphToolStripMenuItem";
            resources.ApplyResources(this.stateGraphToolStripMenuItem, "stateGraphToolStripMenuItem");
            this.stateGraphToolStripMenuItem.Click += new System.EventHandler(this.stateGraphToolStripMenuItem_Click);
            // 
            // testGraphDocumentToolStripMenuItem
            // 
            this.testGraphDocumentToolStripMenuItem.Name = "testGraphDocumentToolStripMenuItem";
            resources.ApplyResources(this.testGraphDocumentToolStripMenuItem, "testGraphDocumentToolStripMenuItem");
            // 
            // networkServiceToolStripMenuItem
            // 
            this.networkServiceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tCPServerToolStripMenuItem,
            this.fixedTCPProxyToolStripMenuItem,
            this.socksProxyToolStripMenuItem,
            this.hTTPProxyToolStripMenuItem,
            this.hTTPReverseProxyToolStripMenuItem});
            this.networkServiceToolStripMenuItem.Name = "networkServiceToolStripMenuItem";
            resources.ApplyResources(this.networkServiceToolStripMenuItem, "networkServiceToolStripMenuItem");
            // 
            // tCPServerToolStripMenuItem
            // 
            this.tCPServerToolStripMenuItem.Name = "tCPServerToolStripMenuItem";
            resources.ApplyResources(this.tCPServerToolStripMenuItem, "tCPServerToolStripMenuItem");
            this.tCPServerToolStripMenuItem.Click += new System.EventHandler(this.tCPServerToolStripMenuItem_Click);
            // 
            // fixedTCPProxyToolStripMenuItem
            // 
            this.fixedTCPProxyToolStripMenuItem.Name = "fixedTCPProxyToolStripMenuItem";
            resources.ApplyResources(this.fixedTCPProxyToolStripMenuItem, "fixedTCPProxyToolStripMenuItem");
            this.fixedTCPProxyToolStripMenuItem.Click += new System.EventHandler(this.fixedTCPProxyToolStripMenuItem_Click);
            // 
            // socksProxyToolStripMenuItem
            // 
            this.socksProxyToolStripMenuItem.Name = "socksProxyToolStripMenuItem";
            resources.ApplyResources(this.socksProxyToolStripMenuItem, "socksProxyToolStripMenuItem");
            this.socksProxyToolStripMenuItem.Click += new System.EventHandler(this.socksProxyToolStripMenuItem_Click);
            // 
            // hTTPProxyToolStripMenuItem
            // 
            this.hTTPProxyToolStripMenuItem.Name = "hTTPProxyToolStripMenuItem";
            resources.ApplyResources(this.hTTPProxyToolStripMenuItem, "hTTPProxyToolStripMenuItem");
            this.hTTPProxyToolStripMenuItem.Click += new System.EventHandler(this.hTTPProxyToolStripMenuItem_Click);
            // 
            // hTTPReverseProxyToolStripMenuItem
            // 
            this.hTTPReverseProxyToolStripMenuItem.Name = "hTTPReverseProxyToolStripMenuItem";
            resources.ApplyResources(this.hTTPReverseProxyToolStripMenuItem, "hTTPReverseProxyToolStripMenuItem");
            this.hTTPReverseProxyToolStripMenuItem.Click += new System.EventHandler(this.hTTPReverseProxyToolStripMenuItem_Click);
            // 
            // networkClientToolStripMenuItem
            // 
            this.networkClientToolStripMenuItem.Name = "networkClientToolStripMenuItem";
            resources.ApplyResources(this.networkClientToolStripMenuItem, "networkClientToolStripMenuItem");
            this.networkClientToolStripMenuItem.Click += new System.EventHandler(this.networkClientToolStripMenuItem_Click);
            // 
            // dynamicToolStripMenuItem
            // 
            this.dynamicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptToolStripMenuItem,
            this.assemblyToolStripMenuItem,
            this.parserToolStripMenuItem,
            this.testDocumentToolStripMenuItem});
            this.dynamicToolStripMenuItem.Name = "dynamicToolStripMenuItem";
            resources.ApplyResources(this.dynamicToolStripMenuItem, "dynamicToolStripMenuItem");
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            resources.ApplyResources(this.scriptToolStripMenuItem, "scriptToolStripMenuItem");
            this.scriptToolStripMenuItem.Click += new System.EventHandler(this.scriptToolStripMenuItem_Click);
            // 
            // assemblyToolStripMenuItem
            // 
            this.assemblyToolStripMenuItem.Name = "assemblyToolStripMenuItem";
            resources.ApplyResources(this.assemblyToolStripMenuItem, "assemblyToolStripMenuItem");
            this.assemblyToolStripMenuItem.Click += new System.EventHandler(this.assemblyToolStripMenuItem_Click);
            // 
            // parserToolStripMenuItem
            // 
            this.parserToolStripMenuItem.Name = "parserToolStripMenuItem";
            resources.ApplyResources(this.parserToolStripMenuItem, "parserToolStripMenuItem");
            this.parserToolStripMenuItem.Click += new System.EventHandler(this.parserToolStripMenuItem_Click);
            // 
            // testDocumentToolStripMenuItem
            // 
            this.testDocumentToolStripMenuItem.Name = "testDocumentToolStripMenuItem";
            resources.ApplyResources(this.testDocumentToolStripMenuItem, "testDocumentToolStripMenuItem");
            this.testDocumentToolStripMenuItem.Click += new System.EventHandler(this.testDocumentToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packetLogToolStripMenuItem,
            this.textDocumentToolStripMenuItem,
            this.binaryDocumentToolStripMenuItem,
            this.packetLogDiffToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            resources.ApplyResources(this.dataToolStripMenuItem, "dataToolStripMenuItem");
            // 
            // packetLogToolStripMenuItem
            // 
            this.packetLogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem2,
            this.fromSerializedFileToolStripMenuItem,
            this.fromPCAPToolStripMenuItem});
            this.packetLogToolStripMenuItem.Name = "packetLogToolStripMenuItem";
            resources.ApplyResources(this.packetLogToolStripMenuItem, "packetLogToolStripMenuItem");
            // 
            // emptyToolStripMenuItem2
            // 
            this.emptyToolStripMenuItem2.Name = "emptyToolStripMenuItem2";
            resources.ApplyResources(this.emptyToolStripMenuItem2, "emptyToolStripMenuItem2");
            this.emptyToolStripMenuItem2.Click += new System.EventHandler(this.packetLogToolStripMenuItem_Click);
            // 
            // fromSerializedFileToolStripMenuItem
            // 
            this.fromSerializedFileToolStripMenuItem.Name = "fromSerializedFileToolStripMenuItem";
            resources.ApplyResources(this.fromSerializedFileToolStripMenuItem, "fromSerializedFileToolStripMenuItem");
            this.fromSerializedFileToolStripMenuItem.Click += new System.EventHandler(this.fromSerializedFileToolStripMenuItem_Click);
            // 
            // fromPCAPToolStripMenuItem
            // 
            this.fromPCAPToolStripMenuItem.Name = "fromPCAPToolStripMenuItem";
            resources.ApplyResources(this.fromPCAPToolStripMenuItem, "fromPCAPToolStripMenuItem");
            this.fromPCAPToolStripMenuItem.Click += new System.EventHandler(this.fromPCAPToolStripMenuItem_Click);
            // 
            // textDocumentToolStripMenuItem
            // 
            this.textDocumentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem,
            this.fromFileToolStripMenuItem});
            this.textDocumentToolStripMenuItem.Name = "textDocumentToolStripMenuItem";
            resources.ApplyResources(this.textDocumentToolStripMenuItem, "textDocumentToolStripMenuItem");
            // 
            // emptyToolStripMenuItem
            // 
            this.emptyToolStripMenuItem.Name = "emptyToolStripMenuItem";
            resources.ApplyResources(this.emptyToolStripMenuItem, "emptyToolStripMenuItem");
            this.emptyToolStripMenuItem.Click += new System.EventHandler(this.textDocumentToolStripMenuItem_Click);
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            resources.ApplyResources(this.fromFileToolStripMenuItem, "fromFileToolStripMenuItem");
            this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fromFileToolStripMenuItem_Click);
            // 
            // binaryDocumentToolStripMenuItem
            // 
            this.binaryDocumentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem1,
            this.fromFileToolStripMenuItem1});
            this.binaryDocumentToolStripMenuItem.Name = "binaryDocumentToolStripMenuItem";
            resources.ApplyResources(this.binaryDocumentToolStripMenuItem, "binaryDocumentToolStripMenuItem");
            // 
            // emptyToolStripMenuItem1
            // 
            this.emptyToolStripMenuItem1.Name = "emptyToolStripMenuItem1";
            resources.ApplyResources(this.emptyToolStripMenuItem1, "emptyToolStripMenuItem1");
            this.emptyToolStripMenuItem1.Click += new System.EventHandler(this.binaryDocumentToolStripMenuItem_Click);
            // 
            // fromFileToolStripMenuItem1
            // 
            this.fromFileToolStripMenuItem1.Name = "fromFileToolStripMenuItem1";
            resources.ApplyResources(this.fromFileToolStripMenuItem1, "fromFileToolStripMenuItem1");
            this.fromFileToolStripMenuItem1.Click += new System.EventHandler(this.fromFileToolStripMenuItem1_Click);
            // 
            // packetLogDiffToolStripMenuItem
            // 
            this.packetLogDiffToolStripMenuItem.Name = "packetLogDiffToolStripMenuItem";
            resources.ApplyResources(this.packetLogDiffToolStripMenuItem, "packetLogDiffToolStripMenuItem");
            this.packetLogDiffToolStripMenuItem.Click += new System.EventHandler(this.packetLogDiffToolStripMenuItem_Click);
            // 
            // duplicateDocumentToolStripMenuItem
            // 
            this.duplicateDocumentToolStripMenuItem.Name = "duplicateDocumentToolStripMenuItem";
            resources.ApplyResources(this.duplicateDocumentToolStripMenuItem, "duplicateDocumentToolStripMenuItem");
            this.duplicateDocumentToolStripMenuItem.Click += new System.EventHandler(this.duplicateDocumentToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            resources.ApplyResources(this.renameToolStripMenuItem, "renameToolStripMenuItem");
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // editProjectMetaToolStripMenuItem
            // 
            this.editProjectMetaToolStripMenuItem.Name = "editProjectMetaToolStripMenuItem";
            resources.ApplyResources(this.editProjectMetaToolStripMenuItem, "editProjectMetaToolStripMenuItem");
            this.editProjectMetaToolStripMenuItem.Click += new System.EventHandler(this.editProjectMetaToolStripMenuItem_Click);
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageListIcons.Images.SetKeyName(0, "VSProject_genericproject.ico");
            this.imageListIcons.Images.SetKeyName(1, "UtilityText.ico");
            this.imageListIcons.Images.SetKeyName(2, "VSFolder_closed.bmp");
            this.imageListIcons.Images.SetKeyName(3, "VSFolder_open.bmp");
            // 
            // ProjectExplorerControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewProject);
            this.Name = "ProjectExplorerControl";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewProject;
        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packetLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tCPServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixedTCPProxyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem socksProxyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTTPProxyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicProxyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assemblyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dynamicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testGraphDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stateGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editProjectMetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem fromSerializedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromPCAPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hTTPReverseProxyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packetLogDiffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateDocumentToolStripMenuItem;
    }
}
