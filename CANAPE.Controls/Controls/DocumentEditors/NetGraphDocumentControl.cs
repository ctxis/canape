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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using CANAPE.Controls.GraphEditor;
using CANAPE.DataFrames;
using CANAPE.DataFrames.Filters;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Forms;
using CANAPE.NodeFactories;
using CANAPE.NodeLibrary;
using CANAPE.Properties;
using CANAPE.Utils;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class NetGraphDocumentControl : UserControl
    {
        public const string NODECONFIG_DATA = "NODECONFIG";
        public const string NODEFILTER_DATA = "NODEFILTER";
        public const string NODEPROPERTIES_DATA = "NODEPROPERTIES";

        class LineContainer
        {
            private GraphLine _line;
            private GraphEditorControl _control;

            [LocalizedDescription("LineContainer_LabelDescription", typeof(Properties.Resources)), Category("Control")]
            public string Label
            {
                get
                {
                    return _line.Label;
                }

                set
                {
                    if (_line.Label != value)
                    {
                        _line.Label = value;
                        _control.Dirty = true;
                        _control.Invalidate();
                    }
                }
            }

            [LocalizedDescription("LineContainer_WeakDescription", typeof(Properties.Resources)), Category("Control")]
            public bool Weak
            {
                get 
                {
                    if (_line.Tag != null)
                    {
                        return (bool)_line.Tag;
                    }
                    else
                    {
                        return false;
                    }
                }
                set
                {
                    bool isWeak = false;

                    if (_line.Tag != null)
                    {
                        isWeak = (bool)_line.Tag;
                    }

                    if (isWeak != value)
                    {
                        _line.Tag = value;
                        _line.LineDashStyle = value ? DashStyle.Dot : DashStyle.Solid;
                        _control.Dirty = true;
                        _control.Invalidate();
                    }
                }
            }

            public LineContainer(GraphLine line, GraphEditorControl control)
            {
                _line = line;
                _control = control;
            }
        }

        NetGraphDocument _document;
        Dictionary<string, GraphNodeTemplate> _templates;
        PointF _currMousePos;
        bool _populatingControl;

        private void AddNodeTemplate(string templateName, string nameTemplate, GraphNodeShape shape, 
            float width, float height, Color backColor, Color lineColor, Color selectedLineColor, Color textColor, Color hatchedColor, Type tagType)
        {
            if (typeof(BaseNodeConfig).IsAssignableFrom(tagType))
            {
                _templates[templateName] = new GraphNodeTemplate(nameTemplate, shape, 
                    width, height, backColor, lineColor, selectedLineColor, textColor, hatchedColor, tagType);
            }
            else
            {
                throw new ArgumentException("Invalid node config type for editor template");
            }
        }

        public NetGraphDocumentControl(IDocumentObject document)
        {
            _document = (NetGraphDocument)document;
            _templates = new Dictionary<string, GraphNodeTemplate>();

            AddNodeTemplate(ServerEndpointConfig.NodeName, "SERVER{0}", GraphNodeShape.Ellipse, 100.0f, 100.0f, 
                Color.Purple, Color.Black, Color.Red, Color.White, Color.WhiteSmoke, typeof(ServerEndpointConfig));
            AddNodeTemplate(ClientEndpointConfig.NodeName, "CLIENT{0}", GraphNodeShape.Ellipse, 100.0f, 100.0f,
                Color.LightGoldenrodYellow, Color.Black, Color.Red, Color.Black, Color.Black, typeof(ClientEndpointConfig));
            AddNodeTemplate(EditPacketNodeConfig.NodeName, "EDIT{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.Sienna, Color.Black, Color.Red, Color.Black, Color.Black, typeof(EditPacketNodeConfig));
            AddNodeTemplate(LogPacketConfig.NodeName, "LOG{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.Silver, Color.Black, Color.Red, Color.Black, Color.Black, typeof(LogPacketConfig));
            AddNodeTemplate(DirectNodeConfig.NodeName, "NOP{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.SlateBlue, Color.Black, Color.Red, Color.Black, Color.Black, typeof(DirectNodeConfig));
            AddNodeTemplate(DelayNodeConfig.NodeName, "DELAY{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.SlateBlue, Color.Black, Color.Red, Color.Black, Color.Black, typeof(DelayNodeConfig));
            AddNodeTemplate(DynamicNodeConfig.NodeName, "DYN{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.AliceBlue, Color.Black, Color.Red, Color.Black, Color.Black, typeof(DynamicNodeConfig));
            AddNodeTemplate(LibraryNodeConfig.NodeName, "LIB{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.Blue, Color.Black, Color.Red, Color.White, Color.WhiteSmoke, typeof(LibraryNodeConfig));
            AddNodeTemplate(NetGraphContainerConfig.NodeName, "GRAPH{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.Aqua, Color.Black, Color.Red, Color.Black, Color.Black, typeof(NetGraphContainerConfig));
            AddNodeTemplate(DecisionNodeConfig.NodeName, "IF{0}", GraphNodeShape.Rhombus, 100.0f, 80.0f,
                Color.Crimson, Color.Black, Color.Red, Color.Black, Color.Black, typeof(DecisionNodeConfig));
            AddNodeTemplate(SwitchNodeConfig.NodeName, "SWITCH{0}", GraphNodeShape.Rhombus, 100.0f, 80.0f,
                Color.Yellow, Color.Black, Color.Red, Color.Black, Color.Black, typeof(SwitchNodeConfig));
            AddNodeTemplate(SslLayerSectionNodeConfig.NodeName, "SSL{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.BlanchedAlmond, Color.Black, Color.Red, Color.Black, Color.Black, typeof(SslLayerSectionNodeConfig));
            AddNodeTemplate(LayerSectionNodeConfig.NodeName, "LAYER{0}", GraphNodeShape.RoundedRectangle, 100.0f, 50.0f,
                Color.Coral, Color.Black, Color.Red, Color.Black, Color.Black, typeof(LayerSectionNodeConfig));
            
            InitializeComponent();
            PopulateGraphFromDocument(true);
        }

        /// <summary>
        /// Setup the node factory object
        /// </summary>
        /// <param name="n">The node to configure</param>
        private void SetNode(GraphNode n)
        {
            BaseNodeConfig config = n.Tag as BaseNodeConfig;
            if (config != null)
            {
                n.Hatched = !config.Enabled;
                n.Label = config.Label;

                config.LabelChanged += new EventHandler(factory_LabelChanged);
                config.DirtyChanged += new EventHandler(factory_DirtyChanged);
                config.EnabledChanged += new EventHandler(config_EnabledChanged);
                config.PropertiesChanged += new EventHandler(config_PropertiesChanged);
            }
        }

        void config_PropertiesChanged(object sender, EventArgs e)
        {
            netEditor.Dirty = true;
        }

        void config_EnabledChanged(object sender, EventArgs e)
        {
            BaseNodeConfig config = sender as BaseNodeConfig;

            GraphNode node = netEditor.GetNodeByTag(config);

            if (node != null)
            {
                node.Hatched = !config.Enabled;
                netEditor.Invalidate();
            }
        }

        void factory_DirtyChanged(object sender, EventArgs e)
        {
            BaseNodeConfig config = sender as BaseNodeConfig;
            ILinkedNodeConfig linkedConfig = config as ILinkedNodeConfig;

            if (linkedConfig != null)
            {
                if (linkedConfig.LinkedNode == null)
                {
                    GraphNode n = netEditor.GetNodeByTag(linkedConfig);

                    netEditor.RemoveLinkLine(n);
                }
                else
                {
                    GraphNode src = netEditor.GetNodeByTag(linkedConfig);
                    GraphNode dest = netEditor.GetNodeByTag(linkedConfig.LinkedNode);

                    netEditor.RemoveLinkLine(src);
                    netEditor.RemoveLinkLine(dest);
                    AddLinkLine(src, dest);
                }

                netEditor.Invalidate();
            }

            netEditor.Dirty = true;
        }

        void factory_LabelChanged(object sender, EventArgs e)
        {
            BaseNodeConfig config = sender as BaseNodeConfig;

            GraphNode node = netEditor.GetNodeByTag(config);

            if (node != null)
            {
                node.Label = config.Label;
                MasterLayerNodeConfig masterConfig = config as MasterLayerNodeConfig;
                if (masterConfig != null)
                {
                    masterConfig.Slave.Label = config.Label + "-Slave";
                }

                netEditor.Invalidate();
            }
        }
        
        /// <summary>
        /// Add an existing node to the editor
        /// </summary>            
        /// <param name="config">The node entry</param>
        /// <param name="p">The point to set</param>
        /// <param name="z">The z co-ordinate</param>
        private GraphNode AddNode(BaseNodeConfig config, PointF p, float z)
        {
            GraphNodeTemplate template = _templates[config.GetNodeName()];

            GraphNode n = netEditor.AddNode(p, z, config.Id, config.Label, template.Shape,
                        template.Width, template.Height, template.BackColor, template.LineColor, template.SelectedLineColor,
                        template.TextColor, template.HatchedColor, config);
            
            SetNode(n);

            netEditor.SelectedObject = n;

            return n;
        }

        /// <summary>
        /// Add a node at the current location with a new pipe node
        /// </summary>
        /// <param name="templateName"></param>
        private void AddNode(string templateName)
        {
            GraphNodeTemplate template = _templates[templateName];
            BaseNodeConfig config = null;
            string label = null;

            if (template.TagType == typeof(LibraryNodeConfig))
            {
                using (SelectLibraryNodeForm frm = new SelectLibraryNodeForm())
                {
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        NodeLibraryManager.NodeLibraryType type = frm.Node;
                        object nodeConfig = null;

                        if (type.ConfigType != null)
                        {
                            nodeConfig = Activator.CreateInstance(type.ConfigType);
                        }

                        config = new LibraryNodeConfig(type.Type, type.Name, nodeConfig);

                        if (!String.IsNullOrWhiteSpace(type.NodeName))
                        {
                            label = type.NodeName;
                        }
                    }
                }
            }
            else
            {
                config = (BaseNodeConfig)Activator.CreateInstance(template.TagType);
            }

            if (config != null)
            {
                if (label != null)
                {
                    config.Label = label;
                }
                else
                {
                    config.Label = template.GetNewName();
                }

                GraphNode n = AddNode(config, _currMousePos, 0.0f);
                
                if (config is MasterLayerNodeConfig)
                {                    
                    MasterLayerNodeConfig masterConfig = config as MasterLayerNodeConfig;
                    masterConfig.Slave.Label = config.Label + "-Slave";
                    AddLinkLine(n, AddNode(masterConfig.Slave, new PointF(_currMousePos.X + 75.0f, _currMousePos.Y), 0.0f));
                }

                netEditor.SelectedObject = n;
            }
        }

        private void AddLinkLine(GraphNode src, GraphNode dest)
        {
            netEditor.AddLinkLine(src, dest, Color.Blue, DashStyle.Dash, false);
        }

        private void PopulateGraphFromDocument(bool centre)
        {
            Dictionary<Guid, GraphNode> idToNode = new Dictionary<Guid, GraphNode>();
            List<GraphNode> linkedNodes = new List<GraphNode>();
            _populatingControl = true;

            netEditor.SuspendLayout();
            netEditor.ClearGraph();
            netEditor.DocumentWidth = NetGraphDocument.DEFAULT_DOCUMENT_WIDTH;
            netEditor.DocumentHeight = NetGraphDocument.DEFAULT_DOCUMENT_HEIGHT;    

            foreach(var n in _document.Nodes)
            {
                idToNode[n.Id] = AddNode(n, new PointF(n.X, n.Y), n.Z);
                ILinkedNodeConfig linkedConfig = n as ILinkedNodeConfig;

                if ((linkedConfig != null) && (linkedConfig.LinkedNode != null))
                {                    
                    linkedNodes.Add(idToNode[n.Id]);
                }
            }

            foreach (GraphNode n in linkedNodes)
            {
                ILinkedNodeConfig config = n.Tag as ILinkedNodeConfig;

                if (idToNode.ContainsKey(config.LinkedNode.Id))
                {
                    AddLinkLine(n, idToNode[config.LinkedNode.Id]);
                }
            }

            foreach (LineConfig l in _document.Lines)
            {
                if((idToNode.ContainsKey(l.SourceNode.Id) && (idToNode.ContainsKey(l.DestNode.Id))))
                {
                    GraphLine newLine = netEditor.AddLine(idToNode[l.SourceNode.Id], idToNode[l.DestNode.Id]);
                    newLine.BiDirection = l.BiDirection;                    
                    newLine.Label = l.PathName;
                    newLine.Tag = l.WeakPath;
                    newLine.LineDashStyle = l.WeakPath ? DashStyle.Dot : DashStyle.Solid;
                }
            }

            netEditor.SelectedObject = null;

            if (centre)
            {
                netEditor.CenterViewOfGraph();                    
            }

            netEditor.ResumeLayout();
            _populatingControl = false;
        }

        private void PopulateDocumentFromGraph()
        {
            if (!_populatingControl)
            {
                List<BaseNodeConfig> nodes = new List<BaseNodeConfig>();
                List<LineConfig> lines = new List<LineConfig>();
                GraphData graph = netEditor.Graph;

                foreach (var n in graph.Nodes)
                {
                    BaseNodeConfig node = (BaseNodeConfig)n.Tag;
                    node.X = n.Center.X;
                    node.Y = n.Center.Y;
                    node.Z = n.Z;

                    nodes.Add(node);
                }

                foreach (var l in graph.Lines)
                {
                    bool isWeak = false;
                    if (l.Tag != null)
                    {
                        isWeak = (bool)l.Tag;
                    }
                    LineConfig line = new LineConfig((BaseNodeConfig)l.SourceShape.Tag,
                        (BaseNodeConfig)l.DestShape.Tag, l.BiDirection, l.Label, isWeak);

                    lines.Add(line);
                }

                _document.UpdateGraph(nodes.ToArray(), lines.ToArray());
            }
        }

        private void NodeGraphForm_Load(object sender, EventArgs e)
        {
            Text = _document.Name;

            netEditor.Dirty = false;
            netEditor.DirtyChanged += new EventHandler(netEditor_DirtyChanged);
            netEditor.SelectedObjectChanged += new EventHandler(netEditor_SelectedObjectChanged);

            propertyGrid.SelectedObject = _document;

            netEditor.Invalidate();
        }

        private void AddNode_Click(object sender, EventArgs e)
        {
            AddNode(((ToolStripMenuItem)sender).Tag as string);
        }
        
        private void netEditor_DirtyChanged(object sender, EventArgs e)
        {
            if ((_document != null) && (netEditor.Dirty == true))
            {
                _document.Dirty = true;
                PopulateDocumentFromGraph();
            }
            netEditor.Dirty = false;
        }

        private void netEditor_SelectedObjectChanged(object sender, EventArgs e)
        {
            deleteSelectedToolStripMenuItem.Enabled = (netEditor.SelectedObject != null);

            if (netEditor.SelectedObject != null)
            {
                if (netEditor.SelectedObject is GraphNode)
                {
                    BaseNodeConfig config = ((GraphNode)netEditor.SelectedObject).Tag as BaseNodeConfig;

                    if (config is SlaveLayerNodeConfig)
                    {
                        config = ((SlaveLayerNodeConfig)config).Master;
                    }

                    propertyGrid.SelectedObject = config;
                }
                else if (netEditor.SelectedObject is GraphLine)
                {
                    propertyGrid.SelectedObject = new LineContainer((GraphLine)netEditor.SelectedObject, netEditor);
                }
                else
                {
                    propertyGrid.SelectedObject = _document;
                }
            }
            else
            {
                propertyGrid.SelectedObject = _document;
            }
        }        

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (netEditor.SelectedObject != null)
            {
                netEditor.DeleteObject(netEditor.SelectedObject);
            }
        }

        /// <summary>
        /// Copy a node config, used to allow external applications to create a suitable node
        /// </summary>
        /// <param name="node"></param>
        public static void CopyNode(BaseNodeConfig config)
        {
            SlaveLayerNodeConfig slaveConfig = config as SlaveLayerNodeConfig;
            if (slaveConfig != null)
            {
                // Copy the master, not slave
                config = slaveConfig.Master;
            }

            Clipboard.SetData(NODECONFIG_DATA, config);
        }

        private void CopySelectedNode()
        {
            GraphNode node = netEditor.SelectedObject as GraphNode;

            if (node != null)
            {
                CopyNode((BaseNodeConfig)node.Tag);                
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (netEditor.SelectedObject is GraphNode)
            {
                CopySelectedNode();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (netEditor.SelectedObject is GraphNode)
            {
                CopySelectedNode();
                netEditor.DeleteObject(netEditor.SelectedObject);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsData(NODECONFIG_DATA))
            {
                try
                {
                    BaseNodeConfig config = Clipboard.GetData(NODECONFIG_DATA) as BaseNodeConfig;

                    if (config != null)
                    {
                        // Create a new ID
                        config.Id = Guid.NewGuid();
                        GraphNode n = AddNode(config, _currMousePos, 0.0f);

                        if (config is MasterLayerNodeConfig)
                        {
                            MasterLayerNodeConfig masterConfig = config as MasterLayerNodeConfig;
                            AddLinkLine(n, AddNode(masterConfig.Slave, new PointF(_currMousePos.X + 50.0f, _currMousePos.Y + 50.0f), 0.0f));
                        }
                    }
                }
                catch (SerializationException ex)
                {
                    MessageBox.Show(this, ex.Message, "Error Pasting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool nodeSelected = netEditor.SelectedObject is GraphNode;

            if (nodeSelected)
            {
                copyToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyFiltersToolStripMenuItem.Enabled = true;
                copyPropertiesToolStripMenuItem.Enabled = true;
                toggleEnableToolStripMenuItem.Enabled = true;
                filtersToolStripMenuItem.Enabled = true;
                propertiesToolStripMenuItem.Enabled = true;
                centreOnNodeToolStripMenuItem.Enabled = true;
            }
            else
            {
                copyToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyFiltersToolStripMenuItem.Enabled = false;
                copyPropertiesToolStripMenuItem.Enabled = false;
                toggleEnableToolStripMenuItem.Enabled = false;
                filtersToolStripMenuItem.Enabled = false;
                propertiesToolStripMenuItem.Enabled = false;
                centreOnNodeToolStripMenuItem.Enabled = false;
            }

            if (Clipboard.ContainsData(NODECONFIG_DATA))
            {
                pasteToolStripMenuItem.Enabled = true;
            }
            else
            {
                pasteToolStripMenuItem.Enabled = false;
            }

            if (Clipboard.ContainsData(NODEFILTER_DATA) && nodeSelected)
            {
                pasteFiltersToolStripMenuItem.Enabled = true;
            }
            else
            {
                pasteFiltersToolStripMenuItem.Enabled = false;
            }

            if (Clipboard.ContainsData(NODEPROPERTIES_DATA) && nodeSelected)
            {
                pastePropertiesToolStripMenuItem.Enabled = true;
            }
            else
            {
                pastePropertiesToolStripMenuItem.Enabled = false;
            }
        }

        private void copyFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphNode node = netEditor.SelectedObject as GraphNode;

            if (node != null)
            {
                BaseNodeConfig config = (BaseNodeConfig)node.Tag;

                Clipboard.SetData(NODEFILTER_DATA, config.Filters);
            }
        }

        private void pasteFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphNode node = netEditor.SelectedObject as GraphNode;

            if ((node != null) && Clipboard.ContainsData(NODEFILTER_DATA))
            {
                BaseNodeConfig config = (BaseNodeConfig)node.Tag;

                DataFrameFilterFactory[] filters = (DataFrameFilterFactory[])Clipboard.GetData(NODEFILTER_DATA);

                if (filters != null)
                {
                    bool overwrite = true;

                    if (config.Filters.Length > 0)
                    {
                        if (MessageBox.Show(this, "Node already contains filters, do you want to overwrite? (Clicking No will append to the list)",
                            "Overwrite Filters?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            overwrite = false;
                        }
                    }

                    if (overwrite)
                    {
                        config.Filters = filters;
                    }
                    else
                    {
                        IDataFrameFilterFactory[] oldFilters = config.Filters;
                        int len = oldFilters.Length;
                        Array.Resize(ref oldFilters, len + filters.Length);
                        Array.Copy(filters, 0, oldFilters, len, filters.Length);
                        config.Filters = oldFilters;
                    }
                }
            }
        }

        private void copyPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphNode node = netEditor.SelectedObject as GraphNode;

            if (node != null)
            {
                BaseNodeConfig config = (BaseNodeConfig)node.Tag;

                Clipboard.SetData(NODEPROPERTIES_DATA, config.Properties);
            }
        }

        private void pastePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphNode node = netEditor.SelectedObject as GraphNode;

            if ((node != null) && Clipboard.ContainsData(NODEPROPERTIES_DATA))
            {
                BaseNodeConfig config = (BaseNodeConfig)node.Tag;

                Dictionary<string, string> props = (Dictionary<string, string>)Clipboard.GetData(NODEPROPERTIES_DATA);

                if (props != null)
                {
                    config.Properties = props;
                }
            }
        }

        private void toggleEnableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphNode node = netEditor.SelectedObject as GraphNode;

            if (node != null)
            {
                BaseNodeConfig config = (BaseNodeConfig)node.Tag;
                config.Enabled = !config.Enabled;
            }
        }

        private void netEditor_MouseDown(object sender, MouseEventArgs e)
        {
            // Capture mouse position when right clicking for context menu
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                _currMousePos = new PointF(e.X, e.Y);
            }
        }

        private void netEditor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GraphNode node = netEditor.SelectedObject as GraphNode;

                if (node != null)
                {
                    BaseNodeConfig config = (BaseNodeConfig)node.Tag;

                    PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(config);

                    foreach (PropertyDescriptor desc in coll)
                    {
                        if (typeof(IDocumentObject).IsAssignableFrom(desc.PropertyType))
                        {
                            object o = config;
                            ICustomTypeDescriptor custom = config as ICustomTypeDescriptor;
                            if (custom != null)
                            {
                                o = custom.GetPropertyOwner(desc);
                            }

                            IDocumentObject document = (IDocumentObject)desc.GetValue(o);
                            if(document == null)
                            {
                                if (MessageBox.Show(this, Resources.ResourceManager.GetString("NetGraphDocumentControl_NoDocumentSet", GeneralUtils.GetCurrentCulture()),
                                    Resources.ResourceManager.GetString("NetGraphDocumentControl_NoDocumentSetCaption", GeneralUtils.GetCurrentCulture()),
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    // Create document
                                    if (desc.PropertyType == typeof(ScriptDocument))
                                    {
                                        using (SelectScriptTemplateForm frm = new SelectScriptTemplateForm())
                                        {
                                            if (frm.ShowDialog(this) == DialogResult.OK)
                                            {
                                                ScriptDocument script = CANAPEProject.CurrentProject.CreateDocument<ScriptDocument>(frm.Template.TypeName);
                                                script.Script = frm.Template.GetText();
                                                document = script;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        document = CANAPEProject.CurrentProject.CreateDocument(desc.PropertyType);
                                    }
                                }
                            }

                            if(document != null)
                            {
                                DocumentControl.Show(document);
                                desc.SetValue(o, document);

                                // Reset selected objects
                                propertyGrid.SelectedObjects = propertyGrid.SelectedObjects;                                
                            }

                            break;
                        }
                    }
                }
            }
        }

        private static DataFrameFilterFactory GetFilterForValue(DataValue value)
        {
            SetDataFrameFilterFactory ret;

            if (value.Value is byte[])
            {
                ret = new BinaryDataFrameFilterFactory() { Match = value.Value as byte[] };
            }
            else
            {
                ret = new StringDataFrameFilterFactory() { Match = value.Value.ToString() };
            }

            ret.Path = value.Path;
            ret.SearchMode = DataFrameFilterSearchMode.Equals;

            return ret;
        }

        private static void CopyAsFilter(DataKey root, List<DataFrameFilterFactory> exp)
        {
            foreach (DataNode node in root.SubNodes)
            {
                DataValue value = node as DataValue;

                if (value != null)
                {
                    exp.Add(GetFilterForValue(value));
                }
                else
                {
                    DataKey key = node as DataKey;
                    if (key != null)
                    {
                        CopyAsFilter(key, exp);
                    }
                }
            }
        }

        private static void CopyFilters(DataFrameFilterFactory[] filters)
        {
            Clipboard.SetData(NetGraphDocumentControl.NODEFILTER_DATA, filters);
        }

        public static void CopyAsFilter(DataNode node)
        {
            DataKey key = node as DataKey;
            if (key != null)
            {
                CopyFilters(DataFrameFilterFactory.CreateFilter(key).ToArray());
            }
            else
            {
                DataValue value = node as DataValue;
                if (value != null)
                {
                    CopyFilters(new DataFrameFilterFactory[] { DataFrameFilterFactory.CreateFilter(value) });
                }
            }
        } 

        public static void CopyAsFilter(DataFrame frame)
        {
            CopyFilters(DataFrameFilterFactory.CreateFilter(frame).ToArray());
        }

        private void DeleteByConfig(BaseNodeConfig config)
        {
            GraphNode n = netEditor.GetNodeByTag(config);

            if (n != null)
            {
                netEditor.DeleteNode(n);
            }
        }

        private void netEditor_NodeDeleted(object sender, NodeDeletedEventArgs e)
        {
            BaseNodeConfig config = e.Node.Tag as BaseNodeConfig;

            if (config != null)
            {                
                config.Delete();
                if (config is MasterLayerNodeConfig)
                {
                    MasterLayerNodeConfig masterConfig = config as MasterLayerNodeConfig;
                    DeleteByConfig(masterConfig.Slave);
                }
                else if (config is SlaveLayerNodeConfig)
                {
                    SlaveLayerNodeConfig slaveConfig = config as SlaveLayerNodeConfig;
                    DeleteByConfig(slaveConfig.Master);
                }
            }
        }

        private void autoLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _document.AutoLayout();

            PopulateGraphFromDocument(true);
        }

        private void centreGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            netEditor.CenterViewOfGraph();
        }

        private void centreOnNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphNode node = netEditor.SelectedObject as GraphNode;

            if (node != null)
            {
                BaseNodeConfig config = (BaseNodeConfig)node.Tag;

                netEditor.CenterViewOnNode(node);
            }
        }

    }
}
