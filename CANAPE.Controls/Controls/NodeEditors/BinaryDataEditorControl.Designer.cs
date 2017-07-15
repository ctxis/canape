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
            this.textEditorControl = new CANAPE.Controls.NodeRichTextBox();
            this.hexEditorControl = new CANAPE.Controls.HexEditorControl();
            this.inlineSearchControl = new CANAPE.Controls.InlineSearchControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageHex = new System.Windows.Forms.TabPage();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl.SuspendLayout();
            this.tabPageHex.SuspendLayout();
            this.tabPageText.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textEditorControl
            // 
            this.textEditorControl.BackColor = System.Drawing.SystemColors.Control;
            this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.Location = new System.Drawing.Point(3, 3);
            this.textEditorControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.ShowMatchingBracket = false;
            this.textEditorControl.ShowVRuler = false;
            this.textEditorControl.Size = new System.Drawing.Size(1166, 661);
            this.textEditorControl.TabIndex = 6;
            // 
            // hexEditorControl
            // 
            this.hexEditorControl.BytesPerLine = 16;
            this.hexEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexEditorControl.HexColor = System.Drawing.Color.White;
            this.hexEditorControl.Location = new System.Drawing.Point(3, 3);
            this.hexEditorControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.hexEditorControl.Name = "hexEditorControl";
            this.hexEditorControl.ReadOnly = false;
            this.hexEditorControl.Size = new System.Drawing.Size(1084, 575);
            this.hexEditorControl.TabIndex = 4;
            this.hexEditorControl.BytesChanged += new System.EventHandler(this._byteProv_Changed);
            // 
            // inlineSearchControl
            // 
            this.inlineSearchControl.Location = new System.Drawing.Point(6, 8);
            this.inlineSearchControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.inlineSearchControl.Name = "inlineSearchControl";
            this.inlineSearchControl.Size = new System.Drawing.Size(812, 33);
            this.inlineSearchControl.TabIndex = 5;
            this.inlineSearchControl.SearchNext += new System.EventHandler<CANAPE.Controls.InlineSearchControl.SearchEventArgs>(this.inlineSearchControl_SearchNext);
            this.inlineSearchControl.SearchPrev += new System.EventHandler<CANAPE.Controls.InlineSearchControl.SearchEventArgs>(this.inlineSearchControl_SearchPrev);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageHex);
            this.tabControl.Controls.Add(this.tabPageText);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 52);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1180, 700);
            this.tabControl.TabIndex = 7;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            this.tabControl.Deselected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Deselected);
            // 
            // tabPageHex
            // 
            this.tabPageHex.Controls.Add(this.hexEditorControl);
            this.tabPageHex.Location = new System.Drawing.Point(4, 29);
            this.tabPageHex.Name = "tabPageHex";
            this.tabPageHex.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHex.Size = new System.Drawing.Size(1090, 581);
            this.tabPageHex.TabIndex = 0;
            this.tabPageHex.Text = "Hex";
            this.tabPageHex.UseVisualStyleBackColor = true;
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.textEditorControl);
            this.tabPageText.Location = new System.Drawing.Point(4, 29);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageText.Size = new System.Drawing.Size(1172, 667);
            this.tabPageText.TabIndex = 1;
            this.tabPageText.Text = "Text";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.inlineSearchControl, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1186, 755);
            this.tableLayoutPanel.TabIndex = 8;
            // 
            // BinaryDataEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BinaryDataEditorControl";
            this.Size = new System.Drawing.Size(1186, 755);
            this.Load += new System.EventHandler(this.BinaryDataEditorControl_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageHex.ResumeLayout(false);
            this.tabPageText.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private HexEditorControl hexEditorControl;
        private InlineSearchControl inlineSearchControl;
        private NodeRichTextBox textEditorControl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageHex;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}
