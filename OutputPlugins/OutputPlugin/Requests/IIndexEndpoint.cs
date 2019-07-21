namespace SearchIndexer.Outputs.OutputPlugin.Requests
{
  public interface IIndexEndpoint
  {
    string IndexerEndpoint { get; }
    string UserName { get; }
    string Password { get; }
  }
}
