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
using System.Windows.Forms.DataVisualization.Charting;
using CANAPE.Documents.Net;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Controls.PacketCharts
{
    public partial class NetworkTrafficChartControl : UserControl, IPacketLogViewer
    {
        private const double MIN_TIME_DIVISION = 0.1f;
        private const int MAX_BUCKETS = 100;

        LogPacket[] _packets;
        Dictionary<string, List<LogPacket>> _bytag;
        Dictionary<string, long>[] _bucketsByteCount;
        DateTime _startTime;
        DateTime _endTime;
        double _bucketLength;

        private Series CreateSeries(string tag, Color color)
        {
            Series series = new Series();
            series.Name = tag;
            series.ChartType = SeriesChartType.StackedArea;
            series.ChartArea = "ChartArea";
            series.ToolTip = tag + " byte count at #VALX = #VALY";

            series.Points.AddXY(0.0, 0.0);

            return series;
        }

        private void UpdateChart()
        {
            Dictionary<string, Series> series = new Dictionary<string, Series>();

            chartNetworkTraffic.Series.Clear();

            int point = 1;

            foreach (KeyValuePair<string, List<LogPacket>> pair in _bytag)
            {
                series.Add(pair.Key, CreateSeries(pair.Key, pair.Value[0].Color.ToColor()));
                
            }

            foreach (Dictionary<string, long> bucket in _bucketsByteCount)
            {
                foreach (KeyValuePair<string, Series> pair in series)
                {
                    if (bucket.ContainsKey(pair.Key))
                    {
                        pair.Value.Points.AddXY((double)point * _bucketLength, bucket[pair.Key]);
                    }
                    else
                    {
                        pair.Value.Points.AddXY((double)point * _bucketLength, 0);
                    }
                }

                point++;
            }

            foreach (Series s in series.Values)
            {
                s.Points.AddXY((double)point * _bucketLength, 0);
                chartNetworkTraffic.Series.Add(s);
            }
        }

        public NetworkTrafficChartControl()
        {
            InitializeComponent();
        }

        private void listViewTags_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateChart();
        }

        public void SetPackets(LogPacket[] packets)
        {
            _packets = packets;

            if (packets.Length > 0)
            {
                _bytag = packets.GroupBy(p => String.IsNullOrWhiteSpace(p.Tag) ? "Unknown Tag" : p.Tag).ToDictionary(g => g.Key, g => g.ToList());

                _startTime = DateTime.MaxValue;
                _endTime = DateTime.MinValue;

                foreach (LogPacket packet in _packets)
                {
                    if (packet.Timestamp < _startTime)
                    {
                        _startTime = packet.Timestamp;
                    }

                    if (packet.Timestamp > _endTime)
                    {
                        _endTime = packet.Timestamp;
                    }
                }      
          
                double secs = _endTime.Subtract(_startTime).TotalSeconds;
                double bucketLength = secs / (double)MAX_BUCKETS;
                int bucketCount = MAX_BUCKETS+1;

                if(bucketLength < MIN_TIME_DIVISION)
                {
                    bucketCount = (int)Math.Ceiling(secs / MIN_TIME_DIVISION) + 1;
                    bucketLength = MIN_TIME_DIVISION;
                }

                _bucketsByteCount = new Dictionary<string, long>[bucketCount];
                _bucketLength = bucketLength;

                for (int i = 0; i < bucketCount; ++i)
                {
                    _bucketsByteCount[i] = new Dictionary<string, long>();
                }

                foreach (LogPacket packet in _packets)
                {
                    TimeSpan currSpan = packet.Timestamp.Subtract(_startTime);

                    int bucketNumber = (int)Math.Truncate(currSpan.TotalSeconds / bucketLength);
                    string tag = packet.Tag ?? "Unknown Tag";

                    Dictionary<string, long> bucket = _bucketsByteCount[bucketNumber];

                    if (!bucket.ContainsKey(tag))
                    {
                        bucket[tag] = 0;
                    }

                    bucket[tag] = bucket[tag] + packet.Length;
                }
            }
            else
            {
                _bytag = new Dictionary<string, List<LogPacket>>();
            }

            UpdateChart();
        }


        public void SetPackets(LogPacketCollection packets)
        {
            lock (packets)
            {
                SetPackets(packets.ToArray());
            }
        }
    }
}
