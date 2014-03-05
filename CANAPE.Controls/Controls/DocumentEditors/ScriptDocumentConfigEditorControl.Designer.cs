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
    partial class ScriptDocumentConfigEditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptDocumentConfigEditorControl));
            this.checkBoxShowEOLMarkers = new System.Windows.Forms.CheckBox();
            this.checkBoxShowLineNumbers = new System.Windows.Forms.CheckBox();
            this.checkBoxShowTabs = new System.Windows.Forms.CheckBox();
            this.checkBoxConvertTabsToSpaces = new System.Windows.Forms.CheckBox();
            this.checkBoxShowSpaces = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxShowEOLMarkers
            // 
            resources.ApplyResources(this.checkBoxShowEOLMarkers, "checkBoxShowEOLMarkers");
            this.checkBoxShowEOLMarkers.Name = "checkBoxShowEOLMarkers";
            this.checkBoxShowEOLMarkers.UseVisualStyleBackColor = true;
            this.checkBoxShowEOLMarkers.CheckedChanged += new System.EventHandler(this.check_Changed);
            // 
            // checkBoxShowLineNumbers
            // 
            resources.ApplyResources(this.checkBoxShowLineNumbers, "checkBoxShowLineNumbers");
            this.checkBoxShowLineNumbers.Name = "checkBoxShowLineNumbers";
            this.checkBoxShowLineNumbers.UseVisualStyleBackColor = true;
            this.checkBoxShowLineNumbers.CheckedChanged += new System.EventHandler(this.check_Changed);
            // 
            // checkBoxShowTabs
            // 
            resources.ApplyResources(this.checkBoxShowTabs, "checkBoxShowTabs");
            this.checkBoxShowTabs.Name = "checkBoxShowTabs";
            this.checkBoxShowTabs.UseVisualStyleBackColor = true;
            this.checkBoxShowTabs.CheckedChanged += new System.EventHandler(this.check_Changed);
            // 
            // checkBoxConvertTabsToSpaces
            // 
            resources.ApplyResources(this.checkBoxConvertTabsToSpaces, "checkBoxConvertTabsToSpaces");
            this.checkBoxConvertTabsToSpaces.Name = "checkBoxConvertTabsToSpaces";
            this.checkBoxConvertTabsToSpaces.UseVisualStyleBackColor = true;
            this.checkBoxConvertTabsToSpaces.CheckedChanged += new System.EventHandler(this.check_Changed);
            // 
            // checkBoxShowSpaces
            // 
            resources.ApplyResources(this.checkBoxShowSpaces, "checkBoxShowSpaces");
            this.checkBoxShowSpaces.Name = "checkBoxShowSpaces";
            this.checkBoxShowSpaces.UseVisualStyleBackColor = true;
            this.checkBoxShowSpaces.CheckedChanged += new System.EventHandler(this.check_Changed);
            // 
            // ScriptDocumentConfigEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxShowEOLMarkers);
            this.Controls.Add(this.checkBoxShowLineNumbers);
            this.Controls.Add(this.checkBoxShowTabs);
            this.Controls.Add(this.checkBoxConvertTabsToSpaces);
            this.Controls.Add(this.checkBoxShowSpaces);
            this.Name = "ScriptDocumentConfigEditorControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxShowEOLMarkers;
        private System.Windows.Forms.CheckBox checkBoxShowLineNumbers;
        private System.Windows.Forms.CheckBox checkBoxShowTabs;
        private System.Windows.Forms.CheckBox checkBoxConvertTabsToSpaces;
        private System.Windows.Forms.CheckBox checkBoxShowSpaces;
    }
}
