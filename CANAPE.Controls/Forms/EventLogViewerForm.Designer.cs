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
    partial class EventLogViewerForm
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
            System.Windows.Forms.Label lblTimestamp;
            System.Windows.Forms.Label lblSource;
            System.Windows.Forms.Label lblType;
            System.Windows.Forms.Label lblDetails;
            System.Windows.Forms.Button btnClose;
            this.lblTimestampName = new System.Windows.Forms.Label();
            this.lblSourceName = new System.Windows.Forms.Label();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.richTextBoxDetails = new System.Windows.Forms.RichTextBox();
            lblTimestamp = new System.Windows.Forms.Label();
            lblSource = new System.Windows.Forms.Label();
            lblType = new System.Windows.Forms.Label();
            lblDetails = new System.Windows.Forms.Label();
            btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTimestamp
            // 
            lblTimestamp.AutoSize = true;
            lblTimestamp.Location = new System.Drawing.Point(12, 9);
            lblTimestamp.Name = "lblTimestamp";
            lblTimestamp.Size = new System.Drawing.Size(64, 13);
            lblTimestamp.TabIndex = 0;
            lblTimestamp.Text = "Timestamp: ";
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Location = new System.Drawing.Point(12, 31);
            lblSource.Name = "lblSource";
            lblSource.Size = new System.Drawing.Size(44, 13);
            lblSource.TabIndex = 2;
            lblSource.Text = "Source:";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new System.Drawing.Point(12, 55);
            lblType.Name = "lblType";
            lblType.Size = new System.Drawing.Size(34, 13);
            lblType.TabIndex = 5;
            lblType.Text = "Type:";
            // 
            // lblDetails
            // 
            lblDetails.AutoSize = true;
            lblDetails.Location = new System.Drawing.Point(12, 81);
            lblDetails.Name = "lblDetails";
            lblDetails.Size = new System.Drawing.Size(42, 13);
            lblDetails.TabIndex = 7;
            lblDetails.Text = "Details:";
            // 
            // btnClose
            // 
            btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnClose.Location = new System.Drawing.Point(234, 366);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(75, 23);
            btnClose.TabIndex = 8;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTimestampName
            // 
            this.lblTimestampName.AutoSize = true;
            this.lblTimestampName.Location = new System.Drawing.Point(82, 9);
            this.lblTimestampName.Name = "lblTimestampName";
            this.lblTimestampName.Size = new System.Drawing.Size(95, 13);
            this.lblTimestampName.TabIndex = 1;
            this.lblTimestampName.Text = "[Timestamp Name]";
            // 
            // lblSourceName
            // 
            this.lblSourceName.AutoSize = true;
            this.lblSourceName.Location = new System.Drawing.Point(82, 31);
            this.lblSourceName.Name = "lblSourceName";
            this.lblSourceName.Size = new System.Drawing.Size(78, 13);
            this.lblSourceName.TabIndex = 3;
            this.lblSourceName.Text = "[Source Name]";
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.Location = new System.Drawing.Point(82, 55);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(68, 13);
            this.lblTypeName.TabIndex = 6;
            this.lblTypeName.Text = "[Type Name]";
            // 
            // richTextBoxDetails
            // 
            this.richTextBoxDetails.Location = new System.Drawing.Point(15, 97);
            this.richTextBoxDetails.Name = "richTextBoxDetails";
            this.richTextBoxDetails.ReadOnly = true;
            this.richTextBoxDetails.Size = new System.Drawing.Size(508, 263);
            this.richTextBoxDetails.TabIndex = 9;
            this.richTextBoxDetails.Text = "";
            // 
            // EventLogViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = btnClose;
            this.ClientSize = new System.Drawing.Size(537, 399);
            this.Controls.Add(this.richTextBoxDetails);
            this.Controls.Add(btnClose);
            this.Controls.Add(lblDetails);
            this.Controls.Add(this.lblTypeName);
            this.Controls.Add(lblType);
            this.Controls.Add(this.lblSourceName);
            this.Controls.Add(lblSource);
            this.Controls.Add(this.lblTimestampName);
            this.Controls.Add(lblTimestamp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventLogViewerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Event Log Entry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTimestampName;
        private System.Windows.Forms.Label lblSourceName;
        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.RichTextBox richTextBoxDetails;
    }
}