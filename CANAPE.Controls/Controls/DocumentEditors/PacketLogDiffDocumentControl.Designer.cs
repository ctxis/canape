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
    partial class PacketLogDiffDocumentControl
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
            System.Windows.Forms.Label labelLeft;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketLogDiffDocumentControl));
            System.Windows.Forms.Label labelRight;
            System.Windows.Forms.Label labelOutput;
            System.Windows.Forms.Label labelAdded;
            System.Windows.Forms.Label labelDeleted;
            System.Windows.Forms.Label labelModified;
            this.btnDiff = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.packetLogControlLeft = new CANAPE.Controls.PacketLogControl();
            this.packetLogControlRight = new CANAPE.Controls.PacketLogControl();
            this.splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.treeViewOutput = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewBinaryDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxByteCompare = new System.Windows.Forms.CheckBox();
            this.expandDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            labelLeft = new System.Windows.Forms.Label();
            labelRight = new System.Windows.Forms.Label();
            labelOutput = new System.Windows.Forms.Label();
            labelAdded = new System.Windows.Forms.Label();
            labelDeleted = new System.Windows.Forms.Label();
            labelModified = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).BeginInit();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLeft
            // 
            resources.ApplyResources(labelLeft, "labelLeft");
            labelLeft.Name = "labelLeft";
            // 
            // labelRight
            // 
            resources.ApplyResources(labelRight, "labelRight");
            labelRight.Name = "labelRight";
            // 
            // labelOutput
            // 
            resources.ApplyResources(labelOutput, "labelOutput");
            labelOutput.Name = "labelOutput";
            // 
            // labelAdded
            // 
            resources.ApplyResources(labelAdded, "labelAdded");
            labelAdded.BackColor = System.Drawing.Color.Cyan;
            labelAdded.Name = "labelAdded";
            // 
            // labelDeleted
            // 
            resources.ApplyResources(labelDeleted, "labelDeleted");
            labelDeleted.BackColor = System.Drawing.Color.Red;
            labelDeleted.Name = "labelDeleted";
            // 
            // labelModified
            // 
            resources.ApplyResources(labelModified, "labelModified");
            labelModified.BackColor = System.Drawing.Color.Yellow;
            labelModified.Name = "labelModified";
            // 
            // btnDiff
            // 
            resources.ApplyResources(this.btnDiff, "btnDiff");
            this.btnDiff.Name = "btnDiff";
            this.btnDiff.UseVisualStyleBackColor = true;
            this.btnDiff.Click += new System.EventHandler(this.btnDiff_Click);
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.packetLogControlLeft);
            this.splitContainer.Panel1.Controls.Add(labelLeft);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.packetLogControlRight);
            this.splitContainer.Panel2.Controls.Add(labelRight);
            // 
            // packetLogControlLeft
            // 
            resources.ApplyResources(this.packetLogControlLeft, "packetLogControlLeft");
            this.packetLogControlLeft.IsInFindForm = false;
            this.packetLogControlLeft.LogName = null;
            this.packetLogControlLeft.Name = "packetLogControlLeft";
            this.packetLogControlLeft.ReadOnly = false;
            this.packetLogControlLeft.ConfigChanged += new System.EventHandler(this.packetLogControlLeft_ConfigChanged);
            // 
            // packetLogControlRight
            // 
            resources.ApplyResources(this.packetLogControlRight, "packetLogControlRight");
            this.packetLogControlRight.IsInFindForm = false;
            this.packetLogControlRight.LogName = null;
            this.packetLogControlRight.Name = "packetLogControlRight";
            this.packetLogControlRight.ReadOnly = false;
            this.packetLogControlRight.ConfigChanged += new System.EventHandler(this.packetLogControlRight_ConfigChanged);
            // 
            // splitContainerOuter
            // 
            resources.ApplyResources(this.splitContainerOuter, "splitContainerOuter");
            this.splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            this.splitContainerOuter.Panel1.Controls.Add(this.splitContainer);
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(labelAdded);
            this.splitContainerOuter.Panel2.Controls.Add(labelDeleted);
            this.splitContainerOuter.Panel2.Controls.Add(labelModified);
            this.splitContainerOuter.Panel2.Controls.Add(this.treeViewOutput);
            this.splitContainerOuter.Panel2.Controls.Add(labelOutput);
            // 
            // treeViewOutput
            // 
            resources.ApplyResources(this.treeViewOutput, "treeViewOutput");
            this.treeViewOutput.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewOutput.FullRowSelect = true;
            this.treeViewOutput.Name = "treeViewOutput";
            this.treeViewOutput.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewOutput_BeforeExpand);
            this.treeViewOutput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewOutput_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewBinaryDiffToolStripMenuItem,
            this.expandDiffToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // viewBinaryDiffToolStripMenuItem
            // 
            this.viewBinaryDiffToolStripMenuItem.Name = "viewBinaryDiffToolStripMenuItem";
            resources.ApplyResources(this.viewBinaryDiffToolStripMenuItem, "viewBinaryDiffToolStripMenuItem");
            this.viewBinaryDiffToolStripMenuItem.Click += new System.EventHandler(this.viewBinaryDiffToolStripMenuItem_Click);
            // 
            // checkBoxByteCompare
            // 
            resources.ApplyResources(this.checkBoxByteCompare, "checkBoxByteCompare");
            this.checkBoxByteCompare.Name = "checkBoxByteCompare";
            this.checkBoxByteCompare.UseVisualStyleBackColor = true;
            // 
            // expandDiffToolStripMenuItem
            // 
            this.expandDiffToolStripMenuItem.Name = "expandDiffToolStripMenuItem";
            resources.ApplyResources(this.expandDiffToolStripMenuItem, "expandDiffToolStripMenuItem");
            this.expandDiffToolStripMenuItem.Click += new System.EventHandler(this.expandDiffToolStripMenuItem_Click);
            // 
            // PacketLogDiffDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxByteCompare);
            this.Controls.Add(this.splitContainerOuter);
            this.Controls.Add(this.btnDiff);
            this.Name = "PacketLogDiffDocumentControl";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainerOuter.Panel1.ResumeLayout(false);
            this.splitContainerOuter.Panel2.ResumeLayout(false);
            this.splitContainerOuter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).EndInit();
            this.splitContainerOuter.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDiff;
        private PacketLogControl packetLogControlLeft;
        private PacketLogControl packetLogControlRight;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainerOuter;
        private System.Windows.Forms.CheckBox checkBoxByteCompare;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewBinaryDiffToolStripMenuItem;
        private System.Windows.Forms.TreeView treeViewOutput;
        private System.Windows.Forms.ToolStripMenuItem expandDiffToolStripMenuItem;

    }
}
