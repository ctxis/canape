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

using CANAPE.Nodes;
namespace CANAPE.Controls
{
    partial class InjectPacketControl
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
            System.Windows.Forms.Label lblRepeatCount;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InjectPacketControl));
            System.Windows.Forms.Label lblConnection;
            System.Windows.Forms.Label lblNode;
            this.btnInject = new System.Windows.Forms.Button();
            this.comboBoxNodes = new System.Windows.Forms.ComboBox();
            this.numericRepeatCount = new System.Windows.Forms.NumericUpDown();
            this.comboBoxConnection = new System.Windows.Forms.ComboBox();
            this.checkBoxInjectSelected = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPagePackets = new System.Windows.Forms.TabPage();
            this.logPacketControl = new CANAPE.Controls.PacketLogControl();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.numericUpDownPacketDelay = new System.Windows.Forms.NumericUpDown();
            this.checkBoxPacketDelay = new System.Windows.Forms.CheckBox();
            this.checkBoxScript = new System.Windows.Forms.CheckBox();
            this.scriptSelectionControl = new CANAPE.Controls.ScriptSelectionControl();
            this.timerCancel = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            lblRepeatCount = new System.Windows.Forms.Label();
            lblConnection = new System.Windows.Forms.Label();
            lblNode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericRepeatCount)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPagePackets.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRepeatCount
            // 
            resources.ApplyResources(lblRepeatCount, "lblRepeatCount");
            lblRepeatCount.Name = "lblRepeatCount";
            this.toolTip.SetToolTip(lblRepeatCount, resources.GetString("lblRepeatCount.ToolTip"));
            // 
            // lblConnection
            // 
            resources.ApplyResources(lblConnection, "lblConnection");
            lblConnection.Name = "lblConnection";
            this.toolTip.SetToolTip(lblConnection, resources.GetString("lblConnection.ToolTip"));
            // 
            // lblNode
            // 
            resources.ApplyResources(lblNode, "lblNode");
            lblNode.Name = "lblNode";
            this.toolTip.SetToolTip(lblNode, resources.GetString("lblNode.ToolTip"));
            // 
            // btnInject
            // 
            resources.ApplyResources(this.btnInject, "btnInject");
            this.btnInject.Name = "btnInject";
            this.toolTip.SetToolTip(this.btnInject, resources.GetString("btnInject.ToolTip"));
            this.btnInject.UseVisualStyleBackColor = true;
            this.btnInject.Click += new System.EventHandler(this.btnInject_Click);
            // 
            // comboBoxNodes
            // 
            resources.ApplyResources(this.comboBoxNodes, "comboBoxNodes");
            this.comboBoxNodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNodes.Name = "comboBoxNodes";
            this.toolTip.SetToolTip(this.comboBoxNodes, resources.GetString("comboBoxNodes.ToolTip"));
            // 
            // numericRepeatCount
            // 
            resources.ApplyResources(this.numericRepeatCount, "numericRepeatCount");
            this.numericRepeatCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericRepeatCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRepeatCount.Name = "numericRepeatCount";
            this.toolTip.SetToolTip(this.numericRepeatCount, resources.GetString("numericRepeatCount.ToolTip"));
            this.numericRepeatCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboBoxConnection
            // 
            resources.ApplyResources(this.comboBoxConnection, "comboBoxConnection");
            this.comboBoxConnection.DisplayMember = "NetworkDescription";
            this.comboBoxConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConnection.FormattingEnabled = true;
            this.comboBoxConnection.Name = "comboBoxConnection";
            this.toolTip.SetToolTip(this.comboBoxConnection, resources.GetString("comboBoxConnection.ToolTip"));
            this.comboBoxConnection.DropDown += new System.EventHandler(this.comboBoxConnection_DropDown);
            this.comboBoxConnection.SelectedIndexChanged += new System.EventHandler(this.comboBoxConnection_SelectedIndexChanged);
            // 
            // checkBoxInjectSelected
            // 
            resources.ApplyResources(this.checkBoxInjectSelected, "checkBoxInjectSelected");
            this.checkBoxInjectSelected.Name = "checkBoxInjectSelected";
            this.toolTip.SetToolTip(this.checkBoxInjectSelected, resources.GetString("checkBoxInjectSelected.ToolTip"));
            this.checkBoxInjectSelected.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPagePackets);
            this.tabControl.Controls.Add(this.tabPageAdvanced);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.toolTip.SetToolTip(this.tabControl, resources.GetString("tabControl.ToolTip"));
            // 
            // tabPagePackets
            // 
            resources.ApplyResources(this.tabPagePackets, "tabPagePackets");
            this.tabPagePackets.Controls.Add(this.comboBoxConnection);
            this.tabPagePackets.Controls.Add(lblConnection);
            this.tabPagePackets.Controls.Add(this.logPacketControl);
            this.tabPagePackets.Controls.Add(this.checkBoxInjectSelected);
            this.tabPagePackets.Controls.Add(this.btnInject);
            this.tabPagePackets.Controls.Add(this.comboBoxNodes);
            this.tabPagePackets.Controls.Add(lblNode);
            this.tabPagePackets.Controls.Add(this.numericRepeatCount);
            this.tabPagePackets.Controls.Add(lblRepeatCount);
            this.tabPagePackets.Name = "tabPagePackets";
            this.toolTip.SetToolTip(this.tabPagePackets, resources.GetString("tabPagePackets.ToolTip"));
            this.tabPagePackets.UseVisualStyleBackColor = true;
            // 
            // logPacketControl
            // 
            resources.ApplyResources(this.logPacketControl, "logPacketControl");
            this.logPacketControl.IsInFindForm = false;
            this.logPacketControl.LogName = null;
            this.logPacketControl.Name = "logPacketControl";
            this.logPacketControl.ReadOnly = false;
            this.toolTip.SetToolTip(this.logPacketControl, resources.GetString("logPacketControl.ToolTip"));
            // 
            // tabPageAdvanced
            // 
            resources.ApplyResources(this.tabPageAdvanced, "tabPageAdvanced");
            this.tabPageAdvanced.Controls.Add(this.numericUpDownPacketDelay);
            this.tabPageAdvanced.Controls.Add(this.checkBoxPacketDelay);
            this.tabPageAdvanced.Controls.Add(this.checkBoxScript);
            this.tabPageAdvanced.Controls.Add(this.scriptSelectionControl);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.toolTip.SetToolTip(this.tabPageAdvanced, resources.GetString("tabPageAdvanced.ToolTip"));
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // numericUpDownPacketDelay
            // 
            resources.ApplyResources(this.numericUpDownPacketDelay, "numericUpDownPacketDelay");
            this.numericUpDownPacketDelay.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownPacketDelay.Name = "numericUpDownPacketDelay";
            this.toolTip.SetToolTip(this.numericUpDownPacketDelay, resources.GetString("numericUpDownPacketDelay.ToolTip"));
            this.numericUpDownPacketDelay.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownPacketDelay.ValueChanged += new System.EventHandler(this.numericUpDownPacketDelay_ValueChanged);
            // 
            // checkBoxPacketDelay
            // 
            resources.ApplyResources(this.checkBoxPacketDelay, "checkBoxPacketDelay");
            this.checkBoxPacketDelay.Name = "checkBoxPacketDelay";
            this.toolTip.SetToolTip(this.checkBoxPacketDelay, resources.GetString("checkBoxPacketDelay.ToolTip"));
            this.checkBoxPacketDelay.UseVisualStyleBackColor = true;
            this.checkBoxPacketDelay.CheckedChanged += new System.EventHandler(this.checkBoxPacketDelay_CheckedChanged);
            // 
            // checkBoxScript
            // 
            resources.ApplyResources(this.checkBoxScript, "checkBoxScript");
            this.checkBoxScript.Name = "checkBoxScript";
            this.toolTip.SetToolTip(this.checkBoxScript, resources.GetString("checkBoxScript.ToolTip"));
            this.checkBoxScript.UseVisualStyleBackColor = true;
            this.checkBoxScript.CheckedChanged += new System.EventHandler(this.checkBoxScript_CheckedChanged);
            // 
            // scriptSelectionControl
            // 
            resources.ApplyResources(this.scriptSelectionControl, "scriptSelectionControl");
            this.scriptSelectionControl.ClassName = null;
            this.scriptSelectionControl.Document = null;
            this.scriptSelectionControl.Name = "scriptSelectionControl";
            this.toolTip.SetToolTip(this.scriptSelectionControl, resources.GetString("scriptSelectionControl.ToolTip"));
            this.scriptSelectionControl.DocumentChanged += new System.EventHandler(this.scriptSelectionControl_DocumentChanged);
            this.scriptSelectionControl.ClassNameChanged += new System.EventHandler(this.scriptSelectionControl_ClassNameChanged);
            // 
            // timerCancel
            // 
            this.timerCancel.Interval = 30000;
            this.timerCancel.Tick += new System.EventHandler(this.timerCancel_Tick);
            // 
            // InjectPacketControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "InjectPacketControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            ((System.ComponentModel.ISupportInitialize)(this.numericRepeatCount)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPagePackets.ResumeLayout(false);
            this.tabPagePackets.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPacketDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInject;
        private System.Windows.Forms.ComboBox comboBoxNodes;
        private CANAPE.Controls.PacketLogControl logPacketControl;
        private System.Windows.Forms.NumericUpDown numericRepeatCount;
        private System.Windows.Forms.ComboBox comboBoxConnection;
        private System.Windows.Forms.CheckBox checkBoxInjectSelected;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPagePackets;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.CheckBox checkBoxScript;
        private System.Windows.Forms.NumericUpDown numericUpDownPacketDelay;
        private System.Windows.Forms.CheckBox checkBoxPacketDelay;
        private ScriptSelectionControl scriptSelectionControl;
        private System.Windows.Forms.Timer timerCancel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}