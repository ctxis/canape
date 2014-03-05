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
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Extension;
using CANAPE.Forms;
using CANAPE.Scripting;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Undo;

namespace CANAPE.Controls.DocumentEditors
{
    public partial class ScriptDocumentControl : UserControl
    {                
        bool _forParserDocument;
        ScriptDocumentControlConfig _config;

        static Dictionary<string, BaseHighlighter> _highlightingByEngine;
        
        static ScriptDocumentControl()
        {
            _highlightingByEngine = new Dictionary<string, BaseHighlighter>();
            foreach (var ext in ScriptHighlightingExtensionManager.Instance.GetExtensions())
            {
                BaseHighlighter h = ext.CreateInstance();

                if (h.HighlightXml != null)
                {
                    HighlightingManager.Manager.AddSyntaxModeFileProvider(
                        new SimpleSyntaxModeProvider(h.Name + ".xshd", h.Name, "." + h.FileExtension, h.HighlightXml));
                }

                _highlightingByEngine[h.EngineName] = h;
            }
        }
        
        private const string DOCUMENT_CONFIG_NAME = "ScriptControlConfig";

        public static string GetConfigForEngine(string engine)
        {
            return String.Format("{0}_{1}", engine, DOCUMENT_CONFIG_NAME);
        }

        private ScriptDocument _document;        
        private string _currText;
        private bool bIsDirty = false;        

        private static string EngineToHighlight(string engine)
        {
            if (_highlightingByEngine.ContainsKey(engine))
            {
                return _highlightingByEngine[engine].Name;
            }

            return null;
        }

        private static string EngineToExtension(string engine)
        {
            if (_highlightingByEngine.ContainsKey(engine))
            {
                return _highlightingByEngine[engine].FileExtension;
            }

            return null;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool ret = true;

            switch (keyData)
            {
                case (Keys.Control | Keys.S): 
                    toolStripButtonSave.PerformClick();
                    break;
                case (Keys.Control | Keys.Shift | Keys.B):
                    toolStripButtonParse.PerformClick();
                    break;
                case Keys.F3:
                    toolStripButtonNextSearch.PerformClick();
                    break;
                case (Keys.Shift | Keys.F3):
                    toolStripButtonPreviousSearch.PerformClick();
                    break;
                case (Keys.Control | Keys.G):
                    gotoLineToolStripMenuItem.PerformClick();
                    break;
                default:
                    ret = base.ProcessCmdKey(ref msg, keyData);
                    break;
            }

            return ret;
        }

        private void UpdateConfig(ScriptDocumentControlConfig config)
        {
            _config = config;
            textEditorControl.ShowSpaces = _config.ShowSpaces;
            textEditorControl.ShowTabs = _config.ShowTabs;
            textEditorControl.ConvertTabsToSpaces = _config.ConvertTabsToSpaces;
            textEditorControl.ShowLineNumbers = !_config.HideLineNumbers;
            textEditorControl.ShowEOLMarkers = _config.ShowEndofLineMarkers;

        }

        // TODO: Fix this :)
        public ScriptDocumentControl(IDocumentObject document, bool forParserDocument)
        {
            _document = (ScriptDocument)document;
            _currText = _document.Script;

            InitializeComponent();

            Text = _document.Name;

            ScriptDocumentControlConfig config = _document.GetProperty<ScriptDocumentControlConfig>(DOCUMENT_CONFIG_NAME, false);
            
            UpdateConfig(config ?? DocumentControl.GetConfigItem<ScriptDocumentControlConfig>(GetConfigForEngine(_document.Container.Engine), true));
           
            LoadFile(_document.Script);

            _forParserDocument = forParserDocument;

            if (!forParserDocument)
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
                }
            }

            textEditorControl.ActiveTextAreaControl.Document.UndoStack.OperationPushed +=
                new OperationEventHandler(UndoStack_OperationPushed);
            textEditorControl.ActiveTextAreaControl.Document.UndoStack.ActionUndone += new EventHandler(UndoStack_ActionUndone);
            textEditorControl.ActiveTextAreaControl.Document.UndoStack.ActionRedone += new EventHandler(UndoStack_ActionUndone);

            if (GlobalControlConfig.ScriptEditorFont != null)
            {
                textEditorControl.Font = GlobalControlConfig.ScriptEditorFont;
            }

