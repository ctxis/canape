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
    partial class PacketLogViewerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketLogViewerForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPasteFrame = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonConvertToBytes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRevert = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelPosition = new System.Windows.Forms.ToolStripLabel();
            this.frameEditorControl = new CANAPE.Controls.NodeEditors.FrameEditorControl();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonBack,
            this.toolStripButtonForward,
            this.toolStripButtonCopy,
            this.toolStripButtonPasteFrame,
            this.toolStripButtonConvertToBytes,
            this.toolStripSeparator1,
            this.toolStripButtonSave,
            this.toolStripButtonRevert,
            this.toolStripLabelPosition});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // toolStripButtonBack
            // 
            this.toolStripButtonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonBack, "toolStripButtonBack");
            this.toolStripButtonBack.Name = "toolStripButtonBack";
            this.toolStripButtonBack.Click += new System.EventHandler(this.toolStripButtonBack_Click);
            // 
            // toolStripButtonForward
            // 
            this.toolStripButtonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonForward, "toolStripButtonForward");
            this.toolStripButtonForward.Name = "toolStripButtonForward";
            this.toolStripButtonForward.Click += new System.EventHandler(this.toolStripButtonForward_Click);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonCopy, "toolStripButtonCopy");
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripButtonPasteFrame
            // 
            this.toolStripButtonPasteFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonPasteFrame, "toolStripButtonPasteFrame");
            this.toolStripButtonPasteFrame.Name = "toolStripButtonPasteFrame";
            this.toolStripButtonPasteFrame.Click += new System.EventHandler(this.toolStripButtonPasteFrame_Click);
            // 
            // toolStripButtonConvertToBytes
            // 
            this.toolStripButtonConvertToBytes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonConvertToBytes, "toolStripButtonConvertToBytes");
            this.toolStripButtonConvertToBytes.Name = "toolStripButtonConvertToBytes";
            this.toolStripButtonConvertToBytes.Click += new System.EventHandler(this.toolStripButtonConvertToBytes_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSave, "toolStripButtonSave");
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonRevert
            // 
            this.toolStripButtonRevert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonRevert, "toolStripButtonRevert");
            this.toolStripButtonRevert.Name = "toolStripButtonRevert";
            this.toolStripButtonRevert.Click += new System.EventHandler(this.toolStripButtonRevert_Click);
            // 
            // toolStripLabelPosition
            // 
            this.toolStripLabelPosition.Name = "toolStripLabelPosition";
            resources.ApplyResources(this.toolStripLabelPosition, "toolStripLabelPosition");
            // 
            // frameEditorControl
            // 
            resources.ApplyResources(this.frameEditorControl, "frameEditorControl");
            this.frameEditorControl.Name = "frameEditorControl";
            this.frameEditorControl.ReadOnly = false;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // PacketLogViewerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.frameEditorControl);
            this.Controls.Add(this.toolStrip);
            this.Name = "PacketLogViewerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketLogViewerForm_FormClosing);
            this.Load += new System.EventHandler(this.LogViewerForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonBack;
        private System.Windows.Forms.ToolStripButton toolStripButtonForward;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPosition;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonConvertToBytes;
        private Controls.NodeEditors.FrameEditorControl frameEditorControl;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonRevert;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPasteFrame;
        private System.Windows.Forms.Timer timer;
    }
}