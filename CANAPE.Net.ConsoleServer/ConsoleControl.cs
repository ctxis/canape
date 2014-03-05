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
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;

namespace CANAPE.Net.ConsoleServer
{
    public partial class ConsoleControl : UserControl
    {
        private IDataAdapter _adapter;

        public bool LocalEcho { get; set; }
        
        public void AddText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddText), text);
            }
            else
            {
                if (!richTextBoxOutput.IsDisposed)
                {                    
                    richTextBoxOutput.Text += text;
                    richTextBoxOutput.SelectionStart = richTextBoxOutput.Text.Length;
                    richTextBoxOutput.ScrollToCaret();
                }
            }           
        }

        public ConsoleControl(IDataAdapter adapter, Color foreColor, Color backColor, Font consoleFont)
        {
            _adapter = adapter;
            InitializeComponent();
            if (consoleFont != null)
            {
                richTextBoxOutput.Font = consoleFont;
            }
            richTextBoxOutput.ForeColor = foreColor;
            richTextBoxOutput.BackColor = backColor;
            this.Disposed += ConsoleControl_Disposed;
        }

        void ConsoleControl_Disposed(object sender, EventArgs e)
        {
            _adapter.Close();
        }

        private void textBoxCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                AddInput(textBoxCommand.Text);
                textBoxCommand.Text = String.Empty;             
            }
        }

        private void AddInput(string text)
        {
            string line = text + "\n";
            AddText(line);
            _adapter.Write(new DataFrame(line));
        }

        private void textBoxCommand_TextPasted(object sender, ClipboardEventArgs e)
        {
            if (e.ClipboardText.Contains("\n"))
            {
                e.Handled = true;

                // Get current text, insert at caret and process, leave any trailing line unpasted
                StringBuilder currText = new StringBuilder(textBoxCommand.Text);

                currText.Insert(textBoxCommand.SelectionStart, e.ClipboardText);

                string[] parts = Regex.Split(currText.ToString(), @"(?<=[\n])");
                foreach (string line in parts)
                {
                    if (line.EndsWith("\n"))
                    {
                        AddInput(line.TrimEnd());
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(line))
                        {
                            AddInput(line);
                            textBoxCommand.Text = String.Empty;
                        }
                        else
                        {
                            textBoxCommand.Text = line;
                            textBoxCommand.SelectionStart = line.Length;
                        }
                    }
                }
            }
        }
    }
}
