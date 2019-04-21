namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
    public interface IIndexCreateRequest
    {
        string IndexerEndpoint { get; }
        string IndexName { get; }
        string IndexDefinitionFilePath { get; }
    }
}
