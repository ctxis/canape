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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CANAPE.Extension;
using CANAPE.Utils;

namespace CANAPE.Forms
{
    /// <summary>
    /// Extension manager form
    /// </summary>
    public partial class ExtensionManagerForm : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExtensionManagerForm()
        {
            InitializeComponent();

            Dictionary<string, Assembly> assemblies = CANAPEExtensionManager.GetUserExtensionAssemblies();

            foreach (Assembly asm in assemblies.Values)
            {
                ListViewItem item = new ListViewItem(asm.GetName().Name);
                item.SubItems.Add(new Uri(asm.CodeBase).LocalPath);
                item.Tag = asm;

                listViewExtensions.Items.Add(item);
            }
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            string userExtensionDir = CANAPEExtensionManager.GetUserExtensionDirectory(true);

            if (userExtensionDir != null)
            {
                GuiUtils.OpenDocument(userExtensionDir);
            }
            else
            {
                MessageBox.Show(this, Properties.Resources.ExtensionManagerForm_CouldntCreateExtensionDir, 
                    Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
