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
using CANAPE.Utils;

namespace CANAPE.Controls
{
    public partial class PropertyBagViewerControl : UserControl
    {
        public void AddBag(string baseName, PropertyBag properties)
        {
            foreach (KeyValuePair<string, dynamic> pair in properties)
            {
                ListViewItem item = new ListViewItem(String.Format("{0}{1}", baseName, pair.Key));
                item.SubItems.Add(pair.Value.ToString());
                listViewProperties.Items.Add(item);
            }

            foreach (PropertyBag bag in properties.Bags)
            {
                AddBag(String.Format("{0}{1}.", baseName, bag.Name), bag);
            }
        }

        public void UpdateProperties(PropertyBag properties)
        {
            listViewProperties.SuspendLayout();
            listViewProperties.Items.Clear();

            if (properties != null)
            {
                foreach (KeyValuePair<string, dynamic> pair in properties)
                {
                    ListViewItem item = new ListViewItem(pair.Key);
                    item.SubItems.Add(pair.Value.ToString());
                    listViewProperties.Items.Add(item);
                }
            }

            listViewProperties.ResumeLayout();
        }

        public PropertyBagViewerControl()
        {
            
            InitializeComponent();
        }
    }
}
