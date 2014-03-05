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


namespace CANAPE.Scripting
{
    /// <summary>
    /// Simple class which holds a name value pair which is reflected in the data tree
    /// </summary>
    public sealed class NameValueDataPair
    {
        /// <summary>
        /// The name of the pair
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The value of the pair
        /// </summary>
        public dynamic Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the pair</param>
        /// <param name="value">The value of the pair</param>
        public NameValueDataPair(string name, dynamic value)
        {
            Name = name;
            Value = value;
        }
    }
}
