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
    partial class BinaryDataEditorControl
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
            this.radioButtonHex = new System.Windows.Forms.RadioButton();
            this.radioButtonText = new System.Windows.Forms.RadioButton();
            this.textEditorControl = new CANAPE.Controls.NodeRichTextBox();
            this.hexEditorControl = new CANAPE.Controls.HexEditorControl();
            this.inlineSearchControl = new CANAPE.Controls.InlineSearchControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // radioButtonHex
            // 
            this.radioButtonHex.AutoSize = true;
            this.radioButtonHex.Checked = true;
            this.radioButtonHex.Location = new System.Drawing.Point(3, 3);
            this.radioButtonHex.Name = "radioButtonHex";
            this.radioButtonHex.Size = new System.Drawing.Size(44, 17);
            this.radioButtonHex.TabIndex = 0;
            this.radioButtonHex.TabStop = true;
            this.radioButtonHex.Text = "Hex";
            this.toolTip.SetToolTip(this.radioButtonHex, "Check to view data as hex");
            this.radioButtonHex.UseVisualStyleBackColor = true;
            this.radioButtonHex.CheckedChanged += new System.EventHandler(this.radioButtonHex_CheckedChanged);
            // 
            // radioButtonText
            // 
            this.radioButtonText.AutoSize = true;
            this.radioButtonText.Location = new System.Drawing.Point(53, 3);
            this.radioButtonText.Name = "radioButtonText";
            this.radioButtonText.Size = new System.Drawing.Size(46, 17);
            this.radioButtonText.TabIndex = 1;
            this.radioButtonText.Text = "Text";
            this.toolTip.SetToolTip(this.radioButtonText, "Check to view data as text");
            this.radioButtonText.UseVisualStyleBackColor = true;
            this.radioButtonText.CheckedChanged += new System.EventHandler(this.radioButtonText_CheckedChanged);
            // 
            // textEditorControl
            // 
            this.textEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditorControl.BackColor = System.Drawing.SystemColors.Control;
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.Location = new System.Drawing.Point(0, 26);
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.ShowMatchingBracket = false;
            this.textEditorControl.ShowVRuler = false;
            this.textEditorControl.Size = new System.Drawing.Size(791, 465);
            this.textEditorControl.TabIndex = 6;
            this.textEditorControl.Visible = false;
            // 
            // hexEditorControl
            // 
            this.hexEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hexEditorControl.HexColor = System.Drawing.Color.White;
            this.hexEditorControl.Location = new System.Drawing.Point(-1, 26);
            this.hexEditorControl.Name = "hexEditorControl";
            this.hexEditorControl.ReadOnly = false;
            this.hexEditorControl.Size = new System.Drawing.Size(792, 465);
            this.hexEditorControl.TabIndex = 4;
            this.hexEditorControl.BytesChanged += new System.EventHandler(this._byteProv_Changed);
            // 
            // inlineSearchControl
            // 
            this.inlineSearchControl.Location = new System.Drawing.Point(105, 1);
            this.inlineSearchControl.Name = "inlineSearchControl";
            this.inlineSearchControl.Size = new System.Drawing.Size(541, 27);
            this.inlineSearchControl.TabIndex = 5;
            this.inlineSearchControl.SearchNext += new System.EventHandler<CANAPE.Controls.InlineSearchControl.SearchEventArgs>(this.inlineSearchControl_SearchNext);
            this.inlineSearchControl.SearchPrev += new System.EventHandler<CANAPE.Controls.InlineSearchControl.SearchEventArgs>(this.inlineSearchControl_SearchPrev);
            // 
            // BinaryDataEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textEditorControl);
            this.Controls.Add(this.hexEditorControl);
            this.Controls.Add(this.radioButtonText);
            this.Controls.Add(this.radioButtonHex);
            this.Controls.Add(this.inlineSearchControl);
            this.Name = "BinaryDataEditorControl";
            this.Size = new System.Drawing.Size(791, 491);
            this.Load += new System.EventHandler(this.BinaryDataEditorControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonHex;
        private System.Windows.Forms.RadioButton radioButtonText;
        private HexEditorControl hexEditorControl;
        private InlineSearchControl inlineSearchControl;
        private NodeRichTextBox textEditorControl;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
