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
using System.Globalization;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    [Serializable]
    public class NumericStringFuzzerNodeConfig : BaseStringFuzzerNodeConfig
    {
        public enum NumericStringFormat
        {
            Decimal,
            Hexadecimal,
            Octal,
            Binary,
        }

        [LocalizedDescription("NumericStringFuzzerNodeConfig_StartValueDescription", typeof(Properties.Resources))]
        public long StartValue { get; set; }

        [LocalizedDescription("NumericStringFuzzerNodeConfig_EndValueDescription", typeof(Properties.Resources))]
        public long EndValue { get; set; }

        [LocalizedDescription("NumericStringFuzzerNodeConfig_IncrementDescription", typeof(Properties.Resources))]
        public long Increment { get; set; }

        [LocalizedDescription("NumericStringFuzzerNodeConfig_FormatDescription", typeof(Properties.Resources))]
        public NumericStringFormat Format { get; set; }

        [LocalizedDescription("NumericStringFuzzerNodeConfig_CustomFormatDescription", typeof(Properties.Resources))]
        public string CustomFormat { get; set; }

        [LocalizedDescription("NumericStringFuzzerNodeConfig_StoreLocalPositionDescription", typeof(Properties.Resources))]
        public bool StoreLocalPosition { get; set; }

        public NumericStringFuzzerNodeConfig()
        {
            Increment = 1;
            EndValue = ushort.MaxValue;
        }
    }

    [NodeLibraryClass("NumericStringFuzzerNode", typeof(Properties.Resources),
        ConfigType = typeof(NumericStringFuzzerNodeConfig),
        Category = NodeLibraryClassCategory.Fuzzer
        )
    ]
    public class NumericStringFuzzerNode : BaseStringFuzzerNode<NumericStringFuzzerNodeConfig>
    {
        private string _formatString;

        private string FormatValue(long value)
        {
            string ret = null;

            if (_formatString != null)
            {
                ret = String.Format(CultureInfo.InvariantCulture, _formatString, value);
            }
            else
            {
                switch (Config.Format)
                {
                    case NumericStringFuzzerNodeConfig.NumericStringFormat.Octal:
                        ret = Convert.ToString(value, 8);
                        break;
                    case NumericStringFuzzerNodeConfig.NumericStringFormat.Binary:
                        ret = Convert.ToString(value, 2);
                        break;
                }
            }

            return ret;
        }

        protected override string DoFuzz(string value)
        {
            string ret = null;
            long curr = (Config.StoreLocalPosition ? Graph.Meta : Graph.GlobalMeta).IncrementCounterLong(String.Format("{0}_count", Uuid), Config.StartValue, Config.Increment);
            if (Config.Increment < 0)
            {
                if (curr >= Config.EndValue)
                {
                    ret = FormatValue(curr);
                }
            }
            else
            {
                if (curr <= Config.EndValue)
                {
                    ret = FormatValue(curr);
                }
            }

            return ret ?? value;
        }

        protected override void ValidateConfig(NumericStringFuzzerNodeConfig config)
        {
            base.ValidateConfig(config);

            if (config.Increment < 0)
            {
                if (config.EndValue > config.StartValue)
                {
                    throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.NumericStringFuzzerNode_InvalidNegativeIncrement);
                }
            }
            else if (config.Increment > 0)
            {
                if (config.EndValue < config.StartValue)
                {
                    throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.NumericStringFuzzerNode_InvalidPositiveIncrement);
                }
            }
            else
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.NumericStringFuzzerNode_InvalidIncrement);
            }

            if (!String.IsNullOrWhiteSpace(config.CustomFormat))
            {
                _formatString = config.CustomFormat;
            }
            else
            {
                switch (config.Format)
                {
                    case NumericStringFuzzerNodeConfig.NumericStringFormat.Decimal:
                        _formatString = "{0}";
                        break;
                    case NumericStringFuzzerNodeConfig.NumericStringFormat.Hexadecimal:
                        _formatString = "{0:X}";
                        break;
                    case NumericStringFuzzerNodeConfig.NumericStringFormat.Octal:
                    case NumericStringFuzzerNodeConfig.NumericStringFormat.Binary:
                        break;
                    default:
                        throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.NumericStringFuzzerNode_InvalidStringFormat);
                }
            }
        }
    }
}
