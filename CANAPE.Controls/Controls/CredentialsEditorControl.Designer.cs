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
    partial class CredentialsEditorControl
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
            System.Windows.Forms.ColumnHeader columnHeaderPrincipal;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CredentialsEditorControl));
            System.Windows.Forms.ColumnHeader columnHeaderUsername;
            System.Windows.Forms.ColumnHeader columnHeaderDomain;
            this.listViewCredentials = new System.Windows.Forms.ListView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCredentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCredentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeCredentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            columnHeaderPrincipal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderDomain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeaderPrincipal
            // 
            resources.ApplyResources(columnHeaderPrincipal, "columnHeaderPrincipal");
            // 
            // columnHeaderUsername
            // 
            resources.ApplyResources(columnHeaderUsername, "columnHeaderUsername");
            // 
            // columnHeaderDomain
            // 
            resources.ApplyResources(columnHeaderDomain, "columnHeaderDomain");
            // 
            // listViewCredentials
            // 
            this.listViewCredentials.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderPrincipal,
            columnHeaderUsername,
            columnHeaderDomain});
            this.listViewCredentials.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.listViewCredentials, "listViewCredentials");
            this.listViewCredentials.FullRowSelect = true;
            this.listViewCredentials.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewCredentials.MultiSelect = false;
            this.listViewCredentials.Name = "listViewCredentials";
            this.listViewCredentials.UseCompatibleStateImageBehavior = false;
            this.listViewCredentials.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCredentialToolStripMenuItem,
            this.editCredentialToolStripMenuItem,
            this.removeCredentialToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // addCredentialToolStripMenuItem
            // 
            this.addCredentialToolStripMenuItem.Name = "addCredentialToolStripMenuItem";
            resources.ApplyResources(this.addCredentialToolStripMenuItem, "addCredentialToolStripMenuItem");
            this.addCredentialToolStripMenuItem.Click += new System.EventHandler(this.addCredentialToolStripMenuItem_Click);
            // 
            // editCredentialToolStripMenuItem
            // 
            this.editCredentialToolStripMenuItem.Name = "editCredentialToolStripMenuItem";
            resources.ApplyResources(this.editCredentialToolStripMenuItem, "editCredentialToolStripMenuItem");
            this.editCredentialToolStripMenuItem.Click += new System.EventHandler(this.editCredentialToolStripMenuItem_Click);
            // 
            // removeCredentialToolStripMenuItem
            // 
            this.removeCredentialToolStripMenuItem.Name = "removeCredentialToolStripMenuItem";
            resources.ApplyResources(this.removeCredentialToolStripMenuItem, "removeCredentialToolStripMenuItem");
            this.removeCredentialToolStripMenuItem.Click += new System.EventHandler(this.removeCredentialToolStripMenuItem_Click);
            // 
            // CredentialsEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewCredentials);
            this.Name = "CredentialsEditorControl";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewCredentials;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addCredentialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCredentialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeCredentialToolStripMenuItem;
    }
}
