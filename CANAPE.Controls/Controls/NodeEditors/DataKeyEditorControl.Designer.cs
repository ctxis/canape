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

namespace CANAPE.Controls.NodeEditors
{
    partial class DataKeyEditorControl
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
            System.Windows.Forms.ColumnHeader columnHeaderName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataKeyEditorControl));
            System.Windows.Forms.ColumnHeader columnHeaderType;
            System.Windows.Forms.ColumnHeader columnHeaderValue;
            this.listViewValues = new CANAPE.Controls.ListViewExtension();
            columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderType
            // 
            resources.ApplyResources(columnHeaderType, "columnHeaderType");
            // 
            // columnHeaderValue
            // 
            resources.ApplyResources(columnHeaderValue, "columnHeaderValue");
            // 
            // listViewValues
            // 
            resources.ApplyResources(this.listViewValues, "listViewValues");
            this.listViewValues.AutoScrollList = false;
            this.listViewValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderName,
            columnHeaderType,
            columnHeaderValue});
            this.listViewValues.FullRowSelect = true;
            this.listViewValues.GridLines = true;
            this.listViewValues.LabelEdit = true;
            this.listViewValues.Name = "listViewValues";
            this.listViewValues.UseCompatibleStateImageBehavior = false;
            this.listViewValues.View = System.Windows.Forms.View.Details;
            this.listViewValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewValues_MouseDoubleClick);
            // 
            // DataKeyEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewValues);
            this.Name = "DataKeyEditorControl";
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewExtension listViewValues;
    }
}
