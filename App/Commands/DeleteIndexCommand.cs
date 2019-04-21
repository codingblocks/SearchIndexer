using CommandLine;
using Microsoft.Extensions.Logging;
using SearchIndexer.Outputs.OutputPlugin;
using SearchIndexer.Outputs.OutputPlugin.Requests;

namespace SearchIndexer.App.Commands
{
    public class DeleteIndexCommand : ICommand<DeleteIndexCommand.Options>
    {
        [Verb("delete-index", HelpText = "Delete an index")]
        public class Options : IIndexDeleteRequest
        {
            [Option('e', "endpoint", Required = true, HelpText = "Full url of the indexer endpoint")]
            public string IndexerEndpoint { get; set; }
            [Option('n', "name", Required = true, HelpText = "Name of the index to delete")]
            public string IndexName { get; set; }
        }

        private IIndexService IndexService { get; }
        private ILogger<GetDocumentsCommand> Logger { get; }

        public DeleteIndexCommand(IIndexService indexService, ILogger<GetDocumentsCommand> logger)
        {
            IndexService = indexService;
            Logger = logger;
        }

        public int Execute(Options o)
        {
            var success = IndexService.DeleteIndex(o);
            var successMessage = success ? "deleted" : "delete failed";
            Logger.LogInformation($"{o.IndexName} {successMessage}");
            return success ? 0 : 1;
        }
    }

}
