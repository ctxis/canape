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
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CANAPE.DataFrames;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.Factories;
using CANAPE.Net.Layers;
using CANAPE.Nodes;
using CANAPE.Utils;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Runtime.Serialization.Formatters;

namespace CANAPE.Documents
{
    /// <summary>
    /// Event args for a document events
    /// </summary>
    public class DocumentEventArgs : EventArgs
    {
        /// <summary>
        /// The document which this event refers
        /// </summary>
        public IDocumentObject Document { get; private set; }

        internal DocumentEventArgs(IDocumentObject document)
        {
            Document = document;
        }
    }

    /// <summary>
    /// The CANAPE project, only ever a single instance at one time
    /// </summary>
    [Serializable]
    public sealed class CANAPEProject
    {
        [NonSerialized]
        private string _fileName;
        [NonSerialized]
        private bool _isNew;

        private IProxyClientFactory _defaultClient;        

        /// <summary>
        /// Name of loaded project
        /// </summary>
        public string FileName 
        {
            get
            {
                return _fileName;
            }            
        }

        /// <summary>
        /// Indicates if this project is new
        /// </summary>
        public bool IsNew
        {
            get
            {
                return _isNew && !IsDirty;
            }
        }

        /// <summary>
        /// Get list of documents
        /// </summary>
        public IEnumerable<IDocumentObject> Documents
        {
            get
            {
                return _documents.Values;
            }
        }

        /// <summary>
        /// Default client factory
        /// </summary>
        public IProxyClientFactory DefaultProxyClient 
        {
            get
            {
                return _defaultClient;                    
            }

            set
            {
                if (value == null)
                {
                    _defaultClient = new IpProxyClientFactory();
                }
                else if (value is DefaultProxyClientFactory)
                {
                    throw new ArgumentException(CANAPE.Documents.Properties.Resources.CANAPEProject_InvalidDefaultProxy);
                }
                else
                {
                    _defaultClient = value;
                }
            }
        }
        
        private Dictionary<string, IDocumentObject> _documents;
        private Dictionary<string, string> _properties;

        [NonSerialized]
        private bool _documentsModified;

        [NonSerialized]
        private MetaDictionary _globalMeta;

        private CANAPEProject()
        {            
            _documents = new Dictionary<string, IDocumentObject>();
            _properties = new Dictionary<string, string>();
            _defaultClient = new IpProxyClientFactory();
            _globalMeta = new MetaDictionary();
        }
        
        // Resolve an assembly from the project if available
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (currentProject != null)
            {
                foreach (AssemblyDocument doc in currentProject.GetDocumentsByType(typeof(AssemblyDocument)))
                {
                    AssemblyName name = doc.GetName();

                    if((name != null) && (name.FullName == args.Name))
                    {
                        return doc.GetAssembly();
                    }
                }
            }

            return null;
        }

        static CANAPEProject currentProject;

        /// <summary>
        /// Get current project
        /// </summary>
        public static CANAPEProject CurrentProject
        {
            get
            {
                if (currentProject == null)
                {
                    currentProject = new CANAPEProject();
                }

                return currentProject;
            }
        }

        private string GenerateName(string defaultName)
        {
            string ret = null;
            int count = 1;

            if(!_documents.ContainsKey(defaultName.ToLower()))
            {
                return defaultName;
            }

            while (count < int.MaxValue)
            {
                string temp = String.Format("{0} {1}", defaultName, count++);
                if (!_documents.ContainsKey(temp.ToLower()))
                {
                    ret = temp;
                    break;
                }
            }

            if (ret == null)
            {
                throw new InvalidOperationException();
            }

            return ret;
        }

        private void GenerateName(IDocumentObject document)
        {            
            document.Name = GenerateName(document.DefaultName);
        }

        private string MakeUniqueName(string name)
        {
            if (!_documents.ContainsKey(name.ToLower()))
            {
                return name;
            }
            else
            {
                return GenerateName(name);
            }
        }

        /// <summary>
        /// Set the name of a document when copied
        /// </summary>
        /// <param name="document">The document to set the name of</param>
        private void SetCopyName(IDocumentObject document)
        {
            int count = 0;
            while(count < int.MaxValue)
            {
                string temp = String.Format("{0} - Copy {1}", document.Name, count);
                if(!_documents.ContainsKey(temp.ToLower()))
                {
                    document.Name = temp;
                    break;
                }
                count++;
            }

            if(count == int.MaxValue)
            {
                throw new InvalidOperationException();
            }
        }

