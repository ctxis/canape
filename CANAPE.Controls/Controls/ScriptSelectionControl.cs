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

namespace CANAPE.Controls
{
    public partial class ScriptSelectionControl : UserControl
    {
        public ScriptDocument Document { get; set; }

        public string ClassName { get; set; }

        public Type[] ScriptTypes { get; set; }

        public ScriptSelectionControl()
        {
            ScriptTypes = new Type[0];
            InitializeComponent();
        }

        private void UpdateScripts()
        {
            comboBoxScript.Items.Clear();

            foreach (ScriptDocument doc in CANAPEProject.CurrentProject.GetDocumentsByType(typeof(ScriptDocument)))
            {
                comboBoxScript.Items.Add(doc);
            }
        }

        private void ScriptSelectionControl_Load(object sender, EventArgs e)
        {
            UpdateScripts();

            if (comboBoxScript.Items.Count > 0)
            {
                comboBoxScript.SelectedIndex = 0;
            }
        }

        public event EventHandler DocumentChanged;

        public event EventHandler ClassNameChanged;

        private void comboBoxScript_SelectedIndexChanged(object sender, EventArgs e)
        {
            Document = (ScriptDocument)comboBoxScript.SelectedItem;

            if(Document != null)
            {
                comboBoxClassName.Items.Clear();
                foreach (string className in Document.Container.GetClassNames(ScriptTypes))
                {
                    comboBoxClassName.Items.Add(className);
                }
            }

            if (DocumentChanged != null)
            {
                DocumentChanged(this, new EventArgs());
            }
        }

        private void comboBoxClassName_TextChanged(object sender, EventArgs e)
        {
            ClassName = comboBoxClassName.Text;

            if (ClassNameChanged != null)
            {
                ClassNameChanged(this, new EventArgs());
            }
        }

        private void comboBoxScript_DropDown(object sender, EventArgs e)
        {
            object selectedItem = comboBoxScript.SelectedItem;

            UpdateScripts();

            comboBoxScript.SelectedItem = selectedItem;
        }

    }
}
