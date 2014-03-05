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
using System.IO;
using System.Text;
using System.Windows.Forms;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    public partial class DebugWindowForm : Form
    {
        void WriteText(string txt)
        {
            if (!String.IsNullOrWhiteSpace(txt))
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(WriteText), txt);
                }
                else
                {
                    listViewDebugOutput.AddItem(new ListViewItem(txt));
                }
            }
        }

        class OutputTextWriter : TextWriter
        {
            private DebugWindowForm _form;

            public OutputTextWriter(DebugWindowForm form)
            {
                _form = form;
            }

            public override void WriteLine(string value)
            {
                _form.WriteText(value.Trim());
            }

            public override void Write(char value)
            {
                _form.WriteText(value.ToString().Trim());
            }

            public override void Write(string value)
            {
                _form.WriteText(value.Trim());
            }

            public override void Write(char[] buffer, int index, int count)
            {
                _form.WriteText(new string(buffer, index, count));
            }            

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        OutputTextWriter _stdout;
        OutputTextWriter _stderr;

        public DebugWindowForm()
        {
            
            InitializeComponent();
        }

        private void DebugWindowForm_Load(object sender, EventArgs e)
        {
            _stdout = new OutputTextWriter(this);
            Console.SetOut(_stdout);
            _stderr = new OutputTextWriter(this);
            Console.SetError(_stderr);            
        }

        private void DebugWindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput());
            StreamWriter stderr = new StreamWriter(Console.OpenStandardError());
            Console.SetOut(stdout);
            Console.SetError(stderr);
        }

        private void toolStripButtonClearLog_Click(object sender, EventArgs e)
        {
            listViewDebugOutput.Items.Clear();
        }

        private void autoScrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listViewDebugOutput.AutoScrollList = !listViewDebugOutput.AutoScrollList;
            autoScrollToolStripMenuItem.Checked = listViewDebugOutput.AutoScrollList;
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            alwaysOnTopToolStripMenuItem.Checked = this.TopMost;
        }
    }
}