        private void OnDocumentAdded(IDocumentObject document)
        {
            if (DocumentAdded != null)
            {
                DocumentAdded.Invoke(this, new DocumentEventArgs(document));
            }
        }        

        /// <summary>
        /// Create a document from a type parameter
        /// </summary>
        /// <param name="docType">The document type</param>
        /// <param name="args">Arguments to the document</param>
        /// <returns>The document</returns>
        public IDocumentObject CreateDocument(Type docType, params object[] args)
        {
            return AddNewDocument((IDocumentObject)Activator.CreateInstance(docType, args));
        }

        /// <summary>
        /// Add a new document generating a name as required 
        /// </summary>
        /// <param name="document">The document to add</param>
        /// <returns>The document</returns>
        public IDocumentObject AddNewDocument(IDocumentObject document)
        {
            GenerateName(document);

            _documents.Add(document.Name.ToLower(), document);
            _documentsModified = true;

            OnDocumentAdded(document);

            return document;
        }

        /// <summary>
        /// Create a document from a a type parameter
        /// </summary>
        /// <param name="docType">The document type</param>
        /// <returns>The document</returns>
        public IDocumentObject CreateDocument(Type docType)
        {
            return CreateDocument(docType, new object[0]);
        }

        /// <summary>
        /// Create a document of a specified type
        /// </summary>
        /// <typeparam name="T">The type of document</typeparam>
        /// <returns>The new document</returns>
        public T CreateDocument<T>() where T : IDocumentObject, new()
        {
            return (T)CreateDocument(typeof(T));
        }

        /// <summary>
        /// Create a document of a specified type
        /// </summary>
        /// <param name="args">Arguments to the document</param>
        /// <typeparam name="T">The type of document</typeparam>
        /// <returns>The new document</returns>
        public T CreateDocument<T>(params object[] args) where T : IDocumentObject
        {
            return (T)CreateDocument(typeof(T), args);
        }

        /// <summary>
        /// Delete a document
        /// </summary>
        /// <param name="document">The document to delete</param>
        public void DeleteDocument(IDocumentObject document)
        {
            if (_documents.ContainsKey(document.Name.ToLower()))
            {
                _documents.Remove(document.Name.ToLower());
                _documentsModified = true;

                if (DocumentDeleted != null)
                {
                    DocumentDeleted.Invoke(this, new DocumentEventArgs(document));
                }
            }
        }

        /// <summary>
        /// Add an existing document
        /// </summary>
        /// <param name="document">The document to add</param>
        /// <param name="isCopy">Indicates if this is a copy and so needs a new name</param>
        public void AddDocument(IDocumentObject document, bool isCopy)
        {
            if (isCopy)
            {
                SetCopyName(document);
            }

            document.Name = MakeUniqueName(document.Name);

            _documents.Add(document.Name.ToLower(), document);
            _documentsModified = true;
            
            OnDocumentAdded(document);
        }

        /// <summary>
        /// Get a document by name
        /// </summary>
        /// <param name="name">The name of the document</param>
        /// <returns>The document if found, otherwise null</returns>
        public IDocumentObject GetDocumentByName(string name)
        {
            IDocumentObject ret = null;

            if (_documents.ContainsKey(name.ToLower()))
            {
                ret = _documents[name.ToLower()];
            }

            return ret;
        }

        /// <summary>
        /// Get a document by its UUID
        /// </summary>
        /// <param name="uuid">The uuid of the document</param>
        /// <returns>The document if found, otherwise null</returns>
        public IDocumentObject GetDocumentByUuid(Guid uuid)
        {
            IDocumentObject doc = null;

            foreach (var pair in _documents)
            {
                if (pair.Value.Uuid.Equals(uuid))
                {
                    doc = pair.Value;
                    break;
                }
            }

            return doc;
        }

