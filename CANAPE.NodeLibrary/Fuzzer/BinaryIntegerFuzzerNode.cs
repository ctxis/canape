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
using System.Linq;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    [Serializable]
    public class BinaryIntegerFuzzerNodeConfig
    {
        private int _fuzzLength;
        private long _increment;

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_FuzzStartDescription", typeof(Properties.Resources))]
        public int FuzzStart { get; set; }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_FuzzLengthDescription", typeof(Properties.Resources))]
        public int FuzzLength 
        {
            get
            {
                return _fuzzLength;
            }

            set
            {
                if ((value < 1) || (value > 8))
                {
                    throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.BinaryIntegerFuzzerNode_InvalidLength);
                }

                _fuzzLength = value;
            }
        }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_NoConversionDescription", typeof(Properties.Resources))]
        public bool NoConversion { get; set; }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_MinValueDescription", typeof(Properties.Resources))]
        public long MinValue { get; set; }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_MaxValueDescription", typeof(Properties.Resources))]
        public long MaxValue { get; set; }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_IncrementDescription", typeof(Properties.Resources))]
        public long Increment 
        {
            get { return _increment; }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.BinaryIntegerFuzzerNode_InvalidIncrement);
                }

                _increment = value;
            }
        }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_LittleEndianDescription", typeof(Properties.Resources))]
        public bool LittleEndian { get; set; }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_LogFuzzTextDescription", typeof(Properties.Resources))]
        public bool LogFuzzText { get; set; }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_LogPacketsDescription", typeof(Properties.Resources))]
        public bool LogPackets { get; set; }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_ConvertToBytesDescription", typeof(Properties.Resources))]
        public bool ConvertToBytes { get; set; }

        [LocalizedDescription("BinaryIntegerFuzzerNodeConfig_ColorDescription", typeof(Properties.Resources))]
        public ColorValue Color { get; set; }

        public BinaryIntegerFuzzerNodeConfig()
        {
            FuzzLength = 1;
            MinValue = 0;
            MaxValue = byte.MaxValue;
            _increment = 1;
            Color = new ColorValue(255, 255, 255);
        }
    }

     [NodeLibraryClass("BinaryIntegerFuzzerNode", typeof(Properties.Resources),
        ConfigType = typeof(BinaryIntegerFuzzerNodeConfig),
        Category = NodeLibraryClassCategory.Fuzzer
        )
    ]
    public class BinaryIntegerFuzzerNode : BasePipelineNodeWithPersist<BinaryIntegerFuzzerNodeConfig>
    {
        protected override void OnInput(DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);
            DataFrame oldFrame = null;
            bool doLog = false;
            string counterName = String.Format("{0}_count", Uuid);

            if (nodes.Length > 0)
            {
                if (Config.LogPackets)
                {
                    oldFrame = frame.CloneFrame();                    
                }
            }

            foreach (DataNode node in nodes)
            {
                if (Config.NoConversion)
                {
                    DataValue value = node as DataValue;

                    // If not a byte array then ignore
                    if ((value == null) || !(value.Value is byte[]))
                    {
                        LogVerbose(CANAPE.NodeLibrary.Properties.Resources.ByteFuzzer_IgnoringNode, node.Name);
                        continue;
                    }
                }

                byte[] data = node.ToArray();

                int fuzzLength = FuzzerUtils.GetFuzzLength(data.Length, Config.FuzzStart, Config.FuzzLength);
                

                if (fuzzLength > 0)
                {
                    long count = Graph.GlobalMeta.IncrementCounterLong(counterName, Config.MinValue, Config.Increment);

                    if (Config.Increment < 0)
                    {
                        if (count < Config.MaxValue)
                        {
                            LogInfo("No more values to fuzz");
                        }
                        else
                        {
                            doLog = true;
                        }
                    }
                    else if (Config.Increment > 0)
                    {
                        if (count > Config.MaxValue)
                        {
                            LogInfo("No more values to fuzz");
                        }
                        else
                        {
                            doLog = true;
                        }
                    }

                    if (doLog)
                    {
                        if (Config.LogFuzzText)
                        {
                            LogVerbose("Fuzzing with value {0}", count);
                        }
                        
                        byte[] countData = BitConverter.GetBytes(count);

                        Array.Resize(ref countData, fuzzLength);

                        if (!Config.LittleEndian)
                        {
                            countData = countData.Reverse().ToArray();
                        }

                        Buffer.BlockCopy(countData, 0, data, Config.FuzzStart, fuzzLength);

                        node.ReplaceNode(data);
                    }
                }
            }

            if (doLog)
            {
                if (Config.LogPackets)
                {
                    Graph.DoLogPacket(String.Format("{0}: Pre-fuzz", Name), Config.Color, oldFrame, Config.ConvertToBytes);
                    Graph.DoLogPacket(String.Format("{0}: Post-fuzz", Name), Config.Color, frame, Config.ConvertToBytes);
                }
            }

            WriteOutput(frame);
        }

        protected override void ValidateConfig(BinaryIntegerFuzzerNodeConfig config)
        {
            base.ValidateConfig(config);

            if (config.Increment == 0)
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.BinaryIntegerFuzzerNode_InvalidIncrement);
            }

            if ((config.FuzzLength < 1) || (config.FuzzLength > 8))
            {
                throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.BinaryIntegerFuzzerNode_InvalidLength);
            }

            if (config.Increment < 0)
            {
                if (config.MaxValue > config.MinValue)
                {
                    throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.BinaryIntegerFuzzerNode_NegativeInvalidMaxValue);
                }
            }
            else if (config.Increment > 0)
            {
                if (config.MaxValue < config.MinValue)
                {
                    throw new ArgumentException(CANAPE.NodeLibrary.Properties.Resources.BinaryIntegerFuzzerNode_PositiveInvalidMaxValue);
                }
            }
        }
    }
}
