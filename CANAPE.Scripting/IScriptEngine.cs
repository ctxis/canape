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
using System.CodeDom;
using System.Security;

namespace CANAPE.Scripting
{
    /// <summary>
    /// Interface for a script engine
    /// </summary>
    [SecurityCritical]
    public interface IScriptEngine
    {
        /// <summary>
        /// Parse a script and get any errors associated with it
        /// </summary>
        /// <param name="container">The script code to assemble</param>
        /// <returns>An array of errors if any</returns>
        ScriptError[] Parse(ScriptContainer container);

        /// <summary>
        /// Get an instance of an object from the script engine
        /// </summary>
        /// <param name="classname">The fully qualified name of the class</param>
        /// <returns>The created instance, returns null if couldn't create it</returns>
        dynamic GetInstance(string classname);

        /// <summary>
        /// Get an instance of an object passing it specific constructor parameters
        /// </summary>
        /// <param name="classname">The classname to create</param>
        /// <param name="args">The parameters to pass to the constructor</param>
        /// <returns>The created instance, returns null if couldn't create it</returns>
        dynamic GetInstance(string classname, params object[] args);

        /// <summary>
        /// Invoke a public static method in this script
        /// </summary>
        /// <param name="classname">The name of the class</param>
        /// <param name="methodname">The name of the method</param>
        /// <param name="args">Any parameters</param>
        /// <returns>Returns the result of the invoke, null if no return value</returns>
        dynamic Invoke(string classname, string methodname, params object[] args);

        /// <summary>
        /// Generate code from a code compile unit
        /// </summary>
        /// <param name="unit">The code compile unit to generate code from</param>
        /// <returns>The generated code</returns>
        string GenerateCode(CodeCompileUnit unit);

        /// <summary>
        /// Get the types in this script engine
        /// </summary>
        /// <param name="types">List of types to find, if empty will return all types</param>
        /// <returns></returns>
        string[] GetTypes(params Type[] types);
    }
}
