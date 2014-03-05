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
    /// 
    /// </summary>
    [Serializable]
    public class DynamicArrayDataKey2 : DynamicDataKey<IDataArrayParser>
    {        
        /// <summary>
        /// From array method
        /// </summary>
        /// <param name="data">The data as an array</param>        
        public override void FromArray(byte[] data)
        {
            IDataArrayParser convert = CreateConverter();

            try
            {
                if (convert != null)
                {
                    convert.FromArray(data, this, GetLogger());
                }
            }
            catch (Exception ex)
            {
                GetLogger().LogException(ex);
            }
        }

        /// <summary>
        /// To array method
        /// </summary>
        /// <returns>The key as an array</returns>
        public override byte[] ToArray()
        {
            IDataArrayParser convert = (IDataArrayParser)CreateConverter();

            byte[] ret = new byte[0];

            if (convert != null)
            {
                ret = convert.ToArray(this, GetLogger());
            }

            return ret;
        }

        /// <summary>
        /// To writer method
        /// </summary>
        /// <param name="stm">The data writer</param>
        public override void ToWriter(DataWriter stm)
        {
            stm.Write(ToArray());   
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the node</param>
        /// <param name="container">The script container</param>
        /// <param name="logger">Logger object</param>
        /// <param name="state">Object state</param>
        public DynamicArrayDataKey2(string name, DynamicScriptContainer container,
            Logger logger, object state)
            : base(name, container, logger, state)
        {
        }        
    }
}
