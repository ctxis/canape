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
    partial class EditConfigPropertyForm
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
            System.Windows.Forms.Label lblName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditConfigPropertyForm));
            System.Windows.Forms.Label labelType;
            System.Windows.Forms.Label lblDescription;
            System.Windows.Forms.Label lblDefaultValue;
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboBoxTypes = new System.Windows.Forms.ComboBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxStringValue = new System.Windows.Forms.TextBox();
            this.hexEditorControl = new CANAPE.Controls.HexEditorControl();
            lblName = new System.Windows.Forms.Label();
            labelType = new System.Windows.Forms.Label();
            lblDescription = new System.Windows.Forms.Label();
            lblDefaultValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            resources.ApplyResources(lblName, "lblName");
            lblName.Name = "lblName";
            // 
            // labelType
            // 
            resources.ApplyResources(labelType, "labelType");
            labelType.Name = "labelType";
            // 
            // lblDescription
            // 
            resources.ApplyResources(lblDescription, "lblDescription");
            lblDescription.Name = "lblDescription";
            // 
            // lblDefaultValue
            // 
            resources.ApplyResources(lblDefaultValue, "lblDefaultValue");
            lblDefaultValue.Name = "lblDefaultValue";
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboBoxTypes
            // 
            this.comboBoxTypes.DisplayMember = "Name";
            this.comboBoxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypes.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxTypes, "comboBoxTypes");
            this.comboBoxTypes.Name = "comboBoxTypes";
            this.comboBoxTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypes_SelectedIndexChanged);
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // textBoxStringValue
            // 
            this.textBoxStringValue.AcceptsReturn = true;
            this.textBoxStringValue.AcceptsTab = true;
            resources.ApplyResources(this.textBoxStringValue, "textBoxStringValue");
            this.textBoxStringValue.Name = "textBoxStringValue";
            // 
            // hexEditorControl
            // 
            this.hexEditorControl.HexColor = System.Drawing.Color.White;
            resources.ApplyResources(this.hexEditorControl, "hexEditorControl");
            this.hexEditorControl.Name = "hexEditorControl";
            this.hexEditorControl.ReadOnly = false;
            // 
            // EditConfigPropertyForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.hexEditorControl);
            this.Controls.Add(this.textBoxStringValue);
            this.Controls.Add(lblDefaultValue);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(lblDescription);
            this.Controls.Add(labelType);
            this.Controls.Add(this.comboBoxTypes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditConfigPropertyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EditConfigPropertyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox comboBoxTypes;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxStringValue;
        private Controls.HexEditorControl hexEditorControl;
    }
}