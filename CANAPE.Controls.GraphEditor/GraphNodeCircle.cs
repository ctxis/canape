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
    /// 
    /// </summary>
    internal sealed class GraphNodeCircle : GraphNode
    {
        public GraphNodeCircle(Guid id, RectangleF boundary, float z, Color backColor, Color lineColor, Color selectedLineColor, Color textColor, Color hatchedColor) 
            : base(id, boundary, z, backColor, lineColor, selectedLineColor, textColor, hatchedColor)
        {
        }

        /// <summary>
        /// Clip the line destination to be outside of the shape if possible
        /// </summary>
        /// <param name="sourcePoint">The source point of the line</param>
        /// <returns>The clipped line position</returns>
        internal override PointF ClipLine(PointF sourcePoint)
        {            
            float relX = Center.X - sourcePoint.X;
            float relY = Center.Y - sourcePoint.Y;
            float length = (float)Math.Sqrt(GraphUtils.Square(relX) + GraphUtils.Square(relY));
            float newLength = length - Boundary.Width / 2.0f;

            // If we have a length which is less that 0 then return just the center point
            if (newLength < 0)
            {
                return Center;
            }

            return new PointF(sourcePoint.X + ((relX * newLength) / length), sourcePoint.Y + ((relY * newLength) / length));
        }

        /// <summary>
        /// Determine if this point hits the object
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        public override bool HitTest(PointF hit)
        {            
            PointF center = Center;
            float radius2 = GraphUtils.Square(Boundary.Width / 2);
            float distance2 = GraphUtils.Square(hit.X - center.X) + GraphUtils.Square(hit.Y - center.Y);

            return distance2 < radius2;
        }

        protected override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            
            path.AddEllipse(Boundary);

            return path;
        }
    }
}
