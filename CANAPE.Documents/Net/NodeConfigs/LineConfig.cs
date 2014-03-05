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

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LineConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseNodeConfig SourceNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BaseNodeConfig DestNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool BiDirection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PathName { get; set; }
        /// <summary>
        /// Weak path
        /// </summary>
        public bool WeakPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="biDirection"></param>
        /// <param name="pathName"></param>
        /// <param name="weak"></param>
        public LineConfig(BaseNodeConfig source, BaseNodeConfig dest, bool biDirection, string pathName, bool weak)
        {
            SourceNode = source;
            DestNode = dest;
            BiDirection = biDirection;
            PathName = pathName;
            WeakPath = weak;
        }
    }
}
