namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
    public interface IIndexCreateRequest : IIndexEndpoint
  {
        string IndexName { get; }
        string IndexDefinitionFilePath { get; }
  }
}
