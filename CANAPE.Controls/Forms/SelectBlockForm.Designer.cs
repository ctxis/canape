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
    partial class SelectBlockForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectBlockForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numericOffset = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.radioLength = new System.Windows.Forms.RadioButton();
            this.numericLength = new System.Windows.Forms.NumericUpDown();
            this.numericEndOffset = new System.Windows.Forms.NumericUpDown();
            this.radioOffset = new System.Windows.Forms.RadioButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEndOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.toolTip.SetToolTip(this.btnOK, resources.GetString("btnOK.ToolTip"));
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.toolTip.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // numericOffset
            // 
            resources.ApplyResources(this.numericOffset, "numericOffset");
            this.numericOffset.Hexadecimal = true;
            this.numericOffset.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericOffset.Name = "numericOffset";
            this.toolTip.SetToolTip(this.numericOffset, resources.GetString("numericOffset.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // radioLength
            // 
            resources.ApplyResources(this.radioLength, "radioLength");
            this.radioLength.Checked = true;
            this.radioLength.Name = "radioLength";
            this.radioLength.TabStop = true;
            this.toolTip.SetToolTip(this.radioLength, resources.GetString("radioLength.ToolTip"));
            this.radioLength.UseVisualStyleBackColor = true;
            // 
            // numericLength
            // 
            resources.ApplyResources(this.numericLength, "numericLength");
            this.numericLength.Hexadecimal = true;
            this.numericLength.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericLength.Name = "numericLength";
            this.toolTip.SetToolTip(this.numericLength, resources.GetString("numericLength.ToolTip"));
            // 
            // numericEndOffset
            // 
            resources.ApplyResources(this.numericEndOffset, "numericEndOffset");
            this.numericEndOffset.Hexadecimal = true;
            this.numericEndOffset.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericEndOffset.Name = "numericEndOffset";
            this.toolTip.SetToolTip(this.numericEndOffset, resources.GetString("numericEndOffset.ToolTip"));
            // 
            // radioOffset
            // 
            resources.ApplyResources(this.radioOffset, "radioOffset");
            this.radioOffset.Name = "radioOffset";
            this.toolTip.SetToolTip(this.radioOffset, resources.GetString("radioOffset.ToolTip"));
            this.radioOffset.UseVisualStyleBackColor = true;
            this.radioOffset.CheckedChanged += new System.EventHandler(this.radioOffset_CheckedChanged);
            // 
            // SelectBlockForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.numericEndOffset);
            this.Controls.Add(this.radioOffset);
            this.Controls.Add(this.numericLength);
            this.Controls.Add(this.radioLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericOffset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectBlockForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            ((System.ComponentModel.ISupportInitialize)(this.numericOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEndOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numericOffset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioLength;
        private System.Windows.Forms.NumericUpDown numericLength;
        private System.Windows.Forms.NumericUpDown numericEndOffset;
        private System.Windows.Forms.RadioButton radioOffset;
        private System.Windows.Forms.ToolTip toolTip;
    }
}