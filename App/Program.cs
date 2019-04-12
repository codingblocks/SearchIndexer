using Microsoft.Extensions.DependencyInjection;

namespace SearchIndexer.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new AppConfiguration(args);
            using (var serviceProvider = configuration.ConfigureServiceProvider())
            {
                serviceProvider.GetService<App>().Run();
            }
        }
    }
}