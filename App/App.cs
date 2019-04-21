using CommandLine;
using SearchIndexer.App.Commands;

namespace SearchIndexer.App
{
    public class App
    {
        private GetDocumentsCommand GetDocumentsCommand { get; }
        private CreateIndexCommand CreateIndexCommand { get; }
        private ParserResult<object> Options { get; }

        public App(GetDocumentsCommand getDocumentsCommand, CreateIndexCommand createIndexCommand, ParserResult<object> options) // eventually , IIndexService indexService
        {
            GetDocumentsCommand = getDocumentsCommand;
            CreateIndexCommand = createIndexCommand;
            Options = options;
        }

        public int Run()
        {
            var result = Options.MapResult(
                (GetDocumentsCommand.Options o) => GetDocumentsCommand.Execute(o),
                (CreateIndexCommand.Options o) => CreateIndexCommand.Execute(o),
                errs => 1
            );
            return result;
        }
    }
}
