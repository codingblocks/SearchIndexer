using System;
using App;
using App.ArgumentParsing;

namespace SearchIndexer.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var argumentParser = new ArgumentParsingService();
            var parsedArguments = new ArgumentParsingTarget();

            try
            {
                parsedArguments = argumentParser.Parse(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            switch (parsedArguments.RunningMode)
            {
                case RunningMode.Create:
                {
                    Console.WriteLine("Doing some create-y stuff");
                    break;
                }
                case RunningMode.Delete:
                {
                    Console.WriteLine("Doing some delete-y stuff");
                    break;
                }
                case RunningMode.Get:
                {
                    Console.WriteLine("Doing some get-y stuff");
                    break;
                }
                case RunningMode.Update:
                {
                    Console.WriteLine("Doing some update-y stuff");
                    break;
                }
                // anything else?
                default:
                {
                    Console.WriteLine("This should never happen.");
                    break;
                }
            }
            
            Console.WriteLine("I don't do anything yet!");
        }
    }
}
