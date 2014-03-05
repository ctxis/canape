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

namespace CANAPE.NodeLibrary
{
    /// <summary>
    /// Class to manage list of node library elements
    /// </summary>
    public static class NodeLibraryManager
    {
        public class NodeLibraryType
        {
            public Type Type { get; private set; }
            public string Name { get; private set; }
            public Type ConfigType { get; private set; }
            public string NodeName { get; private set; }
            public string Description { get; private set; }
            public NodeLibraryClassCategory Category { get; private set; }

            public NodeLibraryType(Type type, string name, Type configType, string nodeName, string description, NodeLibraryClassCategory category)
            {
                Type = type;
                Name = name;
                ConfigType = configType;
                NodeName = nodeName;
                Description = description;
                Category = category;
            }
        }

        private static List<NodeLibraryType> _types = new List<NodeLibraryType>();

        public static IEnumerable<NodeLibraryType> NodeTypes 
        {
            get
            {
                return _types.AsEnumerable();
            }
        }

        internal static void RegisterLibraryType(NodeLibraryClassAttribute attr, Type t)
        {
            _types.Add(new NodeLibraryType(t, attr.Name, attr.ConfigType, attr.NodeName, attr.Description, attr.Category));
        }

        internal static void UnregisterLibraryType(NodeLibraryClassAttribute attr, Type t)
        {
            int idx = 0;

            for (idx = 0; idx < _types.Count; ++idx)
            {
                if (_types[idx].Type == t)
                {
                    break;
                }
            }

            if (idx < _types.Count)
            {
                _types.RemoveAt(idx);
            }
        }
    }
}
