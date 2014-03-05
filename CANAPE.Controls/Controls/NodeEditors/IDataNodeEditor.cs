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

using System.Drawing;
using CANAPE.DataFrames;

namespace CANAPE.Controls.NodeEditors
{
    /// <summary>
    /// Interface to define a data frame editor control
    /// </summary>
    public interface IDataNodeEditor
    {
        /// <summary>
        /// Sets a frame for editing
        /// </summary>
        /// <param name="node">The node to edit</param>
        /// <param name="selected">Selected node (if applicable)</param>
        /// <param name="color">The associated colour of the frame</param>
        /// <param name="readOnly">Whether the node is read only</param>
        void SetNode(DataNode node, DataNode selected, Color color, bool readOnly);
    }
}
