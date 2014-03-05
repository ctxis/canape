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
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;
using CANAPE.DataFrames;
using CANAPE.Documents.Data;
using CANAPE.Extension;
using CANAPE.Forms;
using CANAPE.Nodes;
using CANAPE.Parser;
using CANAPE.Scripting;
using CANAPE.Utils;

// Disable unused events
#pragma warning disable 67

namespace CANAPE.Controls
{
    public partial class HexEditorControl : UserControl
    {
        public const string DEFAULT_CONFIG_NAME = "DefaultHexEditorControlConfig";

        [Serializable]
        internal abstract class HexEditorControlInspectorEntry
        {
            internal abstract InspectorEntry CreateInspector();
        }

        [Serializable]
        class HexEditorControlConfig
        {
        }

        class ReadOnlyByteProvider : IByteProvider
        {
            private byte[] _data;

            public byte[] Data
            {
                get { return _data; }
            }

            public ReadOnlyByteProvider(byte[] data)
            {
                _data = data;
            }

            #region IByteProvider Members

            public byte ReadByte(long index)
            {
                return _data[index];
            }

            public void WriteByte(long index, byte value)
            {
            }

            public void InsertBytes(long index, byte[] bs)
            {
            }

            public void DeleteBytes(long index, long length)
            {
            }

            public long Length
            {
                get { return _data.LongLength; }
            }

            public event EventHandler LengthChanged;

            public bool HasChanges()
            {
                return false;
            }

            public void ApplyChanges()
            {
            }

            public event EventHandler Changed;

            public bool SupportsWriteByte()
            {
                return false;
            }

            public bool SupportsInsertBytes()
            {
                return false;
            }

            public bool SupportsDeleteBytes()
            {
                return false;
            }

            #endregion
        }

        #region Inspectors

        internal class InspectorEntry
        {
            public bool IsFixed { get; private set; }
            public Func<IByteProvider, long, long, string> UpdateField { get; private set; }
            public string Name { get; private set; }
            public ListViewItem Item { get; set; }

            protected void SetUpdateField(Func<IByteProvider, long, long, string> updateField)
            {
                UpdateField = updateField;
            }

            public InspectorEntry(bool isFixed, Func<IByteProvider, long, long, string> updateField, string name)
            {
                IsFixed = isFixed;
                UpdateField = updateField;
                Name = name;
            }
        }

        class HashAlgorithmInspectorEntry : InspectorEntry
        {
            HashAlgorithm _hasher;

            string GetValue(IByteProvider provider, long pos, long length)
            {                
                // Read out the bytes
                if ((_hasher != null) && (provider != null) && (pos + length <= provider.Length))
                {                    
                    byte[] data = new byte[length];

                    for (long i = 0; i < length; i++)
                    {
                        data[i] = provider.ReadByte(pos + i);
                    }

                    byte[] hash = _hasher.ComputeHash(data);

                    StringBuilder builder = new StringBuilder();

                    foreach (byte b in hash)
                    {
                        builder.AppendFormat("{0:X02}", b);
                    }

                    return builder.ToString();
                }
                else
                {
                    return "";
                }
            }

            public HashAlgorithmInspectorEntry(HashAlgorithm hasher, string name) : base(false, null, name)
            {
                SetUpdateField(GetValue);

                _hasher = hasher;
            }
        }

        class SevenBitIntInspectorEntry : InspectorEntry
        {
            string GetValue(IByteProvider provider, long pos, long length)
            {
                ulong ret = 0;
                int bytePos = 0;

                while (pos < provider.Length)
                {
                    byte b = provider.ReadByte(pos++);
                    ret |= ((ulong)b & 0x7F) << (bytePos++ * 7);
                    if ((b & 0x80) == 0)
                    {
                        break;
                    }
                }

                return string.Format("{0}/0x{0:X08}", ret);
            }

            public SevenBitIntInspectorEntry() : base(false, null, "7Bit Int")
            {
                this.SetUpdateField(GetValue);
            }
        }

        abstract class ChecksumInspectorEntry : InspectorEntry
        {
            protected abstract string GetChecksumString(byte[] data);            

