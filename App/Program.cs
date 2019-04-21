using Microsoft.Extensions.DependencyInjection;

namespace SearchIndexer.App
{
    class Program
    {
        static int Main(string[] args)
        {
            var configuration = new AppConfiguration(args);
            using (var serviceProvider = configuration.ConfigureServiceProvider())
            {
                return serviceProvider.GetService<App>().Execute();
            }
        }
    }
}