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

// Custom packet exporter, select packets in a log then choose Run Script
// from the right click menu

using CANAPE.Scripting;
using CANAPE.Nodes;
using System.IO;
using System.Collections.Generic;

class SelectedPacketsExporter
{	
	// Do the actual work	
	public static void ProcessPackets(IEnumerable<LogPacket> packets)
	{		
		using(StreamWriter writer = new StreamWriter("packets.csv"))
		{
			writer.WriteLine("Timestamp,Tag,Network,Data");
			foreach(LogPacket p in packets)
			{
				writer.WriteLine("{0},{1},{2},{3}", p.Timestamp, p.Tag, p.Network, p.Frame.Root.ToDataString());
			}
		}		
	}
}
