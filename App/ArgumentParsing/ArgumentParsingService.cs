using System;
using System.Collections.Generic;
using System.Text;

namespace App.ArgumentParsing
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

            if (args.Length == 0)
            {
                ShowUsage();
                throw new Exception("Arguments must be provided on application invoke.");
            }

            _parser.ParseCommandLine(args);

            return parsedParameters;
        }

        public void ShowUsage()
        {
            _parser.ShowUsage();
        }
    }
}
