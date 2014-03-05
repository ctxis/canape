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
    partial class AddReferenceForm
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
            System.Windows.Forms.Label lblAsmName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddReferenceForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDotNet = new System.Windows.Forms.TabPage();
            this.listViewDotNet = new CANAPE.Controls.AssemblyNameListView();
            this.tabPageProject = new System.Windows.Forms.TabPage();
            this.listViewProject = new CANAPE.Controls.AssemblyNameListView();
            this.tabPageManual = new System.Windows.Forms.TabPage();
            this.textBoxAssemblyName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            lblAsmName = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageDotNet.SuspendLayout();
            this.tabPageProject.SuspendLayout();
            this.tabPageManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAsmName
            // 
            resources.ApplyResources(lblAsmName, "lblAsmName");
            lblAsmName.Name = "lblAsmName";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageDotNet);
            this.tabControl.Controls.Add(this.tabPageProject);
            this.tabControl.Controls.Add(this.tabPageManual);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageDotNet
            // 
            this.tabPageDotNet.Controls.Add(this.listViewDotNet);
            resources.ApplyResources(this.tabPageDotNet, "tabPageDotNet");
            this.tabPageDotNet.Name = "tabPageDotNet";
            this.tabPageDotNet.UseVisualStyleBackColor = true;
            // 
            // listViewDotNet
            // 
            resources.ApplyResources(this.listViewDotNet, "listViewDotNet");
            this.listViewDotNet.FullRowSelect = true;
            this.listViewDotNet.Name = "listViewDotNet";
            this.listViewDotNet.UseCompatibleStateImageBehavior = false;
            this.listViewDotNet.View = System.Windows.Forms.View.Details;
            this.listViewDotNet.DoubleClick += new System.EventHandler(this.btnOK_Click);
            // 
            // tabPageProject
            // 
            this.tabPageProject.Controls.Add(this.listViewProject);
            resources.ApplyResources(this.tabPageProject, "tabPageProject");
            this.tabPageProject.Name = "tabPageProject";
            this.tabPageProject.UseVisualStyleBackColor = true;
            // 
            // listViewProject
            // 
            resources.ApplyResources(this.listViewProject, "listViewProject");
            this.listViewProject.FullRowSelect = true;
            this.listViewProject.Name = "listViewProject";
            this.listViewProject.UseCompatibleStateImageBehavior = false;
            this.listViewProject.View = System.Windows.Forms.View.Details;
            this.listViewProject.DoubleClick += new System.EventHandler(this.btnOK_Click);
            // 
            // tabPageManual
            // 
            this.tabPageManual.Controls.Add(this.textBoxAssemblyName);
            this.tabPageManual.Controls.Add(lblAsmName);
            resources.ApplyResources(this.tabPageManual, "tabPageManual");
            this.tabPageManual.Name = "tabPageManual";
            this.tabPageManual.UseVisualStyleBackColor = true;
            // 
            // textBoxAssemblyName
            // 
            resources.ApplyResources(this.textBoxAssemblyName, "textBoxAssemblyName");
            this.textBoxAssemblyName.Name = "textBoxAssemblyName";
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddReferenceForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddReferenceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.AddReferenceForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageDotNet.ResumeLayout(false);
            this.tabPageProject.ResumeLayout(false);
            this.tabPageManual.ResumeLayout(false);
            this.tabPageManual.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDotNet;
        private System.Windows.Forms.TabPage tabPageProject;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabPageManual;
        private System.Windows.Forms.TextBox textBoxAssemblyName;
        private Controls.AssemblyNameListView listViewProject;
        private Controls.AssemblyNameListView listViewDotNet;
    }
}