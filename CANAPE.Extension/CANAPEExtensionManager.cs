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
using System.Xml;
using System.Linq;
using CANAPE.Utils;
using System.Globalization;
using System.Text;

namespace CANAPE.Extension
{
    /// <summary>
    /// Static class to handle extensions
    /// </summary>
    public static class CANAPEExtensionManager
    {
        private static Dictionary<string, Assembly> _extensionAsms;

        static CANAPEExtensionManager()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Extension_AssemblyResolve);            
            _extensionAsms = new Dictionary<string, Assembly>();
        

        /// <summary>
        /// Resolve an assembly, ensures that we lookup any registered extensions as Assembly.Load might fail
        /// if it cannot find it in its paths. Also fix up old public key token if needed
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The arguments</param>
        /// <returns>The loaded assembly if found, otherwise null</returns>
        static Assembly Extension_AssemblyResolve(object sender, ResolveEventArgs args)
        {            
            if (_extensionAsms.ContainsKey(args.Name))
            {
                return _extensionAsms[args.Name];
            }
            else 
            {
                return null;
            }
        }

        private static string BuildExtensionDir(string baseDir)
        {
            return Path.Combine(baseDir, "Extensions");
        }

        /// <summary>
        /// Load an extension assembly
        /// </summary>
        /// <param name="asm">The assembly</param>
        private static void LoadExtension(Assembly asm, bool unregister)
        {
            try
            {                
                Type[] ts = null;
                bool registered = false;

                try
                {
                    ts = asm.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    ts = ex.Types;
                }

                foreach (Type t in asm.GetTypes())
                {
                    if (t.IsClass && t.IsPublic && !t.IsAbstract)
                    {
                        // We don't load anything with obsolete attribute
                        if (!t.IsDefined(typeof(ObsoleteAttribute), true))
                        {                            
                            object[] attrs = t.GetCustomAttributes(typeof(CANAPEExtensionAttribute), false);

                            if (attrs.Length > 0)
                            {
                                foreach (CANAPEExtensionAttribute attr in attrs)
                                {
                                    if (unregister)
                                    {
                                        attr.UnregisterType(t);
                                    }
                                    else
                                    {
                                        attr.RegisterType(t);
                                        registered = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (unregister)
                {
                    _extensionAsms.Remove(asm.FullName);
                }
                else
                {
                    if (registered)
                    {
                        _extensionAsms[asm.FullName] = asm;
                    }
                }
            }
            catch (BadImageFormatException)
            {
                // Invalid image
            }
            catch (IOException)
            {
                // Shouldn't happen really
            }
            catch (ReflectionTypeLoadException)
            {
                // Couldn't initialize the types
            }
        }

        /// <summary>
        /// Load an extension assembly
        /// </summary>
        /// <param name="asm">The assembly</param>
        public static void LoadExtension(Assembly asm)
        {
            object[] attrs = asm.GetCustomAttributes(typeof(CANAPERequiredVersionAttribute), false);
            bool load = true;

            if (attrs.Length > 0)
            {
                CANAPERequiredVersionAttribute ver = (CANAPERequiredVersionAttribute)attrs[0];
                load = ver.MatchedVersion();                
            }

            if (load)
            {
                LoadExtension(asm, false);
            }
        }

        /// <summary>
        /// Load an extension assembly from a path
        /// </summary>
        /// <param name="path">The path to the assembly</param>
        public static void LoadExtension(string path)
        {
            try
            {
                Assembly asm = Assembly.LoadFrom(path);

                LoadExtension(asm);
            }
            catch (BadImageFormatException)
            {
                // Invalid image
            }
            catch (IOException)
            {
                // Shouldn't happen really
            }
            catch (ReflectionTypeLoadException)
            {
                // Couldn't initialize the types
            }
        }

        /// <summary>
        /// Load extensions from a directory, checks all .DLL files
        /// </summary>
        /// <param name="dir">The directory to load extensions from</param>
        public static void LoadExtensions(string dir)
        {
            if (dir != null)
            {
                string libraryDir = BuildExtensionDir(dir);

                if (Directory.Exists(libraryDir))
                {
                    try
                    {
                        string[] exts = Directory.GetFiles(libraryDir, "*.dll");

                        foreach (string ext in exts)
                        {
                            LoadExtension(Path.Combine(libraryDir, ext));
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Unload all extensions which come from this assembly, note it doesn't actually unload the 
        /// assembly itself as it can't do that.
        /// </summary>
        /// <param name="asm">The assembly</param>
        public static void UnloadExtension(Assembly asm)
        {
            LoadExtension(asm, true);
        }

        /// <summary>
        /// Get templates based on a particular type (as well as from the config)
        /// </summary>
        /// <param name="type">The type to base the location on, if null then only gets from the config directory</param>
        /// <param name="typeName">Get the name of the sub directory to search</param>
        /// <returns>A list of templates</returns>
        public static IEnumerable<CANAPETemplate> GetTemplates(Type type, string typeName)
        {
            List<CANAPETemplate> templates = new List<CANAPETemplate>();

            if(type != null)
            {
                Uri path = new Uri(type.Assembly.CodeBase);
                templates.AddRange(GetTemplatesFromDir(Path.GetDirectoryName(path.LocalPath), typeName));
            }

            templates.AddRange(GetTemplatesFromDir(GeneralUtils.GetConfigDirectory(), typeName));

            return templates;
        }

        private static List<CANAPETemplate> GetTemplatesFromFile(string indexPath, string basePath, string typeName)
        {
            List<CANAPETemplate> templates = new List<CANAPETemplate>();

            try
            {
                if (File.Exists(indexPath))
                {
                    XmlDocument doc = new XmlDocument();
                
                    doc.Load(indexPath);

                    XmlNodeList nodes = doc.SelectNodes("/templates/template");

                    foreach (XmlNode node in nodes)
                    {
                        XmlElement element = node as XmlElement;

                        if (element != null)
                        {
                            CANAPETemplate template = CANAPETemplate.ReadFromXml(element, basePath, typeName);
                            if (template != null)
                            {
                                templates.Add(template);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (XmlException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }

            return templates;
        }

        private static List<CANAPETemplate> GetTemplatesFromDir(string directory, string typeName)
        {
            string basePath = Path.Combine(directory, "templates", typeName);
            string indexPath = Path.Combine(basePath, "index.xml");

            Dictionary<string, CANAPETemplate> templates = new Dictionary<string, CANAPETemplate>();

            GetTemplatesFromFile(indexPath, basePath, typeName).ForEach(t => templates[t.Id] = t);
            GetTemplatesFromFile(Path.ChangeExtension(indexPath, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName + ".xml"), basePath, typeName).ForEach(t => templates[t.Id] = t);
            GetTemplatesFromFile(Path.ChangeExtension(indexPath, CultureInfo.CurrentUICulture.Name + ".xml"), basePath, typeName).ForEach(t => templates[t.Id] = t);

            return templates.Values.ToList();
        }

        /// <summary>
        /// Get all extension assemblies
        /// </summary>
        public static Dictionary<string, Assembly> ExtensionAssemblies 
        {
            get
            {
                return new Dictionary<string, Assembly>(_extensionAsms);
            }
        }

        /// <summary>
        /// Get the current list of extension assemblies, only ones under user director though
        /// </summary>
        public static Dictionary<string, Assembly> GetUserExtensionAssemblies()
        {            
            string configDir = GeneralUtils.GetConfigDirectory();
            Dictionary<string, Assembly> ret = new Dictionary<string,Assembly>();

            if (configDir != null)
            {
                configDir = Path.GetFullPath(BuildExtensionDir(configDir));

                foreach (KeyValuePair<string, Assembly> pair in _extensionAsms)
                {
                    try
                    {
                        Uri codeBase = new Uri(pair.Value.CodeBase);

                        string localPath = Path.GetFullPath(Path.GetDirectoryName(codeBase.LocalPath));

                        if (localPath.Equals(configDir, StringComparison.CurrentCultureIgnoreCase))
                        {
                            ret[pair.Key] = pair.Value;
                        }
                    }
                    catch (InvalidOperationException)
                    {

                    }
                    catch (UriFormatException)
                    {
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Get the user's extension directory
        /// </summary>
        /// <param name="create">True to create the directory</param>
        /// <returns>The extension directory, null if not created</returns>
        public static string GetUserExtensionDirectory(bool create)
        {
            string configDir = GeneralUtils.GetConfigDirectory();

            if (configDir != null)
            {
                configDir = BuildExtensionDir(configDir);
               
                if (create)
                {
                    if (!Directory.Exists(configDir))
                    {
                        try
                        {
                            Directory.CreateDirectory(configDir);
                        }
                        catch (IOException)
                        {                            
                            configDir = null;
                        }
                    }
                }
            }

            return configDir;
        }
    }
}
