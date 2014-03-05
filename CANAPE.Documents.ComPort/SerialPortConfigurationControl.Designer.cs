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

namespace CANAPE.Documents.ComPort
{
    partial class SerialPortConfigurationControl
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
            System.Windows.Forms.Label labelPort;
            System.Windows.Forms.Label labelFlowControl;
            System.Windows.Forms.Label labelParity;
            System.Windows.Forms.Label labelStopBits;
            System.Windows.Forms.Label labelDataBits;
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.comboBoxFlowControl = new System.Windows.Forms.ComboBox();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.numericUpDownDataBits = new System.Windows.Forms.NumericUpDown();
            this.labelBaud = new System.Windows.Forms.Label();
            this.numericUpDownBaudRate = new System.Windows.Forms.NumericUpDown();
            labelPort = new System.Windows.Forms.Label();
            labelFlowControl = new System.Windows.Forms.Label();
            labelParity = new System.Windows.Forms.Label();
            labelStopBits = new System.Windows.Forms.Label();
            labelDataBits = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataBits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaudRate)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPort
            // 
            labelPort.AutoSize = true;
            labelPort.Location = new System.Drawing.Point(3, 6);
            labelPort.Name = "labelPort";
            labelPort.Size = new System.Drawing.Size(29, 13);
            labelPort.TabIndex = 1;
            labelPort.Text = "Port:";
            // 
            // labelFlowControl
            // 
            labelFlowControl.AutoSize = true;
            labelFlowControl.Location = new System.Drawing.Point(3, 58);
            labelFlowControl.Name = "labelFlowControl";
            labelFlowControl.Size = new System.Drawing.Size(68, 13);
            labelFlowControl.TabIndex = 3;
            labelFlowControl.Text = "Flow Control:";
            // 
            // labelParity
            // 
            labelParity.AutoSize = true;
            labelParity.Location = new System.Drawing.Point(3, 86);
            labelParity.Name = "labelParity";
            labelParity.Size = new System.Drawing.Size(36, 13);
            labelParity.TabIndex = 5;
            labelParity.Text = "Parity:";
            // 
            // labelStopBits
            // 
            labelStopBits.AutoSize = true;
            labelStopBits.Location = new System.Drawing.Point(3, 115);
            labelStopBits.Name = "labelStopBits";
            labelStopBits.Size = new System.Drawing.Size(52, 13);
            labelStopBits.TabIndex = 7;
            labelStopBits.Text = "Stop Bits:";
            // 
            // labelDataBits
            // 
            labelDataBits.AutoSize = true;
            labelDataBits.Location = new System.Drawing.Point(4, 141);
            labelDataBits.Name = "labelDataBits";
            labelDataBits.Size = new System.Drawing.Size(53, 13);
            labelDataBits.TabIndex = 9;
            labelDataBits.Text = "Data Bits:";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(77, 3);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(148, 21);
            this.comboBoxPort.TabIndex = 0;
            this.comboBoxPort.TextUpdate += new System.EventHandler(this.comboBoxPort_TextUpdate);
            // 
            // comboBoxFlowControl
            // 
            this.comboBoxFlowControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFlowControl.FormattingEnabled = true;
            this.comboBoxFlowControl.Location = new System.Drawing.Point(77, 55);
            this.comboBoxFlowControl.Name = "comboBoxFlowControl";
            this.comboBoxFlowControl.Size = new System.Drawing.Size(148, 21);
            this.comboBoxFlowControl.TabIndex = 2;
            this.comboBoxFlowControl.SelectedIndexChanged += new System.EventHandler(this.comboBoxFlowControl_SelectedIndexChanged);
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Location = new System.Drawing.Point(77, 83);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(148, 21);
            this.comboBoxParity.TabIndex = 4;
            this.comboBoxParity.SelectedIndexChanged += new System.EventHandler(this.comboBoxParity_SelectedIndexChanged);
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBits.FormattingEnabled = true;
            this.comboBoxStopBits.Location = new System.Drawing.Point(77, 112);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(148, 21);
            this.comboBoxStopBits.TabIndex = 6;
            this.comboBoxStopBits.SelectedIndexChanged += new System.EventHandler(this.comboBoxStopBits_SelectedIndexChanged);
            // 
            // numericUpDownDataBits
            // 
            this.numericUpDownDataBits.Location = new System.Drawing.Point(77, 139);
            this.numericUpDownDataBits.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownDataBits.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDataBits.Name = "numericUpDownDataBits";
            this.numericUpDownDataBits.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownDataBits.TabIndex = 8;
            this.numericUpDownDataBits.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDataBits.ValueChanged += new System.EventHandler(this.numericUpDownDataBits_ValueChanged);
            // 
            // labelBaud
            // 
            this.labelBaud.AutoSize = true;
            this.labelBaud.Location = new System.Drawing.Point(3, 33);
            this.labelBaud.Name = "labelBaud";
            this.labelBaud.Size = new System.Drawing.Size(61, 13);
            this.labelBaud.TabIndex = 10;
            this.labelBaud.Text = "Baud Rate:";
            // 
            // numericUpDownBaudRate
            // 
            this.numericUpDownBaudRate.Location = new System.Drawing.Point(77, 29);
            this.numericUpDownBaudRate.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownBaudRate.Name = "numericUpDownBaudRate";
            this.numericUpDownBaudRate.Size = new System.Drawing.Size(91, 20);
            this.numericUpDownBaudRate.TabIndex = 11;
            this.numericUpDownBaudRate.ValueChanged += new System.EventHandler(this.numericUpDownBaudRate_ValueChanged);
            // 
            // SerialPortConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownBaudRate);
            this.Controls.Add(this.labelBaud);
            this.Controls.Add(labelDataBits);
            this.Controls.Add(this.numericUpDownDataBits);
            this.Controls.Add(labelStopBits);
            this.Controls.Add(this.comboBoxStopBits);
            this.Controls.Add(labelParity);
            this.Controls.Add(this.comboBoxParity);
            this.Controls.Add(labelFlowControl);
            this.Controls.Add(this.comboBoxFlowControl);
            this.Controls.Add(labelPort);
            this.Controls.Add(this.comboBoxPort);
            this.Name = "SerialPortConfigurationControl";
            this.Size = new System.Drawing.Size(543, 304);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataBits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaudRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.ComboBox comboBoxFlowControl;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.ComboBox comboBoxStopBits;
        private System.Windows.Forms.NumericUpDown numericUpDownDataBits;
        private System.Windows.Forms.Label labelBaud;
        private System.Windows.Forms.NumericUpDown numericUpDownBaudRate;
    }
}
