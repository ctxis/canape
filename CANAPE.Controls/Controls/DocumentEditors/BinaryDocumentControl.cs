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

namespace CANAPE.Controls.DocumentEditors
{
    public partial class BinaryDocumentControl : UserControl
    {
        BinaryDocument _document;

        public BinaryDocumentControl(IDocumentObject document)
        {
            _document = (BinaryDocument)document;
            
            InitializeComponent();
            hexEditorControl.Bytes = _document.Data;
        }

        private void hexEditorControl_BytesChanged(object sender, EventArgs e)
        {
            _document.Data = hexEditorControl.Bytes;
        }
    }
}
