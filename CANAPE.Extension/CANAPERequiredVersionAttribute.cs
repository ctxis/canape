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
using CANAPE.Utils;

namespace CANAPE.Extension
{
    /// <summary>
    /// An attribute which indicates the required canape version
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=false)]
    public sealed class CANAPERequiredVersionAttribute : Attribute
    {
        private Version _minimum;
        private Version _maximum;
       
        public bool MatchedVersion()
        {
            Version curr = GeneralUtils.GetCanapeVersion();
            
            if(curr.CompareTo(_minimum) < 0)
            {
                return false;
            }         

            if (_maximum != null)
            {
                if (curr.CompareTo(_maximum) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="minimum">Minimum version as a string (e.g. 1.3)</param>
        /// <param name="maximum">Maximum version as a string (e.g. 1.3)</param>
        public CANAPERequiredVersionAttribute(string minimum, string maximum)
        {
            _minimum = Version.Parse(minimum);
            if (!String.IsNullOrWhiteSpace(maximum))
            {
                _maximum = Version.Parse(maximum);
            }
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="minimum">Minimum version as a string (e.g. 1.3)</param>        
        public CANAPERequiredVersionAttribute(string minimum) : this(minimum, null)
        {
        }
    }
}
