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
    partial class FillBytesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillBytesForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radioButtonHexValues = new System.Windows.Forms.RadioButton();
            this.labelMin = new System.Windows.Forms.Label();
            this.radioButtonRandom = new System.Windows.Forms.RadioButton();
            this.textBoxHex = new System.Windows.Forms.TextBox();
            this.numericUpDownMin = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMax = new System.Windows.Forms.NumericUpDown();
            this.labelMax = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMax)).BeginInit();
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
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonHexValues
            // 
            resources.ApplyResources(this.radioButtonHexValues, "radioButtonHexValues");
            this.radioButtonHexValues.Checked = true;
            this.radioButtonHexValues.Name = "radioButtonHexValues";
            this.radioButtonHexValues.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonHexValues, resources.GetString("radioButtonHexValues.ToolTip"));
            this.radioButtonHexValues.UseVisualStyleBackColor = true;
            // 
            // labelMin
            // 
            resources.ApplyResources(this.labelMin, "labelMin");
            this.labelMin.Name = "labelMin";
            // 
            // radioButtonRandom
            // 
            resources.ApplyResources(this.radioButtonRandom, "radioButtonRandom");
            this.radioButtonRandom.Name = "radioButtonRandom";
            this.radioButtonRandom.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonRandom, resources.GetString("radioButtonRandom.ToolTip"));
            this.radioButtonRandom.UseVisualStyleBackColor = true;
            this.radioButtonRandom.CheckedChanged += new System.EventHandler(this.radioButtonRandom_CheckedChanged);
            // 
            // textBoxHex
            // 
            resources.ApplyResources(this.textBoxHex, "textBoxHex");
            this.textBoxHex.Name = "textBoxHex";
            this.toolTip.SetToolTip(this.textBoxHex, resources.GetString("textBoxHex.ToolTip"));
            // 
            // numericUpDownMin
            // 
            resources.ApplyResources(this.numericUpDownMin, "numericUpDownMin");
            this.numericUpDownMin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownMin.Name = "numericUpDownMin";
            this.toolTip.SetToolTip(this.numericUpDownMin, resources.GetString("numericUpDownMin.ToolTip"));
            // 
            // numericUpDownMax
            // 
            resources.ApplyResources(this.numericUpDownMax, "numericUpDownMax");
            this.numericUpDownMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownMax.Name = "numericUpDownMax";
            this.toolTip.SetToolTip(this.numericUpDownMax, resources.GetString("numericUpDownMax.ToolTip"));
            this.numericUpDownMax.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // labelMax
            // 
            resources.ApplyResources(this.labelMax, "labelMax");
            this.labelMax.Name = "labelMax";
            // 
            // FillBytesForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.numericUpDownMax);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.numericUpDownMin);
            this.Controls.Add(this.textBoxHex);
            this.Controls.Add(this.radioButtonRandom);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.radioButtonHexValues);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FillBytesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton radioButtonHexValues;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.RadioButton radioButtonRandom;
        private System.Windows.Forms.TextBox textBoxHex;
        private System.Windows.Forms.NumericUpDown numericUpDownMin;
        private System.Windows.Forms.NumericUpDown numericUpDownMax;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.ToolTip toolTip;
    }
}