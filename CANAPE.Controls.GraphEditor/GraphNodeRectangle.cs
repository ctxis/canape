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
    internal sealed class GraphNodeRectangle : GraphNode
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
        public GraphNodeRectangle(Guid id, RectangleF boundary, float z, Color backColor, Color lineColor, Color selectedLineColor, Color textColor, Color hatchedColor)
            : base(id, boundary, z, backColor, selectedLineColor, lineColor, textColor, hatchedColor)
        {
        }

        /// <summary>
        /// Clip a line to the shape
        /// </summary>
        /// <param name="sourcePoint">The source of the line</param>
        /// <returns>The point which clips at the boundary</returns>
        internal override PointF ClipLine(PointF sourcePoint)
        {            
            PointF intersect;

            if (GraphUtils.IntersectLine(sourcePoint, this.Center, 
                new PointF(Boundary.Left, Boundary.Top), 
                new PointF(Boundary.Left, Boundary.Bottom), out intersect))
            {
                return intersect;
            }

            if (GraphUtils.IntersectLine(sourcePoint, this.Center, 
                new PointF(Boundary.Left, Boundary.Top), 
                new PointF(Boundary.Right, Boundary.Top), out intersect))
            {
                return intersect;
            }

            if (GraphUtils.IntersectLine(sourcePoint, this.Center, 
                new PointF(Boundary.Left, Boundary.Bottom), 
                new PointF(Boundary.Right, Boundary.Bottom), out intersect))
            {
                return intersect;
            }

            if (GraphUtils.IntersectLine(sourcePoint, this.Center, 
                new PointF(Boundary.Right, Boundary.Top), 
                new PointF(Boundary.Right, Boundary.Bottom), out intersect))
            {
                return intersect;
            }

            return Center;
        }

        public override bool HitTest(PointF hit)
        {
            if ((hit.X >= Boundary.Left) && (hit.X < Boundary.Right) && (hit.Y >= Boundary.Top) && (hit.Y < Boundary.Bottom))
            {
                return true;
            }

            return false;
        }        

        protected override GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();

            path.AddRectangle(Boundary);

            return path;
        }     
    }
}
