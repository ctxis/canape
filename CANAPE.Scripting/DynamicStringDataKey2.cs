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
using CANAPE.Utils;

namespace CANAPE.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DynamicStringDataKey2 : DynamicDataKey<IDataStringParser>
    {
        /// <summary>
        /// Convert to a data string
        /// </summary>
        /// <returns></returns>
        public override string ToDataString()
        {
            IDataStringParser convert = (IDataStringParser)CreateConverter();            

            try
            {
                return convert.ToString(this, GetLogger());                
            }
            catch (Exception ex)
            {
                GetLogger().LogException(ex);
            }

            // Return an empty string if errored
            return String.Empty;
        }

        /// <summary>
        /// Convert from a data string
        /// </summary>
        /// <param name="str">The data string</param>
        public override void FromDataString(string str)
        {
            IDataStringParser convert = (IDataStringParser)CreateConverter();

            try
            {
                convert.FromString(str, this, GetLogger());
            }
            catch (Exception ex)
            {
                GetLogger().LogException(ex);
            }
        }

        /// <summary>
        /// Convert from an array
        /// </summary>
        /// <param name="data">The data array</param>        
        public override void FromArray(byte[] data)
        {
            FromDataString(BinaryEncoding.Instance.GetString(data));
        }

        /// <summary>
        /// Converts key to an array
        /// </summary>
        /// <returns>The data</returns>
        public override byte[] ToArray()
        {
            return BinaryEncoding.Instance.GetBytes(ToDataString());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of key</param>        
        /// <param name="container">The script container</param>
        /// <param name="logger">The logger</param>
        /// <param name="state">The state of the key</param>        
        public DynamicStringDataKey2(string name, DynamicScriptContainer container, Logger logger, object state)
            : base(name, container, logger, state)
        {
        }
    }
}
