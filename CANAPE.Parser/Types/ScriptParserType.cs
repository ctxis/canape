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
using System.CodeDom;
using System.ComponentModel;
using CANAPE.Scripting;

namespace CANAPE.Parser
{
    [Serializable]
    public class ScriptParserType : ParserType
    {
        [Serializable]
        private class DummyScriptContainer : ScriptContainer
        {
            ScriptParserType _parentType;

            public DummyScriptContainer(ScriptParserType parentType, string engine, string script) 
                : base(engine, Guid.NewGuid(), script, false)
            {
                _parentType = parentType;
            }

            public override string Script
            {
                get
                {
                    return base.Script;
                }
                set
                {
                    if (base.Script != value)
                    {
                        base.Script = value;
                        if (_parentType != null)
                        {
                            _parentType.OnDirty();
                        }
                    }
                }
            }
        }

        private ScriptContainer _script;

        public ScriptParserType(string name, string engine, string script)
            : base(name)
        {
            _script = new DummyScriptContainer(this, engine, script);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScriptContainer Script
        {
            get { return _script; }            
        }

        public override int GetSize()
        {            
            return -1;
        }

        public override System.CodeDom.CodeTypeDeclaration GetCodeType()
        {
            CodeTypeDeclaration type = new CodeTypeDeclaration(Name);

            type.IsClass = true;            
            type.Attributes = MemberAttributes.Final | MemberAttributes.Static;
            type.BaseTypes.Add(typeof(PySnippet));

            CodeConstructor defaultConstructor = new CodeConstructor();
            defaultConstructor.Attributes = MemberAttributes.Public;
            defaultConstructor.BaseConstructorArgs.Add(CodeGen.GetPrimitive(_script.Script));
            type.Members.Add(defaultConstructor);            

            return type;
        }
    }
}