        /// <summary>
        /// Rename document
        /// </summary>
        /// <param name="document">The document to rename</param>
        /// <param name="name">The new name for the document</param>
        /// <returns>True if successfully renamed the document</returns>
        public bool RenameDocument(IDocumentObject document, string name)
        {
            if ((document == null) || (name == null) || (_documents.ContainsKey(name.ToLower())))
            {
                return false;
            }
            else
            {
                _documents.Remove(document.Name.ToLower());
                _documents.Add(name.ToLower(), document);
                document.Name = name;
                _documentsModified = true;

                if (DocumentRenamed != null)
                {
                    DocumentRenamed.Invoke(this, new DocumentEventArgs(document));
                }
                
                return true;
            }
        }

        /// <summary>
        /// Get documents by type
        /// </summary>
        /// <param name="type">The type or base type of the document</param>
        /// <returns>A list of matching documents</returns>
        public IEnumerable<IDocumentObject> GetDocumentsByType(Type type)
        {
            return from i in _documents.Values
                   where type.IsAssignableFrom(i.GetType())
                   select i;
        }

        /// <summary>
        /// Get documents by type
        /// </summary>
        /// <typeparam name="T">The document type</typeparam>
        /// <returns>The list of matching documents</returns>
        public IEnumerable<T> GetDocumentsByType<T>() where T : IDocumentObject
        {
            return GetDocumentsByType(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Create a new document
        /// </summary>
        public static void New()
        {
            currentProject = new CANAPEProject();
            currentProject._isNew = true;
        }
        
        /// <summary>
        /// Indicate if the current document is dirty
        /// </summary>
        public bool IsDirty
        {
            get
            {                
                foreach (var pair in _documents)
                {
                    if (pair.Value.Dirty)
                    {
                        return true;
                    }
                }

                return _documentsModified;
            }            
        }

        /// <summary>
        /// Get a property value on the document
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <returns>The property, or null if not found</returns>
        public string GetProperty(string name)
        {
            string ret = null;

            lock (_properties)
            {
                if (!_properties.TryGetValue(name, out ret))
                {
                    ret = null;
                }
            }

            return ret;
        }

        /// <summary>
        /// Set a property of the project
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="value">The value</param>
        public void SetProperty(string name, string value)
        {
            lock (_properties)
            {
                _properties[name] = value;
            }
        }

        /// <summary>
        /// Get the global meta object for the project, this is ephemeral, it will be destroyed 
        /// when a project is loaded. It should be used to share data between services and the like
        /// </summary>
        public MetaDictionary GlobalMeta
        {
            get { return _globalMeta; }
        }

        /// <summary>
        /// Event used to indicate a document was added
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler<DocumentEventArgs> DocumentAdded;

        /// <summary>
        /// Event used to indicate a document was deleted
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler<DocumentEventArgs> DocumentDeleted;

        /// <summary>
        /// Event used to indicate a document was renamed
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler<DocumentEventArgs> DocumentRenamed;

        /// <summary>
        /// Event sent when a project is loaded
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler ProjectLoaded;

        private static byte[] CANAPE_MAGIC = { 0x43, 0x41, 0x50, 0x45 };

        private static Version ReadFileHeader(Stream stm)
        {
            Version ret = null;

            DataReader reader = new DataReader(stm);
            long streamStart = stm.Position;

            byte[] ver = reader.ReadBytes(CANAPE_MAGIC.Length, true);

            bool valid = true;

            for (int i = 0; i < ver.Length; ++i)
            {
                if (CANAPE_MAGIC[i] != ver[i])
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                ret = new Version(reader.ReadInt32(true), reader.ReadInt32(true));
            }
            else
            {
                // Reset stream
                stm.Position = streamStart;
                ret = new Version(0, 0);
            }
            
            return ret;
        }

        private static void WriteFileVersion(Version ver, Stream stm)
        {
            DataWriter writer = new DataWriter(stm);

            writer.Write(CANAPE_MAGIC);
            writer.Write(ver.Major, true);
            writer.Write(ver.Minor, true);
        }

        /// <summary>
        /// Backup the project, only works if loaded from a file
        /// </summary>
        public static void Backup()
        {
            CANAPEProject currProject = CurrentProject;

            if ((currProject != null) && (currProject._fileName != null))
            {
                string tempName = Path.ChangeExtension(currProject._fileName, ".autobak.canape");

                try
                {
                    using (Stream stm = File.Open(tempName, FileMode.Create, FileAccess.ReadWrite))
                    {
                        WriteFileVersion(GeneralUtils.GetCanapeVersion(), stm);
                        BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File));

                        formatter.Serialize(stm, currentProject);
                    }
                }
                catch (Exception)
                {
                    // We didn't save, delete file
                    try
                    {
                        File.Delete(tempName);
                    }
                    catch (IOException)
                    {
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Save the project to a file
        /// </summary>
        /// <param name="fileName">The filename to save to</param>
        /// <param name="compressed">True to compress the output</param>
        /// <param name="makeBackup">Indicates whether to make a backup or not</param>        
        public static void Save(string fileName, bool compressed, bool makeBackup)
        {
            if (currentProject != null)
            {
                string tempName = Path.GetTempFileName();

                using (Stream stm = File.Open(tempName, FileMode.Create, FileAccess.ReadWrite))
                {                    
                    WriteFileVersion(GeneralUtils.GetCanapeVersion(), stm);
                    
                    using(Stream outStream = compressed ? new GZipStream(stm, CompressionMode.Compress, false) : stm)
                    {
                        BinaryFormatter formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File));

                        formatter.Serialize(outStream, currentProject);
                    }
                }

                if ((makeBackup) && (File.Exists(fileName)))
                {
                    string backupName = fileName + ".bak";
                    try
                    {
                        if (File.Exists(backupName))
                        {
                            File.Delete(backupName);
                        }

                        File.Move(fileName, backupName);
                    }
                    catch (IOException)
                    {
                    }
                }

                // Finally copy the temporary file over the original
                File.Copy(tempName, fileName, true);
                File.Delete(tempName);
                currentProject._isNew = false;
                currentProject._fileName = fileName;
                currentProject._documentsModified = false;

                foreach (var pair in currentProject._documents)
                {
                    pair.Value.Dirty = false;
                }
            }
        }

        sealed class FixSerializationFrom12 : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                // Fix some types in Documents assembly
                if (assemblyName == "CANAPE.Documents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0d5b25a3516745f0")
                {
                    switch(typeName)
                    {
                        case "CANAPE.Documents.Net.Factories.INetworkLayerFactory":
                            return typeof(INetworkLayerFactory);
                        case "CANAPE.Documents.NetClientDocument":
                            return typeof(NetClientDocument);
                        case "CANAPE.Documents.NetGraphDocument":
                            return typeof(NetGraphDocument);
                        case "CANAPE.Documents.NetServiceDocument":
                            return typeof(NetServiceDocument);
                        case "CANAPE.Documents.PacketLogDocument":
                            return typeof(PacketLogDocument);
                        case "CANAPE.Documents.ScriptDocument":
                            return typeof(ScriptDocument);
                    }
                }

                Assembly asm = Assembly.Load(assemblyName);

                return asm.GetType(typeName);
            }
        }

