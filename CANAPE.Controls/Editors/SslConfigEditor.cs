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
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CANAPE.Controls;
using CANAPE.Documents.Net.Factories;
using CANAPE.Forms;
using CANAPE.Net.Layers;

namespace CANAPE.Editors
{
    public class SslConfigEditor : UITypeEditor
    {
        //public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        //{
        //    return UITypeEditorEditStyle.Modal;
        //}

        //public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        //{
        //    if ((provider != null) && (value is SslNetworkLayerConfig))
        //    {
        //        IWindowsFormsEditorService service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

        //        if (service != null)
        //        {
        //            NetworkLayerBinding binding = NetworkLayerBinding.ClientAndServer;

        //            INetworkLayerFactory layerFactory = context.Instance as INetworkLayerFactory;

        //            if (layerFactory != null)
        //            {
        //                binding = layerFactory.Binding;
        //            }

        //            using (SslConfigForm frm = new SslConfigForm((SslNetworkLayerConfig)value, binding))
        //            {                        
        //                if (service.ShowDialog(frm) == DialogResult.OK)
        //                {
        //                    value = frm.Config;
        //                }
        //            }
        //        }
        //    }

        //    return value;
        //}

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((provider != null) && (value is SslNetworkLayerConfig))
            {
                IWindowsFormsEditorService service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

                if (service != null)
                {
                    NetworkLayerBinding binding = NetworkLayerBinding.ClientAndServer;

                    INetworkLayerFactory layerFactory = context.Instance as INetworkLayerFactory;

                    if ((layerFactory != null) && (layerFactory.Binding != NetworkLayerBinding.Default))
                    {
                        binding = layerFactory.Binding;
                    }

                    using (SslConfigControl sslControl = new SslConfigControl())
                    {
                        sslControl.Config = (SslNetworkLayerConfig)value;
                        sslControl.LayerBinding = binding;

                        service.DropDownControl(sslControl);

                        value = sslControl.Config;
                    }

                    //using (SslConfigForm frm = new SslConfigForm((SslNetworkLayerConfig)value, binding))
                    //{
                    //    if (service.ShowDialog(frm) == DialogResult.OK)
                    //    {
                    //        value = frm.Config;
                    //    }
                    //}
                }
            }

            return value;
        }
    }
}
