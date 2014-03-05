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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CANAPE.Controls
{
    /// <summary>
    /// Simple tab page implementation to break resizing into smaller chunks
    /// </summary>
    public class ResizingTabPage : TabPage
    {
        private const int WM_WINDOWPOSCHANGING = 70;
        private const int WM_SETREDRAW = 0xB;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_SHOWWINDOW = 0x40;

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

        [DllImport("User32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter,
        int x, int y, int cx, int cy, int flags);

        [StructLayout(LayoutKind.Sequential)]
        private class WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        };

        private void ResizeChild(WINDOWPOS wpos)
        {            
            if (this.Controls.Count == 1)
            {
                Control child = this.Controls[0];

                // stop window redraw to avoid flicker
                SendMessage(new HandleRef(child, child.Handle), WM_SETREDRAW, 0, 0);

                // start a new stack of SetWindowPos calls
                SetWindowPos(new HandleRef(child, child.Handle), new HandleRef(null, IntPtr.Zero),
                0, 0, wpos.cx, wpos.cy, SWP_NOACTIVATE | SWP_NOZORDER);

                // turn window repainting back on 
                SendMessage(new HandleRef(child, child.Handle), WM_SETREDRAW, 1, 0);

                // send repaint message to this control and its children
                this.Invalidate(true);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_WINDOWPOSCHANGING)
            {
                WINDOWPOS wpos = (WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(WINDOWPOS));

                //System.Diagnostics.Trace.WriteLine(String.Format("WM_WINDOWPOSCHANGING received by {0} flags {1:X}", this.Name, wpos.flags));               

                if (((wpos.flags & (SWP_NOZORDER | SWP_NOACTIVATE)) == (SWP_NOZORDER | SWP_NOACTIVATE)) &&
                ((wpos.flags & ~(SWP_SHOWWINDOW | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE)) == 0))
                {
                    if ((wpos.cx != this.Width) || (wpos.cy != this.Height))
                    {
                        BeginInvoke(new Action<WINDOWPOS>(ResizeChild), new object[] { wpos });
                        return;
                    }
                }
            }

            base.WndProc(ref m);
        }
    }
}
