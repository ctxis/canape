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
using System.Windows.Forms;
using CANAPE.Documents.Net.Factories;
using CANAPE.Forms;

namespace CANAPE.Controls
{
    public partial class DataEndpointSelectionControl : UserControl
    {
        DataEndpointFactory _factory;

        public DataEndpointSelectionControl()
        {
            
            InitializeComponent();
        }

        private void UpdateFactory(DataEndpointFactory factory)
        {
            _factory = factory;

            textBoxLibraryServer.Text = factory.Name;
            propertyGrid.SelectedObject = factory.Config;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataEndpointFactory Factory
        {
            get { return _factory; }
            set
            {
                UpdateFactory(value);
            }
        }

        protected void OnFactoryChanged()
        {
            if (FactoryChanged != null)
            {
                FactoryChanged(this, new EventArgs());
            }
        }

        public event EventHandler FactoryChanged;

        private void btnSelectLibrary_Click(object sender, EventArgs e)
        {
            using (SelectEndpointForm frm = new SelectEndpointForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    UpdateFactory(frm.Server);
                    OnFactoryChanged();
                }
            }
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            OnFactoryChanged();
        }
    }
}
