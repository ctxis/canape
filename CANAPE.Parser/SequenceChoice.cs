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
using CANAPE.Utils;
using NCalc;

namespace CANAPE.Parser
{
    /// <summary>
    /// Represents a single sequence choice
    /// </summary>
    [Serializable]
    public class SequenceChoice
    {
        private string _booleanExpression;
        private SequenceParserType _sequenceType;
        private SequenceChoiceMemberEntry _parentEntry;

        internal SequenceChoiceMemberEntry ParentEntry
        {
            get { return _parentEntry; }
        }

        public SequenceChoice(SequenceChoiceMemberEntry parentEntry)
        {
            _booleanExpression = "value == 0";
            _parentEntry = parentEntry;
        }

        [LocalizedDescription("SequenceChoice_BooleanExpressionDescription", typeof(Properties.Resources)),
                LocalizedCategory("Behavior")]
        public EvalExpression BooleanExpression
        {
            get
            {
                return new EvalExpression(_booleanExpression ?? String.Empty);
            }

            set
            {
                if (value.Expression != _booleanExpression)
                {
                    if (!String.IsNullOrWhiteSpace(value.Expression))
                    {
                        Expression.Compile(value.Expression, true);
                    }

                    _booleanExpression = value.Expression;
                }
            }
        }

        [TypeConverter(typeof(SequenceTypeConverter)), 
            LocalizedDescription("SequenceChoice_SequenceTypeDescription", typeof(Properties.Resources)),
                LocalizedCategory("Behavior")]
        public SequenceParserType SequenceType
        {
            get { return _sequenceType; }
            set
            {
                if (_sequenceType != value)
                {
                    _sequenceType = value;                    
                }
            }
        }

        public override string ToString()
        {
            return String.Format("{0} -> {1}", _booleanExpression, _sequenceType != null ? _sequenceType.Name : String.Empty);
        }
    }
}
