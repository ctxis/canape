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

using System.Drawing;

namespace CANAPE.Utils
{
    /// <summary>
    /// Simple class to convert between out colour structure and System.Drawing's one
    /// </summary>
    public static class ColorValueConverter
    {
        /// <summary>
        /// Convert to to a drawing color
        /// </summary>
        /// <param name="c">The colorvalue to convert</param>
        /// <returns>The color as a Color structure, note alpha is always set to 255</returns>
        public static Color ToColor(this ColorValue c)
        {
            return Color.FromArgb(255, c.R, c.G, c.B);
        }

        /// <summary>
        /// Convert from a drawing color
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static ColorValue FromColor(this Color c)
        {
            return new ColorValue(c.R, c.G, c.B, c.A);
        }
    }
}
