using CommandLine;
using Microsoft.Extensions.Logging;
using SearchIndexer.Outputs.OutputPlugin;

namespace SearchIndexer.App.Commands
{
    public class CreateIndexCommand : ICommand<CreateIndexCommand.Options>
    {
        [Verb("create-index", HelpText = "Create an index")]
        public class Options : IIndexCreateRequest
        {
            [Option('n', "name", Required = true, HelpText = "Name of the index to create")]
            public string Name { get; set; }
            [Option('f', "file", Required = true, HelpText = "File that contains whatever metadata is necessary to create an index")]
            public string FilePath { get; set; }
            
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
            Logger.LogInformation($"{o.Name} {successMessage}");
            return success ? 0 : 1;
        }
    }

}
