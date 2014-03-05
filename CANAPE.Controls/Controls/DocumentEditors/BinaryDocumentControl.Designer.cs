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

namespace CANAPE.Controls.DocumentEditors
{
    partial class BinaryDocumentControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinaryDocumentControl));
            this.hexEditorControl = new CANAPE.Controls.HexEditorControl();
            this.SuspendLayout();
            // 
            // hexEditorControl
            // 
            resources.ApplyResources(this.hexEditorControl, "hexEditorControl");
            this.hexEditorControl.HexColor = System.Drawing.Color.White;
            this.hexEditorControl.Name = "hexEditorControl";
            this.hexEditorControl.ReadOnly = false;
            this.hexEditorControl.BytesChanged += new System.EventHandler(this.hexEditorControl_BytesChanged);
            // 
            // BinaryDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hexEditorControl);
            this.Name = "BinaryDocumentControl";
            this.ResumeLayout(false);

        }

        #endregion

        private HexEditorControl hexEditorControl;
    }
}
