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
using CANAPE.Extension;

namespace CANAPE.Documents.ComPort
{
    [DocumentEditor(typeof(SerialPortServerDocument), SubControl = true)]
    public partial class SerialPortServerDocumentControl : UserControl
    {
        SerialPortServerDocument _document;

        public SerialPortServerDocumentControl(SerialPortServerDocument document)
        {
            _document = document;
            InitializeComponent();
            serialPortConfigurationControl.Config = _document.ServerPort;
            layerEditorControl.Layers = _document.Layers;
            if (_document.ServerFactory != null)
            {
                dataEndpointSelectionControl.Factory = _document.ServerFactory;
            }
        }

        private void dataEndpointSelectionControl_FactoryChanged(object sender, EventArgs e)
        {
            _document.ServerFactory = dataEndpointSelectionControl.Factory;
            _document.Dirty = true;
        }

        private void layerEditorControl_LayersUpdated(object sender, EventArgs e)
        {
            _document.Layers = layerEditorControl.Layers;
        }
    }
}
