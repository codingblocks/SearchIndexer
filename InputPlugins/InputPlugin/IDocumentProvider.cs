using System.Collections.Generic;

namespace SearchIndexer.Inputs.InputPlugin
{
    public interface IDocumentProvider
    {
        IEnumerable<IDocument> GetDocuments(IGetDocumentsCommandOptions options);
    }
}
