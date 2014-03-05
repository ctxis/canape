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
    partial class NetGraphTestDocumentControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetGraphTestDocumentControl));
            this.graphTestControl = new CANAPE.Controls.GraphTestControl();
            this.radioServerToClient = new System.Windows.Forms.RadioButton();
            this.radioClientToServer = new System.Windows.Forms.RadioButton();
            this.btnRun = new System.Windows.Forms.Button();
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
            // graphTestControl
            // 
            resources.ApplyResources(this.graphTestControl, "graphTestControl");
            this.graphTestControl.Name = "graphTestControl";
            this.toolTip.SetToolTip(this.graphTestControl, resources.GetString("graphTestControl.ToolTip"));
            // 
            // radioServerToClient
            // 
            resources.ApplyResources(this.radioServerToClient, "radioServerToClient");
            this.radioServerToClient.Checked = true;
            this.radioServerToClient.Name = "radioServerToClient";
            this.radioServerToClient.TabStop = true;
            this.toolTip.SetToolTip(this.radioServerToClient, resources.GetString("radioServerToClient.ToolTip"));
            this.radioServerToClient.UseVisualStyleBackColor = true;
            // 
            // radioClientToServer
            // 
            resources.ApplyResources(this.radioClientToServer, "radioClientToServer");
            this.radioClientToServer.Name = "radioClientToServer";
            this.radioClientToServer.TabStop = true;
            this.toolTip.SetToolTip(this.radioClientToServer, resources.GetString("radioClientToServer.ToolTip"));
            this.radioClientToServer.UseVisualStyleBackColor = true;
            this.radioClientToServer.CheckedChanged += new System.EventHandler(this.radioClientToServer_CheckedChanged);
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.toolTip.SetToolTip(this.btnRun, resources.GetString("btnRun.ToolTip"));
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // NetGraphTestDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.radioClientToServer);
            this.Controls.Add(this.radioServerToClient);
            this.Controls.Add(label1);
            this.Controls.Add(this.graphTestControl);
            this.Name = "NetGraphTestDocumentControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GraphTestControl graphTestControl;
        private System.Windows.Forms.RadioButton radioServerToClient;
        private System.Windows.Forms.RadioButton radioClientToServer;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
