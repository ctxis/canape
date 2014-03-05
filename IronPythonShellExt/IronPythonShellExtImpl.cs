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

using CANAPE;
using CANAPE.Controls;
using CANAPE.Documents;
using CANAPE.Net;
using CANAPE.Nodes;
using CANAPE.Parser;
using CANAPE.Scripting;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Hosting.Providers;
using Microsoft.Scripting.Hosting.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CANAPE.Extension;
using CANAPE.Forms;
using CANAPE.Security.Cryptography.X509Certificates;
using CANAPE.Documents.Net;
using System.Text;

namespace IronPythonShellExt
{
    [MainFormMenuExtension("IronPython Shell", ShortcutKeys = Keys.Control | Keys.Alt | Keys.P)]
    public class IronPythonShellExtImpl : IMainFormMenuExtension
    {
        public void Execute(MainForm mainForm, ToolStripMenuItem menu)
        {
            try
            {
                ConsoleControl control = new ConsoleControl();                

                DocumentControl.ShowControl("IronPython Shell", control);
            }
            catch
            {

            }
        }
    }
}

