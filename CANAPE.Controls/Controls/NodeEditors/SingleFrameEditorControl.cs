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
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CANAPE.DataFrames;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Controls.NodeEditors
{
    public partial class SingleFrameEditorControl : UserControl
    {
        DataFrame _currFrame;
        DataFrame _oldFrame;
        string _selector;
        Color _color;        

        public SingleFrameEditorControl()
        {
            InitializeComponent();
            timerUpdatePaste.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ReadOnly
        {
            get { return frameEditorControl.ReadOnly; }
            set
            {
                frameEditorControl.ReadOnly = value;
            }
        }

        private void UpdateFrame()
        {
            _currFrame.FrameModified += _currFrame_FrameModified;

            toolStripButtonConvertToBytes.Enabled = !_currFrame.IsBasic;            

            frameEditorControl.SetFrame(_currFrame, _selector, _color);
        }

        /// <summary>
        /// Set the frame to edit
        /// </summary>
        /// <param name="frame">The data frame</param>        
        /// <param name="selector">Root selection path</param>
        /// <param name="color">The colour to display the frame in (if applicable)</param>
        public void SetFrame(DataFrame frame, string selector, Color color)
        {
            toolStripButtonSnapshot.Enabled = false;
            toolStripButtonUndo.Enabled = false;
            _oldFrame = frame;
            _currFrame = frame.CloneFrame();
            _selector = selector;
            _color = color;                        

            UpdateFrame();
        }

        public event EventHandler FrameModified;        

        void UpdateFrameModified()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateFrameModified));
            }
            else
            {                
                toolStripButtonUndo.Enabled = true;
                toolStripButtonSnapshot.Enabled = true;
                EventHandler frameModified = FrameModified;

                if (frameModified != null)
                {
                    frameModified(this, new EventArgs());
                }
            }
        }

        void _currFrame_FrameModified(object sender, EventArgs e)
        {
            UpdateFrameModified();
        }

        public DataFrame GetFrame()
        {
            return _currFrame.CloneFrame();
        }

        private void toolStripButtonCopyFrame_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = new LogPacket[1] { new LogPacket("Copied", Guid.Empty, "Copied", _currFrame.CloneFrame(), _color.FromColor()) };

            try
            {
                Clipboard.SetData(LogPacket.LogPacketArrayFormat, packets);
            }
            catch (ExternalException)
            {
            }
            catch(ThreadStateException)
            {
            }
        }

        private void timerUpdatePaste_Tick(object sender, EventArgs e)
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

        private void toolStripButtonPasteFrame_Click(object sender, EventArgs e)
        {
            try
            {
                LogPacket[] packets = Clipboard.GetData(LogPacket.LogPacketArrayFormat) as LogPacket[];

                if ((packets != null) && (packets.Length > 0))
                {
                    _currFrame = packets[0].Frame;
                    UpdateFrame();
                    UpdateFrameModified();
                }
            }
            catch (ExternalException)
            {
            }
            catch(ThreadStateException)
            {
            }
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            _currFrame = _oldFrame.CloneFrame();
            UpdateFrame();
            UpdateFrameModified();
            toolStripButtonUndo.Enabled = false;
            toolStripButtonSnapshot.Enabled = false;
        }

        private void toolStripButtonConvertToBytes_Click(object sender, EventArgs e)
        {
            _currFrame.ConvertToBasic();
            UpdateFrame();
            UpdateFrameModified();            
        }

        private void toolStripButtonLoadFromFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = Properties.Resources.AllFilesFilter_String;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] data = File.ReadAllBytes(dlg.FileName);

                        _currFrame = new DataFrame(data);
                        UpdateFrame();
                        UpdateFrameModified();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripButtonSnapshot_Click(object sender, EventArgs e)
        {
            _oldFrame = _currFrame.CloneFrame();
            toolStripButtonSnapshot.Enabled = false;
            toolStripButtonUndo.Enabled = false;
        }
    }
}
