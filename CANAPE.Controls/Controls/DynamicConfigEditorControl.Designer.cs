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
    partial class DynamicConfigEditorControl
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
            System.Windows.Forms.ColumnHeader columnHeaderName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DynamicConfigEditorControl));
            System.Windows.Forms.ColumnHeader columnHeaderType;
            System.Windows.Forms.ColumnHeader columnHeaderDefaultValue;
            System.Windows.Forms.ColumnHeader columnHeaderDescription;
            this.listViewConfig = new System.Windows.Forms.ListView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removePropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderDefaultValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderType
            // 
            resources.ApplyResources(columnHeaderType, "columnHeaderType");
            // 
            // columnHeaderDefaultValue
            // 
            resources.ApplyResources(columnHeaderDefaultValue, "columnHeaderDefaultValue");
            // 
            // columnHeaderDescription
            // 
            resources.ApplyResources(columnHeaderDescription, "columnHeaderDescription");
            // 
            // listViewConfig
            // 
            resources.ApplyResources(this.listViewConfig, "listViewConfig");
            this.listViewConfig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderName,
            columnHeaderType,
            columnHeaderDefaultValue,
            columnHeaderDescription});
            this.listViewConfig.FullRowSelect = true;
            this.listViewConfig.MultiSelect = false;
            this.listViewConfig.Name = "listViewConfig";
            this.listViewConfig.UseCompatibleStateImageBehavior = false;
            this.listViewConfig.View = System.Windows.Forms.View.Details;
            this.listViewConfig.DoubleClick += new System.EventHandler(this.editPropertyToolStripMenuItem_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPropertyToolStripMenuItem,
            this.editPropertyToolStripMenuItem,
            this.removePropertyToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // addPropertyToolStripMenuItem
            // 
            this.addPropertyToolStripMenuItem.Name = "addPropertyToolStripMenuItem";
            resources.ApplyResources(this.addPropertyToolStripMenuItem, "addPropertyToolStripMenuItem");
            this.addPropertyToolStripMenuItem.Click += new System.EventHandler(this.addPropertyToolStripMenuItem_Click);
            // 
            // editPropertyToolStripMenuItem
            // 
            this.editPropertyToolStripMenuItem.Name = "editPropertyToolStripMenuItem";
            resources.ApplyResources(this.editPropertyToolStripMenuItem, "editPropertyToolStripMenuItem");
            this.editPropertyToolStripMenuItem.Click += new System.EventHandler(this.editPropertyToolStripMenuItem_Click);
            // 
            // removePropertyToolStripMenuItem
            // 
            this.removePropertyToolStripMenuItem.Name = "removePropertyToolStripMenuItem";
            resources.ApplyResources(this.removePropertyToolStripMenuItem, "removePropertyToolStripMenuItem");
            this.removePropertyToolStripMenuItem.Click += new System.EventHandler(this.removePropertyToolStripMenuItem_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.toolTip.SetToolTip(this.btnAdd, resources.GetString("btnAdd.ToolTip"));
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.addPropertyToolStripMenuItem_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.toolTip.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.removePropertyToolStripMenuItem_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.toolTip.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.editPropertyToolStripMenuItem_Click);
            // 
            // DynamicConfigEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listViewConfig);
            this.Name = "DynamicConfigEditorControl";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewConfig;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removePropertyToolStripMenuItem;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
    }
}
