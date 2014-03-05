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

namespace CANAPE.Controls.NodeEditors
{
    partial class TreeDataKeyEditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeDataKeyEditorControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeViewNodes = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libraryParserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToBytesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToBinaryStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToUnicodeStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToUTF8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToUTF16LEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToUTF16BEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToUTF32LEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToUTF32BEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToUTF7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.renameNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.dataNodeEditorControl = new CANAPE.Controls.NodeEditors.DataNodeEditorControl();
            this.inlineSearchControl = new CANAPE.Controls.InlineSearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeViewNodes);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dataNodeEditorControl);
            // 
            // treeViewNodes
            // 
            this.treeViewNodes.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.treeViewNodes, "treeViewNodes");
            this.treeViewNodes.FullRowSelect = true;
            this.treeViewNodes.HideSelection = false;
            this.treeViewNodes.ImageList = this.imageList;
            this.treeViewNodes.LabelEdit = true;
            this.treeViewNodes.Name = "treeViewNodes";
            this.treeViewNodes.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewNodes_AfterLabelEdit);
            this.treeViewNodes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewNodes_AfterSelect);
            this.treeViewNodes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewNodes_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPathToolStripMenuItem,
            this.importFromFileToolStripMenuItem,
            this.exportToFileToolStripMenuItem,
            this.copyAsFilterToolStripMenuItem,
            this.parseWithToolStripMenuItem,
            this.convertToToolStripMenuItem,
            this.toolStripSeparator1,
            this.renameNodeToolStripMenuItem,
            this.deleteNodeToolStripMenuItem,
            this.cloneNodeToolStripMenuItem,
            this.expandNodeToolStripMenuItem,
            this.collapseNodeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // copyPathToolStripMenuItem
            // 
            this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            resources.ApplyResources(this.copyPathToolStripMenuItem, "copyPathToolStripMenuItem");
            this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.copyPathToolStripMenuItem_Click);
            // 
            // importFromFileToolStripMenuItem
            // 
            this.importFromFileToolStripMenuItem.Name = "importFromFileToolStripMenuItem";
            resources.ApplyResources(this.importFromFileToolStripMenuItem, "importFromFileToolStripMenuItem");
            this.importFromFileToolStripMenuItem.Click += new System.EventHandler(this.importFromFileToolStripMenuItem_Click);
            // 
            // exportToFileToolStripMenuItem
            // 
            this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
            resources.ApplyResources(this.exportToFileToolStripMenuItem, "exportToFileToolStripMenuItem");
            this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
            // 
            // copyAsFilterToolStripMenuItem
            // 
            this.copyAsFilterToolStripMenuItem.Name = "copyAsFilterToolStripMenuItem";
            resources.ApplyResources(this.copyAsFilterToolStripMenuItem, "copyAsFilterToolStripMenuItem");
            this.copyAsFilterToolStripMenuItem.Click += new System.EventHandler(this.copyAsFilterToolStripMenuItem_Click);
            // 
            // parseWithToolStripMenuItem
            // 
            this.parseWithToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.libraryParserToolStripMenuItem,
            this.scriptToolStripMenuItem});
            this.parseWithToolStripMenuItem.Name = "parseWithToolStripMenuItem";
            resources.ApplyResources(this.parseWithToolStripMenuItem, "parseWithToolStripMenuItem");
            // 
            // libraryParserToolStripMenuItem
            // 
            this.libraryParserToolStripMenuItem.Name = "libraryParserToolStripMenuItem";
            resources.ApplyResources(this.libraryParserToolStripMenuItem, "libraryParserToolStripMenuItem");
            this.libraryParserToolStripMenuItem.Click += new System.EventHandler(this.libraryParserToolStripMenuItem_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            resources.ApplyResources(this.scriptToolStripMenuItem, "scriptToolStripMenuItem");
            this.scriptToolStripMenuItem.Click += new System.EventHandler(this.scriptToolStripMenuItem_Click);
            // 
            // convertToToolStripMenuItem
            // 
            this.convertToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToBytesToolStripMenuItem,
            this.convertToBinaryStringToolStripMenuItem,
            this.convertToUnicodeStringToolStripMenuItem});
            this.convertToToolStripMenuItem.Name = "convertToToolStripMenuItem";
            resources.ApplyResources(this.convertToToolStripMenuItem, "convertToToolStripMenuItem");
            // 
            // convertToBytesToolStripMenuItem
            // 
            this.convertToBytesToolStripMenuItem.Name = "convertToBytesToolStripMenuItem";
            resources.ApplyResources(this.convertToBytesToolStripMenuItem, "convertToBytesToolStripMenuItem");
            this.convertToBytesToolStripMenuItem.Click += new System.EventHandler(this.convertToBytesToolStripMenuItem_Click);
            // 
            // convertToBinaryStringToolStripMenuItem
            // 
            this.convertToBinaryStringToolStripMenuItem.Name = "convertToBinaryStringToolStripMenuItem";
            resources.ApplyResources(this.convertToBinaryStringToolStripMenuItem, "convertToBinaryStringToolStripMenuItem");
            this.convertToBinaryStringToolStripMenuItem.Click += new System.EventHandler(this.convertToBinaryStringToolStripMenuItem_Click);
            // 
            // convertToUnicodeStringToolStripMenuItem
            // 
            this.convertToUnicodeStringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToUTF8ToolStripMenuItem,
            this.convertToUTF16LEToolStripMenuItem,
            this.convertToUTF16BEToolStripMenuItem,
            this.convertToUTF32LEToolStripMenuItem,
            this.convertToUTF32BEToolStripMenuItem,
            this.convertToUTF7ToolStripMenuItem});
            this.convertToUnicodeStringToolStripMenuItem.Name = "convertToUnicodeStringToolStripMenuItem";
            resources.ApplyResources(this.convertToUnicodeStringToolStripMenuItem, "convertToUnicodeStringToolStripMenuItem");
            // 
            // convertToUTF8ToolStripMenuItem
            // 
            this.convertToUTF8ToolStripMenuItem.Name = "convertToUTF8ToolStripMenuItem";
            resources.ApplyResources(this.convertToUTF8ToolStripMenuItem, "convertToUTF8ToolStripMenuItem");
            this.convertToUTF8ToolStripMenuItem.Click += new System.EventHandler(this.convertToUTF8ToolStripMenuItem_Click);
            // 
            // convertToUTF16LEToolStripMenuItem
            // 
            this.convertToUTF16LEToolStripMenuItem.Name = "convertToUTF16LEToolStripMenuItem";
            resources.ApplyResources(this.convertToUTF16LEToolStripMenuItem, "convertToUTF16LEToolStripMenuItem");
            this.convertToUTF16LEToolStripMenuItem.Click += new System.EventHandler(this.convertToUTF16LEToolStripMenuItem_Click);
            // 
            // convertToUTF16BEToolStripMenuItem
            // 
            this.convertToUTF16BEToolStripMenuItem.Name = "convertToUTF16BEToolStripMenuItem";
            resources.ApplyResources(this.convertToUTF16BEToolStripMenuItem, "convertToUTF16BEToolStripMenuItem");
            this.convertToUTF16BEToolStripMenuItem.Click += new System.EventHandler(this.convertToUTF16BEToolStripMenuItem_Click);
            // 
            // convertToUTF32LEToolStripMenuItem
            // 
            this.convertToUTF32LEToolStripMenuItem.Name = "convertToUTF32LEToolStripMenuItem";
            resources.ApplyResources(this.convertToUTF32LEToolStripMenuItem, "convertToUTF32LEToolStripMenuItem");
            this.convertToUTF32LEToolStripMenuItem.Click += new System.EventHandler(this.convertToUTF32LEToolStripMenuItem_Click);
            // 
            // convertToUTF32BEToolStripMenuItem
            // 
            this.convertToUTF32BEToolStripMenuItem.Name = "convertToUTF32BEToolStripMenuItem";
            resources.ApplyResources(this.convertToUTF32BEToolStripMenuItem, "convertToUTF32BEToolStripMenuItem");
            this.convertToUTF32BEToolStripMenuItem.Click += new System.EventHandler(this.convertToUTF32BEToolStripMenuItem_Click);
            // 
            // convertToUTF7ToolStripMenuItem
            // 
            this.convertToUTF7ToolStripMenuItem.Name = "convertToUTF7ToolStripMenuItem";
            resources.ApplyResources(this.convertToUTF7ToolStripMenuItem, "convertToUTF7ToolStripMenuItem");
            this.convertToUTF7ToolStripMenuItem.Click += new System.EventHandler(this.convertToUTF7ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // renameNodeToolStripMenuItem
            // 
            this.renameNodeToolStripMenuItem.Name = "renameNodeToolStripMenuItem";
            resources.ApplyResources(this.renameNodeToolStripMenuItem, "renameNodeToolStripMenuItem");
            this.renameNodeToolStripMenuItem.Click += new System.EventHandler(this.renameNodeToolStripMenuItem_Click);
            // 
            // deleteNodeToolStripMenuItem
            // 
            this.deleteNodeToolStripMenuItem.Name = "deleteNodeToolStripMenuItem";
            resources.ApplyResources(this.deleteNodeToolStripMenuItem, "deleteNodeToolStripMenuItem");
            this.deleteNodeToolStripMenuItem.Click += new System.EventHandler(this.deleteNodeToolStripMenuItem_Click);
            // 
            // cloneNodeToolStripMenuItem
            // 
            this.cloneNodeToolStripMenuItem.Name = "cloneNodeToolStripMenuItem";
            resources.ApplyResources(this.cloneNodeToolStripMenuItem, "cloneNodeToolStripMenuItem");
            this.cloneNodeToolStripMenuItem.Click += new System.EventHandler(this.cloneNodeToolStripMenuItem_Click);
            // 
            // expandNodeToolStripMenuItem
            // 
            this.expandNodeToolStripMenuItem.Name = "expandNodeToolStripMenuItem";
            resources.ApplyResources(this.expandNodeToolStripMenuItem, "expandNodeToolStripMenuItem");
            this.expandNodeToolStripMenuItem.Click += new System.EventHandler(this.expandNodeToolStripMenuItem_Click);
            // 
            // collapseNodeToolStripMenuItem
            // 
            this.collapseNodeToolStripMenuItem.Name = "collapseNodeToolStripMenuItem";
            resources.ApplyResources(this.collapseNodeToolStripMenuItem, "collapseNodeToolStripMenuItem");
            this.collapseNodeToolStripMenuItem.Click += new System.EventHandler(this.collapseNodeToolStripMenuItem_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList.Images.SetKeyName(0, "VSObject_Class.bmp");
            this.imageList.Images.SetKeyName(1, "VSObject_Class_Private.bmp");
            this.imageList.Images.SetKeyName(2, "VSObject_Field.bmp");
            this.imageList.Images.SetKeyName(3, "VSObject_Field_Private.bmp");
            this.imageList.Images.SetKeyName(4, "VSObject_Class_Script.bmp");
            this.imageList.Images.SetKeyName(5, "VSObject_Class_Private_Script.bmp");
            this.imageList.Images.SetKeyName(6, "VSObject_Field_Script.bmp");
            this.imageList.Images.SetKeyName(7, "VSObject_Field_Private_Script.bmp");
            // 
            // dataNodeEditorControl
            // 
            resources.ApplyResources(this.dataNodeEditorControl, "dataNodeEditorControl");
            this.dataNodeEditorControl.Name = "dataNodeEditorControl";
            this.dataNodeEditorControl.ShowTreeEditor = false;
            // 
            // inlineSearchControl
            // 
            resources.ApplyResources(this.inlineSearchControl, "inlineSearchControl");
            this.inlineSearchControl.Name = "inlineSearchControl";
            this.inlineSearchControl.SearchNext += new System.EventHandler<CANAPE.Controls.InlineSearchControl.SearchEventArgs>(this.inlineSearchControl_SearchNext);
            this.inlineSearchControl.SearchPrev += new System.EventHandler<CANAPE.Controls.InlineSearchControl.SearchEventArgs>(this.inlineSearchControl_SearchPrev);
            // 
            // TreeDataKeyEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.inlineSearchControl);
            this.Controls.Add(this.splitContainer);
            this.Name = "TreeDataKeyEditorControl";
            this.Load += new System.EventHandler(this.DataFrameViewerControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeViewNodes;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
        private DataNodeEditorControl dataNodeEditorControl;
        private System.Windows.Forms.ToolStripMenuItem copyAsFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseWithToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libraryParserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToBytesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToBinaryStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToUnicodeStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToUTF8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToUTF16LEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToUTF16BEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToUTF32LEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToUTF32BEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToUTF7ToolStripMenuItem;
        private InlineSearchControl inlineSearchControl;
        private System.Windows.Forms.ToolStripMenuItem expandNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem renameNodeToolStripMenuItem;
    }
}
