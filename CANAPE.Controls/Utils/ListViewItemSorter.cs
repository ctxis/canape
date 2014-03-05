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
using System.Collections;
using System.Windows.Forms;

namespace CANAPE.Utils
{
    internal class ListViewItemSorter : IComparer
    {
        internal ListViewItemSorter(int column, bool reverse)
        {
            Column = column;
            Reverse = reverse;
        }

        public int Column { get; set; }
        public bool Reverse { get; set; }

        public int Compare(object x, object y)
        {
            ListViewItem left = (ListViewItem)x;
            ListViewItem right = (ListViewItem)y;

            if (Reverse)
            {
                return String.Compare(right.SubItems[Column].Text, left.SubItems[Column].Text, 
                    false, GeneralUtils.GetCurrentCulture());
            }
            else
            {
                return String.Compare(left.SubItems[Column].Text, right.SubItems[Column].Text, 
                    false, GeneralUtils.GetCurrentCulture());
            }
        }
    }
}