        sealed class FixSerializationFrom13 : SerializationBinder
        {
            const string _oldToken = "PublicKeyToken=0d5b25a3516745f0";
            private string _pubkeyToken;

            internal FixSerializationFrom13()
            {
                byte[] token = typeof(FixSerializationFrom13).Assembly.GetName().GetPublicKeyToken();

                StringBuilder builder = new StringBuilder("PublicKeyToken=");
                foreach (byte b in token)
                {
                    builder.AppendFormat("{0:x02}", b);
                }

                _pubkeyToken = builder.ToString();
            }

            public override Type BindToType(string assemblyName, string typeName)
            {
                if (assemblyName.Contains(_oldToken))
                {
                    assemblyName = assemblyName.Replace(_oldToken, _pubkeyToken);
                }

                if (typeName.Contains(_oldToken))
                {
                    typeName = typeName.Replace(_oldToken, _pubkeyToken);
                }

                Assembly asm = Assembly.Load(assemblyName);

                return asm.GetType(typeName);
            }
        }

        // A small attempt to restrict what types can be accessed
        sealed class SecurityBinder : SerializationBinder
        {
            SerializationBinder _delegateBinder;

            internal SecurityBinder(SerializationBinder delegateBinder)
            {
                _delegateBinder = delegateBinder;
            }

