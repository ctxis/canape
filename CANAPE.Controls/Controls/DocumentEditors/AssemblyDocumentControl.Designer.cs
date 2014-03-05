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
    partial class AssemblyDocumentControl
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
            System.Windows.Forms.Label lblPath;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyDocumentControl));
            System.Windows.Forms.Label lblAsmName;
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.lblAsmNameValue = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            lblPath = new System.Windows.Forms.Label();
            lblAsmName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPath
            // 
            resources.ApplyResources(lblPath, "lblPath");
            lblPath.Name = "lblPath";
            this.toolTip.SetToolTip(lblPath, resources.GetString("lblPath.ToolTip"));
            // 
            // lblAsmName
            // 
            resources.ApplyResources(lblAsmName, "lblAsmName");
            lblAsmName.Name = "lblAsmName";
            this.toolTip.SetToolTip(lblAsmName, resources.GetString("lblAsmName.ToolTip"));
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            this.toolTip.SetToolTip(this.textBoxPath, resources.GetString("textBoxPath.ToolTip"));
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.toolTip.SetToolTip(this.btnBrowse, resources.GetString("btnBrowse.ToolTip"));
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnReload
            // 
            resources.ApplyResources(this.btnReload, "btnReload");
            this.btnReload.Name = "btnReload";
            this.toolTip.SetToolTip(this.btnReload, resources.GetString("btnReload.ToolTip"));
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // lblAsmNameValue
            // 
            resources.ApplyResources(this.lblAsmNameValue, "lblAsmNameValue");
            this.lblAsmNameValue.Name = "lblAsmNameValue";
            this.toolTip.SetToolTip(this.lblAsmNameValue, resources.GetString("lblAsmNameValue.ToolTip"));
            // 
            // AssemblyDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAsmNameValue);
            this.Controls.Add(lblAsmName);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(lblPath);
            this.Controls.Add(this.textBoxPath);
            this.Name = "AssemblyDocumentControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Label lblAsmNameValue;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
