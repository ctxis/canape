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
    partial class BaseDiffControl
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
            System.Windows.Forms.ToolStripLabel toolStripLabelModified;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseDiffControl));
            System.Windows.Forms.ToolStripLabel toolStripLabelDeleted;
            System.Windows.Forms.ToolStripLabel toolStripLabelAdded;
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.richTextBoxLeft = new System.Windows.Forms.RichTextBox();
            this.richTextBoxRight = new System.Windows.Forms.RichTextBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPrev = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.radioButtonText = new System.Windows.Forms.RadioButton();
            this.radioButtonBytes = new System.Windows.Forms.RadioButton();
            this.hexEditorControlLeft = new CANAPE.Controls.HexEditorControl();
            this.hexEditorControlRight = new CANAPE.Controls.HexEditorControl();
            toolStripLabelModified = new System.Windows.Forms.ToolStripLabel();
            toolStripLabelDeleted = new System.Windows.Forms.ToolStripLabel();
            toolStripLabelAdded = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripLabelModified
            // 
            toolStripLabelModified.BackColor = System.Drawing.Color.Yellow;
            toolStripLabelModified.Name = "toolStripLabelModified";
            resources.ApplyResources(toolStripLabelModified, "toolStripLabelModified");
            // 
            // toolStripLabelDeleted
            // 
            toolStripLabelDeleted.BackColor = System.Drawing.Color.Red;
            toolStripLabelDeleted.Name = "toolStripLabelDeleted";
            resources.ApplyResources(toolStripLabelDeleted, "toolStripLabelDeleted");
            // 
            // toolStripLabelAdded
            // 
            toolStripLabelAdded.BackColor = System.Drawing.Color.Cyan;
            toolStripLabelAdded.Name = "toolStripLabelAdded";
            resources.ApplyResources(toolStripLabelAdded, "toolStripLabelAdded");
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.hexEditorControlLeft);
            this.splitContainer.Panel1.Controls.Add(this.richTextBoxLeft);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.hexEditorControlRight);
            this.splitContainer.Panel2.Controls.Add(this.richTextBoxRight);
            // 
            // richTextBoxLeft
            // 
            resources.ApplyResources(this.richTextBoxLeft, "richTextBoxLeft");
            this.richTextBoxLeft.Name = "richTextBoxLeft";
            this.richTextBoxLeft.ReadOnly = true;
            // 
            // richTextBoxRight
            // 
            resources.ApplyResources(this.richTextBoxRight, "richTextBoxRight");
            this.richTextBoxRight.Name = "richTextBoxRight";
            this.richTextBoxRight.ReadOnly = true;
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrev,
            this.toolStripButtonNext,
            this.toolStripLabelInfo,
            this.toolStripSeparator1,
            toolStripLabelModified,
            toolStripLabelDeleted,
            toolStripLabelAdded});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripButtonPrev
            // 
            resources.ApplyResources(this.toolStripButtonPrev, "toolStripButtonPrev");
            this.toolStripButtonPrev.Name = "toolStripButtonPrev";
            this.toolStripButtonPrev.Click += new System.EventHandler(this.toolStripButtonPrev_Click);
            // 
            // toolStripButtonNext
            // 
            resources.ApplyResources(this.toolStripButtonNext, "toolStripButtonNext");
            this.toolStripButtonNext.Name = "toolStripButtonNext";
            this.toolStripButtonNext.Click += new System.EventHandler(this.toolStripButtonNext_Click);
            // 
            // toolStripLabelInfo
            // 
            this.toolStripLabelInfo.Name = "toolStripLabelInfo";
            resources.ApplyResources(this.toolStripLabelInfo, "toolStripLabelInfo");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // radioButtonText
            // 
            resources.ApplyResources(this.radioButtonText, "radioButtonText");
            this.radioButtonText.Name = "radioButtonText";
            this.radioButtonText.UseVisualStyleBackColor = true;
            // 
            // radioButtonBytes
            // 
            resources.ApplyResources(this.radioButtonBytes, "radioButtonBytes");
            this.radioButtonBytes.Checked = true;
            this.radioButtonBytes.Name = "radioButtonBytes";
            this.radioButtonBytes.TabStop = true;
            this.radioButtonBytes.UseVisualStyleBackColor = true;
            this.radioButtonBytes.CheckedChanged += new System.EventHandler(this.radioButtonBytes_CheckedChanged);
            // 
            // hexEditorControlLeft
            // 
            this.hexEditorControlLeft.BytesPerLine = 8;
            resources.ApplyResources(this.hexEditorControlLeft, "hexEditorControlLeft");
            this.hexEditorControlLeft.HexColor = System.Drawing.Color.White;
            this.hexEditorControlLeft.Name = "hexEditorControlLeft";
            this.hexEditorControlLeft.ReadOnly = false;
            // 
            // hexEditorControlRight
            // 
            this.hexEditorControlRight.BytesPerLine = 8;
            resources.ApplyResources(this.hexEditorControlRight, "hexEditorControlRight");
            this.hexEditorControlRight.HexColor = System.Drawing.Color.White;
            this.hexEditorControlRight.Name = "hexEditorControlRight";
            this.hexEditorControlRight.ReadOnly = false;
            // 
            // BaseDiffControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonBytes);
            this.Controls.Add(this.radioButtonText);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.Name = "BaseDiffControl";
            this.Load += new System.EventHandler(this.BinaryFrameDiffControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrev;
        private System.Windows.Forms.ToolStripButton toolStripButtonNext;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripLabel toolStripLabelInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RadioButton radioButtonText;
        private System.Windows.Forms.RadioButton radioButtonBytes;
        private System.Windows.Forms.RichTextBox richTextBoxLeft;
        private System.Windows.Forms.RichTextBox richTextBoxRight;
        private HexEditorControl hexEditorControlLeft;
        private HexEditorControl hexEditorControlRight;
    }
}
