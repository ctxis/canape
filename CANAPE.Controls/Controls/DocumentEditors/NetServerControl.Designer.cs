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
    partial class NetServerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetServerControl));
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.tabPageLayers = new System.Windows.Forms.TabPage();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.checkBoxBroadcast = new System.Windows.Forms.CheckBox();
            this.checkBoxIpv6 = new System.Windows.Forms.CheckBox();
            this.radioButtonUdp = new System.Windows.Forms.RadioButton();
            this.radioButtonTcp = new System.Windows.Forms.RadioButton();
            this.numLocalTcpPort = new System.Windows.Forms.NumericUpDown();
            this.checkBoxGlobal = new System.Windows.Forms.CheckBox();
            this.lblLocalPort = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.layerEditorControl = new CANAPE.Controls.LayerEditorControl();
            this.dataEndpointSelectionControl = new CANAPE.Controls.DataEndpointSelectionControl();
            this.groupBoxServer.SuspendLayout();
            this.tabPageLayers.SuspendLayout();
            this.tabPageDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLocalTcpPort)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxServer
            // 
            resources.ApplyResources(this.groupBoxServer, "groupBoxServer");
            this.groupBoxServer.Controls.Add(this.dataEndpointSelectionControl);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxServer, resources.GetString("groupBoxServer.ToolTip"));
            // 
            // tabPageLayers
            // 
            this.tabPageLayers.Controls.Add(this.layerEditorControl);
            resources.ApplyResources(this.tabPageLayers, "tabPageLayers");
            this.tabPageLayers.Name = "tabPageLayers";
            this.tabPageLayers.UseVisualStyleBackColor = true;
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.checkBoxBroadcast);
            this.tabPageDetails.Controls.Add(this.groupBoxServer);
            this.tabPageDetails.Controls.Add(this.checkBoxIpv6);
            this.tabPageDetails.Controls.Add(this.radioButtonUdp);
            this.tabPageDetails.Controls.Add(this.radioButtonTcp);
            this.tabPageDetails.Controls.Add(this.numLocalTcpPort);
            this.tabPageDetails.Controls.Add(this.checkBoxGlobal);
            this.tabPageDetails.Controls.Add(this.lblLocalPort);
            resources.ApplyResources(this.tabPageDetails, "tabPageDetails");
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // checkBoxBroadcast
            // 
            resources.ApplyResources(this.checkBoxBroadcast, "checkBoxBroadcast");
            this.checkBoxBroadcast.Name = "checkBoxBroadcast";
            this.toolTip.SetToolTip(this.checkBoxBroadcast, resources.GetString("checkBoxBroadcast.ToolTip"));
            this.checkBoxBroadcast.UseVisualStyleBackColor = true;
            this.checkBoxBroadcast.CheckedChanged += new System.EventHandler(this.checkBoxBroadcast_CheckedChanged);
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
            this.radioButtonUdp.CheckedChanged += new System.EventHandler(this.radioButtonUdp_CheckedChanged);
            // 
            // radioButtonTcp
            // 
            resources.ApplyResources(this.radioButtonTcp, "radioButtonTcp");
            this.radioButtonTcp.Checked = true;
            this.radioButtonTcp.Name = "radioButtonTcp";
            this.radioButtonTcp.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonTcp, resources.GetString("radioButtonTcp.ToolTip"));
            this.radioButtonTcp.UseVisualStyleBackColor = true;
            // 
            // numLocalTcpPort
            // 
            resources.ApplyResources(this.numLocalTcpPort, "numLocalTcpPort");
            this.numLocalTcpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numLocalTcpPort.Name = "numLocalTcpPort";
            this.toolTip.SetToolTip(this.numLocalTcpPort, resources.GetString("numLocalTcpPort.ToolTip"));
            this.numLocalTcpPort.Value = new decimal(new int[] {
            12345,
            0,
            0,
            0});
            this.numLocalTcpPort.ValueChanged += new System.EventHandler(this.numLocalTcpPort_ValueChanged);
            // 
            // checkBoxGlobal
            // 
            resources.ApplyResources(this.checkBoxGlobal, "checkBoxGlobal");
            this.checkBoxGlobal.Name = "checkBoxGlobal";
            this.toolTip.SetToolTip(this.checkBoxGlobal, resources.GetString("checkBoxGlobal.ToolTip"));
            this.checkBoxGlobal.UseVisualStyleBackColor = true;
            this.checkBoxGlobal.CheckedChanged += new System.EventHandler(this.checkBoxGlobal_CheckedChanged);
            // 
            // lblLocalPort
            // 
            resources.ApplyResources(this.lblLocalPort, "lblLocalPort");
            this.lblLocalPort.Name = "lblLocalPort";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageDetails);
            this.tabControl.Controls.Add(this.tabPageLayers);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // layerEditorControl
            // 
            this.layerEditorControl.Binding = CANAPE.Net.Layers.NetworkLayerBinding.Server;
            resources.ApplyResources(this.layerEditorControl, "layerEditorControl");
            this.layerEditorControl.Name = "layerEditorControl";
            this.layerEditorControl.LayersUpdated += new System.EventHandler(this.layerEditorControl_LayersUpdated);
            // 
            // dataEndpointSelectionControl
            // 
            resources.ApplyResources(this.dataEndpointSelectionControl, "dataEndpointSelectionControl");
            this.dataEndpointSelectionControl.Name = "dataEndpointSelectionControl";
            this.dataEndpointSelectionControl.FactoryChanged += new System.EventHandler(this.dataEndpointSelectionControl_FactoryChanged);
            // 
            // NetServerControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "NetServerControl";            
            this.groupBoxServer.ResumeLayout(false);
            this.tabPageLayers.ResumeLayout(false);
            this.tabPageDetails.ResumeLayout(false);
            this.tabPageDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLocalTcpPort)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageLayers;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.CheckBox checkBoxIpv6;
        private System.Windows.Forms.RadioButton radioButtonUdp;
        private System.Windows.Forms.RadioButton radioButtonTcp;
        private System.Windows.Forms.NumericUpDown numLocalTcpPort;
        private System.Windows.Forms.CheckBox checkBoxGlobal;
        private System.Windows.Forms.Label lblLocalPort;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.CheckBox checkBoxBroadcast;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private System.Windows.Forms.ToolTip toolTip;
        private LayerEditorControl layerEditorControl;
        private DataEndpointSelectionControl dataEndpointSelectionControl;

    }
}
