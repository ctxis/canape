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
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Parser
{
    /// <summary>
    /// Node configuration
    /// </summary>
    [Serializable]
    public class BinaryVectorConfig
    {
        /// <summary>
        /// Enumeration to indicate the size of the length field
        /// </summary>
        public enum BinaryLengthType
        {
            ByteLength,
            ShortLength,
            IntLength,
            LongLength,
            SevenBitVariableLength,
            TwentyFourBitLength,
        }

        [LocalizedDescription("BinaryVectorConfig_LittleEndianDescription", typeof(Properties.Resources))]
        public bool LittleEndian { get; set; }

        [LocalizedDescription("BinaryVectorConfig_LengthTypeDescription", typeof(Properties.Resources))]
        public BinaryLengthType LengthType { get; set; }

        [LocalizedDescription("BinaryVectorConfig_InclusiveDescription", typeof(Properties.Resources))]
        public bool Inclusive { get; set; }

        [LocalizedDescription("BinaryVectorConfig_StrideDescription", typeof(Properties.Resources))]
        public uint Stride { get; set; }

        /// <summary>
        /// Indicates a pre and post adjustment value to the length to account for uncounted fields
        /// </summary>
        [LocalizedDescription("BinaryVectorConfig_AdjustmentDescription", typeof(Properties.Resources))]
        public int Adjustment { get; set; }

        [LocalizedDescription("BinaryVectorConfig_PrefixLenDescription", typeof(Properties.Resources))]
        public int PrefixLen { get; set; }

        public BinaryVectorConfig()
        {
            LittleEndian = true;
            LengthType = BinaryLengthType.ByteLength;
            Inclusive = true;
            Stride = 1;
            Adjustment = 0;
        }
    }

    [NodeLibraryClass("BinaryVectorParser", typeof(Properties.Resources),
        ConfigType = typeof(BinaryVectorConfig),
        Category=NodeLibraryClassCategory.Control)]
    public class BinaryVectorParser : BasePersistDynamicNode<BinaryVectorConfig>, IDataStreamParser
    {       
        private ulong ReadLength(DataReader reader, Logger logger)
        {
            ulong lengthSize = 0;
            ulong ret = 0;

            switch (Config.LengthType)
            {
                case BinaryVectorConfig.BinaryLengthType.ByteLength:
                    ret = reader.ReadByte();
                    lengthSize = 1;
                    break;
                case BinaryVectorConfig.BinaryLengthType.ShortLength:
                    ret = reader.ReadUInt16(Config.LittleEndian);
                    lengthSize = 2;
                    break;
                case BinaryVectorConfig.BinaryLengthType.IntLength:
                    ret = reader.ReadUInt32(Config.LittleEndian);
                    lengthSize = 4;
                    break;
                case BinaryVectorConfig.BinaryLengthType.LongLength:
                    ret = reader.ReadUInt64(Config.LittleEndian);
                    lengthSize = 8;
                    break;
                case BinaryVectorConfig.BinaryLengthType.SevenBitVariableLength:
                    while (true)
                    {
                        int bytePos = 0;
                        byte b = reader.ReadByte();
                        ret |= ((ulong)b & 0x7F) << (bytePos++ * 7);
                        if ((b & 0x80) == 0)
                        {
                            break;
                        }

                        lengthSize = (ulong)bytePos;
                    }
                    break;
                case BinaryVectorConfig.BinaryLengthType.TwentyFourBitLength:
                    ret = reader.ReadUInt24(Config.LittleEndian);
                    lengthSize = 3;
                    break;
                default:
                    throw new ArgumentException("Invalid length type");
            }

            // Remove the inclusize length
            if (Config.Inclusive)
            {
                ret -= lengthSize;
            }

            return ret;
        }

        public void FromReader(DataReader reader, DataFrames.DataKey root, Utils.Logger logger)
        {
            if (Config.PrefixLen > 0)
            {
                root.AddValue("Prefix", reader.ReadBytes(Config.PrefixLen, true));
            }

            ulong length = ReadLength(reader, logger);

            logger.LogVerbose("Read length field of {0} bytes", length);


            byte[] data = reader.ReadBytes((int)(length*Config.Stride)+Config.Adjustment, true);

            root.AddValue("Data", data);
        }

        private static ulong CalculateInclusive7bit(ulong length)
        {
            // Boundary points of the length
            ulong[] vals = new [] {
                    0x7eUL ,
                    0x3ffdUL ,
                    0x1ffffcUL ,
                    0xffffffbUL ,
                    0x7fffffffaUL ,
                    0x3fffffffff9UL ,
                    0x1fffffffffff8UL ,
                    0xfffffffffffff7UL ,
                    0x7ffffffffffffff6UL };

            for (int i = 0; i < vals.Length; ++i)
            {
                if (length <= vals[i])
                {
                    length += (ulong)i + 1;
                    break;
                }
            }

            return length;
        }

        private void WriteLength(DataWriter writer, ulong length, Logger logger)
        {
            switch (Config.LengthType)
            {
                case BinaryVectorConfig.BinaryLengthType.ByteLength:
                    if (Config.Inclusive)
                    {
                        length++;
                    }

                    writer.Write((byte)length);

                    break;
                case BinaryVectorConfig.BinaryLengthType.ShortLength:
                    if (Config.Inclusive)
                    {
                        length += 2;
                    }
                    writer.WriteUInt16((ushort)length, Config.LittleEndian);
                    break;
                case BinaryVectorConfig.BinaryLengthType.IntLength:
                    if (Config.Inclusive)
                    {
                        length += 4;
                    }
                    writer.WriteUInt32((uint)length, Config.LittleEndian);
                    break;
                case BinaryVectorConfig.BinaryLengthType.LongLength:
                    if (Config.Inclusive)
                    {
                        length += 8;
                    }
                    writer.WriteUInt64(length, Config.LittleEndian);
                    break;
                case BinaryVectorConfig.BinaryLengthType.TwentyFourBitLength:
                    if (Config.Inclusive)
                    {
                        length += 3;
                        writer.WriteUInt24((UInt24)length, Config.LittleEndian);
                    }
                    break;
                case BinaryVectorConfig.BinaryLengthType.SevenBitVariableLength:
                        
                    if (Config.Inclusive)
                    {
                        length = CalculateInclusive7bit(length);
                    }

                    do
                    {
                        byte nextLength = (byte)(length & 0x7F);
                        length >>= 7;

                        if (length != 0)
                        {
                            nextLength |= 0x80;
                        }

                        writer.Write(nextLength);
                    }
                    while (length != 0);

                    break;

                default:
                    throw new ArgumentException("Invalid length type");
            }            
        }

        public void ToWriter(DataFrames.DataWriter writer, DataFrames.DataKey root, Utils.Logger logger)
        {
            MemoryStream stm = new MemoryStream();
            DataWriter w = new DataWriter(stm);

            foreach (DataNode node in root.SubNodes)
            {
                node.ToWriter(w);
            }

            byte[] data = stm.ToArray();

            logger.LogVerbose("Writing length field of {0} bytes", data.Length);

            if ((data.Length % Config.Stride) != 0)
            {
                logger.LogWarning("Data length of {0} is not a multiple of stride {1}", data.Length, Config.Stride);
            }

            WriteLength(writer, (ulong)(data.Length - Config.Adjustment) / Config.Stride, logger);
            writer.Write(stm.ToArray());
        }

        public string ToDisplayString(DataFrames.DataKey root, Utils.Logger logger)
        {
            DataNode node = root.SelectSingleNode("Data");

            if (node != null)
            {
                return node.ToString();
            }
            else
            {
                return String.Format("Type: {0} Little Endian: {1} Exclusive: {2}", Config.LengthType, Config.LittleEndian, Config.Inclusive);
            }            
        }

        protected override void ValidateConfig(BinaryVectorConfig config)
        {
            if (config.Stride == 0)
            {
                throw new ArgumentException("Stride cannot be 0");
            }
        }
    }
}
