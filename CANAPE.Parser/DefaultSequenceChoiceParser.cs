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
using System.Runtime.InteropServices;
using CANAPE.DataFrames;

namespace CANAPE.Parser
{
    [Guid("E8C2E07E-ECEA-4A65-B044-A2D171B9B3AF")]
    public class DefaultSequenceChoiceParser : IStreamTypeParser
    {
        public byte[] Bytes;

        public void FromStream(DataReader reader, StateDictionary state, Utils.Logger logger)
        {
            Bytes = reader.ReadToEnd();
        }

        public void ToStream(DataFrames.DataWriter writer, StateDictionary state, Utils.Logger logger)
        {
            if (Bytes != null)
            {
                writer.Write(Bytes);
            }
        }
    }
}
