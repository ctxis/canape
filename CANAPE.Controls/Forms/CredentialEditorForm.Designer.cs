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
    partial class CredentialEditorForm
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
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CredentialEditorForm));
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.GroupBox groupBoxPrincipal;
            System.Windows.Forms.GroupBox groupBoxCredentials;
            this.textBoxPrincipalName = new System.Windows.Forms.TextBox();
            this.textBoxPrincipalRealm = new System.Windows.Forms.TextBox();
            this.checkBoxShowPassword = new System.Windows.Forms.CheckBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxDomain = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBoxPrincipal = new System.Windows.Forms.GroupBox();
            groupBoxCredentials = new System.Windows.Forms.GroupBox();
            groupBoxPrincipal.SuspendLayout();
            groupBoxCredentials.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // groupBoxPrincipal
            // 
            groupBoxPrincipal.Controls.Add(label2);
            groupBoxPrincipal.Controls.Add(label1);
            groupBoxPrincipal.Controls.Add(this.textBoxPrincipalName);
            groupBoxPrincipal.Controls.Add(this.textBoxPrincipalRealm);
            resources.ApplyResources(groupBoxPrincipal, "groupBoxPrincipal");
            groupBoxPrincipal.Name = "groupBoxPrincipal";
            groupBoxPrincipal.TabStop = false;
            // 
            // textBoxPrincipalName
            // 
            resources.ApplyResources(this.textBoxPrincipalName, "textBoxPrincipalName");
            this.textBoxPrincipalName.Name = "textBoxPrincipalName";
            // 
            // textBoxPrincipalRealm
            // 
            resources.ApplyResources(this.textBoxPrincipalRealm, "textBoxPrincipalRealm");
            this.textBoxPrincipalRealm.Name = "textBoxPrincipalRealm";
            // 
            // groupBoxCredentials
            // 
            groupBoxCredentials.Controls.Add(this.checkBoxShowPassword);
            groupBoxCredentials.Controls.Add(label3);
            groupBoxCredentials.Controls.Add(this.textBoxUsername);
            groupBoxCredentials.Controls.Add(this.textBoxDomain);
            groupBoxCredentials.Controls.Add(this.label4);
            groupBoxCredentials.Controls.Add(this.label5);
            groupBoxCredentials.Controls.Add(this.textBoxPassword);
            resources.ApplyResources(groupBoxCredentials, "groupBoxCredentials");
            groupBoxCredentials.Name = "groupBoxCredentials";
            groupBoxCredentials.TabStop = false;
            // 
            // checkBoxShowPassword
            // 
            resources.ApplyResources(this.checkBoxShowPassword, "checkBoxShowPassword");
            this.checkBoxShowPassword.Name = "checkBoxShowPassword";
            this.checkBoxShowPassword.UseVisualStyleBackColor = true;
            this.checkBoxShowPassword.CheckedChanged += new System.EventHandler(this.checkBoxShowPassword_CheckedChanged);
            // 
            // textBoxUsername
            // 
            resources.ApplyResources(this.textBoxUsername, "textBoxUsername");
            this.textBoxUsername.Name = "textBoxUsername";
            // 
            // textBoxDomain
            // 
            resources.ApplyResources(this.textBoxDomain, "textBoxDomain");
            this.textBoxDomain.Name = "textBoxDomain";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.UseSystemPasswordChar = true;
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
            // CredentialEditorForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(groupBoxCredentials);
            this.Controls.Add(groupBoxPrincipal);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CredentialEditorForm";
            this.Load += new System.EventHandler(this.CredentialEditorForm_Load);
            groupBoxPrincipal.ResumeLayout(false);
            groupBoxPrincipal.PerformLayout();
            groupBoxCredentials.ResumeLayout(false);
            groupBoxCredentials.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textBoxPrincipalName;
        private System.Windows.Forms.TextBox textBoxPrincipalRealm;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDomain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxShowPassword;
    }
}