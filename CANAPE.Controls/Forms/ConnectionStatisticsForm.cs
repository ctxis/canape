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

using System.Collections.Generic;
using System.Windows.Forms;
using CANAPE.Documents.Net;
using CANAPE.Net.Utils;
using CANAPE.Nodes;

namespace CANAPE.Forms
{
    public partial class ConnectionStatisticsForm : Form
    {                
        public ConnectionStatisticsForm(ConnectionHistoryEntry entry, LogPacket[] packets)
        {
            InitializeComponent();

            if (entry != null)
            {
                foreach (KeyValuePair<string, object> pair in entry.Properties)
                {
                    ListViewItem item = listViewProperties.Items.Add(pair.Key);
                    item.SubItems.Add(pair.Value.ToString());
                    item.Tag = pair.Value;
                }
            }
            else
            {
                tabControl.TabPages.Remove(tabPageProperties);
            }

            if (packets != null && packets.Length > 0)
            {
                packetLogControl.SetPackets(packets);
                tagStatisticsChartControl.SetPackets(packets);
                histogramChartControl.SetPackets(packets);
                networkTrafficChartControl.SetPackets(packets);
            }
            else
            {
                tabControl.TabPages.Remove(tabPageHistogram);
                tabControl.TabPages.Remove(tabPagePackets);
                tabControl.TabPages.Remove(tabPageTagStatistics);
                tabControl.TabPages.Remove(tabPageNetworkTraffic);                    
            }
        }
    }
}
