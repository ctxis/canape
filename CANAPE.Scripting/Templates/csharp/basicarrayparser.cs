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
using CANAPE.Utils;
using CANAPE.DataFrames;

// Array parser, acts on a finite blob of data passed in one go
class ArrayParser : IDataArrayParser
{
    // Called to parse the array into a data key
    public void FromArray(byte[] data, DataKey root, Logger logger)
    {
        logger.LogInfo("Received array of length {0}", data.Length);

        root.AddValue("data", data.Reverse().ToArray());
    }

    // Called to convert data key to an array
    public byte[] ToArray(DataKey root, Logger logger)
    {
        logger.LogInfo("Creating array");

        return root["data"].ToArray().Reverse().ToArray();
    }

    // Called to display a string to the user
    public string ToDisplayString(DataKey root, Logger logger)
    {
        return root["data"].ToString();
    }
}

