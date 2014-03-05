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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CANAPE.Documents.Net;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Controls.PacketCharts
{
    public partial class HistogramChartControl : UserControl, IPacketLogViewer
    {
        Dictionary<string, List<LogPacket>> _bytag;
        LogPacket[] _packets;
        int _lastCol;
        bool _ascSort;

        public HistogramChartControl()
        {
            InitializeComponent();
        }

        private void UpdateSeries(string tag, IEnumerable<LogPacket> packets)
        {
            Series series = new Series();
            long totalBytes = 0;
            double[] values = new double[256];
            int count = 0;

            chartHistogram.Series.Clear();
            
            series.ChartArea = "ChartArea";
            series.CustomProperties = "PointWidth=1";
            series.Name = tag;
            series.Color = Color.Black;

            foreach (LogPacket packet in packets)
            {
                byte[] data = packet.Frame.ToArray();

                foreach (byte b in data)
                {
                    values[b] += 1.0f;
                }

                count++;
                totalBytes += data.Length;
            }

            for (int i = 0; i < 256; ++i)
            {
                double value = values[i] / (double)totalBytes;
                int idx = series.Points.AddXY(i, value);

                series.Points[i].ToolTip = String.Format("{0}/0x{0:X02} {1}", i, value);

                ListViewItem item = listViewByteCounts.Items.Add(String.Format("0x{0:X02}", i));
                item.SubItems.Add(((char)i).ToString());
                item.SubItems.Add(((long)values[i]).ToString());

                Tuple<int, long> t = new Tuple<int, long>(i, (long)values[i]);
                item.Tag = t;
            }

            _lastCol = -1;
            _ascSort = false;

            chartHistogram.Titles.Clear();
            chartHistogram.Titles.Add("Main").Text =
                    String.Format(CANAPE.Properties.Resources.HistogramChartControl_Title,
                    count, totalBytes);            

            chartHistogram.Series.Add(series);
        }

        public void SetPackets(LogPacket[] packets)
        {
            _packets = packets;

            if (packets.Length > 0)
            {
                _bytag = packets.GroupBy(p => String.IsNullOrWhiteSpace(p.Tag) ? "Unknown Tag" : p.Tag).ToDictionary(g => g.Key, g => g.ToList());
                comboBoxTag.Items.Clear();

                comboBoxTag.Items.Add(Properties.Resources.HistogramChartControl_AllTags);
                foreach (KeyValuePair<string, List<LogPacket>> pair in _bytag)
                {
                    comboBoxTag.Items.Add(pair.Key);
                }
                comboBoxTag.SelectedIndex = 0;
            }
            else
            {
                _bytag = new Dictionary<string, List<LogPacket>>();                
            }
        }


        public void SetPackets(LogPacketCollection packets)
        {
            lock (packets)
            {
                SetPackets(packets.ToArray());
            }
        }

        private void comboBoxTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTag.SelectedIndex == 0)
            {
                UpdateSeries(Properties.Resources.HistogramChartControl_AllTags, _packets);
            }
            else
            {
                string key = comboBoxTag.SelectedItem.ToString();

                if (_bytag.ContainsKey(key))
                {
                    UpdateSeries(key, _bytag[key]);
                }
            }
        }

        private void radioButtonAsChart_CheckedChanged(object sender, EventArgs e)
        {
            chartHistogram.Visible = radioButtonAsChart.Checked;
            listViewByteCounts.Visible = !radioButtonAsChart.Checked;
        }

        class ListComparer : IComparer
        {
            private bool _asc;
            private int _col;

            public ListComparer(int col, bool asc)
            {
                _asc = asc;
                _col = col;
            }

            public int Compare(object x, object y)
            {
                ListViewItem xitem = x as ListViewItem;
                ListViewItem yitem = y as ListViewItem;

                Tuple<int, long> xt = (Tuple<int, long>)xitem.Tag;
                Tuple<int, long> yt = (Tuple<int, long>)yitem.Tag;

                long vx;
                long vy;

                if ((_col == 0) || (_col == 1))
                {
                    vx = xt.Item1;
                    vy = yt.Item1;
                }
                else
                {
                    vx = xt.Item2;
                    vy = yt.Item2;
                }

                if (_asc)
                {
                    return vx.CompareTo(vy);
                }
                else
                {
                    return vy.CompareTo(vx);
                }
            }
        }

        private void listViewByteCounts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _lastCol)
            {
                _ascSort = !_ascSort;
            }
            else
            {
                _ascSort = false;
            }

            _lastCol = e.Column;

            listViewByteCounts.ListViewItemSorter = new ListComparer(_lastCol, _ascSort);
            listViewByteCounts.Sort();
        }
    }
}
