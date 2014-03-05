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
    partial class HttpProxyOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HttpProxyOptionsForm));
            System.Windows.Forms.Label labelUsername;
            System.Windows.Forms.Label labelPassword;
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSsl = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.sslConfigControl = new CANAPE.Controls.SslConfigControl();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.checkBoxHttp10 = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxEnableAuth = new System.Windows.Forms.CheckBox();
            this.groupBoxAuth = new System.Windows.Forms.GroupBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            labelUsername = new System.Windows.Forms.Label();
            labelPassword = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageSsl.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.groupBoxAuth.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageSsl);
            this.tabControl.Controls.Add(this.tabPageOptions);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageSsl
            // 
            this.tabPageSsl.Controls.Add(this.label1);
            this.tabPageSsl.Controls.Add(this.sslConfigControl);
            resources.ApplyResources(this.tabPageSsl, "tabPageSsl");
            this.tabPageSsl.Name = "tabPageSsl";
            this.tabPageSsl.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // sslConfigControl
            // 
            resources.ApplyResources(this.sslConfigControl, "sslConfigControl");
            this.sslConfigControl.LayerBinding = ((CANAPE.Net.Layers.NetworkLayerBinding)((CANAPE.Net.Layers.NetworkLayerBinding.Client | CANAPE.Net.Layers.NetworkLayerBinding.Server)));
            this.sslConfigControl.Name = "sslConfigControl";
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
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.groupBoxAuth);
            this.tabPageOptions.Controls.Add(this.checkBoxHttp10);
            resources.ApplyResources(this.tabPageOptions, "tabPageOptions");
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.UseVisualStyleBackColor = true;
            // 
            // checkBoxHttp10
            // 
            resources.ApplyResources(this.checkBoxHttp10, "checkBoxHttp10");
            this.checkBoxHttp10.Name = "checkBoxHttp10";
            this.toolTip.SetToolTip(this.checkBoxHttp10, resources.GetString("checkBoxHttp10.ToolTip"));
            this.checkBoxHttp10.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableAuth
            // 
            resources.ApplyResources(this.checkBoxEnableAuth, "checkBoxEnableAuth");
            this.checkBoxEnableAuth.Name = "checkBoxEnableAuth";
            this.checkBoxEnableAuth.UseVisualStyleBackColor = true;
            this.checkBoxEnableAuth.CheckedChanged += new System.EventHandler(this.checkBoxEnableAuth_CheckedChanged);
            // 
            // groupBoxAuth
            // 
            this.groupBoxAuth.Controls.Add(labelPassword);
            this.groupBoxAuth.Controls.Add(labelUsername);
            this.groupBoxAuth.Controls.Add(this.textBoxPassword);
            this.groupBoxAuth.Controls.Add(this.textBoxUsername);
            this.groupBoxAuth.Controls.Add(this.checkBoxEnableAuth);
            resources.ApplyResources(this.groupBoxAuth, "groupBoxAuth");
            this.groupBoxAuth.Name = "groupBoxAuth";
            this.groupBoxAuth.TabStop = false;
            // 
            // textBoxUsername
            // 
            resources.ApplyResources(this.textBoxUsername, "textBoxUsername");
            this.textBoxUsername.Name = "textBoxUsername";
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            // 
            // labelUsername
            // 
            resources.ApplyResources(labelUsername, "labelUsername");
            labelUsername.Name = "labelUsername";
            // 
            // labelPassword
            // 
            resources.ApplyResources(labelPassword, "labelPassword");
            labelPassword.Name = "labelPassword";
            // 
            // HttpProxyOptionsForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "HttpProxyOptionsForm";
            this.ShowInTaskbar = false;
            this.tabControl.ResumeLayout(false);
            this.tabPageSsl.ResumeLayout(false);
            this.tabPageSsl.PerformLayout();
            this.tabPageOptions.ResumeLayout(false);
            this.tabPageOptions.PerformLayout();
            this.groupBoxAuth.ResumeLayout(false);
            this.groupBoxAuth.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSsl;
        private Controls.SslConfigControl sslConfigControl;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.CheckBox checkBoxHttp10;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBoxAuth;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.CheckBox checkBoxEnableAuth;
        private System.Windows.Forms.TextBox textBoxPassword;
    }
}