using SearchIndexer.InputPlugin;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Podcasts
{
    public class PodcastInputService : IDocumentProvider
    {
        IEnumerable<IFeedMetaData> _feeds;
        public PodcastInputService(IEnumerable<IFeedMetaData> feeds )
        {
            _feeds = feeds;
        }

        public IEnumerable<ISerializable> GetDocuments()
        {
            var documents = new List<PodcastEpisode>();
            _feeds.ToList().ForEach(f => GetEpisodes(f));
            return documents;
        }

        private IEnumerable<PodcastEpisode> GetEpisodes(IFeedMetaData feed)
        {
            return new List<PodcastEpisode> { new PodcastEpisode { Title = "Test" } };
        }
    }
}
