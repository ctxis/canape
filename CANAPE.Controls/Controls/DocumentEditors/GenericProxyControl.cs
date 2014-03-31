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
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.Factories;
using CANAPE.Forms;
using CANAPE.Net.Layers;

namespace CANAPE.Controls
{
    public partial class GenericProxyControl : UserControl, IReadOnlyControl
    {                
        GenericProxyDocument _document;
        bool _readOnly;

        public GenericProxyControl(GenericProxyDocument document)
        {           
            _document = document;
            
            InitializeComponent();            
            proxyClientControl.Client = _document.Client;

            numTcpPort.Value = (decimal)_document.LocalPort;
            checkBoxGlobal.Checked = _document.AnyBind;
            checkBoxIpv6.Checked = _document.Ipv6Bind;

            if (!HasAdvancedOptions())
            {
                btnAdvancedOptions.Visible = false;
                btnAdvancedOptions.Enabled = false;
            }

            if (_document.Filters != null)
            {
                foreach (ProxyFilterFactory f in _document.Filters)
                {
                    AddFilterToList(f);
                }
            }
        }

        private bool HasAdvancedOptions()
        {
            if (_document is FullHttpProxyDocument)
            {
                return true;
            }

            return false;
        }

        private void ShowAdvancedOptions()
        {
            Form frm = null;

            if (_document is FullHttpProxyDocument)
            {
                frm = new HttpProxyOptionsForm(_document as FullHttpProxyDocument);                
            }

            if (frm != null)
            {
                using (frm)
                {
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Do nothing
                    }
                }
            }
        }

        private void EditSelectedFilter_EventHandler(object sender, EventArgs e)
        {
            if (listViewFilters.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewFilters.SelectedItems[0];
                using (EditFilterForm frm = new EditFilterForm())
                {
                    frm.Filter = (IpProxyFilterFactory)item.Tag;

                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!_readOnly)
                        {
                            IpProxyFilterFactory filter = frm.Filter;

                            item.Text = filter.ToString();
                            item.Tag = filter;

                            ResizeColumns();
                        }
                    }
                }

