using CommandLine;
using SearchIndexer.Inputs.InputPlugin;

namespace SearchIndexer.App.Options
{
    [Verb("get-documents", HelpText = "Add or update files in an index")]
    public class GetDocumentsOptions : IGetDocumentsCommandOptions
    {
        [Option('f', "file", Required = true, HelpText = "File that contains enough meta data to get a set of documents")]
        public string FilePath { get; set; }
    }
}
