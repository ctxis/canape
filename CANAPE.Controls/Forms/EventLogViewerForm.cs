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
using CANAPE.Controls;
using CANAPE.Utils;

namespace CANAPE.Forms
{
    public partial class EventLogViewerForm : Form
    {
        public EventLogViewerForm(Logger.EventLogEntry entry)
        {
            
            InitializeComponent();
            lblSourceName.Text = entry.SourceName ?? "";
            lblTimestampName.Text = entry.Timestamp.ToString();
            lblTypeName.Text = entry.EntryType.ToString();
            if (entry.ExceptionObject != null)
            {
                richTextBoxDetails.Text = entry.ExceptionObject.ToString();
            }
            else
            {
                richTextBoxDetails.Text = entry.Text;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
