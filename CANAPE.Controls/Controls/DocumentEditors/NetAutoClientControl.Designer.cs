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
    partial class NetAutoClientControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetAutoClientControl));
            this.groupBoxClient = new System.Windows.Forms.GroupBox();
            this.tabPageClient = new System.Windows.Forms.TabPage();
            this.proxyClientControl = new CANAPE.Controls.ProxyClientControl();
            this.tabPageLayers = new System.Windows.Forms.TabPage();
            this.layerEditorControl = new CANAPE.Controls.LayerEditorControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.checkBoxLimitConns = new System.Windows.Forms.CheckBox();
            this.numericTotalConns = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSpecifyTimeout = new System.Windows.Forms.CheckBox();
            this.numericTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblConn = new System.Windows.Forms.Label();
            this.numericMaxConns = new System.Windows.Forms.NumericUpDown();
            this.checkBoxIpv6 = new System.Windows.Forms.CheckBox();
            this.radioButtonUdp = new System.Windows.Forms.RadioButton();
            this.radioButtonTcp = new System.Windows.Forms.RadioButton();
            this.numTcpPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dataEndpointSelectionControl = new CANAPE.Controls.DataEndpointSelectionControl();
            this.groupBoxClient.SuspendLayout();
            this.tabPageClient.SuspendLayout();
            this.tabPageLayers.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTotalConns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxConns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTcpPort)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxClient
            // 
            resources.ApplyResources(this.groupBoxClient, "groupBoxClient");
            this.groupBoxClient.Controls.Add(this.dataEndpointSelectionControl);
            this.groupBoxClient.Name = "groupBoxClient";
            this.groupBoxClient.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxClient, resources.GetString("groupBoxClient.ToolTip"));
            // 
            // tabPageClient
            // 
            this.tabPageClient.Controls.Add(this.proxyClientControl);
            resources.ApplyResources(this.tabPageClient, "tabPageClient");
            this.tabPageClient.Name = "tabPageClient";
            this.tabPageClient.UseVisualStyleBackColor = true;
            // 
            // proxyClientControl
            // 
            resources.ApplyResources(this.proxyClientControl, "proxyClientControl");
            this.proxyClientControl.HideDefault = false;
            this.proxyClientControl.Name = "proxyClientControl";
            this.proxyClientControl.ClientChanged += new System.EventHandler(this.proxyClientControl_ClientChanged);
            // 
            // tabPageLayers
            // 
            this.tabPageLayers.Controls.Add(this.layerEditorControl);
            resources.ApplyResources(this.tabPageLayers, "tabPageLayers");
            this.tabPageLayers.Name = "tabPageLayers";
            this.tabPageLayers.UseVisualStyleBackColor = true;
            // 
            // layerEditorControl
            // 
            this.layerEditorControl.Binding = CANAPE.Net.Layers.NetworkLayerBinding.Client;
            resources.ApplyResources(this.layerEditorControl, "layerEditorControl");
            this.layerEditorControl.Name = "layerEditorControl";
            this.layerEditorControl.LayersUpdated += new System.EventHandler(this.layerEditorControl_LayersUpdated);
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.checkBoxLimitConns);
            this.tabPageConfig.Controls.Add(this.numericTotalConns);
            this.tabPageConfig.Controls.Add(this.checkBoxSpecifyTimeout);
            this.tabPageConfig.Controls.Add(this.numericTimeout);
            this.tabPageConfig.Controls.Add(this.lblConn);
            this.tabPageConfig.Controls.Add(this.numericMaxConns);
            this.tabPageConfig.Controls.Add(this.groupBoxClient);
            this.tabPageConfig.Controls.Add(this.checkBoxIpv6);
            this.tabPageConfig.Controls.Add(this.radioButtonUdp);
            this.tabPageConfig.Controls.Add(this.radioButtonTcp);
            this.tabPageConfig.Controls.Add(this.numTcpPort);
            this.tabPageConfig.Controls.Add(this.textBoxHost);
            this.tabPageConfig.Controls.Add(this.lblHost);
            this.tabPageConfig.Controls.Add(this.lblPort);
            resources.ApplyResources(this.tabPageConfig, "tabPageConfig");
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // checkBoxLimitConns
            // 
            resources.ApplyResources(this.checkBoxLimitConns, "checkBoxLimitConns");
            this.checkBoxLimitConns.Name = "checkBoxLimitConns";
            this.toolTip.SetToolTip(this.checkBoxLimitConns, resources.GetString("checkBoxLimitConns.ToolTip"));
            this.checkBoxLimitConns.UseVisualStyleBackColor = true;
            this.checkBoxLimitConns.CheckedChanged += new System.EventHandler(this.checkBoxLimitConns_CheckedChanged);
            // 
            // numericTotalConns
            // 
            resources.ApplyResources(this.numericTotalConns, "numericTotalConns");
            this.numericTotalConns.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericTotalConns.Name = "numericTotalConns";
            this.toolTip.SetToolTip(this.numericTotalConns, resources.GetString("numericTotalConns.ToolTip"));
            this.numericTotalConns.ValueChanged += new System.EventHandler(this.numericUpDownTotalConns_ValueChanged);
            // 
            // checkBoxSpecifyTimeout
            // 
            resources.ApplyResources(this.checkBoxSpecifyTimeout, "checkBoxSpecifyTimeout");
            this.checkBoxSpecifyTimeout.Name = "checkBoxSpecifyTimeout";
            this.toolTip.SetToolTip(this.checkBoxSpecifyTimeout, resources.GetString("checkBoxSpecifyTimeout.ToolTip"));
            this.checkBoxSpecifyTimeout.UseVisualStyleBackColor = true;
            this.checkBoxSpecifyTimeout.CheckedChanged += new System.EventHandler(this.checkBoxSpecifyTimeout_CheckedChanged);
            // 
            // numericTimeout
            // 
            this.numericTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            resources.ApplyResources(this.numericTimeout, "numericTimeout");
            this.numericTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericTimeout.Name = "numericTimeout";
            this.toolTip.SetToolTip(this.numericTimeout, resources.GetString("numericTimeout.ToolTip"));
            this.numericTimeout.ValueChanged += new System.EventHandler(this.numericTimeout_ValueChanged);
            // 
            // lblConn
            // 
            resources.ApplyResources(this.lblConn, "lblConn");
            this.lblConn.Name = "lblConn";
            // 
            // numericMaxConns
            // 
            resources.ApplyResources(this.numericMaxConns, "numericMaxConns");
            this.numericMaxConns.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMaxConns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxConns.Name = "numericMaxConns";
            this.toolTip.SetToolTip(this.numericMaxConns, resources.GetString("numericMaxConns.ToolTip"));
            this.numericMaxConns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxConns.ValueChanged += new System.EventHandler(this.numericMaxConns_ValueChanged);
            // 
            // checkBoxIpv6
            // 
            resources.ApplyResources(this.checkBoxIpv6, "checkBoxIpv6");
            this.checkBoxIpv6.Name = "checkBoxIpv6";
            this.toolTip.SetToolTip(this.checkBoxIpv6, resources.GetString("checkBoxIpv6.ToolTip"));
            this.checkBoxIpv6.UseVisualStyleBackColor = true;
            this.checkBoxIpv6.CheckedChanged += new System.EventHandler(this.checkBoxIpv6_CheckedChanged);
            // 
            // radioButtonUdp
            // 
            resources.ApplyResources(this.radioButtonUdp, "radioButtonUdp");
            this.radioButtonUdp.Name = "radioButtonUdp";
            this.radioButtonUdp.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonUdp, resources.GetString("radioButtonUdp.ToolTip"));
            this.radioButtonUdp.UseVisualStyleBackColor = true;
            // 
            // radioButtonTcp
            // 
            resources.ApplyResources(this.radioButtonTcp, "radioButtonTcp");
            this.radioButtonTcp.Checked = true;
            this.radioButtonTcp.Name = "radioButtonTcp";
            this.radioButtonTcp.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonTcp, resources.GetString("radioButtonTcp.ToolTip"));
            this.radioButtonTcp.UseVisualStyleBackColor = true;
            this.radioButtonTcp.CheckedChanged += new System.EventHandler(this.radioButtonTcp_CheckedChanged);
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
            80,
            0,
            0,
            0});
            this.numTcpPort.ValueChanged += new System.EventHandler(this.numTcpPort_ValueChanged);
            // 
            // textBoxHost
            // 
            resources.ApplyResources(this.textBoxHost, "textBoxHost");
            this.textBoxHost.Name = "textBoxHost";
            this.toolTip.SetToolTip(this.textBoxHost, resources.GetString("textBoxHost.ToolTip"));
            this.textBoxHost.TextChanged += new System.EventHandler(this.textBoxHost_TextChanged);
            // 
            // lblHost
            // 
            resources.ApplyResources(this.lblHost, "lblHost");
            this.lblHost.Name = "lblHost";
            // 
            // lblPort
            // 
            resources.ApplyResources(this.lblPort, "lblPort");
            this.lblPort.Name = "lblPort";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageLayers);
            this.tabControl.Controls.Add(this.tabPageClient);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // dataEndpointSelectionControl
            // 
            resources.ApplyResources(this.dataEndpointSelectionControl, "dataEndpointSelectionControl");
            this.dataEndpointSelectionControl.Name = "dataEndpointSelectionControl";
            this.dataEndpointSelectionControl.FactoryChanged += new System.EventHandler(this.dataEndpointSelectionControl_FactoryChanged);
            // 
            // NetAutoClientControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "NetAutoClientControl";
            this.groupBoxClient.ResumeLayout(false);
            this.tabPageClient.ResumeLayout(false);
            this.tabPageLayers.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTotalConns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxConns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTcpPort)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageClient;
        private ProxyClientControl proxyClientControl;
        private System.Windows.Forms.TabPage tabPageLayers;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.CheckBox checkBoxIpv6;
        private System.Windows.Forms.RadioButton radioButtonUdp;
        private System.Windows.Forms.RadioButton radioButtonTcp;
        private System.Windows.Forms.NumericUpDown numTcpPort;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label lblConn;
        private System.Windows.Forms.NumericUpDown numericMaxConns;
        private System.Windows.Forms.NumericUpDown numericTimeout;
        private System.Windows.Forms.CheckBox checkBoxSpecifyTimeout;
        private System.Windows.Forms.NumericUpDown numericTotalConns;
        private System.Windows.Forms.CheckBox checkBoxLimitConns;
        private System.Windows.Forms.GroupBox groupBoxClient;
        private System.Windows.Forms.ToolTip toolTip;
        private LayerEditorControl layerEditorControl;
        private DataEndpointSelectionControl dataEndpointSelectionControl;

    }
}
