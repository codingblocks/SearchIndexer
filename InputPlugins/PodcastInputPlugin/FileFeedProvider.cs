using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Podcasts
{
    public class FileFeedProvider : IFeedProvider
    {
        public IEnumerable<IFeedMetaData> GetFeeds()
        {
            var filename = Path.Combine(Directory.GetCurrentDirectory(), "\\feeds.json"); ; // TODO this should be external? config based? :shrug:
            using (StreamReader r = new StreamReader(filename))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<IFeedMetaData>>(json);
            }
        }
    }
}
