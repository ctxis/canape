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
using System.Collections.Generic;
using System.IO;
using CANAPE.Documents.Net;
using CANAPE.Net;
using CANAPE.Nodes;
using CANAPE.Parser;
using CANAPE.Scripting;
using CANAPE.Security.Cryptography.X509Certificates;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Hosting.Providers;
using Microsoft.Scripting.Hosting.Shell;

namespace CANAPE.Cli
{
    internal sealed class Program : ConsoleHost
    {
        protected override CommandLine CreateCommandLine()
        {
            return new PythonCommandLine();
        }

        protected override void ExecuteInternal()
        {
            PythonContext ctx = HostingHelpers.GetLanguageContext(Engine) as PythonContext;
            (HostingHelpers.GetLanguageContext(base.Engine) as PythonContext).SetModuleState(typeof(ScriptEngine), base.Engine);

            ctx.SetModuleState(typeof(ScriptEngine), Engine);
            Runtime.LoadAssembly(typeof(NetGraph).Assembly);
            Runtime.LoadAssembly(typeof(NetGraphDocument).Assembly);
            Runtime.LoadAssembly(typeof(ProxyNetworkService).Assembly);
            Runtime.LoadAssembly(typeof(ExpressionResolver).Assembly);
            Runtime.LoadAssembly(typeof(Program).Assembly);
            Runtime.LoadAssembly(typeof(BaseDataEndpoint).Assembly);                        
            Runtime.LoadAssembly(typeof(CertificateUtils).Assembly);
            Runtime.LoadAssembly(typeof(Org.BouncyCastle.Asn1.Asn1Encodable).Assembly);            
            ICollection<string> searchPaths = Engine.GetSearchPaths();
            string item = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PythonLib");
            searchPaths.Add(item);
            Engine.SetSearchPaths(searchPaths);
            base.ExecuteInternal();
        }

        protected override IConsole CreateConsole(ScriptEngine engine, CommandLine commandLine, ConsoleOptions options)
        {
            PythonConsoleOptions options2 = (PythonConsoleOptions)options;
            if (!options2.BasicConsole)
            {
                return new SuperConsole(commandLine, options.ColorfulConsole);
            }
            return new BasicConsole(options.ColorfulConsole);
        }

        protected override OptionsParser CreateOptionsParser()
        {
            return new PythonOptionsParser();
        }

        protected override ScriptRuntimeSetup CreateRuntimeSetup()
        {
            ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
            foreach (LanguageSetup setup2 in setup.LanguageSetups)
            {
                if (setup2.FileExtensions.Contains(".py"))
                {
                    setup2.Options["SearchPaths"] = new string[0];
                }
            }
            return setup;
        }

        [STAThread]
        public static int Main(string[] args)
        {            
            if (Environment.GetEnvironmentVariable("TERM") == null)
            {
                Environment.SetEnvironmentVariable("TERM", "dumb");
            }
            return new Program().Run(args);
        }

        protected override void ParseHostOptions(string[] args)
        {
            foreach (string str in args)
            {
                base.Options.IgnoredArgs.Add(str);
            }
        }

        protected override Type Provider
        {
            get
            {
                return typeof(PythonContext);
            }
        }        
    }

}
