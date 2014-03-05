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

namespace CANAPE.DataFrames
{    
    /// <summary>
    /// Base class for a primitive data value
    /// </summary>
    [Serializable]
    public abstract class PrimitiveDataValue : DataValue
    {
        /// <summary>
        /// Indicates whether to swap bytes or not
        /// </summary>
        private bool _swapBytes;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract byte[] OnToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        protected abstract void OnFromArray(byte[] data);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override byte[] ToArray()
        {
            byte[] ret = null;

            ret = OnToArray();

            if (_swapBytes)
            {
                return ret.Reverse().ToArray();
            }
            else
            {
                return ret;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public override void FromArray(byte[] data)
        {
            if (_swapBytes)
            {
                OnFromArray(data.Reverse().ToArray());
            }
            else
            {
                OnFromArray(data);
            }

            OnModified();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the value</param>
        /// <param name="littleEndian">Set whether the value is little endian</param>
        protected PrimitiveDataValue(string name, bool littleEndian) : base(name)
        {
            LittleEndian = littleEndian;
        }

        /// <summary>
        /// Get or set endian mode for data value
        /// </summary>
        public bool LittleEndian
        {
            get
            {
                if (BitConverter.IsLittleEndian)
                {
                    return !_swapBytes;
                }
                else
                {
                    return _swapBytes;
                }
            }

            set
            {
                if (BitConverter.IsLittleEndian)
                {
                    _swapBytes = !value;
                }
                else
                {
                    _swapBytes = value;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the value</param>
        protected PrimitiveDataValue(string name)
            : this(name, true)
        {

        } 
    }
}
