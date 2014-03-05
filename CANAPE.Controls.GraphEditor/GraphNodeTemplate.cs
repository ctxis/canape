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
using System.Drawing;

namespace CANAPE.Controls.GraphEditor
{
    /// <summary>
    /// Template for a graph node
    /// </summary>
    public sealed class GraphNodeTemplate
    {
        /// <summary>
        /// The shape type for the node
        /// </summary>
        public GraphNodeShape Shape { get; private set; }

        /// <summary>
        /// A template for the name
        /// </summary>
        public string NameTemplate { get; private set; }

        /// <summary>
        /// Width of the node
        /// </summary>
        public float Width { get; private set; }

        /// <summary>
        /// Height of the node
        /// </summary>
        public float Height { get; private set; }

        /// <summary>
        /// Background colour of the node
        /// </summary>
        public Color BackColor { get; private set; }

        /// <summary>
        /// Line colour surrounding the node
        /// </summary>
        public Color LineColor { get; private set; }

        /// <summary>
        /// The selected line color
        /// </summary>
        public Color SelectedLineColor { get; private set; }

        /// <summary>
        /// Text colour
        /// </summary>
        public Color TextColor { get; private set; }

        /// <summary>
        /// Hatched colour
        /// </summary>
        public Color HatchedColor { get; private set; }

        /// <summary>
        /// Type of tag data
        /// </summary>
        public Type TagType { get; private set; }

        private int _currCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameTemplate"></param>
        /// <param name="shape"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="backColor"></param>
        /// <param name="lineColor"></param>
        /// <param name="selectedLineColor"></param>
        /// <param name="textColor"></param>
        /// <param name="hatchedColor"></param>
        /// <param name="tagType"></param>
        public GraphNodeTemplate(string nameTemplate, 
            GraphNodeShape shape, 
            float width, 
            float height, 
            Color backColor, 
            Color lineColor, 
            Color selectedLineColor, 
            Color textColor, 
            Color hatchedColor, 
            Type tagType)
        {
            NameTemplate = nameTemplate;
            Shape = shape;
            Width = width;
            Height = height;
            BackColor = backColor;
            LineColor = lineColor;
            SelectedLineColor = selectedLineColor;
            TextColor = textColor;
            HatchedColor = hatchedColor;
            TagType = tagType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetNewName()
        {
            return String.Format(NameTemplate, _currCount++);
        }

    }
}
