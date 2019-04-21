namespace SearchIndexer.Outputs.OutputPlugin
{
    public interface IIndexExistsRequest
    {
        string IndexerEndpoint { get; }
        string IndexName { get; }
    }
}
