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
    /// 
    /// </summary>
    [Serializable]
    public class UnixDateTimeDataValue : DateTimeDataValue
    {   
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override byte[] OnToArray()
        {
            return BitConverter.GetBytes(Utils.GeneralUtils.ToUnixTime(_time));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        protected override void OnFromArray(byte[] data)
        {
            if (data.Length == 4)
            {
                throw new ArgumentException(CANAPE.Properties.Resources.UnixDateTimeDataValue_OnFromArray);
            }

            _time = Utils.GeneralUtils.FromUnixTime(BitConverter.ToInt32(data, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="littleEndian"></param>
        /// <param name="time"></param>
        public UnixDateTimeDataValue(string name, bool littleEndian, DateTime time) 
            : base(name, littleEndian, time)
        {            
        }
    }
}
