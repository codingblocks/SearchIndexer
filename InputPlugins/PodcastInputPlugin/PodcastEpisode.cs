using System.Runtime.Serialization;

namespace Podcasts
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
