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
using System.Windows.Forms;
using CANAPE.DataFrames;

namespace CANAPE.Controls.NodeEditors
{
    /// <summary>
    ///
    /// </summary>
    [DataNodeEditor(DataNodeClasses.BINARY_NODE_CLASS)]
    public partial class BinaryDataNodeEditorControl : UserControl, IDataNodeEditor
    {
        DataNode _node;
        Color _color;
        bool _readOnly; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="readOnly"></param>
        public BinaryDataNodeEditorControl()
        {
            
            InitializeComponent();
        }

        private void SetupFrame()
        {
            if (_node != null)
            {
                binaryDataEditorControl.PacketColor = _color;
                ByteArrayDataValue byteArray = _node as ByteArrayDataValue;

                if (byteArray != null)
                {
                    binaryDataEditorControl.Data = (byte[])byteArray.Value;
                }
                else
                {
                    binaryDataEditorControl.Data = _node.ToArray();
                }

                binaryDataEditorControl.ReadOnly = _readOnly;
            }
        }

        /// <summary>
        /// Set the ndoe
        /// </summary>
        /// <param name="node"></param>
        /// <param name="selected"></param>
        /// <param name="color"></param>
        /// <param name="readOnly"></param>
        public void SetNode(DataNode node, DataNode selected, Color color, bool readOnly)
        {
            _node = node;
            _color = color;
            _readOnly = readOnly;

            SetupFrame();
        }

        private void BinaryDataFrameViewerControl_Load(object sender, EventArgs e)
        {
            SetupFrame();
        }

        private void binaryDataEditorControl_DataChanged(object sender, EventArgs e)
        {
            if (_node != null)
            {
                ByteArrayDataValue byteArray = _node as ByteArrayDataValue;

                if (byteArray != null)
                {
                    byteArray.Value = binaryDataEditorControl.Data;
                }
                else
                {
                    _node = _node.ReplaceNode(binaryDataEditorControl.Data);
                }
            }
        }
    }
}
