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
using System.Text;
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Controls;
using CANAPE.Documents.Data;

namespace CANAPE.Forms
{
    public partial class SelectScriptForm : Form
    {
        public ScriptDocument Document { get; private set; }
        public string ClassName { get; private set; }

        public SelectScriptForm()
        {
            
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (scriptSelectionControl.Document == null)
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.ScriptSelectForm_NoDocument, CANAPE.Properties.Resources.ScriptSelectForm_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrWhiteSpace(scriptSelectionControl.ClassName))
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.ScriptSelectForm_NoClass, CANAPE.Properties.Resources.ScriptSelectForm_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Document = scriptSelectionControl.Document;
                ClassName = scriptSelectionControl.ClassName;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
