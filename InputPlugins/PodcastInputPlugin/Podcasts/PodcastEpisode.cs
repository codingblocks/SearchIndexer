using System.Runtime.Serialization;

namespace SearchIndexer.Inputs.PodcastInputPlugin.Podcasts
{
    public class PodcastEpisode : ISerializable
    {
        public string Title { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
