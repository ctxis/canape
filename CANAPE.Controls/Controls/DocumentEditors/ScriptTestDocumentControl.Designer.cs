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
    partial class ScriptTestDocumentControl
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
            System.Windows.Forms.Label label4;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptTestDocumentControl));
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.comboBoxClassName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.graphTestControl = new CANAPE.Controls.GraphTestControl();
            this.btnRun = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnEditConfig = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            this.toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            this.toolTip.SetToolTip(this.textBoxPath, resources.GetString("textBoxPath.ToolTip"));
            this.textBoxPath.TextChanged += new System.EventHandler(this.textBoxPath_TextChanged);
            // 
            // comboBoxClassName
            // 
            resources.ApplyResources(this.comboBoxClassName, "comboBoxClassName");
            this.comboBoxClassName.FormattingEnabled = true;
            this.comboBoxClassName.Name = "comboBoxClassName";
            this.toolTip.SetToolTip(this.comboBoxClassName, resources.GetString("comboBoxClassName.ToolTip"));
            this.comboBoxClassName.DropDown += new System.EventHandler(this.comboBoxClassName_DropDown);
            this.comboBoxClassName.TextChanged += new System.EventHandler(this.comboBoxClassName_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // graphTestControl
            // 
            resources.ApplyResources(this.graphTestControl, "graphTestControl");
            this.graphTestControl.Name = "graphTestControl";
            this.toolTip.SetToolTip(this.graphTestControl, resources.GetString("graphTestControl.ToolTip"));
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.toolTip.SetToolTip(this.btnRun, resources.GetString("btnRun.ToolTip"));
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnEditConfig
            // 
            resources.ApplyResources(this.btnEditConfig, "btnEditConfig");
            this.btnEditConfig.Name = "btnEditConfig";
            this.toolTip.SetToolTip(this.btnEditConfig, resources.GetString("btnEditConfig.ToolTip"));
            this.btnEditConfig.UseVisualStyleBackColor = true;
            this.btnEditConfig.Click += new System.EventHandler(this.btnEditConfig_Click);
            // 
            // ScriptTestDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRun);
            this.Controls.Add(label4);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.comboBoxClassName);
            this.Controls.Add(this.btnEditConfig);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.graphTestControl);
            this.Name = "ScriptTestDocumentControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphTestControl graphTestControl;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.ComboBox comboBoxClassName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnEditConfig;

    }
}
