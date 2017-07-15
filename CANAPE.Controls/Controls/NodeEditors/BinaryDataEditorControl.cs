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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CANAPE.Utils;
using ICSharpCode.TextEditor.Document;

namespace CANAPE.Controls.NodeEditors
{
    /// <summary>
    /// Control to edit a binary blob in text or binary mode
    /// </summary>
    public partial class BinaryDataEditorControl : UserControl
    {
        private byte[] _data;        
        private bool _changed;
        private Encoding _textEncoding;
        private Color _color;
        private bool _readOnly;

        /// <summary>
        /// Event to signal the data has changed
        /// </summary>
        public event EventHandler DataChanged;

        /// <summary>
        /// Rebuilds data from one or other of the editors
        /// </summary>
        /// <param name="hex">If true rebuild from the hex editor</param>
        private void RebuildData(bool hex)
        {
            if (_changed)
            {                
                if (hex)
                {
                    _data = hexEditorControl.Bytes;              
                }
                else
                {
                    _data = _textEncoding.GetBytes(textEditorControl.Text);
                }

                _changed = false;
            }
        }

        private void LoadData(bool hex)
        {
            if (hex)
            {
                hexEditorControl.Bytes = _data;
                hexEditorControl.ReadOnly = ReadOnly;
            }
            else
            {
                textEditorControl.TextChanged -= new EventHandler(richTextBox_TextChanged);
                textEditorControl.Text = _textEncoding.GetString(_data);
                textEditorControl.TextChanged += new EventHandler(richTextBox_TextChanged);
                textEditorControl.IsReadOnly = ReadOnly;
            }

            _changed = false;
        }

        void richTextBox_TextChanged(object sender, EventArgs e)
        {
            OnDataChanged();
        }

        protected void OnDataChanged()
        {
            _changed = true;

            if (DataChanged != null)
            {
                DataChanged.Invoke(this, new EventArgs());
            }            
        }

        void _byteProv_Changed(object sender, EventArgs e)
        {
            OnDataChanged();
        }

        private bool IsHexSelected()
        {
            return tabControl.SelectedTab == tabPageHex;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public byte[] Data
        {
            get
            {
                RebuildData(IsHexSelected());
                return _data;
            }

            set
            {
                _data = value;
                // Reload the data into which ever is visible
                LoadData(IsHexSelected());
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color PacketColor
        {
            get
            {
                return _color;                
            }

            set
            {
                _color = value;
                hexEditorControl.HexColor = _color;

                DefaultHighlightingStrategy hi = new DefaultHighlightingStrategy();

                hi.Extensions = new string[0];
                hi.AddRuleSet(new HighlightRuleSet());

                HighlightBackground color = hi.GetColorFor("Default") as HighlightBackground;

                color = new HighlightBackground(color.Color, _color, false, false);

                hi.SetColorFor("Default", color);

                textEditorControl.ActiveTextAreaControl.TextArea.Document.HighlightingStrategy = hi;
                textEditorControl.ActiveTextAreaControl.TextArea.Invalidate();
            }
        }

        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; hexEditorControl.ReadOnly = _readOnly; textEditorControl.IsReadOnly = _readOnly; }
        }

        /// <summary>
        /// Switch view
        /// </summary>
        /// <param name="hex">Indicates what we are switch to, if true we are switch to hex view</param>
        private void SwitchViewX(bool hex)
        {            
            RebuildData(!hex);
            LoadData(hex);
        }

        public BinaryDataEditorControl()
        {
            
            InitializeComponent();
            _textEncoding = new BinaryEncoding(true);            
            _data = new byte[0];
        }

        private void BinaryDataEditorControl_Load(object sender, EventArgs e)
        {
            LoadData(true);
        }

        private void DoFind(InlineSearchControl.SearchEventArgs e, bool next)
        {
            bool found = false;

            if (IsHexSelected())
            {
                byte[] data;

                if (e.Data != null)
                {
                    data = e.Data;
                }
                else
                {
                    data = new BinaryEncoding().GetBytes(e.Text);
                }

                found = hexEditorControl.FindAndSelect(data, next);
            }
            else
            {
                string s;                

                if (e.Text != null)
                {
                    s = e.Text;
                }
                else
                {
                    s = new BinaryEncoding().GetString(e.Data);
                }

                found = textEditorControl.FindAndSelect(s, next);
            }

            if (!found)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.BinaryDataEditorControl_NoMatch,
                    CANAPE.Properties.Resources.BinaryDataEditorControl_NoMatchCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inlineSearchControl_SearchNext(object sender, InlineSearchControl.SearchEventArgs e)
        {
            DoFind(e, true);
        }

        private void inlineSearchControl_SearchPrev(object sender, InlineSearchControl.SearchEventArgs e)
        {
            DoFind(e, false);
        }

        private void tabControl_Deselected(object sender, TabControlEventArgs e)
        {
            RebuildData(e.TabPage == tabPageHex);
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            LoadData(e.TabPage == tabPageHex);
        }
    }
}
