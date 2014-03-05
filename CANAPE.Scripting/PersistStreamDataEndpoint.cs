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

using System.IO;
using CANAPE.DataAdapters;
using CANAPE.Utils;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Base class for a stream based endpoint with persistence
    /// </summary>
    public abstract class PersistStreamDataEndpoint<T, R> : BasePersistDataEndpointRef<T, R>
        where R : class
        where T : class, R, new()
    {
        /// <summary>
        /// Run method
        /// </summary>
        /// <param name="adapter">The data adapter to use</param>
        /// <param name="logger">The logger to use</param>
        public override void Run(DataAdapters.IDataAdapter adapter, Utils.Logger logger)
        {
            DataAdapterToStream stm = new DataAdapterToStream(adapter);

            OnRun(stm, logger);
        }

        /// <summary>
        /// Abstract method for derived classes to implement
        /// </summary>
        /// <param name="stm">The binary stream</param>
        /// <param name="logger">The logger to use</param>
        protected abstract void OnRun(Stream stm, Logger logger);
    }
}
