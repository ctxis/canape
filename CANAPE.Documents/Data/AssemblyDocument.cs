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
using System.Reflection;
using System.IO;
using CANAPE.Scripting.DotNet;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// A document to contain a binary assembly
    /// </summary>
    [Serializable]
    public class AssemblyDocument : ScriptDocument
    {
        [NonSerialized]
        Assembly _asm;

        AssemblyName _asmName;

        /// <summary>
        /// Serialize the name
        /// </summary>
        string _name;

        /// <summary>
        /// Data
        /// </summary>
        byte[] _assemblyData;

        /// <summary>
        /// 
        /// </summary>
        string _originalPath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assemblyData">The binary data of the assembly</param>
        /// <param name="originalPath">Original path, not required</param>
        /// <exception cref="BadImageFormatException">Thrown if the assembly data is invalid</exception>
        public AssemblyDocument(byte[] assemblyData, string originalPath) : base("assembly")
        {
            ReloadAssembly(assemblyData, originalPath);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Original path, not required</param>
        /// <exception cref="BadImageFormatException">Thrown if the assembly data is invalid</exception>
        /// <exception cref="IOException">Thrown if cannot read file</exception>
        public AssemblyDocument(string path) 
            : this(File.ReadAllBytes(path), path)
        {
        }

        /// <summary>
        /// Reload the assembly from a byte array
        /// </summary>
        /// <param name="data">The data to reload</param>
        /// <param name="originalPath">Original path, not required</param>
        /// <exception cref="BadImageFormatException">Thrown if the assembly data is invalid</exception>
        public void ReloadAssembly(byte[] data, string originalPath)
        {
            Assembly asm = GetAssembly(data);

            _asm = asm;
            _assemblyData = data;
            _script.Script = _asm != null ? GetName().FullName : null;
            _originalPath = originalPath;
        }
        
        /// <summary>
        /// Get or set the binary assembly data
        /// </summary>
        public byte[] AssemblyData
        {
            get { return _assemblyData; }
        }

        /// <summary>
        /// Represents the original path this assembly was loaded from
        /// </summary>
        public string OriginalPath
        {
            get { return _originalPath; }
        }

        /// <summary>
        /// Get the name of the assembly
        /// </summary>
        /// <returns>The name, null if no assembly</returns>
        [Obsolete]
        public string GetAssemblyName()
        {
            if (_name == null)
            {
                Assembly asm = GetAssembly();
                if (asm != null)
                {
                    _name = asm.GetName().Name;
                }
            }

            return _name;
        }        

        /// <summary>
        /// Get the assembly name
        /// </summary>
        /// <returns>The assembly name, null if no assembly</returns>
        public AssemblyName GetName()
        {
            if (_asmName == null)
            {
                Assembly asm = GetAssembly();
                if (asm != null)
                {
                    _asmName = asm.GetName();
                }
            }

            return _asmName;
        }

        private Assembly GetAssembly(byte[] data)
        {
            if (data == null)
            {
                throw new BadImageFormatException("Assembly data can not be null");
            }

            Assembly asm = Assembly.Load(data);

            DotNetScriptEngine.RegisterInMemoryAssembly(asm.GetName(), data);

            return asm;
        }

        /// <summary>
        /// Get the loaded assembly
        /// </summary>
        /// <returns>The assembly</returns>
        /// <exception cref="BadImageFormatException">Thrown if the assembly data is invalid</exception>
        public Assembly GetAssembly()
        {
            if (_asm == null)
            {
                _asm = GetAssembly(_assemblyData);
            }

            return _asm;
        }

        /// <summary>
        /// Get default document name
        /// </summary>
        public override string DefaultName
        {
            get { return "Assembly"; }
        }
    }
}
