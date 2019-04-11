using System;

namespace SearchIndexer.App.ArgumentParsing
{
    public class ArgumentParsingService
    {
        private readonly CommandLineParser.CommandLineParser _parser;

        public ArgumentParsingService()
        {
            _parser = new CommandLineParser.CommandLineParser();
        }

        public ArgumentParsingTarget Parse(string[] args)
        {
            var parsedParameters = new ArgumentParsingTarget();
            _parser.ExtractArgumentAttributes(parsedParameters);

            try
            {
                _parser.ParseCommandLine(args);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to parse command line arguments. See usage and original exception below.");
                ShowUsage();
                Console.WriteLine(ex);
            }

            return parsedParameters;
        }

        public void ShowUsage()
        {
            _parser.ShowUsage();
        }
    }
}
