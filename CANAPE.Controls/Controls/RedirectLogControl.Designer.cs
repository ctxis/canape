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
    partial class RedirectLogControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedirectLogControl));
            this.textBoxBaseName = new System.Windows.Forms.TextBox();
            this.lblBaseName = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.radioButtonDiscard = new System.Windows.Forms.RadioButton();
            this.radioButtonToFiles = new System.Windows.Forms.RadioButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // textBoxBaseName
            // 
            resources.ApplyResources(this.textBoxBaseName, "textBoxBaseName");
            this.textBoxBaseName.Name = "textBoxBaseName";
            this.toolTip.SetToolTip(this.textBoxBaseName, resources.GetString("textBoxBaseName.ToolTip"));
            this.textBoxBaseName.TextChanged += new System.EventHandler(this.textBoxBaseName_TextChanged);
            // 
            // lblBaseName
            // 
            resources.ApplyResources(this.lblBaseName, "lblBaseName");
            this.lblBaseName.Name = "lblBaseName";
            this.toolTip.SetToolTip(this.lblBaseName, resources.GetString("lblBaseName.ToolTip"));
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.toolTip.SetToolTip(this.btnBrowse, resources.GetString("btnBrowse.ToolTip"));
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // radioButtonDiscard
            // 
            resources.ApplyResources(this.radioButtonDiscard, "radioButtonDiscard");
            this.radioButtonDiscard.Checked = true;
            this.radioButtonDiscard.Name = "radioButtonDiscard";
            this.radioButtonDiscard.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonDiscard, resources.GetString("radioButtonDiscard.ToolTip"));
            this.radioButtonDiscard.UseVisualStyleBackColor = true;
            // 
            // radioButtonToFiles
            // 
            resources.ApplyResources(this.radioButtonToFiles, "radioButtonToFiles");
            this.radioButtonToFiles.Name = "radioButtonToFiles";
            this.radioButtonToFiles.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonToFiles, resources.GetString("radioButtonToFiles.ToolTip"));
            this.radioButtonToFiles.UseVisualStyleBackColor = true;
            this.radioButtonToFiles.CheckedChanged += new System.EventHandler(this.radioButtonToFiles_CheckedChanged);
            // 
            // RedirectLogControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonToFiles);
            this.Controls.Add(this.radioButtonDiscard);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblBaseName);
            this.Controls.Add(this.textBoxBaseName);
            this.Name = "RedirectLogControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxBaseName;
        private System.Windows.Forms.Label lblBaseName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.RadioButton radioButtonDiscard;
        private System.Windows.Forms.RadioButton radioButtonToFiles;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
