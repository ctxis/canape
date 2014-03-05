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
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    /// <summary>
    /// Config for binary fuzzer node
    /// </summary>
    [Serializable]
    public class RandomCharArrayFuzzerNodeConfig : BaseRandomArrayFuzzerConfig
    {
        [LocalizedDescription("RandomCharArrayFuzzerNodeConfig_MinValueDescription", typeof(Properties.Resources))]
        public ushort MinValue { get; set; }

        [LocalizedDescription("RandomCharArrayFuzzerNodeConfig_MaxValueDescription", typeof(Properties.Resources))]
        public ushort MaxValue { get; set; }

        [LocalizedDescription("RandomCharArrayFuzzerNodeConfig_NoConversionDescription", typeof(Properties.Resources))]
        public bool NoConversion { get; set; }

        [LocalizedDescription("RandomCharArrayFuzzerNodeConfig_StringEncodingDescription", typeof(Properties.Resources))]
        public BinaryStringEncoding StringEncoding { get; set; }

        public RandomCharArrayFuzzerNodeConfig()
        {
            MaxValue = char.MaxValue;
        }
    }

    public class RandomCharArrayFuzzerNode : BaseRandomArrayFuzzer<char, RandomCharArrayFuzzerNodeConfig>
    {
        Encoding _encoding;

        protected override char[] NodeToArray(DataFrames.DataNode node)
        {
            DataValue value = node as DataValue;
            char[] ret = null;

            if ((value != null) && (value.Value is string))
            {
                ret = ((string)value.Value).ToCharArray();
            }
            else
            {
                if (!Config.NoConversion)
                {
                    ret = _encoding.GetChars(node.ToArray());
                }
            }

            return ret;
        }

        protected override DataNode ArrayToNode(char[] array, DataNode node)
        {
            DataValue value = node as DataValue;            

            if ((value != null) && (value.Value is string))
            {
                value.Value = new String(array);
            }
            else
            {
                if (!Config.NoConversion)
                {
                    node.ReplaceNode(_encoding.GetBytes(array));
                }
            }

            return node;
        }

        protected override char FuzzValue(BaseRandomArrayFuzzerConfig.FuzzCombinationMode mode, char value)
        {           
            ushort fuzz = (ushort)_random.Next(Config.MinValue, Config.MaxValue);
            ushort ret = (ushort)value;

            switch (mode)
            {
                case BaseRandomArrayFuzzerConfig.FuzzCombinationMode.Add:
                    ret += fuzz;
                    break;
                case BaseRandomArrayFuzzerConfig.FuzzCombinationMode.And:
                    ret &= fuzz;
                    break;
                case BaseRandomArrayFuzzerConfig.FuzzCombinationMode.Or:
                    ret |= fuzz;
                    break;
                case BaseRandomArrayFuzzerConfig.FuzzCombinationMode.Overwrite:
                    ret = fuzz;
                    break;                
                case BaseRandomArrayFuzzerConfig.FuzzCombinationMode.Xor:
                    ret ^= fuzz;
                    break;
            }

            return (char)ret;            
        }

        protected override void  ValidateConfig(RandomCharArrayFuzzerNodeConfig config)
        {
 	        base.ValidateConfig(config);

            if (Config.MaxValue < Config.MinValue)
            {
                throw new ArgumentException("Invalid max value, must be greater or equal to minimum");
            }

            _encoding = GeneralUtils.GetEncodingFromType(Config.StringEncoding);
        }
    }
}
