using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SearchIndexer.Inputs.InputPlugin
{
    public interface IDocumentProvider
    {
        IEnumerable<ISerializable> GetDocuments(IGetDocumentsCommandOptions options);
    }
}
