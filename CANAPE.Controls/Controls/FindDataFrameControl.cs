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
using System.ComponentModel;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.DataFrames.Filters;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Utils;
using System.Linq;

namespace CANAPE.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FindDataFrameControl : UserControl
    {
        private class FindArguments
        {
            public LogPacket[] packetLog;
            public IDataFrameFilter filter;
        }

        private IList<LogPacket> _packetLog;

        enum FilterMode
        {
            String,
            Binary,
            Regex,
        }

        /// <summary>
        /// 
        /// </summary>
        public FindDataFrameControl()
        {
            
            InitializeComponent();
           
            foreach (object en in Enum.GetValues(typeof(FilterMode)))
            {
                comboBoxMode.Items.Add(en);
            }

            comboBoxMode.SelectedIndex = 0;

            foreach (object en in Enum.GetValues(typeof(StringDataFrameFilterFactory.FilterStringEncoding)))
            {
                comboBoxEncoding.Items.Add(en);
            }

            comboBoxEncoding.SelectedIndex = 0;
        }

        public void SetupControl(IList<LogPacket> packetLog, bool readOnly, PacketLogControl parentLog)
        {
            _packetLog = packetLog;
            packetLogControl.ParentLog = parentLog;
            packetLogControl.ReadOnly = readOnly;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            FindArguments args = (FindArguments)e.Argument;
            int length = args.packetLog.Length;

            for (int i = 0; i < length; ++i)
            {
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    int percent = (i * 100) / length;
                    if (args.filter.IsMatch(args.packetLog[i].Frame))
                    {
                        backgroundWorker.ReportProgress(percent, args.packetLog[i]);
                    }
                    else
                    {
                        backgroundWorker.ReportProgress(percent);
                    }
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                packetLogControl.AddLogEntry((LogPacket)e.UserState);
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnFind.Text = CANAPE.Properties.Resources.FindPacketForm_FindButtonText;
            progressBar.Value = 100;
        }

        private IDataFrameFilter CreateFilter()
        {
            SetDataFrameFilterFactory factory;

            FilterMode mode = (FilterMode)comboBoxMode.SelectedItem;

            if (mode == FilterMode.Binary)
            {
                
                factory = new BinaryDataFrameFilterFactory() 
                            { Match = GeneralUtils.HexToBinary(textBoxValue.Text) };
            }
            else if(mode == FilterMode.Regex)
            {
                factory = new RegexDataFrameFilterFactory()
                {
                    CaseSensitive = checkBoxCaseSensitive.Checked,
                    Match = textBoxValue.Text,
                    StringEncoding = (StringDataFrameFilterFactory.FilterStringEncoding)comboBoxEncoding.SelectedItem
                };
            }
            else if(mode == FilterMode.String)
            {
                factory = new StringDataFrameFilterFactory()
                {
                    CaseSensitive = checkBoxCaseSensitive.Checked,
                    Match = textBoxValue.Text,
                    StringEncoding = (StringDataFrameFilterFactory.FilterStringEncoding)comboBoxEncoding.SelectedItem
                };
            }
            else
            {
                throw new ArgumentException(CANAPE.Properties.Resources.FindPacketForm_SelectMode);
            }

            factory.Path = textBoxPath.Text;
            factory.SearchMode = DataFrameFilterSearchMode.Contains;
            
            return factory.CreateFilter();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
            else
            {
                if ((comboBoxEncoding.SelectedItem == null) || (comboBoxMode.SelectedItem == null))
                {
                    MessageBox.Show(this, CANAPE.Properties.Resources.FindPacketForm_SelectModeEncoding,
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (textBoxValue.Text.Length == 0)
                {
                    MessageBox.Show(CANAPE.Properties.Resources.FindPacketForm_SpecifySearch, 
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        // Start find
                        FindArguments args = new FindArguments();
                        packetLogControl.ClearLog();

                        lock (_packetLog)
                        {
                            args.packetLog = _packetLog.ToArray();
                        }

                        args.filter = CreateFilter();
                        btnFind.Text = CANAPE.Properties.Resources.FindPacketForm_CancelButtonText;
                        backgroundWorker.RunWorkerAsync(args);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterMode mode = (FilterMode)comboBoxMode.SelectedItem;
            if (mode == FilterMode.Binary)
            {
                checkBoxCaseSensitive.Enabled = false;
                comboBoxEncoding.Enabled = false;
            }
            else
            {
                checkBoxCaseSensitive.Enabled = true;
                comboBoxEncoding.Enabled = true;
            }
        }
    }
}