            string GetValue(IByteProvider provider, long pos, long length)
            {
                // Read out the bytes
                if ((provider != null) && (pos + length <= provider.Length))
                {
                    byte[] data = new byte[length];

                    for (long i = 0; i < length; i++)
                    {
                        data[i] = provider.ReadByte(pos + i);
                    }
                    
                    return GetChecksumString(data);
                }
                else
                {
                    return "";
                }
            }

            protected ChecksumInspectorEntry(string name)
                : base(false, null, name)
            {
                SetUpdateField(GetValue);
            }
        }        

        class Uint32ChecksumInspectorEntry : ChecksumInspectorEntry
        {
            protected override string GetChecksumString(byte[] data)
            {
                uint chk = 0;

                foreach (byte b in data)
                {
                    chk += b;
                }

                return String.Format("0x{0:X08}", chk);
            }

            public Uint32ChecksumInspectorEntry()
                : base("32bit Checksum")
            {
            }
        }

        class CRC32ChecksumInspectorEntry : ChecksumInspectorEntry
        {                
            protected override string GetChecksumString(byte[] data)
            {
                return String.Format("0x{0:X08}", Crc32.ComputeChecksum(data));
            }

            public CRC32ChecksumInspectorEntry()
                : base("CRC32")
            {
            }
        }

        #endregion

        object CreateExtensionObject(Type t)
        {
            ConstructorInfo ci = t.GetConstructor(new[] { typeof(IWin32Window) });

            if (ci != null)
            {
                return ci.Invoke(new object[] { this });
            }
            else
            {
                ci = t.GetConstructor(new Type[0]);
                if (ci != null)
                {
                    return ci.Invoke(new object[0]);
                }
            }

            throw new MissingMethodException();
        }

        IByteProvider _provider;
        List<InspectorEntry> _entries;

        public HexEditorControl()
        {
            
            InitializeComponent();
            _entries = new List<InspectorEntry>();
        }

        private void SetBytes(byte[] bytes)
        {
            if (_provider != null)
            {
                _provider.Changed -= _provider_Changed;
            }

            if (hexBox.ReadOnly)
            {
                _provider = new ReadOnlyByteProvider(bytes);
            }
            else
            {
                _provider = new DynamicByteProvider(bytes);
            }

            _provider.Changed += new EventHandler(_provider_Changed);

            hexBox.ByteProvider = _provider;

            UpdateInspector(true);
        }

        public event EventHandler BytesChanged;

