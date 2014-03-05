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

using System.CodeDom;

namespace CANAPE.Parser
{
    // Interface for objects which support direct reading and writing expressions
    public interface IMemberReaderWriter
    {
        CodeExpression GetReaderExpression(CodeExpression reader);        
        CodeExpression GetWriterExpression(CodeExpression writer, CodeExpression obj);
    }
}
