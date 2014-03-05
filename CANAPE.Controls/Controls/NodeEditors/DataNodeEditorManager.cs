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
using CANAPE.DataFrames;

namespace CANAPE.Controls.NodeEditors
{
    /// <summary>
    /// Static class to manage data node editor controls
    /// </summary>
    public static class DataNodeEditorManager
    {
        private static Dictionary<Guid, Type> _editors;

        static DataNodeEditorManager()
        {
            _editors = new Dictionary<Guid, Type>();
            RegisterEditor(typeof(BinaryDataNodeEditorControl));
            RegisterEditor(typeof(FlagsEnumDataNodeEditorControl));
            RegisterEditor(typeof(EnumDataNodeEditorControl));
        }

        /// <summary>
        /// Register a new editor with a specific guid
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="type"></param>
        public static void RegisterEditor(Guid guid, Type type)
        {
            _editors[guid] = type;
        }

        /// <summary>
        /// Unregister a new editor with a specific guid
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="type"></param>
        public static void UnregisterEditor(Guid guid, Type type)
        {
            _editors.Remove(guid);
        }

        /// <summary>
        /// Register an editor when the type is marked with DataNodeEditorAttributes
        /// </summary>
        /// <param name="type"></param>
        internal static void RegisterEditor(Type type)
        {
            object[] attrs = type.GetCustomAttributes(typeof(DataNodeEditorAttribute), true);

            foreach (DataNodeEditorAttribute attr in attrs)
            {
                RegisterEditor(attr, type);
            }
        }

        internal static void RegisterEditor(DataNodeEditorAttribute attr, Type type)
        {
            RegisterEditor(attr.NodeClass, type);
        }

        /// <summary>
        /// Get an editor instance by guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static IDataNodeEditor GetEditor(Guid guid)
        {
            IDataNodeEditor ret = null;

            if (_editors.ContainsKey(guid))
            {
                ret = (IDataNodeEditor)Activator.CreateInstance(_editors[guid]);
            }

            return ret;
        }

        /// <summary>
        /// Get an editor instance.
        /// </summary>
        /// <param name="node">The datanode to edit</param>
        /// <returns>The editor instance, or null on error</returns>
        public static IDataNodeEditor GetEditor(DataNode node)
        {
            IDataNodeEditor ret = GetEditor(node.GetDisplayClass());

            return ret;
        }

        /// <summary>
        /// Is there a registered editor for this class?
        /// </summary>
        /// <param name="guid">The class guid</param>
        /// <returns>True if there is an editor</returns>
        public static bool HasEditor(Guid guid)
        {
            return _editors.ContainsKey(guid);
        }

        /// <summary>
        /// Is there a registered editor for this node
        /// </summary>
        /// <param name="node">The node</param>
        /// <returns>True if there is an editor</returns>
        public static bool HasEditor(DataNode node)
        {
            return HasEditor(node.Class);
        }
    }
}
