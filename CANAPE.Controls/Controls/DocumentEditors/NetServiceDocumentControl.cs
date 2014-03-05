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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Net;
using CANAPE.Extension;
using CANAPE.Forms;
using CANAPE.Net;
using CANAPE.Nodes;
using CANAPE.Utils;
using System.Threading;
using CANAPE.Security;
using System.Threading.Tasks;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class NetServiceDocumentControl : UserControl
    {
        private const string PACKETLOG_CONFIG = "PacketLogControlConfig";

        private class FileRedirectLogEntry
        {
            public string Name { get; private set; }
            public FilePacketLogger Logger { get; private set; }

            public FileRedirectLogEntry(string name, string fileName)
            {
                Name = name;
                Logger = new FilePacketLogger(fileName);
            }
        }

        protected ProxyNetworkService _service;
        protected ProxyNetworkService _repeaterService;
        protected NetServiceDocument _document;
        protected Control _serviceControl;
        private IReadOnlyControl _readOnlyControl;
        protected int _connId;
        private bool _enableRedirect;
        private Dictionary<Guid, FileRedirectLogEntry> _fileLogs;
        private long _currentLogIndex;        

        public static bool PacketLogMutable { get; set; }

        private string _defaultGraph;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool ret = true;

            switch(keyData)
            {
                case Keys.F5:
                    btnStart.PerformClick();
                    break;
                default:
                    ret = base.ProcessCmdKey(ref msg, keyData);
                    break;
            }

            return ret;
        }

        private Type GetControlTypeForDocument(NetServiceDocument document)
        {
            foreach(var ext in DocumentEditorManager.Instance.GetExtensions())
            {
                DocumentEditorAttribute attr = (DocumentEditorAttribute)ext.ExtensionAttribute;

                if (ext.ExtensionAttribute.DocumentType == document.GetType())
                {
                    return ext.ExtensionType;
                }
            }

            if (document is FixedProxyDocument)
            {
                return typeof(FixedProxyControl);
            }
            else if (document is SocksProxyDocument)
            {
                return typeof(GenericProxyControl);
            }
            else if (document is HttpProxyDocument)
            {
                return typeof(GenericProxyControl);
            }
            else if (document is NetServerDocument)
            {
                return typeof(NetServerControl);
            }
            else if (document is NetAutoClientDocument)
            {
                return typeof(NetAutoClientControl);
            }
            else if (document is FullHttpProxyDocument)
            {
                return typeof(GenericProxyControl);
            }

            throw new ArgumentException("Unknown document type");
        }

        public NetServiceDocumentControl(NetServiceDocument document)
        {
            
            InitializeComponent();
            if (components == null)
            {
                components = new Container();
            }

            _serviceControl = (Control)Activator.CreateInstance(GetControlTypeForDocument(document), document);            
            _readOnlyControl = _serviceControl as IReadOnlyControl;
            panelSettings.Controls.Add(_serviceControl);
            _serviceControl.Dock = DockStyle.Fill;
            _document = document;
            packetLogControl.LogName = _document.Name;
            packetLogControl.ReadOnly = !PacketLogMutable;
            packetLogControl.SetPackets(_document.Packets);
            metaEditorControl.Meta = _document.GlobalMeta;
            networkHistoryControl.SetDocument(_document);
            _defaultGraph = "Default";
            _fileLogs = new Dictionary<Guid, FileRedirectLogEntry>();
            Disposed += new EventHandler(NetServiceControl_Disposed);
            credentialsEditorControl.SetCredentials(_document.Credentials);

            PacketLogControlConfig config = document.GetProperty(PACKETLOG_CONFIG) as PacketLogControlConfig;

            if (config != null)
            {
                packetLogControl.Config = config;
            }

            Text = _document.Name;
            UpdateNetgraphs();

            comboBoxNetgraph.SelectedItem = null;

            foreach (object item in comboBoxNetgraph.Items)
            {
                NetGraphDocument doc = item as NetGraphDocument;
                if (doc == _document.NetGraph)
                {
                    comboBoxNetgraph.SelectedItem = item;
                    break;
                }
            }
        }

        void NetServiceControl_Disposed(object sender, EventArgs e)
        {
            StopService();
        }

        EditPacketForm CreateEditor(EditPacketEventArgs e)
        {
            if (InvokeRequired)
            {
                return (EditPacketForm)Invoke(new Func<EditPacketEventArgs, EditPacketForm>(CreateEditor), e);
            }
            else
            {
                EditPacketForm frm = new EditPacketForm();

                frm.Frame = e.Frame;
                frm.Selector = e.SelectPath;
                frm.DisplayColor = ColorValueConverter.ToColor(e.Color);
                frm.DisplayTag = e.Tag;
                frm.ShowDisableEditor = true;
                frm.ShowReadOnly = true;

                frm.Show();

                frm.Activate();

                return frm;

            }
        }

        void DisposeEditor(EditPacketForm frm)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<EditPacketForm>(DisposeEditor), frm);
            }

            else
            {
                frm.Dispose();
            }
        }

        void _service_EditPacketEvent(object sender, EditPacketEventArgs e)
        {
            EditPacketForm frm = CreateEditor(e);

            try
            {
                frm.WaitEvent.WaitOne();

                if (frm.DialogResult == DialogResult.OK)
                {
                    e.Frame = frm.Frame;
                }

                if (frm.DisableEditor)
                {
                    if (e.Sender != null)
                    {
                        e.Sender.Enabled = false;
                    }
                }
            }
            finally
            {
                DisposeEditor(frm);
            }
        }

        void _service_NewConnectionEvent(object sender, ConnectionEventArgs e)
        {
            AddConnection(e.Graph);
        }

        private void UpdateNetgraphs()
        {
            var documents = CANAPEProject.CurrentProject.GetDocumentsByType(typeof(NetGraphDocument));

            object selected = comboBoxNetgraph.SelectedItem;
            bool foundItem = false;

            comboBoxNetgraph.Items.Clear();
            comboBoxNetgraph.Items.Add(_defaultGraph);
            
            foreach (var doc in documents)
            {
                if (doc == selected)
                {
                    foundItem = true;
                }

                comboBoxNetgraph.Items.Add(doc);
            }

            if (foundItem)
            {
                comboBoxNetgraph.SelectedItem = selected;
            }
            else
            {
                comboBoxNetgraph.SelectedItem = _defaultGraph;
            }
        }

        void AddGraph(NetGraph graph)
        {
            ListViewItem item = listViewConns.Items.Add(_connId.ToString());
            _connId++;
            TimeSpan currentSpan = new TimeSpan(DateTime.UtcNow.Ticks);
            item.SubItems.Add(graph.NetworkDescription);
            item.SubItems.Add(String.Format("{0}s", (long)currentSpan.Subtract(graph.CreatedTicks).TotalSeconds));
            item.Tag = graph;
        }

        void UpdateGraph(ListViewItem item)
        {
            NetGraph graph = item.Tag as NetGraph;
            TimeSpan currentSpan = new TimeSpan(DateTime.UtcNow.Ticks);
            item.SubItems[2].Text = String.Format("{0}s", (long)currentSpan.Subtract(graph.CreatedTicks).TotalSeconds);
        }

        void UpdateConnections()
        {
            listViewConns.SuspendLayout();

            foreach (ListViewItem item in listViewConns.Items)
            {
                UpdateGraph(item);
            }

            listViewConns.ResumeLayout();
        }

        void AddConnection(NetGraph netGraph)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<NetGraph>(AddConnection), netGraph);
            }
            else
            {
                AddGraph(netGraph);
            }
        }

        void RemoveConnection(NetGraph graph)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<NetGraph>(RemoveConnection), graph);
            }
            else
            {
                foreach (ListViewItem item in listViewConns.Items)
                {
                    if (item.Tag == graph)
                    {
                        listViewConns.Items.Remove(item);
                        break;
                    }
                }
            }            
        }

        private string GetDescriptionString(NetGraph graph)
        {
            if (graph.Meta.ContainsKey("src") && graph.Meta.ContainsKey("dest"))
            {
                return String.Format("{0} -> {1}", graph.Meta["src"].ToString(), graph.Meta["dest"].ToString());
            }
            else
            {
                return "Unknown";
            }
        }

        private void StopService()
        {
            bool stopped = false;

            try
            {
                if (_service != null)
                {                    
                    _service.Stop();
                    stopped = true;
                }                
            }
            catch (Exception ex)
            {
                eventLogControl.Logger.LogException(ex);
            }
            finally
            {
                if (!stopped)
                {
                    OnServiceStop();
                }
            }
        }

        private void StartService()
        {
            if (checkBoxClearOnStart.Checked)
            {
                _document.GlobalMeta.Clear();
            }

            _service = _document.Create(eventLogControl.Logger);
            _service.EditPacketEvent += new EventHandler<EditPacketEventArgs>(_service_EditPacketEvent);
            _service.FilterLogPacketEvent += new EventHandler<FilterPacketLogEventArgs>(_service_FilterLogPacketEvent);
            _service.NewConnectionEvent += new EventHandler<ConnectionEventArgs>(_service_NewConnectionEvent);
            _service.CloseConnectionEvent += new EventHandler<ConnectionEventArgs>(_service_CloseConnectionEvent);
            _service.ServiceStoppedEvent += new EventHandler(_service_ServiceStoppedEvent);
            _service.ResolveCredentials += ResolveCredentials;
            _service.Start();
            netGraphNodesControl.Service = _service;
            injectPacketControl.Service = _service;
        }

        void OnServiceStop()
        {
            CloseLogFiles();

            DocumentControl.SetIcon(_document, null, null);

            if (!IsDisposed)
            {
                netGraphNodesControl.Service = null;
                listViewConns.Items.Clear();
                btnStart.Text = "Start";                
                comboBoxNetgraph.Enabled = true;
                EnableControl(true);
                _service = null;
            }
        }

        void _service_ServiceStoppedEvent(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnServiceStop));
            }
            else
            {
                OnServiceStop();
            }
        }

        void _service_FilterLogPacketEvent(object sender, FilterPacketLogEventArgs e)
        {
            if (_enableRedirect)
            {
                if (redirectLogControl.Mode == RedirectLogControl.RedirectLogMode.ToFile)
                {
                    FilePacketLogger logger = GetLogFile(e.Graph);
                    if (logger != null)
                    {
                        logger.AddPacket(e.Packet);
                    }
                }
                e.Filter = true;
            }
        }

        private void RemoveFileLog(NetGraph g)
        {
            if (g != null)
            {
                lock (_fileLogs)
                {
                    if (_fileLogs.ContainsKey(g.Uuid))
                    {
                        _fileLogs[g.Uuid].Logger.Dispose();
                        _fileLogs.Remove(g.Uuid);
                    }
                }
            }
        }

        void _service_CloseConnectionEvent(object sender, ConnectionEventArgs e)
        {
            RemoveFileLog(e.Graph);
            RemoveConnection(e.Graph);
        }

        private FilePacketLogger GetLogFile(NetGraph graph)
        {
            FilePacketLogger logger = null;

            lock (_fileLogs)
            {
                if (_fileLogs.ContainsKey(graph.Uuid))
                {
                    logger =_fileLogs[graph.Uuid].Logger;
                }
                else 
                {
                    long currLogIndex = Interlocked.Increment(ref _currentLogIndex);
                    string fileName = Path.Combine(redirectLogControl.BaseDirectory, 
                        String.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), 
                        GeneralUtils.SanitizeFilename(graph.NetworkDescription, '_')+currLogIndex.ToString()+".log"));

                    try
                    {
                        FileRedirectLogEntry redirect = new FileRedirectLogEntry(graph.NetworkDescription, fileName);
                        _fileLogs.Add(graph.Uuid, redirect);

                        logger = redirect.Logger;
                    }
                    catch (IOException ex)
                    {
                        eventLogControl.Logger.LogException(ex);
                    }
                }
            }

            return logger;
        }

        private void CloseLogFiles()
        {
            lock (_fileLogs)
            {
                foreach (KeyValuePair<Guid, FileRedirectLogEntry> pair in _fileLogs)
                {
                    try
                    {
                        pair.Value.Logger.Dispose();
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }

        private void comboBoxNetgraph_DropDown(object sender, EventArgs e)
        {
            UpdateNetgraphs();
        }

        private void comboBoxNetgraph_SelectionChangeCommitted(object sender, EventArgs e)
        {            
            _document.NetGraph = comboBoxNetgraph.SelectedItem as NetGraphDocument;
        }

        private void tabPageConns_Enter(object sender, EventArgs e)
        {
            UpdateConnections();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateConnections();
        }

        private void killConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewConns.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewConns.SelectedItems[0];
                NetGraph graph = item.Tag as NetGraph;

                if (_service != null)
                {
                    _service.CloseConnection(graph);
                }
                else
                {
                    // If no service, then just destroy, shouldn't be here anyway
                    try
                    {
                        ((IDisposable)graph).Dispose();
                    }
                    catch
                    { }
                    item.Remove();
                }
            }
        }

        private void EnableControl(bool bEnable)
        {
            if (_readOnlyControl != null)
            {
                _readOnlyControl.ReadOnly = !bEnable;
            }
            else
            {
                _serviceControl.Enabled = bEnable;
            }

            checkBoxRedirect.Enabled = bEnable;
            redirectLogControl.Enabled = bEnable;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (_service == null)
                {
                    StartService();
                    DocumentControl.SetIcon(_document, "NetService_Started", Properties.Resources.Network_Map_Started);
                    btnStart.Text = "Stop";
                    comboBoxNetgraph.Enabled = false;
                    EnableControl(false);
                }
                else
                {
                    StopService();
                }
            }
            catch (NetServiceException ex)
            {
                Exception dispEx = ex;

                _service = null;

                if (ex.InnerException != null)
                {
                    dispEx = ex.InnerException;
                }

                MessageBox.Show(this, dispEx.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void checkBoxRedirect_CheckedChanged(object sender, EventArgs e)
        {
            _enableRedirect = checkBoxRedirect.Checked;
            redirectLogControl.Enabled = checkBoxRedirect.Checked;
        }

        private void ResolveCredentials(object sender, ResolveCredentialsEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<ResolveCredentialsEventArgs>(ResolveCredentials), sender, e);
            }
            else
            {                
                if (e.Principal.PrincipalType == typeof(AuthenticationCredentials))
                {
                    if (_document.Credentials.ContainsKey(e.Principal))
                    {
                        e.Result = new ResolveCredentialsResult(_document.Credentials[e.Principal], false);
                    }
                    else
                    {
                        using (GetAuthenticationCredentialsForm frm = new GetAuthenticationCredentialsForm(e.Principal))
                        {
                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                AuthenticationCredentials creds = new AuthenticationCredentials(frm.Username, frm.Domain, frm.Password);

                                e.Result = new ResolveCredentialsResult(creds, frm.SaveCreds && frm.SessionOnly);

                                if (frm.SaveCreds && !frm.SessionOnly)
                                {
                                    _document.Credentials[e.Principal] = creds;
                                    credentialsEditorControl.UpdateCredentials();
                                }
                            }
                        }
                    }
                }                
            }
        }

        private void packetLogControl_ConfigChanged(object sender, EventArgs e)
        {
            _document.SetProperty(PACKETLOG_CONFIG, packetLogControl.Config);
        }

        private void credentialsEditorControl_CredentialsUpdated(object sender, EventArgs e)
        {
            _document.Dirty = true;
        }
    }
}
