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
using System.IO;
using System.Text;
using System.Windows.Forms;
using CANAPE.Forms;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    public partial class EventLogControl : UserControl
    {        
        Logger _logger;

        public EventLogControl()
        {
            _logger = new CANAPE.Utils.Logger();
            
            InitializeComponent();
            _logger.LogEntryAdded += new EventHandler<Logger.LogEntryAddedEventArgs>(_logger_LogEntryAdded);
        }

        public Logger Logger { get { return _logger; } }

        public void AddLogEntry(Logger.EventLogEntry entry)
        {
            if ((entry != null) && (entry.Text != null))
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action<Logger.EventLogEntry>(AddLogEntry), entry);
                }
                else
                {
                    ListViewItem item = new ListViewItem(entry.Timestamp.ToString());
                    item.SubItems.Add(entry.EntryType.ToString());
                    item.SubItems.Add(entry.SourceName ?? "Unknown");
                    
                    string[] s = entry.Text.Split('\r', '\n');
                    if (s.Length > 0)
                    {
                        item.SubItems.Add(s[0]);
                    }
                    else
                    {
                        item.SubItems.Add(entry.Text);
                    }

                    if (entry.ExceptionObject != null)
                    {
                        item.ToolTipText = entry.ExceptionObject.ToString();
                    }

                    item.Tag = entry;
                    listViewLog.AddItem(item);
                }
            }
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewLog.Items.Count > 0)
            {
                if (!GlobalControlConfig.RequireEventLogClearConfirmation || 
                    MessageBox.Show(Properties.Resources.EventLogControl_ClearLog, 
                        Properties.Resources.EventLogControl_ClearLogCaption, 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    listViewLog.Items.Clear();
                }
            }
        }

        void _logger_LogEntryAdded(object sender, Logger.LogEntryAddedEventArgs e)
        {
            AddLogEntry(e.LogEntry);
        }

        private bool IsLogLevelSet(Logger.LogEntryType entryType)
        {
            return ((entryType & Logger.LogLevel) == entryType);
        }

        private void ToggleLogLevel(Logger.LogEntryType entryType)
        {
            if (IsLogLevelSet(entryType))
            {
                Logger.LogLevel &= ~entryType;
            }
            else
            {
                Logger.LogLevel |= entryType;
            }
        }

        private void verboseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleLogLevel(CANAPE.Utils.Logger.LogEntryType.Verbose);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleLogLevel(CANAPE.Utils.Logger.LogEntryType.Info);
        }

        private void warningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleLogLevel(CANAPE.Utils.Logger.LogEntryType.Warning);
        }

        private void errorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleLogLevel(CANAPE.Utils.Logger.LogEntryType.Error);
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            verboseToolStripMenuItem.Checked = IsLogLevelSet(CANAPE.Utils.Logger.LogEntryType.Verbose);
            infoToolStripMenuItem.Checked = IsLogLevelSet(CANAPE.Utils.Logger.LogEntryType.Info);
            warningToolStripMenuItem.Checked = IsLogLevelSet(CANAPE.Utils.Logger.LogEntryType.Warning);
            errorToolStripMenuItem.Checked = IsLogLevelSet(CANAPE.Utils.Logger.LogEntryType.Error);
            if (listViewLog.SelectedItems.Count > 0)
            {
                copyToolStripMenuItem.Enabled = true;
            }
            else
            {
                copyToolStripMenuItem.Enabled = false;
            }

            if (listViewLog.Items.Count > 0)
            {
                saveLogToolStripMenuItem.Enabled = true;
            }
            else
            {
                saveLogToolStripMenuItem.Enabled = false;
            }
        }

        private string ItemsToText(IEnumerable items)
        {
            StringBuilder builder = new StringBuilder();

            foreach (ListViewItem item in items)
            {
                Logger.EventLogEntry entry = item.Tag as Logger.EventLogEntry;
                
                builder.AppendFormat("{0} - {1} - {2}", entry.Timestamp, entry.EntryType, entry.SourceName);
                builder.AppendLine(entry.Text);
                builder.AppendLine();
                if (entry.ExceptionObject != null)
                {
                    builder.AppendLine(entry.ExceptionObject.ToString());
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewLog.SelectedItems.Count > 0)
            {
                Clipboard.SetText(ItemsToText(listViewLog.SelectedItems));
            }
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = Properties.Resources.EventLogControl_TextFilesFilter;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(dlg.FileName, ItemsToText(listViewLog.Items));
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString, 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void autoScrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewLog.AutoScrollList = autoScrollToolStripMenuItem.Checked;
        }

        private void listViewLog_DoubleClick(object sender, EventArgs e)
        {
            if (listViewLog.SelectedItems.Count > 0)
            {
                Logger.EventLogEntry entry = listViewLog.SelectedItems[0].Tag as Logger.EventLogEntry;

                if (entry != null)
                {
                    EventLogViewerForm frm = new EventLogViewerForm(entry);
                    frm.Show(this);
                }
            } 
        }
    }
}
