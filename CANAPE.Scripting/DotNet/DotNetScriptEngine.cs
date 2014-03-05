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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CANAPE.Scripting.DotNet
{
    /// <summary>
    /// .NET CodeDomProvider script engine
    /// </summary>
    public class DotNetScriptEngine : AssemblyScriptEngine
    {        
        private CodeDomProvider _provider;
        private string _options;

        private static Dictionary<string, byte[]> _memoryAssemblyCache = new Dictionary<string, byte[]>();

        /// <summary>
        /// Register an in-memory assembly for later use in compilation, if this is not available it will
        /// fail to compile correctly
        /// </summary>
        /// <param name="name">The name of the assembly</param>
        /// <param name="data">The executable data</param>
        public static void RegisterInMemoryAssembly(AssemblyName name, byte[] data)
        {
            lock (_memoryAssemblyCache)
            {
                _memoryAssemblyCache[name.FullName] = data;
            }
        }

        #region IScriptEngine Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="options"></param>
        public DotNetScriptEngine(CodeDomProvider provider, string options) : base()
        {
            _provider = provider;
            _options = options != null ? options : String.Empty;
        }

        private static byte[] GetDataForAssembly(AssemblyName name)
        {
            lock (_memoryAssemblyCache)
            {
                if (_memoryAssemblyCache.ContainsKey(name.FullName))
                {
                    return _memoryAssemblyCache[name.FullName];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public override ScriptError[] Parse(ScriptContainer container)
        {
            List<ScriptError> errors = new List<ScriptError>();

            try
            {
                CompilerParameters compileParams = new CompilerParameters();
                TempFileCollection tempFiles = new TempFileCollection(Path.GetTempPath(), container.EnableDebug);                

                compileParams.GenerateExecutable = false;
                compileParams.GenerateInMemory = true;                
                compileParams.IncludeDebugInformation = true;
                compileParams.TempFiles = tempFiles;  
                compileParams.ReferencedAssemblies.Add("System.dll");                
                compileParams.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
                compileParams.ReferencedAssemblies.Add("System.Core.dll");
                compileParams.ReferencedAssemblies.Add("System.Numerics.dll");
                
                foreach (AssemblyName name in container.ReferencedNames)
                {
                    Assembly asm = Assembly.Load(name);

                    if (!String.IsNullOrWhiteSpace(asm.Location))
                    {
                        compileParams.ReferencedAssemblies.Add(asm.Location);
                    }
                    else
                    {
                        byte[] assemblyData = GetDataForAssembly(name);

                        if (assemblyData != null)
                        {
                            string fileName = Path.GetTempFileName();
                            File.WriteAllBytes(fileName, assemblyData);

                            tempFiles.AddFile(fileName, false);

                            compileParams.ReferencedAssemblies.Add(fileName);
                        }
                        else
                        {
                            errors.Add(new ScriptError(String.Format(Properties.Resources.DotNetScriptEngine_CannotGetAssemblyLocation, name),
                                Properties.Resources.Scripting_Warning, 1, 1));
                        }
                    }
                }

                if (!String.IsNullOrEmpty(_options))
                {                    
                    compileParams.CompilerOptions = _options;
                }

                List<string> scripts = new List<string>();
                scripts.Add(container.Script);

                string src = CreateScriptContainerSource(container);
                if (!String.IsNullOrWhiteSpace(src))
                {
                    scripts.Add(src);
                }                

                CompilerResults results = _provider.CompileAssemblyFromSource(compileParams, scripts.ToArray());
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError e in results.Errors)
                    {
                        errors.Add(new ScriptError(e.ErrorText, Properties.Resources.Scripting_Error, e.Line, e.Column));
                    }
                }
                else
                {
                    _assembly = results.CompiledAssembly;
                }

                if (container.EnableDebug)
                {
                    foreach (string file in tempFiles)
                    {
                        ScriptUtils.AddTempFile(file);
                    }
                }
            }
            catch (Exception ex)
            {
                errors.Add(new ScriptError(String.Format(Properties.Resources.DotNetScriptEngine_CompileException, ex.Message), Properties.Resources.Scripting_FatalError, 0, 0));
            }

            // Test Code
            foreach(ScriptError error in errors)
            {
                System.Diagnostics.Trace.WriteLine(error.Description);
            }

            return errors.ToArray();
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>Empty string</returns>
        protected virtual string CreateScriptContainerSource(ScriptContainer container)
        {            
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace ns = new CodeNamespace("CANAPE.Scripting");

            CodeTypeDeclaration decl = new CodeTypeDeclaration("Runtime");
            decl.Attributes = MemberAttributes.Public | MemberAttributes.Static;

            CodeTypeReference propType = new CodeTypeReference(typeof(ScriptContainer));

            CodeMemberProperty prop = new CodeMemberProperty();
            prop.Name = "Script";
            prop.Type = propType;
            prop.Attributes = MemberAttributes.Public | MemberAttributes.Static;

            CodeObjectCreateExpression createObject = new CodeObjectCreateExpression(
                    propType, new CodePrimitiveExpression(container.Engine),
                    new CodeObjectCreateExpression(new CodeTypeReference(typeof(Guid)), new CodePrimitiveExpression(container.Uuid.ToString())),
                    new CodePrimitiveExpression(container.Script), new CodePrimitiveExpression(container.EnableDebug));

            prop.GetStatements.Add(new CodeMethodReturnStatement(createObject));
               
            decl.Members.Add(prop);

            ns.Types.Add(decl);

            unit.Namespaces.Add(ns);

            return GenerateCode(unit);        
        }

        /// <summary>
        /// Generate code from a code compile unit
        /// </summary>
        /// <param name="unit">The code compile unit</param>
        /// <returns>The string of code</returns>
        public override string GenerateCode(CodeCompileUnit unit)
        {
            StringBuilder builder = new StringBuilder();            
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.IndentString = "    ";
            options.BlankLinesBetweenMembers = false;
            //options.VerbatimOrder = true;
            TextWriter writer = new StringWriter(builder);
            _provider.GenerateCodeFromCompileUnit(unit, writer, options);

            return builder.ToString();
        }


        #endregion
    }
}
