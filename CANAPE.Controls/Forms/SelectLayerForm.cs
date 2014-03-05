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
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Extension;
using CANAPE.Documents.Net.Factories;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Net.Layers;

namespace CANAPE.Forms
{
    public sealed class SelectLayerForm : SelectLibraryOrScriptForm
    {
        public SelectLayerForm()
            : base(LayerClassChoiceConverter.GetTypes())
        {
        }

        protected override ListViewItem[] PopulateTemplates()
        {
            List<ListViewItem> items = new List<ListViewItem>();

            foreach (var ext in NetworkLayerFactoryManager.Instance.GetExtensions())
            {
                ListViewItem item = new ListViewItem(ext.ExtensionAttribute.Name);
                item.SubItems.Add(ext.ExtensionAttribute.Description);
                item.Tag = ext.ExtensionType;
                items.Add(item);
            }

            return items.ToArray();
        }

        public INetworkLayerFactory Factory { get; private set; }

        protected override bool SelectScript(ScriptDocument document, string className)
        {
            Factory = new ScriptNetworkLayerFactory(document, className);

            return true;
        }

        protected override bool SelectTemplate(object template)
        {
            Type t = template as Type;

            Factory = (INetworkLayerFactory)Activator.CreateInstance(t);

            return true;
        }
    }
}
