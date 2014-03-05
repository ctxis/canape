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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CANAPE.DataFrames;

namespace CANAPE.Controls.NodeEditors
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FrameEditorControl : UserControl
    {        
        private DataFrame _frame;

        /// <summary>
        /// 
        /// </summary>
        public FrameEditorControl()
        {            
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ReadOnly
        {
            get;
            set; 
        }

        /// <summary>
        /// Set the frame to edit
        /// </summary>
        /// <param name="frame">The data frame</param>        
        /// <param name="selector">Root selection path</param>
        /// <param name="color">The colour to display the frame in (if applicable)</param>
        public void SetFrame(DataFrame frame, string selector, Color color)
        {
            DataNode node = null;
            DataNode curr = frame.Current;

            _frame = frame;
            if (!String.IsNullOrWhiteSpace(selector))
            {
                node = _frame.SelectSingleNode(selector);
            }

            if (node == null)
            {
                node = _frame.Root;
            }

            dataNodeEditorControl.SetNode(node, curr, color, ReadOnly);
        }
    }
}
