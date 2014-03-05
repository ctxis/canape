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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CANAPE.Controls.GraphEditor
{
    /// <summary>
    /// Class to define a rectangular node
    /// </summary>    
    internal sealed class GraphNodeRoundedRectangle : GraphNode
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boundary"></param>
        /// <param name="backColor"></param>
        /// <param name="lineColor"></param>
        /// <param name="textColor"></param>
        /// <param name="hatchedColor"></param>
        public GraphNodeRoundedRectangle(Guid id, RectangleF boundary, float z, Color backColor, Color lineColor, Color selectedLineColor, Color textColor, Color hatchedColor)
            : base(id, boundary, z, backColor, lineColor, selectedLineColor, textColor, hatchedColor)
        {
        }

        protected override GraphicsPath GetPath()
        {
            RectangleF boundary = Boundary;
            GraphicsPath path = new GraphicsPath();
            float radius = (float)Math.Floor(boundary.Height * 0.2);
            float x = boundary.Left;
            float y = boundary.Top;

            radius = Math.Max(1.0f, radius);

            path.AddLine(x + radius, y, x + boundary.Width - (radius * 2), y);
            path.AddArc(x + boundary.Width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            path.AddLine(x + boundary.Width, y + radius, x + boundary.Width, y + boundary.Height - (radius * 2));
            path.AddArc(x + boundary.Width - (radius * 2), y + boundary.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
            path.AddLine(x + boundary.Width - (radius * 2), y + boundary.Height, x + radius, y + boundary.Height);
            path.AddArc(x, y + boundary.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
            path.AddLine(x, y + boundary.Height - (radius * 2), x, y + radius);
            path.AddArc(x, y, radius * 2, radius * 2, 180, 90);

            return path;
        }
    }
}
