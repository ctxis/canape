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

using System.Collections.Generic;
using CANAPE.Extension;
using CANAPE.Scripting;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    public class SelectScriptTemplateForm : SelectTemplateForm
    {
        private static Dictionary<string, List<CANAPETemplate>> CreateTemplates()
        {
            Dictionary<string, List<CANAPETemplate>> ret = new Dictionary<string,List<CANAPETemplate>>();
            foreach (string engine in ScriptEngineFactory.Engines)
            {                
                string name = ScriptEngineFactory.GetDescriptionForEngine(engine);
                if (name != null)
                {
                    List<CANAPETemplate> templates = new List<CANAPETemplate>();

                    templates.Add(new CANAPETemplate(CANAPE.Properties.Resources.SelectScriptTemplateForm_EmptyScript, 
                        CANAPE.Properties.Resources.SelectScriptTemplateForm_EmptyScriptDescription, engine, new byte[0]));

                    foreach (CANAPETemplate template in ScriptUtils.GetTemplates(engine))
                    {
                        templates.Add(template);
                    }

                    ret.Add(name, templates);
                }
            }

            return ret;
        }

        public SelectScriptTemplateForm()
            : base(CreateTemplates())
        {
        }
    }
}
