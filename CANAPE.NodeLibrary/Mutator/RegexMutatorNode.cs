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
using System.Text;
using System.Text.RegularExpressions;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Mutator
{
    [Serializable]
    public class RegexMutatorNodeConfig
    {
        [LocalizedDescription("RegexMutatorNodeConfig_RegexDescription", typeof(Properties.Resources))]
        public string Regex { get; set; }

        [LocalizedDescription("RegexMutatorNodeConfig_ReplacementDescription", typeof(Properties.Resources))]
        public string Replacement { get; set; }

        [LocalizedDescription("RegexMutatorNodeConfig_CaseSensitiveDescription", typeof(Properties.Resources))]
        public bool CaseSensitive { get; set; }

        [LocalizedDescription("RegexMutatorNodeConfig_MultiLineDescription", typeof(Properties.Resources))]
        public bool MultiLine { get; set; }

        [LocalizedDescription("RegexMutatorNodeConfig_StringEncodingDescription", typeof(Properties.Resources))]
        public BinaryStringEncoding StringEncoding { get; set; }

        public RegexMutatorNodeConfig()
        {
            Regex = String.Empty;
            Replacement = String.Empty;
        }
    }

    [NodeLibraryClass("RegexMutatorNode", typeof(Properties.Resources),
        ConfigType = typeof(RegexMutatorNodeConfig),        
        Category = NodeLibraryClassCategory.Mutator)]
    public class RegexMutatorNode : BasePipelineNodeWithPersist<RegexMutatorNodeConfig>
    {
        Regex _re;
        Encoding _encoding;

        protected override void OnInput(DataFrames.DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);

            foreach (DataNode node in nodes)
            {
                DataValue value = node as DataValue;

                if ((value != null) && (value.Value is string))
                {
                    value.Value = _re.Replace(value.Value, Config.Replacement);
                }
                else
                {
                    string s = _re.Replace(_encoding.GetString(node.ToArray()), Config.Replacement);

                    node.ReplaceNode(_encoding.GetBytes(s));
                }
            }

            WriteOutput(frame);
        }

        protected override void ValidateConfig(RegexMutatorNodeConfig config)
        {
            RegexOptions opts = RegexOptions.None;

            if(!config.CaseSensitive)
            {
                opts |= RegexOptions.IgnoreCase;
            }

            if(config.MultiLine)
            {
                opts |= RegexOptions.Multiline;
            }

            _re = new Regex(config.Regex, opts);

            _encoding = GeneralUtils.GetEncodingFromType(Config.StringEncoding);
        }
    }
}
