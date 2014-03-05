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

namespace CANAPE.Controls
{
    public partial class RedirectLogControl : UserControl
    {
        public enum RedirectLogMode
        {
            Discard,
            ToFile,
        }

        private RedirectLogMode _mode;
        private string _outputDirectory;

        public RedirectLogControl()
        {
            
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (!String.IsNullOrWhiteSpace(textBoxBaseName.Text))
            {
                dlg.SelectedPath = textBoxBaseName.Text;
            }

            dlg.ShowNewFolderButton = true;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                textBoxBaseName.Text = dlg.SelectedPath;
            }
        }

        public string BaseDirectory { get { return _outputDirectory; } }

        public RedirectLogMode Mode { get { return _mode; } }

        private void radioButtonToFiles_CheckedChanged(object sender, EventArgs e)
        {
            lblBaseName.Enabled = radioButtonToFiles.Checked;
            textBoxBaseName.Enabled = radioButtonToFiles.Checked;
            btnBrowse.Enabled = radioButtonToFiles.Checked;

            _mode = radioButtonToFiles.Checked ? RedirectLogMode.ToFile : RedirectLogMode.Discard;
        }

        private void textBoxBaseName_TextChanged(object sender, EventArgs e)
        {
            _outputDirectory = textBoxBaseName.Text;
        }
    }
}
