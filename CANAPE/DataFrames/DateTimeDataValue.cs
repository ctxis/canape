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
    /// A data value to hold a date time structure
    /// </summary>
    [Serializable]
    public abstract class DateTimeDataValue : PrimitiveDataValue
    {
        /// <summary>
        /// The internal datetime
        /// </summary>
        protected DateTime _time;
        
        /// <summary>
        /// 
        /// </summary>
        public override dynamic Value
        {
            get
            {
                return _time;
            }
            set
            {
                if (value is DateTime)
                {
                    _time = (DateTime)value;
                }
                else if (value is long)
                {
                    _time = DateTime.FromFileTimeUtc((long)value);
                }
                else if (value is int)
                {
                    _time = Utils.GeneralUtils.FromUnixTime((int)value);
                }
                else
                {
                    _time = DateTime.Parse(value.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool FixedLength
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {           
            return _time.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="littleEndian"></param>
        /// <param name="time"></param>
        protected DateTimeDataValue(string name, bool littleEndian, DateTime time)
            : base(name, littleEndian)
        {
            _time = time;
        }
    }
}
