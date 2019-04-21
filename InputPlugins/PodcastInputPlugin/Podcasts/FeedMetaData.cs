using Newtonsoft.Json;

namespace SearchIndexer.Inputs.PodcastInputPlugin.Podcasts
{
    internal class FeedMetaData
    {
        [JsonProperty(PropertyName = "titleCleanser")]
        public string TitleCleanser { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string FeedUrl { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string PodcastTitle { get; set; }

        [JsonProperty(PropertyName = "forceHttps")]
        public bool ForceHttps { get; set; }
    }
}
