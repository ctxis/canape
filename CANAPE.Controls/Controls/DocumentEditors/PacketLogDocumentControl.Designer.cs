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
    partial class PacketLogDocumentControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketLogDocumentControl));
            this.logPacketControl = new CANAPE.Controls.PacketLogControl();
            this.SuspendLayout();
            // 
            // logPacketControl
            // 
            resources.ApplyResources(this.logPacketControl, "logPacketControl");
            this.logPacketControl.IsInFindForm = false;
            this.logPacketControl.LogName = null;
            this.logPacketControl.Name = "logPacketControl";
            this.logPacketControl.ReadOnly = false;
            this.logPacketControl.ConfigChanged += new System.EventHandler(this.logPacketControl_ConfigChanged);
            // 
            // PacketLogDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logPacketControl);
            this.Name = "PacketLogDocumentControl";
            this.ResumeLayout(false);

        }

        #endregion

        private CANAPE.Controls.PacketLogControl logPacketControl;
    }
}