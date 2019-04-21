namespace SearchIndexer.Outputs.OutputPlugin
{
    public interface IIndexDeleteRequest
    {
        string IndexerEndpoint { get; }
        string IndexName { get; }
    }
}
