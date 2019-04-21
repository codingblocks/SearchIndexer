namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
    public interface IIndexExistsRequest
    {
        string IndexerEndpoint { get; }
        string IndexName { get; }
    }
}
