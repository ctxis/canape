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
    partial class StartupForm
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
            System.Windows.Forms.Label lblNew;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupForm));
            System.Windows.Forms.Label lblOpen;
            System.Windows.Forms.Label lblEmpty;
            System.Windows.Forms.ColumnHeader columnHeaderFileName;
            System.Windows.Forms.PictureBox pictureBox;
            this.btnNew = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblPrevious = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnEmpty = new System.Windows.Forms.Button();
            this.checkBoxShowAtStartup = new System.Windows.Forms.CheckBox();
            this.listViewPrevious = new System.Windows.Forms.ListView();
            lblNew = new System.Windows.Forms.Label();
            lblOpen = new System.Windows.Forms.Label();
            lblEmpty = new System.Windows.Forms.Label();
            columnHeaderFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNew
            // 
            resources.ApplyResources(lblNew, "lblNew");
            lblNew.Name = "lblNew";
            // 
            // lblOpen
            // 
            resources.ApplyResources(lblOpen, "lblOpen");
            lblOpen.Name = "lblOpen";
            // 
            // lblEmpty
            // 
            resources.ApplyResources(lblEmpty, "lblEmpty");
            lblEmpty.Name = "lblEmpty";
            // 
            // columnHeaderFileName
            // 
            resources.ApplyResources(columnHeaderFileName, "columnHeaderFileName");
            // 
            // pictureBox
            // 
            resources.ApplyResources(pictureBox, "pictureBox");
            pictureBox.Name = "pictureBox";
            pictureBox.TabStop = false;
            // 
            // btnNew
            // 
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lblPrevious
            // 
            resources.ApplyResources(this.lblPrevious, "lblPrevious");
            this.lblPrevious.Name = "lblPrevious";
            // 
            // btnPrevious
            // 
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnEmpty
            // 
            this.btnEmpty.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnEmpty, "btnEmpty");
            this.btnEmpty.Name = "btnEmpty";
            this.btnEmpty.UseVisualStyleBackColor = true;
            this.btnEmpty.Click += new System.EventHandler(this.btnEmpty_Click);
            // 
            // checkBoxShowAtStartup
            // 
            resources.ApplyResources(this.checkBoxShowAtStartup, "checkBoxShowAtStartup");
            this.checkBoxShowAtStartup.Checked = true;
            this.checkBoxShowAtStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowAtStartup.Name = "checkBoxShowAtStartup";
            this.checkBoxShowAtStartup.UseVisualStyleBackColor = true;
            this.checkBoxShowAtStartup.CheckedChanged += new System.EventHandler(this.checkBoxShowAtStartup_CheckedChanged);
            // 
            // listViewPrevious
            // 
            this.listViewPrevious.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderFileName});
            this.listViewPrevious.FullRowSelect = true;
            this.listViewPrevious.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            resources.ApplyResources(this.listViewPrevious, "listViewPrevious");
            this.listViewPrevious.MultiSelect = false;
            this.listViewPrevious.Name = "listViewPrevious";
            this.listViewPrevious.ShowItemToolTips = true;
            this.listViewPrevious.UseCompatibleStateImageBehavior = false;
            this.listViewPrevious.View = System.Windows.Forms.View.Details;
            this.listViewPrevious.DoubleClick += new System.EventHandler(this.btnPrevious_Click);
            // 
            // StartupForm
            // 
            this.AcceptButton = this.btnNew;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnEmpty;
            this.Controls.Add(pictureBox);
            this.Controls.Add(this.listViewPrevious);
            this.Controls.Add(this.checkBoxShowAtStartup);
            this.Controls.Add(lblEmpty);
            this.Controls.Add(this.btnEmpty);
            this.Controls.Add(this.lblPrevious);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(lblOpen);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(lblNew);
            this.Controls.Add(this.btnNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartupForm";
            this.Load += new System.EventHandler(this.NewProjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnEmpty;
        private System.Windows.Forms.Label lblPrevious;
        private System.Windows.Forms.CheckBox checkBoxShowAtStartup;
        private System.Windows.Forms.ListView listViewPrevious;

    }
}