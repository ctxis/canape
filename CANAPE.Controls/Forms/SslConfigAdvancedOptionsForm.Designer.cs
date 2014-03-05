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
    partial class SslConfigAdvancedOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SslConfigAdvancedOptionsForm));
            this.checkBoxRequireClientCert = new System.Windows.Forms.CheckBox();
            this.comboBoxServerProtocol = new System.Windows.Forms.ComboBox();
            this.lblServerProtocol = new System.Windows.Forms.Label();
            this.comboBoxClientProtocol = new System.Windows.Forms.ComboBox();
            this.lblClientProtocol = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxVerifyServerCert = new System.Windows.Forms.CheckBox();
            this.checkBoxVerifyClientCert = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageServer = new System.Windows.Forms.TabPage();
            this.tabPageClient = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl.SuspendLayout();
            this.tabPageServer.SuspendLayout();
            this.tabPageClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxRequireClientCert
            // 
            resources.ApplyResources(this.checkBoxRequireClientCert, "checkBoxRequireClientCert");
            this.checkBoxRequireClientCert.Name = "checkBoxRequireClientCert";
            this.toolTip.SetToolTip(this.checkBoxRequireClientCert, resources.GetString("checkBoxRequireClientCert.ToolTip"));
            this.checkBoxRequireClientCert.UseVisualStyleBackColor = true;
            this.checkBoxRequireClientCert.CheckedChanged += new System.EventHandler(this.checkBoxRequireClientCert_CheckedChanged);
            // 
            // comboBoxServerProtocol
            // 
            resources.ApplyResources(this.comboBoxServerProtocol, "comboBoxServerProtocol");
            this.comboBoxServerProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxServerProtocol.FormattingEnabled = true;
            this.comboBoxServerProtocol.Name = "comboBoxServerProtocol";
            this.toolTip.SetToolTip(this.comboBoxServerProtocol, resources.GetString("comboBoxServerProtocol.ToolTip"));
            // 
            // lblServerProtocol
            // 
            resources.ApplyResources(this.lblServerProtocol, "lblServerProtocol");
            this.lblServerProtocol.Name = "lblServerProtocol";
            this.toolTip.SetToolTip(this.lblServerProtocol, resources.GetString("lblServerProtocol.ToolTip"));
            // 
            // comboBoxClientProtocol
            // 
            resources.ApplyResources(this.comboBoxClientProtocol, "comboBoxClientProtocol");
            this.comboBoxClientProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClientProtocol.FormattingEnabled = true;
            this.comboBoxClientProtocol.Name = "comboBoxClientProtocol";
            this.toolTip.SetToolTip(this.comboBoxClientProtocol, resources.GetString("comboBoxClientProtocol.ToolTip"));
            // 
            // lblClientProtocol
            // 
            resources.ApplyResources(this.lblClientProtocol, "lblClientProtocol");
            this.lblClientProtocol.Name = "lblClientProtocol";
            this.toolTip.SetToolTip(this.lblClientProtocol, resources.GetString("lblClientProtocol.ToolTip"));
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.toolTip.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.toolTip.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxVerifyServerCert
            // 
            resources.ApplyResources(this.checkBoxVerifyServerCert, "checkBoxVerifyServerCert");
            this.checkBoxVerifyServerCert.Name = "checkBoxVerifyServerCert";
            this.toolTip.SetToolTip(this.checkBoxVerifyServerCert, resources.GetString("checkBoxVerifyServerCert.ToolTip"));
            this.checkBoxVerifyServerCert.UseVisualStyleBackColor = true;
            // 
            // checkBoxVerifyClientCert
            // 
            resources.ApplyResources(this.checkBoxVerifyClientCert, "checkBoxVerifyClientCert");
            this.checkBoxVerifyClientCert.Name = "checkBoxVerifyClientCert";
            this.toolTip.SetToolTip(this.checkBoxVerifyClientCert, resources.GetString("checkBoxVerifyClientCert.ToolTip"));
            this.checkBoxVerifyClientCert.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageServer);
            this.tabControl.Controls.Add(this.tabPageClient);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.toolTip.SetToolTip(this.tabControl, resources.GetString("tabControl.ToolTip"));
            // 
            // tabPageServer
            // 
            resources.ApplyResources(this.tabPageServer, "tabPageServer");
            this.tabPageServer.Controls.Add(this.comboBoxServerProtocol);
            this.tabPageServer.Controls.Add(this.checkBoxVerifyClientCert);
            this.tabPageServer.Controls.Add(this.checkBoxRequireClientCert);
            this.tabPageServer.Controls.Add(this.lblServerProtocol);
            this.tabPageServer.Name = "tabPageServer";
            this.toolTip.SetToolTip(this.tabPageServer, resources.GetString("tabPageServer.ToolTip"));
            this.tabPageServer.UseVisualStyleBackColor = true;
            // 
            // tabPageClient
            // 
            resources.ApplyResources(this.tabPageClient, "tabPageClient");
            this.tabPageClient.Controls.Add(this.lblClientProtocol);
            this.tabPageClient.Controls.Add(this.checkBoxVerifyServerCert);
            this.tabPageClient.Controls.Add(this.comboBoxClientProtocol);
            this.tabPageClient.Name = "tabPageClient";
            this.toolTip.SetToolTip(this.tabPageClient, resources.GetString("tabPageClient.ToolTip"));
            this.tabPageClient.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelMain
            // 
            resources.ApplyResources(this.tableLayoutPanelMain, "tableLayoutPanelMain");
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.toolTip.SetToolTip(this.tableLayoutPanelMain, resources.GetString("tableLayoutPanelMain.ToolTip"));
            // 
            // SslConfigAdvancedOptionsForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SslConfigAdvancedOptionsForm";
            this.ShowInTaskbar = false;
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.tabControl.ResumeLayout(false);
            this.tabPageServer.ResumeLayout(false);
            this.tabPageServer.PerformLayout();
            this.tabPageClient.ResumeLayout(false);
            this.tabPageClient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxRequireClientCert;
        private System.Windows.Forms.ComboBox comboBoxServerProtocol;
        private System.Windows.Forms.ComboBox comboBoxClientProtocol;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblServerProtocol;
        private System.Windows.Forms.Label lblClientProtocol;
        private System.Windows.Forms.CheckBox checkBoxVerifyServerCert;
        private System.Windows.Forms.CheckBox checkBoxVerifyClientCert;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TabPage tabPageServer;
        private System.Windows.Forms.TabPage tabPageClient;
        private System.Windows.Forms.ToolTip toolTip;
    }
}