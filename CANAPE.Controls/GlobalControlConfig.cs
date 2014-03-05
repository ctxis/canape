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
namespace CANAPE.Controls
{
    /// <summary>
    /// Enum to describe the confirmation mode of the packet log viewer when closing
    /// </summary>
    public enum PacketLogConfirmMode
    {
        /// <summary>
        /// Confirm if any modified packets are open
        /// </summary>
        ConfirmSaveOnClose,

        /// <summary>
        /// No confirmation, always save modified packets
        /// </summary>
        NoConfirmAutoSave,

        /// <summary>
        /// No confirmation, always discard modified packets
        /// </summary>
        NoConfirmAutoRevert
    }

    /// <summary>
    /// Global control configuration
    /// </summary>
    public static class GlobalControlConfig
    {        
        /// <summary>
        /// Specify the new style log viewer should be used
        /// </summary>
        public static bool NewStyleLogViewer { get; set; }

        /// <summary>
        /// Require packet log clear confirmation
        /// </summary>
        public static bool RequirePacketLogClearConfirmation { get; set; }

        /// <summary>
        /// Require event log clear confirmation
        /// </summary>
        public static bool RequireEventLogClearConfirmation { get; set; }

        /// <summary>
        /// Confirmation mode for packet log form
        /// </summary>
        public static PacketLogConfirmMode LogConfirmMode { get; set; }

        /// <summary>
        /// Open the find dialog in a new window
        /// </summary>
        public static bool OpenLogFindInWindow { get; set; }

        /// <summary>
        /// Enable C# Code Completion
        /// </summary>
        public static bool EnableCsCodeCompletion { get; set; }

        /// <summary>
        /// Font for script editor
        /// </summary>
        public static Font ScriptEditorFont { get; set; }

        static GlobalControlConfig()
        {            
            RequirePacketLogClearConfirmation = true;
            RequireEventLogClearConfirmation = true;
        }
    }
}
