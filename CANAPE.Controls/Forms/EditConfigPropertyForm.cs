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
using System.Globalization;
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Net.NodeConfigs;
using System.ComponentModel;
using CANAPE.Utils;
using CANAPE.Documents.Data;
using System.Text;
using CANAPE.Controls;
using CANAPE.Documents.Net;

namespace CANAPE.Forms
{
    public partial class EditConfigPropertyForm : Form
    {
        public EditConfigPropertyForm()
        {
            
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public DynamicNodeConfigProperty Property { get; set; }

        private void EditConfigPropertyForm_Load(object sender, EventArgs e)
        {
            comboBoxTypes.Items.Add(typeof(int));
            comboBoxTypes.Items.Add(typeof(long));
            comboBoxTypes.Items.Add(typeof(string));
            comboBoxTypes.Items.Add(typeof(byte[]));
            comboBoxTypes.Items.Add(typeof(string[]));
            comboBoxTypes.Items.Add(typeof(ColorValue));
            comboBoxTypes.Items.Add(typeof(ScriptDocument));
            comboBoxTypes.Items.Add(typeof(NetGraphDocument));
            comboBoxTypes.Items.Add(typeof(PacketLogDocument));
            comboBoxTypes.Items.Add(typeof(TextDocument));
            comboBoxTypes.Items.Add(typeof(BinaryDocument));
            hexEditorControl.Bytes = new byte[0];

            if (Property != null)
            {
                textBoxName.Text = Property.Name;
                textBoxDescription.Text = Property.Description ?? String.Empty;
                comboBoxTypes.SelectedItem = Property.PropertyType;

                if (Property.PropertyType == typeof(byte[]))
                {
                    hexEditorControl.Bytes = (byte[])Property.DefaultValue ?? new byte[0];
                }
                else if (Property.DefaultValue != null)
                {
                    if (Property.PropertyType == typeof(string[]))
                    {
                        textBoxStringValue.Lines = (string[])Property.DefaultValue;
                    }
                    else
                    {
                        textBoxStringValue.Text = Property.DefaultValue.ToString();
                    }
                }
            }
        }

        private void comboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type selectedType = comboBoxTypes.SelectedItem as Type;

            if (selectedType != null)
            {
                if (selectedType == typeof(byte[]))
                {
                    hexEditorControl.Visible = true;
                    hexEditorControl.Enabled = true;
                    textBoxStringValue.Visible = false;
                    textBoxStringValue.Enabled = false;
                }
                else if (!typeof(IDocumentObject).IsAssignableFrom(selectedType))
                {
                    hexEditorControl.Visible = false;
                    hexEditorControl.Enabled = false;
                    textBoxStringValue.Visible = true;
                    textBoxStringValue.Enabled = true;
                }
                else
                {                    
                    hexEditorControl.Enabled = false;                    
                    textBoxStringValue.Enabled = false;
                }
            }
        }

        private bool IsValidName()
        {
            string name = textBoxName.Text;
            bool ret = false;

            if (!String.IsNullOrWhiteSpace(name))
            {            
                if (Char.IsLetter(name[0]) || (name[0] == '_'))
                {
                    int i = 1;

                    for (i = 1; i < name.Length; ++i)
                    {
                        if (!Char.IsLetterOrDigit(name[i]) && (name[i] != '_'))
                        {
                            break;
                        }
                    }

                    if (i == name.Length)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Type selectedType = comboBoxTypes.SelectedItem as Type;

            if (selectedType == null)
            {
                MessageBox.Show(this, Properties.Resources.EditConfigPropertyForm_SelectAType,
                    Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!IsValidName())
            {
                MessageBox.Show(this, Properties.Resources.EditConfigPropertyForm_SpecifyAName,
                    Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                object defaultValue = null;

                try
                {
                    if (selectedType == typeof(string))
                    {
                        defaultValue = textBoxStringValue.Text;
                    }
                    else if (selectedType == typeof(string[]))
                    {
                        defaultValue = textBoxStringValue.Lines;
                    }
                    else if (selectedType == typeof(byte[]))
                    {
                        defaultValue = hexEditorControl.Bytes;
                    }
                    else if (selectedType == typeof(ColorValue))
                    {
                        defaultValue = ColorValue.Parse(textBoxStringValue.Text);
                    }
                    else if (!typeof(IDocumentObject).IsAssignableFrom(selectedType))
                    {
                        defaultValue = Convert.ChangeType(textBoxStringValue.Text, selectedType, CultureInfo.CurrentUICulture);
                    }

                    Property = new DynamicNodeConfigProperty(textBoxName.Text, selectedType, defaultValue, textBoxDescription.Text);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show(this, ex.Message,
                        Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (OverflowException ex)
                {
                    MessageBox.Show(this, ex.Message,
                        Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(this, ex.Message,
                        Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
