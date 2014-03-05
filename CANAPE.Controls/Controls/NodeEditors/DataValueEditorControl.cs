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
using CANAPE.DataFrames;

namespace CANAPE.Controls.NodeEditors
{
    /// <summary>
    /// Default datavalue editor, has no class because it is only used in specific circumstances
    /// </summary>
    public partial class DataValueEditorControl : UserControl, IDataNodeEditor
    {
        private DataValue _value;
        private Control _currControl;

        /// <summary>
        /// 
        /// </summary>
        public DataValueEditorControl()
        {
            
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            SaveValue();
        }

        private void InvalidConversion(Exception e)
        {
            //MessageBox.Show(this, e.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SaveValue()
        {
            try
            {
                if (_value.Value is bool)
                {
                    _value.Value = checkBoxBoolean.Checked;
                }
                else if (_value.Value is DateTime)
                {
                    _value.Value = dateTimePicker.Value;
                }
                else
                {
                    _value.Value = textBox.Text;
                }
            }
            catch (InvalidCastException e)
            {
                InvalidConversion(e);
            }
            catch (OverflowException e)
            {
                InvalidConversion(e);
            }
            catch (FormatException e)
            {
                InvalidConversion(e);
            }
            catch (ArgumentException e)
            {
                InvalidConversion(e);
            }
        }

        private void ChangeValue(DataValue value, bool readOnly)
        {
            Control newControl = null;
            _value = value;

            if (_value != null)
            {
                if (_value.Value is bool)
                {
                    checkBoxBoolean.Enabled = !(readOnly || _value.Readonly);
                    checkBoxBoolean.CheckedChanged -= checkBoxBoolean_CheckedChanged;
                    checkBoxBoolean.Checked = _value.Value;
                    checkBoxBoolean.CheckedChanged += checkBoxBoolean_CheckedChanged;
                    newControl = checkBoxBoolean;
                }
                else if (_value.Value is DateTime)
                {
                    dateTimePicker.Enabled = !(readOnly || _value.Readonly);
                    dateTimePicker.Value = _value.Value;
                    newControl = dateTimePicker;
                }
                else
                {
                    textBox.ReadOnly = readOnly || _value.Readonly;
                    textBox.TextChanged -= textBox_TextChanged;
                    textBox.Text = _value.Value.ToString();
                    textBox.TextChanged += textBox_TextChanged;
                    newControl = textBox;
                }
            }

            if (newControl != _currControl)
            {
                if (_currControl != null)
                {
                    _currControl.Visible = false;
                }
                _currControl = newControl;
                _currControl.Visible = true;
            }
        }

        public void SetNode(DataNode node, DataNode selected, System.Drawing.Color color, bool readOnly)
        {
            // Quick hack for now
            if (node is DataValue)
            {
                ChangeValue((DataValue)node, readOnly);
            }
        }

        private void checkBoxBoolean_CheckedChanged(object sender, EventArgs e)
        {
            if (_value != null)
            {
                SaveValue();
            }
        }
    }
}
