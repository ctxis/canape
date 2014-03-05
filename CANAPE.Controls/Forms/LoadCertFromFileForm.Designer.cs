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
    partial class LoadCertFromFileForm
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
            System.Windows.Forms.Label labelCert;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadCertFromFileForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxPassword = new System.Windows.Forms.CheckBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.checkBoxKey = new System.Windows.Forms.CheckBox();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.textBoxCertificate = new System.Windows.Forms.TextBox();
            this.btnBrowseCert = new System.Windows.Forms.Button();
            this.btnBrowseKey = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            labelCert = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelCert
            // 
            resources.ApplyResources(labelCert, "labelCert");
            labelCert.Name = "labelCert";
            this.toolTip.SetToolTip(labelCert, resources.GetString("labelCert.ToolTip"));
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.toolTip.SetToolTip(this.btnOk, resources.GetString("btnOk.ToolTip"));
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.toolTip.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxPassword
            // 
            resources.ApplyResources(this.checkBoxPassword, "checkBoxPassword");
            this.checkBoxPassword.Name = "checkBoxPassword";
            this.toolTip.SetToolTip(this.checkBoxPassword, resources.GetString("checkBoxPassword.ToolTip"));
            this.checkBoxPassword.UseVisualStyleBackColor = true;
            this.checkBoxPassword.CheckedChanged += new System.EventHandler(this.checkBoxPassword_CheckedChanged);
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            this.toolTip.SetToolTip(this.textBoxPassword, resources.GetString("textBoxPassword.ToolTip"));
            // 
            // checkBoxKey
            // 
            resources.ApplyResources(this.checkBoxKey, "checkBoxKey");
            this.checkBoxKey.Name = "checkBoxKey";
            this.toolTip.SetToolTip(this.checkBoxKey, resources.GetString("checkBoxKey.ToolTip"));
            this.checkBoxKey.UseVisualStyleBackColor = true;
            this.checkBoxKey.CheckedChanged += new System.EventHandler(this.checkBoxKey_CheckedChanged);
            // 
            // textBoxKey
            // 
            resources.ApplyResources(this.textBoxKey, "textBoxKey");
            this.textBoxKey.Name = "textBoxKey";
            this.toolTip.SetToolTip(this.textBoxKey, resources.GetString("textBoxKey.ToolTip"));
            // 
            // textBoxCertificate
            // 
            resources.ApplyResources(this.textBoxCertificate, "textBoxCertificate");
            this.textBoxCertificate.Name = "textBoxCertificate";
            this.toolTip.SetToolTip(this.textBoxCertificate, resources.GetString("textBoxCertificate.ToolTip"));
            // 
            // btnBrowseCert
            // 
            resources.ApplyResources(this.btnBrowseCert, "btnBrowseCert");
            this.btnBrowseCert.Name = "btnBrowseCert";
            this.toolTip.SetToolTip(this.btnBrowseCert, resources.GetString("btnBrowseCert.ToolTip"));
            this.btnBrowseCert.UseVisualStyleBackColor = true;
            this.btnBrowseCert.Click += new System.EventHandler(this.btnBrowseCert_Click);
            // 
            // btnBrowseKey
            // 
            resources.ApplyResources(this.btnBrowseKey, "btnBrowseKey");
            this.btnBrowseKey.Name = "btnBrowseKey";
            this.toolTip.SetToolTip(this.btnBrowseKey, resources.GetString("btnBrowseKey.ToolTip"));
            this.btnBrowseKey.UseVisualStyleBackColor = true;
            this.btnBrowseKey.Click += new System.EventHandler(this.btnBrowseKey_Click);
            // 
            // LoadCertFromFileForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(labelCert);
            this.Controls.Add(this.btnBrowseKey);
            this.Controls.Add(this.btnBrowseCert);
            this.Controls.Add(this.textBoxCertificate);
            this.Controls.Add(this.textBoxKey);
            this.Controls.Add(this.checkBoxKey);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.checkBoxPassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoadCertFromFileForm";
            this.ShowInTaskbar = false;
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBoxPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.CheckBox checkBoxKey;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.TextBox textBoxCertificate;
        private System.Windows.Forms.Button btnBrowseCert;
        private System.Windows.Forms.Button btnBrowseKey;
        private System.Windows.Forms.ToolTip toolTip;
    }
}