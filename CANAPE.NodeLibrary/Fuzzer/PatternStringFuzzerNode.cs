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
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    [Serializable]
    public class PatternStringFuzzerNodeConfig : BaseStringFuzzerNodeConfig
    {
        [LocalizedDescription("PatternStringFuzzerNodeConfig_MinLengthDescription", typeof(Properties.Resources))]
        public int MinLength { get; set; }

        [LocalizedDescription("PatternStringFuzzerNodeConfig_MaxLengthDescription", typeof(Properties.Resources))]
        public int MaxLength { get; set; }

        [LocalizedDescription("PatternStringFuzzerNodeConfig_PatternDescription", typeof(Properties.Resources))]
        public string Pattern { get; set; }

        [LocalizedDescription("PatternStringFuzzerNodeConfig_FixedLengthDescription", typeof(Properties.Resources))]
        public bool FixedLength { get; set; }

        public PatternStringFuzzerNodeConfig()
        {
            Pattern = "ABC";
            MaxLength = 512;
        }
    }

    [NodeLibraryClass("PatternStringFuzzerNode", typeof(Properties.Resources),
        ConfigType = typeof(PatternStringFuzzerNodeConfig),
        Category = NodeLibraryClassCategory.Fuzzer
        )
    ]
    public class PatternStringFuzzerNode : BaseStringFuzzerNode<PatternStringFuzzerNodeConfig>
    {
        Random _random;

        protected override string DoFuzz(string value)
        {
            StringBuilder builder = new StringBuilder();
            int length = Config.FixedLength ? Config.MaxLength : _random.Next(Config.MinLength, Config.MaxLength + 1);

            while (builder.Length < length)
            {
                builder.Append(Config.Pattern);
            }

            return builder.ToString().Substring(0, length);
        }

        protected override void ValidateConfig(PatternStringFuzzerNodeConfig config)
        {
            base.ValidateConfig(config);

            if (String.IsNullOrEmpty(config.Pattern))
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.PatternStringFuzzerNode_InvalidPattern);
            }

            if ((config.MinLength < 0) || (config.MaxLength < config.MinLength))
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.PatternStringFuzzerNode_InvalidLength);
            }
        }

        public PatternStringFuzzerNode()
        {
            _random = new Random();
        }
    }
}
