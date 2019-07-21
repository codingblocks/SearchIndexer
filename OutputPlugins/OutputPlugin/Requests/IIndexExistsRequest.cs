namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
    public interface IIndexExistsRequest : IIndexEndpoint
    {
        string IndexName { get; }
    }
}
