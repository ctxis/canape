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
using System.Drawing;
using System.Windows.Forms;
using CANAPE.DataFrames;

namespace CANAPE.Controls.NodeEditors
{
    public partial class DataNodeEditorControl : UserControl, IDataNodeEditor
    {
        DataNode _node;
        DataNode _selected;
        Color _color;
        bool _readOnly;
        bool _isLoaded;   

        Dictionary<Guid, IDataNodeEditor> _editors;
        Control _currEditor;
        Control _keyEditor;
        Control _valueEditor;

        public DataNodeEditorControl()
        {            
            _editors = new Dictionary<Guid, IDataNodeEditor>();
            
            InitializeComponent();
        }        

        private void AddEditor(Guid guid, Control editor)
        {
            _editors[guid] = (IDataNodeEditor)editor;
            editor.Visible = false;
            editor.Dock = DockStyle.Fill;
            Controls.Add(editor);
        }

        private void SetupNode()
        {
            if (_node != null)
            {
                IDataNodeEditor editor = null;
                Guid displayClass = _node.GetDisplayClass();

                if (_editors.ContainsKey(displayClass))
                {
                    editor = _editors[displayClass];
                }
                else if(DataNodeEditorManager.HasEditor(_node))
                {
                    // Try and create an editor
                    editor = DataNodeEditorManager.GetEditor(_node);
                    AddEditor(displayClass, (Control)editor);
                }
                else if (_node is DataKey)
                {
                    editor = (IDataNodeEditor)_keyEditor;
                }
                else
                {
                    // Add a default editor for a value 
                    editor = (IDataNodeEditor)_valueEditor;
                }

                if (_currEditor != null)
                {
                    if (_currEditor.GetType() != editor.GetType())
                    {
                        _currEditor.Visible = false;
                    }
                }

                editor.SetNode(_node, _selected, _color, _readOnly);
                _currEditor = (Control)editor;
                _currEditor.Visible = true;
            }
        }

        public void SetNode(DataNode node, DataNode selected, Color color, bool readOnly)
        {
            _node = node;
            _selected = selected;
            _color = color;
            _readOnly = readOnly;

            if (_isLoaded)
            {
                SetupNode();
            }
        }

        public bool ShowTreeEditor { get; set; }

        private void DataNodeEditorControl_Load(object sender, EventArgs e)
        {
            _isLoaded = true;
            if (!DesignMode)
            {
                lblMessage.Visible = false;

                if (ShowTreeEditor)
                {
                    _keyEditor = new TreeDataKeyEditorControl();
                }
                else
                {
                    _keyEditor = new DataKeyEditorControl();
                }

                _keyEditor.Visible = false;
                _keyEditor.Dock = DockStyle.Fill;
                Controls.Add(_keyEditor);

                _valueEditor = new DataValueEditorControl();
                _valueEditor.Visible = false;
                _valueEditor.Dock = DockStyle.Fill;
                Controls.Add(_valueEditor);

                SetupNode();
            }
        }
    }
}
