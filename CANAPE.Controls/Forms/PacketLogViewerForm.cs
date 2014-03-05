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
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Forms
{
    internal partial class PacketLogViewerForm : Form
    {
        int _index;
        IList<LogPacket> _packets;
        PacketEntry[] _modifiedPackets;
        bool _newStyleLogViewer;

        private struct PacketEntry
        {
            public bool modified;
            public LogPacket packet;
        }

        public bool ReadOnly { get; set; }

        private bool IsFrameModified()
        {
            if (_packets.Count == 0)
            {
                return false;
            }

            return _modifiedPackets[_index].modified;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool ret = true;

            switch(keyData)
            {
                case Keys.Control | Keys.N:
                    toolStripButtonForward.PerformClick();
                    break;
                case Keys.Control | Keys.P:
                    toolStripButtonBack.PerformClick();
                    break;                    
                case Keys.Control | Keys.S:
                    if (_newStyleLogViewer && IsFrameModified())
                    {
                        toolStripButtonSave.PerformClick();
                    }
                    break;
                default:
                    ret = base.ProcessCmdKey(ref msg, keyData);
                    break;
            }

            return ret;
        }

        public PacketLogViewerForm(LogPacket curr, IList<LogPacket> packets)
        {
            for (int i = 0; i < packets.Count; i++)
            {
                if (packets[i] == curr)
                {
                    _index = i;
                    break;
                }
            }
            
            _packets = packets;
            _modifiedPackets = new PacketEntry[_packets.Count];
            _newStyleLogViewer = GlobalControlConfig.NewStyleLogViewer;

            
            InitializeComponent();

            timer.Start();
        }

        private void SetModifiedFrame()
        {
            _modifiedPackets[_index].modified = false;
            _modifiedPackets[_index].packet = _packets[_index].ClonePacket();
            _modifiedPackets[_index].packet.Frame.FrameModified += new EventHandler(_currPacket_FrameModified);
        }

        private LogPacket GetCurrentPacket()
        {
            if (_packets.Count == 0)
            {
                return null;
            }

            if (_newStyleLogViewer)
            {
                if (_modifiedPackets[_index].packet == null)
                {
                    SetModifiedFrame();
                }

                return _modifiedPackets[_index].packet;
            }
            else
            {
                return _packets[_index];
            }
        }

        private void OnFrameModified()
        {
            _modifiedPackets[_index].modified = true;
            toolStripButtonSave.Enabled = true;
            toolStripButtonRevert.Enabled = true;
        }

        void _currPacket_FrameModified(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnFrameModified));
            }
            else
            {
                OnFrameModified();
            }
        }

        private void UpdatePacketDisplay()
        {
            LogPacket p = GetCurrentPacket();

            if (p == null)
            {
                return;
            }

            if (!_newStyleLogViewer)
            {
                frameEditorControl.ReadOnly = ReadOnly;
            }

            frameEditorControl.SetFrame(p.Frame, null, ColorValueConverter.ToColor(p.Color));

            if (p.Frame.IsBasic || (ReadOnly && !_newStyleLogViewer))
            {
                toolStripButtonConvertToBytes.Enabled = false;
            }
            else
            {
                toolStripButtonConvertToBytes.Enabled = true;
            }

            if (_newStyleLogViewer)
            {
                toolStripButtonSave.Enabled = IsFrameModified();
                toolStripButtonRevert.Enabled = IsFrameModified();
            }

            toolStripLabelPosition.Text = String.Format(CANAPE.Properties.Resources.PacketLogViewerForm_Header, _index + 1, 
                _packets.Count, p.Tag, p.Network, p.Timestamp.ToString());
        }

        private void LogViewerForm_Load(object sender, EventArgs e)
        {
            if(_packets.Count == 0)
            {
                Close();
            }

            if (!_newStyleLogViewer)
            {
                Text = !ReadOnly ? CANAPE.Properties.Resources.PacketLogViewerForm_Title
                        : CANAPE.Properties.Resources.PacketLogViewerForm_TitleReadOnly;
                toolStripButtonSave.Visible = false;
                toolStripButtonRevert.Visible = false;
            }
            else
            {
                Text = CANAPE.Properties.Resources.PacketLogViewerForm_Title;
            }

            UpdatePacketDisplay();
        }

        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {           
            _index -= 1;
            if (_index < 0)
            {
                _index = _packets.Count - 1;
            }

            UpdatePacketDisplay();
        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {           
            _index++;

            if (_index > _packets.Count - 1)
            {
                _index = 0;
            }

            UpdatePacketDisplay();
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            LogPacket currPacket = GetCurrentPacket();

            if (currPacket != null)
            {
                LogPacket[] packets = new LogPacket[1] { currPacket };

                Clipboard.SetData(LogPacket.LogPacketArrayFormat, packets);
            }
        }

        private void toolStripButtonConvertToBytes_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(CANAPE.Properties.Resources.PacketLogViewerForm_ConvertToBytes, 
                CANAPE.Properties.Resources.PacketLogViewerForm_ConvertToBytesCaption, 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    GetCurrentPacket().Frame.ConvertToBasic();
                    UpdatePacketDisplay();
                }
                catch (NullReferenceException)
                {
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void SavePacketAtIndex(int index)
        {
            _modifiedPackets[index].packet.Frame.CopyTo(_packets[index].Frame);
            _modifiedPackets[index].modified = false;
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (_packets.Count == 0)
            {
                return;
            }

            SavePacketAtIndex(_index);
            toolStripButtonSave.Enabled = false;
            toolStripButtonRevert.Enabled = false;
        }

        private void toolStripButtonRevert_Click(object sender, EventArgs e)
        {
            SetModifiedFrame();
            UpdatePacketDisplay();
        }

        private void PacketLogViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_newStyleLogViewer)
            {
                bool isModified = IsFrameModified();

                // Check first, most likely candidate
                if (!isModified)
                {
                    foreach (PacketEntry ent in _modifiedPackets)
                    {
                        if (ent.modified)
                        {
                            isModified = true;
                            break;
                        }
                    }
                }

                if (isModified)
                {
                    bool savePackets = false;

                    if (GlobalControlConfig.LogConfirmMode == PacketLogConfirmMode.ConfirmSaveOnClose)
                    {
                        // Confirm save
                        DialogResult res = MessageBox.Show(this, Properties.Resources.PacketLogViewerForm_ConfirmSave,
                                Properties.Resources.PacketLogViewerForm_ConfirmSaveCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (res == DialogResult.Yes)
                        {
                            savePackets = true;
                        }
                        else if (res == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                        }
                    }
                    else if (GlobalControlConfig.LogConfirmMode == PacketLogConfirmMode.NoConfirmAutoSave)
                    {
                        // Auto save
                        savePackets = true;
                    }
                    else
                    {
                        // We just discard everything
                    }

                    if (savePackets)
                    {
                        for (int i = 0; i < _packets.Count; ++i)
                        {
                            if (_modifiedPackets[i].modified)
                            {
                                SavePacketAtIndex(i);
                            }
                        }
                    }
                }
            }
        }

        private void toolStripButtonPasteFrame_Click(object sender, EventArgs e)
        {
            try
            {
                LogPacket[] packets = Clipboard.GetData(LogPacket.LogPacketArrayFormat) as LogPacket[];

                if ((packets != null) && (packets.Length > 0))
                {
                    packets[0].Frame.CopyTo(GetCurrentPacket().Frame);
                    UpdatePacketDisplay();
                }
            }
            catch (ExternalException)
            {
            }
            catch (ThreadStateException)
            {
            }
            catch (NullReferenceException)
            {
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                toolStripButtonPasteFrame.Enabled = Clipboard.ContainsData(LogPacket.LogPacketArrayFormat);
            }
            catch (ExternalException)
            {
            }
            catch (ThreadStateException)
            {
            }
        }
    }
}
