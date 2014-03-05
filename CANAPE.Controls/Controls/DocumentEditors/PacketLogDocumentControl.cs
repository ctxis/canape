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
    public partial class PacketLogDocumentControl : UserControl
    {
        PacketLogDocument _document;

        private const string PACKETLOG_CONFIG = "PacketLogControlConfig";

        public PacketLogDocumentControl(IDocumentObject document)
        {
            _document = (PacketLogDocument)document;
            
            InitializeComponent();
            logPacketControl.SetPackets(_document.Packets);
            logPacketControl.LogName = _document.Name;
            PacketLogControlConfig config = _document.GetProperty(PACKETLOG_CONFIG) as PacketLogControlConfig;

            if (config != null)
            {
                logPacketControl.Config = config;
            }

            Text = _document.Name;
        }

        private void logPacketControl_ConfigChanged(object sender, EventArgs e)
        {
            _document.SetProperty(PACKETLOG_CONFIG, logPacketControl.Config);
        }
    }
}
