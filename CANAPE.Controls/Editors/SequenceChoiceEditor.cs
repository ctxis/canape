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
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CANAPE.Forms;
using CANAPE.Parser;

namespace CANAPE.Editors
{
    public class SequenceChoiceEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            SequenceChoice[] choices = value as SequenceChoice[];
            SequenceChoiceMemberEntry entry = (SequenceChoiceMemberEntry)ParserUtils.GetCompatibleType(typeof(SequenceChoiceMemberEntry), context.Instance);

            if ((choices != null) && (provider != null) && (entry != null))
            {
                IWindowsFormsEditorService service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

                if (service != null)
                {                    
                    Dictionary<string, Func<object>> dict = new Dictionary<string,Func<object>>();

                    dict.Add("New Choice", () => new SequenceChoice(entry));

                    using (ObjectCollectionForm frm = new ObjectCollectionForm(choices, dict))
                    {
                        if (service.ShowDialog(frm) == DialogResult.OK)
                        {
                            value = frm.Objects.Cast<SequenceChoice>().ToArray();
                        }
                    }
                }
            }

            return value;
        }
    }
}
