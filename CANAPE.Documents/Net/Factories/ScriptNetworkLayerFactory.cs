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
using System.IO;
using System.Threading;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Documents.Data;
using CANAPE.Documents.Extension;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Net.Layers;
using CANAPE.Nodes;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.Factories
{
    /// <summary>
    /// Layer factory for a script
    /// </summary>
    [Serializable, NetworkLayerFactory("ScriptNetworkLayerFactory", typeof(Properties.Resources))]
    public sealed class ScriptNetworkLayerFactory : BaseNetworkLayerFactory, IScriptProvider, ICustomTypeDescriptor
    {        
        private Dictionary<string, object> _config;

        Dictionary<string, object> IScriptProvider.Config { get { return _config; } }

        [NonSerialized]
        private ScriptDocument _script;

        private Guid _uuid;

        void IScriptProvider.SetDirty()
        {
        }

        /// <summary>
        /// The script document to use
        /// </summary>        
        [TypeConverter(typeof(DocumentChoiceConverter<ScriptDocument>)), LocalizedDescription("ScriptNetworkLayerFactory_ScriptDescription",
                typeof(Properties.Resources)), Category("Scripting")]
        public ScriptDocument Script
        {
            get
            {
                ReloadScript();

                return _script;
            }

            set
            {
                if (value == null)
                {
                    if (_script != null)
                    {
                        _script = null;
                        _uuid = Guid.Empty;                        
                    }
                }
                else
                {
                    if (value.Uuid != _uuid)
                    {
                        _script = value;
                        if (_script != null)
                        {
                            _uuid = _script.Uuid;
                        }
                        else
                        {
                            _uuid = Guid.Empty;
                        }

                        string[] classNames = _script.Container.GetClassNames(new Type[] { 
                                typeof(INetworkLayer)});

                        if (classNames.Length > 0)
                        {
                            ClassName = classNames[0];
                        }
                        else
                        {
                            ClassName = String.Empty;
                        }
                    }
                }
            }
        }

        private void ReloadScript()
        {
            if ((_script == null) && (_uuid != Guid.Empty))
            {
                _script = (ScriptDocument)CANAPEProject.CurrentProject.GetDocumentByUuid(_uuid);
                if (_script == null)
                {
                    _uuid = Guid.Empty;
                    ClassName = String.Empty;
                }
            }
        }

        /// <summary>
        /// The name of the class to instantiate
        /// </summary>
        [TypeConverter(typeof(LayerClassChoiceConverter)), LocalizedDescription("ScriptNetworkLayerFactory_ClassNameDescription", 
                typeof(Properties.Resources)), Category("Scripting")]
        public string ClassName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document">The script document</param>
        /// <param name="className">The class name</param>
        public ScriptNetworkLayerFactory(ScriptDocument document, string className)
        {
            Script = document;
            ClassName = className;
            Description = Properties.Resources.ScriptNetworkLayerFactory_Name;           
            _config = new Dictionary<string, object>();
            _desc = new ScriptDynamicTypeDescriptor(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ScriptNetworkLayerFactory() : this(null, String.Empty)
        {
        }

        /// <summary>
        /// Create the layer
        /// </summary>
        /// <param name="logger">The logger to use when creating</param>
        /// <returns>The network layer</returns>
        public override INetworkLayer CreateLayer(Logger logger)
        {
            if (Script == null)
            {
                throw new InvalidOperationException(String.Format(Properties.Resources.ScriptNetworkLayerFactory_SpecifyScript, Description));
            }

            if (ClassName == null)
            {
                throw new InvalidOperationException(String.Format(Properties.Resources.ScriptNetworkLayerFactory_SpecifyClassName, Description));
            }

            object obj = Script.Container.GetInstance(ClassName);

            INetworkLayer layer = obj as INetworkLayer;      
            if(layer == null)
            {
                IDataStreamParser parser = obj as IDataStreamParser;
                if(parser != null)
                {
                    layer = new ParserNetworkLayer(DynamicScriptContainer.Create(Script.Container, ClassName), logger);
                }
            }

            if(layer == null)
            {
                throw new InvalidOperationException(String.Format(Properties.Resources.ScriptNetworkLayerFactory_InvalidType, Script.Name));
            }            

            IPersistNode persist = layer as IPersistNode;

            if (persist != null)
            {
                persist.SetState(new DynamicConfigObject(_config), logger);
            }

            return layer;
        }

        #region  ICustomTypeDescriptor Members

        [NonSerialized]
        ICustomTypeDescriptor _desc;

        ICustomTypeDescriptor GetDescriptor()
        {
            if (_desc == null)
            {
                _desc = new ScriptDynamicTypeDescriptor(this);
            }
            return _desc;
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return GetDescriptor().GetAttributes();
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return GetDescriptor().GetClassName();
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return GetDescriptor().GetComponentName();
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return GetDescriptor().GetConverter();
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return GetDescriptor().GetDefaultEvent();
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return GetDescriptor().GetDefaultProperty();
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return GetDescriptor().GetEditor(editorBaseType);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return GetDescriptor().GetEvents(attributes);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return GetDescriptor().GetEvents();
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return GetDescriptor().GetProperties(attributes);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return GetDescriptor().GetProperties();
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return GetDescriptor().GetPropertyOwner(pd);
        }
        #endregion
    }
}
