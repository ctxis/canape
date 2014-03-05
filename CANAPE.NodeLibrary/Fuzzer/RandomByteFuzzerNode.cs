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
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    /// <summary>
    /// Config for binary fuzzer node
    /// </summary>
    [Serializable]
    public class RandomByteFuzzerNodeConfig : BaseRandomArrayFuzzerConfig
    {
        [LocalizedDescription("RandomByteFuzzerNodeConfig_MinByteValueDescription", typeof(Properties.Resources))]
        public byte MinByteValue { get; set; }

        [LocalizedDescription("RandomByteFuzzerNodeConfig_MaxByteValueDescription", typeof(Properties.Resources))]
        public byte MaxByteValue { get; set; }

        [LocalizedDescription("RandomByteFuzzerNodeConfig_NoConversionDescription", typeof(Properties.Resources))]
        public bool NoConversion { get; set; }
        
        public RandomByteFuzzerNodeConfig()
        {
            MaxByteValue = byte.MaxValue;
        }
    }

    [NodeLibraryClass("RandomByteFuzzerNode", typeof(Properties.Resources),
        ConfigType = typeof(RandomByteFuzzerNodeConfig),
        Category = NodeLibraryClassCategory.Fuzzer
        )
    ]
    public class RandomByteFuzzerNode : BaseRandomArrayFuzzer<byte, RandomByteFuzzerNodeConfig>
    {
        protected override void ValidateConfig(RandomByteFuzzerNodeConfig config)
        {
            base.ValidateConfig(config);

            if (config.MaxByteValue < config.MinByteValue)
            {
                throw new ArgumentOutOfRangeException(CANAPE.NodeLibrary.Properties.Resources.RandomByteFuzzerNode_ConfigError);
            }
        }

        protected override byte[] NodeToArray(DataNode node)
        {
            byte[] ret = null;

            DataValue value = node as DataValue;

            if ((value != null) && (value.Value is byte[]))
            {
                ret = (byte[])value.Value;
            }
            else
            {
                if (!Config.NoConversion)
                {
                    ret = node.ToArray();
                }
            }

            return ret;
        }

        protected override DataNode ArrayToNode(byte[] array, DataNode node)
        {
            ByteArrayDataValue value = new ByteArrayDataValue(node.Name, array);

            return value;
        }

        protected override byte FuzzValue(BaseRandomArrayFuzzerConfig.FuzzCombinationMode mode, byte value)
        {            
            byte fuzz = (byte)_random.Next(Config.MinByteValue, Config.MaxByteValue);
            byte ret = value;

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

            return ret;        
        }
    }
}
