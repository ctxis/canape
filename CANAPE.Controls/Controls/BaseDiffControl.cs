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
using System.Threading;
using System.Windows.Forms;
using Be.Windows.Forms;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    public partial class BaseDiffControl : UserControl
    {
        private byte[] _left;
        private byte[] _right;
        private DiffRange[] _bytediffs;        
        private DiffRange[] _linediffs;
        private string[] _leftlines;
        private string[] _rightlines;
        private int _currDiff;
        private Thread _worker;

        private static string[] SplitLines(string text)
        {
            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; ++i)
            {
                lines[i] = lines[i].TrimEnd('\r');
            }

            return lines;
        }

        /// <summary>
        /// Set the data to difference
        /// </summary>
        /// <param name="left">The left data</param>
        /// <param name="right">The right data</param>
        public void SetData(DataNode left, DataNode right)
        {
            _left = left.ToArray();
            _right = right.ToArray();

            BinaryEncoding enc = new BinaryEncoding(true);

            string leftStr = left.ToDataString();
            string rightStr = right.ToDataString();

            leftStr = leftStr.Replace('\0', ' ');
            rightStr = rightStr.Replace('\0', ' ');

            _leftlines = SplitLines(leftStr);
            _rightlines = SplitLines(rightStr);

            richTextBoxLeft.Lines = _leftlines;
            richTextBoxRight.Lines = _rightlines;

            hexEditorControlLeft.Bytes = _left;
            hexEditorControlRight.Bytes = _right;
        }

        public BaseDiffControl()
        {
            _currDiff = -1;
            _bytediffs = new DiffRange[0];
            _linediffs = new DiffRange[0];
            
            InitializeComponent();            
        }

        private void SetLineDifference(int index)
        {
            if ((index >= 0) && (index < _linediffs.Length))
            {
                int leftIdx = richTextBoxLeft.GetFirstCharIndexFromLine((int)_linediffs[index].LeftStartPos);
                int rightIdx = richTextBoxRight.GetFirstCharIndexFromLine((int)_linediffs[index].RightStartPos);

                if ((leftIdx > 0) && (rightIdx > 0))
                {
                    richTextBoxLeft.Select(leftIdx, 0);
                    richTextBoxLeft.ScrollToCaret();
                    richTextBoxRight.Select(rightIdx, 0);
                    richTextBoxRight.ScrollToCaret();
                }

                toolStripLabelInfo.Text = String.Format(Properties.Resources.BinaryFrameDiffControl_LabelFormat,
                    index + 1, _linediffs.Length);

                _currDiff = index;
            }
            else
            {
                toolStripLabelInfo.Text = String.Format(Properties.Resources.BinaryFrameDiffControl_LabelFormat, 0, 0);
            }
        }

        private void SetByteDifference(int index)
        {
            if ((index >= 0) && (index < _bytediffs.Length))
            {                
                hexEditorControlLeft.Select(_bytediffs[index].LeftStartPos, 0);
                hexEditorControlRight.Select(_bytediffs[index].RightStartPos, 0);

                toolStripLabelInfo.Text = String.Format(Properties.Resources.BinaryFrameDiffControl_LabelFormat,
                    index + 1, _bytediffs.Length);

                _currDiff = index;
            }
            else
            {
                toolStripLabelInfo.Text = String.Format(Properties.Resources.BinaryFrameDiffControl_LabelFormat, 0, 0);
            }
        }

        private void SetDifference(int index)
        {
            if (radioButtonBytes.Checked)
            {
                SetByteDifference(index);
            }
            else
            {
                SetLineDifference(index);
            }
        }

        private void ColorLine(RichTextBox textBox, int line, Color color)
        {
            int index = textBox.GetFirstCharIndexFromLine(line);

            if (index >= 0)
            {
                textBox.Select(index, textBox.Lines[line].Length);
                textBox.SelectionBackColor = color;
            }
        }

        private void CompleteDifferences()
        { 
            // This is probably redundant but might as well be sure
            if ((_bytediffs.Length == 0) && (_linediffs.Length == 0))
            {
                MessageBox.Show(this, Properties.Resources.BinaryFrameDiffControl_NoDifferenceFound,
                    Properties.Resources.BinaryFrameDiffControl_NoDifferenceFoundCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripLabelInfo.Text = String.Format(Properties.Resources.BinaryFrameDiffControl_LabelFormat, 0, 0);
            }
            else
            {                
                foreach (DiffRange range in _bytediffs)
                {
                    if (range.LeftLength > 0)
                    {
                        hexEditorControlLeft.AddAnnotation(range.LeftStartPos, range.LeftStartPos + range.LeftLength - 1, Color.Black, DiffRange.GetColor(range.LeftType));
                    }

                    if (range.RightLength > 0)
                    {
                        hexEditorControlRight.AddAnnotation(range.RightStartPos, range.RightStartPos + range.RightLength - 1, Color.Black, DiffRange.GetColor(range.RightType));
                    }
                }

                foreach (DiffRange range in _linediffs)
                {
                    if (range.LeftLength > 0)
                    {
                        for (int i = 0; i < range.LeftLength; ++i)
                        {
                            ColorLine(richTextBoxLeft, (int)range.LeftStartPos + i, DiffRange.GetColor(range.LeftType));
                        }
                    }

                    if (range.RightLength > 0)
                    {
                        for (int i = 0; i < range.RightLength; ++i)
                        {
                            ColorLine(richTextBoxRight, (int)range.RightStartPos + i, DiffRange.GetColor(range.RightType));
                        }
                    }
                }

                SetDifference(0);
            }
        }

        private void BinaryFrameDiffControl_Load(object sender, EventArgs e)
        {
            Disposed += BinaryFrameDiffControl_Disposed;
            if ((_left != null) && (_right != null))
            {                
                toolStripLabelInfo.Text = Properties.Resources.BinaryFrameDiffControl_BuildingDifferences;
                _worker = new Thread(DoWork);
                _worker.Start();
            }
        }

        void BinaryFrameDiffControl_Disposed(object sender, EventArgs e)
        {
            if ((_worker != null) && (_worker.IsAlive))
            {
                _worker.Abort();
                _worker.Join(100);
            }
        }

        private void toolStripButtonPrev_Click(object sender, EventArgs e)
        {
            if (_bytediffs.Length > 0)
            {
                if (_currDiff == 0)
                {
                    int idx;

                    if (radioButtonBytes.Checked)
                    {
                        idx = _bytediffs.Length - 1;
                    }
                    else
                    {
                        idx = _linediffs.Length - 1;
                    }

                    SetDifference(idx);
                }
                else
                {
                    SetDifference(_currDiff - 1);
                }
            }
        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            if (_bytediffs.Length > 0)
            {
                int maxDiff = radioButtonBytes.Checked ? _bytediffs.Length - 1 : _linediffs.Length - 1;

                if (_currDiff == maxDiff)
                {
                    SetDifference(0);
                }
                else
                {
                    SetDifference(_currDiff + 1);
                }
            }
        }

        private void DoWork()
        {
            try
            {
                _bytediffs = DiffRange.BuildDifferences(_left, _right, EqualityComparer<byte>.Default).ToArray();
                _linediffs = DiffRange.BuildDifferences(_leftlines, _rightlines, EqualityComparer<string>.Default).ToArray();
                Invoke(new Action(CompleteDifferences));
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch
            {
            }
        }

        private void radioButtonBytes_CheckedChanged(object sender, EventArgs e)
        {
            bool hexVisible = radioButtonBytes.Checked;
            hexEditorControlLeft.Visible = hexVisible;
            hexEditorControlRight.Visible = hexVisible;
            richTextBoxLeft.Visible = !hexVisible;
            richTextBoxRight.Visible = !hexVisible;
            SetDifference(0);
        }
    }
}
