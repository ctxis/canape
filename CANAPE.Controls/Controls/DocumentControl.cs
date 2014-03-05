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

using CANAPE.Documents;
using CANAPE.Utils;
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace CANAPE.Controls
{
    public static class DocumentControl
    {
        public static IComponent ShowControl(string name, Control control, Control parentControl)
        {
            IDocumentControl c = CANAPEServiceProvider.GlobalInstance.GetService(typeof(IDocumentControl)) as IDocumentControl;

            return c.HostControl(name, control, parentControl);
        }

        public static IComponent ShowControl(string name, Control control)
        {
            return ShowControl(name, control, null);
        }

        public static void Show(IDocumentObject document)
        {
            IDocumentControl control = CANAPEServiceProvider.GlobalInstance.GetService(typeof(IDocumentControl)) as IDocumentControl;

            if (control != null)
            { 
                control.ShowDocumentForm(document);            
            }  
        }

        public static void Rename(IDocumentObject document)
        {
            IDocumentControl control = CANAPEServiceProvider.GlobalInstance.GetService(typeof(IDocumentControl)) as IDocumentControl;

            if (control != null)
            {             
                control.RenameDocumentForm(document);
            }
        }

        public static void Close(IDocumentObject document)
        {
            IDocumentControl control = CANAPEServiceProvider.GlobalInstance.GetService(typeof(IDocumentControl)) as IDocumentControl;

            if (control != null)
            {             
                control.CloseDocumentForm(document);
            } 
        }

        public static void SetIcon(IDocumentObject document, string iconName, Icon icon)
        {
            IDocumentControl control = CANAPEServiceProvider.GlobalInstance.GetService(typeof(IDocumentControl)) as IDocumentControl;

            if (control != null)
            {
                control.SetDocumentIcon(document, iconName, icon);
            } 
        }

        public static T GetConfigItem<T>(string name, bool create) where T : class, new()
        {
            IDocumentControl control = CANAPEServiceProvider.GlobalInstance.GetService(typeof(IDocumentControl)) as IDocumentControl;
            T ret = null;

            if (control != null)
            {
                object item = control.GetConfigItem(name);
                ret = item as T;
            }

            if((ret == null) && create)
            {
                return new T();
            }

            return ret;
        }

        public static void SetConfigItem(string name, object value)
        {
            IDocumentControl control = CANAPEServiceProvider.GlobalInstance.GetService(typeof(IDocumentControl)) as IDocumentControl;            

            if (control != null)
            {
                control.SetConfigItem(name, value);                
            }
        }
    }
}
