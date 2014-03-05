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
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using System.Reflection;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class AssemblyDocumentControl : UserControl
    {
        AssemblyDocument _document;

        public AssemblyDocumentControl(IDocumentObject document)
        {
            _document = (AssemblyDocument)document;
            
            InitializeComponent();
            textBoxPath.Text = _document.OriginalPath ?? "";

            AssemblyName name = _document.GetName();

            if (name != null)
            {
                lblAsmNameValue.Text = name.FullName;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Assembly Files (*.dll;*.exe)|*.dll;*.exe|All Files (*.*)|*.*";
                dlg.FileName = textBoxPath.Text;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    textBoxPath.Text = dlg.FileName;
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                _document.ReloadAssembly(File.ReadAllBytes(textBoxPath.Text), textBoxPath.Text);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (BadImageFormatException ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
