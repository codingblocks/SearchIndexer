namespace SearchIndexer.App
{
    public interface ICommandArguments
    {
        RunningMode RunningMode { get; set; }
        bool Help { get; set; }
    }
}