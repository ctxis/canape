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
    partial class NetServiceDocumentControl
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
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetServiceDocumentControl));
            this.btnStart = new System.Windows.Forms.Button();
            this.comboBoxNetgraph = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.tabPagePacketLog = new System.Windows.Forms.TabPage();
            this.packetLogControl = new CANAPE.Controls.PacketLogControl();
            this.tabPageEvents = new System.Windows.Forms.TabPage();
            this.eventLogControl = new CANAPE.Controls.EventLogControl();
            this.tabPageGlobalMeta = new System.Windows.Forms.TabPage();
            this.checkBoxClearOnStart = new System.Windows.Forms.CheckBox();
            this.metaEditorControl = new CANAPE.Controls.MetaEditorControl();
            this.tabPageConns = new System.Windows.Forms.TabPage();
            this.listViewConns = new System.Windows.Forms.ListView();
            this.columnId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnConnectTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.killConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.networkHistoryControl = new CANAPE.Controls.ConnectionHistoryControl();
            this.tabPageNetGraphs = new System.Windows.Forms.TabPage();
            this.netGraphNodesControl = new CANAPE.Controls.ActiveNetgraphControl();
            this.tabPageInjector = new System.Windows.Forms.TabPage();
            this.injectPacketControl = new CANAPE.Controls.InjectPacketControl();
            this.tabPageRedirectLog = new System.Windows.Forms.TabPage();
            this.redirectLogControl = new CANAPE.Controls.RedirectLogControl();
            this.checkBoxRedirect = new System.Windows.Forms.CheckBox();
            this.tabPageCredentials = new System.Windows.Forms.TabPage();
            this.credentialsEditorControl = new CANAPE.Controls.CredentialsEditorControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelSettings = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabPagePacketLog.SuspendLayout();
            this.tabPageEvents.SuspendLayout();
            this.tabPageGlobalMeta.SuspendLayout();
            this.tabPageConns.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.tabPageHistory.SuspendLayout();
            this.tabPageNetGraphs.SuspendLayout();
            this.tabPageInjector.SuspendLayout();
            this.tabPageRedirectLog.SuspendLayout();
            this.tabPageCredentials.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.toolTip.SetToolTip(this.btnStart, resources.GetString("btnStart.ToolTip"));
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // comboBoxNetgraph
            // 
            this.comboBoxNetgraph.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNetgraph.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxNetgraph, "comboBoxNetgraph");
            this.comboBoxNetgraph.Name = "comboBoxNetgraph";
            this.toolTip.SetToolTip(this.comboBoxNetgraph, resources.GetString("comboBoxNetgraph.ToolTip"));
            this.comboBoxNetgraph.DropDown += new System.EventHandler(this.comboBoxNetgraph_DropDown);
            this.comboBoxNetgraph.SelectionChangeCommitted += new System.EventHandler(this.comboBoxNetgraph_SelectionChangeCommitted);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageSettings);
            this.tabControl.Controls.Add(this.tabPagePacketLog);
            this.tabControl.Controls.Add(this.tabPageEvents);
            this.tabControl.Controls.Add(this.tabPageGlobalMeta);
            this.tabControl.Controls.Add(this.tabPageConns);
            this.tabControl.Controls.Add(this.tabPageHistory);
            this.tabControl.Controls.Add(this.tabPageNetGraphs);
            this.tabControl.Controls.Add(this.tabPageInjector);
            this.tabControl.Controls.Add(this.tabPageRedirectLog);
            this.tabControl.Controls.Add(this.tabPageCredentials);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.panelSettings);
            this.tabPageSettings.Controls.Add(this.btnStart);
            this.tabPageSettings.Controls.Add(this.comboBoxNetgraph);
            this.tabPageSettings.Controls.Add(label1);
            resources.ApplyResources(this.tabPageSettings, "tabPageSettings");
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // tabPagePacketLog
            // 
            this.tabPagePacketLog.Controls.Add(this.packetLogControl);
            resources.ApplyResources(this.tabPagePacketLog, "tabPagePacketLog");
            this.tabPagePacketLog.Name = "tabPagePacketLog";
            this.tabPagePacketLog.UseVisualStyleBackColor = true;
            // 
            // packetLogControl
            // 
            resources.ApplyResources(this.packetLogControl, "packetLogControl");
            this.packetLogControl.IsInFindForm = false;
            this.packetLogControl.LogName = null;
            this.packetLogControl.Name = "packetLogControl";
            this.packetLogControl.ReadOnly = false;
            this.packetLogControl.ConfigChanged += new System.EventHandler(this.packetLogControl_ConfigChanged);
            // 
            // tabPageEvents
            // 
            this.tabPageEvents.Controls.Add(this.eventLogControl);
            resources.ApplyResources(this.tabPageEvents, "tabPageEvents");
            this.tabPageEvents.Name = "tabPageEvents";
            this.tabPageEvents.UseVisualStyleBackColor = true;
            // 
            // eventLogControl
            // 
            resources.ApplyResources(this.eventLogControl, "eventLogControl");
            this.eventLogControl.Name = "eventLogControl";
            // 
            // tabPageGlobalMeta
            // 
            this.tabPageGlobalMeta.Controls.Add(this.checkBoxClearOnStart);
            this.tabPageGlobalMeta.Controls.Add(this.metaEditorControl);
            resources.ApplyResources(this.tabPageGlobalMeta, "tabPageGlobalMeta");
            this.tabPageGlobalMeta.Name = "tabPageGlobalMeta";
            this.tabPageGlobalMeta.UseVisualStyleBackColor = true;
            // 
            // checkBoxClearOnStart
            // 
            resources.ApplyResources(this.checkBoxClearOnStart, "checkBoxClearOnStart");
            this.checkBoxClearOnStart.Name = "checkBoxClearOnStart";
            this.checkBoxClearOnStart.UseVisualStyleBackColor = true;
            // 
            // metaEditorControl
            // 
            resources.ApplyResources(this.metaEditorControl, "metaEditorControl");
            this.metaEditorControl.Name = "metaEditorControl";
            // 
            // tabPageConns
            // 
            this.tabPageConns.Controls.Add(this.listViewConns);
            resources.ApplyResources(this.tabPageConns, "tabPageConns");
            this.tabPageConns.Name = "tabPageConns";
            this.tabPageConns.UseVisualStyleBackColor = true;
            this.tabPageConns.Enter += new System.EventHandler(this.tabPageConns_Enter);
            // 
            // listViewConns
            // 
            this.listViewConns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnId,
            this.columnDescription,
            this.columnConnectTime});
            this.listViewConns.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.listViewConns, "listViewConns");
            this.listViewConns.FullRowSelect = true;
            this.listViewConns.GridLines = true;
            this.listViewConns.MultiSelect = false;
            this.listViewConns.Name = "listViewConns";
            this.toolTip.SetToolTip(this.listViewConns, resources.GetString("listViewConns.ToolTip"));
            this.listViewConns.UseCompatibleStateImageBehavior = false;
            this.listViewConns.View = System.Windows.Forms.View.Details;
            // 
            // columnId
            // 
            resources.ApplyResources(this.columnId, "columnId");
            // 
            // columnDescription
            // 
            resources.ApplyResources(this.columnDescription, "columnDescription");
            // 
            // columnConnectTime
            // 
            resources.ApplyResources(this.columnConnectTime, "columnConnectTime");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.killConnectionToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // killConnectionToolStripMenuItem
            // 
            this.killConnectionToolStripMenuItem.Name = "killConnectionToolStripMenuItem";
            resources.ApplyResources(this.killConnectionToolStripMenuItem, "killConnectionToolStripMenuItem");
            this.killConnectionToolStripMenuItem.Click += new System.EventHandler(this.killConnectionToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            resources.ApplyResources(this.refreshToolStripMenuItem, "refreshToolStripMenuItem");
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.networkHistoryControl);
            resources.ApplyResources(this.tabPageHistory, "tabPageHistory");
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // networkHistoryControl
            // 
            resources.ApplyResources(this.networkHistoryControl, "networkHistoryControl");
            this.networkHistoryControl.Name = "networkHistoryControl";
            // 
            // tabPageNetGraphs
            // 
            this.tabPageNetGraphs.Controls.Add(this.netGraphNodesControl);
            resources.ApplyResources(this.tabPageNetGraphs, "tabPageNetGraphs");
            this.tabPageNetGraphs.Name = "tabPageNetGraphs";
            this.tabPageNetGraphs.UseVisualStyleBackColor = true;
            // 
            // netGraphNodesControl
            // 
            resources.ApplyResources(this.netGraphNodesControl, "netGraphNodesControl");
            this.netGraphNodesControl.Name = "netGraphNodesControl";
            // 
            // tabPageInjector
            // 
            this.tabPageInjector.Controls.Add(this.injectPacketControl);
            resources.ApplyResources(this.tabPageInjector, "tabPageInjector");
            this.tabPageInjector.Name = "tabPageInjector";
            this.tabPageInjector.UseVisualStyleBackColor = true;
            // 
            // injectPacketControl
            // 
            resources.ApplyResources(this.injectPacketControl, "injectPacketControl");
            this.injectPacketControl.Name = "injectPacketControl";
            // 
            // tabPageRedirectLog
            // 
            this.tabPageRedirectLog.Controls.Add(this.redirectLogControl);
            this.tabPageRedirectLog.Controls.Add(this.checkBoxRedirect);
            resources.ApplyResources(this.tabPageRedirectLog, "tabPageRedirectLog");
            this.tabPageRedirectLog.Name = "tabPageRedirectLog";
            this.tabPageRedirectLog.UseVisualStyleBackColor = true;
            // 
            // redirectLogControl
            // 
            resources.ApplyResources(this.redirectLogControl, "redirectLogControl");
            this.redirectLogControl.Name = "redirectLogControl";
            // 
            // checkBoxRedirect
            // 
            resources.ApplyResources(this.checkBoxRedirect, "checkBoxRedirect");
            this.checkBoxRedirect.Name = "checkBoxRedirect";
            this.toolTip.SetToolTip(this.checkBoxRedirect, resources.GetString("checkBoxRedirect.ToolTip"));
            this.checkBoxRedirect.UseVisualStyleBackColor = true;
            this.checkBoxRedirect.CheckedChanged += new System.EventHandler(this.checkBoxRedirect_CheckedChanged);
            // 
            // tabPageCredentials
            // 
            this.tabPageCredentials.Controls.Add(this.credentialsEditorControl);
            resources.ApplyResources(this.tabPageCredentials, "tabPageCredentials");
            this.tabPageCredentials.Name = "tabPageCredentials";
            this.tabPageCredentials.UseVisualStyleBackColor = true;
            // 
            // credentialsEditorControl
            // 
            resources.ApplyResources(this.credentialsEditorControl, "credentialsEditorControl");
            this.credentialsEditorControl.Name = "credentialsEditorControl";
            this.credentialsEditorControl.CredentialsUpdated += new System.EventHandler(this.credentialsEditorControl_CredentialsUpdated);
            // 
            // panelSettings
            // 
            resources.ApplyResources(this.panelSettings, "panelSettings");
            this.panelSettings.Name = "panelSettings";
            // 
            // NetServiceDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "NetServiceDocumentControl";
            this.tabControl.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.tabPagePacketLog.ResumeLayout(false);
            this.tabPageEvents.ResumeLayout(false);
            this.tabPageGlobalMeta.ResumeLayout(false);
            this.tabPageGlobalMeta.PerformLayout();
            this.tabPageConns.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.tabPageHistory.ResumeLayout(false);
            this.tabPageNetGraphs.ResumeLayout(false);
            this.tabPageInjector.ResumeLayout(false);
            this.tabPageRedirectLog.ResumeLayout(false);
            this.tabPageRedirectLog.PerformLayout();
            this.tabPageCredentials.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPagePacketLog;
        private CANAPE.Controls.PacketLogControl packetLogControl;
        private System.Windows.Forms.TabPage tabPageEvents;
        private CANAPE.Controls.EventLogControl eventLogControl;
        private System.Windows.Forms.TabPage tabPageConns;
        private System.Windows.Forms.ListView listViewConns;
        private System.Windows.Forms.ColumnHeader columnId;
        private System.Windows.Forms.ColumnHeader columnDescription;
        protected System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.ComboBox comboBoxNetgraph;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TabPage tabPageNetGraphs;
        private ActiveNetgraphControl netGraphNodesControl;
        private System.Windows.Forms.TabPage tabPageInjector;
        private InjectPacketControl injectPacketControl;
        private System.Windows.Forms.TabPage tabPageGlobalMeta;
        private MetaEditorControl metaEditorControl;
        private System.Windows.Forms.ColumnHeader columnConnectTime;
        private System.Windows.Forms.TabPage tabPageRedirectLog;
        private System.Windows.Forms.CheckBox checkBoxRedirect;
        private RedirectLogControl redirectLogControl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabPageHistory;
        private ConnectionHistoryControl networkHistoryControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem killConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageCredentials;
        private CredentialsEditorControl credentialsEditorControl;
        private System.Windows.Forms.CheckBox checkBoxClearOnStart;
        private System.Windows.Forms.Panel panelSettings;
    }
}