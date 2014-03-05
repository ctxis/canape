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

namespace CANAPE.NodeLibrary.Mutator
{
    [Serializable]
    public class BinaryMutatorNodeConfig
    {
        [LocalizedDescription("BinaryMutatorNodeConfig_MatchDescription", typeof(Properties.Resources))]
        public byte[] Match { get; set; }

        [LocalizedDescription("BinaryMutatorNodeConfig_ReplacementDescription", typeof(Properties.Resources))]
        public byte[] Replacement { get; set; }

        public BinaryMutatorNodeConfig()
        {
            Match = new byte[0];
            Replacement = new byte[0];
        }
    }

    [NodeLibraryClass("BinaryMutatorNode", typeof(Properties.Resources),
        ConfigType = typeof(BinaryMutatorNodeConfig),        
        Category = NodeLibraryClassCategory.Mutator)]
    public class BinaryMutatorNode : BasePipelineNodeWithPersist<BinaryMutatorNodeConfig>
    {
        protected override void OnInput(DataFrames.DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);

            foreach (DataNode node in nodes)
            {
                byte[] data = node.ToArray();
                bool match = false;
                int i = 0;

                for (i = 0; i < data.Length - Config.Match.Length + 1; ++i)
                {
                    match = GeneralUtils.MatchArray(data, i, Config.Match);
                    if (match)
                    {
                        break;
                    }
                }

                if (match)
                {
                    byte[] newArray = new byte[data.Length - Config.Match.Length + Config.Replacement.Length];

                    Buffer.BlockCopy(data, 0, newArray, 0, i);
                    Buffer.BlockCopy(Config.Replacement, 0, newArray, i, Config.Replacement.Length);
                    Buffer.BlockCopy(data, i + Config.Match.Length, newArray, i + Config.Replacement.Length, data.Length - i - Config.Match.Length);

                    node.ReplaceNode(newArray);
                }
            }

            WriteOutput(frame);
        }
    }
}
