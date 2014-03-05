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

using System.Collections.Generic;
using CANAPE.Utils;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// Equality comparer for a data frame
    /// </summary>
    public sealed class DataFrameEqualityComparer : IEqualityComparer<DataFrame>
    {
        private DataNodeEqualityComparer _nodeComparer;

        private static DataFrameEqualityComparer _default;

        /// <summary>
        /// Get default comparer
        /// </summary>
        public static DataFrameEqualityComparer Default
        {
            get
            {
                if(_default == null)
                {
                    _default = new DataFrameEqualityComparer();
                }

                return _default;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DataFrameEqualityComparer()
        {
            _nodeComparer = new DataNodeEqualityComparer();
        }

        /// <summary>
        /// Equals method
        /// </summary>
        /// <param name="x">First frame</param>
        /// <param name="y">Second frame</param>
        /// <returns>True if equals</returns>
        public bool Equals(DataFrame x, DataFrame y)
        {           
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            else 
            {
                if (x.IsBasic)
                {
                    if (y.IsBasic)
                    {
                        return GeneralUtils.CompareBytes(x.ToArray(), y.ToArray());
                    }
                }
                else if ((x.Root != null) && (y.Root != null))
                {
                    return _nodeComparer.Equals(x.Root, y.Root); 
                }
            }

            return false; 
        }

        /// <summary>
        /// Get the hash code of the data frame
        /// </summary>
        /// <param name="obj">The dataframe object</param>
        /// <returns>The hash code</returns>
        public int GetHashCode(DataFrame obj)
        {
            if (obj.IsBasic)
            {
                return GeneralUtils.GetBytesHashCode(obj.ToArray());
            }
            else if (obj.Root != null)
            {
                return _nodeComparer.GetHashCode(obj.Root);
            }
            else
            {
                return 0;
            }
        }
    }
}
