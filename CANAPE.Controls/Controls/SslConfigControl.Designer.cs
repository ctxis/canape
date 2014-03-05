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
    partial class SslConfigControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SslConfigControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.radioSpecify = new System.Windows.Forms.RadioButton();
            this.radioAuto = new System.Windows.Forms.RadioButton();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.buttonLoadFromFile = new System.Windows.Forms.Button();
            this.buttonLoadStore = new System.Windows.Forms.Button();
            this.buttonView = new System.Windows.Forms.Button();
            this.labelSubjectName = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.lblClientCert = new System.Windows.Forms.Label();
            this.textBoxClientCert = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.btnLoadClient = new System.Windows.Forms.Button();
            this.btnViewClient = new System.Windows.Forms.Button();
            this.btnLoadStoreClient = new System.Windows.Forms.Button();
            this.checkBoxSsl = new System.Windows.Forms.CheckBox();
            this.btnAdvancedOptions = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.groupBox);
            this.splitContainer.Panel1.Controls.Add(this.textBoxSubject);
            this.splitContainer.Panel1.Controls.Add(this.buttonLoadFromFile);
            this.splitContainer.Panel1.Controls.Add(this.buttonLoadStore);
            this.splitContainer.Panel1.Controls.Add(this.buttonView);
            this.splitContainer.Panel1.Controls.Add(this.labelSubjectName);
            this.splitContainer.Panel1.Controls.Add(this.buttonCreate);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lblClientCert);
            this.splitContainer.Panel2.Controls.Add(this.textBoxClientCert);
            this.splitContainer.Panel2.Controls.Add(this.buttonClear);
            this.splitContainer.Panel2.Controls.Add(this.btnLoadClient);
            this.splitContainer.Panel2.Controls.Add(this.btnViewClient);
            this.splitContainer.Panel2.Controls.Add(this.btnLoadStoreClient);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.radioSpecify);
            this.groupBox.Controls.Add(this.radioAuto);
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox, resources.GetString("groupBox.ToolTip"));
            // 
            // radioSpecify
            // 
            resources.ApplyResources(this.radioSpecify, "radioSpecify");
            this.radioSpecify.Name = "radioSpecify";
            this.radioSpecify.TabStop = true;
            this.toolTip.SetToolTip(this.radioSpecify, resources.GetString("radioSpecify.ToolTip"));
            this.radioSpecify.UseVisualStyleBackColor = true;
            this.radioSpecify.CheckedChanged += new System.EventHandler(this.radioSpecify_CheckedChanged);
            // 
            // radioAuto
            // 
            resources.ApplyResources(this.radioAuto, "radioAuto");
            this.radioAuto.Checked = true;
            this.radioAuto.Name = "radioAuto";
            this.radioAuto.TabStop = true;
            this.toolTip.SetToolTip(this.radioAuto, resources.GetString("radioAuto.ToolTip"));
            this.radioAuto.UseVisualStyleBackColor = true;
            // 
            // textBoxSubject
            // 
            resources.ApplyResources(this.textBoxSubject, "textBoxSubject");
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBoxSubject, resources.GetString("textBoxSubject.ToolTip"));
            // 
            // buttonLoadFromFile
            // 
            resources.ApplyResources(this.buttonLoadFromFile, "buttonLoadFromFile");
            this.buttonLoadFromFile.Name = "buttonLoadFromFile";
            this.toolTip.SetToolTip(this.buttonLoadFromFile, resources.GetString("buttonLoadFromFile.ToolTip"));
            this.buttonLoadFromFile.UseVisualStyleBackColor = true;
            this.buttonLoadFromFile.Click += new System.EventHandler(this.buttonLoadFromFile_Click);
            // 
            // buttonLoadStore
            // 
            resources.ApplyResources(this.buttonLoadStore, "buttonLoadStore");
            this.buttonLoadStore.Name = "buttonLoadStore";
            this.toolTip.SetToolTip(this.buttonLoadStore, resources.GetString("buttonLoadStore.ToolTip"));
            this.buttonLoadStore.UseVisualStyleBackColor = true;
            this.buttonLoadStore.Click += new System.EventHandler(this.buttonLoadStore_Click);
            // 
            // buttonView
            // 
            resources.ApplyResources(this.buttonView, "buttonView");
            this.buttonView.Name = "buttonView";
            this.toolTip.SetToolTip(this.buttonView, resources.GetString("buttonView.ToolTip"));
            this.buttonView.UseVisualStyleBackColor = true;
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            // 
            // labelSubjectName
            // 
            resources.ApplyResources(this.labelSubjectName, "labelSubjectName");
            this.labelSubjectName.Name = "labelSubjectName";
            // 
            // buttonCreate
            // 
            resources.ApplyResources(this.buttonCreate, "buttonCreate");
            this.buttonCreate.Name = "buttonCreate";
            this.toolTip.SetToolTip(this.buttonCreate, resources.GetString("buttonCreate.ToolTip"));
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // lblClientCert
            // 
            resources.ApplyResources(this.lblClientCert, "lblClientCert");
            this.lblClientCert.Name = "lblClientCert";
            // 
            // textBoxClientCert
            // 
            resources.ApplyResources(this.textBoxClientCert, "textBoxClientCert");
            this.textBoxClientCert.Name = "textBoxClientCert";
            this.textBoxClientCert.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBoxClientCert, resources.GetString("textBoxClientCert.ToolTip"));
            // 
            // buttonClear
            // 
            resources.ApplyResources(this.buttonClear, "buttonClear");
            this.buttonClear.Name = "buttonClear";
            this.toolTip.SetToolTip(this.buttonClear, resources.GetString("buttonClear.ToolTip"));
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // btnLoadClient
            // 
            resources.ApplyResources(this.btnLoadClient, "btnLoadClient");
            this.btnLoadClient.Name = "btnLoadClient";
            this.toolTip.SetToolTip(this.btnLoadClient, resources.GetString("btnLoadClient.ToolTip"));
            this.btnLoadClient.UseVisualStyleBackColor = true;
            this.btnLoadClient.Click += new System.EventHandler(this.btnLoadClient_Click);
            // 
            // btnViewClient
            // 
            resources.ApplyResources(this.btnViewClient, "btnViewClient");
            this.btnViewClient.Name = "btnViewClient";
            this.toolTip.SetToolTip(this.btnViewClient, resources.GetString("btnViewClient.ToolTip"));
            this.btnViewClient.UseVisualStyleBackColor = true;
            this.btnViewClient.Click += new System.EventHandler(this.btnViewClient_Click);
            // 
            // btnLoadStoreClient
            // 
            resources.ApplyResources(this.btnLoadStoreClient, "btnLoadStoreClient");
            this.btnLoadStoreClient.Name = "btnLoadStoreClient";
            this.toolTip.SetToolTip(this.btnLoadStoreClient, resources.GetString("btnLoadStoreClient.ToolTip"));
            this.btnLoadStoreClient.UseVisualStyleBackColor = true;
            this.btnLoadStoreClient.Click += new System.EventHandler(this.btnLoadStoreClient_Click);
            // 
            // checkBoxSsl
            // 
            resources.ApplyResources(this.checkBoxSsl, "checkBoxSsl");
            this.checkBoxSsl.Name = "checkBoxSsl";
            this.toolTip.SetToolTip(this.checkBoxSsl, resources.GetString("checkBoxSsl.ToolTip"));
            this.checkBoxSsl.UseVisualStyleBackColor = true;
            this.checkBoxSsl.CheckedChanged += new System.EventHandler(this.checkBoxSsl_CheckedChanged);
            // 
            // btnAdvancedOptions
            // 
            resources.ApplyResources(this.btnAdvancedOptions, "btnAdvancedOptions");
            this.btnAdvancedOptions.Name = "btnAdvancedOptions";
            this.toolTip.SetToolTip(this.btnAdvancedOptions, resources.GetString("btnAdvancedOptions.ToolTip"));
            this.btnAdvancedOptions.UseVisualStyleBackColor = true;
            this.btnAdvancedOptions.Click += new System.EventHandler(this.btnAdvancedOptions_Click);
            // 
            // SslConfigControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.btnAdvancedOptions);
            this.Controls.Add(this.checkBoxSsl);
            this.Name = "SslConfigControl";
            this.Load += new System.EventHandler(this.SslConfigControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioAuto;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton radioSpecify;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.Button buttonLoadFromFile;
        private System.Windows.Forms.Button buttonLoadStore;
        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.Label labelSubjectName;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.CheckBox checkBoxSsl;
        private System.Windows.Forms.Label lblClientCert;
        private System.Windows.Forms.TextBox textBoxClientCert;
        private System.Windows.Forms.Button btnViewClient;
        private System.Windows.Forms.Button btnLoadStoreClient;
        private System.Windows.Forms.Button btnLoadClient;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button btnAdvancedOptions;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
