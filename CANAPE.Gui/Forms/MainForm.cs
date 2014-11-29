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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Controls.DocumentEditors;
using CANAPE.Controls.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Net.Factories;
using CANAPE.Extension;
using CANAPE.Scripting;
using CANAPE.Security.Cryptography.X509Certificates;
using CANAPE.Utils;
using WeifenLuo.WinFormsUI.Docking;

namespace CANAPE.Forms
{
    /// <summary>
    /// Main GUI form
    /// </summary>
    public partial class MainForm : Form, IDocumentControl
    {
        Dictionary<IDocumentObject, DockContent> _entries;
        ProjectExplorer _projectExplorer;
        DebugWindowForm _debugWindow;
        string _fileName;
        Task _backupTask;

        const double UPDATE_PERIOD_DAYS = 7.0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">A file to open at startup</param>
        public MainForm(string fileName)
        {
            if (!Properties.Settings.Default.DontShowSplash)
            {
                using (AboutForm frm = new AboutForm(true))
                {
                    frm.ShowDialog();
                }
            }            

            InitializeComponent();

            MainFormMenuExtensionManager.Instance.AddToMenu(menuStrip, extensionToolStripMenuItem, ExtensionMenuClicked);

            CANAPEServiceProvider.GlobalInstance.RegisterService(typeof(IDocumentControl), this);
            _projectExplorer = new ProjectExplorer();
            _fileName = fileName;
            _entries = new Dictionary<IDocumentObject, DockContent>();

            // Check for help file precense
            aPIHelpToolStripMenuItem.Enabled = File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Help", "CANAPE_Documentation.chm"));            
        }

        private void ExtensionMenuClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if ((item != null) && (item.Tag is Type))
            {
                Type t = (Type)item.Tag;

                IMainFormMenuExtension ext = (IMainFormMenuExtension)Activator.CreateInstance(t);

                ext.Execute(this, item);
            }
        }

