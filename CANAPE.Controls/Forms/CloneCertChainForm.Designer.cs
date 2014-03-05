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
    partial class CloneCertChainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloneCertChainForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.btnShowProxyOptions = new System.Windows.Forms.Button();
            this.textBoxDest = new System.Windows.Forms.TextBox();
            this.labelShowAdvanced = new System.Windows.Forms.Label();
            this.proxyClientControl = new CANAPE.Controls.ProxyClientControl();
            this.panelProxy = new System.Windows.Forms.Panel();
            this.panelUrl = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelProxy.SuspendLayout();
            this.panelUrl.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(72, 7);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(372, 20);
            this.textBoxUrl.TabIndex = 6;
            this.textBoxUrl.Text = "https://host";
            this.toolTip.SetToolTip(this.textBoxUrl, "Specify connection to clone chain from, use tcp://host:port for raw TCP and https" +
        "://host:port for HTTPS");
            // 
            // btnShowProxyOptions
            // 
            this.btnShowProxyOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnShowProxyOptions.Image")));
            this.btnShowProxyOptions.Location = new System.Drawing.Point(134, 4);
            this.btnShowProxyOptions.Name = "btnShowProxyOptions";
            this.btnShowProxyOptions.Size = new System.Drawing.Size(25, 23);
            this.btnShowProxyOptions.TabIndex = 9;
            this.toolTip.SetToolTip(this.btnShowProxyOptions, "Click to show proxy configuration");
            this.btnShowProxyOptions.UseVisualStyleBackColor = true;
            this.btnShowProxyOptions.Click += new System.EventHandler(this.btnShowProxyOptions_Click);
            // 
            // textBoxDest
            // 
            this.textBoxDest.Location = new System.Drawing.Point(72, 33);
            this.textBoxDest.Name = "textBoxDest";
            this.textBoxDest.Size = new System.Drawing.Size(291, 20);
            this.textBoxDest.TabIndex = 8;
            this.toolTip.SetToolTip(this.textBoxDest, "Specify a directory to place output");
            // 
            // labelShowAdvanced
            // 
            this.labelShowAdvanced.AutoSize = true;
            this.labelShowAdvanced.Location = new System.Drawing.Point(3, 9);
            this.labelShowAdvanced.Name = "labelShowAdvanced";
            this.labelShowAdvanced.Size = new System.Drawing.Size(128, 13);
            this.labelShowAdvanced.TabIndex = 8;
            this.labelShowAdvanced.Text = "Show Advanced Options:";
            // 
            // proxyClientControl
            // 
            this.proxyClientControl.HideDefault = false;
            this.proxyClientControl.Location = new System.Drawing.Point(0, 33);
            this.proxyClientControl.Name = "proxyClientControl";
            this.proxyClientControl.Size = new System.Drawing.Size(276, 262);
            this.proxyClientControl.TabIndex = 7;
            this.proxyClientControl.Visible = false;
            // 
            // panelProxy
            // 
            this.panelProxy.AutoSize = true;
            this.panelProxy.Controls.Add(this.proxyClientControl);
            this.panelProxy.Controls.Add(this.btnShowProxyOptions);
            this.panelProxy.Controls.Add(this.labelShowAdvanced);
            this.panelProxy.Location = new System.Drawing.Point(3, 69);
            this.panelProxy.Name = "panelProxy";
            this.panelProxy.Size = new System.Drawing.Size(279, 298);
            this.panelProxy.TabIndex = 10;
            // 
            // panelUrl
            // 
            this.panelUrl.Controls.Add(this.btnBrowse);
            this.panelUrl.Controls.Add(this.textBoxDest);
            this.panelUrl.Controls.Add(this.label2);
            this.panelUrl.Controls.Add(this.textBoxUrl);
            this.panelUrl.Controls.Add(this.label1);
            this.panelUrl.Location = new System.Drawing.Point(3, 3);
            this.panelUrl.Name = "panelUrl";
            this.panelUrl.Size = new System.Drawing.Size(451, 60);
            this.panelUrl.TabIndex = 12;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(369, 31);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Destination:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "URL:";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.Controls.Add(this.panelUrl);
            this.flowLayoutPanel.Controls.Add(this.panelProxy);
            this.flowLayoutPanel.Controls.Add(this.panelButtons);
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(457, 413);
            this.flowLayoutPanel.TabIndex = 14;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnOK);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Location = new System.Drawing.Point(3, 373);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(449, 37);
            this.panelButtons.TabIndex = 14;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(134, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(230, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // CloneCertChainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(464, 402);
            this.Controls.Add(this.flowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloneCertChainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clone Certificate Chain";
            this.panelProxy.ResumeLayout(false);
            this.panelProxy.PerformLayout();
            this.panelUrl.ResumeLayout(false);
            this.panelUrl.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private Controls.ProxyClientControl proxyClientControl;
        private System.Windows.Forms.Label labelShowAdvanced;
        private System.Windows.Forms.Button btnShowProxyOptions;
        private System.Windows.Forms.Panel panelProxy;
        private System.Windows.Forms.Panel panelUrl;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textBoxDest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}