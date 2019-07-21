namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
    public interface IIndexUpdateRequest : IIndexEndpoint
    {
        string IndexName { get; }
    }
}
