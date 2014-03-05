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
    /// <summary>
    /// 
    /// </summary>
    public partial class TextDocumentControl : UserControl
    {
        TextDocument _document;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        public TextDocumentControl(IDocumentObject document)
        {
            _document = (TextDocument)document;
            
            InitializeComponent();
            textBox.Text = _document.Text;
            Text = _document.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            _document.Text = textBox.Text;
        }
    }
}
