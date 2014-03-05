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

namespace CANAPE.Scripting
{
    /// <summary>
    /// Class to hold a generic script error
    /// </summary>
    [Serializable]
    public class ScriptError
    {
        /// <summary>
        /// Error description
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Error severity
        /// </summary>
        public string Severity { get; private set; }
        /// <summary>
        /// Error line
        /// </summary>
        public int Line { get; private set; }
        /// <summary>
        /// Error column
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="description">Error description</param>
        /// <param name="severity">Error severity</param>
        /// <param name="line">Error line</param>
        /// <param name="column">Error column</param>
        public ScriptError(string description, string severity, int line, int column)
        {
            Description = description;
            Severity = severity;
            Line = line;
            Column = column;
        }
    }
}
