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
    partial class ConnectionStatisticsForm
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
            System.Windows.Forms.ColumnHeader columnHeaderName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionStatisticsForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageProperties = new System.Windows.Forms.TabPage();
            this.listViewProperties = new System.Windows.Forms.ListView();
            this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageTagStatistics = new System.Windows.Forms.TabPage();
            this.tagStatisticsChartControl = new CANAPE.Controls.PacketCharts.TagStatisticsChartControl();
            this.tabPageHistogram = new System.Windows.Forms.TabPage();
            this.histogramChartControl = new CANAPE.Controls.PacketCharts.HistogramChartControl();
            this.tabPageNetworkTraffic = new System.Windows.Forms.TabPage();
            this.networkTrafficChartControl = new CANAPE.Controls.PacketCharts.NetworkTrafficChartControl();
            this.tabPagePackets = new System.Windows.Forms.TabPage();
            this.packetLogControl = new CANAPE.Controls.PacketLogControl();
            columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl.SuspendLayout();
            this.tabPageProperties.SuspendLayout();
            this.tabPageTagStatistics.SuspendLayout();
            this.tabPageHistogram.SuspendLayout();
            this.tabPageNetworkTraffic.SuspendLayout();
            this.tabPagePackets.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeaderName
            // 
            columnHeaderName.Text = "Name";
            columnHeaderName.Width = 138;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageProperties);
            this.tabControl.Controls.Add(this.tabPageTagStatistics);
            this.tabControl.Controls.Add(this.tabPageHistogram);
            this.tabControl.Controls.Add(this.tabPageNetworkTraffic);
            this.tabControl.Controls.Add(this.tabPagePackets);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(843, 519);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageProperties
            // 
            this.tabPageProperties.Controls.Add(this.listViewProperties);
            this.tabPageProperties.Location = new System.Drawing.Point(4, 22);
            this.tabPageProperties.Name = "tabPageProperties";
            this.tabPageProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProperties.Size = new System.Drawing.Size(835, 493);
            this.tabPageProperties.TabIndex = 0;
            this.tabPageProperties.Text = "Properties";
            this.tabPageProperties.UseVisualStyleBackColor = true;
            // 
            // listViewProperties
            // 
            this.listViewProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderName,
            this.columnHeaderValue});
            this.listViewProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProperties.FullRowSelect = true;
            this.listViewProperties.Location = new System.Drawing.Point(3, 3);
            this.listViewProperties.Name = "listViewProperties";
            this.listViewProperties.Size = new System.Drawing.Size(829, 487);
            this.listViewProperties.TabIndex = 0;
            this.listViewProperties.UseCompatibleStateImageBehavior = false;
            this.listViewProperties.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Text = "Value";
            this.columnHeaderValue.Width = 308;
            // 
            // tabPageTagStatistics
            // 
            this.tabPageTagStatistics.Controls.Add(this.tagStatisticsChartControl);
            this.tabPageTagStatistics.Location = new System.Drawing.Point(4, 22);
            this.tabPageTagStatistics.Name = "tabPageTagStatistics";
            this.tabPageTagStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTagStatistics.Size = new System.Drawing.Size(835, 493);
            this.tabPageTagStatistics.TabIndex = 2;
            this.tabPageTagStatistics.Text = "Tag Statistics";
            this.tabPageTagStatistics.UseVisualStyleBackColor = true;
            // 
            // tagStatisticsChartControl
            // 
            this.tagStatisticsChartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagStatisticsChartControl.Location = new System.Drawing.Point(3, 3);
            this.tagStatisticsChartControl.Name = "tagStatisticsChartControl";
            this.tagStatisticsChartControl.Size = new System.Drawing.Size(829, 487);
            this.tagStatisticsChartControl.TabIndex = 0;
            // 
            // tabPageHistogram
            // 
            this.tabPageHistogram.Controls.Add(this.histogramChartControl);
            this.tabPageHistogram.Location = new System.Drawing.Point(4, 22);
            this.tabPageHistogram.Name = "tabPageHistogram";
            this.tabPageHistogram.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHistogram.Size = new System.Drawing.Size(835, 493);
            this.tabPageHistogram.TabIndex = 3;
            this.tabPageHistogram.Text = "Histogram";
            this.tabPageHistogram.UseVisualStyleBackColor = true;
            // 
            // histogramChartControl
            // 
            this.histogramChartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.histogramChartControl.Location = new System.Drawing.Point(3, 3);
            this.histogramChartControl.Name = "histogramChartControl";
            this.histogramChartControl.Size = new System.Drawing.Size(829, 487);
            this.histogramChartControl.TabIndex = 0;
            // 
            // tabPageNetworkTraffic
            // 
            this.tabPageNetworkTraffic.Controls.Add(this.networkTrafficChartControl);
            this.tabPageNetworkTraffic.Location = new System.Drawing.Point(4, 22);
            this.tabPageNetworkTraffic.Name = "tabPageNetworkTraffic";
            this.tabPageNetworkTraffic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNetworkTraffic.Size = new System.Drawing.Size(835, 493);
            this.tabPageNetworkTraffic.TabIndex = 5;
            this.tabPageNetworkTraffic.Text = "Network Traffic";
            this.tabPageNetworkTraffic.UseVisualStyleBackColor = true;
            // 
            // networkTrafficChartControl
            // 
            this.networkTrafficChartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.networkTrafficChartControl.Location = new System.Drawing.Point(3, 3);
            this.networkTrafficChartControl.Name = "networkTrafficChartControl";
            this.networkTrafficChartControl.Size = new System.Drawing.Size(829, 487);
            this.networkTrafficChartControl.TabIndex = 0;
            // 
            // tabPagePackets
            // 
            this.tabPagePackets.Controls.Add(this.packetLogControl);
            this.tabPagePackets.Location = new System.Drawing.Point(4, 22);
            this.tabPagePackets.Name = "tabPagePackets";
            this.tabPagePackets.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePackets.Size = new System.Drawing.Size(835, 493);
            this.tabPagePackets.TabIndex = 4;
            this.tabPagePackets.Text = "Packet Log";
            this.tabPagePackets.UseVisualStyleBackColor = true;
            // 
            // packetLogControl
            // 
            this.packetLogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetLogControl.IsInFindForm = false;
            this.packetLogControl.Location = new System.Drawing.Point(3, 3);
            this.packetLogControl.LogName = null;
            this.packetLogControl.Name = "packetLogControl";
            this.packetLogControl.ReadOnly = false;
            this.packetLogControl.Size = new System.Drawing.Size(829, 487);
            this.packetLogControl.TabIndex = 0;
            // 
            // ConnectionStatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 519);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionStatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection Statistics";
            this.tabControl.ResumeLayout(false);
            this.tabPageProperties.ResumeLayout(false);
            this.tabPageTagStatistics.ResumeLayout(false);
            this.tabPageHistogram.ResumeLayout(false);
            this.tabPageNetworkTraffic.ResumeLayout(false);
            this.tabPagePackets.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageProperties;
        private System.Windows.Forms.ListView listViewProperties;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
        private System.Windows.Forms.TabPage tabPageTagStatistics;
        private System.Windows.Forms.TabPage tabPageHistogram;
        private Controls.PacketCharts.HistogramChartControl histogramChartControl;
        private System.Windows.Forms.TabPage tabPagePackets;
        private Controls.PacketLogControl packetLogControl;
        private Controls.PacketCharts.TagStatisticsChartControl tagStatisticsChartControl;
        private System.Windows.Forms.TabPage tabPageNetworkTraffic;
        private Controls.PacketCharts.NetworkTrafficChartControl networkTrafficChartControl;
    }
}