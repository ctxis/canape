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
using System.ComponentModel;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    [Serializable]
    public class TextListFuzzerNodeConfig : BaseStringFuzzerNodeConfig
    {
        /// <summary>
        /// The text document
        /// </summary>
        [LocalizedDescription("TextListFuzzerNodeConfig_DocumentDescription", typeof(Properties.Resources)), 
            TypeConverter(typeof(DocumentChoiceConverter<TextDocument>))]
        public TextDocument Document { get; set; }

        [LocalizedDescription("TextListFuzzerNodeConfig_FuzzTextListDescription", typeof(Properties.Resources))]
        public string[] FuzzTextList { get; set; }

        [LocalizedDescription("TextListFuzzerNodeConfig_SelectRandomDescription", typeof(Properties.Resources))]
        public bool SelectRandom { get; set; }

        [LocalizedDescription("TextListFuzzerNodeConfig_StoreLocalPositionDescription", typeof(Properties.Resources))]
        public bool StoreLocalPosition { get; set; }

        [LocalizedDescription("TextListFuzzerNodeConfig_CommentStringDescription", typeof(Properties.Resources))]
        public string CommentString { get; set; }

        [LocalizedDescription("TextListFuzzerNodeConfig_IgnoreEmptyLines", typeof(Properties.Resources))]
        public bool IgnoreEmptyLines { get; set; }

        public TextListFuzzerNodeConfig()
        {
            CommentString = "#";
            IgnoreEmptyLines = true;
        }
    }

    [NodeLibraryClass("TextListFuzzerNode", typeof(Properties.Resources),
        ConfigType = typeof(TextListFuzzerNodeConfig),
        Category = NodeLibraryClassCategory.Fuzzer
        )
    ]
    public class TextListFuzzerNode : BaseStringFuzzerNode<TextListFuzzerNodeConfig>
    {
        Random _rand;
        List<string> _lines;

        public TextListFuzzerNode()
        {
            _rand = new Random();
        }

        private bool CheckString(string s)
        {
            bool ret = true;

            if (s != null)
            {
                LogInfo(s);
                if (Config.IgnoreEmptyLines)
                {
                    // Go around again
                    if (String.IsNullOrWhiteSpace(s))
                    {
                        ret = false;
                    }
                }

                // Ignore comments
                if (!String.IsNullOrWhiteSpace(Config.CommentString))
                {
                    if (s.StartsWith(Config.CommentString))
                    {
                        ret = false;
                    }
                }
            }

            return ret;
        }

        private string SelectRandomString()
        {
            string ret = null;

            // This is to prevent an infinite loop
            int maxRandom = 50;

            do
            {
                int idx = _rand.Next(_lines.Count);

                maxRandom--;
                ret = _lines[idx];                
            }
            while (!CheckString(ret) && maxRandom > 0);

            if (maxRandom == 0)
            {
                LogError(CANAPE.NodeLibrary.Properties.Resources.TextListFuzzerNode_MaxRandomCheckExceeded);                    
            }

            return ret;
        }

        private string SelectString()
        {
            MetaDictionary dict = Config.StoreLocalPosition ? Graph.Meta : Graph.GlobalMeta;
            string ret = null;

            do
            {
                int idx = dict.IncrementCounter(String.Format("{0}_counter", Uuid), 1);

                if (idx >= _lines.Count)
                {
                    LogInfo(CANAPE.NodeLibrary.Properties.Resources.TextListFuzzerNode_NoMoreLines);
                    ret = null;
                }
                else
                {
                    ret = _lines[idx];
                }
            }
            while (!CheckString(ret));

            return ret;
        }

        protected override string DoFuzz(string value)
        {
            string ret = Config.SelectRandom ? SelectRandomString() : SelectString();

            return ret ?? value;
        }
        
        protected override void ValidateConfig(TextListFuzzerNodeConfig config)
        {
            base.ValidateConfig(config);

            _lines = new List<string>();

            if (config.FuzzTextList != null)
            {
                foreach (string s in config.FuzzTextList)
                {
                    _lines.Add(s.Trim());
                }
            }

            if (config.Document != null)
            {
                _lines.AddRange(config.Document.GetLines());
            }
        }
    }
}
