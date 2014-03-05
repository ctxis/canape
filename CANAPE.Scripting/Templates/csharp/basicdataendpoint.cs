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

using System.Linq;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

class DataEndpoint : BaseDataEndpoint
{
    // Run method, should exit when finished
    public override void Run(IDataAdapter adapter, Logger logger)
    {
        // Write out a message on connection
        adapter.Write(new DataFrame("Hello There!\n"));

        DataFrame frame = adapter.Read();

        while (frame != null)
        {
            logger.LogInfo("Received {0}", frame);

            // Write it back out again reversed
            adapter.Write(new DataFrame(frame.ToArray().Reverse().ToArray()));

            frame = adapter.Read();
        }
    }

    // Gets a textual description of the server
    public override string Description
    {
        get { return "Example Reversing Endpoint"; }
    }
}
