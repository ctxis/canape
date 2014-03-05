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


namespace CANAPE.Extension
{
    /// <summary>
    /// Interface to implement a custom inspector
    /// </summary>
    public interface IHexInspector
    {
        /// <summary>
        /// Get an inspection string
        /// </summary>
        /// <param name="data">The data from the hex editor</param>
        /// <param name="pos">The position in the hex editor of a selection</param>
        /// <param name="len">The length of the selection</param>
        /// <returns>The inspection string</returns>
        string Inspect(HexInspectorData data, long pos, long len);

        /// <summary>
        /// Get whether this inspector works on fixed data or a selection
        /// </summary>
        bool IsFixed { get; }

        /// <summary>
        /// Get the display string for this inspector
        /// </summary>
        string DisplayString { get; }
    }
}