        private void InitializeTree()
        {
            foreach (KeyValuePair<IDocumentObject, DockContent> entry in _entries)
            {
                DockContent win = entry.Value;

                if ((win != null) && (!win.IsDisposed))
                {
                    win.Dispose();
                }
            }
            _entries.Clear();

            _projectExplorer.ClearEntries();
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetConfigFromProperties()
        {
            NetServiceDocumentControl.PacketLogMutable = Properties.Settings.Default.NetServicePacketLogMutable;
            GlobalControlConfig.NewStyleLogViewer = Properties.Settings.Default.NewStyleLogViewer;
            GlobalControlConfig.RequireEventLogClearConfirmation = Properties.Settings.Default.EventLogClearConfirm;
            GlobalControlConfig.RequirePacketLogClearConfirmation = Properties.Settings.Default.PacketLogClearConfirm;
            GlobalControlConfig.LogConfirmMode = Properties.Settings.Default.PacketLogConfirmMode;
            GlobalControlConfig.OpenLogFindInWindow = Properties.Settings.Default.OpenFindWindowsInDialog;
            GlobalControlConfig.EnableCsCodeCompletion = Properties.Settings.Default.EnableCSCodeCompletion;
            GlobalControlConfig.ScriptEditorFont = Properties.Settings.Default.ScriptEditorFont;
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ProjectExplorer).ToString())
            {
                _projectExplorer.DockPanel = null;
                return _projectExplorer;
            }
            else
            {
                return null;
            }
        }

        private void SetTitle(string fileName)
        {
            Text = String.Format(GeneralUtils.GetCurrentCulture(), Properties.Resources.MainForm_TitleText,
                fileName ?? Properties.Resources.MainForm_UntitledFile);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bool loaded = false;           

            if (!String.IsNullOrEmpty(Properties.Settings.Default.ScriptConfig))
            {
                try
                {
                    ScriptDocumentControlConfig config = (ScriptDocumentControlConfig) GeneralUtils.StringToObject(Properties.Settings.Default.ScriptConfig);

                    foreach (string engine in ScriptEngineFactory.Engines)
                    {
                        SetConfigItem(ScriptDocumentControl.GetConfigForEngine(engine), config);  
                    }                                      
                }
                catch
                {
                }

                Properties.Settings.Default.ScriptConfig = null;
                Program.SaveSettings();
            }

            SetConfigFromProperties();
            
            if(!loaded)
            {
                _projectExplorer.Show(dockPanel);
                _projectExplorer.DockState = DockState.DockRight;
            }

            if (_fileName == null)
            {
                if (Properties.Settings.Default.ShowStartupForm)
                {
                    using (StartupForm newProject = new StartupForm())
                    {
                        if (newProject.ShowDialog(this) == DialogResult.OK)
                        {
                            if (newProject.FileName == null)
                            {
                                NewDocument(true);
                            }
                            else
                            {
                                LoadProject(newProject.FileName, true, true);
                            }
                        }
                        else
                        {
                            NewDocument(false);
                        }
                    }
                }
            }
            else
            {
                LoadProject(_fileName, true, true);
            }

            if (Properties.Settings.Default.AutoSaveEnabled)
            {
                timerAutoSaveTimer.Interval = Properties.Settings.Default.AutoSaveTimerMins * 60 * 1000;
                timerAutoSaveTimer.Enabled = true;
            }
     
        }
       
        private void projectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _projectExplorer.Show(dockPanel, DockState.DockRight);
        }

        private void UpdateRecentFiles(string file)
        {
            StringCollection coll = Properties.Settings.Default.RecentFiles;
            int index = -1;

            if (coll == null)
            {
                coll = new StringCollection();
                Properties.Settings.Default.RecentFiles = coll;
            }

            for (int i = 0; i < coll.Count; ++i)
            {
                if (coll[i].Equals(file, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                coll.RemoveAt(index);
            }
            else
            {
                if (coll.Count > 4)
                {
                    coll.RemoveAt(4);
                }
            }

            coll.Insert(0, file);

            Program.SaveSettings();
        }

        private void SaveDocument(bool bSaveAs)
        {
            string fileName = null;

            if (bSaveAs || (CANAPEProject.CurrentProject.FileName == null))
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = Properties.Resources.OpenFileDialog_ProjectFilter;
                    dlg.OverwritePrompt = true;

                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = dlg.FileName;
                    }
                }
            }
            else
            {
                fileName = CANAPEProject.CurrentProject.FileName;
            }
            
            if(fileName != null)
            {                                   
                using (SavingLoadingForm frm = new SavingLoadingForm(fileName, true, true, true))
                {
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        SetTitle(CANAPEProject.CurrentProject.FileName);
                        UpdateRecentFiles(fileName);
                    }
                    else
                    {
                        MessageBox.Show(this, frm.Error.Message,
                            Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument(false);
        }

        private bool CheckDirtyFlag()
        {
            bool cancel = false;

            if (CANAPEProject.CurrentProject.IsDirty)
            {
                DialogResult res = MessageBox.Show(this, Properties.Resources.MainForm_ProjectModified,
                    Properties.Resources.MainForm_ProjectModifiedCaption,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    cancel = true;
                }
                else if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveDocument(false);
                }
            }

            return cancel;
        }

        private void LoadProject(string fileName, bool verifyVersion, bool secure)
        {
            using (SavingLoadingForm frm = new SavingLoadingForm(fileName, false, verifyVersion, secure))
            {
                InitializeTree();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    SetTitle(CANAPEProject.CurrentProject.FileName);
                    OnProjectChanged();
                    UpdateRecentFiles(fileName);                    
                }
                else
                {
                    if ((frm.Error is InvalidVersionException) && verifyVersion)
                    {
                        if (MessageBox.Show(this, String.Format(Properties.Resources.LoadProject_OpenAnyway, frm.Error.Message),
                            Properties.Resources.LoadProject_OpenAnywayCaption,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            LoadProject(fileName, false, secure);
                        }
                    }
                    else if ((frm.Error is SecurityException) && secure)
                    {
                        if (MessageBox.Show(this, String.Format(Properties.Resources.LoadProject_SecurityWarning, frm.Error.Message),
                            Properties.Resources.LoadProject_SecurityWarningCaption,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            LoadProject(fileName, verifyVersion, false);
                        }
                    }
                    else
                    {
                        if (frm.Error != null)
                        {
                            MessageBox.Show(this, frm.Error.Message,
                                Properties.Resources.MessageBox_ErrorString,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show(this, Properties.Resources.MainForm_UnknownError,
                                Properties.Resources.MessageBox_ErrorString,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckDirtyFlag())
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = Properties.Resources.OpenFileDialog_ProjectFilter;

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadProject(dlg.FileName, true, true);
                    }
                }
            }
        }

        private void RestartProgram(string fileName)
        {
            string commandLine = String.Empty;            
            string processPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;

            if(fileName != null)
            {
                commandLine = String.Format("\"{0}\"", fileName);
            }
            
            ProcessStartInfo startInfo = new ProcessStartInfo(processPath, commandLine);

            Process.Start(startInfo);
        }

        private void OnProjectChanged()
        {
            ScriptUtils.ReInitialize();            
        }

        private bool CheckForExitConfirmation()
        {
            bool ret = true;

            if (Properties.Settings.Default.ConfirmOnExit)
            {
                ret = MessageBox.Show(this, Properties.Resources.MainForm_ConfirmExit, 
                    Properties.Resources.MainForm_ConfirmExitCaption, MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning) == DialogResult.Yes;
            }

            return ret;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CheckForExitConfirmation() && !CheckDirtyFlag())
            {
                IDockContent[] docs = dockPanel.Documents.ToArray();
                foreach (DockContent frm in docs)
                {
                    frm.Close();
                }

                if (_backupTask != null)
                {
                    _backupTask.Wait(1000);
                }
            }
            else
            {
                e.Cancel = true;
            }                        
        }

        private void NewDocument(bool fromTemplate)
        {
            if (!CheckDirtyFlag())
            {
                CANAPETemplate template = null;

                if (fromTemplate)
                {
                    using (SelectProjectTemplateForm frm = new SelectProjectTemplateForm())
                    {
                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            template = frm.Template;
                        }
                    }
                }
            
                CANAPEProject.New();
                InitializeTree();
                SetTitle(null);

                if (template != null)
                {
                    try
                    {
                        // Allow insecure load, if someone has added a template they could just add a plugin
                        CANAPEProject.Load(template.GetStream(), null, true, false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
                IProxyClientFactory client = (IProxyClientFactory)GeneralUtils.StringToObject(Properties.Settings.Default.ProxyClient);
                if (client != null)
                {
                    try
                    {
                        CANAPEProject.CurrentProject.DefaultProxyClient = client;
                    }
                    catch (ArgumentException)
                    {
                        CANAPEProject.CurrentProject.DefaultProxyClient = new IpProxyClientFactory();
                    }
                }

                OnProjectChanged();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument(true);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument(true);
        }

        private void debugConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if((_debugWindow == null) || (_debugWindow.IsDisposed))
            {
                _debugWindow = new DebugWindowForm();
                _debugWindow.Show();
            }
            else
            {
                _debugWindow.Close();
            }
        }

        private void tabsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if ((_debugWindow == null) || (_debugWindow.IsDisposed))
            {
                debugConsoleToolStripMenuItem.Checked = false;
            }
            else
            {
                debugConsoleToolStripMenuItem.Checked = true;
            }                        
        }

        private void exportRootCertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = Properties.Resources.MainForm_CertSaveFilter;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        X509Certificate2 cert = CertManager.GetRootCert();

                        File.WriteAllText(dlg.FileName, CertificateUtils.ExportToPEM(cert), Encoding.ASCII);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            StringCollection coll = Properties.Settings.Default.RecentFiles;

            if ((coll != null) && (coll.Count > 0))
            {
                recentFilesToolStripMenuItem.DropDownItems.Clear();

                foreach (string file in coll)
                {
                    ToolStripItem item = recentFilesToolStripMenuItem.DropDownItems.Add(file);
                    item.Tag = file;

                    item.Click += new EventHandler(recentFileItem_Click);
                }
                recentFilesToolStripMenuItem.Enabled = true;
            }
            else
            {
                recentFilesToolStripMenuItem.Enabled = false;
            }
        }

        void recentFileItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;

            if (item != null)
            {
                if (!CheckDirtyFlag())
                {
                    LoadProject((string)item.Tag, true, true);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm frm = new AboutForm(false))
            {
                frm.ShowDialog(this);
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ConfigurationForm frm = new ConfigurationForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    SetConfigFromProperties();
                    timerAutoSaveTimer.Interval = Properties.Settings.Default.AutoSaveTimerMins * 60 * 1000;
                    timerAutoSaveTimer.Enabled = Properties.Settings.Default.AutoSaveEnabled;                  
                }
            }
        }

        /// <summary>
        /// Host a control in a new window
        /// </summary>
        /// <param name="name">The name of the window</param>
        /// <param name="control">The control to host</param>
        /// <returns>A component reference</returns>
        public IComponent HostControl(string name, Control control)
        {
            return HostControl(name, control, null);
        }

        /// <summary>
        /// Host a control in a new window with an optional parent
        /// </summary>
        /// <param name="name">The name of the window</param>
        /// <param name="control">The control to host</param>
        /// <param name="parentControl">The parent control</param>
        /// <returns>A component reference</returns>
        public IComponent HostControl(string name, Control control, Control parentControl)
        {
            if (InvokeRequired)
            {
                return (IComponent)Invoke(new Func<string, Control, Control, IComponent>(HostControl), name, control, parentControl);
            }
            else
            {
                DocumentForm form = new DocumentForm(name, control);
                DockContent dock = null;

                if (parentControl != null)
                {
                    Control c = parentControl.Parent;

                    while (c != null)
                    {
                        if (c is DockContent)
                        {
                            dock = (DockContent)c;
                            break;
                        }

                        c = c.Parent;
                    }
                }

                if (dock != null)
                {
                    form.Show(dock.PanelPane, DockAlignment.Bottom, 0.5);
                }
                else
                {
                    form.Show(this.dockPanel, DockState.Document);
                }

                return form;
            }
        }

        /// <summary>
        /// Show a document object in a form
        /// </summary>
        /// <param name="document">The document to show</param>
        public void ShowDocumentForm(IDocumentObject document)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<IDocumentObject>(ShowDocumentForm), document);
            }
            else
            {
                try
                {
                    if ((!_entries.ContainsKey(document)) || (_entries[document].IsDisposed))
                    {
                        _entries[document] = (DockContent)new DocumentForm(document);
                        _entries[document].Show(dockPanel, DockState.Document);
                    }
                    else
                    {
                        _entries[document].Show();
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(this, ex.Message, 
                        Properties.Resources.MessageBox_ErrorString, 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Rename a document form
        /// </summary>
        /// <param name="document">The document to rename</param>
        public void RenameDocumentForm(IDocumentObject document)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<IDocumentObject>(RenameDocumentForm), document);
            }
            else
            {
                if (_entries.ContainsKey(document))
                {
                    DockContent win = _entries[document];
                    if ((win != null) && !win.IsDisposed)
                    {
                        win.Text = document.Name;
                    }
                } 
            }
        }

        /// <summary>
        /// Close a document form
        /// </summary>
        /// <param name="document">The document to close</param>
        public void CloseDocumentForm(IDocumentObject document)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<IDocumentObject>(CloseDocumentForm), document);
            }
            else
            {
                if (_entries.ContainsKey(document))
                {
                    DockContent win = _entries[document];

                    if ((win != null) && (!win.IsDisposed))
                    {
                        win.Close();
                    }
                }
            }
        }

        private void installRootCertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                X509Certificate2 rootCert = new X509Certificate2(CertManager.GetRootCert().Export(X509ContentType.Cert));

                if (CertificateUtils.FindCertByThumbprint(StoreName.Root, StoreLocation.CurrentUser, rootCert.Thumbprint) != null)
                {
                    MessageBox.Show(this, Properties.Resources.MainForm_RootCertAlreadyInstalled, Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {                    
                    CertManager.AddCertToStore(StoreName.Root, StoreLocation.CurrentUser, rootCert);
                }
            }
            catch (CryptographicException)
            {

            }
        }

        private void aPIHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuiUtils.OpenDocument(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Help", "CANAPE_Documentation.chm"));
        }

        private void newEmptyProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument(false);
        }

        private void viewRootCertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                X509Certificate2UI.DisplayCertificate(CertManager.GetRootCert(), Handle);
            }
            catch (ArgumentNullException)
            {
            }
            catch (CryptographicException)
            {
            }
        }

        private void createNewCertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CreateCertForm frm = new CreateCertForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        using (SaveFileDialog dlg = new SaveFileDialog())
                        {
                            dlg.Filter = Properties.Resources.CreateCert_SaveFilter;

                            if (dlg.ShowDialog(this) == DialogResult.OK)
                            {
                                GetPasswordForm getPass = new GetPasswordForm();
                                SecureString password = null;

                                if (getPass.ShowDialog(this) == DialogResult.OK)
                                {
                                    password = getPass.Password;
                                    if (password.Length == 0)
                                    {
                                        password = null;
                                    }
                                }

                                string ext = Path.GetExtension(dlg.FileName);

                                if (ext.Equals(".pfx") || ext.Equals(".p12"))
                                {
                                    File.WriteAllBytes(dlg.FileName, frm.Certificate.Export(X509ContentType.Pfx, password));
                                }
                                else
                                {
                                    File.WriteAllText(dlg.FileName, CertificateUtils.ExportToPEM(frm.Certificate) +
                                                CertificateUtils.ExportToPEM((RSA)frm.Certificate.PrivateKey, password));
                                }
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (CryptographicException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cloneCertChainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CloneCertChainForm frm = new CloneCertChainForm())
            {
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Set the document icon
        /// </summary>
        /// <param name="document">The document to set the icon for</param>
        /// <param name="iconName">The name of the icon</param>
        /// <param name="icon">The icon itself</param>
        public void SetDocumentIcon(IDocumentObject document, string iconName, Icon icon)
        {
            if ((_projectExplorer != null) && !_projectExplorer.IsDisposed)
            {
                _projectExplorer.SetIconForDocument(document, iconName, icon);
            }
        }

        private void extensionManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ExtensionManagerForm frm = new ExtensionManagerForm())
            {
                frm.ShowDialog(this);
            }
        }

        private void timerAutoSaveTimer_Tick(object sender, EventArgs e)
        {
            if (CANAPE.Documents.CANAPEProject.CurrentProject.IsDirty)
            {
                if ((_backupTask == null) || _backupTask.IsCompleted || _backupTask.IsFaulted)
                {
                    _backupTask = Task.Run(new Action(CANAPEProject.Backup));                    
                }                
            }
        }

        private static Dictionary<string, object> _controlConfig;

        internal static void ClearControlConfig()
        {
            _controlConfig = null;
        }

        private static Dictionary<string, object> GetControlConfig()
        {            
            if (_controlConfig != null)
            {
                return _controlConfig;
            }

            try
            {
                _controlConfig = (Dictionary<string, object>)GeneralUtils.StringToObject(Properties.Settings.Default.ControlConfig);
            }
            catch
            {
            }

            if (_controlConfig == null)
            {
                _controlConfig = new Dictionary<string, object>();
            }

            return _controlConfig;
        }

        private static void UpdateControlConfig()
        {
            try
            {
                Properties.Settings.Default.ControlConfig = GeneralUtils.ObjectToString(_controlConfig);
                Program.SaveSettings();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Get a configuration item
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <returns>The item if it exists otherwise null</returns>
        public object GetConfigItem(string name)
        {
            Dictionary<string, object> config = GetControlConfig();

            if(config.ContainsKey(name))
            {                
                return config[name];                
            }

            return null;
        }

        /// <summary>
        /// Set a configuration item
        /// </summary>
        /// <param name="name">The name to set</param>
        /// <param name="value">The value to set, if null removes it</param>
        public void SetConfigItem(string name, object value)
        {
            Dictionary<string, object> config = GetControlConfig();

            if (value == null)
            {
                config.Remove(name);
            }
            else
            {
                config[name] = value;
            }

            UpdateControlConfig();            
        }

        private void viewCertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LoadCertFromFileForm frm = new LoadCertFromFileForm())
            {
                frm.NoPrivateKey = true;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    X509Certificate2UI.DisplayCertificate(frm.Certificate, this.Handle);
                }
            }
        }

        private void makeNewRootCertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CreateCertForm frm = new CreateCertForm(true))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        if (MessageBox.Show(this, Properties.Resources.MainForm_ReplaceRootCA, Properties.Resources.MessageBiox_WarningString,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            CertManager.SetRootCert(frm.Certificate);
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (CryptographicException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(this, ex.Message, Properties.Resources.MessageBox_ErrorString,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
