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
using System.Windows.Forms;
using CANAPE.Documents.Data;
using CANAPE.Parser;
using CANAPE.Scripting;

namespace CANAPE.Controls.DocumentEditors.ParserEditors
{
    public partial class ScriptParserTypeEditorControl : UserControl
    {
        ScriptParserType _currentType;
        Dictionary<ScriptParserType, Control> _controls;
        Control _visibleControl;

        private class DummyScriptDocument : ScriptDocument
        {
            public DummyScriptDocument(ScriptContainer script)
            {
                _script = script;
            }
        }

        public ScriptParserTypeEditorControl()
        {
            InitializeComponent();

            _controls = new Dictionary<ScriptParserType, Control>();
            Disposed += ScriptParserTypeEditorControl_Disposed;
        }

        void ScriptParserTypeEditorControl_Disposed(object sender, EventArgs e)
        {
            foreach (ScriptDocumentControl control in _controls.Values)
            {
                control.Dispose();
            }
        }

        private void SetupType()
        {
            if (_visibleControl != null)
            {
                _visibleControl.Visible = false;
            }

            if (!_controls.ContainsKey(_currentType))
            {
                Control c = new ScriptDocumentControl(new DummyScriptDocument(_currentType.Script), true);
                _controls.Add(_currentType, c);                
                Controls.Add(c);
                c.Dock = DockStyle.Fill;
            }

            _visibleControl = _controls[_currentType];
            _visibleControl.Visible = true;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScriptParserType CurrentType
        {
            get { return _currentType; }
            set
            {
                _currentType = value;
                SetupType();
            }
        }
    }
}
