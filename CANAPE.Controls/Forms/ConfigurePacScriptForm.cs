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
using System.Net;
using System.Windows.Forms;
using CANAPE.Documents.Net.Factories;
using CANAPE.Utils;
using System.Reflection;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    public partial class ConfigurePacScriptForm : Form
    {
        public string Script { get; set; }

        public ConfigurePacScriptForm()
        {
            
            InitializeComponent();
        }

        private void ConfigurePacScriptForm_Load(object sender, EventArgs e)
        {
            textBoxScript.Text = Script ?? String.Empty;
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = Properties.Resources.AllFilesFilter_String;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        textBoxScript.Text = File.ReadAllText(dlg.FileName, BinaryEncoding.Instance);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBoxScript_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = !String.IsNullOrWhiteSpace(textBoxScript.Text);
            Script = textBoxScript.Text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptProxyClientFactory factory = new ScriptProxyClientFactory();
                factory.Script = textBoxScript.Text;
                IWebProxyScript proxy = factory.GetProxyInstance();

                if (proxy != null)
                {
                    Uri u = new Uri(textBoxUrl.Text, UriKind.Absolute);

                    MessageBox.Show(this, String.Format(Properties.Resources.ConfigurePacScriptForm_TestReturned,
                        proxy.Run(u.AbsoluteUri, u.Host)));
                }
                else
                {
                    MessageBox.Show(this, Properties.Resources.ConfigurePacScriptForm_CannotUseProxyScript, 
                        Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                TargetInvocationException tex = ex as TargetInvocationException;
                
                MessageBox.Show(this, tex != null ? tex.InnerException.Message : ex.Message, Properties.Resources.MessageBox_ErrorString, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
