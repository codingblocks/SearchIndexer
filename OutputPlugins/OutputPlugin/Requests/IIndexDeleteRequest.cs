namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
    public interface IIndexDeleteRequest
    {
        string IndexerEndpoint { get; }
        string IndexName { get; }
    }
}
