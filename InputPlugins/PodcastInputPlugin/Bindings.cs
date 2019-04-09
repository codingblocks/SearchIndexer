using Microsoft.Extensions.DependencyInjection;
namespace Podcasts
{
    public class Bindings
    {
        public void Configure(ServiceCollection services)
        {
            services.AddTransient<IFeedProvider, FileFeedProvider>(); // TODO: Should be configurable
        }
    }
}
