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
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Net;
using CANAPE.Forms;
using CANAPE.Net.Utils;
using CANAPE.Nodes;

namespace CANAPE.Controls
{
    public partial class ConnectionHistoryControl : UserControl
    {        
        NetServiceDocument _document;
        bool _autoScroll;

        public ConnectionHistoryControl()
        {
            
            InitializeComponent();     
        }

        private void UpdateHistory()
        {
            if (_document != null)
            {
                int count;

                lock (_document.History)
                {
                    count = _document.History.Count;
                }

                if (listViewHistory.VirtualListSize != count)
                {
                    listViewHistory.SuspendLayout();

                    if (count == 0)
                    {
                        listViewHistory.SelectedIndices.Clear();
                    }
                    else if (count < listViewHistory.VirtualListSize)
                    {
                        List<int> newIndicies = new List<int>();

                        // Only change selected indicies if the list has shrunk

                        for (int i = 0; i < listViewHistory.SelectedIndices.Count; ++i)
                        {
                            if (listViewHistory.SelectedIndices[i] < count)
                            {
                                newIndicies.Add(listViewHistory.SelectedIndices[i]);
                            }
                        }

                        listViewHistory.SelectedIndices.Clear();

                        foreach (int idx in newIndicies)
                        {
                            listViewHistory.SelectedIndices.Add(idx);
                        }
                    }

                    // Don't really want to do this, but for some reason it is a pain to debug what is actually
                    // going wrong, it is inside some non-obvious part of the the Windows.Forms listview class
                    try
                    {
                        listViewHistory.VirtualListSize = count;
                    }
                    catch (NullReferenceException ex)
                    {
                        System.Diagnostics.Trace.WriteLine(ex.ToString());
                    }

                    if (_autoScroll && (count > 0))
                    {
                        listViewHistory.EnsureVisible(count - 1);
                    }

                    listViewHistory.ResumeLayout();
                    listViewHistory.Invalidate();
                }
            }
        }

        public void SetDocument(NetServiceDocument document)
        {
            _document = document;
            
            UpdateHistory();
        }


        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewHistory.BeginUpdate();
            
            lock (_document.History)
            {
                _document.History.Clear();
            }
            UpdateHistory();

            listViewHistory.EndUpdate();
        }

        private ConnectionHistoryEntry GetSelectedEntry()
        {
            if ((_document != null) && listViewHistory.SelectedIndices.Count > 0)
            {
                int idx = listViewHistory.SelectedIndices[0];                

                lock (_document.History)
                {
                    if (idx < _document.History.Count)
                    {
                        return _document.History[idx];
                    }
                }
            }

            return null;
        }

        private void viewPacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            ConnectionHistoryEntry entry = GetSelectedEntry();
              
            if (entry != null)
            {
                LogPacket[] packets = _document.Packets.GetPacketsForNetwork(entry.NetId);
                if (packets.Length > 0)
                {
                    PacketLogViewerForm frm = new PacketLogViewerForm(packets[0], packets);

                    components.Add(frm);

                    frm.Show();
                }
                else
                {
                    MessageBox.Show(this, Properties.Resources.ConnectionHistoryControl_NoPackets, 
                        Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        private void listViewHistory_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            lock (_document.History)
            {
                if ((e.ItemIndex >= 0) && (e.ItemIndex < _document.History.Count))
                {
                    ConnectionHistoryEntry entry = _document.History[e.ItemIndex];

                    ListViewItem item = new ListViewItem(entry.StartTime.ToString());

                    item.SubItems.Add(entry.EndTime.Subtract(entry.StartTime).ToString());
                    item.SubItems.Add(entry.NetworkDescription);
                    item.Tag = entry;

                    e.Item = item;
                }
                else
                {
                    // Return a dummy item
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");                    
                    e.Item = item;
                }
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                listViewHistory.BeginUpdate();

                UpdateHistory();

                listViewHistory.EndUpdate();
            }
        }

        private void autoScrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _autoScroll = autoScrollToolStripMenuItem.Checked;
        }

        private void viewStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionHistoryEntry entry = GetSelectedEntry();

            if (entry != null)
            {
                LogPacket[] packets = _document.Packets.GetPacketsForNetwork(entry.NetId);

                ConnectionStatisticsForm frm = new ConnectionStatisticsForm(entry, packets); 

                frm.Show();                
            }
        }
    }
}
