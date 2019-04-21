using CommandLine;
using ElasticsearchOutputPlugin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SearchIndexer.App.Commands;
using SearchIndexer.Inputs.InputPlugin;
using SearchIndexer.Inputs.PodcastInputPlugin;
using SearchIndexer.Outputs.OutputPlugin;

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
                .AddTransient<IIndexService, ElasticsearchIndexService>() // TODO config based
                .ConfigureCommands(_args)
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
