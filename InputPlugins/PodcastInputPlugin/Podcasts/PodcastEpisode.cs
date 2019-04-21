using Newtonsoft.Json;
using SearchIndexer.Inputs.InputPlugin;
using System;
using System.Collections.Generic;

namespace SearchIndexer.Inputs.PodcastInputPlugin.Podcasts
{
    [Serializable]
    public class PodcastEpisode : IDocument
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "podcastTitle")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "published")]
        public DateTime? Published { get; set; }

        [JsonProperty(PropertyName = "audioUrl")]
        public string AudioUrl { get; set; }

        [JsonProperty(PropertyName = "episode")]
        public string Episode { get; set; }

        [JsonProperty(PropertyName = "season")]
        public string Season { get; set; }

        [JsonProperty(PropertyName = "feed")]
        public string Feed { get; set; }

        [JsonProperty(PropertyName = "episodeType")]
        public string EpisodeType { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public IEnumerable<string> Tags { get; set; }
    }
}
