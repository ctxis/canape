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
using System.Security;

namespace CANAPE.Controls.Forms
{
    public partial class GetPasswordForm : Form
    {
        public SecureString Password { get; private set; }

        public GetPasswordForm()
        {
            
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Password = new SecureString();

            foreach (char c in textBoxPassword.Text)
            {
                Password.AppendChar(c);
            }
            Password.MakeReadOnly();
            
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
