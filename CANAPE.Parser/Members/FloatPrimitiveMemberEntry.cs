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

namespace CANAPE.Parser
{
    [Serializable]
    public sealed class FloatPrimitiveMemberEntry : NumericPrimitiveMemberEntry
    {
        private static Type CheckType(Type t)
        {
            if ((t != typeof(float)) && (t != typeof(double)))
            {
                throw new ArgumentException(CANAPE.Parser.Properties.Resources.FloatPrimitiveMemberEntry_InvalidType);
            }

            return t;
        }

        public FloatPrimitiveMemberEntry(string name, Type type, Endian endian)
            : base(name, CheckType(type), endian)
        {
        }

        protected override string GetFormatString()
        {
            return null;
        }
    }
}
