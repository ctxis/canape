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
    partial class ScriptSelectionControl
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
            System.Windows.Forms.Label label2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptSelectionControl));
            System.Windows.Forms.Label label1;
            this.comboBoxClassName = new System.Windows.Forms.ComboBox();
            this.comboBoxScript = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            this.toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            this.toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // comboBoxClassName
            // 
            resources.ApplyResources(this.comboBoxClassName, "comboBoxClassName");
            this.comboBoxClassName.FormattingEnabled = true;
            this.comboBoxClassName.Name = "comboBoxClassName";
            this.toolTip.SetToolTip(this.comboBoxClassName, resources.GetString("comboBoxClassName.ToolTip"));
            this.comboBoxClassName.TextChanged += new System.EventHandler(this.comboBoxClassName_TextChanged);
            // 
            // comboBoxScript
            // 
            resources.ApplyResources(this.comboBoxScript, "comboBoxScript");
            this.comboBoxScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScript.FormattingEnabled = true;
            this.comboBoxScript.Name = "comboBoxScript";
            this.toolTip.SetToolTip(this.comboBoxScript, resources.GetString("comboBoxScript.ToolTip"));
            this.comboBoxScript.DropDown += new System.EventHandler(this.comboBoxScript_DropDown);
            this.comboBoxScript.SelectedIndexChanged += new System.EventHandler(this.comboBoxScript_SelectedIndexChanged);
            // 
            // ScriptSelectionControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(label2);
            this.Controls.Add(this.comboBoxClassName);
            this.Controls.Add(label1);
            this.Controls.Add(this.comboBoxScript);
            this.Name = "ScriptSelectionControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.ScriptSelectionControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxClassName;
        private System.Windows.Forms.ComboBox comboBoxScript;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
