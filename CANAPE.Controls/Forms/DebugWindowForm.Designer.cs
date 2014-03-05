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

namespace CANAPE.Forms
{
    partial class DebugWindowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugWindowForm));
            System.Windows.Forms.ColumnHeader columnHeaderText;
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonClearLog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.listViewDebugOutput = new CANAPE.Controls.ListViewExtension();
            columnHeaderText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem,
            this.autoScrollToolStripMenuItem,
            this.alwaysOnTopToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            // 
            // clearLogToolStripMenuItem
            // 
            resources.ApplyResources(this.clearLogToolStripMenuItem, "clearLogToolStripMenuItem");
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonClearLog_Click);
            // 
            // autoScrollToolStripMenuItem
            // 
            resources.ApplyResources(this.autoScrollToolStripMenuItem, "autoScrollToolStripMenuItem");
            this.autoScrollToolStripMenuItem.Name = "autoScrollToolStripMenuItem";
            this.autoScrollToolStripMenuItem.Click += new System.EventHandler(this.autoScrollToolStripMenuItem_Click);
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            resources.ApplyResources(this.alwaysOnTopToolStripMenuItem, "alwaysOnTopToolStripMenuItem");
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClearLog,
            this.toolStripSeparator1});
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripButtonClearLog
            // 
            resources.ApplyResources(this.toolStripButtonClearLog, "toolStripButtonClearLog");
            this.toolStripButtonClearLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClearLog.Name = "toolStripButtonClearLog";
            this.toolStripButtonClearLog.Click += new System.EventHandler(this.toolStripButtonClearLog_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // listViewDebugOutput
            // 
            resources.ApplyResources(this.listViewDebugOutput, "listViewDebugOutput");
            this.listViewDebugOutput.AutoScrollList = false;
            this.listViewDebugOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderText});
            this.listViewDebugOutput.ContextMenuStrip = this.contextMenuStrip;
            this.listViewDebugOutput.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewDebugOutput.Name = "listViewDebugOutput";
            this.listViewDebugOutput.UseCompatibleStateImageBehavior = false;
            this.listViewDebugOutput.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderText
            // 
            resources.ApplyResources(columnHeaderText, "columnHeaderText");
            // 
            // DebugWindowForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewDebugOutput);
            this.Controls.Add(this.toolStrip);
            this.Name = "DebugWindowForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindowForm_FormClosing);
            this.Load += new System.EventHandler(this.DebugWindowForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.ListViewExtension listViewDebugOutput;
        private System.Windows.Forms.ToolStripMenuItem autoScrollToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;

    }
}