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
using System.IO;
using System.Text;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Mutator
{
    [Serializable]
    public class StringConverterNodeConfig
    {
        [LocalizedDescription("StringConverterNodeConfig_EncodingDescription", typeof(Properties.Resources))]
        public BinaryStringEncoding Encoding { get; set; }
    }

    [NodeLibraryClass("StringConverterNode", typeof(Properties.Resources),
        ConfigType = typeof(StringConverterNodeConfig),
        Category = NodeLibraryClassCategory.Mutator)]
    public class StringConverterNode : BasePipelineNodeWithPersist<StringConverterNodeConfig>
    {
        protected override void OnInput(DataFrames.DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);
            Encoding encoding = GeneralUtils.GetEncodingFromType(Config.Encoding);

            foreach (DataNode node in nodes)
            {
                MemoryStream stm = new MemoryStream(node.ToArray());
                DataReader reader = new DataReader(stm);
                StringBuilder builder = new StringBuilder();

                try
                {
                    while (!reader.Eof)
                    {
                        builder.Append(reader.ReadChar(encoding));
                    }
                }
                catch (EndOfStreamException)
                {
                }

                node.ReplaceNode(builder.ToString(), encoding);
            }

            WriteOutput(frame);
        }
    }
}
