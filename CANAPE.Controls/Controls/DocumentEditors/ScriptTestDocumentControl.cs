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
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Forms;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class ScriptTestDocumentControl : UserControl
    {
        ScriptTestDocument _document;

        public ScriptTestDocumentControl(IDocumentObject document)
        {
            _document = document as ScriptTestDocument;
            
            InitializeComponent();
            graphTestControl.Document = _document;
            textBoxPath.Text = _document.Path;
            comboBoxClassName.Text = _document.ClassName;    
        }

        private void textBoxPath_TextChanged(object sender, EventArgs e)
        {
            _document.Path = textBoxPath.Text;
        }

        private void btnEditConfig_Click(object sender, EventArgs e)
        {
            using (ObjectEditorForm frm = new ObjectEditorForm(_document.Config))
            {
                frm.ShowDialog(this);
                comboBoxClassName.Text = _document.Config.ClassName;
                textBoxPath.Text = _document.Config.SelectionPath;
            }
        }

        private void comboBoxClassName_DropDown(object sender, EventArgs e)
        {
            comboBoxClassName.Items.Clear();
            string[] classNames = _document.GetClassNames();

            foreach (string s in classNames)
            {
                comboBoxClassName.Items.Add(s);
            }
        }

        private void comboBoxClassName_TextChanged(object sender, EventArgs e)
        {
            _document.ClassName = comboBoxClassName.Text;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_document.Path))
            {
                MessageBox.Show(this, "Path must be set to a value", CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrWhiteSpace(_document.ClassName))
            {
                MessageBox.Show(this, "Class name must be set to a value", CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                graphTestControl.Run();
            }
        }
    }
}
