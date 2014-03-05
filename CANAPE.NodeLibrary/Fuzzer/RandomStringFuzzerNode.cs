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
using System.ComponentModel;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    [Serializable]
    public class RandomStringFuzzerNodeConfig : BaseStringFuzzerNodeConfig
    {
        [LocalizedDescription("RandomStringFuzzerNodeConfig_MinLengthDescription", typeof(Properties.Resources))]
        public int MinLength { get; set; }

        [LocalizedDescription("RandomStringFuzzerNodeConfig_MaxLengthDescription", typeof(Properties.Resources))]
        public int MaxLength { get; set; }

        [LocalizedDescription("RandomStringFuzzerNodeConfig_FixedLengthDescription", typeof(Properties.Resources))]
        public bool FixedLength { get; set; }

        [LocalizedDescription("RandomStringFuzzerNodeConfig_MinRandomCharDescription", typeof(Properties.Resources))]
        public ushort MinRandomChar { get; set; }

        [LocalizedDescription("RandomStringFuzzerNodeConfig_MaxRandomCharDescription", typeof(Properties.Resources))]
        public ushort MaxRandomChar { get; set; }

        public RandomStringFuzzerNodeConfig()
        {
            MinRandomChar = 0;
            MaxRandomChar = ushort.MaxValue;
            MinLength = 0;
            MaxLength = 512;
        }
    }

    [NodeLibraryClass("RandomStringFuzzerNode", typeof(Properties.Resources),
        ConfigType = typeof(RandomStringFuzzerNodeConfig),
        Category = NodeLibraryClassCategory.Fuzzer
        )
    ]
    public class RandomStringFuzzerNode : BaseStringFuzzerNode<RandomStringFuzzerNodeConfig>
    {
        Random _random;

        private string BuildRandomString(int length)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; ++i)
            {
                builder.Append((char)_random.Next(Config.MinRandomChar, Config.MaxRandomChar));
            }

            return builder.ToString();
        }

        protected override string DoFuzz(string value)
        {
            if (Config.FixedLength)
            {
                return BuildRandomString(Config.MaxLength);
            }
            else
            {
                return BuildRandomString(_random.Next(Config.MinLength, Config.MaxLength + 1));
            }
        }

        protected override void ValidateConfig(RandomStringFuzzerNodeConfig config)
        {
            base.ValidateConfig(config);

            if ((config.MaxLength < 0) || (config.MinLength < 0) || ((config.MaxLength + 1) < config.MinLength))
            {
                throw new ArgumentException("Invalid minimum or maximum fuzz lengths");
            }
        }

        public RandomStringFuzzerNode()
        {
            _random = new Random();
        }
    }
}
