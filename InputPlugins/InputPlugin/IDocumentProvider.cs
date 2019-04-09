using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SearchIndexer.InputPlugin
{
    public interface IDocumentProvider
    {
        IEnumerable<ISerializable> GetDocuments();
    }
}
