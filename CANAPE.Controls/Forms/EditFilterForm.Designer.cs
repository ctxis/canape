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
    partial class EditFilterForm
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
            System.Windows.Forms.Label lblGraph;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFilterForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMatch = new System.Windows.Forms.TabPage();
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxRegex = new System.Windows.Forms.CheckBox();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numFilterPort = new System.Windows.Forms.NumericUpDown();
            this.tabPageConnection = new System.Windows.Forms.TabPage();
            this.checkBoxBlock = new System.Windows.Forms.CheckBox();
            this.textBoxRedirectHost = new System.Windows.Forms.TextBox();
            this.labelRedirectHost = new System.Windows.Forms.Label();
            this.labelRedirectPort = new System.Windows.Forms.Label();
            this.numRedirectPort = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRedirect = new System.Windows.Forms.CheckBox();
            this.comboBoxNetgraph = new System.Windows.Forms.ComboBox();
            this.tabPageClient = new System.Windows.Forms.TabPage();
            this.checkBoxClient = new System.Windows.Forms.CheckBox();
            this.proxyClientControl = new CANAPE.Controls.ProxyClientControl();
            this.tabPageLayers = new System.Windows.Forms.TabPage();
            this.layerEditorControl = new CANAPE.Controls.LayerEditorControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            lblGraph = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageMatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFilterPort)).BeginInit();
            this.tabPageConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRedirectPort)).BeginInit();
            this.tabPageClient.SuspendLayout();
            this.tabPageLayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGraph
            // 
            resources.ApplyResources(lblGraph, "lblGraph");
            lblGraph.Name = "lblGraph";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageMatch);
            this.tabControl.Controls.Add(this.tabPageConnection);
            this.tabControl.Controls.Add(this.tabPageClient);
            this.tabControl.Controls.Add(this.tabPageLayers);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageMatch
            // 
            this.tabPageMatch.Controls.Add(this.checkBoxEnabled);
            this.tabPageMatch.Controls.Add(this.checkBoxRegex);
            this.tabPageMatch.Controls.Add(this.textBoxHost);
            this.tabPageMatch.Controls.Add(this.label1);
            this.tabPageMatch.Controls.Add(this.label2);
            this.tabPageMatch.Controls.Add(this.numFilterPort);
            resources.ApplyResources(this.tabPageMatch, "tabPageMatch");
            this.tabPageMatch.Name = "tabPageMatch";
            this.tabPageMatch.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnabled
            // 
            resources.ApplyResources(this.checkBoxEnabled, "checkBoxEnabled");
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.toolTip.SetToolTip(this.checkBoxEnabled, resources.GetString("checkBoxEnabled.ToolTip"));
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegex
            // 
            resources.ApplyResources(this.checkBoxRegex, "checkBoxRegex");
            this.checkBoxRegex.Name = "checkBoxRegex";
            this.toolTip.SetToolTip(this.checkBoxRegex, resources.GetString("checkBoxRegex.ToolTip"));
            this.checkBoxRegex.UseVisualStyleBackColor = true;
            // 
            // textBoxHost
            // 
            resources.ApplyResources(this.textBoxHost, "textBoxHost");
            this.textBoxHost.Name = "textBoxHost";
            this.toolTip.SetToolTip(this.textBoxHost, resources.GetString("textBoxHost.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numFilterPort
            // 
            resources.ApplyResources(this.numFilterPort, "numFilterPort");
            this.numFilterPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numFilterPort.Name = "numFilterPort";
            this.toolTip.SetToolTip(this.numFilterPort, resources.GetString("numFilterPort.ToolTip"));
            // 
            // tabPageConnection
            // 
            this.tabPageConnection.Controls.Add(this.checkBoxBlock);
            this.tabPageConnection.Controls.Add(this.textBoxRedirectHost);
            this.tabPageConnection.Controls.Add(this.labelRedirectHost);
            this.tabPageConnection.Controls.Add(this.labelRedirectPort);
            this.tabPageConnection.Controls.Add(this.numRedirectPort);
            this.tabPageConnection.Controls.Add(this.checkBoxRedirect);
            this.tabPageConnection.Controls.Add(this.comboBoxNetgraph);
            this.tabPageConnection.Controls.Add(lblGraph);
            resources.ApplyResources(this.tabPageConnection, "tabPageConnection");
            this.tabPageConnection.Name = "tabPageConnection";
            this.tabPageConnection.UseVisualStyleBackColor = true;
            // 
            // checkBoxBlock
            // 
            resources.ApplyResources(this.checkBoxBlock, "checkBoxBlock");
            this.checkBoxBlock.Name = "checkBoxBlock";
            this.toolTip.SetToolTip(this.checkBoxBlock, resources.GetString("checkBoxBlock.ToolTip"));
            this.checkBoxBlock.UseVisualStyleBackColor = true;
            // 
            // textBoxRedirectHost
            // 
            resources.ApplyResources(this.textBoxRedirectHost, "textBoxRedirectHost");
            this.textBoxRedirectHost.Name = "textBoxRedirectHost";
            this.toolTip.SetToolTip(this.textBoxRedirectHost, resources.GetString("textBoxRedirectHost.ToolTip"));
            // 
            // labelRedirectHost
            // 
            resources.ApplyResources(this.labelRedirectHost, "labelRedirectHost");
            this.labelRedirectHost.Name = "labelRedirectHost";
            // 
            // labelRedirectPort
            // 
            resources.ApplyResources(this.labelRedirectPort, "labelRedirectPort");
            this.labelRedirectPort.Name = "labelRedirectPort";
            // 
            // numRedirectPort
            // 
            resources.ApplyResources(this.numRedirectPort, "numRedirectPort");
            this.numRedirectPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numRedirectPort.Name = "numRedirectPort";
            this.toolTip.SetToolTip(this.numRedirectPort, resources.GetString("numRedirectPort.ToolTip"));
            // 
            // checkBoxRedirect
            // 
            resources.ApplyResources(this.checkBoxRedirect, "checkBoxRedirect");
            this.checkBoxRedirect.Name = "checkBoxRedirect";
            this.toolTip.SetToolTip(this.checkBoxRedirect, resources.GetString("checkBoxRedirect.ToolTip"));
            this.checkBoxRedirect.UseVisualStyleBackColor = true;
            this.checkBoxRedirect.CheckedChanged += new System.EventHandler(this.checkBoxRedirect_CheckedChanged);
            // 
            // comboBoxNetgraph
            // 
            this.comboBoxNetgraph.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxNetgraph, "comboBoxNetgraph");
            this.comboBoxNetgraph.Name = "comboBoxNetgraph";
            this.toolTip.SetToolTip(this.comboBoxNetgraph, resources.GetString("comboBoxNetgraph.ToolTip"));
            // 
            // tabPageClient
            // 
            this.tabPageClient.Controls.Add(this.checkBoxClient);
            this.tabPageClient.Controls.Add(this.proxyClientControl);
            resources.ApplyResources(this.tabPageClient, "tabPageClient");
            this.tabPageClient.Name = "tabPageClient";
            this.tabPageClient.UseVisualStyleBackColor = true;
            // 
            // checkBoxClient
            // 
            resources.ApplyResources(this.checkBoxClient, "checkBoxClient");
            this.checkBoxClient.Name = "checkBoxClient";
            this.toolTip.SetToolTip(this.checkBoxClient, resources.GetString("checkBoxClient.ToolTip"));
            this.checkBoxClient.UseVisualStyleBackColor = true;
            this.checkBoxClient.CheckedChanged += new System.EventHandler(this.checkBoxClient_CheckedChanged);
            // 
            // proxyClientControl
            // 
            resources.ApplyResources(this.proxyClientControl, "proxyClientControl");
            this.proxyClientControl.HideDefault = false;
            this.proxyClientControl.Name = "proxyClientControl";
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
            this.layerEditorControl.Binding = CANAPE.Net.Layers.NetworkLayerBinding.Default;
            resources.ApplyResources(this.layerEditorControl, "layerEditorControl");
            this.layerEditorControl.Name = "layerEditorControl";
            this.layerEditorControl.LayersUpdated += new System.EventHandler(this.layerEditorControl_LayersUpdated);
            // 
            // EditFilterForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditFilterForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EditFilterForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageMatch.ResumeLayout(false);
            this.tabPageMatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFilterPort)).EndInit();
            this.tabPageConnection.ResumeLayout(false);
            this.tabPageConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRedirectPort)).EndInit();
            this.tabPageClient.ResumeLayout(false);
            this.tabPageClient.PerformLayout();
            this.tabPageLayers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMatch;
        private System.Windows.Forms.TabPage tabPageConnection;
        private System.Windows.Forms.ComboBox comboBoxNetgraph;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numFilterPort;
        private System.Windows.Forms.TextBox textBoxRedirectHost;
        private System.Windows.Forms.Label labelRedirectHost;
        private System.Windows.Forms.Label labelRedirectPort;
        private System.Windows.Forms.NumericUpDown numRedirectPort;
        private System.Windows.Forms.CheckBox checkBoxRedirect;
        private System.Windows.Forms.TabPage tabPageClient;
        private Controls.ProxyClientControl proxyClientControl;
        private System.Windows.Forms.CheckBox checkBoxClient;
        private System.Windows.Forms.CheckBox checkBoxRegex;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBoxEnabled;
        private System.Windows.Forms.CheckBox checkBoxBlock;
        private System.Windows.Forms.TabPage tabPageLayers;
        private Controls.LayerEditorControl layerEditorControl;
    }
}