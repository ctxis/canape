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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CANAPE.Controls.DocumentEditors;
using CANAPE.DataFrames;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net;
using CANAPE.Extension;
using CANAPE.Forms;
using CANAPE.NodeLibrary;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    public partial class PacketLogControl : UserControl, IPacketLogViewer
    {
        public const string DEFAULT_CONFIG_NAME = "DefaultPacketLogControlConfig";

        private class SortedLogPacketFacade
        {
            LogPacketCollection _packets;
            List<int> _remapTable;

            public SortedLogPacketFacade(LogPacketCollection packets)
            {
                _packets = packets;
                _remapTable = new List<int>();
            }

            public int GetSortedIndex(int index)
            {
                if(_remapTable.Count > index)
                {
                    return _remapTable[index];
                }

                return index;
            }

            public void Sort(PacketLogColumn col)
            {             
            }
        }

        // Thread safety is only through the use of the message pump
        LogPacketCollection _packets;
        bool _autoScroll;
        PacketLogColumn _currentCol;
        bool _sortAscending;
        PacketLogControlConfig _config;

        public PacketLogControl()
        {
            // Create a default list
            _packets = new LogPacketCollection();
            _autoScroll = false;
            _currentCol = null;
            components = new Container();
            
            InitializeComponent();

            UpdateConfig(DocumentControl.GetConfigItem<PacketLogControlConfig>(DEFAULT_CONFIG_NAME, true));
        }

        public bool IsInFindForm { get; set; }

        private void UpdateConfig(PacketLogControlConfig config)
        {
            _config = config;

            listLogPackets.SuspendLayout();
            listLogPackets.Columns.Clear();

            foreach (PacketLogColumn col in _config.Columns)
            {
                ColumnHeader header = listLogPackets.Columns.Add(col.Name, col.ColumnWidth);
                header.Tag = col;
                
            }

            if (_config.DefaultFont != null)
            {
                listLogPackets.Font = _config.DefaultFont;
            }

            _autoScroll = _config.AutoScroll;
            autoScrollToolStripMenuItem.Checked = _autoScroll;

            listLogPackets.ResumeLayout();
            listLogPackets.Invalidate();
        }

        /// <summary>
        /// The configuration for the packet log
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public PacketLogControlConfig Config
        {
            get { return _config; }
            set 
            { 
                UpdateConfig(value);
            }
        }

        /// <summary>
        /// Used when in a find form, indicates the parent control so you can cross reference
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public PacketLogControl ParentLog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        public void SelectLogPacket(LogPacket packet)
        {
            int foundIndex = -1;

            lock (_packets)
            {
                int packetCount = _packets.Count;

                for (int i = 0; i < packetCount; ++i)
                {
                    if (_packets[i].Uuid == packet.Uuid)
                    {
                        foundIndex = i;
                        break;
                    }
                }
            }

            if (foundIndex >= 0)
            {
                try
                {
                    listLogPackets.SelectedIndices.Clear();
                    listLogPackets.SelectedIndices.Add(foundIndex);
                    listLogPackets.EnsureVisible(foundIndex);

                    Form topLevelForm = this.TopLevelControl as Form;

                    if (topLevelForm != null)
                    {
                        topLevelForm.Activate();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    // foundIndex wasn't actually in the list
                }
            }
        }
        
        /// <summary>
        /// Set the backing packet store with an existing packet collection
        /// </summary>
        /// <param name="packets"></param>
        public void SetPackets(LogPacketCollection packets)
        {
            _packets = packets;
        }

        public void SetPackets(LogPacket[] packets)
        {
            _packets = new LogPacketCollection(packets);
        }

        public class LogEventArgs : EventArgs
        {
            public LogPacket p { get; private set; }
            public LogEventArgs(LogPacket p)
                : base()
            {
                this.p = p;
            }
        }

        private LogPacket[] GetPackets(LogPacket selectedPacket, Func<LogPacket, LogPacket, bool> selectFunc)
        {
            lock (_packets)
            {
                List<LogPacket> ret = new List<LogPacket>();
                foreach (LogPacket p in _packets)
                {
                    if (selectFunc(selectedPacket, p))
                    {
                        ret.Add(p);
                    }
                }

                return ret.ToArray();
            }
        }

        private LogPacket[] GetPackets(bool bSelected)
        {
            LogPacket[] packets = null;

            if (InvokeRequired)
            {
                packets = (LogPacket[])Invoke(new Func<LogPacket[]>(GetPackets));
            }
            else
            {
                lock (_packets)
                {
                    if (bSelected)
                    {
                        List<LogPacket> list = new List<LogPacket>();

                        foreach (int idx in listLogPackets.SelectedIndices)
                        {
                            if ((idx >= 0) && (idx < _packets.Count))
                            {
                                list.Add(_packets[idx]);
                            }
                        }

                        packets = list.ToArray();
                    }
                    else
                    {
                        packets = _packets.ToArray<LogPacket>();
                    }
                }
            }

            return packets;
        }

        private LogPacket[] GetPackets()
        {
            return GetPackets(false);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public LogPacket[] Packets { get { return GetPackets(); } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public LogPacket[] SelectedPackets { get { return GetPackets(true); } }

        public bool ReadOnly
        {
            get;
            set;
        }

        private void AddListEntry(LogPacket p, bool refresh)
        {
            if (refresh)
            {
                listLogPackets.BeginUpdate();
            }

            lock (_packets)
            {
                _packets.Add(p);
            }

            if (refresh)
            {
                RefreshLog();
                listLogPackets.EndUpdate();
            }
        }

        public void AddLogEntry(LogPacket p)
        {
            try
            {
                if (InvokeRequired)
                {
                    AddListEntry(p, false);
                }
                else
                {
                    AddListEntry(p, true);
                }
            }
            catch (Exception ex)
            {
                Logger.GetSystemLogger().LogException(ex);
            }
        }

        private void copyPacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);
            
            if (packets.Length > 0)
            {
                Clipboard.SetData(LogPacket.LogPacketArrayFormat, packets);
            }
        }

        private void EnableMenuForSelection(bool enable)
        {            
            copyPacketsToolStripMenuItem.Enabled = enable;
            convertToBytePacketsToolStripMenuItem.Enabled = enable;
            changeTagToolStripMenuItem.Enabled = enable;
            copyToToolStripMenuItem.Enabled = enable;
            modifyToolStripMenuItem.Enabled = enable;
            copyToToolStripMenuItem.Enabled = enable;
        }

        private void SetMenuForReadOnly(bool visible)
        {
            modifyToolStripMenuItem.Visible = visible;
            importPacketsToolStripMenuItem.Visible = visible;
            addToolStripMenuItem.Enabled = visible;
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listLogPackets.SelectedIndices.Count > 0)
            {
                EnableMenuForSelection(true);
                copyToToolStripMenuItem.DropDownItems.Clear();
                GuiUtils.CreatePacketLogCopyItems(copyToToolStripMenuItem.DropDownItems, () => GetPackets(true));
            }
            else
            {
                EnableMenuForSelection(false);
            }

            if (Clipboard.ContainsData(LogPacket.LogPacketArrayFormat) && (ReadOnly == false))
            {
                pastePacketsToolStripMenuItem.Enabled = true;
            }
            else
            {
                pastePacketsToolStripMenuItem.Enabled = false;
            }

            extensionsToolStripMenuItem.DropDownItems.Clear();
            foreach(var ext in PacketLogExtensionManager.Instance.GetExtensions())
            {
                ToolStripItem item = extensionsToolStripMenuItem.DropDownItems.Add(ext.ExtensionAttribute.Name);

                item.Click += new EventHandler(extensionItem_Click);
                item.Tag = ext.ExtensionType;
            }

            extensionsToolStripMenuItem.Visible = extensionsToolStripMenuItem.DropDownItems.Count > 0;

            SetMenuForReadOnly(!ReadOnly);

            GuiUtils.CreateScriptMenuItems(runScriptStripMenuItem.DropDownItems, RunScript_Click);
            if (runScriptStripMenuItem.DropDownItems.Count == 0)
            {
                runScriptStripMenuItem.Visible = false;
            }

            readOnlyToolStripMenuItem.Checked = ReadOnly;
        }

        private class ScriptPacketLogExtension : PacketLogSelectedExtension
        {
            ScriptDocument _document;

            public ScriptPacketLogExtension(ScriptDocument document)
            {
                _document = document;
            }

            protected override void Run(IEnumerable<LogPacket> selectedPackets)
            {
                ScriptUtils.Invoke(_document.Container, null, "ProcessPackets", selectedPackets);
            }
        }

        void RunScript_Click(ScriptDocument script)
        {
            try
            {                
                RunPacketLogExtension(ScriptUtils.GetInstance<IPacketLogExtension>(script.Container) 
                    ?? new ScriptPacketLogExtension(script));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RunPacketLogExtension(IPacketLogExtension ext)
        {
            int[] selectedIndicies = new int[listLogPackets.SelectedIndices.Count];

            listLogPackets.SelectedIndices.CopyTo(selectedIndicies, 0);

            ext.Run(_packets, selectedIndicies);
        }

        void extensionItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem)sender;
                IPacketLogExtension ext = (IPacketLogExtension)Activator.CreateInstance((Type)item.Tag);

                RunPacketLogExtension(ext);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowLog(IList<LogPacket> ps)
        {
            PacketLogViewerForm frm = new PacketLogViewerForm(_packets[listLogPackets.SelectedIndices[0]], ps);

            components.Add(frm);

            frm.ReadOnly = ReadOnly;
            frm.Show();
        }

        private void ShowLog(bool selected)
        {
            if (listLogPackets.SelectedIndices.Count > 0)
            {
                ShowLog(GetPackets(selected));
            }
        }

        private void listLogPackets_DoubleClick(object sender, EventArgs e)
        {
            ShowLog(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearLog()
        {
            listLogPackets.BeginUpdate();
            lock (_packets)
            {
                _packets.Clear();
            }
            RefreshLog();
            listLogPackets.EndUpdate();
        }

        public void RefreshLog()
        {
            if (_packets != null)
            { 
                int count;

                lock (_packets)
                {
                    count = _packets.Count;
                }

                if (listLogPackets.VirtualListSize != count)
                {
                    listLogPackets.SuspendLayout();

                    if (count == 0)
                    {
                        listLogPackets.SelectedIndices.Clear();
                    }
                    else if (count < listLogPackets.VirtualListSize)
                    {
                        List<int> newIndicies = new List<int>();

                        // Only change selected indicies if the list has shrunk

                        for (int i = 0; i < listLogPackets.SelectedIndices.Count; ++i)
                        {
                            if (listLogPackets.SelectedIndices[i] < count)
                            {
                                newIndicies.Add(listLogPackets.SelectedIndices[i]);
                            }
                        }

                        listLogPackets.SelectedIndices.Clear();

                        foreach (int idx in newIndicies)
                        {
                            listLogPackets.SelectedIndices.Add(idx);
                        }
                    }

                    // Don't really want to do this, but for some reason it is a pain to debug what is actually
                    // going wrong, it is inside some non-obvious part of the the Windows.Forms listview class
                    try
                    {
                        listLogPackets.VirtualListSize = count;
                    }
                    catch (NullReferenceException ex)
                    {
                        System.Diagnostics.Trace.WriteLine(ex.ToString());
                    }

                    if (_autoScroll && (count > 0))
                    {
                        listLogPackets.EnsureVisible(count - 1);
                    }

                    listLogPackets.ResumeLayout();
                    listLogPackets.Invalidate();
                }
            }
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listLogPackets.Items.Count > 0)
            {
                if (!GlobalControlConfig.RequirePacketLogClearConfirmation || 
                    MessageBox.Show(CANAPE.Properties.Resources.PacketLogControl_ClearLog, CANAPE.Properties.Resources.PacketLogControl_ClearLogCaption, 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ClearLog();
                }
            }
        }

        private void pastePacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = (LogPacket[])Clipboard.GetData(LogPacket.LogPacketArrayFormat);

            if (packets != null)
            {
                listLogPackets.BeginUpdate();
                lock (_packets)
                {
                    foreach (var p in packets)
                    {
                        _packets.Add(p);
                    }
                }

                RefreshLog();
                listLogPackets.EndUpdate();
            }
        }

        private void addPacketToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            LogPacket p = new LogPacket("Custom", Guid.Empty, "Custom Network", 
                new DataFrame(new byte[0]), ColorValueConverter.FromColor(Color.Azure)); 
            
            AddLogEntry(p);      
        }

        private void convertToBytePacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            LogPacket[] packets = GetPackets(true);

            try
            {
                foreach (LogPacket p in packets)
                {
                    p.Frame.ConvertToBasic();
                }
            }
            catch
            {

            }

            listLogPackets.Invalidate();
        }

        private void mergePacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            if (packets.Length > 0)
            {
                listLogPackets.BeginUpdate();
                listLogPackets.SelectedIndices.Clear();
                int index = -1;

                try
                {
                    lock (_packets)
                    {
                        List<byte> data = new List<byte>();

                        foreach (var p in packets)
                        {
                            data.AddRange(p.Frame.ToArray());
                        }

                        packets[0].Frame.ConvertToBasic(data.ToArray());
                        for (int i = 1; i < packets.Length; i++)
                        {
                            _packets.Remove(packets[i]);
                        }

                        index = _packets.IndexOf(packets[0]);
                    }

                    RefreshLog();

                    if (index >= 0)
                    {
                        listLogPackets.SelectedIndices.Add(index);
                    }
                }
                catch
                { }

                listLogPackets.EndUpdate();
            }            
        }

        private void listLogPackets_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowLog(false);
            }
        }

        delegate void WritePacketDelegate(Stream stm, LogPacket[] p);

        private void SavePacketsToFile(string filter, WritePacketDelegate writePackets)
        {
            LogPacket[] packets = GetPackets(true);

            if (packets.Length > 0)
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = filter;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using (Stream stm = File.Open(dlg.FileName, FileMode.Create, FileAccess.ReadWrite))
                            {
                                writePackets(stm, packets);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format(CANAPE.Properties.Resources.PacketLogControl_ErrorWritingPackets, ex.Message),
                                CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
              
        private void WritePacketsHex(Stream stm, LogPacket[] ps)
        {
            using (TextWriter writer = new StreamWriter(stm, Encoding.UTF8))
            {   
                foreach(LogPacket p in ps)
                {
                    writer.WriteLine("Time {0} - Tag '{1}' - Network '{2}'",
                        p.Timestamp.ToString(), p.Tag, p.Network);

                    writer.WriteLine(GeneralUtils.BuildHexDump(16, p.Frame.ToArray()));
                    writer.WriteLine();
                }
            }
        }

        private void WritePacketsText(Stream stm, LogPacket[] ps)
        {
            using (TextWriter writer = new StreamWriter(stm, Encoding.UTF8))
            {
                foreach (LogPacket p in ps)
                {
                    writer.WriteLine("Time {0} - Tag '{1}' - Network '{2}'",
                    p.Timestamp.ToString(), p.Tag, p.Network);
                    writer.WriteLine(GeneralUtils.MakeByteString(p.Frame.ToArray()));
                }
            }
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePacketsToFile(CANAPE.Properties.Resources.TextFileFilter_String, WritePacketsText);
        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePacketsToFile(CANAPE.Properties.Resources.AllFilesFilter_String, (stm, ps) =>
            {
                foreach (LogPacket p in ps) p.Frame.ToStream(stm);
            });
        }

        private void listLogPackets_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            lock (_packets)
            {
                if ((e.ItemIndex >= 0) && (e.ItemIndex < _packets.Count))
                {
                    LogPacket p = _packets[e.ItemIndex];                    

                    PacketLogColumn col = (PacketLogColumn)listLogPackets.Columns[0].Tag;
                    ListViewItem item = new ListViewItem(col.ToString(p, e.ItemIndex));

                    for(int i = 1; i < listLogPackets.Columns.Count; ++i)
                    {
                        col = (PacketLogColumn)listLogPackets.Columns[i].Tag;
                        item.SubItems.Add(col.ToString(p, e.ItemIndex));
                    }

                    item.BackColor = ColorValueConverter.ToColor(p.Color);
                    item.Tag = p;

                    e.Item = item;
                }
                else
                {
                    // Return a dummy item
                    ListViewItem item = new ListViewItem("0");

                    for(int i = 1; i < listLogPackets.Columns.Count; ++i)
                    {
                        item.SubItems.Add("");
                    }

                    e.Item = item;
                }
            }
        }

        private void deletePacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            listLogPackets.BeginUpdate();

            foreach (var p in packets)
            {
                _packets.Remove(p);
            }

            RefreshLog();

            listLogPackets.EndUpdate();
        }        

        private void serializedFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LogPacket[] packets = GetPackets(true);

                if (packets.Length > 0)
                {
                    using (SaveFileDialog dlg = new SaveFileDialog())
                    {
                        dlg.Filter = CANAPE.Properties.Resources.AllFilesFilter_String;

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            GeneralUtils.SerializeLogPackets(packets, dlg.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(CANAPE.Properties.Resources.PacketLogControl_ErrorWritingPackets, ex.Message),
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void serializedFramesByTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LogPacket[] packets = GetPackets(true);

                if (packets.Length > 0)
                {
                    using (SaveFileDialog dlg = new SaveFileDialog())
                    {
                        dlg.Filter = CANAPE.Properties.Resources.AllFilesFilter_String;

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            Dictionary<string, List<LogPacket>> frameDict
                                = new Dictionary<string, List<LogPacket>>();

                            foreach (var p in packets)
                            {
                                string tag = p.Tag ?? "";
                                List<LogPacket> frames = null;

                                if (frameDict.ContainsKey(tag))
                                {
                                    frames = frameDict[tag];
                                }
                                else
                                {
                                    frames = new List<LogPacket>();
                                    frameDict[tag] = frames;
                                }

                                frames.Add(p);
                            }

                            foreach (var pair in frameDict)
                            {
                                GeneralUtils.SerializeLogPackets(pair.Value.ToArray(), String.Format("{0}.{1}", dlg.FileName, pair.Key));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(CANAPE.Properties.Resources.PacketLogControl_ErrorWritingPackets, ex.Message),
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePacketsToFile(CANAPE.Properties.Resources.TextFileFilter_String, WritePacketsHex);
        }

        private void HideMenuItem(ToolStripMenuItem menuItem)
        {
            menuItem.Enabled = false;
            menuItem.Visible = false;
        }

        private void LogPacketControl_Load(object sender, EventArgs e)
        {
            if (IsInFindForm)
            {
                HideMenuItem(addToolStripMenuItem);
                HideMenuItem(deletePacketsToolStripMenuItem);
                HideMenuItem(clearLogToolStripMenuItem);
                HideMenuItem(autoScrollToolStripMenuItem);
                HideMenuItem(findToolStripMenuItem);
                HideMenuItem(pastePacketsToolStripMenuItem);
                HideMenuItem(importPacketsToolStripMenuItem);
                HideMenuItem(modifyToolStripMenuItem);
            }
            else
            {
                HideMenuItem(inParentLogToolStripMenuItem);
            }

            RefreshLog();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                listLogPackets.BeginUpdate();

                RefreshLog();

                listLogPackets.EndUpdate();
            }
        }

        private void autoScrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _autoScroll = autoScrollToolStripMenuItem.Checked;
            _config.AutoScroll = _autoScroll;
            OnConfigChanged();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalControlConfig.OpenLogFindInWindow)            
            {
                FindDataFrameForm findForm = new FindDataFrameForm();
                findForm.Setup(_packets, this.ReadOnly, this);

                components.Add(findForm);

                findForm.Show(this);
            }
            else
            {
                FindDataFrameControl control = new FindDataFrameControl();
                control.SetupControl(_packets, this.ReadOnly, this); 

                string name = LogName != null ? LogName + " Packet Find" : "Packet Find";

                components.Add(DocumentControl.ShowControl(name, control, this));
            }            
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectPackets(p => true);
        }

        private void listLogPackets_MouseDown(object sender, MouseEventArgs e)
        {            
            listLogPackets.Focus();
        }

        private void changeColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            if (packets.Length > 0)
            {
                using (ColorDialog dlg = new ColorDialog())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var p in packets)
                        {
                            p.Color = ColorValueConverter.FromColor(dlg.Color);
                        }

                        listLogPackets.Invalidate();
                    }
                }
            }
        }

        void SortPackets(PacketLogColumn column, bool ascending)
        {
            lock(_packets)
            {
                LogPacket[] packets = null;

                packets = column.OrderBy(_packets, ascending).ToArray();
                
                _packets.Clear();
                foreach (LogPacket p in packets)
                {
                    _packets.Add(p);
                }
            }
        }

        private void listLogPackets_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            PacketLogColumn col = (PacketLogColumn)listLogPackets.Columns[e.Column].Tag;
                       
            if (col == _currentCol)
            {
                _sortAscending = !_sortAscending;
            }
            else
            {
                _sortAscending = true;
            }

           _currentCol = col;

           SortPackets(col, _sortAscending);                
           listLogPackets.Invalidate();
        }

        void addToExistingDocument_Click(object sender, EventArgs e)
        {
            if (listLogPackets.SelectedIndices.Count > 0)
            {
                ToolStripItem item = sender as ToolStripItem;

                if (item != null)
                {
                    IDocumentObject doc = item.Tag as IDocumentObject;

                    if (doc != null)
                    {
                        AddPacketsToDocument(doc);
                    }
                }
            }
        }

        void AddPacketsToDocument(IDocumentObject doc)
        {
            LogPacket[] packets = GetPackets(true);

            PacketLogDocument packetLog = doc as PacketLogDocument;
            TestDocument testDoc = doc as TestDocument;

            if (packetLog != null)
            {
                foreach (LogPacket packet in packets)
                {
                    packetLog.AddPacket((LogPacket)packet.Clone());
                }
            }
            else if (testDoc != null)
            {
                IEnumerable<LogPacket> newPackets = packets.Select(p => (LogPacket)p.Clone());
                testDoc.AddRangeInputPacket(newPackets);
            }
        }

        void newPacketLog_Click(object sender, EventArgs e)
        {
            if (listLogPackets.SelectedIndices.Count > 0)
            {
                PacketLogDocument doc = CANAPEProject.CurrentProject.CreateDocument<PacketLogDocument>();

                AddPacketsToDocument(doc);

                DocumentControl.Show(doc);
            }
        }

        private void diffPacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listLogPackets.SelectedIndices.Count == 2)
            {
                LogPacket[] packets = GetPackets(true);

                BinaryFrameDiffForm frm = new BinaryFrameDiffForm(packets[0].Frame.Root, packets[1].Frame.Root);

                components.Add(frm);

                frm.Show(this);
            }
            else
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.PacketLogControl_SelectDiffPackets, 
                    CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectInParentLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((ParentLog != null) && (!ParentLog.IsDisposed))
            {
                if (listLogPackets.SelectedIndices.Count > 0)
                {
                    LogPacket[] packets = GetPackets(true);

                    ParentLog.SelectLogPacket(packets[0]);
                }
            }
        }

        private void addPacketFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = CANAPE.Properties.Resources.AllFilesFilter_String;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AddLogEntry(new LogPacket("Custom", Guid.Empty, "Custom Network",
                                new DataFrame(File.ReadAllBytes(dlg.FileName)),
                                ColorValueConverter.FromColor(Color.Azure)));
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(String.Format(CANAPE.Properties.Resources.PacketLogControl_ErrorReadingFile, ex.Message),
                            CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = CANAPE.Properties.Resources.AllFilesFilter_String;
                dlg.Multiselect = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        List<LogPacket> packets = new List<LogPacket>();

                        foreach (string fileName in dlg.FileNames)
                        {
                            packets.AddRange(GeneralUtils.DeserializeLogPackets(fileName));
                        }

                        listLogPackets.BeginUpdate();
                        lock (_packets)
                        {
                            foreach (var p in packets)
                            {
                                _packets.Add(p);
                            }
                        }

                        RefreshLog();
                        listLogPackets.EndUpdate();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(String.Format(CANAPE.Properties.Resources.PacketLogControl_ErrorReadingFile, ex.Message),
                            CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (SerializationException ex)
                    {
                        MessageBox.Show(String.Format(CANAPE.Properties.Resources.PacketLogControl_ErrorReadingFile, ex.Message),
                            CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void fromPCAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = Properties.Resources.PCAPFileFilter_String;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        LogPacket[] packets = PcapReader.Load(dlg.FileName, false);

                        listLogPackets.BeginUpdate();
                        lock (_packets)
                        {
                            foreach (var p in packets)
                            {
                                _packets.Add(p);
                            }
                        }

                        RefreshLog();
                        listLogPackets.EndUpdate();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(String.Format(CANAPE.Properties.Resources.PacketLogControl_ErrorReadingFile, ex.Message),
                            CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (SerializationException ex)
                    {
                        MessageBox.Show(String.Format(CANAPE.Properties.Resources.PacketLogControl_ErrorReadingFile, ex.Message),
                            CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void WritePacketsHTML(Stream stm, LogPacket[] ps)
        {
            using (TextWriter writer = new StreamWriter(stm))
            {
                WritePacketsHTML(writer, ps);
            }
        }

        private void WritePacketsHTML(TextWriter textWriter, LogPacket[] ps)
        {
            using (XmlWriter writer = XmlWriter.Create(textWriter))
            {
                writer.WriteStartElement("html");
                writer.WriteStartElement("head");
                writer.WriteElementString("title", "Packet Log");             
                writer.WriteEndElement();                
                writer.WriteStartElement("body");

                foreach (LogPacket p in ps)
                {
                    writer.WriteElementString("h2", String.Format("Time {0} - Tag '{1}' - Network '{2}'",
                        p.Timestamp.ToString(), p.Tag, p.Network));
                    writer.WriteStartElement("pre");
                    writer.WriteAttributeString("style", String.Format("background-color:#{0:X02}{1:X02}{2:X02}", p.Color.R, p.Color.G, p.Color.B));
                    writer.WriteString(GeneralUtils.BuildHexDump(16, p.Frame.ToArray()));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePacketsToFile("HTML Files (*.htm;*.html)|*.htm;*.html|All Files (*.*)|*.*", WritePacketsHTML);
        }

        private class PacketLogDragDrop
        {
            public List<int> Indicies { get; set; }
            public LogPacketCollection Packets { get; set; }
        }

        private void listLogPackets_ItemDrag(object sender, ItemDragEventArgs e)
        {
            List<int> indicies = new List<int>(listLogPackets.SelectedIndices.OfType<int>());

            PacketLogDragDrop drop = new PacketLogDragDrop();
            drop.Packets = _packets;
            indicies.Sort();
            drop.Indicies = indicies;

            if (indicies.Count > 0)
            {
                DoDragDrop(drop, DragDropEffects.Scroll | DragDropEffects.Move | DragDropEffects.Copy);
            }
        }

        private void listLogPackets_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(PacketLogDragDrop)) || ReadOnly)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                if ((e.KeyState & 8) != 0)
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.Copy;
                }

                Point point = PointToClient(new Point(e.X, e.Y));
                ListViewItem item = listLogPackets.GetItemAt(point.X, point.Y);
                if (item != null)
                {
                    lock (_packets)
                    {
                        int index = _packets.IndexOf((LogPacket)item.Tag);
                        if (index >= 0)
                        {
                            listLogPackets.SelectedIndices.Clear();
                            listLogPackets.SelectedIndices.Add(index);
                        }
                    }
                }
            }
        }

        private void listLogPackets_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PacketLogDragDrop)) && !ReadOnly)
            {
                PacketLogDragDrop drop = (PacketLogDragDrop)e.Data.GetData(typeof(PacketLogDragDrop));
                List<LogPacket> copiedPackets = new List<LogPacket>();

                lock (drop.Packets)
                {
                    foreach (int index in drop.Indicies)
                    {
                        if (drop.Packets.Count > index)
                        {
                            copiedPackets.Add(drop.Packets[index]);
                        }
                    }
                }

                // If moving then remove original packets
                if ((e.KeyState & 8) != 0)
                {
                    int indexOfs = 0;

                    lock (drop.Packets)
                    {
                        foreach (int i in drop.Indicies)
                        {
                            drop.Packets.RemoveAt(i - indexOfs);
                            indexOfs++;
                        }
                    }

                    if (drop.Packets == _packets)
                    {
                        RefreshLog();
                    }
                }

                Point point = PointToClient(new Point(e.X, e.Y));
                ListViewItem item = listLogPackets.GetItemAt(point.X, point.Y);                

                if (item != null)
                {
                    int idx = item.Index;

                    lock (_packets)
                    {
                        foreach (LogPacket p in copiedPackets)
                        {
                            _packets.Insert(idx++, (LogPacket)p.Clone());
                        }
                    }
                }
                else
                {
                    lock (_packets)
                    {
                        foreach (LogPacket p in copiedPackets)
                        {
                            _packets.Add((LogPacket)p.Clone());
                        }
                    }
                }

                RefreshLog();
            }
        }

        private void SelectPackets(Func<LogPacket, bool> selectFunc)
        {
            listLogPackets.SelectedIndices.Clear();
            listLogPackets.BeginUpdate();
            lock (_packets)
            {
                try
                {
                    int packetCount = _packets.Count;

                    for (int i = 0; i < packetCount; ++i)
                    {
                        if (selectFunc(_packets[i]))
                        {
                            listLogPackets.SelectedIndices.Add(i);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                { }
            }
            listLogPackets.EndUpdate();
        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listLogPackets.SelectedIndices.Count > 0)
            {
                HashSet<Guid> set = new HashSet<Guid>();

                lock (_packets)
                {
                    foreach (int index in listLogPackets.SelectedIndices)
                    {
                        if (index < _packets.Count)
                        {
                            if (!set.Contains(_packets[index].NetId))
                            {
                                set.Add(_packets[index].NetId);
                            }
                        }
                    }
                }

                SelectPackets(p => set.Contains(p.NetId));
            }
        }

        private void tagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listLogPackets.SelectedIndices.Count > 0)
            {
                HashSet<string> set = new HashSet<string>();

                lock (_packets)
                {
                    foreach (int index in listLogPackets.SelectedIndices)
                    {
                        if (index < _packets.Count)
                        {
                            string tag = (_packets[index].Tag ?? "").ToLowerInvariant();
                            if (!set.Contains(tag))
                            {
                                set.Add(tag);
                            }
                        }
                    }
                }

                SelectPackets(p => set.Contains((p.Tag ?? "").ToLowerInvariant()));
            }
        }

        private void tagConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listLogPackets.SelectedIndices.Count > 0)
            {
                HashSet<string> set = new HashSet<string>();

                lock (_packets)
                {
                    foreach (int index in listLogPackets.SelectedIndices)
                    {
                        if (index < _packets.Count)
                        {
                            string tag = (_packets[index].Tag ?? "").ToLowerInvariant();

                            string name = tag + "\0" + _packets[index].NetId.ToString();
                            if (!set.Contains(name))
                            {
                                set.Add(name);
                            }
                        }
                    }
                }

                SelectPackets(p => set.Contains((p.Tag ?? "").ToLower(CultureInfo.InvariantCulture) + "\0" + p.NetId.ToString()));
            }
        }

        private void changeTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            if (packets.Length > 0)
            {
                using (ChangeTagForm dlg = new ChangeTagForm())
                {
                    string commonTag = null;

                    foreach (LogPacket packet in packets)
                    {
                        if (commonTag == null)
                        {
                            commonTag = packet.Tag ?? "";
                        }
                        else
                        {
                            if (!commonTag.Equals(packet.Tag, StringComparison.OrdinalIgnoreCase))
                            {
                                commonTag = null;
                                break;
                            }
                        }
                    }

                    dlg.TagName = commonTag;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        foreach (var p in packets)
                        {
                            p.Tag = dlg.TagName;
                        }

                        listLogPackets.Invalidate();
                    }
                }
            }
        }

        private void RunScript(LogPacket[] packets, ScriptContainer container, string classname)
        {
            if (packets.Length > 0)
            {
                try
                {
                    DataFrame[] frames = ParseWithUtils.ParseFrames(packets.Select(p => p.Frame), "/", container, classname).ToArray();

                    if (frames.Length > 0)
                    {
                        int index = 0;

                        lock (_packets)
                        {
                            index = _packets.IndexOf(packets[0]);
                            int currIndex = index;

                            foreach(LogPacket packet in packets)
                            {
                                _packets.Remove(packet);
                            }

                            LogPacket template = packets[0];

                            foreach (DataFrame frame in frames)
                            {
                                LogPacket newPacket = new LogPacket(template.Tag, template.NetId, Guid.NewGuid(), template.Network, frame, template.Color, template.Timestamp);

                                _packets.Insert(currIndex, newPacket);
                                currIndex++;
                            }
                        }

                        listLogPackets.SelectedIndices.Clear();
                        RefreshLog();
                        listLogPackets.SelectedIndices.Add(index);
                    }
                    else
                    {
                        MessageBox.Show(this, CANAPE.Properties.Resources.TreeDataKeyEditorControl_NoParseResults, 
                            CANAPE.Properties.Resources.TreeDataKeyEditorControl_NoParseResultsCaption,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.TreeDataKeyEditorControl_ParseError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RunScriptFromLibraryNode(LogPacket[] packets, NodeLibraryManager.NodeLibraryType node)
        {
            ScriptContainer container = new ScriptContainer("assembly", Guid.NewGuid(),
                            node.Type.Assembly.FullName, false);

            RunScript(packets, container, node.Type.FullName);
        }

        private void libraryNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            if (packets.Length > 0)
            {
                using (SelectLibraryNodeForm frm = new SelectLibraryNodeForm())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        RunScriptFromLibraryNode(packets, frm.Node);

                        if (!parseWithToolStripMenuItem.DropDownItems.ContainsKey(frm.Node.Type.FullName))
                        {
                            ToolStripMenuItem item = new ToolStripMenuItem(frm.Node.Name, null, null, frm.Node.Type.FullName);


                            item.Click += new EventHandler(parseWithNodeLibraryCache_Click);
                            item.Tag = frm.Node;

                            parseWithToolStripMenuItem.DropDownItems.Add(item);
                        }
                    }
                }
            }
        }

        void parseWithNodeLibraryCache_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            if (packets.Length > 0)
            {
                NodeLibraryManager.NodeLibraryType node = ((ToolStripMenuItem)sender).Tag as NodeLibraryManager.NodeLibraryType;

                if (node != null)
                {
                    RunScriptFromLibraryNode(packets, node);
                }
            }
        }

        private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            if (packets.Length > 0)
            {
                using (SelectScriptForm frm = new SelectScriptForm())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        RunScript(packets, frm.Document.Container, frm.ClassName);
                    }
                }
            }
        }

        private void ShowLogAsHtml(LogPacket[] ps)
        {
            StringWriter writer = new StringWriter();

            WritePacketsHTML(writer, ps);

            HTMLLogViewerForm viewer = new HTMLLogViewerForm(writer.ToString());

            components.Add(viewer);

            viewer.Show(this);
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            foreach (LogPacket p in packets)
            {
                AddLogEntry((LogPacket)p.Clone());
            }

            RefreshLog();
        }

        private void listLogPackets_Click(object sender, EventArgs e)
        {
            Select();
        }

        private void copyAsFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetPackets(true);

            if (packets.Length > 0)
            {
                NetGraphDocumentControl.CopyAsFilter(packets[0].Frame);
            }
        }

        private void ShowLogAsHistogram(LogPacket[] ps)
        {
            ConnectionStatisticsForm viewer = new ConnectionStatisticsForm(null, ps);

            components.Add(viewer);
            viewer.Show(this);

            //HistogramLogViewerForm viewer = new HistogramLogViewerForm(ps);

            //components.Add(viewer);

            //viewer.Show(this);
        }

        private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLog(true);
        }

        private void selectedToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            LogPacket[] ps = GetPackets(true);

            if (ps.Length > 0)
            {
                ShowLogAsHtml(ps);
            }
        }

        private void connectionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLog, (p,n) => p.NetId == n.NetId);
        }

        private void ShowLog(Action<LogPacket[]> showLogAction, Func<LogPacket, LogPacket, bool> selectFunc)
        {
            if (listLogPackets.SelectedIndices.Count > 0)
            {
                LogPacket p;

                lock (_packets)
                {
                    p = _packets[listLogPackets.SelectedIndices[0]];
                }

                LogPacket[] ps = GetPackets(p, selectFunc);

                if (ps.Length > 0)
                {
                    showLogAction(ps);
                }
            }
        }

        private void connectionToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsHtml, (p, n) => p.NetId == n.NetId);
        }

        private void tagToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLog, (p, n) => p.Tag == n.Tag);
        }

        private void tagConnectionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLog, (p, n) => (p.Tag == n.Tag) && (p.NetId == n.NetId));
        }

        private void tagToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsHtml, (p, n) => p.Tag == n.Tag);
        }

        private void tagConnectionToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsHtml, (p, n) => (p.Tag == n.Tag) && (p.NetId == n.NetId));
        }

        private void selectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LogPacket[] ps = GetPackets(true);

            if (ps.Length > 0)
            {
                ShowLogAsHistogram(ps);
            }
        }

        private void connectionToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsHistogram, (p, n) => p.NetId == n.NetId);
        }

        private void tagToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsHistogram, (p, n) => p.Tag == n.Tag);
        }

        private void tagConnectionToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsHistogram, (p, n) => (p.Tag == n.Tag) && (p.NetId == n.NetId));
        }

        private void readOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadOnly = !ReadOnly;
        }

        /// <summary>
        /// Name of the packet log
        /// </summary>
        public string LogName { get; set; }

        private void OpenNewPacketLogWindow(LogPacketCollection packets)
        {
            PacketLogControl control = new PacketLogControl();

            control.SetPackets(packets);

            string name = LogName != null ? LogName + " Packet Log" : "Packet Log";

            components.Add(DocumentControl.ShowControl(name, control));
        }

        private void allPacketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNewPacketLogWindow(_packets);
        }

        private void ShowLogAsNewWindow(LogPacket[] ps)
        {
            OpenNewPacketLogWindow(new LogPacketCollection(ps));
        }

        private void selectedToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ShowLogAsNewWindow(GetPackets(true));            
        }

        private void connectionToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsNewWindow, (p, n) => p.NetId == n.NetId);
        }

        private void connectionTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsNewWindow, (p, n) => (p.Tag == n.Tag) && (p.NetId == n.NetId));
        }

        private void tagToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ShowLog(ShowLogAsNewWindow, (p, n) => p.Tag == n.Tag);
        }

        private void contextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // Re-enable selections
            EnableMenuForSelection(true);
            pastePacketsToolStripMenuItem.Enabled = true;
        }

        private void diffWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PacketLogDiffDocumentControl control = new PacketLogDiffDocumentControl();            

            string name = LogName != null ? LogName + " Packet Diff" : "Packet Diff";            

            components.Add(DocumentControl.ShowControl(name, control, this));
        }

        private void listLogPackets_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {            
            PacketLogColumn col = _config.Columns[e.OldDisplayIndex];
            _config.Columns.RemoveAt(e.OldDisplayIndex);
            _config.Columns.Insert(e.NewDisplayIndex, col);

            OnConfigChanged();
        }

        protected virtual void OnConfigChanged()
        {
            EventHandler handler = ConfigChanged;

            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        [Category("Behavior")]
        public event EventHandler ConfigChanged;

        private void listLogPackets_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            _config.Columns[e.ColumnIndex].ColumnWidth = listLogPackets.Columns[e.ColumnIndex].Width;

            OnConfigChanged();
        }

        private void changeColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PacketLogControlConfig config = GeneralUtils.CloneObject(_config);
            Dictionary<string, Type> typeMap = new Dictionary<string,Type>();      

            typeMap.Add(Properties.Resources.PacketLogControl_NoColumn, typeof(NumberPacketLogColumn));
            typeMap.Add(Properties.Resources.PacketLogControl_TimestampColumn, typeof(TimestampPacketLogColumn));
            typeMap.Add(Properties.Resources.PacketLogControl_TagColumn, typeof(TagPacketLogColumn));
            typeMap.Add(Properties.Resources.PacketLogControl_NetworkColumn, typeof(NetworkPacketLogColumn));
            typeMap.Add(Properties.Resources.PacketLogControl_DataColumn, typeof(DataPacketLogColumn));
            typeMap.Add(Properties.Resources.PacketLogControl_LengthColumn, typeof(LengthPacketLogColumn));
            typeMap.Add(Properties.Resources.PacketLogControl_HashColumn, typeof(HashPacketLogColumn));
            typeMap.Add(Properties.Resources.PacketLogControl_CustomColumn, typeof(CustomPacketLogColumn));

            using (ObjectCollectionForm frm = new ObjectCollectionForm(config.Columns, typeMap))
            {
                frm.Text = Properties.Resources.PacketLogControl_ColumnEditor;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    config.Columns.Clear();
                    config.Columns.AddRange(frm.Objects.Cast<PacketLogColumn>());
                    if (config.Columns.Count == 0)
                    {
                        config.Columns.Add(new NumberPacketLogColumn());
                    }
                    UpdateConfig(config);
                    OnConfigChanged();
                }
           } 
        }

        private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new FontDialog())
            {
                dlg.Font = listLogPackets.Font;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    listLogPackets.Font = dlg.Font;
                    _config.DefaultFont = dlg.Font;
                    OnConfigChanged();
                }
            }
        }
    }
}
