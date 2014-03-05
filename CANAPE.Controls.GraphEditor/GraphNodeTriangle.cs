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
    class GraphNodeTriangle : GraphNode
    {
        public GraphNodeTriangle(Guid id, RectangleF boundary, float z, Color backColor, Color lineColor, Color selectedLineColor, Color textColor, Color hatchedColor)
            : base(id, boundary, z, backColor, lineColor, selectedLineColor, textColor, hatchedColor)
        {
        }

        protected override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            float midPoint = (Boundary.Right + Boundary.Left) / 2.0f;

            path.AddLine(Boundary.Left, Boundary.Bottom, Boundary.Right, Boundary.Bottom);
            path.AddLine(Boundary.Right, Boundary.Bottom, midPoint, Boundary.Top);
            path.AddLine(midPoint, Boundary.Top, Boundary.Left, Boundary.Bottom);

            return path;
        }
    }
}
