using System.Collections.Generic;

namespace SearchIndexer.Outputs.OutputPlugin
{
    public interface IIndexService
    {
        bool CreateIndex(IIndexCreateRequest definition);
        bool IndexExists(IIndexExistsRequest definition);
        bool DeleteIndex(IIndexDeleteRequest definition);

        bool AddOrUpdateDocuments<TIndexDefinition, TDocument>(TIndexDefinition definition, IEnumerable<TDocument> documents);
        bool DeleteDocuments<TDeleteIndexDefinition, TDeleteDocumentDefinition>(TDeleteIndexDefinition definition, IEnumerable<TDeleteDocumentDefinition> documents);
    }
}
