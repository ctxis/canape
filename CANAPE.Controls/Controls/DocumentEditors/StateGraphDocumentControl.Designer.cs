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
    partial class StateGraphDocumentControl
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
            System.Windows.Forms.Label lblMetaName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateGraphDocumentControl));
            System.Windows.Forms.ColumnHeader columnHeaderState;
            System.Windows.Forms.ColumnHeader columnHeaderGraph;
            this.textBoxMetaName = new System.Windows.Forms.TextBox();
            this.lblDefaultState = new System.Windows.Forms.Label();
            this.comboDefaultState = new System.Windows.Forms.ComboBox();
            this.checkGlobalMeta = new System.Windows.Forms.CheckBox();
            this.listViewStateEntries = new System.Windows.Forms.ListView();
            this.lblStateEntries = new System.Windows.Forms.Label();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySetMetaNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            lblMetaName = new System.Windows.Forms.Label();
            columnHeaderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderGraph = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMetaName
            // 
            resources.ApplyResources(lblMetaName, "lblMetaName");
            lblMetaName.Name = "lblMetaName";
            // 
            // columnHeaderState
            // 
            resources.ApplyResources(columnHeaderState, "columnHeaderState");
            // 
            // columnHeaderGraph
            // 
            resources.ApplyResources(columnHeaderGraph, "columnHeaderGraph");
            // 
            // textBoxMetaName
            // 
            resources.ApplyResources(this.textBoxMetaName, "textBoxMetaName");
            this.textBoxMetaName.Name = "textBoxMetaName";
            this.toolTip.SetToolTip(this.textBoxMetaName, resources.GetString("textBoxMetaName.ToolTip"));
            this.textBoxMetaName.TextChanged += new System.EventHandler(this.textBoxMetaName_TextChanged);
            // 
            // lblDefaultState
            // 
            resources.ApplyResources(this.lblDefaultState, "lblDefaultState");
            this.lblDefaultState.Name = "lblDefaultState";
            // 
            // comboDefaultState
            // 
            this.comboDefaultState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDefaultState.FormattingEnabled = true;
            resources.ApplyResources(this.comboDefaultState, "comboDefaultState");
            this.comboDefaultState.Name = "comboDefaultState";
            this.toolTip.SetToolTip(this.comboDefaultState, resources.GetString("comboDefaultState.ToolTip"));
            this.comboDefaultState.DropDown += new System.EventHandler(this.comboDefaultState_DropDown);
            this.comboDefaultState.SelectedIndexChanged += new System.EventHandler(this.comboDefaultState_SelectedIndexChanged);
            // 
            // checkGlobalMeta
            // 
            resources.ApplyResources(this.checkGlobalMeta, "checkGlobalMeta");
            this.checkGlobalMeta.Name = "checkGlobalMeta";
            this.toolTip.SetToolTip(this.checkGlobalMeta, resources.GetString("checkGlobalMeta.ToolTip"));
            this.checkGlobalMeta.UseVisualStyleBackColor = true;
            // 
            // listViewStateEntries
            // 
            resources.ApplyResources(this.listViewStateEntries, "listViewStateEntries");
            this.listViewStateEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderState,
            columnHeaderGraph});
            this.listViewStateEntries.ContextMenuStrip = this.contextMenuStrip;
            this.listViewStateEntries.FullRowSelect = true;
            this.listViewStateEntries.HideSelection = false;
            this.listViewStateEntries.MultiSelect = false;
            this.listViewStateEntries.Name = "listViewStateEntries";
            this.toolTip.SetToolTip(this.listViewStateEntries, resources.GetString("listViewStateEntries.ToolTip"));
            this.listViewStateEntries.UseCompatibleStateImageBehavior = false;
            this.listViewStateEntries.View = System.Windows.Forms.View.Details;
            this.listViewStateEntries.SelectedIndexChanged += new System.EventHandler(this.listViewStateEntries_SelectedIndexChanged);
            // 
            // lblStateEntries
            // 
            resources.ApplyResources(this.lblStateEntries, "lblStateEntries");
            this.lblStateEntries.Name = "lblStateEntries";
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.toolTip.SetToolTip(this.btnAdd, resources.GetString("btnAdd.ToolTip"));
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.toolTip.SetToolTip(this.btnRemove, resources.GetString("btnRemove.ToolTip"));
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.copySetMetaNodeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            this.addToolStripMenuItem.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(this.removeToolStripMenuItem, "removeToolStripMenuItem");
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // copySetMetaNodeToolStripMenuItem
            // 
            this.copySetMetaNodeToolStripMenuItem.Name = "copySetMetaNodeToolStripMenuItem";
            resources.ApplyResources(this.copySetMetaNodeToolStripMenuItem, "copySetMetaNodeToolStripMenuItem");
            this.copySetMetaNodeToolStripMenuItem.Click += new System.EventHandler(this.copySetMetaNodeToolStripMenuItem_Click);
            // 
            // StateGraphDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.lblStateEntries);
            this.Controls.Add(this.listViewStateEntries);
            this.Controls.Add(this.checkGlobalMeta);
            this.Controls.Add(this.comboDefaultState);
            this.Controls.Add(this.lblDefaultState);
            this.Controls.Add(lblMetaName);
            this.Controls.Add(this.textBoxMetaName);
            this.Name = "StateGraphDocumentControl";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMetaName;
        private System.Windows.Forms.Label lblDefaultState;
        private System.Windows.Forms.ComboBox comboDefaultState;
        private System.Windows.Forms.CheckBox checkGlobalMeta;
        private System.Windows.Forms.ListView listViewStateEntries;
        private System.Windows.Forms.Label lblStateEntries;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySetMetaNodeToolStripMenuItem;
    }
}
