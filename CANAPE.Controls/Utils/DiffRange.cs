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
using System.Drawing;

namespace CANAPE.Utils
{
    internal class DiffRange
    {
        public enum DiffType
        {
            None,
            Added,
            Deleted,
            Modified,
        }

        /// <summary>
        /// Start position in the left frame 
        /// </summary>
        public long LeftStartPos { get; set; }

        /// <summary>
        /// Start position in the right frame
        /// </summary>
        public long RightStartPos { get; set; }

        /// <summary>
        /// Length of difference
        /// </summary>
        public long LeftLength { get; set; }

        public long RightLength { get; set; }

        public DiffType LeftType { get; set; }

        public DiffType RightType { get; set; }

        public static Color GetColor(DiffType type)
        {
            switch (type)
            {
                case DiffType.Added:
                    return Color.Cyan;
                case DiffType.Deleted:
                    return Color.Red;
                case DiffType.Modified:
                    return Color.Yellow;
            }

            return Color.White;
        }

        public DiffRange(long leftStartPos, long rightStartPos, long leftlength, long rightlength)
        {
            LeftStartPos = leftStartPos;
            RightStartPos = rightStartPos;
            LeftLength = leftlength;
            RightLength = rightlength;

            if (rightlength == leftlength)
            {
                LeftType = DiffType.Modified;
                RightType = DiffType.Modified;
            }
            else
            {
                RightType = DiffType.Added;
                LeftType = DiffType.Deleted;
            }
        }

        public static IEnumerable<DiffRange> BuildDifferences<T>(IList<T> left, IList<T> right, IEqualityComparer<T> comparer)
        {
            List<DiffRange> diffs = new List<DiffRange>();

            // Need to do this in the background
            foreach (DiffItem item in Diff.DiffList(left, right, comparer))
            {
                if (item.deletedA == item.insertedB)
                {
                    diffs.Add(new DiffRange(item.StartA, item.StartB, item.deletedA, item.insertedB));
                }
                else if (item.deletedA < item.insertedB)
                {
                    // In this case we have initial modified data then added data in right hand side
                    if (item.deletedA > 0)
                    {
                        diffs.Add(new DiffRange(item.StartA, item.StartB, item.deletedA, item.deletedA));
                    }
                    diffs.Add(new DiffRange(item.StartA + item.deletedA, item.StartB + item.deletedA, 0, item.insertedB - item.deletedA));
                }
                else
                {
                    // In this case we have initially modified data then deleted in left hand side
                    if (item.insertedB > 0)
                    {
                        diffs.Add(new DiffRange(item.StartA, item.StartB, item.insertedB, item.insertedB));
                    }
                    diffs.Add(new DiffRange(item.StartA + item.insertedB, item.StartB + item.insertedB, item.deletedA - item.insertedB, 0));
                }
            }

            return diffs;
        }
    }

}
