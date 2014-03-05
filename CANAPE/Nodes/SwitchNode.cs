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

using CANAPE.DataFrames;
using CANAPE.Utils;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;

namespace CANAPE.Nodes
{
    /// <summary>
    /// A switch node implementation
    /// </summary>
    public class SwitchNode : BasePipelineNode
    {        
        private bool _dropUnknown;
        private SwitchNodeSelectionMode _mode;
        private Dictionary<string, Regex> _regexs;

        /// <summary>
        /// Constructor
        /// </summary>        
        /// <param name="dropUnknown">Whether to drop unknown paths</param>
        /// <param name="mode">The selection mode</param>
        public SwitchNode(bool dropUnknown, SwitchNodeSelectionMode mode)
        {     
            _dropUnknown = dropUnknown;
            _mode = mode;
            _regexs = new Dictionary<string, Regex>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        protected override void OnInput(DataFrames.DataFrame frame)
        {
            bool writtenOutput = false;

            DataNode node = SelectSingleNode(frame);

            if (node != null)
            {
                string name = node.ToString();

                if (_mode == SwitchNodeSelectionMode.ExactMatch)
                {
                    if (HasOutput(name))
                    {
                        WriteOutput(frame, name);
                        writtenOutput = true;
                    }
                }
                else
                {
                    foreach (OutputNode output in _output)
                    {
                        try
                        {
                            if (!String.IsNullOrEmpty(output.PathName))
                            {
                                Regex r;

                                if (_regexs.ContainsKey(output.PathName))
                                {
                                    r = _regexs[output.PathName];
                                }
                                else
                                {
                                    r = _mode == SwitchNodeSelectionMode.RegexMatch
                                        ? new Regex(output.PathName, RegexOptions.Multiline | RegexOptions.IgnoreCase)
                                        : GeneralUtils.GlobToRegex(output.PathName);
                                    _regexs[output.PathName] = r;
                                }

                                if (r.IsMatch(name))
                                {
                                    WriteOutput(frame, output.PathName);
                                    writtenOutput = true;
                                    break;
                                }
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            LogException(ex);
                        }
                    }
                }
            }

            if (!writtenOutput && !_dropUnknown)
            {
                WriteOutput(frame);
            }
        }
    }
}
