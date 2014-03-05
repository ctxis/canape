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
using System.Collections.Specialized;
using CANAPE.Documents.Data;
using CANAPE.Net;
using CANAPE.Parser;
using CANAPE.Scripting;
using CANAPE.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Net.Protocols.Parser;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// Document to contain a script
    /// </summary>
    [Serializable, TestDocumentType(typeof(ScriptTestDocument))]
    public class ScriptDocument : BaseDocumentObject
    {
        /// <summary>
        /// Script container object
        /// </summary>
        protected ScriptContainer _script;

        /// <summary>
        /// Script configuration properties
        /// </summary>
        private Dictionary<string, DynamicNodeConfigProperty> _config;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScriptDocument(string engine)            
        {
            _script = new ScriptContainer(engine, Uuid, "", false);
            _config = new Dictionary<string, DynamicNodeConfigProperty>();
            
            // Add some more assemblies that we can see
            _script.ReferencedNames.Add(typeof(ScriptDocument).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(ProxyNetworkService).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(CertificateUtils).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(ExpressionResolver).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(HttpParser).Assembly.GetName());            
        }

        /// <summary>
        /// Called on deserialization
        /// </summary>
        protected override void OnDeserialization()
        {
            base.OnDeserialization();

            if (_config == null)
            {
                _config = new Dictionary<string, DynamicNodeConfigProperty>();
            }
        }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected ScriptDocument()
        {            
            _config = new Dictionary<string, DynamicNodeConfigProperty>();
        }

        void NetServiceDocument_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        /// Method called when document is copied
        /// </summary>
        protected override void OnCopy()
        {
            base.OnCopy();

            _script.Uuid = Uuid;
        }

        /// <summary>
        /// Get or set the script data
        /// </summary>
        public string Script 
        {
            get
            {
                return _script.Script;
            }

            set
            {
                if (_script.Script != value)
                {
                    Dirty = true;
                    _script.Script = value;
                }
            }
        }

        /// <summary>
        /// Get the configuration values
        /// </summary>
        public IEnumerable<DynamicNodeConfigProperty> Configuration
        {
            get
            {
                return _config.Values;
            }
        }

        /// <summary>
        /// Add a new configuration property (or update old one)
        /// </summary>
        /// <param name="property">The property to add</param>
        public void AddConfiguration(DynamicNodeConfigProperty property)
        {
            _config[property.Name] = property;
            Dirty = true;
        }

        /// <summary>
        /// Remove a configuration property
        /// </summary>
        /// <param name="property">The property to remove</param>
        public void RemoveConfiguration(DynamicNodeConfigProperty property)
        {
            _config.Remove(property.Name);
            Dirty = true;
        }

        /// <summary>
        /// Clear the configuration
        /// </summary>
        public void ClearConfiguration()
        {
            _config.Clear();
            Dirty = true;
        }

        /// <summary>
        /// Return a referencable container which holds the script and engine
        /// </summary>
        public ScriptContainer Container
        {
            get
            {
                return _script;
            }
        }

        /// <summary>
        /// Default name of script
        /// </summary>
        public override string DefaultName
        {
            get 
            {
                return String.Format(Properties.Resources.ScriptDocument_DefaultName, 
                    ScriptEngineFactory.GetDescriptionForEngine(_script.Engine));
            }
        }

        private static ScriptDocument FindScriptDocument(string name)
        {
            ScriptDocument doc = CANAPEProject.CurrentProject.GetDocumentByName(name) as ScriptDocument;

            if (doc == null)
            {
                Guid g;

                if (Guid.TryParse(name, out g))
                {
                    doc = CANAPEProject.CurrentProject.GetDocumentByUuid(g) as ScriptDocument;                    
                }

                if (doc == null)
                {
                    throw new ArgumentException(String.Format(CANAPE.Documents.Properties.Resources.ScriptDocument_CannotFindDocument, name));
                }
            }

            return doc;
        }

        /// <summary>
        /// Get an instance of a class from the script
        /// </summary>
        /// <param name="document">The name of the document, can also be a string version of the UUID</param>
        /// <param name="classname">The classname to get</param>
        /// <exception cref="ArgumentException">Thrown if cannot find the document</exception>
        /// <returns>An instance of the object</returns>
        public static object GetInstance(string document, string classname)
        {
            return FindScriptDocument(document).Container.GetInstance(classname);
        }

        /// <summary>
        /// Get an instance of a class from the script
        /// </summary>
        /// <param name="document">The name of the document, can also be a string version of the UUID</param>
        /// <param name="classname">The classname to get</param>
        /// <param name="args">The arguments for the constructor</param>
        /// <returns>An instance of the object</returns>
        public static object GetInstance(string document, string classname, params object[] args)
        {    
            return FindScriptDocument(document).Container.GetInstance(classname, args);            
        }

        /// <summary>
        /// Invoke a static method on the script
        /// </summary>
        /// <param name="document">The name of the document, can also be a string version of the UUID</param>
        /// <param name="classname">The classname to use</param>
        /// <param name="methodname">The method name</param>
        /// <param name="args">The arguments</param>
        /// <returns>The return value of the method</returns>
        public static object Invoke(string document, string classname, string methodname, params object[] args)
        {
            return FindScriptDocument(document).Container.Invoke(classname, methodname, args);            
        }
    }
}
