using SearchIndexer.Outputs.OutputPlugin.Requests;
using System.Collections.Generic;

namespace SearchIndexer.Outputs.OutputPlugin
{
    public interface IIndexService
    {
        bool CreateIndex(IIndexCreateRequest request);
        bool IndexExists(IIndexExistsRequest request);
        bool DeleteIndex(IIndexDeleteRequest request);
        bool AddOrUpdateDocuments<T>(IIndexUpdateRequest request, IEnumerable<T> documents) where T : class;
    }
}
