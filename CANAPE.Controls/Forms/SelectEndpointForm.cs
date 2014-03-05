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
using System.Windows.Forms;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.Factories;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.NodeLibrary;
using CANAPE.Scripting;

namespace CANAPE.Forms
{
    public class SelectEndpointForm : SelectLibraryOrScriptForm
    {
        public DataEndpointFactory Server
        {
            get;
            private set;
        }

        public SelectEndpointForm()
            : base(EndpointClassChoiceConverter.GetTypes())
        {
        }

        protected override ListViewItem[] PopulateTemplates()
        {
            List<ListViewItem> items = new List<ListViewItem>();

            foreach (NodeLibraryManager.NodeLibraryType t in NodeLibraryManager.NodeTypes)
            {
                if (typeof(IDataEndpoint).IsAssignableFrom(t.Type))
                {
                    if (!FilterServers || (t.Category != NodeLibrary.NodeLibraryClassCategory.Server))
                    {
                        ListViewItem item = new ListViewItem(t.Name);
                        item.SubItems.Add(t.Description);
                        item.Tag = t;
                        items.Add(item);
                    }
                }
            }

            return items.ToArray();
        }

        protected override bool SelectScript(ScriptDocument document, string className)
        {
            Server = new ScriptDataEndpointFactory(document, className);

            return true;
        }

        protected override bool SelectTemplate(object template)
        {
            NodeLibraryManager.NodeLibraryType node = (NodeLibraryManager.NodeLibraryType)template;

            if (node.ConfigType != null)
            {
                Server = new LibraryDataEndpointFactory(node.Type, node.ConfigType, node.Name);
            }
            else
            {
                Server = new LibraryDataEndpointFactory(node.Type, node.Name);
            }

            return true;
        }
    }
}
