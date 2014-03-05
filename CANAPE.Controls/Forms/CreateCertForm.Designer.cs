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
    public partial class CreateCertForm
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
            System.Windows.Forms.GroupBox groupBoxSigning;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateCertForm));
            this.radioButtonSelfSign = new System.Windows.Forms.RadioButton();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.btnLoadStore = new System.Windows.Forms.Button();
            this.radioButtonDefaultCA = new System.Windows.Forms.RadioButton();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.radioButtonSpecifyCA = new System.Windows.Forms.RadioButton();
            this.lblSubject = new System.Windows.Forms.Label();
            this.textBoxCN = new System.Windows.Forms.TextBox();
            this.labelCommonName = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnLoadStoreCert = new System.Windows.Forms.Button();
            this.btnLoadFileCert = new System.Windows.Forms.Button();
            this.checkBoxCA = new System.Windows.Forms.CheckBox();
            this.radioButtonSimpleCN = new System.Windows.Forms.RadioButton();
            this.groupBoxCert = new System.Windows.Forms.GroupBox();
            this.comboBoxRsaKeySize = new System.Windows.Forms.ComboBox();
            this.lblKeySize = new System.Windows.Forms.Label();
            this.radioButtonSubject = new System.Windows.Forms.RadioButton();
            this.radioButtonTemplate = new System.Windows.Forms.RadioButton();
            this.lblHash = new System.Windows.Forms.Label();
            this.comboBoxHash = new System.Windows.Forms.ComboBox();
            groupBoxSigning = new System.Windows.Forms.GroupBox();
            groupBoxSigning.SuspendLayout();
            this.groupBoxCert.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSigning
            // 
            groupBoxSigning.Controls.Add(this.radioButtonSelfSign);
            groupBoxSigning.Controls.Add(this.textBoxSubject);
            groupBoxSigning.Controls.Add(this.btnLoadStore);
            groupBoxSigning.Controls.Add(this.radioButtonDefaultCA);
            groupBoxSigning.Controls.Add(this.btnLoadFile);
            groupBoxSigning.Controls.Add(this.radioButtonSpecifyCA);
            groupBoxSigning.Controls.Add(this.lblSubject);
            resources.ApplyResources(groupBoxSigning, "groupBoxSigning");
            groupBoxSigning.Name = "groupBoxSigning";
            groupBoxSigning.TabStop = false;
            // 
            // radioButtonSelfSign
            // 
            resources.ApplyResources(this.radioButtonSelfSign, "radioButtonSelfSign");
            this.radioButtonSelfSign.Name = "radioButtonSelfSign";
            this.radioButtonSelfSign.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonSelfSign, resources.GetString("radioButtonSelfSign.ToolTip"));
            this.radioButtonSelfSign.UseVisualStyleBackColor = true;
            // 
            // textBoxSubject
            // 
            resources.ApplyResources(this.textBoxSubject, "textBoxSubject");
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBoxSubject, resources.GetString("textBoxSubject.ToolTip"));
            // 
            // btnLoadStore
            // 
            resources.ApplyResources(this.btnLoadStore, "btnLoadStore");
            this.btnLoadStore.Name = "btnLoadStore";
            this.toolTip.SetToolTip(this.btnLoadStore, resources.GetString("btnLoadStore.ToolTip"));
            this.btnLoadStore.UseVisualStyleBackColor = true;
            this.btnLoadStore.Click += new System.EventHandler(this.btnLoadStore_Click);
            // 
            // radioButtonDefaultCA
            // 
            resources.ApplyResources(this.radioButtonDefaultCA, "radioButtonDefaultCA");
            this.radioButtonDefaultCA.Checked = true;
            this.radioButtonDefaultCA.Name = "radioButtonDefaultCA";
            this.radioButtonDefaultCA.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonDefaultCA, resources.GetString("radioButtonDefaultCA.ToolTip"));
            this.radioButtonDefaultCA.UseVisualStyleBackColor = true;
            // 
            // btnLoadFile
            // 
            resources.ApplyResources(this.btnLoadFile, "btnLoadFile");
            this.btnLoadFile.Name = "btnLoadFile";
            this.toolTip.SetToolTip(this.btnLoadFile, resources.GetString("btnLoadFile.ToolTip"));
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // radioButtonSpecifyCA
            // 
            resources.ApplyResources(this.radioButtonSpecifyCA, "radioButtonSpecifyCA");
            this.radioButtonSpecifyCA.Name = "radioButtonSpecifyCA";
            this.toolTip.SetToolTip(this.radioButtonSpecifyCA, resources.GetString("radioButtonSpecifyCA.ToolTip"));
            this.radioButtonSpecifyCA.UseVisualStyleBackColor = true;
            this.radioButtonSpecifyCA.CheckedChanged += new System.EventHandler(this.radioButtonSpecifyCA_CheckedChanged);
            // 
            // lblSubject
            // 
            resources.ApplyResources(this.lblSubject, "lblSubject");
            this.lblSubject.Name = "lblSubject";
            // 
            // textBoxCN
            // 
            resources.ApplyResources(this.textBoxCN, "textBoxCN");
            this.textBoxCN.Name = "textBoxCN";
            this.toolTip.SetToolTip(this.textBoxCN, resources.GetString("textBoxCN.ToolTip"));
            // 
            // labelCommonName
            // 
            resources.ApplyResources(this.labelCommonName, "labelCommonName");
            this.labelCommonName.Name = "labelCommonName";
            // 
            // buttonCreate
            // 
            this.buttonCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonCreate, "buttonCreate");
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // btnLoadStoreCert
            // 
            resources.ApplyResources(this.btnLoadStoreCert, "btnLoadStoreCert");
            this.btnLoadStoreCert.Name = "btnLoadStoreCert";
            this.toolTip.SetToolTip(this.btnLoadStoreCert, resources.GetString("btnLoadStoreCert.ToolTip"));
            this.btnLoadStoreCert.UseVisualStyleBackColor = true;
            this.btnLoadStoreCert.Click += new System.EventHandler(this.btnLoadStoreCert_Click);
            // 
            // btnLoadFileCert
            // 
            resources.ApplyResources(this.btnLoadFileCert, "btnLoadFileCert");
            this.btnLoadFileCert.Name = "btnLoadFileCert";
            this.toolTip.SetToolTip(this.btnLoadFileCert, resources.GetString("btnLoadFileCert.ToolTip"));
            this.btnLoadFileCert.UseVisualStyleBackColor = true;
            this.btnLoadFileCert.Click += new System.EventHandler(this.btnLoadFileCert_Click);
            // 
            // checkBoxCA
            // 
            resources.ApplyResources(this.checkBoxCA, "checkBoxCA");
            this.checkBoxCA.Name = "checkBoxCA";
            this.toolTip.SetToolTip(this.checkBoxCA, resources.GetString("checkBoxCA.ToolTip"));
            this.checkBoxCA.UseVisualStyleBackColor = true;
            // 
            // radioButtonSimpleCN
            // 
            resources.ApplyResources(this.radioButtonSimpleCN, "radioButtonSimpleCN");
            this.radioButtonSimpleCN.Checked = true;
            this.radioButtonSimpleCN.Name = "radioButtonSimpleCN";
            this.radioButtonSimpleCN.TabStop = true;
            this.radioButtonSimpleCN.UseVisualStyleBackColor = true;
            // 
            // groupBoxCert
            // 
            this.groupBoxCert.Controls.Add(this.comboBoxHash);
            this.groupBoxCert.Controls.Add(this.lblHash);
            this.groupBoxCert.Controls.Add(this.comboBoxRsaKeySize);
            this.groupBoxCert.Controls.Add(this.lblKeySize);
            this.groupBoxCert.Controls.Add(this.radioButtonSubject);
            this.groupBoxCert.Controls.Add(this.checkBoxCA);
            this.groupBoxCert.Controls.Add(this.radioButtonTemplate);
            this.groupBoxCert.Controls.Add(this.btnLoadStoreCert);
            this.groupBoxCert.Controls.Add(this.btnLoadFileCert);
            this.groupBoxCert.Controls.Add(this.radioButtonSimpleCN);
            this.groupBoxCert.Controls.Add(this.labelCommonName);
            this.groupBoxCert.Controls.Add(this.textBoxCN);
            resources.ApplyResources(this.groupBoxCert, "groupBoxCert");
            this.groupBoxCert.Name = "groupBoxCert";
            this.groupBoxCert.TabStop = false;
            // 
            // comboBoxRsaKeySize
            // 
            this.comboBoxRsaKeySize.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxRsaKeySize, "comboBoxRsaKeySize");
            this.comboBoxRsaKeySize.Name = "comboBoxRsaKeySize";
            // 
            // lblKeySize
            // 
            resources.ApplyResources(this.lblKeySize, "lblKeySize");
            this.lblKeySize.Name = "lblKeySize";
            // 
            // radioButtonSubject
            // 
            resources.ApplyResources(this.radioButtonSubject, "radioButtonSubject");
            this.radioButtonSubject.Name = "radioButtonSubject";
            this.radioButtonSubject.TabStop = true;
            this.radioButtonSubject.UseVisualStyleBackColor = true;
            // 
            // radioButtonTemplate
            // 
            resources.ApplyResources(this.radioButtonTemplate, "radioButtonTemplate");
            this.radioButtonTemplate.Name = "radioButtonTemplate";
            this.radioButtonTemplate.UseVisualStyleBackColor = true;
            this.radioButtonTemplate.CheckedChanged += new System.EventHandler(this.radioButtonTemplate_CheckedChanged);
            // 
            // lblHash
            // 
            resources.ApplyResources(this.lblHash, "lblHash");
            this.lblHash.Name = "lblHash";
            // 
            // comboBoxHash
            // 
            this.comboBoxHash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHash.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxHash, "comboBoxHash");
            this.comboBoxHash.Name = "comboBoxHash";
            // 
            // CreateCertForm
            // 
            this.AcceptButton = this.buttonCreate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.groupBoxCert);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(groupBoxSigning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateCertForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateCertForm_FormClosing);
            groupBoxSigning.ResumeLayout(false);
            groupBoxSigning.PerformLayout();
            this.groupBoxCert.ResumeLayout(false);
            this.groupBoxCert.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCN;
        private System.Windows.Forms.Label labelCommonName;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RadioButton radioButtonDefaultCA;
        private System.Windows.Forms.RadioButton radioButtonSpecifyCA;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnLoadStore;
        private System.Windows.Forms.RadioButton radioButtonSimpleCN;
        private System.Windows.Forms.GroupBox groupBoxCert;
        private System.Windows.Forms.RadioButton radioButtonTemplate;
        private System.Windows.Forms.Button btnLoadStoreCert;
        private System.Windows.Forms.Button btnLoadFileCert;
        private System.Windows.Forms.RadioButton radioButtonSelfSign;
        private System.Windows.Forms.CheckBox checkBoxCA;
        private System.Windows.Forms.RadioButton radioButtonSubject;
        private System.Windows.Forms.ComboBox comboBoxRsaKeySize;
        private System.Windows.Forms.Label lblKeySize;
        private System.Windows.Forms.ComboBox comboBoxHash;
        private System.Windows.Forms.Label lblHash;
    }
}