        void _provider_Changed(object sender, EventArgs e)
        {
            UpdateInspector(true);

            if (BytesChanged != null)
            {
                BytesChanged.Invoke(this, new EventArgs());
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public byte[] Bytes
        {
            get 
            {
                if (_provider is ReadOnlyByteProvider)
                {
                    return (byte[])((ReadOnlyByteProvider)_provider).Data.Clone();
                }
                else
                {
                    return ((DynamicByteProvider)_provider).Bytes.ToArray();
                }
            }
            set 
            {
                SetBytes(value);
            }
        }

        public bool ReadOnly
        {
            get { return hexBox.ReadOnly; }
            set 
            {
                if (hexBox.ReadOnly != value)
                {
                    hexBox.ReadOnly = value;
                    byte[] data = this.Bytes;
                    SetBytes(data);
                }
            }
        }        
        
        public Color HexColor
        {
            get { return hexBox.BackColor; }
            set { hexBox.BackColor = value; }
        }

        public int BytesPerLine
        {
            get { return hexBox.BytesPerLine; }
            set { hexBox.BytesPerLine = value; }
        }

        private void UpdateInspector(bool updateFixed)
        {
            if (_provider != null)
            {
                foreach (InspectorEntry ent in _entries)
                {
                    if ((ent.Item != null) && (updateFixed || !ent.IsFixed))
                    {
                        ent.Item.SubItems[1].Text = ent.UpdateField(_provider, hexBox.SelectionStart, hexBox.SelectionLength);                        
                    }
                }
            }
        }

        private void hexBox_SelectionStartChanged(object sender, EventArgs e)
        {
            UpdateInspector(true);
        }

        private void hexBox_SelectionLengthChanged(object sender, EventArgs e)
        {
            UpdateInspector(false);
        }

        private static byte[] GetBytesFromProvider(IByteProvider provider, long pos, long length)
        {
            byte[] data = null;

            // Read out the bytes
            if ((provider != null) && (pos + length <= provider.Length))
            {
                data = new byte[length];

                for (long i = 0; i < length; i++)
                {
                    data[i] = provider.ReadByte(pos + i);
                }
            }

            return data;
        }

        private string GetIntegerValue<T>(IByteProvider provider, long pos, bool littleEndian) where T : struct
        {
            GenericDataValue<T> value = new GenericDataValue<T>("", new T(), littleEndian);
            byte[] data = GetBytesFromProvider(provider, pos, Marshal.SizeOf(typeof(T)));              

            if(data != null)
            {
                value.FromArray(data);

                return String.Format("{0}/0x{0:X}", value.Value);
            }
            else
            {
                return "?";
            }
        }

        private static string GetUnixTime(IByteProvider provider, long pos, bool littleEndian)
        {
            byte[] data = GetBytesFromProvider(provider, pos, 4);

            if (data != null)            
            {
                DateTime t = new DateTime(1970, 1, 1);

                return t.AddSeconds((double)BitConverter.ToUInt32(data, 0)).ToString();
            }
            else
            {
                return "?";
            }
        }

        private static string GetFileTime(IByteProvider provider, long pos, bool littleEndian)
        {
            byte[] data = GetBytesFromProvider(provider, pos, 8);

            if (data != null)
            {
                long ft = BitConverter.ToInt64(data, 0);

                if (ft >= 0)
                {
                    try
                    {
                        return DateTime.FromFileTimeUtc(BitConverter.ToInt64(data, 0)).ToString();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        // Do nothing
                    }
                }
            }
            
            return "?";
        }


        private void AddIntegerEntry<S,U>() 
            where S : struct 
            where U : struct
        {
            _entries.Add(new InspectorEntry(true, (provider, pos, len) => String.Format("Signed: {0}, Unsigned: {1}",
                GetIntegerValue<S>(provider, pos, true), GetIntegerValue<U>(provider, pos, true)), typeof(S).Name + " (little)"));
            _entries.Add(new InspectorEntry(true, (provider, pos, len) => String.Format("Signed: {0}, Unsigned: {1}",
                GetIntegerValue<S>(provider, pos, false), GetIntegerValue<U>(provider, pos, false)), typeof(S).Name + " (big)"));
        }

        private void AddInspectorEntry(InspectorEntry ent)
        {
            ListViewItem item = listViewInspector.Items.Add(ent.Name);
            item.SubItems.Add("");
            ent.Item = item;
            item.Tag = ent;
        }

        private void HexEditorControl_Load(object sender, EventArgs e)
        {
            _entries.Add(new InspectorEntry(false, (provider, pos, len) => String.Format("Pos:{0}/0x{0:X} Length:{1}/0x{1:X}",
                pos, len), "Selection"));
            AddIntegerEntry<int, uint>();
            AddIntegerEntry<short, ushort>();
            AddIntegerEntry<long, ulong>();
            _entries.Add(new InspectorEntry(true, (provider, pos, len) => String.Format("Signed: {0}, Unsigned: {1}",
                GetIntegerValue<byte>(provider, pos, true), GetIntegerValue<sbyte>(provider, pos, true)), "Byte"));            
            _entries.Add(new SevenBitIntInspectorEntry());
            
            foreach (InspectorEntry ent in _entries)
            {
                AddInspectorEntry(ent);                
            }

            UpdateInspector(true);

            if (ContextMenuStrip != null)
            {
                contextMenuStripHex.Items.AddRange(ContextMenuStrip.Items);                
            }
        }

        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewInspector.SelectedItems.Count > 0)
            {
                StringBuilder builder = new StringBuilder();

                foreach (ListViewItem item in listViewInspector.SelectedItems)
                {
                    builder.AppendLine(item.SubItems[1].Text);
                }

                Clipboard.SetText(builder.ToString());
            }
        }

