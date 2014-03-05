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

namespace CANAPE.Forms
{
    partial class ConfigurationForm
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
            System.Windows.Forms.GroupBox groupBoxSavingAndLoading;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            System.Windows.Forms.Label labelUIOptions;
            System.Windows.Forms.Label lblScriptType;
            System.Windows.Forms.GroupBox groupBoxScript;
            System.Windows.Forms.GroupBox groupBoxGeneral;
            this.numericUpDownAutoSaveTimer = new System.Windows.Forms.NumericUpDown();
            this.labelAutoSaveTimer = new System.Windows.Forms.Label();
            this.checkBoxAutoSave = new System.Windows.Forms.CheckBox();
            this.checkBoxCompress = new System.Windows.Forms.CheckBox();
            this.checkBoxBackup = new System.Windows.Forms.CheckBox();
            this.comboBoxScriptType = new System.Windows.Forms.ComboBox();
            this.scriptDocumentConfigEditorControl = new CANAPE.Controls.DocumentEditors.ScriptDocumentConfigEditorControl();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.lblFont = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabPageClient = new System.Windows.Forms.TabPage();
            this.labelClientHelp = new System.Windows.Forms.Label();
            this.proxyClientControl = new CANAPE.Controls.ProxyClientControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageScriptEditor = new System.Windows.Forms.TabPage();
            this.labelScriptHelp = new System.Windows.Forms.Label();
            this.tabPageOthers = new System.Windows.Forms.TabPage();
            this.btnResetToDefault = new System.Windows.Forms.Button();
            this.checkBoxCheckUpdates = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageUI = new System.Windows.Forms.TabPage();
            this.checkBoxOpenFindInDialog = new System.Windows.Forms.CheckBox();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLogConfirm = new System.Windows.Forms.ComboBox();
            this.checkBoxRequireEventLogClearConfirm = new System.Windows.Forms.CheckBox();
            this.checkBoxRequirePacketLogClearConfirm = new System.Windows.Forms.CheckBox();
            this.checkBoxNewStyle = new System.Windows.Forms.CheckBox();
            this.checkBoxConfirmClose = new System.Windows.Forms.CheckBox();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.checkBoxPacketLogMutable = new System.Windows.Forms.CheckBox();
            this.checkBoxShowStartupForm = new System.Windows.Forms.CheckBox();
            this.checkBoxShowSplash = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            groupBoxSavingAndLoading = new System.Windows.Forms.GroupBox();
            labelUIOptions = new System.Windows.Forms.Label();
            lblScriptType = new System.Windows.Forms.Label();
            groupBoxScript = new System.Windows.Forms.GroupBox();
            groupBoxGeneral = new System.Windows.Forms.GroupBox();
            groupBoxSavingAndLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoSaveTimer)).BeginInit();
            groupBoxScript.SuspendLayout();
            groupBoxGeneral.SuspendLayout();
            this.tabPageClient.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageScriptEditor.SuspendLayout();
            this.tabPageOthers.SuspendLayout();
            this.tabPageUI.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSavingAndLoading
            // 
            groupBoxSavingAndLoading.Controls.Add(this.numericUpDownAutoSaveTimer);
            groupBoxSavingAndLoading.Controls.Add(this.labelAutoSaveTimer);
            groupBoxSavingAndLoading.Controls.Add(this.checkBoxAutoSave);
            groupBoxSavingAndLoading.Controls.Add(this.checkBoxCompress);
            groupBoxSavingAndLoading.Controls.Add(this.checkBoxBackup);
            resources.ApplyResources(groupBoxSavingAndLoading, "groupBoxSavingAndLoading");
            groupBoxSavingAndLoading.Name = "groupBoxSavingAndLoading";
            groupBoxSavingAndLoading.TabStop = false;
            this.toolTip.SetToolTip(groupBoxSavingAndLoading, resources.GetString("groupBoxSavingAndLoading.ToolTip"));
            // 
            // numericUpDownAutoSaveTimer
            // 
            resources.ApplyResources(this.numericUpDownAutoSaveTimer, "numericUpDownAutoSaveTimer");
            this.numericUpDownAutoSaveTimer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAutoSaveTimer.Name = "numericUpDownAutoSaveTimer";
            this.toolTip.SetToolTip(this.numericUpDownAutoSaveTimer, resources.GetString("numericUpDownAutoSaveTimer.ToolTip"));
            this.numericUpDownAutoSaveTimer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelAutoSaveTimer
            // 
            resources.ApplyResources(this.labelAutoSaveTimer, "labelAutoSaveTimer");
            this.labelAutoSaveTimer.Name = "labelAutoSaveTimer";
            // 
            // checkBoxAutoSave
            // 
            resources.ApplyResources(this.checkBoxAutoSave, "checkBoxAutoSave");
            this.checkBoxAutoSave.Name = "checkBoxAutoSave";
            this.toolTip.SetToolTip(this.checkBoxAutoSave, resources.GetString("checkBoxAutoSave.ToolTip"));
            this.checkBoxAutoSave.UseVisualStyleBackColor = true;
            // 
            // checkBoxCompress
            // 
            resources.ApplyResources(this.checkBoxCompress, "checkBoxCompress");
            this.checkBoxCompress.Name = "checkBoxCompress";
            this.toolTip.SetToolTip(this.checkBoxCompress, resources.GetString("checkBoxCompress.ToolTip"));
            this.checkBoxCompress.UseVisualStyleBackColor = true;
            // 
            // checkBoxBackup
            // 
            resources.ApplyResources(this.checkBoxBackup, "checkBoxBackup");
            this.checkBoxBackup.Name = "checkBoxBackup";
            this.toolTip.SetToolTip(this.checkBoxBackup, resources.GetString("checkBoxBackup.ToolTip"));
            this.checkBoxBackup.UseVisualStyleBackColor = true;
            // 
            // labelUIOptions
            // 
            resources.ApplyResources(labelUIOptions, "labelUIOptions");
            labelUIOptions.Name = "labelUIOptions";
            // 
            // lblScriptType
            // 
            resources.ApplyResources(lblScriptType, "lblScriptType");
            lblScriptType.Name = "lblScriptType";
            // 
            // groupBoxScript
            // 
            groupBoxScript.Controls.Add(lblScriptType);
            groupBoxScript.Controls.Add(this.comboBoxScriptType);
            groupBoxScript.Controls.Add(this.scriptDocumentConfigEditorControl);
            resources.ApplyResources(groupBoxScript, "groupBoxScript");
            groupBoxScript.Name = "groupBoxScript";
            groupBoxScript.TabStop = false;
            // 
            // comboBoxScriptType
            // 
            this.comboBoxScriptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScriptType.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxScriptType, "comboBoxScriptType");
            this.comboBoxScriptType.Name = "comboBoxScriptType";
            this.comboBoxScriptType.SelectedIndexChanged += new System.EventHandler(this.comboBoxScriptType_SelectedIndexChanged);
            // 
            // scriptDocumentConfigEditorControl
            // 
            resources.ApplyResources(this.scriptDocumentConfigEditorControl, "scriptDocumentConfigEditorControl");
            this.scriptDocumentConfigEditorControl.Name = "scriptDocumentConfigEditorControl";
            this.scriptDocumentConfigEditorControl.ConfigChanged += new System.EventHandler(this.scriptDocumentConfigEditorControl_ConfigChanged);
            // 
            // groupBoxGeneral
            // 
            groupBoxGeneral.Controls.Add(this.btnSelectFont);
            groupBoxGeneral.Controls.Add(this.lblFont);
            resources.ApplyResources(groupBoxGeneral, "groupBoxGeneral");
            groupBoxGeneral.Name = "groupBoxGeneral";
            groupBoxGeneral.TabStop = false;
            // 
            // btnSelectFont
            // 
            resources.ApplyResources(this.btnSelectFont, "btnSelectFont");
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.UseVisualStyleBackColor = true;
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // lblFont
            // 
            resources.ApplyResources(this.lblFont, "lblFont");
            this.lblFont.Name = "lblFont";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabPageClient
            // 
            this.tabPageClient.Controls.Add(this.labelClientHelp);
            this.tabPageClient.Controls.Add(this.proxyClientControl);
            resources.ApplyResources(this.tabPageClient, "tabPageClient");
            this.tabPageClient.Name = "tabPageClient";
            this.tabPageClient.UseVisualStyleBackColor = true;
            // 
            // labelClientHelp
            // 
            resources.ApplyResources(this.labelClientHelp, "labelClientHelp");
            this.labelClientHelp.Name = "labelClientHelp";
            // 
            // proxyClientControl
            // 
            this.proxyClientControl.HideDefault = true;
            resources.ApplyResources(this.proxyClientControl, "proxyClientControl");
            this.proxyClientControl.Name = "proxyClientControl";
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageClient);
            this.tabControl.Controls.Add(this.tabPageScriptEditor);
            this.tabControl.Controls.Add(this.tabPageOthers);
            this.tabControl.Controls.Add(this.tabPageUI);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageScriptEditor
            // 
            this.tabPageScriptEditor.Controls.Add(groupBoxGeneral);
            this.tabPageScriptEditor.Controls.Add(groupBoxScript);
            this.tabPageScriptEditor.Controls.Add(this.labelScriptHelp);
            resources.ApplyResources(this.tabPageScriptEditor, "tabPageScriptEditor");
            this.tabPageScriptEditor.Name = "tabPageScriptEditor";
            this.tabPageScriptEditor.UseVisualStyleBackColor = true;
            // 
            // labelScriptHelp
            // 
            resources.ApplyResources(this.labelScriptHelp, "labelScriptHelp");
            this.labelScriptHelp.Name = "labelScriptHelp";
            // 
            // tabPageOthers
            // 
            this.tabPageOthers.Controls.Add(this.btnResetToDefault);
            this.tabPageOthers.Controls.Add(this.checkBoxCheckUpdates);
            this.tabPageOthers.Controls.Add(this.label1);
            this.tabPageOthers.Controls.Add(groupBoxSavingAndLoading);
            resources.ApplyResources(this.tabPageOthers, "tabPageOthers");
            this.tabPageOthers.Name = "tabPageOthers";
            this.tabPageOthers.UseVisualStyleBackColor = true;
            // 
            // btnResetToDefault
            // 
            resources.ApplyResources(this.btnResetToDefault, "btnResetToDefault");
            this.btnResetToDefault.Name = "btnResetToDefault";
            this.btnResetToDefault.UseVisualStyleBackColor = true;
            this.btnResetToDefault.Click += new System.EventHandler(this.btnResetToDefault_Click);
            // 
            // checkBoxCheckUpdates
            // 
            resources.ApplyResources(this.checkBoxCheckUpdates, "checkBoxCheckUpdates");
            this.checkBoxCheckUpdates.Name = "checkBoxCheckUpdates";
            this.toolTip.SetToolTip(this.checkBoxCheckUpdates, resources.GetString("checkBoxCheckUpdates.ToolTip"));
            this.checkBoxCheckUpdates.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabPageUI
            // 
            this.tabPageUI.Controls.Add(this.checkBoxOpenFindInDialog);
            this.tabPageUI.Controls.Add(this.comboBoxLanguage);
            this.tabPageUI.Controls.Add(this.lblLanguage);
            this.tabPageUI.Controls.Add(this.label2);
            this.tabPageUI.Controls.Add(this.comboBoxLogConfirm);
            this.tabPageUI.Controls.Add(this.checkBoxRequireEventLogClearConfirm);
            this.tabPageUI.Controls.Add(this.checkBoxRequirePacketLogClearConfirm);
            this.tabPageUI.Controls.Add(this.checkBoxNewStyle);
            this.tabPageUI.Controls.Add(labelUIOptions);
            this.tabPageUI.Controls.Add(this.checkBoxConfirmClose);
            this.tabPageUI.Controls.Add(this.btnClearHistory);
            this.tabPageUI.Controls.Add(this.checkBoxPacketLogMutable);
            this.tabPageUI.Controls.Add(this.checkBoxShowStartupForm);
            this.tabPageUI.Controls.Add(this.checkBoxShowSplash);
            resources.ApplyResources(this.tabPageUI, "tabPageUI");
            this.tabPageUI.Name = "tabPageUI";
            this.tabPageUI.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpenFindInDialog
            // 
            resources.ApplyResources(this.checkBoxOpenFindInDialog, "checkBoxOpenFindInDialog");
            this.checkBoxOpenFindInDialog.Name = "checkBoxOpenFindInDialog";
            this.toolTip.SetToolTip(this.checkBoxOpenFindInDialog, resources.GetString("checkBoxOpenFindInDialog.ToolTip"));
            this.checkBoxOpenFindInDialog.UseVisualStyleBackColor = true;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DisplayMember = "NativeName";
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxLanguage, "comboBoxLanguage");
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            // 
            // lblLanguage
            // 
            resources.ApplyResources(this.lblLanguage, "lblLanguage");
            this.lblLanguage.Name = "lblLanguage";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBoxLogConfirm
            // 
            this.comboBoxLogConfirm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogConfirm.FormattingEnabled = true;
            this.comboBoxLogConfirm.Items.AddRange(new object[] {
            resources.GetString("comboBoxLogConfirm.Items"),
            resources.GetString("comboBoxLogConfirm.Items1"),
            resources.GetString("comboBoxLogConfirm.Items2")});
            resources.ApplyResources(this.comboBoxLogConfirm, "comboBoxLogConfirm");
            this.comboBoxLogConfirm.Name = "comboBoxLogConfirm";
            this.toolTip.SetToolTip(this.comboBoxLogConfirm, resources.GetString("comboBoxLogConfirm.ToolTip"));
            // 
            // checkBoxRequireEventLogClearConfirm
            // 
            resources.ApplyResources(this.checkBoxRequireEventLogClearConfirm, "checkBoxRequireEventLogClearConfirm");
            this.checkBoxRequireEventLogClearConfirm.Name = "checkBoxRequireEventLogClearConfirm";
            this.toolTip.SetToolTip(this.checkBoxRequireEventLogClearConfirm, resources.GetString("checkBoxRequireEventLogClearConfirm.ToolTip"));
            this.checkBoxRequireEventLogClearConfirm.UseVisualStyleBackColor = true;
            // 
            // checkBoxRequirePacketLogClearConfirm
            // 
            resources.ApplyResources(this.checkBoxRequirePacketLogClearConfirm, "checkBoxRequirePacketLogClearConfirm");
            this.checkBoxRequirePacketLogClearConfirm.Name = "checkBoxRequirePacketLogClearConfirm";
            this.toolTip.SetToolTip(this.checkBoxRequirePacketLogClearConfirm, resources.GetString("checkBoxRequirePacketLogClearConfirm.ToolTip"));
            this.checkBoxRequirePacketLogClearConfirm.UseVisualStyleBackColor = true;
            // 
            // checkBoxNewStyle
            // 
            resources.ApplyResources(this.checkBoxNewStyle, "checkBoxNewStyle");
            this.checkBoxNewStyle.Name = "checkBoxNewStyle";
            this.toolTip.SetToolTip(this.checkBoxNewStyle, resources.GetString("checkBoxNewStyle.ToolTip"));
            this.checkBoxNewStyle.UseVisualStyleBackColor = true;
            // 
            // checkBoxConfirmClose
            // 
            resources.ApplyResources(this.checkBoxConfirmClose, "checkBoxConfirmClose");
            this.checkBoxConfirmClose.Name = "checkBoxConfirmClose";
            this.toolTip.SetToolTip(this.checkBoxConfirmClose, resources.GetString("checkBoxConfirmClose.ToolTip"));
            this.checkBoxConfirmClose.UseVisualStyleBackColor = true;
            // 
            // btnClearHistory
            // 
            resources.ApplyResources(this.btnClearHistory, "btnClearHistory");
            this.btnClearHistory.Name = "btnClearHistory";
            this.toolTip.SetToolTip(this.btnClearHistory, resources.GetString("btnClearHistory.ToolTip"));
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // checkBoxPacketLogMutable
            // 
            resources.ApplyResources(this.checkBoxPacketLogMutable, "checkBoxPacketLogMutable");
            this.checkBoxPacketLogMutable.Name = "checkBoxPacketLogMutable";
            this.toolTip.SetToolTip(this.checkBoxPacketLogMutable, resources.GetString("checkBoxPacketLogMutable.ToolTip"));
            this.checkBoxPacketLogMutable.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowStartupForm
            // 
            resources.ApplyResources(this.checkBoxShowStartupForm, "checkBoxShowStartupForm");
            this.checkBoxShowStartupForm.Name = "checkBoxShowStartupForm";
            this.toolTip.SetToolTip(this.checkBoxShowStartupForm, resources.GetString("checkBoxShowStartupForm.ToolTip"));
            this.checkBoxShowStartupForm.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowSplash
            // 
            resources.ApplyResources(this.checkBoxShowSplash, "checkBoxShowSplash");
            this.checkBoxShowSplash.Name = "checkBoxShowSplash";
            this.toolTip.SetToolTip(this.checkBoxShowSplash, resources.GetString("checkBoxShowSplash.ToolTip"));
            this.checkBoxShowSplash.UseVisualStyleBackColor = true;
            // 
            // ConfigurationForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            groupBoxSavingAndLoading.ResumeLayout(false);
            groupBoxSavingAndLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoSaveTimer)).EndInit();
            groupBoxScript.ResumeLayout(false);
            groupBoxScript.PerformLayout();
            groupBoxGeneral.ResumeLayout(false);
            groupBoxGeneral.PerformLayout();
            this.tabPageClient.ResumeLayout(false);
            this.tabPageClient.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageScriptEditor.ResumeLayout(false);
            this.tabPageScriptEditor.PerformLayout();
            this.tabPageOthers.ResumeLayout(false);
            this.tabPageOthers.PerformLayout();
            this.tabPageUI.ResumeLayout(false);
            this.tabPageUI.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabPageClient;
        private Controls.ProxyClientControl proxyClientControl;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageScriptEditor;
        private System.Windows.Forms.Label labelClientHelp;
        private System.Windows.Forms.Label labelScriptHelp;
        private System.Windows.Forms.TabPage tabPageOthers;
        private System.Windows.Forms.CheckBox checkBoxBackup;
        private System.Windows.Forms.CheckBox checkBoxCompress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabPageUI;
        private System.Windows.Forms.CheckBox checkBoxConfirmClose;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.CheckBox checkBoxPacketLogMutable;
        private System.Windows.Forms.CheckBox checkBoxShowStartupForm;
        private System.Windows.Forms.CheckBox checkBoxShowSplash;
        private System.Windows.Forms.CheckBox checkBoxNewStyle;
        private System.Windows.Forms.CheckBox checkBoxRequirePacketLogClearConfirm;
        private System.Windows.Forms.CheckBox checkBoxRequireEventLogClearConfirm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLogConfirm;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.CheckBox checkBoxCheckUpdates;
        private System.Windows.Forms.CheckBox checkBoxOpenFindInDialog;
        private System.Windows.Forms.CheckBox checkBoxAutoSave;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoSaveTimer;
        private System.Windows.Forms.Label labelAutoSaveTimer;
        private Controls.DocumentEditors.ScriptDocumentConfigEditorControl scriptDocumentConfigEditorControl;
        private System.Windows.Forms.Button btnResetToDefault;
        private System.Windows.Forms.ComboBox comboBoxScriptType;
        private System.Windows.Forms.Button btnSelectFont;
        private System.Windows.Forms.Label lblFont;
    }
}