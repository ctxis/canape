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
    [Serializable]
    public class BaseRandomArrayFuzzerConfig
    {
        public enum FuzzCombinationMode
        {
            Overwrite = 0,
            Add = 1,
            Xor = 2,
            And = 3,
            Or = 4,
            Max = 5,
            Random = -1
        }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_MaxFuzzPointsDescription", typeof(Properties.Resources))]
        public int MaxFuzzPoints { get; set; }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_RandomFuzzPointCountDescription", typeof(Properties.Resources))]
        public bool RandomFuzzPointCount { get; set; }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_CombinationModeDescription", typeof(Properties.Resources))]
        public FuzzCombinationMode CombinationMode { get; set; }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_LogFuzzTextDescription", typeof(Properties.Resources))]
        public bool LogFuzzText { get; set; }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_LogPacketsDescription", typeof(Properties.Resources))]
        public bool LogPackets { get; set; }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_ConvertToBytesDescription", typeof(Properties.Resources))]
        public bool ConvertToBytes { get; set; }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_ColorDescription", typeof(Properties.Resources))]
        public ColorValue Color { get; set; }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_FuzzStartDescription", typeof(Properties.Resources))]
        public int FuzzStart { get; set; }

        [LocalizedDescription("BaseRandomArrayFuzzerConfig_FuzzLengthDescription", typeof(Properties.Resources))]
        public int FuzzLength { get; set; }

        public BaseRandomArrayFuzzerConfig()
        {
            MaxFuzzPoints = 1;
            Color = new ColorValue(255, 255, 255);
            FuzzLength = -1;
        }
    }

    public abstract class BaseRandomArrayFuzzer<U, T> : BasePipelineNodeWithPersist<T> where T : BaseRandomArrayFuzzerConfig, new() where U : struct
    {
        protected Random _random;

        public BaseRandomArrayFuzzer()
        {
            _random = new Random();
        }

        private int GetPointCount(int length)
        {
            int max = Config.MaxFuzzPoints > 0 ? Config.MaxFuzzPoints : length;
            if (Config.RandomFuzzPointCount)
            {
                return _random.Next(max) + 1;
            }
            else
            {
                return max;
            }
        }

        protected override void OnInput(DataFrames.DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);
            DataFrame preFuzz = null;
            bool fuzzed = false;

            if (nodes.Length > 0)
            {
                if (Config.LogPackets)
                {
                    preFuzz = frame.CloneFrame();
                }
            }

            foreach (DataNode node in nodes)
            {
                U[] data = NodeToArray(node);

                if(data == null)
                {
                    LogVerbose(CANAPE.NodeLibrary.Properties.Resources.BaseRandomArrayFuzzer_IgnoringData, node.Name);
                    continue;
                }

                int fuzzLength = FuzzerUtils.GetFuzzLength(data.Length, Config.FuzzStart, Config.FuzzLength);

                if (fuzzLength > 0)
                {
                    int points = GetPointCount(fuzzLength);

                    if (Config.LogFuzzText)
                    {
                        LogVerbose(CANAPE.NodeLibrary.Properties.Resources.BaseRandomArrayFuzzer_FuzzCount, points);
                    }

                    while (points > 0)
                    {
                        int pos = _random.Next(Config.FuzzStart, Config.FuzzStart + fuzzLength);
                        BaseRandomArrayFuzzerConfig.FuzzCombinationMode mode = Config.CombinationMode;

                        if (mode == BaseRandomArrayFuzzerConfig.FuzzCombinationMode.Random)
                        {
                            mode = (BaseRandomArrayFuzzerConfig.FuzzCombinationMode)_random.Next((int)BaseRandomArrayFuzzerConfig.FuzzCombinationMode.Max);
                        }

                        if ((pos >= 0) && (pos < data.Length))
                        {
                            U value = FuzzValue(mode, data[pos]);

                            if (!value.Equals(data[pos]))
                            {
                                fuzzed = true;
                                if (Config.LogFuzzText)
                                {
                                    LogInfo(CANAPE.NodeLibrary.Properties.Resources.BaseRandomArrayFuzzer_FuzzInfo,
                                            pos, data[pos], value);
                                }
                            }

                            data[pos] = value;
                        }
                    
                        points--;
                    }

                    node.ReplaceNode(ArrayToNode(data, node));
                }
            }

            if (fuzzed)
            {
                if (Config.LogPackets)
                {
                    Graph.DoLogPacket(String.Format(CANAPE.NodeLibrary.Properties.Resources.BaseRandomArrayFuzzer_PreFuzzInfo, Name), 
                        Config.Color, preFuzz, Config.ConvertToBytes);
                    Graph.DoLogPacket(String.Format(CANAPE.NodeLibrary.Properties.Resources.BaseRandomArrayFuzzer_PostFuzzInfo, Name), 
                        Config.Color, frame, Config.ConvertToBytes);
                }
            }

            WriteOutput(frame);
        }

        protected abstract U[] NodeToArray(DataNode node);
        protected abstract DataNode ArrayToNode(U[] array, DataNode node);
        protected abstract U FuzzValue(BaseRandomArrayFuzzerConfig.FuzzCombinationMode mode, U value);
    }
}