        private void removeInspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewInspector.SelectedItems.Count > 0)
            {
                ListViewItem[] items = new ListViewItem[listViewInspector.SelectedItems.Count];

                listViewInspector.SelectedItems.CopyTo(items, 0);

                foreach (ListViewItem item in items)
                {
                    listViewInspector.Items.Remove(item);
                    _entries.Remove((InspectorEntry)item.Tag);
                }
            }
        }

        private void mD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InspectorEntry ent = new HashAlgorithmInspectorEntry(MD5.Create(), "MD5");
            _entries.Add(ent);
            AddInspectorEntry(ent);
        }

        private void sHA1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InspectorEntry ent = new HashAlgorithmInspectorEntry(SHA1.Create(), "SHA1");
            _entries.Add(ent);
            AddInspectorEntry(ent);
        }

        private void bitChecksumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InspectorEntry ent = new Uint32ChecksumInspectorEntry();
            _entries.Add(ent);
            AddInspectorEntry(ent);
        }

        private void cRC32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InspectorEntry ent = new CRC32ChecksumInspectorEntry();
            _entries.Add(ent);
            AddInspectorEntry(ent);
        }

        /// <summary>
        /// Find and select a range of bytes
        /// </summary>
        /// <param name="data">The data to find</param>
        /// <param name="next">Whether to find the next match or previous match</param>
        /// <returns>True to indicate we found a match</returns>
        public bool FindAndSelect(byte[] data, bool next)
        {
            bool ret = false;

            if ((_provider != null) && (data.Length <= _provider.Length))
            {
                long foundIndex = -1;
                long pos = hexBox.SelectionStart;
                
                if (next)
                {
                    if (pos < 0)
                    {
                        pos = 0;
                    }
                    else
                    {
                        pos = pos + 1;
                    }

                    while ((pos + data.Length) <= _provider.Length)
                    {
                        foundIndex = pos;
                        for (long i = 0; i < data.Length; ++i)
                        {
                            if(_provider.ReadByte(i+pos) != data[i])
                            {
                                foundIndex = -1;
                                break;
                            }
                        }

                        if(foundIndex >= 0)
                        {
                            break;
                        }

                        pos++;
                    }
                }
                else
                {
                    if (pos > 0)
                    {
                        pos = pos - 1;

                        while (pos >= 0)
                        {
                            if ((pos + data.Length) < _provider.Length)
                            {
                                foundIndex = pos;
                                for (long i = 0; i < data.Length; ++i)
                                {
                                    if (_provider.ReadByte(i + pos) != data[i])
                                    {
                                        foundIndex = -1;                                        
                                        break;
                                    }
                                }
                            }

                            if (foundIndex >= 0)
                            {
                                break;
                            }

                            pos--;
                        }
                    }
                }

                if (foundIndex >= 0)
                {                    
                    hexBox.Select(pos, data.Length);
                    hexBox.ScrollByteIntoView(pos + data.Length);
                    hexBox.ScrollByteIntoView(pos);
                    ret = true;
                }
            }

            return ret;
        }

        private void unixTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InspectorEntry ent = new InspectorEntry(true, (provider, pos, len) => String.Format("{0} (little) - {1} (big)",
                GetUnixTime(provider, pos, true), GetUnixTime(provider, pos, false)), "Unix Time");
            _entries.Add(ent);
            AddInspectorEntry(ent);
        }

        private void windowsFileTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InspectorEntry ent = new InspectorEntry(true, (provider, pos, len) => String.Format("{0} (little) - {1} (big)",
                GetFileTime(provider, pos, true), GetFileTime(provider, pos, false)), "Windows File Time");
            _entries.Add(ent);
            AddInspectorEntry(ent);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexBox.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexBox.Copy();
        }

        private LogPacket[] CreatePacketFromSelection()
        {
            LogPacket[] ret = null;

            try
            {
                if (hexBox.CanCopy())
                {
                    long startPos = hexBox.SelectionStart;
                    long length = hexBox.SelectionLength;

                    if (startPos >= 0)
                    {
                        byte[] data = new byte[length];                        

                        for (long i = 0; i < length; ++i)
                        {
                            data[i] = _provider.ReadByte(startPos + i);
                        }
                        
                        ret = new LogPacket[1]; 
                        ret[0] = new LogPacket("Hex", Guid.NewGuid(), "Hex Copy", new DataFrame(data), ColorValueConverter.FromColor(this.HexColor));
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {                
            }

            return ret ?? new LogPacket[0];
        }

        private void contextMenuStripHex_Opening(object sender, CancelEventArgs e)
        {
            copyToolStripMenuItem.Enabled = hexBox.CanCopy();
            copyHexToolStripMenuItem.Enabled = hexBox.CanCopy();
            cutToolStripMenuItem.Enabled = hexBox.CanCut();
            copyToToolStripMenuItem.Enabled = hexBox.CanCopy();
            pasteToolStripMenuItem.Enabled = hexBox.CanPaste();
            pasteHexToolStripMenuItem.Enabled = hexBox.CanPasteHex();
            fillSelectionToolStripMenuItem.Enabled = hexBox.SelectionLength > 0;
            fillSelectionToolStripMenuItem.Visible = !ReadOnly;
            convertToolStripMenuItem.Visible = !ReadOnly && hexBox.CanCopy();

            convertToolStripMenuItem.DropDownItems.Clear();

            HexConverterExtensionManager.Instance.AddToMenu(convertToolStripMenuItem, applyConverter_Click);

            copyToToolStripMenuItem.DropDownItems.Clear();
            
            GuiUtils.CreatePacketLogCopyItems(copyToToolStripMenuItem.DropDownItems, () => CreatePacketFromSelection());
            GuiUtils.CreateScriptMenuItems(applyScriptToolStripMenuItem.DropDownItems, applyScript_Click);
            if (applyScriptToolStripMenuItem.DropDownItems.Count == 0)
            {
                applyScriptToolStripMenuItem.Visible = false;
            }
        }

        private void ApplyHexConverter(IHexConverter converter)
        {
            long startPos = hexBox.SelectionStart;
            long length = hexBox.SelectionLength;

            if (startPos >= 0)
            {
                byte[] ba = new byte[hexBox.SelectionLength];

                for (long i = 0; i < length; ++i)
                {
                    ba[i] = _provider.ReadByte(i + startPos);
                }

                byte[] newBa = converter.Convert(startPos, ba);

                if (newBa != null)
                {
                    if (ba.Length == newBa.Length)
                    {
                        for (long i = 0; i < newBa.Length; ++i)
                        {
                            _provider.WriteByte(i + startPos, newBa[i]);
                        }
                    }
                    else
                    {
                        _provider.DeleteBytes(startPos, length);
                        _provider.InsertBytes(startPos, newBa);
                    }
                    hexBox.Select(startPos, newBa.Length);
                    hexBox.Invalidate();
                }
            }
        }

        private class ScriptHexConverter : IHexConverter
        {
            ScriptDocument _document;
            public ScriptHexConverter(ScriptDocument document)
            {
                _document = document;
            }

            public byte[] Convert(long startPos, byte[] ba)
            {
                return (byte[])ScriptUtils.Invoke(_document.Container, null, "ConvertHex", startPos, ba);
            }
        }

        private void applyScript_Click(ScriptDocument script)
        {
            try
            {
                ApplyHexConverter(ScriptUtils.GetInstance<IHexConverter>(script.Container) 
                    ?? new ScriptHexConverter(script));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pasteHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexBox.PasteHex();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hexBox.SelectAll();
        }

        private void fillSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((hexBox.SelectionLength > 0) && (_provider is DynamicByteProvider))
            {
                using (FillBytesForm frm = new FillBytesForm((DynamicByteProvider)_provider, 
                    hexBox.SelectionStart, hexBox.SelectionLength))
                {
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        hexBox.Invalidate();
                    }
                }
            }
        }

        private void selectBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SelectBlockForm frm = new SelectBlockForm((int)hexBox.SelectionStart,
                (int)hexBox.ByteProvider.Length))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    hexBox.Select(frm.Offset, frm.Length);
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            extensionToolStripMenuItem.DropDownItems.Clear();

            foreach (var ext in HexInspectorExtensionManager.Instance.GetExtensions())
            {
                ToolStripItem item = extensionToolStripMenuItem.DropDownItems.Add(ext.ExtensionAttribute.Name);
                item.Tag = ext.ExtensionType;
                item.Click += new EventHandler(addExtensionInspector_Click);
            }

            extensionToolStripMenuItem.Visible = extensionToolStripMenuItem.DropDownItems.Count > 0;
        }

        class ExtensionInspectorEntry : InspectorEntry
        {
            IHexInspector _inspector;

            string GetValue(IByteProvider provider, long pos, long len)
            {
                string ret = null;

                try
                {
                    ret = _inspector.Inspect(new HexInspectorData(provider), pos, len);
                }
                catch
                {
                }

                return ret ?? String.Empty;
            }

            public ExtensionInspectorEntry(IHexInspector inspector)
                : base(inspector.IsFixed, null, inspector.DisplayString)
            {
                SetUpdateField(GetValue);
                _inspector = inspector;
            }
        }

        void addExtensionInspector_Click(object sender, EventArgs e)
        {
            try
            {
                Type t = ((ToolStripItem)sender).Tag as Type;

                if (t != null)
                {
                    IHexInspector inspector = (IHexInspector)CreateExtensionObject(t);
                    InspectorEntry ent = new ExtensionInspectorEntry(inspector);

                    _entries.Add(ent);
                    AddInspectorEntry(ent);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, 
                    ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void applyConverter_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;

            if ((item != null) && (item.Tag is Type))
            {
                try
                {
                    ApplyHexConverter((IHexConverter)CreateExtensionObject((Type)item.Tag));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,
                        ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void copyHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (hexBox.CanCopy())
                {
                    long startPos = hexBox.SelectionStart;
                    long length = hexBox.SelectionLength;

                    if (startPos >= 0)
                    {
                        string[] values = new string[length];

                        for (long i = 0; i < length; ++i)
                        {
                            values[i] = String.Format("{0:X02}", _provider.ReadByte(startPos + i));
                        }

                        Clipboard.SetText(String.Join(" ", values));
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        private string GetIpAddress(IByteProvider provider, long pos, bool littleEndian, bool ipv6)
        {
            byte[] data = GetBytesFromProvider(provider, pos, ipv6 ? 16 : 4);

            if (data != null)
            {
                if (littleEndian)
                {
                    data = data.Reverse().ToArray();
                }

                IPAddress addr = new IPAddress(data);

                return addr.ToString();
            }

            return "?";
        }

        private void AddIpAddressInspector(bool ipv6)
        {
            InspectorEntry ent = new InspectorEntry(true, (provider, pos, len) => String.Format("{0} (big) {1} (little)",
                GetIpAddress(provider, pos, false, ipv6), GetIpAddress(provider, pos, true, ipv6)), "IPv4");
            _entries.Add(ent);
            AddInspectorEntry(ent);
        }

        private void iPv4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddIpAddressInspector(false);
        }

        private void iPv6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddIpAddressInspector(true);
        }

        private void copyPacketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = CreatePacketFromSelection();

            if (packets.Length > 0)
            {
                Clipboard.SetData(LogPacket.LogPacketArrayFormat, packets);
            }
        }

        public void Select(long position, long length)
        {
            hexBox.Select(position, length);
        }

        /// <summary>
        /// Add an annotation
        /// </summary>
        /// <param name="startPos">The start position</param>
        /// <param name="endPos">The end position</param>
        /// <param name="foreColor">The foreground color of the annotation</param>
        /// <param name="backColor">The background color of the annotation</param>
        public void AddAnnotation(long startPos, long endPos, Color foreColor, Color backColor)
        {
            hexBox.AddAnnotation(startPos, endPos, foreColor, backColor);
        }

        /// <summary>
        /// Remove an annotation at a specific position
        /// </summary>
        /// <param name="pos">The byte position</param>
        public void RemoveAnnotation(long pos)
        {
            hexBox.RemoveAnnotation(pos);
        }

        /// <summary>
        /// Clear all annotations
        /// </summary>
        public void ClearAnnotations()
        {
            hexBox.ClearAnnotations();
        }

        private void editDefaultInspectorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void applyScriptToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {

        }
    }
}
