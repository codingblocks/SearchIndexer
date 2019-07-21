using CommandLine;
using Microsoft.Extensions.Logging;
using SearchIndexer.Inputs.InputPlugin;
using SearchIndexer.Inputs.InputPlugin.Requests;
using SearchIndexer.Outputs.OutputPlugin;
using SearchIndexer.Outputs.OutputPlugin.Requests;
using System.Linq;

namespace SearchIndexer.App.Commands
{
    public class UpdateDocumentsCommand : ICommand<UpdateDocumentsCommand.Options>
    {
        [Verb("update-documents", HelpText = "Update the documents in an index")]
        public class Options : IIndexUpdateRequest, IDocumentGetRequest
        {
            [Option('e', "endpoint", Required = true, HelpText = "Full url of the indexer endpoint")]
            public string IndexerEndpoint { get; set; }
            [Option('n', "name", Required = true, HelpText = "Name of the index to create")]
            public string IndexName { get; set; }
            [Option('f', "file", Required = true, HelpText = "File that contains enough meta data to get a set of documents")]
            public string FilePath { get; set; }
            [Option('u', "username", Required = false, HelpText = "User name for authentication")]
            public string UserName { get; set; }
            [Option('p', "password", Required = false, HelpText = "Password for authentication")]
            public string Password { get; set; }
    }

        private IDocumentProvider DocumentProvider { get; }
        private IIndexService IndexService { get; }
        private ILogger<GetDocumentsCommand> Logger { get; }

        public UpdateDocumentsCommand(IDocumentProvider documentProvider, IIndexService indexService, ILogger<GetDocumentsCommand> logger)
        {
            DocumentProvider = documentProvider;
            IndexService = indexService;
            Logger = logger;
        }

        public int Execute(Options o)
        {
            var documents = DocumentProvider.GetDocuments(o);

            var success = IndexService.AddOrUpdateDocuments(o, documents);
            var successMessage = success ? "updated" : "failed";
            Logger.LogInformation($"{documents.Count()} document(s) {successMessage} to {o.IndexName}");
            return success ? 0 : 1;
        }
    }

}
