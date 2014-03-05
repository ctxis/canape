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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace CANAPE.Controls
{
    public partial class AssemblyNameListView : ListView
    {
        public AssemblyNameListView()
        {
            
            InitializeComponent();
        }

        public void AddName(AssemblyName name)
        {
            ListViewItem item = new ListViewItem(name.Name);

            item.SubItems.Add(name.Version.ToString());

            StringBuilder builder = new StringBuilder();

            byte[] publicKey = name.GetPublicKeyToken();

            if (publicKey == null)
            {
                item.SubItems.Add("null");
            }
            else
            {
                foreach (byte b in publicKey)
                {
                    builder.AppendFormat("{0:X02}", b);
                }
                item.SubItems.Add(builder.ToString());
            }

            item.Tag = name;

            Items.Add(item);
        }

        public AssemblyName[] GetNames(bool selected)
        {
            IEnumerable<ListViewItem> items = selected ? SelectedItems.Cast<ListViewItem>() : Items.Cast<ListViewItem>();
            Dictionary<string, AssemblyName> names = new Dictionary<string, AssemblyName>();

            foreach (ListViewItem item in items)
            {
                AssemblyName name = (AssemblyName)item.Tag;

                names[name.FullName] = name;
            }

            return names.Values.ToArray();
        }
    }
}
