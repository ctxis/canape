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
    partial class EditPacketForm
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

            WaitEvent.Dispose();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPacketForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxReadOnly = new System.Windows.Forms.CheckBox();
            this.checkBoxDisableEdit = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.frameEditorControl = new CANAPE.Controls.NodeEditors.SingleFrameEditorControl();
            this.SuspendLayout();
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkBoxReadOnly
            // 
            resources.ApplyResources(this.checkBoxReadOnly, "checkBoxReadOnly");
            this.checkBoxReadOnly.Name = "checkBoxReadOnly";
            this.toolTip.SetToolTip(this.checkBoxReadOnly, resources.GetString("checkBoxReadOnly.ToolTip"));
            this.checkBoxReadOnly.UseVisualStyleBackColor = true;
            this.checkBoxReadOnly.CheckedChanged += new System.EventHandler(this.checkBoxReadOnly_CheckedChanged);
            // 
            // checkBoxDisableEdit
            // 
            resources.ApplyResources(this.checkBoxDisableEdit, "checkBoxDisableEdit");
            this.checkBoxDisableEdit.Name = "checkBoxDisableEdit";
            this.toolTip.SetToolTip(this.checkBoxDisableEdit, resources.GetString("checkBoxDisableEdit.ToolTip"));
            this.checkBoxDisableEdit.UseVisualStyleBackColor = true;
            this.checkBoxDisableEdit.CheckedChanged += new System.EventHandler(this.checkBoxDisableEdit_CheckedChanged);
            // 
            // frameEditorControl
            // 
            resources.ApplyResources(this.frameEditorControl, "frameEditorControl");
            this.frameEditorControl.Name = "frameEditorControl";
            this.frameEditorControl.ReadOnly = false;            
            // 
            // EditPacketForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.frameEditorControl);
            this.Controls.Add(this.checkBoxDisableEdit);
            this.Controls.Add(this.checkBoxReadOnly);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "EditPacketForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditPacketForm_FormClosed);
            this.Load += new System.EventHandler(this.EditPacketForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBoxReadOnly;
        private System.Windows.Forms.CheckBox checkBoxDisableEdit;
        private Controls.NodeEditors.SingleFrameEditorControl frameEditorControl;
        private System.Windows.Forms.ToolTip toolTip;
    }
}