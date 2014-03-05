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

namespace CANAPE.Controls.GraphEditor
{
    /// <summary>
    /// Generic interface for objects on the graph (either nodes or lines)
    /// </summary>
    public interface IGraphObject
    {        
        /// <summary>
        /// Determine if a point in the display hits the object
        /// </summary>
        /// <param name="hit">The point to test</param>
        /// <returns>True if the point is within the shape</returns>
        bool HitTest(PointF hit);

        /// <summary>
        /// 
        /// </summary>
        RectangleF Boundary { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        Rectangle GetClipRegion(Font f, bool selected);

        /// <summary>
        /// The object's label
        /// </summary>
        string Label { get; set; }
    }    
}
