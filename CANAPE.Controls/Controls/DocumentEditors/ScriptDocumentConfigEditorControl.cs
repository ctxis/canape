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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class ScriptDocumentConfigEditorControl : UserControl
    {
        public event EventHandler ConfigChanged;

        public ScriptDocumentConfigEditorControl()
        {
            InitializeComponent();
        }

        private void UpdateConfig(ScriptDocumentControlConfig config)
        {           
            checkBoxShowTabs.Checked = config.ShowTabs;
            checkBoxShowSpaces.Checked = config.ShowSpaces;
            checkBoxShowLineNumbers.Checked = !config.HideLineNumbers;
            checkBoxConvertTabsToSpaces.Checked = config.ConvertTabsToSpaces;            
            checkBoxShowEOLMarkers.Checked = config.ShowEndofLineMarkers;
        }

        private ScriptDocumentControlConfig RebuildConfig()
        {
            ScriptDocumentControlConfig config = new ScriptDocumentControlConfig();

            config.ConvertTabsToSpaces = checkBoxConvertTabsToSpaces.Checked;
            config.HideLineNumbers = !checkBoxShowLineNumbers.Checked;
            config.ShowSpaces = checkBoxShowSpaces.Checked;
            config.ShowTabs = checkBoxShowTabs.Checked;
            config.ShowEndofLineMarkers = checkBoxShowEOLMarkers.Checked;            

            return config;
        }

        void check_Changed(object sender, EventArgs e)
        {
            OnConfigChanged();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScriptDocumentControlConfig Config
        {
            get { return RebuildConfig(); }
            set { UpdateConfig(value); }
        }

        private void OnConfigChanged()
        {
            if (ConfigChanged != null)
            {
                ConfigChanged(this, new EventArgs());
            }
        }

    }
}