            if (forParserDocument)
            {
                toolStripButtonSave.Visible = false;
                toolStripButtonOptions.Visible = false;
                toolStripButtonOpenTest.Visible = false;
            }
        }

        public ScriptDocumentControl(IDocumentObject document) 
            : this(document, false)
        {            
        }

        private void LoadFile(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            MemoryStream stm = new MemoryStream(data);
            textEditorControl.Encoding = Encoding.UTF8;
            textEditorControl.Text = str;            
            string highLight = EngineToHighlight(_document.Container.Engine);
            if (highLight != null)
            {
                textEditorControl.SetHighlighting(highLight);
            }

            bIsDirty = false;
            toolStripButtonSave.Enabled = false;
        }

        private void UpdateUndo()
        {
            toolStripButtonUndo.Enabled = textEditorControl.ActiveTextAreaControl.Document.UndoStack.CanUndo;
            toolStripButtonRedo.Enabled = textEditorControl.ActiveTextAreaControl.Document.UndoStack.CanRedo;
        }

        void UndoStack_ActionUndone(object sender, EventArgs e)
        {
            UpdateUndo();
        }

        void UndoStack_OperationPushed(object sender, OperationEventArgs e)
        {
            UpdateUndo();
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bIsDirty)
            {
                DialogResult res = MessageBox.Show(Properties.Resources.ScriptDocumentControl_SaveScript, 
                    String.Format(Properties.Resources.ScriptDocumentControl_SaveScriptCaption, _document.Name),
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveData();
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    bIsDirty = false;
                }
            }
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {                
                string ext = EngineToExtension(_document.Container.Engine);

                dlg.Filter = Properties.Resources.AllFilesFilter_String;
                if (ext != null)
                {
                    dlg.Filter = String.Format(Properties.Resources.ScriptDocumentControl_SourceFilter,
                        EngineToHighlight(_document.Container.Engine), ext, dlg.Filter);
                }

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        string script = File.ReadAllText(dlg.FileName);

                        textEditorControl.Text = script;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveData()
        {
            if (_document.Script != _currText)
            {
                _document.Script = _currText;
            }

            bIsDirty = false;
            toolStripButtonSave.Enabled = false;
        }        

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            bool doSave = true;

            if (!InternalParseScript())
            {
                if (MessageBox.Show(this, Properties.Resources.ScriptDocumentControl_ParseFailedSaveScript, 
                    Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    doSave = false;
                }
            }

            if (doSave)
            {
                SaveData();
            }
        }

        private bool InternalParseScript()
        {
            bool ret = false;
            listViewErrors.Items.Clear();
            ScriptContainer newContainer = new ScriptContainer(_document.Container, _currText);

            ScriptError[] errs = ScriptUtils.Parse(newContainer);

            if (errs.Length == 0)
            {
                ret = true;
            }
            else
            {
                foreach (var err in errs)
                {
                    ListViewItem item = listViewErrors.Items.Add(err.Severity);
                    item.SubItems.Add(err.Description);
                    item.SubItems.Add(err.Line.ToString());
                    item.SubItems.Add(err.Column.ToString());
                    item.Tag = err;
                }
            }

            return ret;
        }

        private void toolStripButtonParse_Click(object sender, EventArgs e)
        {
            if (InternalParseScript())
            {
                MessageBox.Show(this, Properties.Resources.ScriptDocumentControl_ScriptOk, Properties.Resources.ScriptDocumentControl_ScriptOkCaption, 
                    MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void textEditorControl_TextChanged(object sender, EventArgs e)
        {           
            bIsDirty = true;
            toolStripButtonSave.Enabled = true;
            _currText = textEditorControl.Text;

            // In parser document always save
            if (_forParserDocument)
            {
                SaveData();
            }
        }

        private void listViewErrors_DoubleClick(object sender, EventArgs e)
        {
            if (listViewErrors.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewErrors.SelectedItems[0];
                ScriptError err = item.Tag as ScriptError;

                if (err.Line > 0) 
                {
                   GotoText(err.Line, err.Column);
                }
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            CutText();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            CopyText();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            PasteText();
        }

        private void GotoText(int line, int column)
        {            
           textEditorControl.ActiveTextAreaControl.JumpTo(line - 1, column);            
        }

        private void CopyText()
        {            
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null, null);            
        }

        private void CutText()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null, null);
        }

        private void PasteText()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null, null);
            
        }

        private void toolStripButtonSaveToFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                string ext = EngineToExtension(_document.Container.Engine);

                dlg.Filter = Properties.Resources.AllFilesFilter_String;
                if (ext != null)
                {
                    dlg.Filter = String.Format(Properties.Resources.ScriptDocumentControl_SourceFilter, 
                        EngineToHighlight(_document.Container.Engine), ext, dlg.Filter);
                }

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(dlg.FileName, _document.Container.Script);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripButtonOptions_Click(object sender, EventArgs e)
        {
            using (ScriptOptionsForm frm = new ScriptOptionsForm(_document))
            {
                frm.ShowDialog();
            }
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

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextAreaClipboardHandler handler = textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler;

            copyToolStripMenuItem.Enabled = handler.EnableCopy;
            cutToolStripMenuItem.Enabled = handler.EnableCut;
            pasteToolStripMenuItem.Enabled = handler.EnablePaste;
            deleteToolStripMenuItem.Enabled = handler.EnableDelete;
            selectAllToolStripMenuItem.Enabled = handler.EnableSelectAll;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(sender, e);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(this, e);
        }

        private void toolStripTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            bool textValid = !String.IsNullOrWhiteSpace(toolStripTextBoxSearch.Text);            
            toolStripButtonNextSearch.Enabled = textValid;
            toolStripButtonPreviousSearch.Enabled = textValid;
        }

        private void DoSearch(bool next)
        {
            string s = toolStripTextBoxSearch.Text;
            string text = textEditorControl.Text;            
            int pos = 0;

            if (textEditorControl.ActiveTextAreaControl.SelectionManager.SelectionCollection.Count > 0)
            {
                pos = textEditorControl.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;
            }
            else
            {
                pos = textEditorControl.ActiveTextAreaControl.Caret.Offset;
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
                textEditorControl.ActiveTextAreaControl.SelectionManager.ClearSelection();

                TextLocation startSelection = textEditorControl.ActiveTextAreaControl.Document.OffsetToPosition(index);
                TextLocation endSelection = textEditorControl.ActiveTextAreaControl.Document.OffsetToPosition(index + s.Length);
                textEditorControl.ActiveTextAreaControl.SelectionManager.SetSelection(startSelection, endSelection);
                textEditorControl.ActiveTextAreaControl.Caret.Position = startSelection;
                textEditorControl.ActiveTextAreaControl.ScrollTo(startSelection.Line, startSelection.Column);
            }
            else
            {
                MessageBox.Show(this, CANAPE.Properties.Resources.ScriptDocumentControl_NoMatch,
                    CANAPE.Properties.Resources.ScriptDocumentControl_NoMatchCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonNextSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textEditorControl.Text))
            {
                DoSearch(true);
            }
        }

        private void toolStripButtonPreviousSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textEditorControl.Text))
            {
                DoSearch(false);
            }
        }

        private void gotoLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (GotoLineForm frm = new GotoLineForm())
            {
                frm.CurrentLine = textEditorControl.ActiveTextAreaControl.Caret.Line + 1;
                frm.MaximumLine = textEditorControl.Document.TotalNumberOfLines;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    GotoText(frm.CurrentLine, textEditorControl.ActiveTextAreaControl.Caret.Column);
                }
            }
        }

        private void toolStripTextBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                toolStripButtonNextSearch.PerformClick();
            }
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {            
            textEditorControl.ActiveTextAreaControl.Document.UndoStack.Undo();
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            textEditorControl.ActiveTextAreaControl.Document.UndoStack.Redo();
        }

        private void toolStripButtonOpenTest_Click(object sender, EventArgs e)
        {
            ScriptTestDocument testDocument = null;

            foreach(ScriptTestDocument doc in CANAPEProject.CurrentProject.GetDocumentsByType(typeof(ScriptTestDocument)))
            {
                if(doc.Document == _document)
                {
                    testDocument = doc;
                    break;
                }
            }

            if (testDocument == null)
            {
                testDocument = new ScriptTestDocument(_document);
                testDocument.Name = String.Format("Test {0}", _document.Name);

                CANAPEProject.CurrentProject.AddDocument(testDocument, false);
            }

            DocumentControl.Show(testDocument);
        }

        private void editorConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ScriptEditorConfigurationForm frm = new ScriptEditorConfigurationForm(_config))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    UpdateConfig(frm.Config);
                    _document.SetProperty(DOCUMENT_CONFIG_NAME, _config);
                }
            }
        }
    }
}
