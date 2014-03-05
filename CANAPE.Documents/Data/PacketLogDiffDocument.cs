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
using CANAPE.Documents.Net;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// A document which represents a packet log difference
    /// </summary>
    [Serializable]
    public sealed class PacketLogDiffDocument : BaseDocumentObject
    {
        private LogPacketCollection _left;
        private LogPacketCollection _right;

        /// <summary>
        /// Constructor
        /// </summary>
        public PacketLogDiffDocument()
        {
            _left = new LogPacketCollection();
            _right = new LogPacketCollection();
        }

        /// <summary>
        /// Left log
        /// </summary>
        public LogPacketCollection Left { get { return _left; } }

        /// <summary>
        /// Right log
        /// </summary>
        public LogPacketCollection Right { get { return _right; } }

        private void SetupCollections()
        {
            _left.CollectionChanged += col_CollectionChanged;
            _left.FrameModified += col_FrameModified;
            _right.CollectionChanged += col_CollectionChanged;
            _right.FrameModified += col_FrameModified;
        }

        void col_FrameModified(object sender, EventArgs e)
        {
            Dirty = true;
        }

        void col_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        /// On deserialization
        /// </summary>
        protected override void OnDeserialization()
        {
            base.OnDeserialization();
            SetupCollections();
        }

        /// <summary>
        /// Default name
        /// </summary>
        public override string DefaultName
        {
            get { return Properties.Resources.PacketLogDiffDocument_DefaultName; }
        }
    }
}
