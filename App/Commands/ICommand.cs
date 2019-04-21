namespace SearchIndexer.App.Commands
{
    public interface ICommand<T>
    {
        int Execute(T o);
    }
}