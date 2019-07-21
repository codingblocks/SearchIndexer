using CommandLine;
using Microsoft.Extensions.Logging;
using SearchIndexer.Inputs.InputPlugin;
using SearchIndexer.Inputs.InputPlugin.Requests;
using System.Linq;

namespace SearchIndexer.App.Commands
{
    public class GetDocumentsCommand : ICommand<GetDocumentsCommand.Options>
    {
        [Verb("get-documents", HelpText = "Add or update files in an index")]
        public class Options : IDocumentGetRequest
        {
            [Option('f', "file", Required = true, HelpText = "File that contains enough meta data to get a set of documents")]
            public string FilePath { get; set; }
            [Option('u', "username", Required = false, HelpText = "User name for authentication")]
            public string UserName { get; set; }
            [Option('p', "password", Required = false, HelpText = "Password for authentication")]
            public string Password { get; set; }
    }

        private IDocumentProvider DocumentProvider { get; }
        private ILogger<GetDocumentsCommand> Logger { get; }

        public GetDocumentsCommand(IDocumentProvider documentProvider, ILogger<GetDocumentsCommand> logger)
        {
            DocumentProvider = documentProvider;
            Logger = logger;
        }

        public int Execute(Options o)
        {
            var documents = DocumentProvider.GetDocuments(o);
            Logger.LogInformation($"{documents.Count()} document(s) found");
            return 0;
        }
    }

}
