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
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Fuzzer
{
    [Serializable]
    public class BaseStringFuzzerNodeConfig
    {
        private string _regex;

        [LocalizedDescription("BaseStringFuzzerNodeConfig_LogFuzzTextDescription", typeof(Properties.Resources)), Category("Logging")]
        public bool LogFuzzText { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_LogPacketsDescription", typeof(Properties.Resources)), Category("Logging")]
        public bool LogPackets { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_ColorDescription", typeof(Properties.Resources)), Category("Logging")]
        public ColorValue Color { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_FuzzStartDescription", typeof(Properties.Resources)), Category("Replacement")]
        public int FuzzStart { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_FuzzLengthDescription", typeof(Properties.Resources)), Category("Replacement")]
        public int FuzzLength { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_ConvertToBytesDescription", typeof(Properties.Resources))]
        public bool ConvertToBytes { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_BinaryEncodingDescription", typeof(Properties.Resources))]
        public BinaryStringEncoding BinaryEncoding { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_NoConversionDescription", typeof(Properties.Resources))]
        public bool NoConversion { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_RegexMatchDescription", typeof(Properties.Resources)), Category("Replacement")]
        public string RegexMatch { 
            get
            {
                return _regex; 
            }
            
            set
            {
                if (value != null)
                {
                    Regex r = new Regex(value);
                }

                _regex = value;
            }
        }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_CaseSensitiveDescription", typeof(Properties.Resources)), Category("Replacement")]
        public bool CaseSensitive { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_MultilineDescription", typeof(Properties.Resources)), Category("Replacement")]
        public bool Multiline { get; set; }

        [LocalizedDescription("BaseStringFuzzerNodeConfig_MaxReplacementsDescription", typeof(Properties.Resources)), Category("Replacement")]
        public int MaxReplacements { get; set; }

        public Regex GetMatcher()
        {
            RegexOptions opts = RegexOptions.None;

            if(!CaseSensitive)
            {
                opts |= RegexOptions.IgnoreCase;
            }

            if(Multiline)
            {
                opts |= RegexOptions.Multiline;
            }

            return new Regex(RegexMatch, opts);
        }

        public BaseStringFuzzerNodeConfig()
        {
            Color = new ColorValue(255, 255, 255);
            FuzzLength = -1;
            MaxReplacements = -1;
        }
    }

    public abstract class BaseStringFuzzerNode<T> : BasePipelineNodeWithPersist<T> where T : BaseStringFuzzerNodeConfig, new()
    {
        Encoding _encoding;
        Regex _re;

        protected abstract string DoFuzz(string value);

        private string FuzzString(string value)
        {            
            if (_re != null)
            {
                if (Config.MaxReplacements < 0)
                {
                    return _re.Replace(value, m => DoFuzz(m.Value));
                }
                else
                {
                    return _re.Replace(value, m => DoFuzz(m.Value), Config.MaxReplacements);
                }
            }
            else
            {
                return DoFuzz(value);
            }
        }

        protected override void OnInput(DataFrames.DataFrame frame)
        {
            DataNode[] nodes = frame.SelectNodes(SelectionPath);
            DataFrame preFuzzFrame = null;
            int nodeCount = nodes.Length;
            bool fuzzed = false;

            if (nodeCount > 0)
            {
                if (Config.LogPackets)
                {
                    preFuzzFrame = frame.CloneFrame();
                }
            }

            foreach (DataNode node in nodes)
            {
                DataValue value = node as DataValue;
                string fuzzString;
                
                if ((value != null) && (value.Value is string))
                {
                    fuzzString = value.Value;
                }
                else
                {
                    if (Config.NoConversion)
                    {
                        continue;
                    }

                    value = null;
                    fuzzString = _encoding.GetString(node.ToArray());
                }

                int fuzzLength = FuzzerUtils.GetFuzzLength(fuzzString.Length, Config.FuzzStart, Config.FuzzLength);
                string newString;
      
                if ((Config.FuzzStart == 0) && (fuzzLength == fuzzString.Length))
                {
                    newString = FuzzString(fuzzString);
                }
                else if ((Config.FuzzStart >= 0) && (fuzzLength > 0))
                {
                    string prefixString = fuzzString.Substring(0, Config.FuzzStart);
                    string suffixString = fuzzString.Substring(Config.FuzzStart + fuzzLength);

                    newString = String.Concat(prefixString, FuzzString(fuzzString.Substring(Config.FuzzStart, fuzzLength)), suffixString);
                }
                else
                {
                    continue;
                }

                if (newString != fuzzString)
                {
                    fuzzed = true;
                    if (Config.LogFuzzText)
                    {
                        LogInfo("Fuzzed string '{0}'", fuzzString);
                    }

                }
                
                if (value != null)
                {
                    value.Value = newString;
                }
                else
                {
                    node.ReplaceNode(_encoding.GetBytes(newString));
                }
            }

            if (nodeCount > 0)
            {
                if (Config.LogPackets)
                {
                    if (fuzzed)
                    {
                        Graph.DoLogPacket(String.Format("{0}: Pre-fuzz", Name), Config.Color, preFuzzFrame, Config.ConvertToBytes);
                        Graph.DoLogPacket(String.Format("{0}: Post-fuzz", Name), Config.Color, frame, Config.ConvertToBytes);
                    }
                }
            }

            WriteOutput(frame);
        }

        protected override void ValidateConfig(T config)
        {
            base.ValidateConfig(config);

            _encoding = GeneralUtils.GetEncodingFromType(config.BinaryEncoding);

            if (!String.IsNullOrWhiteSpace(config.RegexMatch))
            {
                _re = config.GetMatcher();
            }
        }
    }
}
