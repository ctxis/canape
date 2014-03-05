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
    partial class ScriptOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptOptionsForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxEnableDebug = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageReferences = new System.Windows.Forms.TabPage();
            this.assemblyNameListView = new CANAPE.Controls.AssemblyNameListView();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddReference = new System.Windows.Forms.Button();
            this.tabPageDebugging = new System.Windows.Forms.TabPage();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.dynamicConfigEditorControl = new CANAPE.Controls.DynamicConfigEditorControl();
            label1 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageReferences.SuspendLayout();
            this.tabPageDebugging.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
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
            // checkBoxEnableDebug
            // 
            resources.ApplyResources(this.checkBoxEnableDebug, "checkBoxEnableDebug");
            this.checkBoxEnableDebug.Name = "checkBoxEnableDebug";
            this.checkBoxEnableDebug.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageReferences);
            this.tabControl.Controls.Add(this.tabPageDebugging);
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageReferences
            // 
            this.tabPageReferences.Controls.Add(this.assemblyNameListView);
            this.tabPageReferences.Controls.Add(this.btnRemove);
            this.tabPageReferences.Controls.Add(this.btnAddReference);
            resources.ApplyResources(this.tabPageReferences, "tabPageReferences");
            this.tabPageReferences.Name = "tabPageReferences";
            this.tabPageReferences.UseVisualStyleBackColor = true;
            // 
            // assemblyNameListView
            // 
            resources.ApplyResources(this.assemblyNameListView, "assemblyNameListView");
            this.assemblyNameListView.FullRowSelect = true;
            this.assemblyNameListView.Name = "assemblyNameListView";
            this.assemblyNameListView.UseCompatibleStateImageBehavior = false;
            this.assemblyNameListView.View = System.Windows.Forms.View.Details;
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddReference
            // 
            resources.ApplyResources(this.btnAddReference, "btnAddReference");
            this.btnAddReference.Name = "btnAddReference";
            this.btnAddReference.UseVisualStyleBackColor = true;
            this.btnAddReference.Click += new System.EventHandler(this.btnAddReference_Click);
            // 
            // tabPageDebugging
            // 
            this.tabPageDebugging.Controls.Add(this.checkBoxEnableDebug);
            this.tabPageDebugging.Controls.Add(label1);
            resources.ApplyResources(this.tabPageDebugging, "tabPageDebugging");
            this.tabPageDebugging.Name = "tabPageDebugging";
            this.tabPageDebugging.UseVisualStyleBackColor = true;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.dynamicConfigEditorControl);
            resources.ApplyResources(this.tabPageConfig, "tabPageConfig");
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // dynamicConfigEditorControl
            // 
            resources.ApplyResources(this.dynamicConfigEditorControl, "dynamicConfigEditorControl");
            this.dynamicConfigEditorControl.Name = "dynamicConfigEditorControl";
            // 
            // ScriptOptionsForm
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
            this.Name = "ScriptOptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ScriptOptionsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageReferences.ResumeLayout(false);
            this.tabPageDebugging.ResumeLayout(false);
            this.tabPageDebugging.PerformLayout();
            this.tabPageConfig.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBoxEnableDebug;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageReferences;
        private System.Windows.Forms.TabPage tabPageDebugging;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAddReference;
        private Controls.AssemblyNameListView assemblyNameListView;
        private System.Windows.Forms.TabPage tabPageConfig;
        private Controls.DynamicConfigEditorControl dynamicConfigEditorControl;
    }
}