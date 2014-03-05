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

namespace CANAPE.DataFrames
{
    /// <summary>
    /// A data value which is backed by a dynamic object, should only be used in 
    /// specific circumstances as it will likely break things
    /// </summary>
    [Serializable]
    public sealed class DynamicDataValue : DataValue
    {
        private object _object;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the value</param>
        public DynamicDataValue(string name) : base(name)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the value</param>
        /// <param name="value">The value</param>
        public DynamicDataValue(string name, object value)
            : base(name)
        {
            _object = value;
        }

        /// <summary>
        /// Returns a dynamic value
        /// </summary>
        public override dynamic Value
        {
            get
            {
                return _object;
            }
            set
            {                
                if (_object != value)
                {
                    _object = value;
                    OnModified();
                }
            }
        }

        /// <summary>
        /// Always false as we do not know
        /// </summary>
        public override bool FixedLength
        {
            get { return false; }
        }

        /// <summary>
        /// Always throws NotImplementedException
        /// </summary>
        /// <returns>N/A</returns>
        public override byte[] ToArray()
        {
            return new byte[0];
        }

        /// <summary>
        /// Always throws NoImplementedException
        /// </summary>
        /// <param name="data">N/A</param>
        public override void FromArray(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
