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
using System.Linq;
using System.Text;
using CANAPE.Utils;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// Equality comparer for data nodes
    /// </summary>
    public class DataNodeEqualityComparer : IEqualityComparer<DataNode>
    {
        private bool CompareKeys(DataKey x, DataKey y)
        {                     
            bool ret = true;

            if (x.SubNodes.Count != y.SubNodes.Count)
            {
                ret = false;
            }
            else
            {
                for (int i = 0; i < y.SubNodes.Count; ++i)
                {
                    if(!Equals(x.SubNodes[i], y.SubNodes[i]))
                    {                    
                        ret = false;
                        break;
                    }
                }
            }

            return ret;
        }

        private bool CompareValues(DataValue x, DataValue y)
        {
            Type xType = x.Value.GetType();

            if (xType == y.Value.GetType())
            {
                if (xType == typeof(byte[]))
                {
                    return GeneralUtils.CompareBytes((byte[])x.Value, (byte[])y.Value);
                }
                else
                {
                    return x.Value.Equals(y.Value);
                }
            }

            return false;
        }

        /// <summary>
        /// Compare two data nodes
        /// </summary>
        /// <param name="x">The left data node</param>
        /// <param name="y">The right data node</param>
        /// <returns>True if they are equal</returns>
        public bool Equals(DataNode x, DataNode y)
        {
            bool ret = false;

            if (x.Name == y.Name)
            {
                if ((x is DataKey) && (y is DataKey))
                {
                    ret = CompareKeys((DataKey)x, (DataKey)y);
                }
                else if ((x is DataValue) && (y is DataValue))
                {
                    ret = CompareValues((DataValue)x, (DataValue)y);
                }
            }

            return ret;
        }

        private int GetHashCodeKey(DataKey key)
        {
            int hash = 27;

            hash = (hash * 13) + key.Name.GetHashCode();

            foreach (DataNode node in key.SubNodes)
            {                
                hash = (hash * 13) + GetHashCode(node);                
            }

            return hash;
        }

        private int GetHashCodeValue(DataValue value)
        {
            byte[] bytes = value.Value as byte[];

            if (bytes != null)
            {
                int hash = 27;

                foreach (byte b in bytes)
                {                    
                    hash = (13 * hash) + b;                    
                }

                return hash;
            }
            else
            {
                return (int)value.Value.GetHashCode();
            }
        }

        /// <summary>
        /// Get the hash code of a data node
        /// </summary>
        /// <param name="obj">The data node</param>
        /// <returns>The hash code</returns>
        public int GetHashCode(DataNode obj)
        {
            int code = 0;

            if (obj is DataKey)
            {
                code = GetHashCodeKey((DataKey)obj);
            }
            else
            {
                code = GetHashCodeValue((DataValue)obj);
            }

            return (code * 13) + obj.Name.GetHashCode();
        }
    }
}
