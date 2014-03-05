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
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    [Serializable]
    public class RandomLengthStringFuzzerNodeConfig
    {
        public enum GenerationMode
        {
            RandomCharacters,
            RepeatingPattern,
        }

        [LocalizedDescription("RandomLengthStringFuzzerNodeConfig_MinLengthDescription", typeof(Properties.Resources))]
        public int MinLength { get; set; }

        [LocalizedDescription("RandomLengthStringFuzzerNodeConfig_MaxLengthDescription", typeof(Properties.Resources))]
        public int MaxLength { get; set; }

        [LocalizedDescription("RandomLengthStringFuzzerNodeConfig_ModeDescription", typeof(Properties.Resources))]
        public GenerationMode Mode { get; set; }

        [LocalizedDescription("RandomLengthStringFuzzerNodeConfig_PatternDescription", typeof(Properties.Resources))]
        public string Pattern { get; set; }

        [LocalizedDescription("RandomLengthStringFuzzerNodeConfig_MinRandomCharDescription", typeof(Properties.Resources))]
        public int MinRandomChar { get; set; }

        [LocalizedDescription("RandomLengthStringFuzzerNodeConfig_MaxRandomCharDescription", typeof(Properties.Resources))]
        public int MaxRandomChar { get; set; }

        [LocalizedDescription("RandomLengthStringFuzzerNodeConfig_AppendFuzzDescription", typeof(Properties.Resources))]
        public bool AppendFuzz { get; set; }

        public RandomLengthStringFuzzerNodeConfig()
        {
            MinLength = 0;
            MaxLength = 65535;
            Pattern = "A";
            MinRandomChar = 32; // Printable range
            MaxRandomChar = 127;
        }
    }

    [NodeLibraryClass("RandomLengthStringFuzzerNode", typeof(Properties.Resources),
        ConfigType = typeof(RandomLengthStringFuzzerNodeConfig),
        Category = NodeLibraryClassCategory.Fuzzer
        ), Obsolete
    ]
    public class RandomLengthStringFuzzerNode : 
        BasePipelineNodeWithPersist<RandomLengthStringFuzzerNodeConfig>
    {
        Random _random;

        private void BuildRandomString(int length, StringBuilder builder)
        {
            for (int i = 0; i < length; ++i)
            {
                builder.Append((char)_random.Next(Config.MinRandomChar, Config.MaxRandomChar));
            }
        }

        // Slow way of doing things, could be sped up
        private void BuildPatternString(int length, StringBuilder builder)
        {
            string pattern = Config.Pattern;
            int count = length / pattern.Length;
            int mod = length % pattern.Length;

            while (count > 0)
            {
                builder.Append(pattern);
                count--;
            }

            if (mod > 0)
            {
                builder.Append(pattern.Substring(0, mod));
            }
        }

        private string DoFuzz(string value)
        {
            StringBuilder builder = new StringBuilder();
            int length = _random.Next(Config.MinLength, Config.MaxLength);

            if (Config.AppendFuzz)
            {
                builder.Append(value);
            }

            if (Config.Mode == RandomLengthStringFuzzerNodeConfig.GenerationMode.RandomCharacters)
            {
                BuildRandomString(length, builder);
            }
            else if(Config.Mode == RandomLengthStringFuzzerNodeConfig.GenerationMode.RepeatingPattern)
            {
                BuildPatternString(length, builder);
            }

            return builder.ToString();
        }

        protected override void OnInput(DataFrame frame)
        {            
            DataNode[] nodes = frame.SelectNodes(SelectionPath);

            foreach (DataNode node in nodes)
            {
                DataValue value = node as DataValue;

                if (value == null)
                {
                    LogInfo("Ignoring node {0}, not a data value", node.Name);
                    continue;
                }

                if (!(value.Value is string))
                {
                    LogInfo("Ignoring node {0}, not a string", node.Name);
                    continue;
                }

                value.Value = DoFuzz(value.Value);
            }

            WriteOutput(frame);
        }

        protected override void ValidateConfig(RandomLengthStringFuzzerNodeConfig config)
        {
            if ((config.MaxLength < 0) || (config.MinLength < 0) || (config.MaxLength < config.MinLength))
            {
                throw new ArgumentException("Invalid minimum or maximum fuzz lengths");
            }

            if(config.Mode == RandomLengthStringFuzzerNodeConfig.GenerationMode.RandomCharacters)
            {
                if ((config.MinRandomChar < 0) 
                    || (config.MaxRandomChar < 0) 
                    || (config.MaxRandomChar < config.MinRandomChar) 
                    || (config.MaxRandomChar > (int)Char.MaxValue))
                {
                    throw new ArgumentException("Invalid random character range");
                }
            }
            else if (config.Mode == RandomLengthStringFuzzerNodeConfig.GenerationMode.RepeatingPattern)
            {
                if (String.IsNullOrEmpty(config.Pattern))
                {
                    throw new ArgumentException("Pattern cannot be empty when using a pattern generator");
                }
            }
            else
            {
                throw new ArgumentException("Invalid generation mode");
            }
        }

        public RandomLengthStringFuzzerNode()
        {
            _random = new Random();
        }
    }
}
