using System.Collections.Generic;

namespace SearchIndexer.OutputPlugin
{
    public interface IIndexService
    {
        bool CreateIndex<TIndexDefinition>(TIndexDefinition definition);
        TIndexResult GetIndex<TGetIndexDefinition, TIndexResult>(TGetIndexDefinition definition);
        bool DeleteIndex<TDeleteIndexDefinition>(TDeleteIndexDefinition definition);

        bool AddOrUpdateDocuments<TIndexDefinition, TDocument>(TIndexDefinition definition, IEnumerable<TDocument> documents);
        bool DeleteDocuments<TDeleteIndexDefinition, TDeleteDocumentDefinition>(TDeleteIndexDefinition definition, IEnumerable<TDeleteDocumentDefinition> documents);
    }
}