            private bool AllowedTypeOrAssembly(Type type)
            {
                // "Safe" types I guess, just let them through
                if(type.IsEnum || type.IsPrimitive || type == typeof(String))
                {
                    return true;
                }
                                
                string typeNamespace = type.Namespace.ToLower();           

                if (typeNamespace.StartsWith("canape"))
                {
                    return true;
                }

                switch(typeNamespace.ToLower())
                {
                    case "system":
                    case "system.collections":
                    case "system.collections.generic":
                    case "system.text":
                    case "system.security.authentication":  
                    case "system.collections.objectmodel":
                    case "system.reflection":
                        return true;
                }

                return false;
            }

            public override Type BindToType(string assemblyName, string typeName)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("{0} {1}", assemblyName, typeName));
                Type type = null;
               
                if (_delegateBinder != null)
                {
                    type = _delegateBinder.BindToType(assemblyName, typeName);
                }
                else
                {
                    type = Type.GetType(String.Format("{0},{1}", typeName, assemblyName));
                }

                if (type != null)
                {
                    if (!AllowedTypeOrAssembly(type))
                    {
                        string name = type.FullName;
                        if (type.IsGenericType)
                        {
                            name = type.GetGenericTypeDefinition().FullName;
                        }

                        throw new SecurityException(String.Format(Properties.Resources.CANAPEProject_InsecureType, name));
                    }
                }

                return type;
            }
        }

        // Create the binary formatter with compat hacks for particular versions if needed
        private static BinaryFormatter CreateFormatter(Version ver, bool secure)
        {
            BinaryFormatter ret = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File));
            SerializationBinder binder = null;

            if (ver.Major == 1)
            {
                if (ver.Minor < 3)
                {
                    binder = new FixSerializationFrom12();
                }
                else if(ver.Minor == 3)
                {
                    binder = new FixSerializationFrom13();
                }
            }

            if (secure)
            {
                binder = new SecurityBinder(binder);
                ret.FilterLevel = TypeFilterLevel.Low;
            }

            ret.Binder = binder;
            
            return ret;
        }

        /// <summary>
        /// Load a project from a file
        /// </summary>
        /// <param name="stm">The stream</param>
        /// <param name="fileName">The filename to use (can be null)</param>
        /// <param name="verifyVersion">Set true to verify the version being opened match this canape</param>
        /// <param name="secure">Attemps to make the load secure, not likely to succeed</param>        
        public static void Load(Stream stm, string fileName, bool verifyVersion, bool secure)
        {
            // If an empty stream
            if (stm.Length == 0)
            {
                New();
            }
            else
            {
                Version ver = ReadFileHeader(stm);
                bool compressed = true;

                if (verifyVersion)
                {
                    if (ver.CompareTo(GeneralUtils.GetCanapeVersion()) != 0)
                    {
                        throw new InvalidVersionException(ver);
                    }
                }

                if (stm.ReadByte() == 0x1F)
                {
                    compressed = true;
                }
                else
                {
                    compressed = false;
                }

                stm.Position = stm.Position - 1;

                using (Stream inStream = compressed ? new GZipStream(stm, CompressionMode.Decompress, false) : stm)
                {                    
                    BinaryFormatter formatter = CreateFormatter(ver, secure);
                    CANAPEProject newProject = null;

                    // Note that all this is going to do is prevent anything during 
                    // direct load of objects and scripts, it won't do anything against anything else
                    if (secure)
                    {
                        try
                        {
                            PermissionSet ps = new PermissionSet(PermissionState.None);
                            ps.PermitOnly();
                            newProject = (CANAPEProject)formatter.UnsafeDeserialize(inStream, null);
                        }
                        finally
                        {
                            CodeAccessPermission.RevertPermitOnly();
                        }
                    }
                    else
                    {
                        newProject = (CANAPEProject)formatter.Deserialize(inStream);
                    }

                    newProject._fileName = fileName;
                    newProject._globalMeta = new MetaDictionary();

                    currentProject = newProject;

                    if (ProjectLoaded != null)
                    {
                        ProjectLoaded.Invoke(currentProject, new EventArgs());
                    }
                }
            }
        }

        /// <summary>
        /// Load a project from a file
        /// </summary>
        /// <param name="fileName">The filename to use</param>
        /// <param name="verifyVersion">Set true to verify the version being opened match this canape</param>
        /// <param name="secure">Attempts to open the file securely</param>        
        public static void Load(string fileName, bool verifyVersion, bool secure)
        {             
            using (Stream stm = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                Load(stm, fileName, verifyVersion, secure);
            }
        }
    }
}
