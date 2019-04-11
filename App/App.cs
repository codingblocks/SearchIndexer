using System;
using Microsoft.Extensions.Logging;

namespace SearchIndexer.App
{
    public class App
    {
        private ILogger _logger;
        private ICommandArguments _parsedArguments;

        public App(ILogger<App> logger, ICommandArguments parsedArguments)
        {
            _logger = logger;
            _parsedArguments = parsedArguments;
        }

        public void Run()
        {
            _logger.LogInformation("Application is running");
            switch (_parsedArguments.RunningMode)
            {
                case RunningMode.Create:
                {
                    _logger.LogInformation("Doing some create-y stuff");
                    break;
                }
                case RunningMode.Delete:
                {
                    _logger.LogInformation("Doing some delete-y stuff");
                    break;
                }
                case RunningMode.Get:
                {
                    _logger.LogInformation("Doing some get-y stuff");
                    break;
                }
                case RunningMode.Update:
                {
                    _logger.LogInformation("Doing some update-y stuff");
                    break;
                }
                case RunningMode.None:
                {
                    _logger.LogError("No running mode was provided.");
                    throw new ArgumentNullException(nameof(_parsedArguments.RunningMode));
                }
                // anything else?
                default:
                    {
                        _logger.LogInformation("This should never happen.");
                        break;
                    }
            }
        }
    }
}
