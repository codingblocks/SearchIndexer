using CommandLine;
using Microsoft.Extensions.Logging;
using SearchIndexer.App.Options;
using SearchIndexer.Inputs.InputPlugin;
using System.Linq;

namespace SearchIndexer.App
{
    public class App
    {
        private ILogger<App> Logger { get; }
        private IDocumentProvider DocumentProvider { get; }
        private ParserResult<GetDocumentsOptions> Options { get; }

        public App(ILogger<App> logger, IDocumentProvider documentProvider, ParserResult<GetDocumentsOptions> options) // eventually , IIndexService indexService
        {
            Logger = logger;
            DocumentProvider = documentProvider;
            Options = options;
        }

        public void Run()
        {
            Options.MapResult(
            (GetDocumentsOptions options) => {
                var documents = DocumentProvider.GetDocuments(options);
                Logger.LogInformation($"Got {documents.Count()} document(s) found");
                return 0;
            },
            errs => 1);
        }
    }
}
