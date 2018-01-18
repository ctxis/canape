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

namespace CANAPE.Controls.DocumentEditors
{
    partial class NetGraphDocumentControl
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetGraphDocumentControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.netEditor = new CANAPE.Controls.GraphEditor.GraphEditorControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverEndpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientEndpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editPacketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetLoggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netGraphContainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decisionNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delayNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dynamicNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libraryNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerSectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sSLLayerSectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genericLayerSectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleEnableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centreGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centreOnNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip);
            this.splitContainer1.Panel2.Controls.Add(this.netEditor);
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.Name = "propertyGrid";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomOut});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // netEditor
            // 
            this.netEditor.AllowBidirectionLines = false;
            this.netEditor.BackColor = System.Drawing.SystemColors.Window;
            this.netEditor.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.netEditor, "netEditor");
            this.netEditor.DrawDropShadow = true;
            this.netEditor.DropShadowOffsetX = 5F;
            this.netEditor.DropShadowOffsetY = 5F;
            this.netEditor.Name = "netEditor";
            this.netEditor.NodeDeleted += new System.EventHandler<CANAPE.Controls.GraphEditor.NodeDeletedEventArgs>(this.netEditor_NodeDeleted);
            this.netEditor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.netEditor_MouseDoubleClick);
            this.netEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.netEditor_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNodeToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem,
            this.toggleEnableToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.autoLayoutToolStripMenuItem,
            this.centreGraphToolStripMenuItem,
            this.centreOnNodeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // addNodeToolStripMenuItem
            // 
            this.addNodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverEndpointToolStripMenuItem,
            this.clientEndpointToolStripMenuItem,
            this.toolStripSeparator2,
            this.editPacketToolStripMenuItem,
            this.packetLoggerToolStripMenuItem,
            this.nopToolStripMenuItem,
            this.netGraphContainerToolStripMenuItem,
            this.decisionNodeToolStripMenuItem,
            this.switchNodeToolStripMenuItem,
            this.delayNodeToolStripMenuItem,
            this.toolStripSeparator1,
            this.dynamicNodeToolStripMenuItem,
            this.libraryNodeToolStripMenuItem,
            this.layerSectionsToolStripMenuItem});
            this.addNodeToolStripMenuItem.Name = "addNodeToolStripMenuItem";
            resources.ApplyResources(this.addNodeToolStripMenuItem, "addNodeToolStripMenuItem");
            // 
            // serverEndpointToolStripMenuItem
            // 
            this.serverEndpointToolStripMenuItem.Name = "serverEndpointToolStripMenuItem";
            resources.ApplyResources(this.serverEndpointToolStripMenuItem, "serverEndpointToolStripMenuItem");
            this.serverEndpointToolStripMenuItem.Tag = "server";
            this.serverEndpointToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // clientEndpointToolStripMenuItem
            // 
            this.clientEndpointToolStripMenuItem.Name = "clientEndpointToolStripMenuItem";
            resources.ApplyResources(this.clientEndpointToolStripMenuItem, "clientEndpointToolStripMenuItem");
            this.clientEndpointToolStripMenuItem.Tag = "client";
            this.clientEndpointToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // editPacketToolStripMenuItem
            // 
            this.editPacketToolStripMenuItem.Name = "editPacketToolStripMenuItem";
            resources.ApplyResources(this.editPacketToolStripMenuItem, "editPacketToolStripMenuItem");
            this.editPacketToolStripMenuItem.Tag = "editnode";
            this.editPacketToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // packetLoggerToolStripMenuItem
            // 
            this.packetLoggerToolStripMenuItem.Name = "packetLoggerToolStripMenuItem";
            resources.ApplyResources(this.packetLoggerToolStripMenuItem, "packetLoggerToolStripMenuItem");
            this.packetLoggerToolStripMenuItem.Tag = "lognode";
            this.packetLoggerToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // nopToolStripMenuItem
            // 
            this.nopToolStripMenuItem.Name = "nopToolStripMenuItem";
            resources.ApplyResources(this.nopToolStripMenuItem, "nopToolStripMenuItem");
            this.nopToolStripMenuItem.Tag = "dirnode";
            this.nopToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // netGraphContainerToolStripMenuItem
            // 
            this.netGraphContainerToolStripMenuItem.Name = "netGraphContainerToolStripMenuItem";
            resources.ApplyResources(this.netGraphContainerToolStripMenuItem, "netGraphContainerToolStripMenuItem");
            this.netGraphContainerToolStripMenuItem.Tag = "netgraph";
            this.netGraphContainerToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // decisionNodeToolStripMenuItem
            // 
            this.decisionNodeToolStripMenuItem.Name = "decisionNodeToolStripMenuItem";
            resources.ApplyResources(this.decisionNodeToolStripMenuItem, "decisionNodeToolStripMenuItem");
            this.decisionNodeToolStripMenuItem.Tag = "ifnode";
            this.decisionNodeToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // switchNodeToolStripMenuItem
            // 
            this.switchNodeToolStripMenuItem.Name = "switchNodeToolStripMenuItem";
            resources.ApplyResources(this.switchNodeToolStripMenuItem, "switchNodeToolStripMenuItem");
            this.switchNodeToolStripMenuItem.Tag = "switchnode";
            this.switchNodeToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // delayNodeToolStripMenuItem
            // 
            this.delayNodeToolStripMenuItem.Name = "delayNodeToolStripMenuItem";
            resources.ApplyResources(this.delayNodeToolStripMenuItem, "delayNodeToolStripMenuItem");
            this.delayNodeToolStripMenuItem.Tag = "delaynode";
            this.delayNodeToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // dynamicNodeToolStripMenuItem
            // 
            this.dynamicNodeToolStripMenuItem.Name = "dynamicNodeToolStripMenuItem";
            resources.ApplyResources(this.dynamicNodeToolStripMenuItem, "dynamicNodeToolStripMenuItem");
            this.dynamicNodeToolStripMenuItem.Tag = "dynnode";
            this.dynamicNodeToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // libraryNodeToolStripMenuItem
            // 
            this.libraryNodeToolStripMenuItem.Name = "libraryNodeToolStripMenuItem";
            resources.ApplyResources(this.libraryNodeToolStripMenuItem, "libraryNodeToolStripMenuItem");
            this.libraryNodeToolStripMenuItem.Tag = "libnode";
            this.libraryNodeToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // layerSectionsToolStripMenuItem
            // 
            this.layerSectionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sSLLayerSectionToolStripMenuItem,
            this.genericLayerSectionToolStripMenuItem});
            this.layerSectionsToolStripMenuItem.Name = "layerSectionsToolStripMenuItem";
            resources.ApplyResources(this.layerSectionsToolStripMenuItem, "layerSectionsToolStripMenuItem");
            // 
            // sSLLayerSectionToolStripMenuItem
            // 
            this.sSLLayerSectionToolStripMenuItem.Name = "sSLLayerSectionToolStripMenuItem";
            resources.ApplyResources(this.sSLLayerSectionToolStripMenuItem, "sSLLayerSectionToolStripMenuItem");
            this.sSLLayerSectionToolStripMenuItem.Tag = "ssllayersection";
            this.sSLLayerSectionToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // genericLayerSectionToolStripMenuItem
            // 
            this.genericLayerSectionToolStripMenuItem.Name = "genericLayerSectionToolStripMenuItem";
            resources.ApplyResources(this.genericLayerSectionToolStripMenuItem, "genericLayerSectionToolStripMenuItem");
            this.genericLayerSectionToolStripMenuItem.Tag = "layersection";
            this.genericLayerSectionToolStripMenuItem.Click += new System.EventHandler(this.AddNode_Click);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            resources.ApplyResources(this.deleteSelectedToolStripMenuItem, "deleteSelectedToolStripMenuItem");
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedToolStripMenuItem_Click);
            // 
            // toggleEnableToolStripMenuItem
            // 
            this.toggleEnableToolStripMenuItem.Name = "toggleEnableToolStripMenuItem";
            resources.ApplyResources(this.toggleEnableToolStripMenuItem, "toggleEnableToolStripMenuItem");
            this.toggleEnableToolStripMenuItem.Click += new System.EventHandler(this.toggleEnableToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyFiltersToolStripMenuItem,
            this.pasteFiltersToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            resources.ApplyResources(this.filtersToolStripMenuItem, "filtersToolStripMenuItem");
            // 
            // copyFiltersToolStripMenuItem
            // 
            this.copyFiltersToolStripMenuItem.Name = "copyFiltersToolStripMenuItem";
            resources.ApplyResources(this.copyFiltersToolStripMenuItem, "copyFiltersToolStripMenuItem");
            this.copyFiltersToolStripMenuItem.Click += new System.EventHandler(this.copyFiltersToolStripMenuItem_Click);
            // 
            // pasteFiltersToolStripMenuItem
            // 
            this.pasteFiltersToolStripMenuItem.Name = "pasteFiltersToolStripMenuItem";
            resources.ApplyResources(this.pasteFiltersToolStripMenuItem, "pasteFiltersToolStripMenuItem");
            this.pasteFiltersToolStripMenuItem.Click += new System.EventHandler(this.pasteFiltersToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPropertiesToolStripMenuItem,
            this.pastePropertiesToolStripMenuItem});
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
            // 
            // copyPropertiesToolStripMenuItem
            // 
            this.copyPropertiesToolStripMenuItem.Name = "copyPropertiesToolStripMenuItem";
            resources.ApplyResources(this.copyPropertiesToolStripMenuItem, "copyPropertiesToolStripMenuItem");
            this.copyPropertiesToolStripMenuItem.Click += new System.EventHandler(this.copyPropertiesToolStripMenuItem_Click);
            // 
            // pastePropertiesToolStripMenuItem
            // 
            this.pastePropertiesToolStripMenuItem.Name = "pastePropertiesToolStripMenuItem";
            resources.ApplyResources(this.pastePropertiesToolStripMenuItem, "pastePropertiesToolStripMenuItem");
            this.pastePropertiesToolStripMenuItem.Click += new System.EventHandler(this.pastePropertiesToolStripMenuItem_Click);
            // 
            // autoLayoutToolStripMenuItem
            // 
            this.autoLayoutToolStripMenuItem.Name = "autoLayoutToolStripMenuItem";
            resources.ApplyResources(this.autoLayoutToolStripMenuItem, "autoLayoutToolStripMenuItem");
            this.autoLayoutToolStripMenuItem.Click += new System.EventHandler(this.autoLayoutToolStripMenuItem_Click);
            // 
            // centreGraphToolStripMenuItem
            // 
            this.centreGraphToolStripMenuItem.Name = "centreGraphToolStripMenuItem";
            resources.ApplyResources(this.centreGraphToolStripMenuItem, "centreGraphToolStripMenuItem");
            this.centreGraphToolStripMenuItem.Click += new System.EventHandler(this.centreGraphToolStripMenuItem_Click);
            // 
            // centreOnNodeToolStripMenuItem
            // 
            this.centreOnNodeToolStripMenuItem.Name = "centreOnNodeToolStripMenuItem";
            resources.ApplyResources(this.centreOnNodeToolStripMenuItem, "centreOnNodeToolStripMenuItem");
            this.centreOnNodeToolStripMenuItem.Click += new System.EventHandler(this.centreOnNodeToolStripMenuItem_Click);
            // 
            // NetGraphDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "NetGraphDocumentControl";
            this.Load += new System.EventHandler(this.NodeGraphForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPacketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packetLoggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dynamicNodeToolStripMenuItem;
        private CANAPE.Controls.GraphEditor.GraphEditorControl netEditor;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverEndpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientEndpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libraryNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastePropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleEnableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netGraphContainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decisionNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripMenuItem layerSectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sSLLayerSectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delayNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genericLayerSectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centreGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centreOnNodeToolStripMenuItem;
    }
}