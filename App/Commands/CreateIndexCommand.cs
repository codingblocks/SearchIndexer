using CommandLine;
using Microsoft.Extensions.Logging;
using SearchIndexer.Outputs.OutputPlugin;
using SearchIndexer.Outputs.OutputPlugin.Requests;

namespace SearchIndexer.App.Commands
{
    public class CreateIndexCommand : ICommand<CreateIndexCommand.Options>
    {
        [Verb("create-index", HelpText = "Create an index")]
        public class Options : IIndexCreateRequest
        {
            [Option('e', "endpoint", Required = true, HelpText = "Full url of the indexer endpoint")]
            public string IndexerEndpoint { get; set; }
            [Option('n', "name", Required = true, HelpText = "Name of the index to create")]
            public string IndexName { get; set; }
            [Option('f', "file", Required = true, HelpText = "File that contains whatever metadata is necessary to create an index")]
            public string IndexDefinitionFilePath { get; set; }
        }

        private IIndexService IndexService { get; }
        private ILogger<GetDocumentsCommand> Logger { get; }

        public CreateIndexCommand(IIndexService indexService, ILogger<GetDocumentsCommand> logger)
        {
            IndexService = indexService;
            Logger = logger;
        }

        public int Execute(Options o)
        {
            var success = IndexService.CreateIndex(o);
            var successMessage = success ? "created" : "creation failed";
            Logger.LogInformation($"{o.IndexName} {successMessage}");
            return success ? 0 : 1;
        }
    }

}
