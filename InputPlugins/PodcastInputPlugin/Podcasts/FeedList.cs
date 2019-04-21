using Newtonsoft.Json;
using System.Collections.Generic;

namespace SearchIndexer.Inputs.PodcastInputPlugin.Podcasts
{
    internal class FeedList
    {
        [JsonProperty(PropertyName = "feeds")]
        internal IEnumerable<FeedMetaData> Feeds { get; set; }
    }
}
