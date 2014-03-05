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
    partial class HistogramChartControl
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
            System.Windows.Forms.Label lblTag;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistogramChartControl));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.ColumnHeader columnHeaderByteValue;
            System.Windows.Forms.ColumnHeader columnHeaderCount;
            System.Windows.Forms.ColumnHeader columnHeaderCharacter;
            this.chartHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBoxTag = new System.Windows.Forms.ComboBox();
            this.radioButtonAsChart = new System.Windows.Forms.RadioButton();
            this.radioButtonAsList = new System.Windows.Forms.RadioButton();
            this.listViewByteCounts = new System.Windows.Forms.ListView();
            lblTag = new System.Windows.Forms.Label();
            columnHeaderByteValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderCharacter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTag
            // 
            resources.ApplyResources(lblTag, "lblTag");
            lblTag.Name = "lblTag";
            // 
            // chartHistogram
            // 
            resources.ApplyResources(this.chartHistogram, "chartHistogram");
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.Maximum = 255D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "Byte Value";
            chartArea1.AxisY.Title = "Weight";
            chartArea1.Name = "ChartArea";
            this.chartHistogram.ChartAreas.Add(chartArea1);
            this.chartHistogram.Name = "chartHistogram";
            this.chartHistogram.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartHistogram.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black};
            series1.ChartArea = "ChartArea";
            series1.CustomProperties = "PointWidth=1";
            series1.Name = "Bytes";
            this.chartHistogram.Series.Add(series1);
            // 
            // comboBoxTag
            // 
            this.comboBoxTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTag.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxTag, "comboBoxTag");
            this.comboBoxTag.Name = "comboBoxTag";
            this.comboBoxTag.SelectedIndexChanged += new System.EventHandler(this.comboBoxTag_SelectedIndexChanged);
            // 
            // radioButtonAsChart
            // 
            resources.ApplyResources(this.radioButtonAsChart, "radioButtonAsChart");
            this.radioButtonAsChart.Checked = true;
            this.radioButtonAsChart.Name = "radioButtonAsChart";
            this.radioButtonAsChart.TabStop = true;
            this.radioButtonAsChart.UseVisualStyleBackColor = true;
            this.radioButtonAsChart.CheckedChanged += new System.EventHandler(this.radioButtonAsChart_CheckedChanged);
            // 
            // radioButtonAsList
            // 
            resources.ApplyResources(this.radioButtonAsList, "radioButtonAsList");
            this.radioButtonAsList.Name = "radioButtonAsList";
            this.radioButtonAsList.UseVisualStyleBackColor = true;
            // 
            // listViewByteCounts
            // 
            resources.ApplyResources(this.listViewByteCounts, "listViewByteCounts");
            this.listViewByteCounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderByteValue,
            columnHeaderCharacter,
            columnHeaderCount});
            this.listViewByteCounts.FullRowSelect = true;
            this.listViewByteCounts.Name = "listViewByteCounts";
            this.listViewByteCounts.UseCompatibleStateImageBehavior = false;
            this.listViewByteCounts.View = System.Windows.Forms.View.Details;
            this.listViewByteCounts.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewByteCounts_ColumnClick);
            // 
            // columnHeaderByteValue
            // 
            resources.ApplyResources(columnHeaderByteValue, "columnHeaderByteValue");
            // 
            // columnHeaderCount
            // 
            resources.ApplyResources(columnHeaderCount, "columnHeaderCount");
            // 
            // columnHeaderCharacter
            // 
            resources.ApplyResources(columnHeaderCharacter, "columnHeaderCharacter");
            // 
            // HistogramChartControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewByteCounts);
            this.Controls.Add(this.radioButtonAsList);
            this.Controls.Add(this.radioButtonAsChart);
            this.Controls.Add(this.comboBoxTag);
            this.Controls.Add(lblTag);
            this.Controls.Add(this.chartHistogram);
            this.Name = "HistogramChartControl";
            ((System.ComponentModel.ISupportInitialize)(this.chartHistogram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartHistogram;
        private System.Windows.Forms.ComboBox comboBoxTag;
        private System.Windows.Forms.RadioButton radioButtonAsChart;
        private System.Windows.Forms.RadioButton radioButtonAsList;
        private System.Windows.Forms.ListView listViewByteCounts;
    }
}
