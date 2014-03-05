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
    partial class HexEditorControl
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
            System.Windows.Forms.ColumnHeader columnHeaderType;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HexEditorControl));
            System.Windows.Forms.ColumnHeader columnHeaderValue;
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.contextMenuStripHex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPacketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fillSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewInspector = new CANAPE.Controls.ListViewExtension();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeInspectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mD5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sHA1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitChecksumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cRC32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPv4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPv6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unixTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsFileTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDefaultInspectorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripHex.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeaderType
            // 
            resources.ApplyResources(columnHeaderType, "columnHeaderType");
            // 
            // columnHeaderValue
            // 
            resources.ApplyResources(columnHeaderValue, "columnHeaderValue");
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.hexBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewInspector);
            // 
            // hexBox
            // 
            this.hexBox.ContextMenuStrip = this.contextMenuStripHex;
            resources.ApplyResources(this.hexBox, "hexBox");
            this.hexBox.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Name = "hexBox";
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.StringViewVisible = true;
            this.hexBox.UseFixedBytesPerLine = true;
            this.hexBox.VScrollBarVisible = true;
            this.hexBox.SelectionStartChanged += new System.EventHandler(this.hexBox_SelectionStartChanged);
            this.hexBox.SelectionLengthChanged += new System.EventHandler(this.hexBox_SelectionLengthChanged);
            // 
            // contextMenuStripHex
            // 
            this.contextMenuStripHex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.copyHexToolStripMenuItem,
            this.copyToToolStripMenuItem,
            this.copyPacketToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.pasteHexToolStripMenuItem,
            this.toolStripSeparator1,
            this.fillSelectionToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.selectBlockToolStripMenuItem,
            this.toolStripSeparator2,
            this.convertToolStripMenuItem,
            this.applyScriptToolStripMenuItem});
            this.contextMenuStripHex.Name = "contextMenuStripHex";
            resources.ApplyResources(this.contextMenuStripHex, "contextMenuStripHex");
            this.contextMenuStripHex.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripHex_Opening);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyHexToolStripMenuItem
            // 
            this.copyHexToolStripMenuItem.Name = "copyHexToolStripMenuItem";
            resources.ApplyResources(this.copyHexToolStripMenuItem, "copyHexToolStripMenuItem");
            this.copyHexToolStripMenuItem.Click += new System.EventHandler(this.copyHexToolStripMenuItem_Click);
            // 
            // copyToToolStripMenuItem
            // 
            this.copyToToolStripMenuItem.Name = "copyToToolStripMenuItem";
            resources.ApplyResources(this.copyToToolStripMenuItem, "copyToToolStripMenuItem");
            // 
            // copyPacketToolStripMenuItem
            // 
            this.copyPacketToolStripMenuItem.Name = "copyPacketToolStripMenuItem";
            resources.ApplyResources(this.copyPacketToolStripMenuItem, "copyPacketToolStripMenuItem");
            this.copyPacketToolStripMenuItem.Click += new System.EventHandler(this.copyPacketToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // pasteHexToolStripMenuItem
            // 
            this.pasteHexToolStripMenuItem.Name = "pasteHexToolStripMenuItem";
            resources.ApplyResources(this.pasteHexToolStripMenuItem, "pasteHexToolStripMenuItem");
            this.pasteHexToolStripMenuItem.Click += new System.EventHandler(this.pasteHexToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // fillSelectionToolStripMenuItem
            // 
            this.fillSelectionToolStripMenuItem.Name = "fillSelectionToolStripMenuItem";
            resources.ApplyResources(this.fillSelectionToolStripMenuItem, "fillSelectionToolStripMenuItem");
            this.fillSelectionToolStripMenuItem.Click += new System.EventHandler(this.fillSelectionToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // selectBlockToolStripMenuItem
            // 
            this.selectBlockToolStripMenuItem.Name = "selectBlockToolStripMenuItem";
            resources.ApplyResources(this.selectBlockToolStripMenuItem, "selectBlockToolStripMenuItem");
            this.selectBlockToolStripMenuItem.Click += new System.EventHandler(this.selectBlockToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            resources.ApplyResources(this.convertToolStripMenuItem, "convertToolStripMenuItem");
            // 
            // applyScriptToolStripMenuItem
            // 
            this.applyScriptToolStripMenuItem.Name = "applyScriptToolStripMenuItem";
            resources.ApplyResources(this.applyScriptToolStripMenuItem, "applyScriptToolStripMenuItem");
            this.applyScriptToolStripMenuItem.DropDownOpening += new System.EventHandler(this.applyScriptToolStripMenuItem_DropDownOpening);
            // 
            // listViewInspector
            // 
            this.listViewInspector.AutoScrollList = false;
            this.listViewInspector.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderType,
            columnHeaderValue});
            this.listViewInspector.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.listViewInspector, "listViewInspector");
            this.listViewInspector.FullRowSelect = true;
            this.listViewInspector.GridLines = true;
            this.listViewInspector.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewInspector.Name = "listViewInspector";
            this.listViewInspector.UseCompatibleStateImageBehavior = false;
            this.listViewInspector.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTextToolStripMenuItem,
            this.removeInspectorToolStripMenuItem,
            this.addToolStripMenuItem,
            this.editDefaultInspectorsToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // copyTextToolStripMenuItem
            // 
            this.copyTextToolStripMenuItem.Name = "copyTextToolStripMenuItem";
            resources.ApplyResources(this.copyTextToolStripMenuItem, "copyTextToolStripMenuItem");
            this.copyTextToolStripMenuItem.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // removeInspectorToolStripMenuItem
            // 
            this.removeInspectorToolStripMenuItem.Name = "removeInspectorToolStripMenuItem";
            resources.ApplyResources(this.removeInspectorToolStripMenuItem, "removeInspectorToolStripMenuItem");
            this.removeInspectorToolStripMenuItem.Click += new System.EventHandler(this.removeInspectorToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mD5ToolStripMenuItem,
            this.sHA1ToolStripMenuItem,
            this.bitChecksumToolStripMenuItem,
            this.cRC32ToolStripMenuItem,
            this.networkToolStripMenuItem,
            this.dateToolStripMenuItem,
            this.extensionToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            // 
            // mD5ToolStripMenuItem
            // 
            this.mD5ToolStripMenuItem.Name = "mD5ToolStripMenuItem";
            resources.ApplyResources(this.mD5ToolStripMenuItem, "mD5ToolStripMenuItem");
            this.mD5ToolStripMenuItem.Click += new System.EventHandler(this.mD5ToolStripMenuItem_Click);
            // 
            // sHA1ToolStripMenuItem
            // 
            this.sHA1ToolStripMenuItem.Name = "sHA1ToolStripMenuItem";
            resources.ApplyResources(this.sHA1ToolStripMenuItem, "sHA1ToolStripMenuItem");
            this.sHA1ToolStripMenuItem.Click += new System.EventHandler(this.sHA1ToolStripMenuItem_Click);
            // 
            // bitChecksumToolStripMenuItem
            // 
            this.bitChecksumToolStripMenuItem.Name = "bitChecksumToolStripMenuItem";
            resources.ApplyResources(this.bitChecksumToolStripMenuItem, "bitChecksumToolStripMenuItem");
            this.bitChecksumToolStripMenuItem.Click += new System.EventHandler(this.bitChecksumToolStripMenuItem_Click);
            // 
            // cRC32ToolStripMenuItem
            // 
            this.cRC32ToolStripMenuItem.Name = "cRC32ToolStripMenuItem";
            resources.ApplyResources(this.cRC32ToolStripMenuItem, "cRC32ToolStripMenuItem");
            this.cRC32ToolStripMenuItem.Click += new System.EventHandler(this.cRC32ToolStripMenuItem_Click);
            // 
            // networkToolStripMenuItem
            // 
            this.networkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iPv4ToolStripMenuItem,
            this.iPv6ToolStripMenuItem});
            this.networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            resources.ApplyResources(this.networkToolStripMenuItem, "networkToolStripMenuItem");
            // 
            // iPv4ToolStripMenuItem
            // 
            this.iPv4ToolStripMenuItem.Name = "iPv4ToolStripMenuItem";
            resources.ApplyResources(this.iPv4ToolStripMenuItem, "iPv4ToolStripMenuItem");
            this.iPv4ToolStripMenuItem.Click += new System.EventHandler(this.iPv4ToolStripMenuItem_Click);
            // 
            // iPv6ToolStripMenuItem
            // 
            this.iPv6ToolStripMenuItem.Name = "iPv6ToolStripMenuItem";
            resources.ApplyResources(this.iPv6ToolStripMenuItem, "iPv6ToolStripMenuItem");
            this.iPv6ToolStripMenuItem.Click += new System.EventHandler(this.iPv6ToolStripMenuItem_Click);
            // 
            // dateToolStripMenuItem
            // 
            this.dateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unixTimeToolStripMenuItem,
            this.windowsFileTimeToolStripMenuItem});
            this.dateToolStripMenuItem.Name = "dateToolStripMenuItem";
            resources.ApplyResources(this.dateToolStripMenuItem, "dateToolStripMenuItem");
            // 
            // unixTimeToolStripMenuItem
            // 
            this.unixTimeToolStripMenuItem.Name = "unixTimeToolStripMenuItem";
            resources.ApplyResources(this.unixTimeToolStripMenuItem, "unixTimeToolStripMenuItem");
            this.unixTimeToolStripMenuItem.Click += new System.EventHandler(this.unixTimeToolStripMenuItem_Click);
            // 
            // windowsFileTimeToolStripMenuItem
            // 
            this.windowsFileTimeToolStripMenuItem.Name = "windowsFileTimeToolStripMenuItem";
            resources.ApplyResources(this.windowsFileTimeToolStripMenuItem, "windowsFileTimeToolStripMenuItem");
            this.windowsFileTimeToolStripMenuItem.Click += new System.EventHandler(this.windowsFileTimeToolStripMenuItem_Click);
            // 
            // extensionToolStripMenuItem
            // 
            this.extensionToolStripMenuItem.Name = "extensionToolStripMenuItem";
            resources.ApplyResources(this.extensionToolStripMenuItem, "extensionToolStripMenuItem");
            // 
            // editDefaultInspectorsToolStripMenuItem
            // 
            this.editDefaultInspectorsToolStripMenuItem.Name = "editDefaultInspectorsToolStripMenuItem";
            resources.ApplyResources(this.editDefaultInspectorsToolStripMenuItem, "editDefaultInspectorsToolStripMenuItem");
            this.editDefaultInspectorsToolStripMenuItem.Click += new System.EventHandler(this.editDefaultInspectorsToolStripMenuItem_Click);
            // 
            // HexEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "HexEditorControl";
            this.Load += new System.EventHandler(this.HexEditorControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripHex.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Be.Windows.Forms.HexBox hexBox;
        private ListViewExtension listViewInspector;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeInspectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mD5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sHA1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitChecksumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cRC32ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unixTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsFileTimeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripHex;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extensionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPv4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPv6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editDefaultInspectorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPacketToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem applyScriptToolStripMenuItem;
    }
}
