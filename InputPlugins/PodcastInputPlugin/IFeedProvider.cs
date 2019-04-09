using System.Collections.Generic;

namespace Podcasts
{
    public interface IFeedProvider
    {
        IEnumerable<IFeedMetaData> GetFeeds();
    }
}
