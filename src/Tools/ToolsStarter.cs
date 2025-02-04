using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Env = System.Environment;
using Simple;
using log4net;
using System.Reflection;
using Simple.Generator.Console;
using Locadora.Tools.Templates;
using Locadora.Tools.Templates.AutoContracts;

namespace Locadora.Tools
{
    

    public static class ToolsStarter
    {
        public static void Main(string[] args)
        {
            new Configurator().ChangeToRoot();
            Console.WriteLine("Dir: '{0}'.", Env.CurrentDirectory);

            VerifyContracts(args);

            var env = args.FirstOrDefault();
            if (env.With(x => x.Trim()) == "*") env = null;

            new ConsoleCommandReader(new Context(env), 
                Configurator.IsProduction).Run(args.Skip(1));
        }

        private static void VerifyContracts(string[] args)
        {
            if (args.Length == 0 && !Configurator.IsProduction)
                new AutoContractsTemplate().Verify();
        }


    }
}
