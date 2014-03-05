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

using System.Collections.Generic;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace CANAPE.Scripting.Dynamic
{
    internal class ScriptErrorListener : ErrorListener
    {
        public List<ScriptError> Errors { get; private set; }

        public ScriptErrorListener()
        {
            Errors = new List<ScriptError>();
        }

        public override void ErrorReported(ScriptSource source, string message, SourceSpan span, int errorCode, Severity severity)
        {
            ScriptError arg = new ScriptError(message, severity.ToString(), span.Start.Line, span.Start.Column);

            Errors.Add(arg);
        }
    }
}
