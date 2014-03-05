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

namespace CANAPE.Controls.PacketCharts
{
    partial class NetworkTrafficChartControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartNetworkTraffic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartNetworkTraffic)).BeginInit();
            this.SuspendLayout();
            // 
            // chartNetworkTraffic
            // 
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.Title = "Time (s)";
            chartArea1.AxisY.Title = "Byte Count";
            chartArea1.Name = "ChartArea";
            this.chartNetworkTraffic.ChartAreas.Add(chartArea1);
            this.chartNetworkTraffic.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartNetworkTraffic.Legends.Add(legend1);
            this.chartNetworkTraffic.Location = new System.Drawing.Point(0, 0);
            this.chartNetworkTraffic.Name = "chartNetworkTraffic";
            series1.ChartArea = "ChartArea";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartNetworkTraffic.Series.Add(series1);
            this.chartNetworkTraffic.Size = new System.Drawing.Size(847, 495);
            this.chartNetworkTraffic.TabIndex = 0;
            this.chartNetworkTraffic.Text = "chart";
            // 
            // NetworkTrafficChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartNetworkTraffic);
            this.Name = "NetworkTrafficChartControl";
            this.Size = new System.Drawing.Size(847, 495);
            ((System.ComponentModel.ISupportInitialize)(this.chartNetworkTraffic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartNetworkTraffic;
    }
}
