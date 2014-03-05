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

namespace CANAPE.Documents.ComPort
{
    partial class SerialPortProxyServerDocumentControl
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
            System.Windows.Forms.GroupBox groupBoxServerPort;
            System.Windows.Forms.GroupBox groupBoxClient;
            this.serialPortConfigurationControlServer = new CANAPE.Documents.ComPort.SerialPortConfigurationControl();
            this.serialPortConfigurationControlClient = new CANAPE.Documents.ComPort.SerialPortConfigurationControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.tabPageLayers = new System.Windows.Forms.TabPage();
            this.layerEditorControl = new CANAPE.Controls.LayerEditorControl();
            groupBoxServerPort = new System.Windows.Forms.GroupBox();
            groupBoxClient = new System.Windows.Forms.GroupBox();
            groupBoxServerPort.SuspendLayout();
            groupBoxClient.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.tabPageLayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxServerPort
            // 
            groupBoxServerPort.Controls.Add(this.serialPortConfigurationControlServer);
            groupBoxServerPort.Location = new System.Drawing.Point(6, 16);
            groupBoxServerPort.Name = "groupBoxServerPort";
            groupBoxServerPort.Size = new System.Drawing.Size(251, 191);
            groupBoxServerPort.TabIndex = 0;
            groupBoxServerPort.TabStop = false;
            groupBoxServerPort.Text = "Server Port";
            // 
            // serialPortConfigurationControlServer
            // 
            this.serialPortConfigurationControlServer.Location = new System.Drawing.Point(7, 20);
            this.serialPortConfigurationControlServer.Name = "serialPortConfigurationControlServer";
            this.serialPortConfigurationControlServer.Size = new System.Drawing.Size(238, 165);
            this.serialPortConfigurationControlServer.TabIndex = 0;
            // 
            // groupBoxClient
            // 
            groupBoxClient.Controls.Add(this.serialPortConfigurationControlClient);
            groupBoxClient.Location = new System.Drawing.Point(281, 16);
            groupBoxClient.Name = "groupBoxClient";
            groupBoxClient.Size = new System.Drawing.Size(251, 191);
            groupBoxClient.TabIndex = 1;
            groupBoxClient.TabStop = false;
            groupBoxClient.Text = "Client Port";
            // 
            // serialPortConfigurationControlClient
            // 
            this.serialPortConfigurationControlClient.Location = new System.Drawing.Point(7, 20);
            this.serialPortConfigurationControlClient.Name = "serialPortConfigurationControlClient";
            this.serialPortConfigurationControlClient.Size = new System.Drawing.Size(238, 165);
            this.serialPortConfigurationControlClient.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageLayers);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(659, 422);
            this.tabControl.TabIndex = 2;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(groupBoxServerPort);
            this.tabPageConfig.Controls.Add(groupBoxClient);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(651, 396);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Config";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // tabPageLayers
            // 
            this.tabPageLayers.Controls.Add(this.layerEditorControl);
            this.tabPageLayers.Location = new System.Drawing.Point(4, 22);
            this.tabPageLayers.Name = "tabPageLayers";
            this.tabPageLayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLayers.Size = new System.Drawing.Size(651, 396);
            this.tabPageLayers.TabIndex = 1;
            this.tabPageLayers.Text = "Layers";
            this.tabPageLayers.UseVisualStyleBackColor = true;
            // 
            // layerEditorControl
            // 
            this.layerEditorControl.Binding = CANAPE.Net.Layers.NetworkLayerBinding.Default;
            this.layerEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerEditorControl.Location = new System.Drawing.Point(3, 3);
            this.layerEditorControl.Name = "layerEditorControl";
            this.layerEditorControl.Size = new System.Drawing.Size(645, 390);
            this.layerEditorControl.TabIndex = 0;
            this.layerEditorControl.LayersUpdated += new System.EventHandler(this.layerEditorControl_LayersUpdated);
            // 
            // SerialPortProxyServerDocumentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "SerialPortProxyServerDocumentControl";
            this.Size = new System.Drawing.Size(659, 422);
            groupBoxServerPort.ResumeLayout(false);
            groupBoxClient.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageLayers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SerialPortConfigurationControl serialPortConfigurationControlServer;
        private SerialPortConfigurationControl serialPortConfigurationControlClient;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageLayers;
        private Controls.LayerEditorControl layerEditorControl;
    }
}
