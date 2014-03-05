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
    [Serializable, Obsolete("Use DynamicStringDataKey2 instead")]
    public class DynamicStringDataKey : DynamicArrayDataKey
    {
        /// <summary>
        /// Convert from an array
        /// </summary>
        /// <param name="data">The data array</param>        
        public override void FromArray(byte[] data)
        {
            IDataStringParser convert = (IDataStringParser)CreateConverter();
            BinaryEncoding encoding = new BinaryEncoding();

            try
            {
                convert.FromString(encoding.GetString(data), this, GetLogger());
            }
            catch (Exception ex)
            {
                GetLogger().LogException(ex);
            }
        }

        /// <summary>
        /// Converts key to an array
        /// </summary>
        /// <returns>The data</returns>
        public override byte[] ToArray()
        {
            IDataStringParser convert = (IDataStringParser)CreateConverter();
            BinaryEncoding encoding = new BinaryEncoding();
            byte[] ret = new byte[0];

            try
            {
                ret = encoding.GetBytes(convert.ToString(this, GetLogger()));
            }
            catch (Exception ex)
            {
                GetLogger().LogException(ex);
            }

            return ret;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of key</param>
        /// <param name="engine">Script engine</param>
        /// <param name="script">Script code</param>
        /// <param name="classname">Class name</param>
        /// <param name="referencedAssemblies">Referenced assemblies</param>
        /// <param name="logger">The logger</param>
        /// <param name="state">The state of the key</param>
        /// <param name="enableDebug">Whether to enable debugging</param>
        public DynamicStringDataKey(string name, string engine, string script, 
            bool enableDebug, string classname, IEnumerable<Assembly> referencedAssemblies, Logger logger, object state)
            : base(name, engine, script, enableDebug, classname, referencedAssemblies, logger, state)
        {
        }
    }
}
