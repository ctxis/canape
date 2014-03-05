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
using System.Linq;
using CANAPE.Scripting;
using CANAPE.DataFrames;
using CANAPE.Utils;

class StreamParser : IDataStreamParser
{
    // Populate a datakey with data from a stream
    public void FromReader(DataReader reader, DataKey root, Logger logger)
    {
        // Read up to five bytes and reverse them
        byte[] data = reader.ReadBytes(5, false).Reverse().ToArray();

        logger.LogInfo("Read {0} bytes from stream", data.Length);

        // Add to key
        root.AddValue("data", data);
    }

    // Write a datakey to a stream
    public void ToWriter(DataWriter writer, DataKey root, Logger logger)
    {
        logger.LogInfo("Writing to stream");

        // Write Data node back to stream
        // Will throw  MissingDataNodeException if it does not exist
        byte[] data = root["data"].ToArray().Reverse().ToArray();

        writer.Write(data);
    }

    // Return a string to display to the user
    public string ToDisplayString(DataKey root, Logger logger)
    {
        DataNode node = root.SelectSingleNode("data");

        if (node == null)
        {
            return String.Empty;
        }
        else
        {
            return node.ToString();
        }
    }
}
