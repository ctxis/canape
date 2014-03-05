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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Forms;
using CANAPE.DataAdapters;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Net;
using CANAPE.Net.Utils;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    public partial class InjectPacketControl : UserControl
    {
        ProxyNetworkService _service;
        InjectPacketControlConfig _config;
        NetGraph _injectGraph;

        /// <summary>
        /// A configuration object for the packet control
        /// </summary>
        [Serializable]
        public sealed class InjectPacketControlConfig : IDeserializationCallback
        {
            private bool _enableScripting;
            private Guid _scriptDocumentId;
            private string _scriptDocumentClass;
            private bool _enablePacketDelay;
            private long _packetDelayMs;

            public LogPacketCollection Packets { get; private set; }

            public bool EnableScripting
            {
                get { return _enableScripting; }
                set
                {
                    if (_enableScripting != value)
                    {
                        _enableScripting = value;
                        OnConfigChanged();
                    }
                }
            }

            public Guid ScriptDocumentId
            {
                get { return _scriptDocumentId; }
                set
                {
                    if (_scriptDocumentId != value)
                    {
                        _scriptDocumentId = value;
                        OnConfigChanged();
                    }
                }
            }

            public string ScriptDocumentClass
            {
                get { return _scriptDocumentClass; }
                set
                {
                    if(_scriptDocumentClass != value)
                    {
                        _scriptDocumentClass = value;
                        OnConfigChanged();
                    }
                }
            }

            public bool EnablePacketDelay
            {
                get { return _enablePacketDelay; }
                set
                {
                    if (_enablePacketDelay != value)
                    {
                        _enablePacketDelay = value;
                        OnConfigChanged();
                    }
                }
            }

            public long PacketDelayMs
            {
                get { return _packetDelayMs; }
                set
                {
                    if (_packetDelayMs != value)
                    {
                        _packetDelayMs = value;
                        OnConfigChanged();
                    }
                }
            }

            public InjectPacketControlConfig()
            {
                Packets = new LogPacketCollection();                
            }

            public event EventHandler ConfigChanged;

            #region IDeserializationCallback Members

            public void OnDeserialization(object sender)
            {
                Packets.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Packets_CollectionChanged);
                Packets.FrameModified += new EventHandler(Packets_FrameModified);
            }

            private void OnConfigChanged()
            {
                if (ConfigChanged != null)
                {
                    ConfigChanged(this, new EventArgs());
                }
            }

            void Packets_FrameModified(object sender, EventArgs e)
            {
                OnConfigChanged();
            }

            void Packets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                OnConfigChanged();
            }

            #endregion
        }

        public InjectPacketControl()
        {            
            InitializeComponent();
            scriptSelectionControl.ScriptTypes = NodeClassChoiceConverter.GetTypes();
            btnInject.Text = CANAPE.Properties.Resources.InjectPacketControl_InjectButtonText;
            Config = new InjectPacketControlConfig();            
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProxyNetworkService Service 
        {
            get { return _service; }
            set
            {
                _service = value;                
            }
        }

        private void SetupConfig()
        {
            logPacketControl.SetPackets(_config.Packets);
            checkBoxPacketDelay.Checked = _config.EnablePacketDelay;
            checkBoxScript.Checked = _config.EnableScripting;
            scriptSelectionControl.Document = CANAPEProject.CurrentProject.GetDocumentByUuid(_config.ScriptDocumentId) as ScriptDocument;
            scriptSelectionControl.ClassName = _config.ScriptDocumentClass;
            numericUpDownPacketDelay.Value = _config.PacketDelayMs >= 0 ? _config.PacketDelayMs : 0;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public InjectPacketControlConfig Config
        {
            get { return _config; }
            set
            {
                if (_config != value)
                {
                    _config = value ?? new InjectPacketControlConfig();
                    SetupConfig();
                }
            }
        }

        private void CancelInject()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(CancelInject));
            }
            else
            {
                timerCancel.Stop();

                if (_injectGraph != null)
                {
                    ((IDisposable)_injectGraph).Dispose();
                    _injectGraph = null;
                }

                btnInject.Text = CANAPE.Properties.Resources.InjectPacketControl_InjectButtonText;
            }            
        }
        
        private void btnInject_Click(object sender, EventArgs e)
        {
            if (_injectGraph != null)
            {
                CancelInject();
            }
            else
            {
                try
                {
                    NetGraph selectedGraph = comboBoxConnection.SelectedItem as NetGraph;

                    while ((selectedGraph == null) || (selectedGraph.CheckShutdown()))
                    {
                        PopulateConnections();

                        if (comboBoxConnection.Items.Count == 0)
                        {
                            selectedGraph = null;
                            break;
                        }

                        comboBoxConnection.SelectedItem = comboBoxConnection.Items[0];

                        selectedGraph = comboBoxConnection.SelectedItem as NetGraph;
                    }

                    if (selectedGraph != null)
                    {
                        if (logPacketControl.Packets.Length > 0)
                        {
                            if (comboBoxNodes.SelectedItem != null)
                            {
                                int repeatCount = (int)numericRepeatCount.Value;
                                BasePipelineNode node = (BasePipelineNode)comboBoxNodes.SelectedItem;

                                LogPacket[] basePackets = checkBoxInjectSelected.Checked ? logPacketControl.SelectedPackets : logPacketControl.Packets;

                                List<LogPacket> packets = new List<LogPacket>();

                                for (int i = 0; i < repeatCount; ++i)
                                {
                                    packets.AddRange((LogPacket[])GeneralUtils.CloneObject(basePackets));
                                }

                                NetGraphBuilder builder = new NetGraphBuilder();

                                ClientEndpointFactory client = builder.AddClient("client", Guid.NewGuid());
                                ServerEndpointFactory server = builder.AddServer("server", Guid.NewGuid());
                                DynamicNodeFactory dyn = null;                                
                                
                                BaseNodeFactory startNode = client;

                                if (_config.EnablePacketDelay && (_config.PacketDelayMs > 0))
                                {
                                    DelayNodeFactory delay = builder.AddNode(new DelayNodeFactory("delay", Guid.NewGuid()) { PacketDelayMs = (int)_config.PacketDelayMs });
                                    builder.AddLine(startNode, delay, null);
                                    startNode = delay;
                                }

                                if (_config.EnableScripting && _config.ScriptDocumentId != Guid.Empty && !String.IsNullOrWhiteSpace(_config.ScriptDocumentClass))
                                {
                                    ScriptDocument doc = CANAPEProject.CurrentProject.GetDocumentByUuid(_config.ScriptDocumentId) as ScriptDocument;

                                    if (doc != null)
                                    {
                                        dyn = new DynamicNodeFactory("dyn", Guid.NewGuid(), doc.Container, _config.ScriptDocumentClass, null);

                                        builder.AddNode(dyn);
                                        builder.AddLine(startNode, dyn, null);
                                        startNode = dyn;
                                    }
                                }

                                builder.AddLine(startNode, server, null);

                                _injectGraph = builder.Factory.Create(selectedGraph.Logger, selectedGraph, selectedGraph.GlobalMeta,
                                    new MetaDictionary(), new PropertyBag("root"));

                                QueuedDataAdapter inputAdapter = new QueuedDataAdapter();
                                foreach (LogPacket p in packets)
                                {
                                    inputAdapter.Enqueue(p.Frame);
                                }
                                inputAdapter.StopEnqueue();

                                _injectGraph.BindEndpoint(client.Id, inputAdapter);


                                _injectGraph.BindEndpoint(server.Id, new DelegateDataAdapter(
                                    () => this.CancelInject(),
                                    frame => node.Input(frame),
                                    null
                                    ));

                                // Start injection
                                (_injectGraph.Nodes[client.Id] as IPipelineEndpoint).Start();

                                // Check if the dynamic node was an endpoint (so a generator), start as well
                                if ((dyn != null) && (_injectGraph.Nodes[dyn.Id] is PipelineEndpoint))
                                {
                                    (_injectGraph.Nodes[dyn.Id] as IPipelineEndpoint).Start();
                                }

                                // Start cancel timer
                                timerCancel.Start();

                                btnInject.Text = CANAPE.Properties.Resources.InjectPacketControl_CancelButtonText;
                            }
                            else
                            {
                                MessageBox.Show(this, CANAPE.Properties.Resources.InjectPacketForm_SelectNode,
                                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, CANAPE.Properties.Resources.InjectPacketForm_SelectGraph,
                            CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show(this, CANAPE.Properties.Resources.InjectPacketForm_NotSupported,
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(this, ex.Message,
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (NodeFactoryException ex)
                {
                    MessageBox.Show(this, ex.Message,
                        CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddGraphToComboBox(NetGraph graph)
        {            
            foreach (KeyValuePair<Guid, BasePipelineNode> pair in graph.Nodes)
            {                
                if (!pair.Value.Hidden)
                {
                    if (pair.Value is NetGraphContainerNode)
                    {
                        AddGraphToComboBox(((NetGraphContainerNode)pair.Value).ContainedGraph);
                    }
                    else if (pair.Value is LayerSectionNode)
                    {
                        LayerSectionNode node = pair.Value as LayerSectionNode;

                        if (node.IsMaster)
                        {
                            foreach (NetGraph subGraph in node.Graphs)
                            {
                                AddGraphToComboBox(subGraph);
                            }
                        }
                    }
                    else
                    {
                        comboBoxNodes.Items.Add(pair.Value);
                    }
                }
            }
        }

        private void comboBoxConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            NetGraph selected = comboBoxConnection.SelectedItem as NetGraph;

            if (selected != null)
            {
                BasePipelineNode selectedNode = comboBoxNodes.SelectedItem as BasePipelineNode;

                comboBoxNodes.Items.Clear();
                AddGraphToComboBox(selected);

                if (selectedNode != null)
                {
                    foreach (BasePipelineNode node in comboBoxNodes.Items)
                    {
                        if (node.Name == selectedNode.Name)
                        {
                            comboBoxNodes.SelectedItem = node;
                            break;
                        }
                    }
                }
            }
            else
            {
                comboBoxNodes.Items.Clear();
            }
        }

        private void PopulateConnections()
        {
            comboBoxConnection.Items.Clear();

            if (_service != null)
            {
                NetGraph[] graphs = _service.Connections;

                foreach (NetGraph graph in graphs)
                {
                    comboBoxConnection.Items.Add(graph);
                }
            }
        }

        private void comboBoxConnection_DropDown(object sender, EventArgs e)
        {
            PopulateConnections();
        }

        private void checkBoxScript_CheckedChanged(object sender, EventArgs e)
        {
            scriptSelectionControl.Enabled = checkBoxScript.Checked;
            _config.EnableScripting = checkBoxScript.Checked;
        }

        private void checkBoxPacketDelay_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownPacketDelay.Enabled = checkBoxPacketDelay.Checked;
            _config.EnablePacketDelay = checkBoxPacketDelay.Checked;
        }

        private void numericUpDownPacketDelay_ValueChanged(object sender, EventArgs e)
        {
            _config.PacketDelayMs = (long)numericUpDownPacketDelay.Value;
        }

        private void scriptSelectionControl_ClassNameChanged(object sender, EventArgs e)
        {
            _config.ScriptDocumentClass = scriptSelectionControl.ClassName;
        }

        private void scriptSelectionControl_DocumentChanged(object sender, EventArgs e)
        {
            if (scriptSelectionControl.Document != null)
            {
                _config.ScriptDocumentId = scriptSelectionControl.Document.Uuid;
            }
            else
            {
                _config.ScriptDocumentId = Guid.Empty;
            }
        }

        private void timerCancel_Tick(object sender, EventArgs e)
        {
            CancelInject();
        }
    }
}
