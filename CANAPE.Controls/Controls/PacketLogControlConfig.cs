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
using System.Drawing;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    [Serializable]
    public class PacketLogControlConfig
    {
        public List<PacketLogColumn> Columns { get; private set; }

        public Font DefaultFont { get; set; }

        public bool AutoScroll { get; set; }

        public PacketLogControlConfig()
        {
            Columns = new List<PacketLogColumn>();

            Columns.Add(new NumberPacketLogColumn());
            Columns.Add(new TimestampPacketLogColumn());
            Columns.Add(new TagPacketLogColumn());
            Columns.Add(new NetworkPacketLogColumn());
            Columns.Add(new DataPacketLogColumn());
            Columns.Add(new LengthPacketLogColumn());
            Columns.Add(new HashPacketLogColumn());
        }

        public PacketLogControlConfig CloneConfig()
        {
            return GeneralUtils.CloneObject(this);
        }
    }
}
