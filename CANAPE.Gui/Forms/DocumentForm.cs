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
using CANAPE.Controls.DocumentEditors;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net;
using CANAPE.Utils;
using WeifenLuo.WinFormsUI.Docking;
using CANAPE.Extension;
using System.Linq;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DocumentForm : DockContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private static Control GetControlForDocument(IDocumentObject document)
        {
            foreach (var ext in DocumentEditorManager.Instance.GetExtensions().Where(e => !e.ExtensionAttribute.SubControl))
            {
                if (ext.ExtensionAttribute.DocumentType == document.GetType())
                {
                    return (Control)Activator.CreateInstance(ext.ExtensionType, document);
                }
            }

            if (document is NetServiceDocument)
            {
                return new NetServiceDocumentControl((NetServiceDocument)document);
            }
            else if (document is StateGraphDocument)
            {
                return new StateGraphDocumentControl(document);
            }
            else if (document is NetGraphDocument)
            {
                return new NetGraphDocumentControl((NetGraphDocument)document);
            }
            else if (document is TextDocument)
            {
                return new TextDocumentControl(document);
            }
            else if (document is PacketLogDocument)
            {
                return new PacketLogDocumentControl(document);
            }
            else if (document is ParserDocument)
            {
                return new ParserDocumentControl(document);
            }
            else if (document is AssemblyDocument)
            {
                return new AssemblyDocumentControl(document);
            }
            else if (document is ScriptDocument)
            {
                return new ScriptDocumentControl(document);
            }
            else if (document is ScriptTestDocument)
            {
                return new ScriptTestDocumentControl(document);
            }
            else if (document is NetGraphTestDocument)
            {
                return new NetGraphTestDocumentControl(document);
            }
            else if (document is BinaryDocument)
            {
                return new BinaryDocumentControl(document);
            }
            else if (document is PacketLogDiffDocument)
            {
                return new PacketLogDiffDocumentControl((PacketLogDiffDocument)document);
            }
            else
            {
                throw new ArgumentException(
                    String.Format(CANAPE.Properties.Resources.DocumentForm_NoRegisteredEditor, document.Name));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the form</param>
        /// <param name="control">A control to host</param>
        public DocumentForm(string name, Control control)
        {
                       
            InitializeComponent();            
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);            
            Text = name;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document">The document to display</param>
        public DocumentForm(IDocumentObject document) 
            : this(document.Name, GetControlForDocument(document))
        {
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDockContent[] content = DockPanel.DocumentsToArray();

            foreach (IDockContent c in content)
            {
                Form frm = c as Form;

                if ((frm != null) && (frm != this))
                {
                    frm.Close();
                }                    
            }            
        }
    }
}
