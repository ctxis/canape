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
    /// A class to hold a line object
    /// </summary>    
    public class GraphLine : IGraphObject
    {
        const float PEN_WIDTH = 8.0f;

        /// <summary>
        /// The node which sources the line
        /// </summary>
        public GraphNode SourceShape { get; set; }

        /// <summary>
        /// The node which is the destination of the line
        /// </summary>
        public GraphNode DestShape { get; set; }

        /// <summary>
        /// The colour of the line
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// Is this line bi-directional
        /// </summary>
        public bool BiDirection { get; set; }

        /// <summary>
        /// Set the dash style used for the line
        /// </summary>
        public DashStyle LineDashStyle { get; set; }

        /// <summary>
        /// Gets or sets an associated object
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GraphLine()
        {
            LineDashStyle = DashStyle.Solid;
        }

        /// <summary>
        /// 
        /// </summary>
        public RectangleF Boundary
        {
            get
            {
                return GraphUtils.GetLineBoundingBox(SourceShape.Center, DestShape.Center);
            }
        }

        private Pen CreateLinePen(bool selected)
        {
            Pen p = new Pen(LineColor, PEN_WIDTH);
            if (!selected)
            {
                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                if (BiDirection)
                {
                    p.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;                    
                }
                else
                {
                    p.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;                    
                }

                p.DashStyle = LineDashStyle;
            }
            else
            {
                p.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
                p.EndCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;                
            }

            return p;
        }

        private GraphicsPath GetPath(bool selected)
        {
            GraphicsPath path = new GraphicsPath();
            PointF sourceCenter = SourceShape.Center;
            PointF destCenter = DestShape.Center;

            // Add back in stuff for linux/mono support
            if (!selected)
            {                
                if (BiDirection)
                {                    
                    path.AddLine(SourceShape.ClipLine(destCenter), DestShape.ClipLine(sourceCenter));
                }
                else
                {                    
                    path.AddLine(sourceCenter, DestShape.ClipLine(sourceCenter));
                }
            }
            else
            {
                path.AddLine(sourceCenter, destCenter);
            }

            return path;
        }

        private GraphicsPath GetLabelPath(Font f)
        {
            GraphicsPath path = new GraphicsPath();
            StringFormat sformat = new StringFormat();
            sformat.Alignment = StringAlignment.Center;
            sformat.LineAlignment = StringAlignment.Far;
            PointF lineCenter = CalculateLineCenter(SourceShape.Center, DestShape.Center);
            float angle = (float)(CalculateLineAngle(SourceShape.Center, DestShape.Center) / Math.PI) * 180.0f;

            Matrix m = new Matrix();
            m.Translate(lineCenter.X, lineCenter.Y);
            m.Rotate(angle);
            path.AddString(Label, f.FontFamily, (int)f.Style, f.Size * 1.5f, new Point(0, 0), sformat);
            path.Transform(m);

            return path;
        }

        public Rectangle GetClipRegion(Font f, bool selected)
        {
            return new Rectangle();
        }

        /// <summary>
        /// Textual label of this line
        /// </summary>
        public string Label { get; set; }

        private static bool IsRunningOnWindows()
        {
            PlatformID id = Environment.OSVersion.Platform;

            if ((id == PlatformID.Win32NT) || (id == PlatformID.Win32Windows))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private PointF CalculateLineCenter(PointF sourceCenter, PointF destCenter)
        {
            return new PointF(((destCenter.X - sourceCenter.X) / 2.0f) + sourceCenter.X, ((destCenter.Y - sourceCenter.Y) / 2.0f) + sourceCenter.Y);
        }

        private float CalculateLineAngle(PointF sourceCenter, PointF destCenter)
        {
            double angle = Math.Atan2(sourceCenter.Y - destCenter.Y, sourceCenter.X - destCenter.X);

            // If 4th quadrant
            if (angle < (-Math.PI / 2.0))
            {
                angle += Math.PI;
            }
            else if (angle > (Math.PI / 2.0)) // 2nd quadrant
            {
                angle -= Math.PI;
            }            

            return (float)angle;
        }

        /// <summary>
        /// Really crude line end cap so it displays something under linux (where cairo doesn't support any decent line caps)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="arrowLength"></param>
        /// <param name="arrowDegrees"></param>
        private void DrawArrowLineCap(Graphics g, PointF startPoint, PointF endPoint, float arrowLength, float arrowDegrees)
        {
            double lineAngle = Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X) + Math.PI;

            double x1 = endPoint.X + arrowLength * Math.Cos(lineAngle - arrowDegrees);
            double y1 = endPoint.Y + arrowLength * Math.Sin(lineAngle - arrowDegrees);
            double x2 = endPoint.X + arrowLength * Math.Cos(lineAngle + arrowDegrees);
            double y2 = endPoint.Y + arrowLength * Math.Sin(lineAngle + arrowDegrees);

            PointF[] points = new PointF[3];
            points[2] = new PointF((float)x1, (float)y1);
            points[1] = endPoint;
            points[0] = new PointF((float)x2, (float)y2);

            g.FillPolygon(Brushes.Black, points);
        }

        public virtual void Draw(Graphics g, Font f, bool selected)
        {
            if ((SourceShape != null) && (DestShape != null))
            {
                using (Pen p = CreateLinePen(selected))
                {
                    using (GraphicsPath path = GetPath(selected))
                    {
                        g.DrawPath(p, path);
                    }
                }

                if (!String.IsNullOrWhiteSpace(Label))
                {
                    using (GraphicsPath path = GetLabelPath(f))
                    {                        
                        g.FillPath(Brushes.Black, path);
                    }
                }

            }

        }

        const float HIT_SIZE = 10.0f;

        public virtual bool HitTest(PointF hit)
        {
            if ((SourceShape != null) && (DestShape != null))
            {
                RectangleF hitbox = new RectangleF(hit.X - (HIT_SIZE / 2.0f), hit.Y - (HIT_SIZE / 2.0f), HIT_SIZE, HIT_SIZE);
                PointF intersect;

                return GraphUtils.IntersectRectangle(hitbox, SourceShape.Center, DestShape.Center, out intersect);
            }
            else
            {
                return false;
            }
        }
    }
}
