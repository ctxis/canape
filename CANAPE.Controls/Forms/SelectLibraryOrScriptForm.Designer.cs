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
    partial class SelectLibraryOrScriptForm
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
            System.Windows.Forms.ColumnHeader columnHeaderName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLibraryOrScriptForm));
            System.Windows.Forms.ColumnHeader columnHeaderDescription;
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageLibrary = new System.Windows.Forms.TabPage();
            this.listViewNodes = new System.Windows.Forms.ListView();
            this.tabPageScript = new System.Windows.Forms.TabPage();
            this.scriptSelectionControl = new CANAPE.Controls.ScriptSelectionControl();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl.SuspendLayout();
            this.tabPageLibrary.SuspendLayout();
            this.tabPageScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderDescription
            // 
            resources.ApplyResources(columnHeaderDescription, "columnHeaderDescription");
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageLibrary);
            this.tabControl.Controls.Add(this.tabPageScript);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageLibrary
            // 
            this.tabPageLibrary.Controls.Add(this.listViewNodes);
            resources.ApplyResources(this.tabPageLibrary, "tabPageLibrary");
            this.tabPageLibrary.Name = "tabPageLibrary";
            this.tabPageLibrary.UseVisualStyleBackColor = true;
            // 
            // listViewNodes
            // 
            this.listViewNodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderName,
            columnHeaderDescription});
            resources.ApplyResources(this.listViewNodes, "listViewNodes");
            this.listViewNodes.FullRowSelect = true;
            this.listViewNodes.GridLines = true;
            this.listViewNodes.MultiSelect = false;
            this.listViewNodes.Name = "listViewNodes";
            this.listViewNodes.UseCompatibleStateImageBehavior = false;
            this.listViewNodes.View = System.Windows.Forms.View.Details;
            this.listViewNodes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewNodes_ColumnClick);
            this.listViewNodes.DoubleClick += new System.EventHandler(this.listViewNodes_DoubleClick);
            // 
            // tabPageScript
            // 
            this.tabPageScript.Controls.Add(this.scriptSelectionControl);
            resources.ApplyResources(this.tabPageScript, "tabPageScript");
            this.tabPageScript.Name = "tabPageScript";
            this.tabPageScript.UseVisualStyleBackColor = true;
            // 
            // scriptSelectionControl
            // 
            this.scriptSelectionControl.ClassName = null;
            this.scriptSelectionControl.Document = null;
            resources.ApplyResources(this.scriptSelectionControl, "scriptSelectionControl");
            this.scriptSelectionControl.Name = "scriptSelectionControl";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // SelectTemplateOrScriptForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectTemplateOrScriptForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SelectServerForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageLibrary.ResumeLayout(false);
            this.tabPageScript.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLibrary;
        private System.Windows.Forms.TabPage tabPageScript;
        private System.Windows.Forms.ListView listViewNodes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Controls.ScriptSelectionControl scriptSelectionControl;
    }
}