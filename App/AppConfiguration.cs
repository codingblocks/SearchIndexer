using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SearchIndexer.App.ArgumentParsing;

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
                .AddLogging(lb => lb.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug) // TODO Configify me
                .AddTransient<App>()
                .AddSingleton<ICommandArguments>(x => (new ArgumentParsingService()).Parse(_args))
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
