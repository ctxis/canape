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

namespace CANAPE.Documents.ComPort
{
    partial class SerialPortServerDocumentControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialPortServerDocumentControl));
            this.dataEndpointSelectionControl = new CANAPE.Controls.DataEndpointSelectionControl();
            this.serialPortConfigurationControl = new CANAPE.Documents.ComPort.SerialPortConfigurationControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.tabPageLayers = new System.Windows.Forms.TabPage();
            this.layerEditorControl = new CANAPE.Controls.LayerEditorControl();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.tabPageLayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataEndpointSelectionControl
            // 
            resources.ApplyResources(this.dataEndpointSelectionControl, "dataEndpointSelectionControl");
            this.dataEndpointSelectionControl.Name = "dataEndpointSelectionControl";
            this.dataEndpointSelectionControl.FactoryChanged += new System.EventHandler(this.dataEndpointSelectionControl_FactoryChanged);
            // 
            // serialPortConfigurationControl
            // 
            resources.ApplyResources(this.serialPortConfigurationControl, "serialPortConfigurationControl");
            this.serialPortConfigurationControl.Name = "serialPortConfigurationControl";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageLayers);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.serialPortConfigurationControl);
            this.tabPageConfig.Controls.Add(this.dataEndpointSelectionControl);
            resources.ApplyResources(this.tabPageConfig, "tabPageConfig");
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // tabPageLayers
            // 
            this.tabPageLayers.Controls.Add(this.layerEditorControl);
            resources.ApplyResources(this.tabPageLayers, "tabPageLayers");
            this.tabPageLayers.Name = "tabPageLayers";
            this.tabPageLayers.UseVisualStyleBackColor = true;
            // 
            // layerEditorControl
            // 
            this.layerEditorControl.Binding = CANAPE.Net.Layers.NetworkLayerBinding.Default;
            resources.ApplyResources(this.layerEditorControl, "layerEditorControl");
            this.layerEditorControl.Name = "layerEditorControl";
            this.layerEditorControl.LayersUpdated += new System.EventHandler(this.layerEditorControl_LayersUpdated);
            // 
            // SerialPortServerDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "SerialPortServerDocumentControl";
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageLayers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SerialPortConfigurationControl serialPortConfigurationControl;
        private Controls.DataEndpointSelectionControl dataEndpointSelectionControl;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageLayers;
        private Controls.LayerEditorControl layerEditorControl;
    }
}
