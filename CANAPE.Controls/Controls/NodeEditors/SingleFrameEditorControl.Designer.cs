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

namespace CANAPE.Controls.NodeEditors
{
    partial class SingleFrameEditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleFrameEditorControl));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCopyFrame = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPasteFrame = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonConvertToBytes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.frameEditorControl = new CANAPE.Controls.NodeEditors.FrameEditorControl();
            this.timerUpdatePaste = new System.Windows.Forms.Timer(this.components);
            this.toolStripButtonLoadFromFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSnapshot = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSnapshot,
            this.toolStripButtonCopyFrame,
            this.toolStripButtonPasteFrame,
            this.toolStripButtonConvertToBytes,
            this.toolStripButtonLoadFromFile,
            this.toolStripSeparator1,
            this.toolStripButtonUndo});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(832, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "Tool Strip";
            // 
            // toolStripButtonCopyFrame
            // 
            this.toolStripButtonCopyFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopyFrame.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopyFrame.Image")));
            this.toolStripButtonCopyFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopyFrame.Name = "toolStripButtonCopyFrame";
            this.toolStripButtonCopyFrame.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCopyFrame.Text = "Copy Frame";
            this.toolStripButtonCopyFrame.Click += new System.EventHandler(this.toolStripButtonCopyFrame_Click);
            // 
            // toolStripButtonPasteFrame
            // 
            this.toolStripButtonPasteFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPasteFrame.Enabled = false;
            this.toolStripButtonPasteFrame.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPasteFrame.Image")));
            this.toolStripButtonPasteFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPasteFrame.Name = "toolStripButtonPasteFrame";
            this.toolStripButtonPasteFrame.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPasteFrame.Text = "Paste Frame";
            this.toolStripButtonPasteFrame.Click += new System.EventHandler(this.toolStripButtonPasteFrame_Click);
            // 
            // toolStripButtonConvertToBytes
            // 
            this.toolStripButtonConvertToBytes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonConvertToBytes.Enabled = false;
            this.toolStripButtonConvertToBytes.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonConvertToBytes.Image")));
            this.toolStripButtonConvertToBytes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConvertToBytes.Name = "toolStripButtonConvertToBytes";
            this.toolStripButtonConvertToBytes.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonConvertToBytes.Text = "Convert to Bytes";
            this.toolStripButtonConvertToBytes.Click += new System.EventHandler(this.toolStripButtonConvertToBytes_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Enabled = false;
            this.toolStripButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndo.Image")));
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUndo.Text = "Undo";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // frameEditorControl
            // 
            this.frameEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frameEditorControl.Location = new System.Drawing.Point(3, 28);
            this.frameEditorControl.Name = "frameEditorControl";
            this.frameEditorControl.ReadOnly = false;
            this.frameEditorControl.Size = new System.Drawing.Size(826, 450);
            this.frameEditorControl.TabIndex = 1;
            // 
            // timerUpdatePaste
            // 
            this.timerUpdatePaste.Interval = 1000;
            this.timerUpdatePaste.Tick += new System.EventHandler(this.timerUpdatePaste_Tick);
            // 
            // toolStripButtonLoadFromFile
            // 
            this.toolStripButtonLoadFromFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLoadFromFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLoadFromFile.Image")));
            this.toolStripButtonLoadFromFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoadFromFile.Name = "toolStripButtonLoadFromFile";
            this.toolStripButtonLoadFromFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLoadFromFile.Text = "Load From File";
            this.toolStripButtonLoadFromFile.Click += new System.EventHandler(this.toolStripButtonLoadFromFile_Click);
            // 
            // toolStripButtonSnapshot
            // 
            this.toolStripButtonSnapshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSnapshot.Enabled = false;
            this.toolStripButtonSnapshot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSnapshot.Image")));
            this.toolStripButtonSnapshot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSnapshot.Name = "toolStripButtonSnapshot";
            this.toolStripButtonSnapshot.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSnapshot.Text = "Snapshot Packet";
            this.toolStripButtonSnapshot.Click += new System.EventHandler(this.toolStripButtonSnapshot_Click);
            // 
            // SingleFrameEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.frameEditorControl);
            this.Controls.Add(this.toolStrip);
            this.Name = "SingleFrameEditorControl";
            this.Size = new System.Drawing.Size(832, 481);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopyFrame;
        private System.Windows.Forms.ToolStripButton toolStripButtonPasteFrame;
        private FrameEditorControl frameEditorControl;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer timerUpdatePaste;
        private System.Windows.Forms.ToolStripButton toolStripButtonConvertToBytes;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoadFromFile;
        private System.Windows.Forms.ToolStripButton toolStripButtonSnapshot;
    }
}
