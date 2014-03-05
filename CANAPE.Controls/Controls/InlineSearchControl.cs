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
using System.Windows.Forms;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    public partial class InlineSearchControl : UserControl
    {
        public class SearchEventArgs : EventArgs
        {
            public byte[] Data { get; private set; }
            public string Text { get; private set; }

            public SearchEventArgs(byte[] data)
            {
                Data = data;
            }

            public SearchEventArgs(string text)
            {
                Text = text;
            }
        }

        public InlineSearchControl()
        {
            
            InitializeComponent();
        }

        private SearchEventArgs CreateEventArgs()
        {
            if (radioBinary.Checked)
            {
                return new SearchEventArgs(GeneralUtils.HexToBinary(textBoxSearch.Text));                
            }
            else
            {
                return new SearchEventArgs(textBoxSearch.Text);
            }
        }

        private void btnNextSearch_Click(object sender, EventArgs e)
        {
            OnSearchNext();
        }

        private void btnPrevSearch_Click(object sender, EventArgs e)
        {
            OnSearchPrev();
        }

        public event EventHandler<SearchEventArgs> SearchNext;
        public event EventHandler<SearchEventArgs> SearchPrev;

        void OnSearchPrev()
        {
            if ((textBoxSearch.Text.Length > 0) && (SearchPrev != null))
            {
                try
                {
                    SearchPrev(this, CreateEventArgs());
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void OnSearchNext()
        {
            if ((textBoxSearch.Text.Length > 0) && (SearchNext != null))
            {
                try
                {
                    SearchNext(this, CreateEventArgs());
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (e.Control)
                {
                    OnSearchPrev();
                }
                else
                {
                    OnSearchNext();
                }
                e.Handled = true;
            }
        }
    }
}
