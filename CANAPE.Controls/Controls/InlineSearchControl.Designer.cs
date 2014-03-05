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
    partial class InlineSearchControl
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
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InlineSearchControl));
            this.btnPrevSearch = new System.Windows.Forms.Button();
            this.btnNextSearch = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.radioBinary = new System.Windows.Forms.RadioButton();
            this.radioText = new System.Windows.Forms.RadioButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            this.toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // btnPrevSearch
            // 
            resources.ApplyResources(this.btnPrevSearch, "btnPrevSearch");
            this.btnPrevSearch.Name = "btnPrevSearch";
            this.toolTip.SetToolTip(this.btnPrevSearch, resources.GetString("btnPrevSearch.ToolTip"));
            this.btnPrevSearch.UseVisualStyleBackColor = true;
            this.btnPrevSearch.Click += new System.EventHandler(this.btnPrevSearch_Click);
            // 
            // btnNextSearch
            // 
            resources.ApplyResources(this.btnNextSearch, "btnNextSearch");
            this.btnNextSearch.Name = "btnNextSearch";
            this.toolTip.SetToolTip(this.btnNextSearch, resources.GetString("btnNextSearch.ToolTip"));
            this.btnNextSearch.UseVisualStyleBackColor = true;
            this.btnNextSearch.Click += new System.EventHandler(this.btnNextSearch_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxSearch, "textBoxSearch");
            this.textBoxSearch.Name = "textBoxSearch";
            this.toolTip.SetToolTip(this.textBoxSearch, resources.GetString("textBoxSearch.ToolTip"));
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // radioBinary
            // 
            resources.ApplyResources(this.radioBinary, "radioBinary");
            this.radioBinary.Name = "radioBinary";
            this.toolTip.SetToolTip(this.radioBinary, resources.GetString("radioBinary.ToolTip"));
            this.radioBinary.UseVisualStyleBackColor = true;
            // 
            // radioText
            // 
            resources.ApplyResources(this.radioText, "radioText");
            this.radioText.Checked = true;
            this.radioText.Name = "radioText";
            this.radioText.TabStop = true;
            this.toolTip.SetToolTip(this.radioText, resources.GetString("radioText.ToolTip"));
            this.radioText.UseVisualStyleBackColor = true;
            // 
            // InlineSearchControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioText);
            this.Controls.Add(this.radioBinary);
            this.Controls.Add(this.btnPrevSearch);
            this.Controls.Add(this.btnNextSearch);
            this.Controls.Add(label1);
            this.Controls.Add(this.textBoxSearch);
            this.Name = "InlineSearchControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrevSearch;
        private System.Windows.Forms.Button btnNextSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.RadioButton radioBinary;
        private System.Windows.Forms.RadioButton radioText;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
