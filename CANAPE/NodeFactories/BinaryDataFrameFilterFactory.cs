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
using System.ComponentModel;
using CANAPE.DataFrames.Filters;
using CANAPE.Utils;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// Factory for a binary data filter
    /// </summary>
    [Serializable]
    public class BinaryDataFrameFilterFactory : SetDataFrameFilterFactory
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BinaryDataFrameFilterFactory()
        {
            Match = new byte[0];
        }

        /// <summary>
        /// Match byte array
        /// </summary>
        [LocalizedDescription("BinaryDataFrameFilterFactory_MatchDescription", typeof(Properties.Resources))]
        public byte[] Match
        {
            get; set;
        }

        /// <summary>
        /// Method to create the filter
        /// </summary>
        /// <returns></returns>
        protected override SetDataFrameFilter OnCreateSetFilter()
        {
            return new BinaryDataFrameFilter(Match);
        }

        /// <summary>
        /// Method to return displayable string
        /// </summary>
        /// <returns></returns>
        protected override string ToDisplayString()
        {
            return String.Format("{0} '{1}'", base.ToDisplayString(), GeneralUtils.EscapeString(new BinaryEncoding().GetString(Match)));
        }
    }
}
