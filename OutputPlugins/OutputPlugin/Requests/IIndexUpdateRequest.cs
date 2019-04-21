namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
    public interface IIndexUpdateRequest
    {
        string IndexerEndpoint { get; }
        string IndexName { get; }
    }
}
