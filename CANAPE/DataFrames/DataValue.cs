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
using System.Linq.Expressions;

namespace CANAPE.DataFrames
{
     /// <summary>
    /// Class to hold a data value
    /// </summary>
    [Serializable]
    public abstract class DataValue : DataNode
    {        
        /// <summary>
        /// Indicates if the underlying binary representation is a fixed length
        /// </summary>
        public abstract bool FixedLength { get; }

        /// <summary>
        /// A .NET style format string for display, might not always be honoured
        /// </summary>
        public string FormatString { get; set; }

        /// <summary>
        /// Write to a stream
        /// </summary>
        /// <param name="stm">DataWriter object</param>
        public override void ToWriter(DataWriter stm)
        {
            stm.Write(ToArray());
        }

        /// <summary>
        /// Convert from a stream
        /// </summary>
        /// <param name="stm">DataReader object</param>        
        public override void FromReader(DataReader stm)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the value</param>
        protected DataValue(string name)
            : base(name)
        {            
        }

        /// <summary>
        /// Default ToString - Returns a basic display string
        /// </summary>
        /// <returns>The display string</returns>
        public override string ToString()
        {
            if (Value == null)
            {
                return "<null>";
            }
            else
            {
                if (String.IsNullOrWhiteSpace(FormatString))
                {
                    return Value.ToString();
                }
                else
                {
                    try
                    {
                        return String.Format(FormatString, Value);
                    }
                    catch (FormatException)
                    {
                        return Value.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Dynamic try conversion operation
        /// </summary>
        /// <param name="binder">The binder</param>
        /// <param name="result">The result</param>
        /// <returns>True if converted</returns>
        public override bool TryConvert(System.Dynamic.ConvertBinder binder, out object result)
        {
            try
            {
                if (binder.Type == typeof(DataValue))
                {
                    result = this;
                }
                else
                {
                    result = Convert.ChangeType(this.Value, binder.Type);
                }

                return true;
            }
            catch (InvalidCastException)
            {
            }

            return base.TryConvert(binder, out result);
        }        
    }
}
