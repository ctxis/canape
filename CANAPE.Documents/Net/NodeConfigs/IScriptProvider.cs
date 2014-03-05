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
using CANAPE.Documents.Data;

namespace CANAPE.Documents.Net.NodeConfigs
{
    /// <summary>
    /// Interface to provide a script property
    /// </summary>
    public interface IScriptProvider
    {
        /// <summary>
        /// The script document property
        /// </summary>
        ScriptDocument Script { get; }

        /// <summary>
        /// Instance configuration for script
        /// </summary>
        Dictionary<string, object> Config { get; }

        /// <summary>
        /// Set the provider as dirty for when the config is changed
        /// </summary>
        void SetDirty();
    }
}
