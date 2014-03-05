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
using System.Reflection;
using System.Runtime.Serialization;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Scripting
{
    /// <summary>
    /// A datakey which is implemented by a dynamic stream object
    /// </summary>
    [Serializable]
    public class DynamicStreamDataKey2 : DynamicDataKey<IDataStreamParser>
    {        
        /// <summary>
        /// Create a key from a DataReader
        /// </summary>
        /// <param name="stm">The data reader</param>
        /// <remarks>This method could throw almost any exception</remarks>
        /// <exception cref="System.IO.EndOfStreamException">Throw when end of stream reached</exception>
        public override void FromReader(DataReader stm)
        {
            IDataStreamParser convert = CreateConverter();

            convert.FromReader(stm, this, GetLogger());
        }

        /// <summary>
        /// Write the key to a DataWriter
        /// </summary>
        /// <remarks>This method could throw almost any exception</remarks>
        /// <param name="stm">The DataWriter object</param>
        public override void ToWriter(DataWriter stm)
        {
            IDataStreamParser convert = CreateConverter();

            convert.ToWriter(stm, this, GetLogger());
        } 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the node</param>
        /// <param name="container">The script container</param>
        /// <param name="logger">Logger object</param>
        /// <param name="state">Object state</param>
        public DynamicStreamDataKey2(string name, DynamicScriptContainer container, Logger logger, object state)
            : base(name, container, logger, state)
        {            
        }               
    }
}
