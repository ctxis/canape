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
using CANAPE.Extension;

namespace CANAPE.Controls.NodeEditors
{
    /// <summary>
    /// Attribute for a data node editor
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DataNodeEditorAttribute : CANAPEExtensionAttribute
    {
        /// <summary>
        /// The Class GUID which this editor can handle
        /// </summary>
        public Guid NodeClass { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodeClass">String form of guid</param>
        public DataNodeEditorAttribute(string nodeClass)
        {
            NodeClass = new Guid(nodeClass);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodeClass">Class GUID</param>
        public DataNodeEditorAttribute(Guid nodeClass)
        {
            NodeClass = nodeClass;
        }

        public override void RegisterType(Type t)
        {
            DataNodeEditorManager.RegisterEditor(this, t);
        }

        public override void UnregisterType(Type t)
        {
            DataNodeEditorManager.UnregisterEditor(NodeClass, t);
        }
    }
}
