using Nest;
using SearchIndexer.Inputs.InputPlugin;
using System;
using System.Collections.Generic;

namespace SearchIndexer.Inputs.PodcastInputPlugin.Podcasts
{
    [Serializable]
    public class PodcastEpisode : IDocument
    {
        [Keyword(Name = "id")]
        public string Id { get; set; }

        [Keyword(Name = "feed")]
        public string Feed { get; set; }

        [Text(Name = "podcast_title")]
        public string PodcastTitle { get; set; }

        [Text(Name = "episode_description")]
        public string EpisodeDescription { get; set; }

        [Date(Name = "published")]
        public DateTime? Published { get; set; }

        [Keyword(Name = "audio_url")]
        public string AudioUrl { get; set; }

        [Text(Name = "episode_title")]
        public string EpisodeTitle { get; set; }

        [Keyword(Name = "season")]
        public string Season { get; set; }

        [Number(Name = "episode_number")]
        public int EpisodeNumber { get; set; }

        [Keyword(Name = "episode_type")]
        public string EpisodeType { get; set; }

        [Keyword(Name = "episode_tags")]
        public List<string> EpisodeTags { get; set; }
    }
}
