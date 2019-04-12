using Newtonsoft.Json;
using System.Collections.Generic;

namespace SearchIndexer.Inputs.PodcastInputPlugin.Podcasts
{
    internal class FeedList
    {
        [JsonProperty(PropertyName = "feeds")]
        internal List<FeedMetaData> Feeds { get; set; }
    }
}
