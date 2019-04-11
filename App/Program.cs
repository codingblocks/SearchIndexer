using System;
using Microsoft.Extensions.DependencyInjection;

namespace SearchIndexer.App
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Configure(args);
            Execute();
        }

        private static void Configure(string[] args)
        {
            var configuration = new AppConfiguration(args);
            _serviceProvider = configuration.ConfigureServiceProvider();
        }

        private static void Execute()
        {
            _serviceProvider.GetService<App>().Run();
            Console.WriteLine("Press any key to terminate application.");
            Console.ReadKey();
        }

    }
}
