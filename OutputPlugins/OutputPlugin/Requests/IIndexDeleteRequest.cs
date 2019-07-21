namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
    public interface IIndexDeleteRequest : IIndexEndpoint
  {
        string IndexName { get; }
  }
}
