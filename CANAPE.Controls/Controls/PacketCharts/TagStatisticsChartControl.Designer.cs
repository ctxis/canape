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
    partial class TagStatisticsChartControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagStatisticsChartControl));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.Label lblType;
            this.comboBoxTagStatisticsType = new System.Windows.Forms.ComboBox();
            this.chartTagDetails = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lblType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartTagDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxTagStatisticsType
            // 
            this.comboBoxTagStatisticsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTagStatisticsType.FormattingEnabled = true;
            this.comboBoxTagStatisticsType.Items.AddRange(new object[] {
            resources.GetString("comboBoxTagStatisticsType.Items"),
            resources.GetString("comboBoxTagStatisticsType.Items1")});
            resources.ApplyResources(this.comboBoxTagStatisticsType, "comboBoxTagStatisticsType");
            this.comboBoxTagStatisticsType.Name = "comboBoxTagStatisticsType";
            this.comboBoxTagStatisticsType.SelectedIndexChanged += new System.EventHandler(this.comboBoxTagStatisticsType_SelectedIndexChanged);
            // 
            // chartTagDetails
            // 
            resources.ApplyResources(this.chartTagDetails, "chartTagDetails");
            this.chartTagDetails.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartAreaTags";
            this.chartTagDetails.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartTagDetails.Legends.Add(legend1);
            this.chartTagDetails.Name = "chartTagDetails";
            series1.ChartArea = "ChartAreaTags";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Tags";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chartTagDetails.Series.Add(series1);
            // 
            // lblType
            // 
            resources.ApplyResources(lblType, "lblType");
            lblType.Name = "lblType";
            // 
            // TagStatisticsChartControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(lblType);
            this.Controls.Add(this.chartTagDetails);
            this.Controls.Add(this.comboBoxTagStatisticsType);
            this.Name = "TagStatisticsChartControl";
            ((System.ComponentModel.ISupportInitialize)(this.chartTagDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTagStatisticsType;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTagDetails;
    }
}
