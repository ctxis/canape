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


namespace CANAPE.DataFrames
{
    /// <summary>
    /// Static class to contain constants for default data node classes
    /// </summary>
    public static class DataNodeClasses
    {
        /// <summary>
        /// Guid class of a binary node
        /// </summary>
        public const string BINARY_NODE_CLASS = "{54FE747E-2C11-4455-B729-3902570CB849}";

        /// <summary>
        /// Guid class for an enum value
        /// </summary>
        public const string ENUM_NODE_CLASS = "{88D0B4F3-29F1-4DE4-ABF4-000000000001}";

        /// <summary>
        /// Guid class for an enum flags value
        /// </summary>
        public const string FLAGS_ENUM_NODE_CLASS = "{88D0B4F3-29F1-4DE4-ABF4-000000000000}";

        /// <summary>
        /// Guid class for a string value
        /// </summary>
        public const string STRING_NODE_CLASS = "{021F13DD-921C-4775-9CB4-AAD0FA01CB78}";
    }
}
