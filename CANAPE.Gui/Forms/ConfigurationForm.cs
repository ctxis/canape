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
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Controls.DocumentEditors;
using CANAPE.Documents.Net.Factories;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Forms
{
    internal partial class ConfigurationForm : Form
    {
        class ScriptEngineConfig
        {
            public string EngineName { get; set; }
            public string Description { get; set; }
            public ScriptDocumentControlConfig Config { get; set; }            

            public override string ToString()
            {
                return Description;
            }
        }

        ScriptEngineConfig _currentEngineConfig;
        Font _scriptFont;

        private void UpdateFontLabel()
        {
            Font currFont = _scriptFont ?? new Font(FontFamily.GenericMonospace, 10.0f);

            lblFont.Text = String.Format("{0} {1}pt", currFont.Name, Math.Round(currFont.SizeInPoints));

        }

        private void ConfigFromSettings()
        {
            if (Properties.Settings.Default.ProxyClient != null)
            {
                try
                {
                    proxyClientControl.Client = (IProxyClientFactory)GeneralUtils.StringToObject(Properties.Settings.Default.ProxyClient);
                }
                catch
                {
                    Properties.Settings.Default.ProxyClient = null;
                    proxyClientControl.Client = null;
                }
            }

            comboBoxScriptType.Items.Clear();

            foreach (string engine in ScriptEngineFactory.Engines)
            {
                ScriptEngineConfig config = new ScriptEngineConfig()
                {
                    EngineName = engine,
                    Description = ScriptEngineFactory.GetDescriptionForEngine(engine),
                    Config = DocumentControl.GetConfigItem<ScriptDocumentControlConfig>(ScriptDocumentControl.GetConfigForEngine(engine), true)
                };

                comboBoxScriptType.Items.Add(config);
            }

            comboBoxScriptType.SelectedIndex = 0;
            
            checkBoxCompress.Checked = Properties.Settings.Default.Compressed;
            checkBoxBackup.Checked = Properties.Settings.Default.MakeBackup;
            checkBoxShowSplash.Checked = !Properties.Settings.Default.DontShowSplash;
            checkBoxCheckUpdates.Checked = Properties.Settings.Default.CheckForUpdates;
            checkBoxShowStartupForm.Checked = Properties.Settings.Default.ShowStartupForm;
            checkBoxPacketLogMutable.Checked = Properties.Settings.Default.NetServicePacketLogMutable;
            checkBoxConfirmClose.Checked = Properties.Settings.Default.ConfirmOnExit;
            checkBoxRequireEventLogClearConfirm.Checked = Properties.Settings.Default.EventLogClearConfirm;
            checkBoxRequirePacketLogClearConfirm.Checked = Properties.Settings.Default.PacketLogClearConfirm;
            checkBoxNewStyle.Checked = Properties.Settings.Default.NewStyleLogViewer;
            checkBoxOpenFindInDialog.Checked = Properties.Settings.Default.OpenFindWindowsInDialog;
            checkBoxAutoSave.Checked = Properties.Settings.Default.AutoSaveEnabled;
            numericUpDownAutoSaveTimer.Value = Properties.Settings.Default.AutoSaveTimerMins;            
            _scriptFont = Properties.Settings.Default.ScriptEditorFont;
            UpdateFontLabel();
            
            if (Enum.IsDefined(typeof(PacketLogConfirmMode), Properties.Settings.Default.PacketLogConfirmMode))
            {
                comboBoxLogConfirm.SelectedIndex = (int)Properties.Settings.Default.PacketLogConfirmMode;
            }
            else
            {
                comboBoxLogConfirm.SelectedIndex = 0;
            }

            ReadOnlyCollection<CultureInfo> languages = GetAvailableCultures();
            string currentLanguage = Properties.Settings.Default.CurrentLanguage;

            CultureInfo currentCulture = null;

            comboBoxLanguage.Items.Clear();

            foreach (CultureInfo culture in languages)
            {
                comboBoxLanguage.Items.Add(culture);
                if (culture.Name == Properties.Settings.Default.CurrentLanguage)
                {
                    currentCulture = culture;
                }
            }

            // English should always be the first culture
            if (currentCulture != null)
            {
                comboBoxLanguage.SelectedItem = currentCulture;
            }
            else
            {
                comboBoxLanguage.SelectedIndex = 0;
            }
        }

        public ConfigurationForm()
        {           
            InitializeComponent();
            ConfigFromSettings();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;

            Properties.Settings.Default.ProxyClient = GeneralUtils.ObjectToString(proxyClientControl.Client);

            foreach (ScriptEngineConfig config in comboBoxScriptType.Items)
            {
                DocumentControl.SetConfigItem(ScriptDocumentControl.GetConfigForEngine(config.EngineName), config.Config);
            }                 

            Properties.Settings.Default.Compressed = checkBoxCompress.Checked;
            Properties.Settings.Default.MakeBackup = checkBoxBackup.Checked;
            Properties.Settings.Default.DontShowSplash = !checkBoxShowSplash.Checked;
            Properties.Settings.Default.CheckForUpdates = checkBoxCheckUpdates.Checked;
            Properties.Settings.Default.ShowStartupForm = checkBoxShowStartupForm.Checked;
            Properties.Settings.Default.NetServicePacketLogMutable = checkBoxPacketLogMutable.Checked;
            Properties.Settings.Default.ConfirmOnExit = checkBoxConfirmClose.Checked;
            Properties.Settings.Default.EventLogClearConfirm = checkBoxRequireEventLogClearConfirm.Checked;
            Properties.Settings.Default.PacketLogClearConfirm = checkBoxRequirePacketLogClearConfirm.Checked;
            Properties.Settings.Default.NewStyleLogViewer = checkBoxNewStyle.Checked;
            Properties.Settings.Default.PacketLogConfirmMode = (PacketLogConfirmMode)comboBoxLogConfirm.SelectedIndex;
            Properties.Settings.Default.OpenFindWindowsInDialog = checkBoxOpenFindInDialog.Checked;
            Properties.Settings.Default.AutoSaveEnabled = checkBoxAutoSave.Checked;
            Properties.Settings.Default.AutoSaveTimerMins = (int)numericUpDownAutoSaveTimer.Value;            
            Properties.Settings.Default.ScriptEditorFont = _scriptFont;

            CultureInfo culture = comboBoxLanguage.SelectedItem as CultureInfo;

            if (culture != null)
            {
                string currentLanguage = Properties.Settings.Default.CurrentLanguage ?? String.Empty;

                if (currentLanguage != culture.Name) 
                {
                    Properties.Settings.Default.CurrentLanguage = culture.Name;
                    MessageBox.Show(this, Properties.Resources.ConfigurationForm_MustRestartToChangeLanguage,
                        Properties.Resources.ConfigurationForm_MustRestartToChangeLanguageCaption,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            Program.SaveSettings();

            Close();
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, Properties.Resources.ConfigurationForm_ClearHistory, Properties.Resources.ConfigurationForm_ClearHistoryCaption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Properties.Settings.Default.RecentFiles != null)
                {
                    Properties.Settings.Default.RecentFiles.Clear();
                    Program.SaveSettings();
                }
            }
        }

        private class DefaultCultureInfo : CultureInfo
        {
            public DefaultCultureInfo() : base("en", true)
            {
            }

            public override string Name
            {
                get
                {
                    return "";
                }
            }

            public override string NativeName
            {
                get
                {
                    return Properties.Resources.ConfigurationForm_DefaultLanguage;
                }
            }
        }

        private static ReadOnlyCollection<CultureInfo> GetAvailableCultures()
        {
            List<CultureInfo> list = new List<CultureInfo>();

            list.Add(new DefaultCultureInfo());

            string startupDir = GeneralUtils.GetCanapeInstallDirectory();
            Assembly asm = Assembly.GetEntryAssembly();

            CultureInfo neutralCulture = CultureInfo.InvariantCulture;
            if (asm != null)
            {
                NeutralResourcesLanguageAttribute attr = Attribute.GetCustomAttribute(asm, typeof(NeutralResourcesLanguageAttribute)) as NeutralResourcesLanguageAttribute;
                if (attr != null)
                    neutralCulture = CultureInfo.GetCultureInfo(attr.CultureName);
            }
            list.Add(neutralCulture);

            if (asm != null)
            {
                string baseName = asm.GetName().Name;
                foreach (string dir in Directory.GetDirectories(startupDir))
                {
                    // Check that the directory name is a valid culture
                    DirectoryInfo dirinfo = new DirectoryInfo(dir);
                    CultureInfo tCulture = null;
                    try
                    {
                        tCulture = CultureInfo.GetCultureInfo(dirinfo.Name);
                    }
                    // Not a valid culture : skip that directory
                    catch (ArgumentException)
                    {
                        continue;
                    }

                    // Check that the directory contains satellite assemblies
                    if (dirinfo.GetFiles(baseName + ".resources.dll").Length > 0)
                    {
                        list.Add(tCulture);
                    }
                }
            }
            return list.AsReadOnly();
        }

        private void btnResetToDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, Properties.Resources.ConfigurationForm_ResetToDefaults, Properties.Resources.ConfigurationForm_ResetToDefaultsCaption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
                MainForm.ClearControlConfig();
                Program.SaveSettings();
                ConfigFromSettings();
            }
        }

        private void comboBoxScriptType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            _currentEngineConfig = (ScriptEngineConfig)comboBoxScriptType.SelectedItem;

            scriptDocumentConfigEditorControl.Config = _currentEngineConfig.Config; 
        }

        private void scriptDocumentConfigEditorControl_ConfigChanged(object sender, EventArgs e)
        {
            _currentEngineConfig.Config = scriptDocumentConfigEditorControl.Config;
        }
   
        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new FontDialog())
            {
                dlg.Font = _scriptFont ?? new Font(FontFamily.GenericMonospace, 10.0f);

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _scriptFont = dlg.Font;
                    UpdateFontLabel();
                }
            }
        }
    }
}
