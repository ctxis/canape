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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CANAPE.Controls.GraphEditor
{
    /// <summary>
    /// Editor control for a directed graph    
    /// </summary>
    public partial class GraphEditorControl : Control
    {        
        #region Private Members
        enum DraggingMode
        {
            None,
            MoveNode,
            DrawLine,
            ScrollWindow
        }

        private List<GraphNode> _nodes;
        private List<GraphLine> _lines;
        private List<GraphLinkLine> _linkLines;
        private IGraphObject _selectedObject;
        private MouseButtons _heldButton;
        private DraggingMode _draggingMode;
        private PointF _lastDragPoint;
        private PointF _scrollPosition;        
        private bool _dirty;
        private object _graphLock;
        private float _xscale;
        private float _yscale;

        const int DEFAULT_DOCUMENT_WIDTH = 2048;
        const int DEFAULT_DOCUMENT_HEIGHT = 2048;

        #endregion

        #region Event Handling
        /// <summary>
        /// Event called when the selected object property changes
        /// </summary>
        public event EventHandler SelectedObjectChanged;

        /// <summary>
        /// Event called when the Dirty property changes
        /// </summary>
        public event EventHandler DirtyChanged;

        /// <summary>
        /// Event called when a node is deleted
        /// </summary>
        public event EventHandler<NodeDeletedEventArgs> NodeDeleted;

        /// <summary>
        /// Overridable method to handle selection changing
        /// </summary>
        protected virtual void OnSelectedObjectChanged()
        {
            SelectedObjectChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Overridable method to handle dirty changing
        /// </summary>
        protected virtual void OnDirtyChanged()
        {
            DirtyChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Overridable method to handle deletion of nodes
        /// </summary>
        /// <param name="node"></param>
        protected virtual void OnNodeDeleted(GraphNode node)
        {
            NodeDeleted?.Invoke(this, new NodeDeletedEventArgs(node));
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public GraphEditorControl()
        {
            _nodes = new List<GraphNode>();
            _lines = new List<GraphLine>();
            _linkLines = new List<GraphLinkLine>();
            _heldButton = MouseButtons.None;
            _lastDragPoint = new PointF();            
            _draggingMode = DraggingMode.None;
            _scrollPosition = new PointF();
            _dirty = false;
            _graphLock = new object();
            using (Graphics g = CreateGraphics())
            {
                _xscale = g.DpiX / 96.0f;
                _yscale = g.DpiY / 96.0f;
            }
            DocumentWidth = (int)(DEFAULT_DOCUMENT_WIDTH * _xscale);
            DocumentHeight = (int)(DEFAULT_DOCUMENT_HEIGHT * _yscale);
            MouseWheel += new MouseEventHandler(GraphEditorControl_MouseWheel);
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);            
            Controls.Add(vScrollBar);
            Controls.Add(hScrollBar);            
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// The total width of the document
        /// </summary>
        public int DocumentWidth
        {
            get;
            set;
        }

        /// <summary>
        /// The total height of the document
        /// </summary>
        public int DocumentHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Get the selected object
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IGraphObject SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                if (value != _selectedObject)
                {
                    _selectedObject = value;
                    OnSelectedObjectChanged();
                }
            }
        }

        /// <summary>
        /// Get dirty status of graph
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool Dirty
        {
            get { return _dirty; }
            set
            {                
                if (value && !_dirty)
                {
                    _dirty = true;
                    OnDirtyChanged();                    
                }
                else
                {
                    _dirty = false;
                }
            }
        }

        /// <summary>
        /// Get or set the graph data
        /// </summary>
        /// <returns>An object containing the graph data</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public GraphData Graph
        {
            get
            {
                GraphData ret;

                lock (_graphLock)
                {
                    ret = new GraphData(_nodes.ToArray(), _lines.ToArray());
                }

                return ret;
            }

            set
            {
                if (value != null)
                {
                    lock (_graphLock)
                    {
                        ClearGraph();
                        _nodes.AddRange(value.Nodes);
                        _lines.AddRange(value.Lines);
                    }

                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Property to allow bi-directional lines
        /// </summary>
        [Category("Graph")]
        public bool AllowBidirectionLines
        {
            get;
            set;
        }

        /// <summary>
        /// Property to indicate we want to draw drop shadows on the nodes
        /// </summary>
        [Category("Graph")]
        public bool DrawDropShadow
        {
            get;
            set;
        }

        /// <summary>
        /// X offset of drop shadow
        /// </summary>
        [Category("Graph")]
        public float DropShadowOffsetX
        {
            get;
            set;
        }

        /// <summary>
        /// Y offset of drop shadow
        /// </summary>
        [Category("Graph")]
        public float DropShadowOffsetY
        {
            get;
            set;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Add a new line to the graph between two nodes
        /// </summary>
        /// <remarks>If line already exists and direction matches will return the same line</remarks>
        /// <param name="source">The source node</param>
        /// <param name="dest">The destination node</param>
        /// <param name="biDirection">Indicates the line is bi-directional</param>
        /// <param name="label">Label for the line, can be null</param>
        /// <returns>The newly created line</returns>
        public GraphLine AddLine(GraphNode source, GraphNode dest)
        {
            // Check if we have already assigned this line or we have the other way
            foreach (GraphLine line in _lines)
            {
                if ((line.SourceShape == source) && (line.DestShape == dest))
                {
                    return line;
                }
                
                if ((line.SourceShape == dest) && (line.DestShape == source))
                {
                    if (AllowBidirectionLines)
                    {
                        line.BiDirection = true;
                        Dirty = true;
                    }

                    return line;
                }                
            }

            GraphLine l = AddNewLine(source, dest, false, null);
            Dirty = true;            

            return l;
        }

        /// <summary>
        /// Add a pre configured node to the graph
        /// </summary>
        /// <param name="p">The point location of the centre (in client co-ordinates)</param>
        /// <param name="z">The z level of the node</param>        
        /// <param name="id">The ID of the node</param>
        /// <param name="name">The text for the node</param>
        /// <param name="shape">The shape type</param>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <param name="backColor">The back color</param>
        /// <param name="lineColor">The line color</param>
        /// <param name="selectedLineColor">The selected line color</param>
        /// <param name="textColor">The text color</param>
        /// <param name="hatchedColor">The hatched color</param>
        /// <param name="tag">A opaque object to store in the node</param>
        /// <exception cref="System.ArgumentException">Throw if node shape is unknown</exception>
        /// <returns>The created node</returns>
        public GraphNode AddNode(PointF p, float z, Guid id, string name, GraphNodeShape shape, float width, float height, 
            Color backColor, Color lineColor, Color selectedLineColor, Color textColor, Color hatchedColor, object tag)
        {
            return AddNodeInternal(ClientToDocumentPoint(p), z, id, name, shape, width, height, 
                backColor, lineColor, selectedLineColor, textColor, hatchedColor, tag);
        }

        /// <summary>
        /// Delete a node from the graph
        /// </summary>
        /// <remarks>Will also deletes any lines attached to the node</remarks>
        /// <param name="node">The node to delete</param>
        public void DeleteNode(GraphNode node)
        {
            _lines.RemoveAll(l => ((l.SourceShape == node) || (l.DestShape == node)));
            _linkLines.RemoveAll(l => ((l.SourceShape == node) || (l.DestShape == node)));
            _nodes.Remove(node);

            if (_selectedObject == node)
            {
                SelectedObject = null;
            }

            OnNodeDeleted(node);

            Dirty = true;

            Invalidate();
        }

        /// <summary>
        /// Delete a line
        /// </summary>
        /// <param name="line">The line to delete</param>
        public void DeleteLine(GraphLine line)
        {
            _lines.Remove(line);
            if (_selectedObject == line)
            {
                SelectedObject = null;
            }

            Dirty = true;

            Invalidate();
        }

        /// <summary>
        /// Delete an object from the graph
        /// </summary>
        /// <param name="obj">The object to delete</param>
        public void DeleteObject(IGraphObject obj)
        {
            if (obj is GraphLine)
            {
                DeleteLine(obj as GraphLine);
            }
            else if (obj is GraphNode)
            {
                DeleteNode(obj as GraphNode);
            }
        }

        /// <summary>
        /// Get the object at the hit point
        /// </summary>
        /// <param name="p">The point to hit (in client co-ordinates)</param>
        /// <returns>The object, null if no object under the point</returns>
        public IGraphObject GetHitObject(PointF p)
        {
            return GetHitObjectInternal(ClientToDocumentPoint(p));            
        }

        /// <summary>
        /// Select an object by point
        /// </summary>
        /// <param name="p">The point to select (in client co-ordinates)</param>
        /// <returns>The graph object selected, null if not nothing selected</returns>
        public IGraphObject SelectObject(PointF p)
        {
            return SelectObjectInternal(ClientToDocumentPoint(p));
        }

        /// <summary>
        /// Clear the graph
        /// </summary>
        public void ClearGraph()
        {
            _nodes.Clear();
            _lines.Clear();
            _linkLines.Clear();
            SelectedObject = null;
            Dirty = true;
            Invalidate();
        }

        /// <summary>
        /// Add an annotation link line
        /// </summary>
        /// <param name="sourceShape">The source shape</param>
        /// <param name="destShape">The destination shape</param>
        /// <param name="lineColor">The line color</param>
        /// <param name="lineDashStyle">The line dash style</param>
        /// <param name="invalidate">Whether to invalid the control or not</param>
        public void AddLinkLine(GraphNode sourceShape, GraphNode destShape, Color lineColor, DashStyle lineDashStyle, bool invalidate)
        {
            if ((destShape != null) && (sourceShape != null))
            {
                if (_linkLines.FindIndex(l => l.Matches(sourceShape, destShape)) < 0)
                {
                    _linkLines.Add(new GraphLinkLine()
                    {
                        DestShape = destShape,
                        SourceShape = sourceShape,
                        LineColor = lineColor,
                        LineDashStyle = lineDashStyle
                    });

                    if (invalidate)
                    {
                        Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Remove a link line which attaches to this node
        /// </summary>
        /// <param name="sourceShape">The node</param>
        public void RemoveLinkLine(GraphNode node)
        {
            _linkLines.RemoveAll(l => ((l.SourceShape == node) || (l.DestShape == node)));
        }

        /// <summary>
        /// Get the graph node by ID
        /// </summary>
        /// <param name="id">The ID to find</param>
        /// <returns>Returns null if not found</returns>
        public GraphNode GetNodeById(Guid id)
        {
            return _nodes.Find(n => n.Id == id);
        }

        /// <summary>
        /// Get a node by its tag value
        /// </summary>
        /// <param name="tag">The tag to find</param>
        /// <returns>Returns null if not found</returns>
        public GraphNode GetNodeByTag(object tag)
        {
            return _nodes.Find(n => n.Tag == tag);
        }
     
        /// <summary>
        /// Try and centre the view of the graph
        /// </summary>
        public void CenterViewOfGraph()
        {
            float centrex = 0.0f;
            float centrey = 0.0f;            

            foreach (GraphNode node in _nodes)
            {
                centrex += node.Center.X;
                centrey += node.Center.Y;
            }

            centrex /= (float)_nodes.Count;
            centrey /= (float)_nodes.Count;

            SetScrollCenter(centrex, centrey);
        }

        public void CenterViewOnNode(GraphNode node)
        {
            SetScrollCenter(node.Center.X, node.Center.Y);
        }

        #endregion

        #region Private Methods

        private void SetScrollCenter(float centrex, float centrey)
        {                        
            centrex -= Size.Width / 2;
            centrey -= Size.Height / 2;

            int deltax = (int)(_scrollPosition.X - centrex);
            int deltay = (int)(_scrollPosition.Y - centrey);

            ScrollClientWindow(deltax, hScrollBar);
            ScrollClientWindow(deltay, vScrollBar);

            Invalidate();
        }

        private void SortNodesByZ()
        {
            // Sort by Z order
            _nodes.Sort(new Comparison<GraphNode>((l, r) => (int)l.Z - (int)r.Z));
        }

        private void MoveNodeToTop(GraphNode node)
        {
            _nodes.Remove(node);
            _nodes.Add(node);
        }

        private void MoveNodeToBottom(GraphNode node)
        {
            _nodes.Remove(node);
            _nodes.Insert(0, node);
        }

        private GraphLine AddNewLine(GraphNode sourceShape, GraphNode destShape, bool biDirection, string label)
        {
            GraphLine l = new GraphLine();

            l.SourceShape = sourceShape;
            l.DestShape = destShape;
            l.LineColor = Color.Black;
            l.BiDirection = biDirection;
            l.Label = label;
            _lines.Add(l);

            return l;
        }

        private PointF ClientToDocumentPoint(PointF cp)
        {           
            return new PointF(cp.X + _scrollPosition.X, cp.Y + _scrollPosition.Y);
        }

        private RectangleF CreateBoundary(PointF p, float width, float height)
        {
            width *= _xscale;
            height *= _yscale;
            p.X *= _xscale;
            p.Y *= _yscale;

            return new RectangleF(p.X - (width / 2.0f), p.Y - (height / 2.0f), width, height);
        }

        /// <summary>
        /// Get the object at the hit point
        /// </summary>
        /// <param name="p">The point to hit (in document co-ordinates)</param>
        /// <returns>The object, null if no object under the point</returns>
        private IGraphObject GetHitObjectInternal(PointF p)
        {
            IGraphObject match = null;

            for (int i = _nodes.Count; i > 0; --i)
            {
                GraphNode s = _nodes[i - 1];
                if (s.HitTest(p))
                {
                    match = s;
                    break;
                }
            }

            if (match == null)
            {
                foreach (var l in _lines)
                {
                    if (l.HitTest(p))
                    {
                        match = l;
                        break;
                    }
                }
            }

            return match;
        }

        /// <summary>
        /// Add a pre configured node to the graph
        /// </summary>
        /// <param name="p">The point location of the centre (in document co-ordinates)</param>
        /// <param name="z">The z level of the node</param>        
        /// <param name="id">The ID of the node</param>
        /// <param name="name">The text for the node</param>
        /// <param name="shape">The shape type</param>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <param name="backColor">The back color</param>
        /// <param name="lineColor">The line color</param>
        /// <param name="selectedLineColor">The selected line color</param>
        /// <param name="textColor">The text color</param>
        /// <param name="hatchedColor">The hatched color</param>
        /// <param name="tag">A opaque object to store in the node</param>
        /// <exception cref="System.ArgumentException">Throw if node shape is unknown</exception>
        /// <returns>The created node</returns>
        private GraphNode AddNodeInternal(PointF p, float z, Guid id, string name, GraphNodeShape shape, float width, float height,
            Color backColor, Color lineColor, Color selectedLineColor, Color textColor, Color hatchedColor, object tag)
        {
            GraphNode s;            

            switch (shape)
            {
                case GraphNodeShape.Ellipse:
                    s = new GraphNodeCircle(id, CreateBoundary(p, width, height),
                        z, backColor, lineColor, selectedLineColor, textColor, hatchedColor);
                    break;
                case GraphNodeShape.Rectangle:
                    s = new GraphNodeRectangle(id, CreateBoundary(p, width, height),
                        z, backColor, lineColor, selectedLineColor, textColor, hatchedColor);
                    break;
                case GraphNodeShape.RoundedRectangle:
                    s = new GraphNodeRoundedRectangle(id, CreateBoundary(p, width, height),
                        z, backColor, lineColor, selectedLineColor, textColor, hatchedColor);
                    break;
                case GraphNodeShape.Triangle:
                    s = new GraphNodeTriangle(id, CreateBoundary(p, width, height),
                        z, backColor, lineColor, selectedLineColor, textColor, hatchedColor);
                    break;
                case GraphNodeShape.Rhombus:
                    s = new GraphNodeRhombus(id, CreateBoundary(p, width, height),
                        z, backColor, lineColor, selectedLineColor, textColor, hatchedColor);
                    break;
                default:
                    throw new ArgumentException(CANAPE.Controls.GraphEditor.Properties.Resources.GraphEditorControl_InvalidNodeType);
            }

            GraphLine line = GetHitObjectInternal(p) as GraphLine;

            if (line != null)
            {
                if (MessageBox.Show(this, CANAPE.Controls.GraphEditor.Properties.Resources.GraphEditorControl_SplitLineMessage, 
                    CANAPE.Controls.GraphEditor.Properties.Resources.GraphEditorControl_SplitLineCaption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string label = line.Label;
                    _lines.Remove(line);
                    if (_selectedObject == line)
                    {
                        SelectedObject = null;
                    }

                    AddNewLine(line.SourceShape, s, line.BiDirection, label);
                    AddNewLine(s, line.DestShape, line.BiDirection, null);
                }
            }

            s.Label = name;
            s.Tag = tag;
            _nodes.Add(s);

            // Use the move location function to adjust the position to fit within the boundary
            s.MoveLocation(new SizeF(), new RectangleF(0, 0, DocumentWidth, DocumentHeight));  

            Dirty = true;
            Invalidate();

            return s;
        }

        /// <summary>
        /// TODO: Implement proper clipping to improve performance with alot of objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphEditorClass_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode)
            {
                GraphNode node = new GraphNodeRoundedRectangle(Guid.NewGuid(), 
                    new RectangleF(10.0f, 10.0f, 100.0f, 50.0f), 0.0f, Color.AliceBlue, Color.Black, Color.Red, Color.Black, Color.Black);
                node.Label = "DEMO1";

                var g = e.Graphics;
                
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (DrawDropShadow)
                {                    
                    node.DrawDropShadow(g, DropShadowOffsetX, DropShadowOffsetY);
                }
            
                node.Draw(g, this.Font, false);
            }
            else
            {
                Graphics g = e.Graphics;
                List<GraphNode> drawNodes = new List<GraphNode>(_nodes);
                List<GraphLine> drawLines = new List<GraphLine>(_lines);
                List<GraphLinkLine> drawLinkLines = new List<GraphLinkLine>(_linkLines);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TranslateTransform(-_scrollPosition.X, -_scrollPosition.Y);
                
                if (DrawDropShadow)
                {
                    foreach (var n in drawNodes)
                    {
                        n.DrawDropShadow(g, DropShadowOffsetX, DropShadowOffsetY);
                    }
                }

                foreach (var l in drawLinkLines)
                {
                    l.Draw(g);
                }

                foreach (var l in drawLines)
                {
                    if (l != _selectedObject)
                    {
                        l.Draw(g, this.Font, false);
                    }
                }

                foreach (var s in drawNodes)
                {
                    s.Draw(g, this.Font, s == _selectedObject);
                }

                // Always draw a selected line above everything
                if (_selectedObject is GraphLine)
                {
                    GraphLine l = _selectedObject as GraphLine;

                    l.Draw(g, this.Font, true);
                }

                // We are dragging a line from a graph node
                if ((_draggingMode == DraggingMode.DrawLine) && (_selectedObject != null) && (_selectedObject is GraphNode))
                {
                    Pen p = null;
                    GraphNode node = _selectedObject as GraphNode;

                    try
                    {
                        p = new Pen(Brushes.Black);
                        g.DrawLine(p, node.Center, _lastDragPoint);
                    }
                    finally
                    {
                        if (p != null)
                        {
                            p.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Select an object by point
        /// </summary>
        /// <param name="p">The point to select (in document co-ordinates)</param>
        /// <returns>The graph object selected, null if not nothing selected</returns>
        private IGraphObject SelectObjectInternal(PointF p)
        {
            IGraphObject match = GetHitObjectInternal(p);

            if (match != null)
            {
                SelectedObject = match;
                if (match is GraphNode)
                {
                    GraphNode node = (GraphNode)match;
                    _nodes.ForEach(n => n.Z = 0);

                    node.Z = 1;
                    MoveNodeToTop(node);
                }
            }
            else
            {
                SelectedObject = null;
            }

            Invalidate();

            return match;
        }

        private void UpdateSelectedObject(PointF newPoint)
        {
            if ((_selectedObject != null) && (_selectedObject is GraphNode))
            {
                GraphNode node = _selectedObject as GraphNode;                

                SizeF delta = new SizeF(newPoint.X - _lastDragPoint.X, newPoint.Y - _lastDragPoint.Y);
 
                node.MoveLocation(delta, new RectangleF(0, 0, DocumentWidth, DocumentHeight));                
                _lastDragPoint = newPoint;
                Dirty = true;
                Invalidate();
            }
        }

        private void UpdateDragLine(PointF newPoint)
        {
            if ((_selectedObject != null) && (_selectedObject is GraphNode))
            {                
                _lastDragPoint = newPoint; 
                Invalidate(); 
            }
        }

        private void UpdateScroll(PointF newPoint, PointF clientPoint)
        {
            int deltaX = (int)((newPoint.X - _lastDragPoint.X));
            int deltaY = (int)((newPoint.Y - _lastDragPoint.Y));       
            
            ScrollClientWindow(deltaX, hScrollBar);
            ScrollClientWindow(deltaY, vScrollBar);

            _lastDragPoint = ClientToDocumentPoint(clientPoint);

            Invalidate();
        }

        private void GraphEditorClass_MouseDown(object sender, MouseEventArgs e)
        {            
            PointF mousePos = ClientToDocumentPoint(new PointF(e.X, e.Y));
            if (_heldButton == MouseButtons.None)
            {
                if ((e.Button == MouseButtons.Left) && (ModifierKeys != Keys.Control))
                {
                    SelectObjectInternal(mousePos);
                    _lastDragPoint = mousePos;
                    _heldButton = e.Button;
                    _draggingMode = DraggingMode.MoveNode;
                }
                else if ((e.Button == MouseButtons.Middle) || ((e.Button == MouseButtons.Left) && (ModifierKeys == Keys.Control)))
                {
                    IGraphObject s = SelectObjectInternal(mousePos);
                    if ((s != null) && (s is GraphNode))
                    {
                        _lastDragPoint = mousePos;
                        _heldButton = e.Button;
                        _draggingMode = DraggingMode.DrawLine;
                    }
                    else if (s == null)
                    {
                        _lastDragPoint = mousePos;
                        _heldButton = e.Button;
                        _draggingMode = DraggingMode.ScrollWindow;
                        this.Cursor = Cursors.SizeAll;
                    }
                }
            }

            Focus();
        }

        private void GraphEditorClass_MouseMove(object sender, MouseEventArgs e)
        {
            PointF clientPoint = new PointF(e.X, e.Y);
            PointF documentPoint = ClientToDocumentPoint(clientPoint);
            bool checkScroll = false;

            switch(_draggingMode)
            {
                case DraggingMode.MoveNode: 
                    UpdateSelectedObject(documentPoint);
                    checkScroll = true;
                    break;
                case DraggingMode.DrawLine:
                    UpdateDragLine(documentPoint);
                    checkScroll = true;
                    break;
                case DraggingMode.ScrollWindow:
                    UpdateScroll(documentPoint, clientPoint);
                    break;
                default:
                    break;
            }

            if (checkScroll)
            {                
                if (e.Y < 0)
                {
                    ScrollClientWindow(-e.Y, vScrollBar);
                }
                else if (e.Y > ClientRectangle.Height)
                {
                    ScrollClientWindow(-(e.Y - ClientRectangle.Height), vScrollBar);
                }

                if (e.X < 0)
                {
                    ScrollClientWindow(-e.X, hScrollBar);
                }
                else if (e.X > ClientRectangle.Width)
                {
                    ScrollClientWindow(-(e.X - ClientRectangle.Width), hScrollBar);
                }
            }
        }

        private void GraphEditorClass_MouseUp(object sender, MouseEventArgs e)
        {
            if (_heldButton == e.Button)
            {
                _heldButton = MouseButtons.None;
                PointF documentPoint = ClientToDocumentPoint(new PointF(e.X, e.Y));

                switch (_draggingMode)
                {
                    case DraggingMode.MoveNode:
                        UpdateSelectedObject(documentPoint);
                        break;
                    case DraggingMode.DrawLine:
                        GraphNode destShape = GetHitObjectInternal(documentPoint) as GraphNode;
                        if ((destShape != null) && (destShape != _selectedObject) && (_selectedObject is GraphNode))
                        {
                            AddLine(_selectedObject as GraphNode, destShape);
                        }
                        Invalidate();
                        break;
                    case DraggingMode.ScrollWindow:
                        Cursor = Cursors.Arrow;
                        break;
                }

                _draggingMode = DraggingMode.None;
            }
        }

        private void GraphEditorClass_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if ((_selectedObject != null) && (_heldButton == MouseButtons.None))
                {
                    DeleteObject(_selectedObject);                    
                }
            }
        }
        
        private void ScrollClientWindow(int delta, ScrollBar scrollbar)
        {
            int curr = scrollbar.Value - delta;
            int maxScroll = scrollbar.Maximum - scrollbar.LargeChange + 1;

            if (curr < 0)
            {
                curr = 0;
            }
            else if (curr > maxScroll)
            {
                curr = maxScroll;
            }

            scrollbar.Value = curr;

            _scrollPosition.X = hScrollBar.Value;
            _scrollPosition.Y = vScrollBar.Value;
        }

        private void GraphEditorControl_MouseWheel(object sender, MouseEventArgs e)
        {
            ScrollClientWindow(e.Delta / 4, vScrollBar);
            Invalidate();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _scrollPosition.X = e.NewValue;            
            Invalidate();
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _scrollPosition.Y = e.NewValue;            
            Invalidate();
        }

        private static float CalculateMaxScroll(float size, ScrollBar scroll)
        {
            return size + scroll.LargeChange - 1.0f;            
        }

        private void ResizeScrollBars()
        {
            float vMax = DocumentHeight - ClientRectangle.Height;
            float hMax = DocumentWidth - ClientRectangle.Width;

            if (vMax < 0.0f)
            {
                vMax = 0.0f;
            }

            if (hMax < 0.0f)
            {
                hMax = 0.0f;
            }

            vScrollBar.Maximum = (int)Math.Ceiling(CalculateMaxScroll(vMax, vScrollBar));
            hScrollBar.Maximum = (int)Math.Ceiling(CalculateMaxScroll(hMax, hScrollBar));

            _scrollPosition.X = Math.Min(_scrollPosition.X, hMax);
            hScrollBar.Value = (int)_scrollPosition.X;
            _scrollPosition.Y = Math.Min(_scrollPosition.Y, vMax);
            vScrollBar.Value = (int)_scrollPosition.Y;            
        }

        private void GraphEditorControl_Resize(object sender, EventArgs e)
        {
            ResizeScrollBars();

            Invalidate();
        }

        #endregion

    }
}
