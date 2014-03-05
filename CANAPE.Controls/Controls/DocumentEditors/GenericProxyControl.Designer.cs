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
    partial class GenericProxyControl
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
            System.Windows.Forms.ColumnHeader columnHost;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenericProxyControl));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveFilterUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveFilterDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sSLPort443ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabClient = new System.Windows.Forms.TabPage();
            this.tabPageFilter = new System.Windows.Forms.TabPage();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.buttonFilterDown = new System.Windows.Forms.Button();
            this.buttonFilterUp = new System.Windows.Forms.Button();
            this.listViewFilters = new System.Windows.Forms.ListView();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.btnAdvancedOptions = new System.Windows.Forms.Button();
            this.checkBoxIpv6 = new System.Windows.Forms.CheckBox();
            this.numTcpPort = new System.Windows.Forms.NumericUpDown();
            this.checkBoxGlobal = new System.Windows.Forms.CheckBox();
            this.labelLocalPort = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.hTTPPort80ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTTPSSLPort443ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sSLAllPortsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proxyClientControl = new CANAPE.Controls.ProxyClientControl();
            this.hTTPAllPortsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            columnHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.tabClient.SuspendLayout();
            this.tabPageFilter.SuspendLayout();
            this.tabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTcpPort)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHost
            // 
            resources.ApplyResources(columnHost, "columnHost");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilterToolStripMenuItem,
            this.deleteFilterToolStripMenuItem,
            this.moveFilterUpToolStripMenuItem,
            this.moveFilterDownToolStripMenuItem,
            this.editFilterToolStripMenuItem,
            this.toggleEnabledToolStripMenuItem,
            this.templatesToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // addFilterToolStripMenuItem
            // 
            this.addFilterToolStripMenuItem.Name = "addFilterToolStripMenuItem";
            resources.ApplyResources(this.addFilterToolStripMenuItem, "addFilterToolStripMenuItem");
            this.addFilterToolStripMenuItem.Click += new System.EventHandler(this.AddFilter_EventHandler);
            // 
            // deleteFilterToolStripMenuItem
            // 
            this.deleteFilterToolStripMenuItem.Name = "deleteFilterToolStripMenuItem";
            resources.ApplyResources(this.deleteFilterToolStripMenuItem, "deleteFilterToolStripMenuItem");
            this.deleteFilterToolStripMenuItem.Click += new System.EventHandler(this.DeleteFilter_EventHandler);
            // 
            // moveFilterUpToolStripMenuItem
            // 
            this.moveFilterUpToolStripMenuItem.Name = "moveFilterUpToolStripMenuItem";
            resources.ApplyResources(this.moveFilterUpToolStripMenuItem, "moveFilterUpToolStripMenuItem");
            this.moveFilterUpToolStripMenuItem.Click += new System.EventHandler(this.MoveFilterUp_EventHandler);
            // 
            // moveFilterDownToolStripMenuItem
            // 
            this.moveFilterDownToolStripMenuItem.Name = "moveFilterDownToolStripMenuItem";
            resources.ApplyResources(this.moveFilterDownToolStripMenuItem, "moveFilterDownToolStripMenuItem");
            this.moveFilterDownToolStripMenuItem.Click += new System.EventHandler(this.MoveFilterDown_EventHandler);
            // 
            // editFilterToolStripMenuItem
            // 
            this.editFilterToolStripMenuItem.Name = "editFilterToolStripMenuItem";
            resources.ApplyResources(this.editFilterToolStripMenuItem, "editFilterToolStripMenuItem");
            this.editFilterToolStripMenuItem.Click += new System.EventHandler(this.EditSelectedFilter_EventHandler);
            // 
            // toggleEnabledToolStripMenuItem
            // 
            this.toggleEnabledToolStripMenuItem.Name = "toggleEnabledToolStripMenuItem";
            resources.ApplyResources(this.toggleEnabledToolStripMenuItem, "toggleEnabledToolStripMenuItem");
            this.toggleEnabledToolStripMenuItem.Click += new System.EventHandler(this.toggleEnabledToolStripMenuItem_Click);
            // 
            // templatesToolStripMenuItem
            // 
            this.templatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sSLPort443ToolStripMenuItem,
            this.hTTPPort80ToolStripMenuItem,
            this.hTTPSSLPort443ToolStripMenuItem,
            this.sSLAllPortsToolStripMenuItem,
            this.hTTPAllPortsToolStripMenuItem});
            this.templatesToolStripMenuItem.Name = "templatesToolStripMenuItem";
            resources.ApplyResources(this.templatesToolStripMenuItem, "templatesToolStripMenuItem");
            // 
            // sSLPort443ToolStripMenuItem
            // 
            this.sSLPort443ToolStripMenuItem.Name = "sSLPort443ToolStripMenuItem";
            resources.ApplyResources(this.sSLPort443ToolStripMenuItem, "sSLPort443ToolStripMenuItem");
            this.sSLPort443ToolStripMenuItem.Click += new System.EventHandler(this.sSLPort443ToolStripMenuItem_Click);
            // 
            // tabClient
            // 
            this.tabClient.Controls.Add(this.proxyClientControl);
            resources.ApplyResources(this.tabClient, "tabClient");
            this.tabClient.Name = "tabClient";
            this.tabClient.UseVisualStyleBackColor = true;
            // 
            // tabPageFilter
            // 
            this.tabPageFilter.Controls.Add(this.btnEdit);
            this.tabPageFilter.Controls.Add(this.btnDelete);
            this.tabPageFilter.Controls.Add(this.btnAdd);
            this.tabPageFilter.Controls.Add(this.buttonFilterDown);
            this.tabPageFilter.Controls.Add(this.buttonFilterUp);
            this.tabPageFilter.Controls.Add(this.listViewFilters);
            resources.ApplyResources(this.tabPageFilter, "tabPageFilter");
            this.tabPageFilter.Name = "tabPageFilter";
            this.tabPageFilter.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.toolTip.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.EditSelectedFilter_EventHandler);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.toolTip.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.DeleteFilter_EventHandler);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.toolTip.SetToolTip(this.btnAdd, resources.GetString("btnAdd.ToolTip"));
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.AddFilter_EventHandler);
            // 
            // buttonFilterDown
            // 
            resources.ApplyResources(this.buttonFilterDown, "buttonFilterDown");
            this.buttonFilterDown.Name = "buttonFilterDown";
            this.toolTip.SetToolTip(this.buttonFilterDown, resources.GetString("buttonFilterDown.ToolTip"));
            this.buttonFilterDown.UseVisualStyleBackColor = true;
            this.buttonFilterDown.Click += new System.EventHandler(this.MoveFilterDown_EventHandler);
            // 
            // buttonFilterUp
            // 
            resources.ApplyResources(this.buttonFilterUp, "buttonFilterUp");
            this.buttonFilterUp.Name = "buttonFilterUp";
            this.toolTip.SetToolTip(this.buttonFilterUp, resources.GetString("buttonFilterUp.ToolTip"));
            this.buttonFilterUp.UseVisualStyleBackColor = true;
            this.buttonFilterUp.Click += new System.EventHandler(this.MoveFilterUp_EventHandler);
            // 
            // listViewFilters
            // 
            resources.ApplyResources(this.listViewFilters, "listViewFilters");
            this.listViewFilters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHost});
            this.listViewFilters.ContextMenuStrip = this.contextMenuStrip;
            this.listViewFilters.FullRowSelect = true;
            this.listViewFilters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewFilters.MultiSelect = false;
            this.listViewFilters.Name = "listViewFilters";
            this.toolTip.SetToolTip(this.listViewFilters, resources.GetString("listViewFilters.ToolTip"));
            this.listViewFilters.UseCompatibleStateImageBehavior = false;
            this.listViewFilters.View = System.Windows.Forms.View.Details;
            this.listViewFilters.DoubleClick += new System.EventHandler(this.EditSelectedFilter_EventHandler);
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.btnAdvancedOptions);
            this.tabPageDetails.Controls.Add(this.checkBoxIpv6);
            this.tabPageDetails.Controls.Add(this.numTcpPort);
            this.tabPageDetails.Controls.Add(this.checkBoxGlobal);
            this.tabPageDetails.Controls.Add(this.labelLocalPort);
            resources.ApplyResources(this.tabPageDetails, "tabPageDetails");
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // btnAdvancedOptions
            // 
            resources.ApplyResources(this.btnAdvancedOptions, "btnAdvancedOptions");
            this.btnAdvancedOptions.Name = "btnAdvancedOptions";
            this.toolTip.SetToolTip(this.btnAdvancedOptions, resources.GetString("btnAdvancedOptions.ToolTip"));
            this.btnAdvancedOptions.UseVisualStyleBackColor = true;
            this.btnAdvancedOptions.Click += new System.EventHandler(this.btnAdvancedOptions_Click);
            // 
            // checkBoxIpv6
            // 
            resources.ApplyResources(this.checkBoxIpv6, "checkBoxIpv6");
            this.checkBoxIpv6.Name = "checkBoxIpv6";
            this.toolTip.SetToolTip(this.checkBoxIpv6, resources.GetString("checkBoxIpv6.ToolTip"));
            this.checkBoxIpv6.UseVisualStyleBackColor = true;
            this.checkBoxIpv6.CheckedChanged += new System.EventHandler(this.checkBoxIpv6_CheckedChanged);
            // 
            // numTcpPort
            // 
            resources.ApplyResources(this.numTcpPort, "numTcpPort");
            this.numTcpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numTcpPort.Name = "numTcpPort";
            this.toolTip.SetToolTip(this.numTcpPort, resources.GetString("numTcpPort.ToolTip"));
            this.numTcpPort.Value = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.numTcpPort.ValueChanged += new System.EventHandler(this.numTcpPort_ValueChanged);
            // 
            // checkBoxGlobal
            // 
            resources.ApplyResources(this.checkBoxGlobal, "checkBoxGlobal");
            this.checkBoxGlobal.Name = "checkBoxGlobal";
            this.toolTip.SetToolTip(this.checkBoxGlobal, resources.GetString("checkBoxGlobal.ToolTip"));
            this.checkBoxGlobal.UseVisualStyleBackColor = true;
            this.checkBoxGlobal.CheckedChanged += new System.EventHandler(this.checkBoxGlobal_CheckedChanged);
            // 
            // labelLocalPort
            // 
            resources.ApplyResources(this.labelLocalPort, "labelLocalPort");
            this.labelLocalPort.Name = "labelLocalPort";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageDetails);
            this.tabControl.Controls.Add(this.tabPageFilter);
            this.tabControl.Controls.Add(this.tabClient);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // hTTPPort80ToolStripMenuItem
            // 
            this.hTTPPort80ToolStripMenuItem.Name = "hTTPPort80ToolStripMenuItem";
            resources.ApplyResources(this.hTTPPort80ToolStripMenuItem, "hTTPPort80ToolStripMenuItem");
            this.hTTPPort80ToolStripMenuItem.Click += new System.EventHandler(this.hTTPPort80ToolStripMenuItem_Click);
            // 
            // hTTPSSLPort443ToolStripMenuItem
            // 
            this.hTTPSSLPort443ToolStripMenuItem.Name = "hTTPSSLPort443ToolStripMenuItem";
            resources.ApplyResources(this.hTTPSSLPort443ToolStripMenuItem, "hTTPSSLPort443ToolStripMenuItem");
            this.hTTPSSLPort443ToolStripMenuItem.Click += new System.EventHandler(this.hTTPSSLPort443ToolStripMenuItem_Click);
            // 
            // sSLAllPortsToolStripMenuItem
            // 
            this.sSLAllPortsToolStripMenuItem.Name = "sSLAllPortsToolStripMenuItem";
            resources.ApplyResources(this.sSLAllPortsToolStripMenuItem, "sSLAllPortsToolStripMenuItem");
            this.sSLAllPortsToolStripMenuItem.Click += new System.EventHandler(this.sSLAllPortsToolStripMenuItem_Click);
            // 
            // proxyClientControl
            // 
            resources.ApplyResources(this.proxyClientControl, "proxyClientControl");
            this.proxyClientControl.HideDefault = false;
            this.proxyClientControl.Name = "proxyClientControl";
            this.proxyClientControl.ClientChanged += new System.EventHandler(this.proxyClientControl_ClientChanged);
            // 
            // hTTPAllPortsToolStripMenuItem
            // 
            this.hTTPAllPortsToolStripMenuItem.Name = "hTTPAllPortsToolStripMenuItem";
            resources.ApplyResources(this.hTTPAllPortsToolStripMenuItem, "hTTPAllPortsToolStripMenuItem");
            this.hTTPAllPortsToolStripMenuItem.Click += new System.EventHandler(this.hTTPAllPortsToolStripMenuItem_Click);
            // 
            // GenericProxyControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "GenericProxyControl";
            this.contextMenuStrip.ResumeLayout(false);
            this.tabClient.ResumeLayout(false);
            this.tabPageFilter.ResumeLayout(false);
            this.tabPageDetails.ResumeLayout(false);
            this.tabPageDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTcpPort)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFilterToolStripMenuItem;
        private System.Windows.Forms.TabPage tabClient;
        private ProxyClientControl proxyClientControl;
        private System.Windows.Forms.TabPage tabPageFilter;
        private System.Windows.Forms.ListView listViewFilters;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.CheckBox checkBoxIpv6;
        private System.Windows.Forms.NumericUpDown numTcpPort;
        private System.Windows.Forms.CheckBox checkBoxGlobal;
        private System.Windows.Forms.Label labelLocalPort;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button buttonFilterDown;
        private System.Windows.Forms.Button buttonFilterUp;
        private System.Windows.Forms.ToolStripMenuItem moveFilterUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveFilterDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editFilterToolStripMenuItem;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAdvancedOptions;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem toggleEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sSLPort443ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTTPPort80ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTTPSSLPort443ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sSLAllPortsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTTPAllPortsToolStripMenuItem;
    }
}
