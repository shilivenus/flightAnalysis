using System;
using System.IO;
using AirlineFlightDataService.Business.EventHandler;
using AirlineFlightDataService.Business.Logger;
using Microsoft.Extensions.Configuration;

namespace AirlineFlightDataService.Business.Watcher
{
    public class FlightWatcher : IWatcher
    {
        private readonly IEventHandler _eventHandler;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public FlightWatcher(IEventHandler eventHandler, IConfiguration configuration, ILogger logger)
        {
            _eventHandler = eventHandler;
            _configuration = configuration;
            _logger = logger;
        }

        public void Run()
        {
            try
            {
                // Create a new FileSystemWatcher and set its properties.
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    var sourceFileFolder = _configuration["source"];

                    if (!Directory.Exists(sourceFileFolder))
                        throw new Exception($"{sourceFileFolder} does not exist.");

                    watcher.Path = sourceFileFolder;

                    // Watch for create new files.
                    watcher.NotifyFilter = NotifyFilters.FileName;

                    // Only watch json files.
                    watcher.Filter = "*.json";

                    // Add event handlers.
                    watcher.Created += _eventHandler.OnCreated;

                    // Begin watching.
                    watcher.EnableRaisingEvents = true;

                    // Wait for the user to quit the program.
                    _logger.LogInfoToConsole("Press 'q' to quit the sample.");
                    while (Console.Read() != 'q') ;
                }
            }
            catch (Exception e)
            {
                _logger.LogErrorToConsole(e, e.Message);
                throw;
            }
        }
    }
}

