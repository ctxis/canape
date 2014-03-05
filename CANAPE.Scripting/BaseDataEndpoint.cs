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

using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Base endpoint class
    /// </summary>
    public abstract class BaseDataEndpoint : PersistNodeImpl<DynamicConfigObject, dynamic>, IDataEndpoint
    {
        #region IDataEndpoint Members

        /// <summary>
        /// Run the endpoint
        /// </summary>
        /// <param name="adapter">The data adapter</param>
        /// <param name="logger">The logger to use</param>
        public abstract void Run(DataAdapters.IDataAdapter adapter, Utils.Logger logger);

        /// <summary>
        /// Description
        /// </summary>
        public abstract string Description
        {
            get;
        }

        /// <summary>
        /// The meta dictionary
        /// </summary>
        public Nodes.MetaDictionary Meta
        {
            get;
            set;
        }

        /// <summary>
        /// The global meta
        /// </summary>
        public Nodes.MetaDictionary GlobalMeta
        {
            get;
            set;
        }


        #endregion
    }
}
