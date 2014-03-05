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
using System.IO;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    internal partial class StartupForm : Form
    {
        public string FileName { get; set; }

        public StartupForm()
        {
            

            InitializeComponent();
        }

        private void NewProjectForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RecentFiles != null)
            {
                foreach (string file in Properties.Settings.Default.RecentFiles)
                {
                    ListViewItem item = listViewPrevious.Items.Add(Path.GetFileNameWithoutExtension(file));
                    item.ToolTipText = file;
                    item.Tag = file;
                }
            }

            if (listViewPrevious.Items.Count > 0)
            {
                btnPrevious.Enabled = true;
                listViewPrevious.Enabled = true;
                lblPrevious.Enabled = true;
            }
            else
            {
                listViewPrevious.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (listViewPrevious.SelectedItems.Count > 0)
            {
                FileName = (string)listViewPrevious.SelectedItems[0].Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.StartupForm_SelectPreviousItem, 
                    CANAPE.Properties.Resources.StartupForm_SelectPreviousItemCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEmpty_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = CANAPE.Properties.Resources.OpenFileDialog_ProjectFilter;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    FileName = dlg.FileName;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void checkBoxShowAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowStartupForm = checkBoxShowAtStartup.Checked;
            Program.SaveSettings();
        }
    }
}
