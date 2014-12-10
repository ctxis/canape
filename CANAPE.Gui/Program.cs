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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CANAPE.Extension;
using CANAPE.NodeConfigs;
using CANAPE.NodeFactories;
using CANAPE.NodeLibrary.Parser;
using CANAPE.Scripting;
using CANAPE.Scripting.DotNet;
using CANAPE.Utils;
using CANAPE.Net.Layers;
using CANAPE.Editors;
using CANAPE.Controls;
using System.Globalization;
using System.Threading;
using CANAPE.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Net.Factories;
using CANAPE.Documents.Data;
using CANAPE.Parser;

namespace CANAPE
{
    /// <summary>
    /// Main class
    /// </summary>
    public static class Program
    {
        private static void RegisterEditor(Type type, Type editorType)
        {
            TypeDescriptor.AddAttributes(type,
             new EditorAttribute(editorType,
                 typeof(System.Drawing.Design.UITypeEditor)));
        }

        private static void InitializeLanguage()
        {
            try
            {
                // The default is to not specify a language
                if (!String.IsNullOrWhiteSpace(Properties.Settings.Default.CurrentLanguage))
                {
                    CultureInfo culture = new CultureInfo(Properties.Settings.Default.CurrentLanguage);
                    GeneralUtils.SetCurrentCulture(culture);
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
            }
            catch(CultureNotFoundException)
            {
            }
        }

        internal static void SaveSettings()
        {
            try
            {                
                Properties.Settings.Default.Save();
            }
            catch
            {
            }
        }

        private static void InitializeLibraries()
        {
            RegisterEditor(typeof(Dictionary<string, string>), typeof(NodePropertiesEditor));
            RegisterEditor(typeof(ColorValue), typeof(ColorValueEditor));
            RegisterEditor(typeof(DataFrameFilterFactory[]), typeof(DataFrameFilterEditor));
            RegisterEditor(typeof(IDataFrameFilterFactory[]), typeof(DataFrameFilterEditor));
            RegisterEditor(typeof(byte[]), typeof(ByteArrayEditor));
            RegisterEditor(typeof(SslNetworkLayerConfig), typeof(SslConfigEditor));
            RegisterEditor(typeof(INetworkLayerFactory[]), typeof(LayersEditor));
            RegisterEditor(typeof(SequenceChoice[]), typeof(SequenceChoiceEditor));

            // Load default extensions
            CANAPEExtensionManager.LoadExtension(typeof(ScriptDocument).Assembly);
            CANAPEExtensionManager.LoadExtension(typeof(HTTPData).Assembly);            
            CANAPEExtensionManager.LoadExtension(typeof(HexEditorControl).Assembly);
            CANAPEExtensionManager.LoadExtensions(AppDomain.CurrentDomain.BaseDirectory);
            CANAPEExtensionManager.LoadExtensions(GeneralUtils.GetConfigDirectory());
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            InitializeLanguage();
            InitializeLibraries();
            string fileName = null;
            int i = 0;

            string configDir = GeneralUtils.GetConfigDirectory(true);

            if (configDir == null)
            {
                MessageBox.Show(String.Format(Properties.Resources.Program_ErrorCreatingUserDirectory, GeneralUtils.GetConfigDirectory(false)),
                    Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            for (i = 0; i < args.Length; ++i)
            {
                if (args[i].StartsWith("-"))
                {
                    if (args[i].StartsWith("-ext:"))
                    {
                        CANAPEExtensionManager.LoadExtension(args[i].Substring("-ext:".Length));
                    }
                }
                else
                {
                    break;
                }
            }

            if (i < args.Length)
            {
                fileName = args[i];
            }

            if (Properties.Settings.Default.RunOnce == false)
            {                
                MessageBox.Show(CANAPE.Properties.Resources.Program_SecurityWarning,
                    CANAPE.Properties.Resources.Program_SecurityWarningCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Properties.Settings.Default.RunOnce = true;             
                Program.SaveSettings();
            }

            if (!Debugger.IsAttached)
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm(fileName));

            ScriptUtils.DeleteTempFiles();
        }

        static void HandleException(Exception e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
            else
            {
                StringBuilder output = new StringBuilder();
                while (e != null)
                {
                    output.AppendLine(e.ToString());

                    e = e.InnerException;
                }

                try
                {
                    string path = Path.Combine(Path.GetTempPath(), "CANAPE_" + Path.GetRandomFileName());

                    File.WriteAllText(path, output.ToString());
                    MessageBox.Show(String.Format(CANAPE.Properties.Resources.FatalError_Message, path),
                    CANAPE.Properties.Resources.FatalError_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Fatal Error, exiting", CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                Environment.Exit(1);
            }
            
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }
    }
}
