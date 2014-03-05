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

namespace CANAPE.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class ListViewExtension : ListView
    {
        public bool AutoScrollList { get; set; }

        public ListViewExtension()
        {
            //Activate double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            //Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }

        public void ScrollToEnd()
        {
            const int WM_VSCROLL = 0x0115;
            IntPtr SB_BOTTOM = (IntPtr)7;

            try
            {
                Message msg = Message.Create(
                       Handle,
                       WM_VSCROLL,
                       SB_BOTTOM,
                       IntPtr.Zero);

                WndProc(ref msg);
            }
            catch (ObjectDisposedException)
            {

            }
        }
        
        public void AddItem(ListViewItem item)
        {           
            Items.Add(item);

            if (AutoScrollList)
            {
                ScrollToEnd();
            }
        }
    }
}
