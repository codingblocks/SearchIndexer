namespace SearchIndexer.Outputs.OutputPlugin
{
    public interface IIndexCreateRequest
    {
        string Name { get; }
        string FilePath { get; }
    }
}
