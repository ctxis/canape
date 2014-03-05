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
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Utils;
using System.Xml;
using System.Globalization;
using System.Collections;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    public partial class AddReferenceForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public AssemblyName[] SelectedNames { get; private set; }

        public AddReferenceForm()
        {
            
            InitializeComponent();
        }

        private class BasicComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                ListViewItem a = (ListViewItem)x;
                ListViewItem b = (ListViewItem)y;

                return a.Text.CompareTo(b.Text);
            }
        }

        private AssemblyName[] GetFrameworkList()
        {
            List<AssemblyName> names = new List<AssemblyName>();

            try
            {
                XmlDocument doc = new XmlDocument();

                string fullPath = Path.Combine(GeneralUtils.GetCanapeInstallDirectory(), "frameworkAssemblies.xml");

                doc.Load(fullPath);

                foreach (XmlElement node in doc.SelectNodes("/FileList/File"))
                {
                    string asmName = node.GetAttribute("AssemblyName");
                    string version = node.GetAttribute("Version");
                    string publicKeyToken = node.GetAttribute("PublicKeyToken");

                    AssemblyName name = new AssemblyName();

                    try
                    {
                        name.Name = asmName;
                        name.Version = new Version(version);
                        name.SetPublicKeyToken(GeneralUtils.HexToBinary(publicKeyToken));
                        name.CultureInfo = CultureInfo.InvariantCulture;

                        names.Add(name);
                    }
                    catch (FormatException)
                    { }
                    catch (ArgumentException)
                    { }
                    catch (OverflowException)
                    { }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (XmlException ex)
            {
                MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(InvalidCastException)
            { }

            return names.ToArray();
        }

        private void AddReferenceForm_Load(object sender, EventArgs e)
        {
            foreach (AssemblyDocument doc in CANAPEProject.CurrentProject.GetDocumentsByType(typeof(AssemblyDocument)))
            {
                try
                {
                    AssemblyName name = doc.GetName();

                    if (name != null)
                    {
                        listViewProject.AddName(name);
                    }
                }
                catch (IOException)
                { }
                catch (BadImageFormatException)
                { }
            }

            listViewProject.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewProject.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // Load .NET list from data

            foreach (AssemblyName name in GetFrameworkList())
            {
                listViewDotNet.AddName(name);
            }            

            foreach (string dllFile in Directory.GetFiles(GeneralUtils.GetCanapeInstallDirectory(), "CANAPE*.dll"))
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(dllFile);

                    listViewDotNet.AddName(asm.GetName());
                }
                catch (BadImageFormatException)
                {
                }
                catch (IOException)
                {
                }
            }

            listViewDotNet.ListViewItemSorter = new BasicComparer();
            listViewDotNet.Sort();
            listViewDotNet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewDotNet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void PopulateFromListView(ListView listView)
        {
            List<AssemblyName> names = new List<AssemblyName>();

            foreach (ListViewItem item in listView.SelectedItems)
            {
                names.Add((AssemblyName)item.Tag);
            }

            SelectedNames = names.ToArray();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TabPage page = tabControl.SelectedTab;
            bool valid = false;

            if (page == tabPageDotNet)
            {
                SelectedNames = listViewDotNet.GetNames(true);
                valid = true;
            }
            else if (page == tabPageProject)
            {
                SelectedNames = listViewProject.GetNames(true);
                valid = true;
            }
            else if (page == tabPageManual)
            {
                // Try manual load
                try
                {
                    if (String.IsNullOrWhiteSpace(textBoxAssemblyName.Text))
                    {
                        MessageBox.Show(this, Properties.Resources.AddReferenceForm_MustSpecifyAnAssemblyName, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Assembly asm = Assembly.Load(textBoxAssemblyName.Text);

                        SelectedNames = new AssemblyName[] { asm.GetName() };

                        valid = true;
                    }
                }
                catch (IOException ex)
                { 
                    MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (BadImageFormatException ex)
                { 
                    MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                valid = true;
            }

            if (valid)
            {
                DialogResult = DialogResult.OK;

                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
