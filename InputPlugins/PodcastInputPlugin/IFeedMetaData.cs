namespace Podcasts
{
    public interface IFeedMetaData
    {
        string TitleCleanser { get; }
        string FeedUrl { get; }
        string PodcastTitle { get; }
        bool ForceHttps { get; }
    }
}
