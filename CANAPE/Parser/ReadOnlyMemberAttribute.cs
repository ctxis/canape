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
    /// An attribute which indicates a member is readonly (advisory only)
    /// </summary>
    [Serializable, AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class ReadOnlyMemberAttribute : Attribute
    {
        /// <summary>
        /// Indicates whether the member is readonly
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="readOnly">Indicates whether the member is read only</param>
        public ReadOnlyMemberAttribute(bool readOnly)
        {
            ReadOnly = readOnly;
        }
    }
}
