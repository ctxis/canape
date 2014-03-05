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
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CANAPE.Extension
{
    /// <summary>
    /// A generic class which implements an extension manager
    /// </summary>
    /// <typeparam name="T">The attribute type</typeparam>
    /// <typeparam name="U">The base type for any extension</typeparam>
    public class GenericExtensionManager<T, U> where T : CANAPEExtensionAttribute
    {
        private static object _lock = new object();
        private static GenericExtensionManager<T, U> _singleton;
        private List<Extension> _extensions; 

        /// <summary>
        /// Extension class
        /// </summary>
        /// <typeparam name="T">The attribute type</typeparam>
        public class Extension
        {
            public T ExtensionAttribute { get; private set; }
            public Type ExtensionType { get; private set; }
            public string Category { get; private set; }
            public bool Browseable { get; private set; }
            public string Description { get; private set; }

            public Extension(T extensionAttribute, Type extensionType)
            {
                ExtensionAttribute = extensionAttribute;
                ExtensionType = extensionType;

                Category = String.Empty;

                object[] attrs = extensionType.GetCustomAttributes(false);

                foreach (object attr in attrs)
                {
                    if (attr is CategoryAttribute)
                    {
                        Category = (attr as CategoryAttribute).Category;
                    }
                    else if (attr is DescriptionAttribute)
                    {
                        Description = (attr as DescriptionAttribute).Description;
                    }
                    else if (attr is BrowsableAttribute)
                    {
                        Browseable = (attr as BrowsableAttribute).Browsable;
                    }
                }
            }

            /// <summary>
            /// Create an instance of the extension type
            /// </summary>
            /// <returns>The extension type</returns>
            public U CreateInstance()
            {
                return (U)Activator.CreateInstance(ExtensionType);
            }

            /// <summary>
            /// Create an instance of the extension type
            /// </summary>
            /// <param name="args">The extension type constructor arguments</param>
            /// <returns>The extension type</returns>
            public U CreateInstance(params Object[] args)
            {
                return (U)Activator.CreateInstance(ExtensionType, args);
            }
        }

        protected GenericExtensionManager()
        {
            _extensions = new List<Extension>();
        }

        public static GenericExtensionManager<T, U> Instance {
            get
            {
                lock (_lock)
                {
                    if (_singleton == null)
                    {
                        _singleton = new GenericExtensionManager<T, U>();
                    }

                    return _singleton;
                }
            }
        }

        public IEnumerable<Extension> GetExtensions()
        {
            return _extensions.ToArray();
        }

        public void RegisterExtension(T attr, Type t)
        {
            if (typeof(U).IsAssignableFrom(t))
            {
                _extensions.Add(new Extension(attr, t));
            }
        }

        public void UnregisterExtension(T attr, Type t)
        {
            int idx = 0;

            for (idx = 0; idx < _extensions.Count; ++idx)
            {
                if (_extensions[idx].ExtensionAttribute.Equals(attr))
                {
                    break;
                }
            }

            if (idx < _extensions.Count)
            {
                _extensions.RemoveAt(idx);
            }
        }

        /// <summary>
        /// Get count of extensions
        /// </summary>
        public int Count
        {
            get { return _extensions.Count; }
        }
    }
}
