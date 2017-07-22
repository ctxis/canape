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
    partial class FindDataFrameControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label labelValue;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDataFrameControl));
            System.Windows.Forms.Label label2;
            this.btnFind = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.labelMode = new System.Windows.Forms.Label();
            this.checkBoxCaseSensitive = new System.Windows.Forms.CheckBox();
            this.comboBoxEncoding = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.packetLogControl = new CANAPE.Controls.PacketLogControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            labelValue = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelValue
            // 
            resources.ApplyResources(labelValue, "labelValue");
            labelValue.Name = "labelValue";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // btnFind
            // 
            resources.ApplyResources(this.btnFind, "btnFind");
            this.btnFind.Name = "btnFind";
            this.toolTip.SetToolTip(this.btnFind, resources.GetString("btnFind.ToolTip"));
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // progressBar
            // 
            this.tableLayoutPanel.SetColumnSpan(this.progressBar, 2);
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // textBoxValue
            // 
            this.tableLayoutPanel.SetColumnSpan(this.textBoxValue, 2);
            resources.ApplyResources(this.textBoxValue, "textBoxValue");
            this.textBoxValue.Name = "textBoxValue";
            this.toolTip.SetToolTip(this.textBoxValue, resources.GetString("textBoxValue.ToolTip"));
            // 
            // comboBoxMode
            // 
            resources.ApplyResources(this.comboBoxMode, "comboBoxMode");
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Name = "comboBoxMode";
            this.toolTip.SetToolTip(this.comboBoxMode, resources.GetString("comboBoxMode.ToolTip"));
            this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
            // 
            // labelMode
            // 
            resources.ApplyResources(this.labelMode, "labelMode");
            this.labelMode.Name = "labelMode";
            // 
            // checkBoxCaseSensitive
            // 
            resources.ApplyResources(this.checkBoxCaseSensitive, "checkBoxCaseSensitive");
            this.checkBoxCaseSensitive.Name = "checkBoxCaseSensitive";
            this.toolTip.SetToolTip(this.checkBoxCaseSensitive, resources.GetString("checkBoxCaseSensitive.ToolTip"));
            this.checkBoxCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // comboBoxEncoding
            // 
            this.tableLayoutPanel.SetColumnSpan(this.comboBoxEncoding, 2);
            resources.ApplyResources(this.comboBoxEncoding, "comboBoxEncoding");
            this.comboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncoding.FormattingEnabled = true;
            this.comboBoxEncoding.Name = "comboBoxEncoding";
            this.toolTip.SetToolTip(this.comboBoxEncoding, resources.GetString("comboBoxEncoding.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxPath
            // 
            this.tableLayoutPanel.SetColumnSpan(this.textBoxPath, 2);
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            this.toolTip.SetToolTip(this.textBoxPath, resources.GetString("textBoxPath.ToolTip"));
            // 
            // packetLogControl
            // 
            resources.ApplyResources(this.packetLogControl, "packetLogControl");
            this.tableLayoutPanel.SetColumnSpan(this.packetLogControl, 3);
            this.packetLogControl.IsInFindForm = true;
            this.packetLogControl.LogName = null;
            this.packetLogControl.Name = "packetLogControl";
            this.packetLogControl.ReadOnly = false;
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(labelValue, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.packetLogControl, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.progressBar, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.comboBoxEncoding, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxPath, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.textBoxValue, 1, 0);
            this.tableLayoutPanel.Controls.Add(label2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.comboBoxMode, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelMode, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.btnFind, 2, 4);
            this.tableLayoutPanel.Controls.Add(this.checkBoxCaseSensitive, 2, 2);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // FindDataFrameControl
            // 
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "FindDataFrameControl";
            resources.ApplyResources(this, "$this");
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PacketLogControl packetLogControl;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.CheckBox checkBoxCaseSensitive;
        private System.Windows.Forms.ComboBox comboBoxEncoding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}