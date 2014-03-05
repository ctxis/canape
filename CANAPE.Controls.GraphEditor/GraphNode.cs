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
    /// Shape of node on graph
    /// </summary>
    public enum GraphNodeShape
    {
        Ellipse,
        Rectangle,
        RoundedRectangle,
        Triangle,
        Rhombus,
    }

    /// <summary>
    /// Base class for a graph node
    /// </summary>
    public abstract class GraphNode : IGraphObject
    {
        /// <summary>
        /// Get the boundary of the node
        /// </summary>
        public RectangleF Boundary { get; private set; }

        /// <summary>
        /// The current Z level of the node
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Get the name of the node
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Get the background color of the node
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the gradiant color
        /// </summary>
        public Color GradientColor { get; set; }

        /// <summary>
        /// Get the line color of the node
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// Get the selected line color of the node
        /// </summary>
        public Color SelectedLineColor { get; set; }

        /// <summary>
        /// Get or set the text color of the node
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// Color to draw if the node is hatched
        /// </summary>
        public Color HatchedColor { get; set; }

        /// <summary>
        /// Get or set a user defined object for the node
        /// </summary>
        public Object Tag { get; set; }

        /// <summary>
        /// Enable or disable hatching
        /// </summary>
        public bool Hatched { get; set; }

        /// <summary>
        /// Get the unique ID of the node
        /// </summary>
        public Guid Id { get; private set; }        
        
        /// <summary>
        /// Get the central point of the node
        /// </summary>
        public PointF Center
        {
            get
            {
                float cx = (Boundary.Left + Boundary.Right) / 2.0f;
                float cy = (Boundary.Top + Boundary.Bottom) / 2.0f;

                return new PointF(cx, cy);
            }
        }

        /// <summary>
        /// Get the graphics path to draw
        /// </summary>
        /// <returns></returns>
        protected abstract GraphicsPath GetPath();

        public Rectangle GetClipRegion(Font f, bool selected)
        {
            return new Rectangle();
        }

        /// <summary>
        /// Draw the node
        /// </summary>
        /// <param name="g">The graphics to use for the drawing</param>
        /// <param name="f"></param>
        /// <param name="selected"></param>
        internal virtual void Draw(Graphics g, Font f, bool selected)
        {
            Pen p = null;
            Brush b = null;
            Brush textBrush = null;
            GraphicsPath path = GetPath();

            try
            {                
                if (!selected)
                {
                    p = new Pen(LineColor);
                }
                else
                {
                    p = new Pen(SelectedLineColor, 3.0f);
                }

                if (Hatched)
                {
                    b = new HatchBrush(HatchStyle.Percent40, HatchedColor, BackColor);
                }
                else
                {
                    if (BackColor == GradientColor)
                    {
                        b = new SolidBrush(BackColor);
                    }
                    else
                    {
                        b = new LinearGradientBrush(Boundary, BackColor, GradientColor, LinearGradientMode.Vertical);
                    }
                }

                textBrush = new SolidBrush(TextColor);

                g.FillPath(b, path);
                g.DrawPath(p, path);                
                
                StringFormat sformat = new StringFormat();
                sformat.Alignment = StringAlignment.Center;
                sformat.LineAlignment = StringAlignment.Center;
                g.DrawString(Label, f, textBrush, Boundary, sformat);
            }
            finally
            {
                if (p != null)
                {
                    p.Dispose();
                }

                if (b != null)
                {
                    b.Dispose();
                }

                if (textBrush != null)
                {
                    textBrush.Dispose();
                }

                if (path != null)
                {
                    path.Dispose();
                }
            }
        }

        /// <summary>
        /// Draw a drop shadow
        /// </summary>
        /// <param name="g">The graphics object to use</param>
        /// <param name="offx">Offset from the current position to draw the shadow</param>
        /// <param name="offy"></param>
        public void DrawDropShadow(Graphics g, float offx, float offy)
        {            
            PathGradientBrush b = null;            
            GraphicsPath path = GetPath();

            try
            {
                Matrix m = new Matrix();
                m.Translate(offx, offy);
                path.Transform(m);
                b = new PathGradientBrush(path);
                
                b.WrapMode = WrapMode.Clamp;
                ColorBlend colors = new ColorBlend(3);

                colors.Colors = new Color[] {Color.Transparent, Color.FromArgb(180, Color.DimGray),  Color.FromArgb(180, Color.DimGray)};
                colors.Positions = new float[] { 0.0f, 0.1f, 1.0f };
                b.InterpolationColors = colors;
                g.FillPath(b, path);                
            }
            finally
            {
                if (path != null)
                {
                    path.Dispose();
                }

                if (b != null)
                {
                    b.Dispose();
                }                
            }
        }
        
        /// <summary>
        /// Move the node to a new location
        /// </summary>
        /// <param name="delta">The distance to move the point</param>
        /// <param name="containerRect">The boundary of the container control which this should be clipped to</param>
        internal void MoveLocation(SizeF delta, RectangleF containerRect)
        {
            float newX = Boundary.X + delta.Width;
            float newY = Boundary.Y + delta.Height;            

            if (newX  < containerRect.X)
            {
                newX = containerRect.X;
            }

            if ((newX + Boundary.Width) >= (containerRect.X + containerRect.Width))
            {
                newX = containerRect.X + containerRect.Width - Boundary.Width;
            }

            if (newY < containerRect.Y)
            {
                newY = containerRect.Y;
            }

            if ((newY + Boundary.Height) >= (containerRect.Y + containerRect.Height))
            {
                newY = containerRect.Y + containerRect.Height - Boundary.Height;
            }

            Boundary = new RectangleF(newX, newY, Boundary.Width, Boundary.Height);            
        }
        
        /// <summary>
        /// Determine if the point lies within the path, can be overriden if a more efficient mechanism
        /// is available
        /// </summary>
        /// <param name="hit">The point to test</param>
        /// <returns>True if the point lies within the shape</returns>
        public virtual bool HitTest(PointF hit)
        {
            using (GraphicsPath path = GetPath())
            {
                return path.IsVisible(hit);
            }
        }

        /// <summary>
        /// Clip the line, defaults to flattening the path and intersecting every line. 
        /// If a more efficient method is available it can be overridden
        /// </summary>
        /// <param name="sourcePoint"></param>
        /// <returns></returns>
        internal virtual PointF ClipLine(PointF sourcePoint)
        {            
            PointF intersect = Center;
            float distance = GraphUtils.CalculateDistance(sourcePoint, Center);

            using (GraphicsPath path = GetPath())
            {                
                path.Flatten();

                if (path.PointCount > 0)
                {
                    PointF lastPoint = path.GetLastPoint();

                    // Intersect all the lines in the path with our line and choose the closest
                    for (int i = 0; i < path.PointCount; ++i)                    
                    {
                        PointF currIntersect;

                        if (GraphUtils.IntersectLine(sourcePoint, this.Center,
                            lastPoint, path.PathPoints[i], out currIntersect))
                        {
                            float newDistance = GraphUtils.CalculateDistance(sourcePoint, currIntersect);
                            if (newDistance < distance)
                            {
                                distance = newDistance;
                                intersect = currIntersect;
                            }
                        }

                        lastPoint = path.PathPoints[i];
                    }
                }
            }

            return intersect;
        }

        public GraphNode(Guid id, RectangleF boundary, float z, Color backColor, Color lineColor, 
            Color selectedLineColor, Color textColor, Color hatchedColor)
        {
            Boundary = boundary;
            Z = z;
            Label = string.Empty;
            BackColor = backColor;
            GradientColor = backColor;
            LineColor = lineColor;
            SelectedLineColor = selectedLineColor;
            TextColor = textColor;
            HatchedColor = hatchedColor;
            Id = id;
        }

        public override string ToString()
        {
            return Label;
        }
    }
}
