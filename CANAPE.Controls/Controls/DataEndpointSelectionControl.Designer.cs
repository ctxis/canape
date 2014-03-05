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
    partial class DataEndpointSelectionControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataEndpointSelectionControl));
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.btnSelectLibrary = new System.Windows.Forms.Button();
            this.textBoxLibraryServer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // btnSelectLibrary
            // 
            resources.ApplyResources(this.btnSelectLibrary, "btnSelectLibrary");
            this.btnSelectLibrary.Name = "btnSelectLibrary";
            this.btnSelectLibrary.UseVisualStyleBackColor = true;
            this.btnSelectLibrary.Click += new System.EventHandler(this.btnSelectLibrary_Click);
            // 
            // textBoxLibraryServer
            // 
            resources.ApplyResources(this.textBoxLibraryServer, "textBoxLibraryServer");
            this.textBoxLibraryServer.Name = "textBoxLibraryServer";
            this.textBoxLibraryServer.ReadOnly = true;
            // 
            // DataEndpointSelectionControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.btnSelectLibrary);
            this.Controls.Add(this.textBoxLibraryServer);
            this.Name = "DataEndpointSelectionControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button btnSelectLibrary;
        private System.Windows.Forms.TextBox textBoxLibraryServer;

    }
}
