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

namespace CANAPE.Parser
{
    /// <summary>
    /// An attribute which reflects the format string used for a data value
    /// </summary>
    [Serializable, AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FormatStringAttribute : Attribute
    {
        /// <summary>
        /// The format string
        /// </summary>
        public string FormatString { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="formatString">The format string</param>
        public FormatStringAttribute(string formatString)
        {
            FormatString = formatString;
        }
    }
}
