using CommandLine;
using Microsoft.Extensions.Logging;
using SearchIndexer.Outputs.OutputPlugin;

namespace SearchIndexer.App.Commands
{
    public class IndexExistsCommand : ICommand<IndexExistsCommand.Options>
    {
        [Verb("index-exists", HelpText = "Checks if an index exists")]
        public class Options : IIndexExistsRequest
        {
            [Option('e', "endpoint", Required = true, HelpText = "Full url of the indexer endpoint")]
            public string IndexerEndpoint { get; set; }
            [Option('n', "name", Required = true, HelpText = "Name of the index to check on")]
            public string IndexName { get; set; }
        }

        private IIndexService IndexService { get; }
        private ILogger<IndexExistsCommand> Logger { get; }

        public IndexExistsCommand(IIndexService indexService, ILogger<IndexExistsCommand> logger)
        {
            IndexService = indexService;
            Logger = logger;
        }

        public int Execute(Options o)
        {
            var exists = IndexService.IndexExists(o);
            var existsMessage = exists ? "exists" : "does not exist";
            Logger.LogInformation($"{o.IndexName} {existsMessage}");
            return 0;
        }
    }

}
