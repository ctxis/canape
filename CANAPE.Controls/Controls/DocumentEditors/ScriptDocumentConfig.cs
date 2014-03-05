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

namespace CANAPE.Controls.DocumentEditors
{
    /// <summary>
    /// Configuration for the script document editor
    /// </summary>
    [Serializable]
    public class ScriptDocumentControlConfig
    {
        /// <summary>
        /// Show spaces
        /// </summary>
        public bool ShowSpaces { get; set; }

        /// <summary>
        /// Show tabs
        /// </summary>
        public bool ShowTabs { get; set; }

        /// <summary>
        /// Convert all tabs to spaces
        /// </summary>
        public bool ConvertTabsToSpaces { get; set; }

        /// <summary>
        /// Hide line numbers
        /// </summary>
        public bool HideLineNumbers { get; set; }

        /// <summary>
        /// Show end of line markers
        /// </summary>
        public bool ShowEndofLineMarkers { get; set; }     
    }
}
