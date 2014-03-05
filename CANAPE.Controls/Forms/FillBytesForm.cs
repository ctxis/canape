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
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Utils;

namespace CANAPE.Forms
{
    public partial class FillBytesForm : Form
    {
        DynamicByteProvider _provider;
        long _startPos;
        long _length;

        public FillBytesForm(DynamicByteProvider provider, long startPos, long length)
        {
            
            InitializeComponent();
            _provider = provider;
            _startPos = startPos;
            _length = length;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {               
                if (radioButtonHexValues.Checked)
                {
                    StringBuilder builder = new StringBuilder();

                    if (String.IsNullOrWhiteSpace(textBoxHex.Text))
                    {
                        throw new ArgumentException(CANAPE.Properties.Resources.FillBytesForm_EmptyHexString);
                    }

                    foreach (char c in textBoxHex.Text)
                    {
                        if (GeneralUtils.IsHex(c))
                        {
                            builder.Append(c);
                        }
                        else if (!Char.IsWhiteSpace(c))
                        {
                            throw new ArgumentException(CANAPE.Properties.Resources.FillBytesForm_InvalidHexString);
                        }
                    }

                    string hexString = builder.ToString();

                    if ((hexString.Length % 2) != 0)
                    {
                        throw new ArgumentException(CANAPE.Properties.Resources.FillBytesForm_InvalidHexStringLength);
                    }

                    List<byte> bytes = new List<byte>();
                    for(int i = 0; i < hexString.Length; i+=2)
                    {
                        bytes.Add(byte.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber));
                    }

                    for (int i = 0; i < _length; ++i)
                    {
                        _provider.WriteByte(_startPos + i, bytes[i % bytes.Count]);
                    }
                }
                else if (radioButtonRandom.Checked)
                {
                    Random rand = new Random();
                    int min = (int)numericUpDownMin.Value;
                    int max = (int)numericUpDownMax.Value;

                    if (max < min)
                    {
                        throw new ArgumentException(CANAPE.Properties.Resources.FillBytesForm_MinMaxInvalid);
                    }
                    
                    for (int i = 0; i < _length; ++i)
                    {
                        _provider.WriteByte(_startPos + i, (byte)rand.Next(min, max));
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButtonRandom_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownMax.Enabled = radioButtonRandom.Checked;
            numericUpDownMin.Enabled = radioButtonRandom.Checked;
            labelMax.Enabled = radioButtonRandom.Checked;
            labelMin.Enabled = radioButtonRandom.Checked;
            textBoxHex.Enabled = !radioButtonRandom.Checked;
        }
    }
}
