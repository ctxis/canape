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
    partial class GraphTestControl
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
            System.Windows.Forms.Label lblInput;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphTestControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelInput = new System.Windows.Forms.TableLayoutPanel();
            this.logPacketControlInput = new CANAPE.Controls.PacketLogControl();
            this.tableLayoutPanelOutput = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.logPacketControlOutput = new CANAPE.Controls.PacketLogControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTest = new System.Windows.Forms.TabPage();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.eventLogControl = new CANAPE.Controls.EventLogControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            lblInput = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tableLayoutPanelInput.SuspendLayout();
            this.tableLayoutPanelOutput.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageTest.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInput
            // 
            resources.ApplyResources(lblInput, "lblInput");
            lblInput.Name = "lblInput";
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanelInput);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanelOutput);
            // 
            // tableLayoutPanelInput
            // 
            resources.ApplyResources(this.tableLayoutPanelInput, "tableLayoutPanelInput");
            this.tableLayoutPanelInput.Controls.Add(lblInput, 0, 0);
            this.tableLayoutPanelInput.Controls.Add(this.logPacketControlInput, 0, 1);
            this.tableLayoutPanelInput.Name = "tableLayoutPanelInput";
            // 
            // logPacketControlInput
            // 
            resources.ApplyResources(this.logPacketControlInput, "logPacketControlInput");
            this.logPacketControlInput.IsInFindForm = false;
            this.logPacketControlInput.LogName = null;
            this.logPacketControlInput.Name = "logPacketControlInput";
            this.logPacketControlInput.ReadOnly = false;
            this.toolTip.SetToolTip(this.logPacketControlInput, resources.GetString("logPacketControlInput.ToolTip"));
            this.logPacketControlInput.ConfigChanged += new System.EventHandler(this.logPacketControlInput_ConfigChanged);
            // 
            // tableLayoutPanelOutput
            // 
            resources.ApplyResources(this.tableLayoutPanelOutput, "tableLayoutPanelOutput");
            this.tableLayoutPanelOutput.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanelOutput.Controls.Add(this.logPacketControlOutput, 0, 1);
            this.tableLayoutPanelOutput.Name = "tableLayoutPanelOutput";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // logPacketControlOutput
            // 
            resources.ApplyResources(this.logPacketControlOutput, "logPacketControlOutput");
            this.logPacketControlOutput.IsInFindForm = false;
            this.logPacketControlOutput.LogName = null;
            this.logPacketControlOutput.Name = "logPacketControlOutput";
            this.logPacketControlOutput.ReadOnly = false;
            this.toolTip.SetToolTip(this.logPacketControlOutput, resources.GetString("logPacketControlOutput.ToolTip"));
            this.logPacketControlOutput.ConfigChanged += new System.EventHandler(this.logPacketControlOutput_ConfigChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageTest);
            this.tabControl.Controls.Add(this.tabPageLog);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageTest
            // 
            this.tabPageTest.Controls.Add(this.splitContainer);
            resources.ApplyResources(this.tabPageTest, "tabPageTest");
            this.tabPageTest.Name = "tabPageTest";
            this.tabPageTest.UseVisualStyleBackColor = true;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.eventLogControl);
            resources.ApplyResources(this.tabPageLog, "tabPageLog");
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // eventLogControl
            // 
            resources.ApplyResources(this.eventLogControl, "eventLogControl");
            this.eventLogControl.Name = "eventLogControl";
            // 
            // GraphTestControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "GraphTestControl";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tableLayoutPanelInput.ResumeLayout(false);
            this.tableLayoutPanelInput.PerformLayout();
            this.tableLayoutPanelOutput.ResumeLayout(false);
            this.tableLayoutPanelOutput.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageTest.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageTest;
        private System.Windows.Forms.SplitContainer splitContainer;
        private PacketLogControl logPacketControlInput;
        private System.Windows.Forms.Label label2;
        private PacketLogControl logPacketControlOutput;
        private System.Windows.Forms.TabPage tabPageLog;
        private EventLogControl eventLogControl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOutput;
    }
}
