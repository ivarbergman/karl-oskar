using System;
using McMaster.Extensions.CommandLineUtils;

namespace karl_oskar
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.HelpOption();
            var optScriptDirectory = app.Option("-dir|--script-directory <DIRECTORY>", "Path to migration script directory", CommandOptionType.SingleValue);
            optScriptDirectory.IsRequired(true);
            var optionConnectionString = app.Option("-cs|--connectionstring <CONNECTIONSTRING>", "Connection string", CommandOptionType.SingleValue);
            optionConnectionString.IsRequired(true);

            app.OnExecute(() =>
            {
                var dir = optScriptDirectory.HasValue() ? optScriptDirectory.Value() : throw new ArgumentException($"{optScriptDirectory.LongName} is mandatory");
                var cs = optionConnectionString.HasValue() ? optionConnectionString.Value() : throw new ArgumentException($"{optScriptDirectory.LongName} is mandatory");

                var machine = new Machine(cs, dir);
                machine.Run();

                return 0;
            });

            return app.Execute(args);
        }
    }
}
