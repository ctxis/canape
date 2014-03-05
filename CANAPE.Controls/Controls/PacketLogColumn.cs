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
using CANAPE.DataFrames;
using CANAPE.Nodes;
using CANAPE.Parser;
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Controls
{
    [Serializable]
    public abstract class PacketLogColumn
    {
        protected PacketLogColumn()
        {
            ColumnWidth = 100;
        }

        abstract protected object GetValue(LogPacket p);        

        [LocalizedDescription("PacketLogColumn_NameDescription", typeof(Properties.Resources)), LocalizedCategory("Behavior")]
        public string Name
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Name;
        }

        [LocalizedDescription("PacketLogColumn_ColumnWidthDescription", typeof(Properties.Resources)), LocalizedCategory("Behavior")]
        public int ColumnWidth { get; set;  }

        [LocalizedDescription("PacketLogColumn_CustomFormatDescription", typeof(Properties.Resources)), LocalizedCategory("Behavior")]
        public string CustomFormat { get; set; }

        public virtual string ToString(LogPacket p, int index)
        {
            object value = GetValue(p);
            byte[] ba = value as byte[];

            if (ba != null)
            {
                int length = ba.Length;
                if (length > 64)
                {
                    length = 64;
                }                

                if (!String.IsNullOrWhiteSpace(CustomFormat))
                {
                    try
                    {
                        StringBuilder builder = new StringBuilder();

                        for (int i = 0; i < length; ++i)
                        {
                            builder.AppendFormat(CustomFormat, ba[i]);
                        }

                        return builder.ToString();
                    }
                    catch (FormatException)
                    {
                        // Error in format
                    }
                }

                return GeneralUtils.EscapeString(BinaryEncoding.Instance.GetString(ba, 0, length));
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(CustomFormat))
                {
                    try
                    {
                        return String.Format(CustomFormat, value);
                    }
                    catch (FormatException)
                    {
                        // Error in format
                    }
                }

                return value.ToString();
            }                       
        }

        public virtual IEnumerable<LogPacket> OrderBy(IEnumerable<LogPacket> ps, bool descending)
        {
            if (descending)
            {
                return ps.OrderByDescending(p => GetValue(p));
            }
            else
            {
                return ps.OrderBy(p => GetValue(p));
            }
        }
    }

    [Serializable]
    public sealed class TimestampPacketLogColumn : PacketLogColumn
    {
        [LocalizedDescription("TimestampPacketLogColumn_TicksDescription", typeof(Properties.Resources)), LocalizedCategory("Behavior")]
        public bool Ticks { get; set; }

        public TimestampPacketLogColumn()
        {
            Name = CANAPE.Properties.Resources.PacketLogControl_TimestampColumn;
        }

        protected override object GetValue(LogPacket p)
        {
            object ret = p.Timestamp;

            if (Ticks)
            {
                return p.Timestamp.Ticks;
            }
            else
            {
                return p.Timestamp;
            }
        }
    }

    [Serializable]
    public sealed class TagPacketLogColumn : PacketLogColumn
    {
        public TagPacketLogColumn()
        {
            Name = CANAPE.Properties.Resources.PacketLogControl_TagColumn;
        }

        protected override object GetValue(LogPacket p)
        {
            return p.Tag ?? "";
        }
    }

    [Serializable]
    public sealed class NetworkPacketLogColumn : PacketLogColumn
    {
        protected override object GetValue(LogPacket p)
        {
            return p.Network ?? "";
        }

        public NetworkPacketLogColumn()
        {
            Name = CANAPE.Properties.Resources.PacketLogControl_NetworkColumn;
        }
    }

    [Serializable]
    public sealed class DataPacketLogColumn : PacketLogColumn
    {
        protected override object GetValue(LogPacket p)
        {
            return p.ToString();
        }

        public DataPacketLogColumn()
        {
            Name = CANAPE.Properties.Resources.PacketLogControl_DataColumn;
        }
    }

    [Serializable]
    public sealed class LengthPacketLogColumn : PacketLogColumn
    {
        protected override object GetValue(LogPacket p)
        {
            return p.Length;
        }

        public LengthPacketLogColumn()
        {
            Name = CANAPE.Properties.Resources.PacketLogControl_LengthColumn;
        }
    }

    [Serializable]
    public sealed class HashPacketLogColumn : PacketLogColumn
    {
        protected override object GetValue(LogPacket p)
        {
            return p.Hash;
        }

        public HashPacketLogColumn()
        {
            Name = CANAPE.Properties.Resources.PacketLogControl_HashColumn;
        }
    }

    [Serializable]
    public sealed class CustomPacketLogColumn : PacketLogColumn
    {
        private string _formatExpression;

        [LocalizedDescription("CustomPacketLogColumn_SelectionPathDescription", typeof(Properties.Resources)), LocalizedCategory("Behavior")]
        public string SelectionPath { get; set; }

        [LocalizedDescription("CustomPacketLogColumn_RawValueDescription", typeof(Properties.Resources)), LocalizedCategory("Behavior")]
        public bool RawValue { get; set; }

        [LocalizedDescription("CustomPacketLogColumn_FormatExpressionDescription", typeof(Properties.Resources)), LocalizedCategory("Behavior")]
        public EvalExpression FormatExpression
        {
            get
            {
                return new EvalExpression(_formatExpression ?? String.Empty);
            }

            set
            {
                if (value.Expression != _formatExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _formatExpression = value.Expression;                    
                }
            }
        }

        public CustomPacketLogColumn()
        {
            SelectionPath = "/";
            Name = Properties.Resources.PacketLogControl_CustomColumn;
        }

        protected override object GetValue(LogPacket p)
        {
            DataNode node = p.Frame.Root.SelectSingleNode(SelectionPath);

            if (node == null)
            {
                node = new StringDataValue("", "");
            }

            if (FormatExpression.IsValid)
            {
                ExpressionResolver _resolver = new ExpressionResolver(typeof(LogPacket));
                Dictionary<string, object> extras = new Dictionary<string,object>();
                extras["value"] = node;

                try
                {
                    return _resolver.Resolve(p, _formatExpression, extras) ?? String.Empty;
                }
                catch (Exception)
                {
                    return String.Empty;
                }
            }
            else
            {
                return RawValue ? node.Value : node;
            }
        }
    }

    [Serializable]
    public sealed class NumberPacketLogColumn : PacketLogColumn
    {
        // Not really implemented at the moment, special case
        protected override object GetValue(LogPacket p)
        {
            throw new NotImplementedException();
        }

        public NumberPacketLogColumn()
        {
            ColumnWidth = 35;
            Name = Properties.Resources.PacketLogControl_NoColumn;
        }

        public override string ToString(LogPacket p, int index)
        {
            if (!String.IsNullOrWhiteSpace(CustomFormat))
            {
                try
                {
                    return String.Format(CustomFormat, index + 1);
                }
                catch (FormatException)
                {
                }
            }

            return (index + 1).ToString();
        }

        // The number column doesn't yet order as it is meaningless
        public override IEnumerable<LogPacket> OrderBy(IEnumerable<LogPacket> ps, bool descending)
        {
            return ps;
        }
    }
}
