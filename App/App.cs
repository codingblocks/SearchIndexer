using CommandLine;
using SearchIndexer.App.Commands;
using System;

namespace SearchIndexer.App
{
    public class App
    {
        private IServiceProvider ServiceProvider { get; }
        private ParserResult<object> ParsedCommand { get; }

        public App(ParserResult<object> parsedCommand, IServiceProvider serviceProvider)
        {
            ParsedCommand = parsedCommand;
            ServiceProvider = serviceProvider;
        }

        public int Execute()
        {
            return ParsedCommand.Execute(ServiceProvider);
        }
    }
}
