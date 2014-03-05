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

using System.Collections.Generic;
using System.Threading;
using CANAPE.DataFrames;
using CANAPE.Nodes;

namespace CANAPE.Utils
{
    internal class ParseWithPipelineNode : BasePipelineNode
    {
        public List<DataFrame> Frames { get; private set; }
        public AutoResetEvent EventFlag { get; private set; }

        protected override void OnInput(DataFrames.DataFrame frame)
        {
            Frames.Add(frame);
        }

        protected override bool OnShutdown()
        {
            EventFlag.Set();
            return false;
        }

        public ParseWithPipelineNode()
        {
            Frames = new List<DataFrame>();
            EventFlag = new AutoResetEvent(false);
        }

        protected override void Dispose(bool disposing)
        {
            if(EventFlag != null)
            {
                EventFlag.Dispose();
                EventFlag = null;
            }
        }
    }
}
