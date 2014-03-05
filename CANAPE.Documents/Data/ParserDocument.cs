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
using CANAPE.Net;
using CANAPE.Parser;
using CANAPE.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto;

namespace CANAPE.Documents.Data
{
    /// <summary>
    /// A document which represents a parser and associated types
    /// </summary>
    [Serializable]
    public class ParserDocument : BaseParserDocument
    {
        private CompileUnitScriptContainer _container;

        /// <summary>
        /// Constructor
        /// </summary>
        public ParserDocument()
        {            
            _container = new CompileUnitScriptContainer("csharp", Uuid, null, false);
            _script = _container;

            // Add some more assemblies that we can see
            _script.ReferencedNames.Add(typeof(ScriptDocument).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(ProxyNetworkService).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(CertificateUtils).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(AsymmetricCipherKeyPair).Assembly.GetName());
            _script.ReferencedNames.Add(typeof(ExpressionResolver).Assembly.GetName());
        }

        /// <summary>
        /// Update the script container
        /// </summary>
        protected override void UpdateContainer()
        {
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace ns = new CodeNamespace();

            foreach (ParserType type in Types)
            {
                CodeTypeDeclaration decl = type.GetCodeType();

                if (decl != null)
                {
                    ns.Types.Add(decl);
                }
            }

            unit.Namespaces.Add(ns);

            _container.CompileUnit = unit;
        }

        /// <summary>
        /// Called on deserialization
        /// </summary>
        protected override void OnDeserialization()
        {
            base.OnDeserialization();

            try
            {
                UpdateContainer();
            }
            catch
            {
            }
        }
    }
}
