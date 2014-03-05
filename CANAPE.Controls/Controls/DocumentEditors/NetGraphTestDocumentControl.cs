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

using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Net;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class NetGraphTestDocumentControl : UserControl
    {
        NetGraphTestDocument _document;

        public NetGraphTestDocumentControl(IDocumentObject document)
        {
            _document = document as NetGraphTestDocument;
            
            InitializeComponent();
            graphTestControl.Document = _document;
            if (_document.ClientToServer)
            {
                radioClientToServer.Checked = true;
            }
            else
            {
                radioServerToClient.Checked = true;
            }
        }

        private void radioClientToServer_CheckedChanged(object sender, System.EventArgs e)
        {
            _document.ClientToServer = radioClientToServer.Checked;
        }

        private void btnRun_Click(object sender, System.EventArgs e)
        {
            graphTestControl.Run();
        }
    }
}
