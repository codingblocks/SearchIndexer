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

        [JsonProperty(PropertyName = "feed")]
        public string Feed { get; set; }

        [JsonProperty(PropertyName = "podcast_title")]
        public string PodcastTitle { get; set; }

        [JsonProperty(PropertyName = "episode_description")]
        public string EpisodeDescription { get; set; }

        [JsonProperty(PropertyName = "published")]
        public DateTime? Published { get; set; }

        [JsonProperty(PropertyName = "audio_url")]
        public string AudioUrl { get; set; }

        [JsonProperty(PropertyName = "episode_title")]
        public string EpisodeTitle { get; set; }

        [JsonProperty(PropertyName = "season")]
        public string Season { get; set; }

        [JsonProperty(PropertyName = "episode_number")]
        public string EpisodeNumber { get; set; }

        [JsonProperty(PropertyName = "episode_type")]
        public string EpisodeType { get; set; }

        [JsonProperty(PropertyName = "episode_tags")]
        public List<string> EpisodeTags { get; set; }
    }
}
