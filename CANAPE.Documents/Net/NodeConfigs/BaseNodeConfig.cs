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
using System.Linq;
using CANAPE.NodeFactories;
using CANAPE.Utils;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class BaseNodeConfig : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected string _label;
        /// <summary>
        /// 
        /// </summary>
        protected bool _enabled;

        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, string> _properties;

        /// <summary>
        /// 
        /// </summary>
        protected DataFrameFilterFactory[] _filters;

        /// <summary>
        /// 
        /// </summary>
        protected Guid _id;

        /// <summary>
        /// 
        /// </summary>
        protected bool _matchAllFilters;

        /// <summary>
        /// 
        /// </summary>
        protected string _selectionPath;

        /// <summary>
        /// 
        /// </summary>
        protected string _comment;

        /// <summary>
        /// Whether the node should be hidden
        /// </summary>
        protected bool _hidden;

        /// <summary>
        /// Whether to log all input traffic
        /// </summary>
        protected bool _logInput;

        /// <summary>
        /// Whether to log all output traffic
        /// </summary>
        protected bool _logOutput;

        /// <summary>
        /// The currently assigned netgraph, may be null
        /// </summary>
        [NonSerialized]
        protected NetGraphDocument _document;

        /// <summary>
        /// Event to trigger when the label changes
        /// </summary>        
        [field: NonSerialized]
        public event EventHandler LabelChanged;

        /// <summary>
        /// Event to trigger when the enable state changes
        /// </summary>        
        [field: NonSerialized]
        public event EventHandler EnabledChanged;

        /// <summary>
        /// Event to trigger when the properties change
        /// </summary>
        [field: NonSerialized]
        public event EventHandler PropertiesChanged;

        /// <summary>
        /// Event to trigger when any other property on the node is changed
        /// </summary>
        [field: NonSerialized]
        public event EventHandler DirtyChanged;

        /// <summary>
        /// Unique ID of the node in this graph
        /// </summary>
        [Browsable(false)]
        public Guid Id { get { return _id; } set { _id = value; } }

        /// <summary>
        /// Specifies the document this graph is currently assigned to
        /// </summary>        
        internal NetGraphDocument Document 
        {
            get
            {
                return _document;
            }

            set
            {
                _document = value;
            }
        }

        /// <summary>
        /// Sets the node as dirty
        /// </summary>
        protected void SetDirty()
        {
            if (DirtyChanged != null)
            {
                DirtyChanged.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Method called when label changes
        /// </summary>
        protected void OnLabelChanged()
        {
            if (LabelChanged != null)
            {
                LabelChanged.Invoke(this, new EventArgs());
            }

            SetDirty();
        }

        /// <summary>
        /// Method called when enabled changes
        /// </summary>
        protected void OnEnabledChanged()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged.Invoke(this, new EventArgs());
            }

            SetDirty();
        }

        /// <summary>
        /// Method called when properties changes
        /// </summary>
        protected void OnPropertiesChanged()
        {
            if (PropertiesChanged != null)
            {
                PropertiesChanged.Invoke(this, new EventArgs());
            }

            SetDirty();
        }

        /// <summary>
        /// Internal constructor
        /// </summary>
        protected BaseNodeConfig()
        {
            _enabled = true;
            _id = Guid.NewGuid();
            _properties = new Dictionary<string, string>();
            _filters = new DataFrameFilterFactory[0];
        }

        /// <summary>
        /// Get the label for the node
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_LabelDescription", typeof(Properties.Resources)), Category("Appearance")]
        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                if(_label != value)
                {
                    _label = value;
                    OnLabelChanged();
                }
            }
        }

        /// <summary>
        /// Get the enabled state of the node
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_EnabledDescription", typeof(Properties.Resources)), Category("Control")]
        public bool Enabled {
            get
            {
                return _enabled;
            }

            set
            {
                if (_enabled != value)
                {                    
                    _enabled = value;
                    OnEnabledChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the dictionary of properties
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_PropertiesDescription", typeof(Properties.Resources)), Category("Control")]
        public Dictionary<string, string> Properties
        {
            get { return _properties; }
            set 
            {
                if (_properties != value)
                {
                    if (value != null)
                    {
                        _properties = value;
                    }
                    else
                    {
                        _properties = new Dictionary<string, string>();
                    }

                    OnPropertiesChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the list of filters
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_FiltersDescription", typeof(Properties.Resources)), Category("Filters")]
        public DataFrameFilterFactory[] Filters
        {
            get { return _filters; }
            set
            {
                if (_filters != value)
                {
                    if (value == null)
                    {
                        _filters = new DataFrameFilterFactory[0];
                    }
                    else
                    {
                        _filters = value;
                    }
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Get or set whether to match on all filters
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_MatchAllFiltersDescription", typeof(Properties.Resources)), Category("Filters")]
        public bool MatchAllFilters
        {
            get { return _matchAllFilters; }
            set
            {
                if (_matchAllFilters != value)
                {
                    _matchAllFilters = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Get or set the selection path
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_SelectionPathDescription", typeof(Properties.Resources)), Category("Control")]
        public string SelectionPath
        {
            get { return _selectionPath ?? "/"; }
            set
            {
                if (_selectionPath != value)
                {
                    _selectionPath = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Get or set a comment associated with the node
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_CommentDescription", typeof(Properties.Resources)), Category("Appearance")]
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Get or set the hidden attribute
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_HiddenDescription", typeof(Properties.Resources)), Category("Control")]
        public bool Hidden
        {
            get { return _hidden; }
            set
            {
                if (_hidden != value)
                {
                    _hidden = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Get or set log input attribute
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_LogInputDescription", typeof(Properties.Resources)), Category("Debug")]
        public bool LogInput
        {
            get { return _logInput;  }
            set
            {
                if (_logInput != value)
                {
                    _logInput = value;
                    SetDirty();
                }
            }
        }

        /// <summary>
        /// Get or set log output attribute
        /// </summary>
        [LocalizedDescription("BaseNodeConfig_LogOutputDescription", typeof(Properties.Resources)), Category("Debug")]
        public bool LogOutput
        {
            get { return _logOutput; }
            set
            {
                if (_logOutput != value)
                {
                    _logOutput = value;
                    SetDirty();
                }
            }
        }


        /// <summary>
        /// X position of the node in a graph form
        /// </summary>
        [Browsable(false)]
        public float X { get; set; }

        /// <summary>
        /// Y position of the node in a graph form
        /// </summary>
        [Browsable(false)]
        public float Y { get; set; }

        /// <summary>
        /// Z position of the node in a graph form
        /// </summary>
        [Browsable(false)]
        public float Z { get; set; }

        /// <summary>
        /// Simple convert to a string
        /// </summary>
        /// <returns>The label of the node</returns>
        public override string ToString()
        {
            return _label;
        }

        /// <summary>
        /// Get the name of the node
        /// </summary>
        /// <returns>The node name</returns>
        public abstract string GetNodeName();

        /// <summary>
        /// Method to create the factory
        /// </summary>
        /// <returns>The new factory</returns>
        protected abstract BaseNodeFactory OnCreateFactory();        

        /// <summary>
        /// Create a factory
        /// </summary>
        /// <returns>The factory</returns>
        public BaseNodeFactory CreateFactory()
        {
            BaseNodeFactory factory = OnCreateFactory();
            factory.Enabled = _enabled;

            if (_filters != null)
            {
                factory.Filters = _filters.ToArray();
            }
            else
            {
                factory.Filters = new DataFrameFilterFactory[0];
            }

            factory.MatchAllFilters = _matchAllFilters;

            foreach (var pair in _properties)
            {
                factory.Properties.Add(pair.Key, pair.Value);
            }

            factory.SelectionPath = _selectionPath ?? "/";
            factory.Hidden = _hidden;
            factory.LogInput = _logInput;
            factory.LogOutput = _logOutput;

            return factory;
        }

        /// <summary>
        /// Internal dispose patten method
        /// </summary>
        /// <param name="disposing">True if disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            // Do nothing
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~BaseNodeConfig()
        {
            Dispose(false);
        }

        /// <summary>
        /// Used to indicate the node has been deleted
        /// </summary>
        public virtual void Delete()
        {

        }
    }
}
