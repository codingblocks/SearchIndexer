using iPodcastSearch;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SearchIndexer.Inputs.InputPlugin;
using SearchIndexer.Inputs.PodcastInputPlugin.Podcasts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;

namespace SearchIndexer.Inputs.PodcastInputPlugin
{
    public class PodcastDocumentProvider : IDocumentProvider
    {
        ILogger<PodcastDocumentProvider> Logger { get; }
        public PodcastDocumentProvider(ILogger<PodcastDocumentProvider> logger)
        {
            Logger = logger;
        }

        public IEnumerable<ISerializable> GetDocuments(IGetDocumentsCommandOptions options)
        {
            var feedFilePath = options.FilePath;
            if (string.IsNullOrWhiteSpace(feedFilePath))
            {
                var errorMessage = "Null or empty file path passed";
                Logger.LogError(errorMessage);
                throw new ArgumentException(errorMessage);
            }
            if (!File.Exists(feedFilePath))
            {
                var errorMessage = "File does not exist";
                Logger.LogError(errorMessage);
                throw new FileNotFoundException(errorMessage);
            }

            Logger.LogInformation($"Valid file found at {feedFilePath}, attempting to read");

            // safe to assume this will be a small document
            var fileText = File.ReadAllText(options.FilePath);
            Logger.LogInformation($"{fileText.Length} characters read, attempting to deserialize as json");
            var feeds = JsonConvert.DeserializeObject<FeedList>(fileText);
            Logger.LogInformation($"{feeds.Feeds.Count()} feeds found");

            var episodes = new List<PodcastEpisode>();

            // TODO parallelize? threadsafe?
            feeds.Feeds.ToList().ForEach(f => episodes.Concat(GetEpisodes(f)));

            return episodes;
        }

        private IEnumerable<PodcastEpisode> GetEpisodes(FeedMetaData feedMetaData)
        {
            if (string.IsNullOrWhiteSpace(feedMetaData.FeedUrl))
            {
                throw new ArgumentException("Invalid feed, no feedUrl found");
            }

            try
            {
                Logger.LogInformation($"Downloading and parsing {feedMetaData.FeedUrl}");
                var feed = PodcastFeedParser.LoadFeedAsync(feedMetaData.FeedUrl).Result;

                Logger.LogInformation($"Loaded feed for {feed.Name}, {feed.EpisodeCount} episodes found");
                return feed.Episodes.Select(e => new PodcastEpisode { Title = feed.Name }); // TODO Other fields
            }
            catch(Exception ex)
            {
                Logger.LogError($"Failure downloading or parsing feed for {feedMetaData.PodcastTitle ?? feedMetaData.FeedUrl}");
                Logger.LogError(ex.ToString());
                Logger.LogError("Continuing anyway");
                return Enumerable.Empty<PodcastEpisode>();
            }

           
        }
    }
}
