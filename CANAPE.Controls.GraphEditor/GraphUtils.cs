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
    internal static class GraphUtils
    {
        /// <summary>
        /// Square a value
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static float Square(float f)
        {
            return f * f;
        }

        public static float CalculateDistance(PointF a, PointF b)
        {
            return Square(a.X - b.X) + Square(a.Y - b.Y);            
        }

        private static bool Within(float a1, float a2, float p)
        {
            if (a1 < a2)
            {
                if ((p >= a1) && (p <= a2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if ((p >= a2) && (p <= a1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IntersectLine(PointF l1, PointF l2, PointF l3, PointF l4, out PointF res)
        {
            float x1 = l1.X;
            float y1 = l1.Y;
            float x2 = l2.X;
            float y2 = l2.Y;
            float x3 = l3.X;
            float y3 = l3.Y;
            float x4 = l4.X;
            float y4 = l4.Y;

            float px = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) /
                ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

            float py = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) /
               ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

            res = new PointF(px, py);

            if (Within(l1.X, l2.X, res.X) && Within(l1.Y, l2.Y, res.Y) && Within(l3.X, l4.X, res.X) && Within(l3.Y, l4.Y, res.Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Intersect a line with a rectangle, returns the first intersecting point 
        /// </summary>
        /// <param name="boundary">The rectangle boundary</param>
        /// <param name="sourcePoint">The source point of the line</param>
        /// <param name="destPoint">The destination point of the line</param>
        /// <param name="intersect">Parameter to store the intersection point if found</param>
        /// <returns>True if the line intersected at least one side of the rectangle</returns>
        public static bool IntersectRectangle(RectangleF boundary, PointF sourcePoint, PointF destPoint, out PointF intersect)
        {            
            if (IntersectLine(sourcePoint, destPoint, 
                new PointF(boundary.Left, boundary.Top), 
                new PointF(boundary.Left, boundary.Bottom), out intersect))
            {
                return true;
            }

            if (GraphUtils.IntersectLine(sourcePoint, destPoint, 
                new PointF(boundary.Left, boundary.Top), 
                new PointF(boundary.Right, boundary.Top), out intersect))
            {
                return true;
            }

            if (GraphUtils.IntersectLine(sourcePoint, destPoint, 
                new PointF(boundary.Left, boundary.Bottom), 
                new PointF(boundary.Right, boundary.Bottom), out intersect))
            {
                return true;
            }

            if (GraphUtils.IntersectLine(sourcePoint, destPoint, 
                new PointF(boundary.Right, boundary.Top), 
                new PointF(boundary.Right, boundary.Bottom), out intersect))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the bounding box for a line
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static RectangleF GetLineBoundingBox(PointF a, PointF b)
        {
            float left;
            float top;
            float width;
            float height;

            if (a.X < b.X)
            {
                left = a.X;
                width = b.X - a.X;
            }
            else
            {
                left = b.X;
                width = a.X - b.X;
            }

            if (a.Y < b.Y)
            {
                top = a.Y;
                height = b.Y - a.Y;
            }
            else
            {
                top = b.Y;
                height = a.Y - b.Y;
            }

            return new RectangleF(top, left, width, height);
        }


    }
}
