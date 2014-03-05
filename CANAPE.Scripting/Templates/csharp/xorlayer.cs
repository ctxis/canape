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
using CANAPE.DataFrames;
using CANAPE.Nodes;
using CANAPE.Net.Layers;
using CANAPE.DataAdapters;

// Simple example script to implement a layer which xor's bytes with a known value
class XorNetworkLayer : DynamicNetworkLayer
{   
	DataFrame XorFrame(DataFrame frame, byte xorValue)
	{
		byte[] data = frame.ToArray();
		
		for(int i = 0; i < data.Length; ++i)
		{
			data[i] = (byte)(data[i] ^ xorValue);
		}
		
		return new DataFrame(data);
	}
 
    // Called when reading from client -> server
	protected override IEnumerable<DataFrame> ReadClientFrames(IDataAdapter client)	
	{
		Logger.LogInfo("Starting to read client frames");
    	DataFrame frame = client.Read();
        while (frame != null)
        {
             yield return XorFrame(frame, 42);

             frame = client.Read();
        }
	}
	
	// Called when writing from server -> client
	protected override void WriteClientFrame(IDataAdapter client, DataFrame frame)
	{
		client.Write(XorFrame(frame, 88));
	}
	
	// Called when reading from server -> client
	protected override IEnumerable<DataFrame> ReadServerFrames(IDataAdapter client)	
	{
		Logger.LogInfo("Starting to read server frames");
    	DataFrame frame = client.Read();
        while (frame != null)
        {
             yield return XorFrame(frame, 88);

             frame = client.Read();
        }
	}
	
	// Called when writing from client -> server
	protected override void WriteServerFrame(IDataAdapter client, DataFrame frame)
	{
		client.Write(XorFrame(frame, 42));
	}
}
