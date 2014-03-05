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
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CANAPE.Utils;

namespace CANAPE.NodeConfigs
{
    public class ColorValueEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((value is ColorValue) && (provider != null))
            {
                IWindowsFormsEditorService service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

                if (service != null)
                {
                    using (ColorDialog frm = new ColorDialog())
                    {
                        frm.Color = ColorValueConverter.ToColor((ColorValue)value);                        

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            value = ColorValueConverter.FromColor(frm.Color);
                        }
                    }
                }
            }

            return value;
        }

        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value is ColorValue)
            {
                using (Brush brush = new SolidBrush(ColorValueConverter.ToColor((ColorValue)e.Value)))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }
        }
    }
}
