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
using System.Reflection;
using System.Windows.Forms;
using CANAPE.DataFrames;
using CANAPE.Documents.Data;
using CANAPE.Extension;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;
using ICSharpCode.TextEditor;

namespace CANAPE.Controls
{
    public partial class NodeRichTextBox : TextEditorControl
    {
        public NodeRichTextBox()
        {
            
            InitializeComponent();
        }

        private void CopyText()
        {
            ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null, null);
        }

        private void CutText()
        {
            ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null, null);
        }

        private void PasteText()
        {
            ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null, null);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutText();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyText();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteText();
        }

        private LogPacket[] GetSelectedPacket()
        {            
            LogPacket[] packets = new LogPacket[1];

            packets[0] = new LogPacket("Text", Guid.NewGuid(), "Text Copy", new DataFrame(ActiveTextAreaControl.SelectionManager.SelectedText), ColorValue.White);

            return packets;
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextAreaClipboardHandler handler = ActiveTextAreaControl.TextArea.ClipboardHandler;

            copyToolStripMenuItem.Enabled = handler.EnableCopy;
            cutToolStripMenuItem.Enabled = handler.EnableCut;
            pasteToolStripMenuItem.Enabled = handler.EnablePaste;
            deleteToolStripMenuItem.Enabled = handler.EnableDelete;
            selectAllToolStripMenuItem.Enabled = handler.EnableSelectAll;
            copyToToolStripMenuItem.Enabled = handler.EnableCopy;
            convertToolStripMenuItem.Visible = !IsReadOnly && handler.EnableCopy;

            convertToolStripMenuItem.DropDownItems.Clear();

            StringConverterExtensionManager.Instance.AddToMenu(convertToolStripMenuItem, applyConverter_Click);

            copyToToolStripMenuItem.DropDownItems.Clear();
            GuiUtils.CreatePacketLogCopyItems(copyToToolStripMenuItem.DropDownItems, () => GetSelectedPacket());
            GuiUtils.CreateScriptMenuItems(applyScriptToolStripMenuItem.DropDownItems, ApplyScript_Click);
            if (applyScriptToolStripMenuItem.DropDownItems.Count == 0)
            {
                applyScriptToolStripMenuItem.Visible = false;
            }
        }

        private class ScriptStringConverter : IStringConverter
        {
            ScriptDocument _document;

            public ScriptStringConverter(ScriptDocument document)
            {
                _document = document;
            }

            public string Convert(long startPos, string s)
            {
                return (string)ScriptUtils.Invoke(_document.Container, null, "ConvertString", startPos, s);
            }
        }

        private void ApplyScript_Click(ScriptDocument script)
        {
            try
            {
                ApplyStringConverter(ScriptUtils.GetInstance<IStringConverter>(script.Container) ?? new ScriptStringConverter(script));
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(this, e);
        }

        private void SetSelection(int index, int length)
        {
            ActiveTextAreaControl.SelectionManager.ClearSelection();

            TextLocation startSelection = ActiveTextAreaControl.Document.OffsetToPosition(index);
            TextLocation endSelection = ActiveTextAreaControl.Document.OffsetToPosition(index + length);
            ActiveTextAreaControl.SelectionManager.SetSelection(startSelection, endSelection);
            ActiveTextAreaControl.Caret.Position = startSelection;
            ActiveTextAreaControl.ScrollTo(startSelection.Line, startSelection.Column);
        }

        public bool FindAndSelect(string s, bool next)
        {            
            string text = this.Text;
            bool found = false;

            int pos = 0;

            if (ActiveTextAreaControl.SelectionManager.SelectionCollection.Count > 0)
            {
                pos = ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;
            }
            else
            {
                pos = ActiveTextAreaControl.Caret.Offset;
            }

            int index = -1;

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

                index = text.IndexOf(s, pos, StringComparison.OrdinalIgnoreCase);

                if (index < 0)
                {
                    // Try and wrap around and find anything
                    index = text.IndexOf(s, StringComparison.OrdinalIgnoreCase);
                }
            }
            else
            {
                if (pos <= 0)
                {
                    pos = text.Length;
                }
                else
                {
                    pos = pos - 1;
                }

                index = text.LastIndexOf(s, pos, StringComparison.OrdinalIgnoreCase);

                if (index < 0)
                {
                    // Try and wrap around and find anything
                    index = text.LastIndexOf(s, StringComparison.OrdinalIgnoreCase);
                }
            }

            if (index >= 0)
            {
                found = true;

                SetSelection(index, s.Length);
            }

            return found;
        }

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

        private void ApplyStringConverter(IStringConverter converter)
        {
            int startPos = ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;
            int length = ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Length;

            if (startPos >= 0)
            {
                string selection = ActiveTextAreaControl.SelectionManager.SelectedText;
                string newSelection = converter.Convert(startPos, selection);

                if ((newSelection != null) && (selection != newSelection))
                {
                    Document.Replace(startPos, selection.Length, newSelection);
                    SetSelection(startPos, newSelection.Length);
                }
            }
        }

        private void applyConverter_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;

            if ((item != null) && (item.Tag is Type))
            {
                try
                {
                    ApplyStringConverter((IStringConverter)CreateExtensionObject((Type)item.Tag));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this,
                        ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void copyPacketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPacket[] packets = GetSelectedPacket();

            if (packets.Length > 0)
            {
                Clipboard.SetData(LogPacket.LogPacketArrayFormat, packets);
            }
        }

    }
}
