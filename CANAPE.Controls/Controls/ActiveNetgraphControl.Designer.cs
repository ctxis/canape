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
            System.Windows.Forms.Label label2;
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
            this.comboBoxView = new System.Windows.Forms.ComboBox();
            this.listViewNetGraph = new CANAPE.Controls.ListViewExtension();
            this.columnHeaderBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.metaEditorControl = new CANAPE.Controls.MetaEditorControl();
            this.propertyBagViewerControl = new CANAPE.Controls.PropertyBagViewerControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderShutdown = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderInput = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderOutput = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            this.toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            this.toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
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
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pastePacketsToolStripMenuItem,
            this.toggleEnableToolStripMenuItem,
            this.shutdownNodeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.toolTip.SetToolTip(this.contextMenuStrip, resources.GetString("contextMenuStrip.ToolTip"));
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // pastePacketsToolStripMenuItem
            // 
            resources.ApplyResources(this.pastePacketsToolStripMenuItem, "pastePacketsToolStripMenuItem");
            this.pastePacketsToolStripMenuItem.Name = "pastePacketsToolStripMenuItem";
            this.pastePacketsToolStripMenuItem.Click += new System.EventHandler(this.pastePacketsToolStripMenuItem_Click);
            // 
            // toggleEnableToolStripMenuItem
            // 
            resources.ApplyResources(this.toggleEnableToolStripMenuItem, "toggleEnableToolStripMenuItem");
            this.toggleEnableToolStripMenuItem.Name = "toggleEnableToolStripMenuItem";
            this.toggleEnableToolStripMenuItem.Click += new System.EventHandler(this.toggleEnableToolStripMenuItem_Click);
            // 
            // shutdownNodeToolStripMenuItem
            // 
            resources.ApplyResources(this.shutdownNodeToolStripMenuItem, "shutdownNodeToolStripMenuItem");
            this.shutdownNodeToolStripMenuItem.Name = "shutdownNodeToolStripMenuItem";
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
            // comboBoxView
            // 
            resources.ApplyResources(this.comboBoxView, "comboBoxView");
            this.comboBoxView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxView.FormattingEnabled = true;
            this.comboBoxView.Items.AddRange(new object[] {
            resources.GetString("comboBoxView.Items"),
            resources.GetString("comboBoxView.Items1"),
            resources.GetString("comboBoxView.Items2")});
            this.comboBoxView.Name = "comboBoxView";
            this.toolTip.SetToolTip(this.comboBoxView, resources.GetString("comboBoxView.ToolTip"));
            this.comboBoxView.SelectedIndexChanged += new System.EventHandler(this.comboBoxView_SelectedIndexChanged);
            // 
            // listViewNetGraph
            // 
            resources.ApplyResources(this.listViewNetGraph, "listViewNetGraph");
            this.listViewNetGraph.AutoScrollList = false;
            this.listViewNetGraph.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderName,
            columnHeaderEnabled,
            columnHeaderShutdown,
            columnHeaderInput,
            columnHeaderOutput,
            this.columnHeaderBytes});
            this.listViewNetGraph.ContextMenuStrip = this.contextMenuStrip;
            this.listViewNetGraph.FullRowSelect = true;
            this.listViewNetGraph.MultiSelect = false;
            this.listViewNetGraph.Name = "listViewNetGraph";
            this.toolTip.SetToolTip(this.listViewNetGraph, resources.GetString("listViewNetGraph.ToolTip"));
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
            this.toolTip.SetToolTip(this.metaEditorControl, resources.GetString("metaEditorControl.ToolTip"));
            // 
            // propertyBagViewerControl
            // 
            resources.ApplyResources(this.propertyBagViewerControl, "propertyBagViewerControl");
            this.propertyBagViewerControl.Name = "propertyBagViewerControl";
            this.toolTip.SetToolTip(this.propertyBagViewerControl, resources.GetString("propertyBagViewerControl.ToolTip"));
            // 
            // ActiveNetgraphControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(label2);
            this.Controls.Add(this.comboBoxView);
            this.Controls.Add(this.propertyBagViewerControl);
            this.Controls.Add(this.checkBoxShowHidden);
            this.Controls.Add(label1);
            this.Controls.Add(this.listViewNetGraph);
            this.Controls.Add(this.comboBoxNetGraph);
            this.Controls.Add(this.metaEditorControl);
            this.Name = "ActiveNetgraphControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.ActiveNetgraphControl_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ComboBox comboBoxView;
        private PropertyBagViewerControl propertyBagViewerControl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem shutdownNodeToolStripMenuItem;
    }
}
