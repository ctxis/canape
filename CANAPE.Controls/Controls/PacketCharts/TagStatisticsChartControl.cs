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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Controls.PacketCharts
{
    public partial class TagStatisticsChartControl : UserControl, IPacketLogViewer
    {
        Dictionary<string, List<LogPacket>> _bytag;

        public TagStatisticsChartControl()
        {
            InitializeComponent();
            _bytag = new Dictionary<string, List<LogPacket>>();
        }

        private void comboBoxTagStatisticsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            chartTagDetails.SuspendLayout();
            string[] keys;
            long[] values;
            string tooltip;
            Color[] colors;

            if (comboBoxTagStatisticsType.SelectedIndex == 0)
            {
                keys = _bytag.Select(p => p.Key).ToArray();
                values = _bytag.Select(p => p.Value.LongCount()).ToArray();
                colors = _bytag.Select(p => ColorValueConverter.ToColor(p.Value.First().Color)).ToArray();
                tooltip = Properties.Resources.TagStatisticsChartControl_CountToolTip;
            }
            else
            {
                keys = _bytag.Select(p => p.Key).ToArray();
                values = _bytag.Select(p => p.Value.Sum(x => x.Length)).ToArray();
                colors = _bytag.Select(p => ColorValueConverter.ToColor(p.Value.First().Color)).ToArray();
                tooltip = Properties.Resources.TagStatisticsChartControl_ByteCountToolTip;
            }

            chartTagDetails.Series["Tags"].Points.DataBindXY(keys, values);
            chartTagDetails.Series["Tags"].ToolTip = tooltip;
            chartTagDetails.Series["Tags"].IsValueShownAsLabel = true;

            for(int i = 0; i < chartTagDetails.Series["Tags"].Points.Count; ++i)
            {
                chartTagDetails.Series["Tags"].Points[i].Color = colors[i];
            }

            chartTagDetails.ResumeLayout();
        }

        public void SetPackets(Nodes.LogPacket[] packets)
        {
            if (packets.Length > 0)
            {
                _bytag = packets.GroupBy(p => String.IsNullOrWhiteSpace(p.Tag) ? "Unknown Tag" : p.Tag).ToDictionary(g => g.Key, g => g.ToList());
            }
            else
            {
                _bytag = new Dictionary<string, List<LogPacket>>();
            }

            comboBoxTagStatisticsType.SelectedIndex = 0;            
        }

        public void SetPackets(Documents.Net.LogPacketCollection packets)
        {
            lock (packets)
            {
                SetPackets(packets.ToArray());
            }
        }
    }
}
