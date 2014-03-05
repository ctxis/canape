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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CANAPE.Controls.GraphEditor
{
    /// <summary>
    /// A link line, which just represents an annotation, is not a reflection of anything selectable
    /// </summary>
    internal class GraphLinkLine
    {
        const float PEN_WIDTH = 1.0f;

        /// <summary>
        /// The node which sources the line
        /// </summary>
        internal GraphNode SourceShape { get; set; }

        /// <summary>
        /// The node which is the destination of the line
        /// </summary>
        internal GraphNode DestShape { get; set; }

        /// <summary>
        /// The color of the line
        /// </summary>
        internal Color LineColor { get; set; }

        /// <summary>
        /// The width of the line
        /// </summary>
        internal float LineWidth { get; set; }

        internal DashStyle LineDashStyle { get; set; }

        public GraphLinkLine()
        {
            LineWidth = PEN_WIDTH;
            LineColor = Color.Black;
            LineDashStyle = DashStyle.Dash;
        }

        private Pen CreateLinePen()
        {
            Pen p = new Pen(LineColor, LineWidth);
            p.DashStyle = LineDashStyle;

            return p;
        }

        private GraphicsPath GetPath()
        {
            GraphicsPath path = new GraphicsPath();
            PointF sourceCenter = SourceShape.Center;
            PointF destCenter = DestShape.Center;

            path.AddLine(sourceCenter, destCenter);

            return path;
        }

        internal void Draw(Graphics g)
        {
            if ((SourceShape != null) && (DestShape != null))
            {
                using (Pen p = CreateLinePen())
                {
                    using (GraphicsPath path = GetPath())
                    {
                        g.DrawPath(p, path);
                    }
                }
            }
        }

        internal bool Matches(GraphNode sourceShape, GraphNode destShape)
        {
            return ((sourceShape == SourceShape) && (destShape == DestShape)) ||
                ((sourceShape == DestShape) && (destShape == SourceShape));
        }
    }
}
