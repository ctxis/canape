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
    internal sealed class GraphNodeRhombus : GraphNode
    {
        public GraphNodeRhombus(Guid id, RectangleF boundary, float z, Color backColor, Color lineColor, Color selectedLineColor, Color textColor, Color hatchedColor) 
            : base(id, boundary, z, backColor, lineColor, selectedLineColor, textColor, hatchedColor)
        {
        }

        protected override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            PointF[] points = new PointF[4];

            points[0] = new PointF(Boundary.Left + Boundary.Width / 2.0f, Boundary.Top);
            points[1] = new PointF(Boundary.Right, Boundary.Top + Boundary.Height / 2.0f);
            points[2] = new PointF(Boundary.Left + Boundary.Width / 2.0f, Boundary.Bottom);
            points[3] = new PointF(Boundary.Left, Boundary.Top + Boundary.Height / 2.0f);

            path.AddPolygon(points);            

            return path;
        }
    }
}
