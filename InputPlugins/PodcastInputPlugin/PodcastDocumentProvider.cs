using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SearchIndexer.Inputs.InputPlugin;
using SearchIndexer.Inputs.InputPlugin.Requests;
using SearchIndexer.Inputs.PodcastInputPlugin.Podcasts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SearchIndexer.Inputs.PodcastInputPlugin
{
    public class PodcastDocumentProvider : IDocumentProvider
    {
        ILogger<PodcastDocumentProvider> Logger { get; }
        public PodcastDocumentProvider(ILogger<PodcastDocumentProvider> logger)
        {
            Logger = logger;
        }

        public IEnumerable<IDocument> GetDocuments(IDocumentGetRequest options)
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

            var episodes = new ConcurrentBag<IDocument>();

            using (var md5 = MD5.Create())
            {
                Parallel.ForEach(feeds.Feeds, f => {
                    LoadEpisodes(md5, f, episodes);
                });
            }
            return episodes; // TODO...
        }

        private void LoadEpisodes(MD5 md5, FeedMetaData feedMetaData, ConcurrentBag<IDocument> destination)
        {
            var name = GetName(feedMetaData);
            var sw = new Stopwatch();
            sw.Start();

            if (string.IsNullOrWhiteSpace(feedMetaData.FeedUrl))
            {
                throw new ArgumentException("Invalid feed, no feedUrl found");
            }

            try
            {
                Logger.LogInformation($"{name} Downloading and parsing");
                var feed = FeedReader.ReadAsync(feedMetaData.FeedUrl).Result;
                sw.Stop();
                Logger.LogInformation($"{name} Loaded the feed, {feed.Items.Count} episode(s) found in {sw.Elapsed.TotalSeconds}s");

                feed.Items.Select(e => new PodcastEpisode
                {
                    Id = GetId(md5, GetAudioUrl(e.SpecificItem)),
                    PodcastTitle = feed.Title,
                    EpisodeDescription = e.Description,
                    EpisodeTitle = e.Title, // TODO regex
                    Published = e.PublishingDate,
                    AudioUrl = GetAudioUrl(e.SpecificItem),
                    // Season = e.Season, // TODO Not Supported by lib
                    // EpisodeNumber = e.Episode, // TODO Not Supported by lib
                    // EpisodeType = e.EpisodeType, // TODO Not Supported by lib
                    // Tags = e.Tags, // TODO Have to build this
                    Feed = feedMetaData.FeedUrl
                }).ToList()
                .ForEach(e => destination.Add(e as IDocument));
            }
            catch (Exception ex)
            {
                sw.Stop();
                Logger.LogError($"{name}: Failure downloading or parsing feed after {sw.Elapsed.TotalSeconds}s");
                Logger.LogError(name + ex.ToString());
                Logger.LogError($"{name} Continuing anyway");
            }
        }

        private string GetAudioUrl(BaseFeedItem specificItem)
        {
            if (specificItem is MediaRssFeedItem mediaItem)
            {
                return mediaItem.Enclosure.Url;
            }

            return specificItem.Link;
        }

        private string GetId(MD5 md5, string audioFileUrl)
        {
            var bytes = new UTF8Encoding().GetBytes(audioFileUrl);
            var hash = md5.ComputeHash(bytes);
            return string.Concat(hash.Select(x => x.ToString("X2")));
        }

        private string GetName(FeedMetaData feedMetaData)
        {
            return feedMetaData.PodcastTitle ?? feedMetaData.FeedUrl;
        }
    }
}
