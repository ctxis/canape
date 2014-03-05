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
    partial class PacketLogControl
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
            System.Windows.Forms.ColumnHeader columnId;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketLogControl));
            System.Windows.Forms.ColumnHeader columnTimestamp;
            System.Windows.Forms.ColumnHeader columnTag;
            System.Windows.Forms.ColumnHeader columnNetwork;
            System.Windows.Forms.ColumnHeader columnData;
            System.Windows.Forms.ColumnHeader columnLength;
            System.Windows.Forms.ColumnHeader columnHash;
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastePacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tagToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tagConnectionToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tagToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tagConnectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tagToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tagConnectionToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToBytePacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergePacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libraryNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inParentLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPacketsAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serializedFramesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serializedFramesByTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromPCAPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diffPacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.changeColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allPacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.runScriptStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.listLogPackets = new CANAPE.Controls.ListViewExtension();
            columnId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnNetwork = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHash = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnId
            // 
            resources.ApplyResources(columnId, "columnId");
            // 
            // columnTimestamp
            // 
            resources.ApplyResources(columnTimestamp, "columnTimestamp");
            // 
            // columnTag
            // 
            resources.ApplyResources(columnTag, "columnTag");
            // 
            // columnNetwork
            // 
            resources.ApplyResources(columnNetwork, "columnNetwork");
            // 
            // columnData
            // 
            resources.ApplyResources(columnData, "columnData");
            // 
            // columnLength
            // 
            resources.ApplyResources(columnLength, "columnLength");
            // 
            // columnHash
            // 
            resources.ApplyResources(columnHash, "columnHash");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.copyPacketsToolStripMenuItem,
            this.pastePacketsToolStripMenuItem,
            this.deletePacketsToolStripMenuItem,
            this.clearLogToolStripMenuItem,
            this.cloneToolStripMenuItem,
            this.copyToToolStripMenuItem,
            this.copyAsFilterToolStripMenuItem,
            this.toolStripSeparator1,
            this.viewToolStripMenuItem,
            this.modifyToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.exportPacketsAsToolStripMenuItem,
            this.importPacketsToolStripMenuItem,
            this.diffPacketsToolStripMenuItem,
            this.toolStripSeparator2,
            this.changeColumnsToolStripMenuItem,
            this.changeFontToolStripMenuItem,
            this.autoScrollToolStripMenuItem,
            this.toolStripSeparator3,
            this.findToolStripMenuItem,
            this.readOnlyToolStripMenuItem,
            this.openInWindowToolStripMenuItem,
            this.runScriptStripMenuItem,
            this.extensionsToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip_Closed);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packetToolStripMenuItem,
            this.packetFromFileToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            // 
            // packetToolStripMenuItem
            // 
            this.packetToolStripMenuItem.Name = "packetToolStripMenuItem";
            resources.ApplyResources(this.packetToolStripMenuItem, "packetToolStripMenuItem");
            this.packetToolStripMenuItem.Click += new System.EventHandler(this.addPacketToolStripMenuItem_Click);
            // 
            // packetFromFileToolStripMenuItem
            // 
            this.packetFromFileToolStripMenuItem.Name = "packetFromFileToolStripMenuItem";
            resources.ApplyResources(this.packetFromFileToolStripMenuItem, "packetFromFileToolStripMenuItem");
            this.packetFromFileToolStripMenuItem.Click += new System.EventHandler(this.addPacketFromFileToolStripMenuItem_Click);
            // 
            // copyPacketsToolStripMenuItem
            // 
            this.copyPacketsToolStripMenuItem.Name = "copyPacketsToolStripMenuItem";
            resources.ApplyResources(this.copyPacketsToolStripMenuItem, "copyPacketsToolStripMenuItem");
            this.copyPacketsToolStripMenuItem.Click += new System.EventHandler(this.copyPacketsToolStripMenuItem_Click);
            // 
            // pastePacketsToolStripMenuItem
            // 
            this.pastePacketsToolStripMenuItem.Name = "pastePacketsToolStripMenuItem";
            resources.ApplyResources(this.pastePacketsToolStripMenuItem, "pastePacketsToolStripMenuItem");
            this.pastePacketsToolStripMenuItem.Click += new System.EventHandler(this.pastePacketsToolStripMenuItem_Click);
            // 
            // deletePacketsToolStripMenuItem
            // 
            this.deletePacketsToolStripMenuItem.Name = "deletePacketsToolStripMenuItem";
            resources.ApplyResources(this.deletePacketsToolStripMenuItem, "deletePacketsToolStripMenuItem");
            this.deletePacketsToolStripMenuItem.Click += new System.EventHandler(this.deletePacketsToolStripMenuItem_Click);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            resources.ApplyResources(this.clearLogToolStripMenuItem, "clearLogToolStripMenuItem");
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // cloneToolStripMenuItem
            // 
            this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            resources.ApplyResources(this.cloneToolStripMenuItem, "cloneToolStripMenuItem");
            this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
            // 
            // copyToToolStripMenuItem
            // 
            this.copyToToolStripMenuItem.Name = "copyToToolStripMenuItem";
            resources.ApplyResources(this.copyToToolStripMenuItem, "copyToToolStripMenuItem");
            // 
            // copyAsFilterToolStripMenuItem
            // 
            this.copyAsFilterToolStripMenuItem.Name = "copyAsFilterToolStripMenuItem";
            resources.ApplyResources(this.copyAsFilterToolStripMenuItem, "copyAsFilterToolStripMenuItem");
            this.copyAsFilterToolStripMenuItem.Click += new System.EventHandler(this.copyAsFilterToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statisticsToolStripMenuItem,
            this.logToolStripMenuItem,
            this.hTMLToolStripMenuItem1});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedToolStripMenuItem1,
            this.connectionToolStripMenuItem3,
            this.tagToolStripMenuItem3,
            this.tagConnectionToolStripMenuItem3});
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            resources.ApplyResources(this.statisticsToolStripMenuItem, "statisticsToolStripMenuItem");
            // 
            // selectedToolStripMenuItem1
            // 
            this.selectedToolStripMenuItem1.Name = "selectedToolStripMenuItem1";
            resources.ApplyResources(this.selectedToolStripMenuItem1, "selectedToolStripMenuItem1");
            this.selectedToolStripMenuItem1.Click += new System.EventHandler(this.selectedToolStripMenuItem1_Click);
            // 
            // connectionToolStripMenuItem3
            // 
            this.connectionToolStripMenuItem3.Name = "connectionToolStripMenuItem3";
            resources.ApplyResources(this.connectionToolStripMenuItem3, "connectionToolStripMenuItem3");
            this.connectionToolStripMenuItem3.Click += new System.EventHandler(this.connectionToolStripMenuItem3_Click);
            // 
            // tagToolStripMenuItem3
            // 
            this.tagToolStripMenuItem3.Name = "tagToolStripMenuItem3";
            resources.ApplyResources(this.tagToolStripMenuItem3, "tagToolStripMenuItem3");
            this.tagToolStripMenuItem3.Click += new System.EventHandler(this.tagToolStripMenuItem3_Click);
            // 
            // tagConnectionToolStripMenuItem3
            // 
            this.tagConnectionToolStripMenuItem3.Name = "tagConnectionToolStripMenuItem3";
            resources.ApplyResources(this.tagConnectionToolStripMenuItem3, "tagConnectionToolStripMenuItem3");
            this.tagConnectionToolStripMenuItem3.Click += new System.EventHandler(this.tagConnectionToolStripMenuItem3_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedToolStripMenuItem,
            this.connectionToolStripMenuItem1,
            this.tagToolStripMenuItem1,
            this.tagConnectionToolStripMenuItem1});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            resources.ApplyResources(this.logToolStripMenuItem, "logToolStripMenuItem");
            // 
            // selectedToolStripMenuItem
            // 
            this.selectedToolStripMenuItem.Name = "selectedToolStripMenuItem";
            resources.ApplyResources(this.selectedToolStripMenuItem, "selectedToolStripMenuItem");
            this.selectedToolStripMenuItem.Click += new System.EventHandler(this.selectedToolStripMenuItem_Click);
            // 
            // connectionToolStripMenuItem1
            // 
            this.connectionToolStripMenuItem1.Name = "connectionToolStripMenuItem1";
            resources.ApplyResources(this.connectionToolStripMenuItem1, "connectionToolStripMenuItem1");
            this.connectionToolStripMenuItem1.Click += new System.EventHandler(this.connectionToolStripMenuItem1_Click);
            // 
            // tagToolStripMenuItem1
            // 
            this.tagToolStripMenuItem1.Name = "tagToolStripMenuItem1";
            resources.ApplyResources(this.tagToolStripMenuItem1, "tagToolStripMenuItem1");
            this.tagToolStripMenuItem1.Click += new System.EventHandler(this.tagToolStripMenuItem1_Click);
            // 
            // tagConnectionToolStripMenuItem1
            // 
            this.tagConnectionToolStripMenuItem1.Name = "tagConnectionToolStripMenuItem1";
            resources.ApplyResources(this.tagConnectionToolStripMenuItem1, "tagConnectionToolStripMenuItem1");
            this.tagConnectionToolStripMenuItem1.Click += new System.EventHandler(this.tagConnectionToolStripMenuItem1_Click);
            // 
            // hTMLToolStripMenuItem1
            // 
            this.hTMLToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedToolStripMenuItem2,
            this.connectionToolStripMenuItem2,
            this.tagToolStripMenuItem2,
            this.tagConnectionToolStripMenuItem2});
            this.hTMLToolStripMenuItem1.Name = "hTMLToolStripMenuItem1";
            resources.ApplyResources(this.hTMLToolStripMenuItem1, "hTMLToolStripMenuItem1");
            // 
            // selectedToolStripMenuItem2
            // 
            this.selectedToolStripMenuItem2.Name = "selectedToolStripMenuItem2";
            resources.ApplyResources(this.selectedToolStripMenuItem2, "selectedToolStripMenuItem2");
            this.selectedToolStripMenuItem2.Click += new System.EventHandler(this.selectedToolStripMenuItem2_Click);
            // 
            // connectionToolStripMenuItem2
            // 
            this.connectionToolStripMenuItem2.Name = "connectionToolStripMenuItem2";
            resources.ApplyResources(this.connectionToolStripMenuItem2, "connectionToolStripMenuItem2");
            this.connectionToolStripMenuItem2.Click += new System.EventHandler(this.connectionToolStripMenuItem2_Click);
            // 
            // tagToolStripMenuItem2
            // 
            this.tagToolStripMenuItem2.Name = "tagToolStripMenuItem2";
            resources.ApplyResources(this.tagToolStripMenuItem2, "tagToolStripMenuItem2");
            this.tagToolStripMenuItem2.Click += new System.EventHandler(this.tagToolStripMenuItem2_Click);
            // 
            // tagConnectionToolStripMenuItem2
            // 
            this.tagConnectionToolStripMenuItem2.Name = "tagConnectionToolStripMenuItem2";
            resources.ApplyResources(this.tagConnectionToolStripMenuItem2, "tagConnectionToolStripMenuItem2");
            this.tagConnectionToolStripMenuItem2.Click += new System.EventHandler(this.tagConnectionToolStripMenuItem2_Click);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToBytePacketsToolStripMenuItem,
            this.mergePacketsToolStripMenuItem,
            this.changeColourToolStripMenuItem,
            this.changeTagToolStripMenuItem,
            this.parseWithToolStripMenuItem});
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            resources.ApplyResources(this.modifyToolStripMenuItem, "modifyToolStripMenuItem");
            // 
            // convertToBytePacketsToolStripMenuItem
            // 
            this.convertToBytePacketsToolStripMenuItem.Name = "convertToBytePacketsToolStripMenuItem";
            resources.ApplyResources(this.convertToBytePacketsToolStripMenuItem, "convertToBytePacketsToolStripMenuItem");
            this.convertToBytePacketsToolStripMenuItem.Click += new System.EventHandler(this.convertToBytePacketsToolStripMenuItem_Click);
            // 
            // mergePacketsToolStripMenuItem
            // 
            this.mergePacketsToolStripMenuItem.Name = "mergePacketsToolStripMenuItem";
            resources.ApplyResources(this.mergePacketsToolStripMenuItem, "mergePacketsToolStripMenuItem");
            this.mergePacketsToolStripMenuItem.Click += new System.EventHandler(this.mergePacketsToolStripMenuItem_Click);
            // 
            // changeColourToolStripMenuItem
            // 
            this.changeColourToolStripMenuItem.Name = "changeColourToolStripMenuItem";
            resources.ApplyResources(this.changeColourToolStripMenuItem, "changeColourToolStripMenuItem");
            this.changeColourToolStripMenuItem.Click += new System.EventHandler(this.changeColourToolStripMenuItem_Click);
            // 
            // changeTagToolStripMenuItem
            // 
            this.changeTagToolStripMenuItem.Name = "changeTagToolStripMenuItem";
            resources.ApplyResources(this.changeTagToolStripMenuItem, "changeTagToolStripMenuItem");
            this.changeTagToolStripMenuItem.Click += new System.EventHandler(this.changeTagToolStripMenuItem_Click);
            // 
            // parseWithToolStripMenuItem
            // 
            this.parseWithToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.libraryNodeToolStripMenuItem,
            this.scriptToolStripMenuItem});
            this.parseWithToolStripMenuItem.Name = "parseWithToolStripMenuItem";
            resources.ApplyResources(this.parseWithToolStripMenuItem, "parseWithToolStripMenuItem");
            // 
            // libraryNodeToolStripMenuItem
            // 
            this.libraryNodeToolStripMenuItem.Name = "libraryNodeToolStripMenuItem";
            resources.ApplyResources(this.libraryNodeToolStripMenuItem, "libraryNodeToolStripMenuItem");
            this.libraryNodeToolStripMenuItem.Click += new System.EventHandler(this.libraryNodeToolStripMenuItem_Click);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            resources.ApplyResources(this.scriptToolStripMenuItem, "scriptToolStripMenuItem");
            this.scriptToolStripMenuItem.Click += new System.EventHandler(this.scriptToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.connectionToolStripMenuItem,
            this.tagToolStripMenuItem,
            this.tagConnectionToolStripMenuItem,
            this.inParentLogToolStripMenuItem});
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            resources.ApplyResources(this.selectToolStripMenuItem, "selectToolStripMenuItem");
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            resources.ApplyResources(this.allToolStripMenuItem, "allToolStripMenuItem");
            this.allToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            resources.ApplyResources(this.connectionToolStripMenuItem, "connectionToolStripMenuItem");
            this.connectionToolStripMenuItem.Click += new System.EventHandler(this.connectionToolStripMenuItem_Click);
            // 
            // tagToolStripMenuItem
            // 
            this.tagToolStripMenuItem.Name = "tagToolStripMenuItem";
            resources.ApplyResources(this.tagToolStripMenuItem, "tagToolStripMenuItem");
            this.tagToolStripMenuItem.Click += new System.EventHandler(this.tagToolStripMenuItem_Click);
            // 
            // tagConnectionToolStripMenuItem
            // 
            this.tagConnectionToolStripMenuItem.Name = "tagConnectionToolStripMenuItem";
            resources.ApplyResources(this.tagConnectionToolStripMenuItem, "tagConnectionToolStripMenuItem");
            this.tagConnectionToolStripMenuItem.Click += new System.EventHandler(this.tagConnectionToolStripMenuItem_Click);
            // 
            // inParentLogToolStripMenuItem
            // 
            this.inParentLogToolStripMenuItem.Name = "inParentLogToolStripMenuItem";
            resources.ApplyResources(this.inParentLogToolStripMenuItem, "inParentLogToolStripMenuItem");
            this.inParentLogToolStripMenuItem.Click += new System.EventHandler(this.selectInParentLogToolStripMenuItem_Click);
            // 
            // exportPacketsAsToolStripMenuItem
            // 
            this.exportPacketsAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textToolStripMenuItem,
            this.hexToolStripMenuItem,
            this.binaryToolStripMenuItem,
            this.hTMLToolStripMenuItem,
            this.serializedFramesToolStripMenuItem,
            this.serializedFramesByTagToolStripMenuItem});
            this.exportPacketsAsToolStripMenuItem.Name = "exportPacketsAsToolStripMenuItem";
            resources.ApplyResources(this.exportPacketsAsToolStripMenuItem, "exportPacketsAsToolStripMenuItem");
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            resources.ApplyResources(this.textToolStripMenuItem, "textToolStripMenuItem");
            this.textToolStripMenuItem.Click += new System.EventHandler(this.textToolStripMenuItem_Click);
            // 
            // hexToolStripMenuItem
            // 
            this.hexToolStripMenuItem.Name = "hexToolStripMenuItem";
            resources.ApplyResources(this.hexToolStripMenuItem, "hexToolStripMenuItem");
            this.hexToolStripMenuItem.Click += new System.EventHandler(this.hexToolStripMenuItem_Click);
            // 
            // binaryToolStripMenuItem
            // 
            this.binaryToolStripMenuItem.Name = "binaryToolStripMenuItem";
            resources.ApplyResources(this.binaryToolStripMenuItem, "binaryToolStripMenuItem");
            this.binaryToolStripMenuItem.Click += new System.EventHandler(this.binaryToolStripMenuItem_Click);
            // 
            // hTMLToolStripMenuItem
            // 
            this.hTMLToolStripMenuItem.Name = "hTMLToolStripMenuItem";
            resources.ApplyResources(this.hTMLToolStripMenuItem, "hTMLToolStripMenuItem");
            this.hTMLToolStripMenuItem.Click += new System.EventHandler(this.hTMLToolStripMenuItem_Click);
            // 
            // serializedFramesToolStripMenuItem
            // 
            this.serializedFramesToolStripMenuItem.Name = "serializedFramesToolStripMenuItem";
            resources.ApplyResources(this.serializedFramesToolStripMenuItem, "serializedFramesToolStripMenuItem");
            this.serializedFramesToolStripMenuItem.Click += new System.EventHandler(this.serializedFramesToolStripMenuItem_Click);
            // 
            // serializedFramesByTagToolStripMenuItem
            // 
            this.serializedFramesByTagToolStripMenuItem.Name = "serializedFramesByTagToolStripMenuItem";
            resources.ApplyResources(this.serializedFramesByTagToolStripMenuItem, "serializedFramesByTagToolStripMenuItem");
            this.serializedFramesByTagToolStripMenuItem.Click += new System.EventHandler(this.serializedFramesByTagToolStripMenuItem_Click);
            // 
            // importPacketsToolStripMenuItem
            // 
            this.importPacketsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromFileToolStripMenuItem,
            this.fromPCAPToolStripMenuItem});
            this.importPacketsToolStripMenuItem.Name = "importPacketsToolStripMenuItem";
            resources.ApplyResources(this.importPacketsToolStripMenuItem, "importPacketsToolStripMenuItem");
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            resources.ApplyResources(this.fromFileToolStripMenuItem, "fromFileToolStripMenuItem");
            this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fromFileToolStripMenuItem_Click);
            // 
            // fromPCAPToolStripMenuItem
            // 
            this.fromPCAPToolStripMenuItem.Name = "fromPCAPToolStripMenuItem";
            resources.ApplyResources(this.fromPCAPToolStripMenuItem, "fromPCAPToolStripMenuItem");
            this.fromPCAPToolStripMenuItem.Click += new System.EventHandler(this.fromPCAPToolStripMenuItem_Click);
            // 
            // diffPacketsToolStripMenuItem
            // 
            this.diffPacketsToolStripMenuItem.Name = "diffPacketsToolStripMenuItem";
            resources.ApplyResources(this.diffPacketsToolStripMenuItem, "diffPacketsToolStripMenuItem");
            this.diffPacketsToolStripMenuItem.Click += new System.EventHandler(this.diffPacketsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // changeColumnsToolStripMenuItem
            // 
            this.changeColumnsToolStripMenuItem.Name = "changeColumnsToolStripMenuItem";
            resources.ApplyResources(this.changeColumnsToolStripMenuItem, "changeColumnsToolStripMenuItem");
            this.changeColumnsToolStripMenuItem.Click += new System.EventHandler(this.changeColumnsToolStripMenuItem_Click);
            // 
            // changeFontToolStripMenuItem
            // 
            this.changeFontToolStripMenuItem.Name = "changeFontToolStripMenuItem";
            resources.ApplyResources(this.changeFontToolStripMenuItem, "changeFontToolStripMenuItem");
            this.changeFontToolStripMenuItem.Click += new System.EventHandler(this.changeFontToolStripMenuItem_Click);
            // 
            // autoScrollToolStripMenuItem
            // 
            this.autoScrollToolStripMenuItem.CheckOnClick = true;
            this.autoScrollToolStripMenuItem.Name = "autoScrollToolStripMenuItem";
            resources.ApplyResources(this.autoScrollToolStripMenuItem, "autoScrollToolStripMenuItem");
            this.autoScrollToolStripMenuItem.Click += new System.EventHandler(this.autoScrollToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            resources.ApplyResources(this.findToolStripMenuItem, "findToolStripMenuItem");
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // readOnlyToolStripMenuItem
            // 
            this.readOnlyToolStripMenuItem.Name = "readOnlyToolStripMenuItem";
            resources.ApplyResources(this.readOnlyToolStripMenuItem, "readOnlyToolStripMenuItem");
            this.readOnlyToolStripMenuItem.Click += new System.EventHandler(this.readOnlyToolStripMenuItem_Click);
            // 
            // openInWindowToolStripMenuItem
            // 
            this.openInWindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allPacketsToolStripMenuItem,
            this.selectedToolStripMenuItem3,
            this.connectionToolStripMenuItem4,
            this.connectionTagToolStripMenuItem,
            this.tagToolStripMenuItem4});
            this.openInWindowToolStripMenuItem.Name = "openInWindowToolStripMenuItem";
            resources.ApplyResources(this.openInWindowToolStripMenuItem, "openInWindowToolStripMenuItem");
            // 
            // allPacketsToolStripMenuItem
            // 
            this.allPacketsToolStripMenuItem.Name = "allPacketsToolStripMenuItem";
            resources.ApplyResources(this.allPacketsToolStripMenuItem, "allPacketsToolStripMenuItem");
            this.allPacketsToolStripMenuItem.Click += new System.EventHandler(this.allPacketsToolStripMenuItem_Click);
            // 
            // selectedToolStripMenuItem3
            // 
            this.selectedToolStripMenuItem3.Name = "selectedToolStripMenuItem3";
            resources.ApplyResources(this.selectedToolStripMenuItem3, "selectedToolStripMenuItem3");
            this.selectedToolStripMenuItem3.Click += new System.EventHandler(this.selectedToolStripMenuItem3_Click);
            // 
            // connectionToolStripMenuItem4
            // 
            this.connectionToolStripMenuItem4.Name = "connectionToolStripMenuItem4";
            resources.ApplyResources(this.connectionToolStripMenuItem4, "connectionToolStripMenuItem4");
            this.connectionToolStripMenuItem4.Click += new System.EventHandler(this.connectionToolStripMenuItem4_Click);
            // 
            // connectionTagToolStripMenuItem
            // 
            this.connectionTagToolStripMenuItem.Name = "connectionTagToolStripMenuItem";
            resources.ApplyResources(this.connectionTagToolStripMenuItem, "connectionTagToolStripMenuItem");
            this.connectionTagToolStripMenuItem.Click += new System.EventHandler(this.connectionTagToolStripMenuItem_Click);
            // 
            // tagToolStripMenuItem4
            // 
            this.tagToolStripMenuItem4.Name = "tagToolStripMenuItem4";
            resources.ApplyResources(this.tagToolStripMenuItem4, "tagToolStripMenuItem4");
            this.tagToolStripMenuItem4.Click += new System.EventHandler(this.tagToolStripMenuItem4_Click);
            // 
            // runScriptStripMenuItem
            // 
            this.runScriptStripMenuItem.Name = "runScriptStripMenuItem";
            resources.ApplyResources(this.runScriptStripMenuItem, "runScriptStripMenuItem");
            // 
            // extensionsToolStripMenuItem
            // 
            this.extensionsToolStripMenuItem.Name = "extensionsToolStripMenuItem";
            resources.ApplyResources(this.extensionsToolStripMenuItem, "extensionsToolStripMenuItem");
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // listLogPackets
            // 
            this.listLogPackets.AllowColumnReorder = true;
            this.listLogPackets.AllowDrop = true;
            this.listLogPackets.AutoScrollList = false;
            this.listLogPackets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnId,
            columnTimestamp,
            columnTag,
            columnNetwork,
            columnData,
            columnLength,
            columnHash});
            this.listLogPackets.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.listLogPackets, "listLogPackets");
            this.listLogPackets.FullRowSelect = true;
            this.listLogPackets.GridLines = true;
            this.listLogPackets.HideSelection = false;
            this.listLogPackets.Name = "listLogPackets";
            this.listLogPackets.UseCompatibleStateImageBehavior = false;
            this.listLogPackets.View = System.Windows.Forms.View.Details;
            this.listLogPackets.VirtualMode = true;
            this.listLogPackets.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listLogPackets_ColumnClick);
            this.listLogPackets.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.listLogPackets_ColumnReordered);
            this.listLogPackets.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.listLogPackets_ColumnWidthChanged);
            this.listLogPackets.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listLogPackets_ItemDrag);
            this.listLogPackets.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listLogPackets_RetrieveVirtualItem);
            this.listLogPackets.Click += new System.EventHandler(this.listLogPackets_Click);
            this.listLogPackets.DragDrop += new System.Windows.Forms.DragEventHandler(this.listLogPackets_DragDrop);
            this.listLogPackets.DragOver += new System.Windows.Forms.DragEventHandler(this.listLogPackets_DragOver);
            this.listLogPackets.DoubleClick += new System.EventHandler(this.listLogPackets_DoubleClick);
            this.listLogPackets.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listLogPackets_KeyDown);
            this.listLogPackets.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listLogPackets_MouseDown);
            // 
            // PacketLogControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listLogPackets);
            this.Name = "PacketLogControl";
            this.Load += new System.EventHandler(this.LogPacketControl_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewExtension listLogPackets;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyPacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastePacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportPacketsAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serializedFramesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serializedFramesByTagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexToolStripMenuItem;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStripMenuItem autoScrollToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diffPacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromPCAPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToBytePacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergePacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeColourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeTagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inParentLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packetFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseWithToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libraryNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extensionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectedToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tagToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tagConnectionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tagToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tagConnectionToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem selectedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tagToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tagConnectionToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem openInWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allPacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem connectionTagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagToolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem changeColumnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem runScriptStripMenuItem;
    }
}
