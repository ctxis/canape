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
using System.Text;
using CANAPE.DataFrames.Filters;
using CANAPE.Utils;

namespace CANAPE.NodeFactories
{
    /// <summary>
    /// Factory class for a string data frame filter
    /// </summary>
    [Serializable]
    public class StringDataFrameFilterFactory : SetDataFrameFilterFactory
    {
        /// <summary>
        /// The match string
        /// </summary>
        protected string _match;

        /// <summary>
        /// The encoding to use if the selected nodes are not strings
        /// </summary>
        public enum FilterStringEncoding
        {
            /// <summary>
            /// 8 bit ascii encoding
            /// </summary>
            ASCII,
            /// <summary>
            /// UTF8
            /// </summary>
            UTF8,
            /// <summary>
            /// UTF7
            /// </summary>
            UTF7,
            /// <summary>
            /// UTF16 little endian
            /// </summary>
            UTF16_LE,
            /// <summary>
            /// UTF16 big endian
            /// </summary>
            UTF16_BE,
            /// <summary>
            /// UTF32 little endian
            /// </summary>
            UTF32_LE,
            /// <summary>
            /// UTF32 big endian
            /// </summary>
            UTF32_BE
        }

        /// <summary>
        /// The string encoding type
        /// </summary>
        [LocalizedDescription("StringDataFrameFilterFactory_StringEncodingDescription", typeof(Properties.Resources))]
        public FilterStringEncoding StringEncoding { get; set; }

        /// <summary>
        /// Overridable method to verify the match string (not used in base class)
        /// Should throw an exception is verification fails
        /// </summary>
        /// <param name="match">The string to match on</param>
        protected virtual void VerifyMatch(string match)
        {
            // Do nothing   
        }

        /// <summary>
        /// The string to match against
        /// </summary>
        [LocalizedDescription("StringDataFrameFilterFactory_MatchDescription", typeof(Properties.Resources))]
        public string Match 
        {
            get { return _match; }
            set
            {
                if (_match != value)
                {
                    VerifyMatch(value);
                    _match = value;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public StringDataFrameFilterFactory()
        {
            _match = "";
        }

        /// <summary>
        /// Whether the match is case sensitive (only applies to string matches)
        /// </summary>
        [LocalizedDescription("StringDataFrameFilterFactory_CaseSensitiveDescription", typeof(Properties.Resources))]
        public bool CaseSensitive { get; set; }

        /// <summary>
        /// Converts encoding to an encoding object
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        protected static Encoding GetEncodingFromType(FilterStringEncoding encoding)
        {
            Encoding ret;

            switch (encoding)
            {
                case FilterStringEncoding.ASCII:
                    ret = new BinaryEncoding();
                    break;
                case FilterStringEncoding.UTF16_BE:
                    ret = new UnicodeEncoding(true, false);
                    break;
                case FilterStringEncoding.UTF16_LE:
                    ret = new UnicodeEncoding(false, false);
                    break;
                case FilterStringEncoding.UTF32_BE:
                    ret = new UTF32Encoding(true, false);
                    break;
                case FilterStringEncoding.UTF32_LE:
                    ret = new UTF32Encoding(false, false);
                    break;
                case FilterStringEncoding.UTF8:
                    ret = new UTF8Encoding();
                    break;
                case FilterStringEncoding.UTF7:
                    ret = new UTF7Encoding();
                    break;
                default:
                    throw new ArgumentException(Properties.Resources.StringDataFrameFilterFactory_GetEncodigFromTypeException);
            }

            return ret;
        }

        /// <summary>
        /// Called to create the filter
        /// </summary>
        /// <returns>A string data frame filter</returns>
        protected override SetDataFrameFilter OnCreateSetFilter()
        {
            return new StringDataFrameFilter(Match, CaseSensitive, GetEncodingFromType(this.StringEncoding));                       
        }

        /// <summary>
        /// Returns the match string
        /// </summary>
        /// <returns></returns>
        protected override string ToDisplayString()
        {
            return String.Format("{0} '{1}'", base.ToDisplayString(), Match);         
        }
    }
}
