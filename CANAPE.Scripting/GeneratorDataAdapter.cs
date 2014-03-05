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

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Scripting
{
    /// <summary>
    /// A data adapter which acts as a generator 
    /// </summary>
    public sealed class GeneratorDataAdapter : EnumerableDataAdapter
    {
        private IDataGenerator _generator;
        private LockedQueue<DataFrame> _inputQueue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="generator">The generator to use</param>
        /// <param name="token">A cancellation token</param>
        public GeneratorDataAdapter(IDataGenerator generator, CancellationToken token)
        {
            _generator = generator;
            _inputQueue = new LockedQueue<DataFrame>(-1, token);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="generator">The generator to use</param>
        public GeneratorDataAdapter(IDataGenerator generator)
            : this(generator, CancellationToken.None)
        {
        }

        /// <summary>
        /// Overriden GetFrames
        /// </summary>
        /// <returns>The enumeration of frames</returns>
        protected sealed override IEnumerable<DataFrame> GetFrames()
        {
            foreach (object obj in _generator.Generate(_inputQueue))
            {                
                if (obj is DataFrame)
                {
                    yield return (DataFrame)obj;
                }
                else if(obj is string)
                {
                    yield return new DataFrame((string)obj);
                }
                else if(obj is byte[])
                {
                    yield return new DataFrame((byte[])obj);
                }
                else if(obj is IDictionary)
                {
                    yield return new DataFrame((IDictionary)obj);
                }
                else if (obj is DataKey)
                {
                    yield return new DataFrame((DataKey)obj);
                }
            }
        }

        /// <summary>
        /// Overridden write method
        /// </summary>
        /// <param name="data">The data frame to write</param>
        public override void Write(DataFrame data)
        {
            _inputQueue.Enqueue(data);
        }

        /// <summary>
        /// Overridden dispose method
        /// </summary>
        /// <param name="disposing">True if disposing</param>
        protected override void OnDispose(bool disposing)
        {
            _inputQueue.Stop();
        }
    }
}
