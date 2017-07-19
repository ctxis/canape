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
    partial class ActiveNetgraphControl
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
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActiveNetgraphControl));
            System.Windows.Forms.ColumnHeader columnHeaderName;
            System.Windows.Forms.ColumnHeader columnHeaderEnabled;
            System.Windows.Forms.ColumnHeader columnHeaderShutdown;
            System.Windows.Forms.ColumnHeader columnHeaderInput;
            System.Windows.Forms.ColumnHeader columnHeaderOutput;
            this.timerUpdateGraph = new System.Windows.Forms.Timer(this.components);
            this.comboBoxNetGraph = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pastePacketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleEnableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxShowHidden = new System.Windows.Forms.CheckBox();
            this.listViewNetGraph = new CANAPE.Controls.ListViewExtension();
            this.columnHeaderBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.metaEditorControl = new CANAPE.Controls.MetaEditorControl();
            this.propertyBagViewerControl = new CANAPE.Controls.PropertyBagViewerControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGraph = new System.Windows.Forms.TabPage();
            this.tabPageMeta = new System.Windows.Forms.TabPage();
            this.tabPageProperties = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderShutdown = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderInput = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderOutput = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageGraph.SuspendLayout();
            this.tabPageMeta.SuspendLayout();
            this.tabPageProperties.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderEnabled
            // 
            resources.ApplyResources(columnHeaderEnabled, "columnHeaderEnabled");
            // 
            // columnHeaderShutdown
            // 
            resources.ApplyResources(columnHeaderShutdown, "columnHeaderShutdown");
            // 
            // columnHeaderInput
            // 
            resources.ApplyResources(columnHeaderInput, "columnHeaderInput");
            // 
            // columnHeaderOutput
            // 
            resources.ApplyResources(columnHeaderOutput, "columnHeaderOutput");
            // 
            // timerUpdateGraph
            // 
            this.timerUpdateGraph.Interval = 1000;
            this.timerUpdateGraph.Tick += new System.EventHandler(this.timerUpdateGraph_Tick);
            // 
            // comboBoxNetGraph
            // 
            resources.ApplyResources(this.comboBoxNetGraph, "comboBoxNetGraph");
            this.comboBoxNetGraph.DisplayMember = "NetworkDescription";
            this.comboBoxNetGraph.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNetGraph.FormattingEnabled = true;
            this.comboBoxNetGraph.Name = "comboBoxNetGraph";
            this.toolTip.SetToolTip(this.comboBoxNetGraph, resources.GetString("comboBoxNetGraph.ToolTip"));
            this.comboBoxNetGraph.DropDown += new System.EventHandler(this.comboBoxNetGraph_DropDown);
            this.comboBoxNetGraph.SelectedIndexChanged += new System.EventHandler(this.comboBoxNetGraph_SelectedIndexChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pastePacketsToolStripMenuItem,
            this.toggleEnableToolStripMenuItem,
            this.shutdownNodeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // pastePacketsToolStripMenuItem
            // 
            this.pastePacketsToolStripMenuItem.Name = "pastePacketsToolStripMenuItem";
            resources.ApplyResources(this.pastePacketsToolStripMenuItem, "pastePacketsToolStripMenuItem");
            this.pastePacketsToolStripMenuItem.Click += new System.EventHandler(this.pastePacketsToolStripMenuItem_Click);
            // 
            // toggleEnableToolStripMenuItem
            // 
            this.toggleEnableToolStripMenuItem.Name = "toggleEnableToolStripMenuItem";
            resources.ApplyResources(this.toggleEnableToolStripMenuItem, "toggleEnableToolStripMenuItem");
            this.toggleEnableToolStripMenuItem.Click += new System.EventHandler(this.toggleEnableToolStripMenuItem_Click);
            // 
            // shutdownNodeToolStripMenuItem
            // 
            this.shutdownNodeToolStripMenuItem.Name = "shutdownNodeToolStripMenuItem";
            resources.ApplyResources(this.shutdownNodeToolStripMenuItem, "shutdownNodeToolStripMenuItem");
            this.shutdownNodeToolStripMenuItem.Click += new System.EventHandler(this.shutdownNodeToolStripMenuItem_Click);
            // 
            // checkBoxShowHidden
            // 
            resources.ApplyResources(this.checkBoxShowHidden, "checkBoxShowHidden");
            this.checkBoxShowHidden.Name = "checkBoxShowHidden";
            this.toolTip.SetToolTip(this.checkBoxShowHidden, resources.GetString("checkBoxShowHidden.ToolTip"));
            this.checkBoxShowHidden.UseVisualStyleBackColor = true;
            this.checkBoxShowHidden.CheckedChanged += new System.EventHandler(this.checkBoxShowHidden_CheckedChanged);
            // 
            // listViewNetGraph
            // 
            this.listViewNetGraph.AutoScrollList = false;
            this.listViewNetGraph.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderName,
            columnHeaderEnabled,
            columnHeaderShutdown,
            columnHeaderInput,
            columnHeaderOutput,
            this.columnHeaderBytes});
            this.listViewNetGraph.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.listViewNetGraph, "listViewNetGraph");
            this.listViewNetGraph.FullRowSelect = true;
            this.listViewNetGraph.MultiSelect = false;
            this.listViewNetGraph.Name = "listViewNetGraph";
            this.listViewNetGraph.UseCompatibleStateImageBehavior = false;
            this.listViewNetGraph.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderBytes
            // 
            resources.ApplyResources(this.columnHeaderBytes, "columnHeaderBytes");
            // 
            // metaEditorControl
            // 
            resources.ApplyResources(this.metaEditorControl, "metaEditorControl");
            this.metaEditorControl.Name = "metaEditorControl";
            // 
            // propertyBagViewerControl
            // 
            resources.ApplyResources(this.propertyBagViewerControl, "propertyBagViewerControl");
            this.propertyBagViewerControl.Name = "propertyBagViewerControl";
            // 
            // tabControl
            // 
            this.tableLayoutPanel.SetColumnSpan(this.tabControl, 3);
            this.tabControl.Controls.Add(this.tabPageGraph);
            this.tabControl.Controls.Add(this.tabPageMeta);
            this.tabControl.Controls.Add(this.tabPageProperties);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageGraph
            // 
            this.tabPageGraph.Controls.Add(this.listViewNetGraph);
            resources.ApplyResources(this.tabPageGraph, "tabPageGraph");
            this.tabPageGraph.Name = "tabPageGraph";
            this.tabPageGraph.UseVisualStyleBackColor = true;
            // 
            // tabPageMeta
            // 
            this.tabPageMeta.Controls.Add(this.metaEditorControl);
            resources.ApplyResources(this.tabPageMeta, "tabPageMeta");
            this.tabPageMeta.Name = "tabPageMeta";
            this.tabPageMeta.UseVisualStyleBackColor = true;
            // 
            // tabPageProperties
            // 
            this.tabPageProperties.Controls.Add(this.propertyBagViewerControl);
            resources.ApplyResources(this.tabPageProperties, "tabPageProperties");
            this.tabPageProperties.Name = "tabPageProperties";
            this.tabPageProperties.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.comboBoxNetGraph, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.checkBoxShowHidden, 2, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // ActiveNetgraphControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ActiveNetgraphControl";
            this.contextMenuStrip.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageGraph.ResumeLayout(false);
            this.tabPageMeta.ResumeLayout(false);
            this.tabPageProperties.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewExtension listViewNetGraph;
        private System.Windows.Forms.Timer timerUpdateGraph;
        private System.Windows.Forms.ComboBox comboBoxNetGraph;
        private System.Windows.Forms.ColumnHeader columnHeaderBytes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem pastePacketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleEnableToolStripMenuItem;
        private MetaEditorControl metaEditorControl;
        private System.Windows.Forms.CheckBox checkBoxShowHidden;
        private PropertyBagViewerControl propertyBagViewerControl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem shutdownNodeToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TabPage tabPageGraph;
        private System.Windows.Forms.TabPage tabPageMeta;
        private System.Windows.Forms.TabPage tabPageProperties;
    }
}
