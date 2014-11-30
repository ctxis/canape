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
    partial class ProxyClientControl
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
            System.Windows.Forms.GroupBox groupBoxConnectionType;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProxyClientControl));
            this.btnPACScript = new System.Windows.Forms.Button();
            this.radioButtonPac = new System.Windows.Forms.RadioButton();
            this.radioDefault = new System.Windows.Forms.RadioButton();
            this.radioSystem = new System.Windows.Forms.RadioButton();
            this.radioProxy = new System.Windows.Forms.RadioButton();
            this.radioDirect = new System.Windows.Forms.RadioButton();
            this.labelHost = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.checkBoxSendHostName = new System.Windows.Forms.CheckBox();
            this.checkBoxIpv6 = new System.Windows.Forms.CheckBox();
            this.radioHttp = new System.Windows.Forms.RadioButton();
            this.radioSocksv4 = new System.Windows.Forms.RadioButton();
            this.radioSocksv5 = new System.Windows.Forms.RadioButton();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            groupBoxConnectionType = new System.Windows.Forms.GroupBox();
            groupBoxConnectionType.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxConnectionType
            // 
            groupBoxConnectionType.Controls.Add(this.btnPACScript);
            groupBoxConnectionType.Controls.Add(this.radioButtonPac);
            groupBoxConnectionType.Controls.Add(this.radioDefault);
            groupBoxConnectionType.Controls.Add(this.radioSystem);
            groupBoxConnectionType.Controls.Add(this.radioProxy);
            groupBoxConnectionType.Controls.Add(this.radioDirect);
            resources.ApplyResources(groupBoxConnectionType, "groupBoxConnectionType");
            groupBoxConnectionType.Name = "groupBoxConnectionType";
            groupBoxConnectionType.TabStop = false;
            this.toolTip.SetToolTip(groupBoxConnectionType, resources.GetString("groupBoxConnectionType.ToolTip"));
            // 
            // btnPACScript
            // 
            resources.ApplyResources(this.btnPACScript, "btnPACScript");
            this.btnPACScript.Name = "btnPACScript";
            this.toolTip.SetToolTip(this.btnPACScript, resources.GetString("btnPACScript.ToolTip"));
            this.btnPACScript.UseVisualStyleBackColor = true;
            this.btnPACScript.Click += new System.EventHandler(this.btnPACScript_Click);
            // 
            // radioButtonPac
            // 
            resources.ApplyResources(this.radioButtonPac, "radioButtonPac");
            this.radioButtonPac.Name = "radioButtonPac";
            this.radioButtonPac.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonPac, resources.GetString("radioButtonPac.ToolTip"));
            this.radioButtonPac.UseVisualStyleBackColor = true;
            this.radioButtonPac.CheckedChanged += new System.EventHandler(this.radioButtonPac_CheckedChanged);
            // 
            // radioDefault
            // 
            resources.ApplyResources(this.radioDefault, "radioDefault");
            this.radioDefault.Checked = true;
            this.radioDefault.Name = "radioDefault";
            this.radioDefault.TabStop = true;
            this.toolTip.SetToolTip(this.radioDefault, resources.GetString("radioDefault.ToolTip"));
            this.radioDefault.UseVisualStyleBackColor = true;
            this.radioDefault.CheckedChanged += new System.EventHandler(this.radioDefault_CheckedChanged);
            // 
            // radioSystem
            // 
            resources.ApplyResources(this.radioSystem, "radioSystem");
            this.radioSystem.Name = "radioSystem";
            this.toolTip.SetToolTip(this.radioSystem, resources.GetString("radioSystem.ToolTip"));
            this.radioSystem.UseVisualStyleBackColor = true;
            this.radioSystem.CheckedChanged += new System.EventHandler(this.radioSystem_CheckedChanged);
            // 
            // radioProxy
            // 
            resources.ApplyResources(this.radioProxy, "radioProxy");
            this.radioProxy.Name = "radioProxy";
            this.toolTip.SetToolTip(this.radioProxy, resources.GetString("radioProxy.ToolTip"));
            this.radioProxy.UseVisualStyleBackColor = true;
            this.radioProxy.CheckedChanged += new System.EventHandler(this.radioProxy_CheckedChanged);
            // 
            // radioDirect
            // 
            resources.ApplyResources(this.radioDirect, "radioDirect");
            this.radioDirect.Name = "radioDirect";
            this.toolTip.SetToolTip(this.radioDirect, resources.GetString("radioDirect.ToolTip"));
            this.radioDirect.UseVisualStyleBackColor = true;
            this.radioDirect.CheckedChanged += new System.EventHandler(this.radioDirect_CheckedChanged);
            // 
            // labelHost
            // 
            resources.ApplyResources(this.labelHost, "labelHost");
            this.labelHost.Name = "labelHost";
            // 
            // labelPort
            // 
            resources.ApplyResources(this.labelPort, "labelPort");
            this.labelPort.Name = "labelPort";
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Controls.Add(this.checkBoxSendHostName);
            this.groupBoxDetails.Controls.Add(this.checkBoxIpv6);
            this.groupBoxDetails.Controls.Add(this.radioHttp);
            this.groupBoxDetails.Controls.Add(this.radioSocksv4);
            this.groupBoxDetails.Controls.Add(this.radioSocksv5);
            this.groupBoxDetails.Controls.Add(this.labelPort);
            this.groupBoxDetails.Controls.Add(this.numericUpDownPort);
            this.groupBoxDetails.Controls.Add(this.textBoxHost);
            this.groupBoxDetails.Controls.Add(this.labelHost);
            resources.ApplyResources(this.groupBoxDetails, "groupBoxDetails");
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.TabStop = false;
            // 
            // checkBoxSendHostName
            // 
            resources.ApplyResources(this.checkBoxSendHostName, "checkBoxSendHostName");
            this.checkBoxSendHostName.Checked = true;
            this.checkBoxSendHostName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSendHostName.Name = "checkBoxSendHostName";
            this.toolTip.SetToolTip(this.checkBoxSendHostName, resources.GetString("checkBoxSendHostName.ToolTip"));
            this.checkBoxSendHostName.UseVisualStyleBackColor = true;
            this.checkBoxSendHostName.CheckedChanged += new System.EventHandler(this.checkBoxSendHostName_CheckedChanged);
            // 
            // checkBoxIpv6
            // 
            resources.ApplyResources(this.checkBoxIpv6, "checkBoxIpv6");
            this.checkBoxIpv6.Name = "checkBoxIpv6";
            this.toolTip.SetToolTip(this.checkBoxIpv6, resources.GetString("checkBoxIpv6.ToolTip"));
            this.checkBoxIpv6.UseVisualStyleBackColor = true;
            this.checkBoxIpv6.CheckedChanged += new System.EventHandler(this.checkBoxIpv6_CheckedChanged);
            // 
            // radioHttp
            // 
            resources.ApplyResources(this.radioHttp, "radioHttp");
            this.radioHttp.Checked = true;
            this.radioHttp.Name = "radioHttp";
            this.radioHttp.TabStop = true;
            this.toolTip.SetToolTip(this.radioHttp, resources.GetString("radioHttp.ToolTip"));
            this.radioHttp.UseVisualStyleBackColor = true;
            this.radioHttp.CheckedChanged += new System.EventHandler(this.radioHttp_CheckedChanged);
            // 
            // radioSocksv4
            // 
            resources.ApplyResources(this.radioSocksv4, "radioSocksv4");
            this.radioSocksv4.Name = "radioSocksv4";
            this.toolTip.SetToolTip(this.radioSocksv4, resources.GetString("radioSocksv4.ToolTip"));
            this.radioSocksv4.UseVisualStyleBackColor = true;
            this.radioSocksv4.CheckedChanged += new System.EventHandler(this.radioSocksv4_CheckedChanged);
            // 
            // radioSocksv5
            // 
            resources.ApplyResources(this.radioSocksv5, "radioSocksv5");
            this.radioSocksv5.Name = "radioSocksv5";
            this.toolTip.SetToolTip(this.radioSocksv5, resources.GetString("radioSocksv5.ToolTip"));
            this.radioSocksv5.UseVisualStyleBackColor = true;
            this.radioSocksv5.CheckedChanged += new System.EventHandler(this.radioSocksv5_CheckedChanged);
            // 
            // numericUpDownPort
            // 
            resources.ApplyResources(this.numericUpDownPort, "numericUpDownPort");
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.toolTip.SetToolTip(this.numericUpDownPort, resources.GetString("numericUpDownPort.ToolTip"));
            this.numericUpDownPort.ValueChanged += new System.EventHandler(this.numericUpDownPort_ValueChanged);
            // 
            // textBoxHost
            // 
            resources.ApplyResources(this.textBoxHost, "textBoxHost");
            this.textBoxHost.Name = "textBoxHost";
            this.toolTip.SetToolTip(this.textBoxHost, resources.GetString("textBoxHost.ToolTip"));
            this.textBoxHost.TextChanged += new System.EventHandler(this.textBoxHost_TextChanged);
            // 
            // ProxyClientControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(groupBoxConnectionType);
            this.Name = "ProxyClientControl";
            this.Load += new System.EventHandler(this.ProxyClientControl_Load);
            groupBoxConnectionType.ResumeLayout(false);
            groupBoxConnectionType.PerformLayout();
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioDirect;
        private System.Windows.Forms.RadioButton radioProxy;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.RadioButton radioSocksv4;
        private System.Windows.Forms.RadioButton radioSocksv5;
        private System.Windows.Forms.RadioButton radioHttp;
        private System.Windows.Forms.CheckBox checkBoxIpv6;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.RadioButton radioSystem;
        private System.Windows.Forms.CheckBox checkBoxSendHostName;
        private System.Windows.Forms.RadioButton radioDefault;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RadioButton radioButtonPac;
        private System.Windows.Forms.Button btnPACScript;
    }
}