                UpdateFilters();
            }
        }

        private void UpdateFilters()
        {
            List<ProxyFilterFactory> filters = new List<ProxyFilterFactory>();
            foreach (ListViewItem item in listViewFilters.Items)
            {
                filters.Add((ProxyFilterFactory)item.Tag);
            }

            _document.Filters = filters.ToArray();
        }

        private void AddFilterToList(ProxyFilterFactory filter)
        {
            ListViewItem item = listViewFilters.Items.Add(filter.ToString());         
            item.Tag = filter;
        }

        private void AddFilter_EventHandler(object sender, EventArgs e)
        {
            using (EditFilterForm frm = new EditFilterForm())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    AddFilterToList(frm.Filter);
                    ResizeColumns();
                    UpdateFilters();
                }
            }
        }

        private void ResizeColumns()
        {
            listViewFilters.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewFilters.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void DeleteFilter_EventHandler(object sender, EventArgs e)
        {
            if (listViewFilters.SelectedItems.Count > 0)
            {
                listViewFilters.Items.Remove(listViewFilters.SelectedItems[0]);
                UpdateFilters();
            }
        }

        private void numTcpPort_ValueChanged(object sender, EventArgs e)
        {
            _document.LocalPort = (int)numTcpPort.Value;
        }

        private void checkBoxGlobal_CheckedChanged(object sender, EventArgs e)
        {
            _document.AnyBind = checkBoxGlobal.Checked;
        }

        private void proxyClientControl_ClientChanged(object sender, EventArgs e)
        {
            _document.Client = proxyClientControl.Client;
        }

        private void checkBoxIpv6_CheckedChanged(object sender, EventArgs e)
        {
            _document.Ipv6Bind = checkBoxIpv6.Checked;
        }

        private void MoveFilterUp_EventHandler(object sender, EventArgs e)
        {
            if ((listViewFilters.SelectedIndices.Count > 0) && (listViewFilters.Items.Count > 1))
            {
                int idx = listViewFilters.SelectedIndices[0];
                if (idx > 0)
                {                    
                    ListViewItem i = listViewFilters.Items[idx];

                    listViewFilters.Items.RemoveAt(idx);
                    listViewFilters.Items.Insert(idx - 1, i);

                    UpdateFilters();
                }
            }

            listViewFilters.Focus();
        }

        private void MoveFilterDown_EventHandler(object sender, EventArgs e)
        {
            if ((listViewFilters.SelectedIndices.Count > 0) && (listViewFilters.Items.Count > 1))
            {
                int idx = listViewFilters.SelectedIndices[0];
                if (idx < (listViewFilters.Items.Count - 1))
                {
                    ListViewItem i = listViewFilters.Items[idx];

                    listViewFilters.Items.RemoveAt(idx);
                    listViewFilters.Items.Insert(idx + 1, i);

                    UpdateFilters();
                }
            }

            listViewFilters.Focus();
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool enableSelected = listViewFilters.SelectedIndices.Count > 0;

            editFilterToolStripMenuItem.Enabled = enableSelected;

            if (!_readOnly)
            {
                moveFilterDownToolStripMenuItem.Enabled = enableSelected;
                moveFilterUpToolStripMenuItem.Enabled = enableSelected;
                deleteFilterToolStripMenuItem.Enabled = enableSelected;
                toggleEnabledToolStripMenuItem.Enabled = enableSelected;
            }
        }

        private void btnAdvancedOptions_Click(object sender, EventArgs e)
        {
            ShowAdvancedOptions();
        }

        private void UpdateReadOnly()
        {
            bool bEnable = !_readOnly;

            numTcpPort.Enabled = bEnable;
            checkBoxGlobal.Enabled = bEnable;
            checkBoxIpv6.Enabled = bEnable;
            labelLocalPort.Enabled = bEnable;
            btnAdvancedOptions.Enabled = bEnable;
            proxyClientControl.Enabled = bEnable;
            buttonFilterDown.Enabled = bEnable;
            buttonFilterUp.Enabled = bEnable;
            btnAdd.Enabled = bEnable;
            btnDelete.Enabled = bEnable;
            moveFilterUpToolStripMenuItem.Enabled = bEnable;
            deleteFilterToolStripMenuItem.Enabled = bEnable;
            moveFilterDownToolStripMenuItem.Enabled = bEnable;
            addFilterToolStripMenuItem.Enabled = bEnable;
            toggleEnabledToolStripMenuItem.Enabled = bEnable;
            templatesToolStripMenuItem.Enabled = bEnable;
        }

        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    UpdateReadOnly();
                }
            }
        }

        private void toggleEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFilters.SelectedItems.Count > 0)
            {
                if (!_readOnly)
                {
                    IpProxyFilterFactory factory = (IpProxyFilterFactory)listViewFilters.SelectedItems[0].Tag;

                    factory.Disabled = !factory.Disabled;

                    listViewFilters.SelectedItems[0].Text = factory.ToString();

                    UpdateFilters();
                    ResizeColumns();
                }
            }
        }

        private void AddTemplateFactory(ProxyFilterFactory factory)
        {    
            AddFilterToList(factory);
            ResizeColumns();
            UpdateFilters();
        
        }

        private void AddNewFilter(string host, int port, params INetworkLayerFactory[] layers)
        {
            IpProxyFilterFactory factory = new IpProxyFilterFactory();
            factory.Address = host;
            factory.Port = port;
            factory.Layers = layers;

            AddTemplateFactory(factory);
        }

        private void sSLPort443ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewFilter("*", 443, new SslNetworkLayerFactory());
        }

        private void hTTPPort80ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewFilter("*", 80, new HttpNetworkLayerFactory());
        }

        private void hTTPSSLPort443ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewFilter("*", 443, new SslNetworkLayerFactory(), new HttpNetworkLayerFactory());
        }

        private void sSLAllPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewFilter("*", 0, new SslNetworkLayerFactory());
        }

        private void hTTPAllPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewFilter("*", 0, new HttpNetworkLayerFactory());
        }

    }
}
