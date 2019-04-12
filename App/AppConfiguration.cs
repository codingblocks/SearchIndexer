using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SearchIndexer.App.Options;
using SearchIndexer.Inputs.InputPlugin;
using SearchIndexer.Inputs.PodcastInputPlugin;

namespace SearchIndexer.App
{
    internal class AppConfiguration
    {
        private string[] _args;

        internal AppConfiguration(string[] args)
        {
            _args = args;
        }

        internal ServiceProvider ConfigureServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(lb =>
                {
                    lb.AddConsole();
                    lb.AddDebug();
                })
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug) // TODO Configify me
                .AddTransient<App>()
                .AddTransient<IDocumentProvider, PodcastDocumentProvider>() // TODO config based
                .AddSingleton<ParserResult<GetDocumentsOptions>>(Parser.Default.ParseArguments<GetDocumentsOptions>(_args)) // TODO Hard-Coded 3p, wrap me!
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
